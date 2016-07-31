using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SystemPageText : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SystemPageText_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SystemPageText, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SystemPageText_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SystemPageText.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SystemPageText_Id"] != null)
      {
        FormView_SystemPageText_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SystemPageText_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SystemPageText_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_SystemPageText_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SystemPageText_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_SystemPageText_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_SystemPageTextId"];
      string SearchField2 = Request.QueryString["Search_SystemPageTextIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SystemPageText_Id=" + Request.QueryString["Search_SystemPageTextId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SystemPageText_IsActive=" + Request.QueryString["Search_SystemPageTextIsActive"] + "&";
      }

      string FinalURL = "Administration_SystemPageText_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Page Text List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SystemPageText_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SystemPageText_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          TextBox TextBox_InsertDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_InsertDescription");
          AjaxControlToolkit.HTMLEditor.Editor Editor_InsertText = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemPageText_Form.FindControl("Editor_InsertText");

          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_Description"].DefaultValue = Server.HtmlEncode(TextBox_InsertDescription.Text);
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_Text"].DefaultValue = Editor_InsertText.Content;
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_History"].DefaultValue = "";
          SqlDataSource_SystemPageText_Form.InsertParameters["SystemPageText_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_InsertDescription");
      AjaxControlToolkit.HTMLEditor.Editor Editor_InsertText = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemPageText_Form.FindControl("Editor_InsertText");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_InsertText.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Description and Text fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["SystemPageTextDescription"] = "";
        string SQLStringSystemPageTextDescription = "SELECT SystemPageText_Description FROM Administration_SystemPageText WHERE SystemPageText_Description = @SystemPageText_Description AND SystemPageText_IsActive = @SystemPageText_IsActive";
        using (SqlCommand SqlCommand_SystemPageTextDescription = new SqlCommand(SQLStringSystemPageTextDescription))
        {
          SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_Description", TextBox_InsertDescription.Text.ToString());
          SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_IsActive", 1);
          DataTable DataTable_SystemPageTextDescription;
          using (DataTable_SystemPageTextDescription = new DataTable())
          {
            DataTable_SystemPageTextDescription.Locale = CultureInfo.CurrentCulture;
            DataTable_SystemPageTextDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemPageTextDescription).Copy();
            if (DataTable_SystemPageTextDescription.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SystemPageTextDescription.Rows)
              {
                Session["SystemPageTextDescription"] = DataRow_Row["SystemPageText_Description"];
              }
            }
            else
            {
              Session["SystemPageTextDescription"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SystemPageTextDescription"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A System Page Text with the Description '" + Session["SystemPageTextDescription"].ToString() + "' already exists<br />";
        }

        Session["SystemPageTextDescription"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemPageText_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SystemPageText_Id"] = e.Command.Parameters["@SystemPageText_Id"].Value;

        string SearchField1 = "s_SystemPageText_Id=" + Session["SystemPageText_Id"].ToString() + "";
        string FinalURL = "Administration_SystemPageText_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Page Text List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SystemPageText_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSystemPageTextModifiedDate"] = e.OldValues["SystemPageText_ModifiedDate"];
        object OLDSystemPageTextModifiedDate = Session["OLDSystemPageTextModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSystemPageTextModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSystemPageText = (DataView)SqlDataSource_SystemPageText_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSystemPageText = DataView_CompareSystemPageText[0];
        Session["DBSystemPageTextModifiedDate"] = Convert.ToString(DataRowView_CompareSystemPageText["SystemPageText_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSystemPageTextModifiedBy"] = Convert.ToString(DataRowView_CompareSystemPageText["SystemPageText_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSystemPageTextModifiedDate = Session["DBSystemPageTextModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSystemPageTextModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSystemPageTextModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SystemPageText_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SystemPageText_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SystemPageText_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SystemPageText_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            TextBox TextBox_EditDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_EditDescription");
            AjaxControlToolkit.HTMLEditor.Editor Editor_EditText = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemPageText_Form.FindControl("Editor_EditText");

            e.NewValues["SystemPageText_Description"] = Server.HtmlEncode(TextBox_EditDescription.Text);
            e.NewValues["SystemPageText_Text"] = Editor_EditText.Content;
            e.NewValues["SystemPageText_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SystemPageText_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SystemPageText", "SystemPageText_Id = " + Request.QueryString["SystemPageText_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SystemPageText_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SystemPageTextHistory"] = Convert.ToString(DataRowView_Form["SystemPageText_History"], CultureInfo.CurrentCulture);

            Session["SystemPageTextHistory"] = Session["History"].ToString() + Session["SystemPageTextHistory"].ToString();
            e.NewValues["SystemPageText_History"] = Session["SystemPageTextHistory"].ToString();

            Session["SystemPageTextHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSystemPageTextModifiedDate"] = "";
        Session["DBSystemPageTextModifiedDate"] = "";
        Session["DBSystemPageTextModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_EditDescription");
      AjaxControlToolkit.HTMLEditor.Editor Editor_EditText = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemPageText_Form.FindControl("Editor_EditText");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_EditText.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Description and Text fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["SystemPageTextId"] = "";
        Session["SystemPageTextDescription"] = "";
        string SQLStringSystemPageTextDescription = "SELECT SystemPageText_Id , SystemPageText_Description FROM Administration_SystemPageText WHERE SystemPageText_Description = @SystemPageText_Description AND SystemPageText_IsActive = @SystemPageText_IsActive";
        using (SqlCommand SqlCommand_SystemPageTextDescription = new SqlCommand(SQLStringSystemPageTextDescription))
        {
          SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_Description", TextBox_EditDescription.Text.ToString());
          SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_IsActive", 1);
          DataTable DataTable_SystemPageTextDescription;
          using (DataTable_SystemPageTextDescription = new DataTable())
          {
            DataTable_SystemPageTextDescription.Locale = CultureInfo.CurrentCulture;
            DataTable_SystemPageTextDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemPageTextDescription).Copy();
            if (DataTable_SystemPageTextDescription.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SystemPageTextDescription.Rows)
              {
                Session["SystemPageTextId"] = DataRow_Row["SystemPageText_Id"];
                Session["SystemPageTextDescription"] = DataRow_Row["SystemPageText_Description"];
              }
            }
            else
            {
              Session["SystemPageTextId"] = "";
              Session["SystemPageTextDescription"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SystemPageTextDescription"].ToString()))
        {
          if (Session["SystemPageTextId"].ToString() != Request.QueryString["SystemPageText_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A System Page Text with the Description '" + Session["SystemPageTextDescription"].ToString() + "' already exists<br />";
          }
        }

        Session["SystemPageTextId"] = "";
        Session["SystemPageTextDescription"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemPageText_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SystemPageText_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_InsertDescription");
      Label Label_InsertDescriptionError = (Label)FormView_SystemPageText_Form.FindControl("Label_InsertDescriptionError");

      Session["SystemPageTextDescription"] = "";
      string SQLStringSystemPageTextDescription = "SELECT SystemPageText_Description FROM Administration_SystemPageText WHERE SystemPageText_Description = @SystemPageText_Description AND SystemPageText_IsActive = @SystemPageText_IsActive";
      using (SqlCommand SqlCommand_SystemPageTextDescription = new SqlCommand(SQLStringSystemPageTextDescription))
      {
        SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_Description", TextBox_InsertDescription.Text.ToString());
        SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_IsActive", 1);
        DataTable DataTable_SystemPageTextDescription;
        using (DataTable_SystemPageTextDescription = new DataTable())
        {
          DataTable_SystemPageTextDescription.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemPageTextDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemPageTextDescription).Copy();
          if (DataTable_SystemPageTextDescription.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemPageTextDescription.Rows)
            {
              Session["SystemPageTextDescription"] = DataRow_Row["SystemPageText_Description"];
            }
          }
          else
          {
            Session["SystemPageTextDescription"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SystemPageTextDescription"].ToString()))
      {
        Label_InsertDescriptionError.Text = "";
      }
      else
      {
        Label_InsertDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A System Page Text with the Description '" + Session["SystemPageTextDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["SystemPageTextDescription"] = "";
    }

    protected void TextBox_EditDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditDescription = (TextBox)FormView_SystemPageText_Form.FindControl("TextBox_EditDescription");
      Label Label_EditDescriptionError = (Label)FormView_SystemPageText_Form.FindControl("Label_EditDescriptionError");

      Session["SystemPageTextId"] = "";
      Session["SystemPageTextDescription"] = "";
      string SQLStringSystemPageTextDescription = "SELECT SystemPageText_Id , SystemPageText_Description FROM Administration_SystemPageText WHERE SystemPageText_Description = @SystemPageText_Description AND SystemPageText_IsActive = @SystemPageText_IsActive";
      using (SqlCommand SqlCommand_SystemPageTextDescription = new SqlCommand(SQLStringSystemPageTextDescription))
      {
        SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_Description", TextBox_EditDescription.Text.ToString());
        SqlCommand_SystemPageTextDescription.Parameters.AddWithValue("@SystemPageText_IsActive", 1);
        DataTable DataTable_SystemPageTextDescription;
        using (DataTable_SystemPageTextDescription = new DataTable())
        {
          DataTable_SystemPageTextDescription.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemPageTextDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemPageTextDescription).Copy();
          if (DataTable_SystemPageTextDescription.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemPageTextDescription.Rows)
            {
              Session["SystemPageTextId"] = DataRow_Row["SystemPageText_Id"];
              Session["SystemPageTextDescription"] = DataRow_Row["SystemPageText_Description"];
            }
          }
          else
          {
            Session["SystemPageTextId"] = "";
            Session["SystemPageTextDescription"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SystemPageTextDescription"].ToString()))
      {
        Label_EditDescriptionError.Text = "";
      }
      else
      {
        if (Session["SystemPageTextId"].ToString() == Request.QueryString["SystemPageText_Id"])
        {
          Label_EditDescriptionError.Text = "";
        }
        else
        {
          Label_EditDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A System Page Text with the Description '" + Session["SystemPageTextDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["SystemPageTextId"] = "";
      Session["SystemPageTextDescription"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}