using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Navigation : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Navigation_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Navigation, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Navigation_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Navigation.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Navigation_Id"] != null)
      {
        FormView_Navigation_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Navigation_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Navigation_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Navigation_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_Navigation_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Navigation_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_NavigationId"];
      string SearchField2 = Request.QueryString["Search_NavigationIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Navigation_Id=" + Request.QueryString["Search_NavigationId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Navigation_IsActive=" + Request.QueryString["Search_NavigationIsActive"] + "&";
      }

      string FinalURL = "Administration_Navigation_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Navigation List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Navigation_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Navigation_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          TextBox TextBox_InsertName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_InsertName");
          AjaxControlToolkit.HTMLEditor.Editor Editor_InsertDescription = (AjaxControlToolkit.HTMLEditor.Editor)FormView_Navigation_Form.FindControl("Editor_InsertDescription");

          SqlDataSource_Navigation_Form.InsertParameters["Navigation_Name"].DefaultValue = Server.HtmlEncode(TextBox_InsertName.Text);
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_Description"].DefaultValue = Editor_InsertDescription.Content;
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_History"].DefaultValue = "";
          SqlDataSource_Navigation_Form.InsertParameters["Navigation_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_InsertName");
      AjaxControlToolkit.HTMLEditor.Editor Editor_InsertDescription = (AjaxControlToolkit.HTMLEditor.Editor)FormView_Navigation_Form.FindControl("Editor_InsertDescription");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_InsertDescription.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Name and Description fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["NavigationName"] = "";
        string SQLStringNavigationName = "SELECT Navigation_Name FROM Administration_Navigation WHERE Navigation_Name = @Navigation_Name AND Navigation_IsActive = @Navigation_IsActive";
        using (SqlCommand SqlCommand_NavigationName = new SqlCommand(SQLStringNavigationName))
        {
          SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_IsActive", 1);
          DataTable DataTable_NavigationName;
          using (DataTable_NavigationName = new DataTable())
          {
            DataTable_NavigationName.Locale = CultureInfo.CurrentCulture;
            DataTable_NavigationName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NavigationName).Copy();
            if (DataTable_NavigationName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_NavigationName.Rows)
              {
                Session["NavigationName"] = DataRow_Row["Navigation_Name"];
              }
            }
            else
            {
              Session["NavigationName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["NavigationName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Navigation with the Name '" + Session["NavigationName"].ToString() + "' already exists<br />";
        }

        Session["NavigationName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Navigation_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Navigation_Id"] = e.Command.Parameters["@Navigation_Id"].Value;

        string SearchField1 = "s_Navigation_Id=" + Session["Navigation_Id"].ToString() + "";
        string FinalURL = "Administration_Navigation_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Navigation List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Navigation_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDNavigationModifiedDate"] = e.OldValues["Navigation_ModifiedDate"];
        object OLDNavigationModifiedDate = Session["OLDNavigationModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDNavigationModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareNavigation = (DataView)SqlDataSource_Navigation_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareNavigation = DataView_CompareNavigation[0];
        Session["DBNavigationModifiedDate"] = Convert.ToString(DataRowView_CompareNavigation["Navigation_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBNavigationModifiedBy"] = Convert.ToString(DataRowView_CompareNavigation["Navigation_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBNavigationModifiedDate = Session["DBNavigationModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBNavigationModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBNavigationModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Navigation_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Navigation_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_Navigation_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Navigation_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            TextBox TextBox_EditName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_EditName");
            AjaxControlToolkit.HTMLEditor.Editor Editor_EditDescription = (AjaxControlToolkit.HTMLEditor.Editor)FormView_Navigation_Form.FindControl("Editor_EditDescription");

            e.NewValues["Navigation_Name"] = Server.HtmlEncode(TextBox_EditName.Text);
            e.NewValues["Navigation_Description"] = Editor_EditDescription.Content;
            e.NewValues["Navigation_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Navigation_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Navigation", "Navigation_Id = " + Request.QueryString["Navigation_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_Navigation_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["NavigationHistory"] = Convert.ToString(DataRowView_Form["Navigation_History"], CultureInfo.CurrentCulture);

            Session["NavigationHistory"] = Session["History"].ToString() + Session["NavigationHistory"].ToString();
            e.NewValues["Navigation_History"] = Session["NavigationHistory"].ToString();

            Session["NavigationHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDNavigationModifiedDate"] = "";
        Session["DBNavigationModifiedDate"] = "";
        Session["DBNavigationModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_EditName");
      AjaxControlToolkit.HTMLEditor.Editor Editor_EditDescription = (AjaxControlToolkit.HTMLEditor.Editor)FormView_Navigation_Form.FindControl("Editor_EditDescription");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_EditDescription.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Name and Description  fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["NavigationId"] = "";
        Session["NavigationName"] = "";
        string SQLStringNavigationName = "SELECT Navigation_Id , Navigation_Name FROM Administration_Navigation WHERE Navigation_Name = @Navigation_Name AND Navigation_IsActive = @Navigation_IsActive";
        using (SqlCommand SqlCommand_NavigationName = new SqlCommand(SQLStringNavigationName))
        {
          SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_Name", TextBox_EditName.Text.ToString());
          SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_IsActive", 1);
          DataTable DataTable_NavigationName;
          using (DataTable_NavigationName = new DataTable())
          {
            DataTable_NavigationName.Locale = CultureInfo.CurrentCulture;
            DataTable_NavigationName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NavigationName).Copy();
            if (DataTable_NavigationName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_NavigationName.Rows)
              {
                Session["NavigationId"] = DataRow_Row["Navigation_Id"];
                Session["NavigationName"] = DataRow_Row["Navigation_Name"];
              }
            }
            else
            {
              Session["NavigationId"] = "";
              Session["NavigationName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["NavigationName"].ToString()))
        {
          if (Session["NavigationId"].ToString() != Request.QueryString["Navigation_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Navigation with the Name '" + Session["NavigationName"].ToString() + "' already exists<br />";
          }
        }

        Session["NavigationId"] = "";
        Session["NavigationName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Navigation_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Navigation_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_Navigation_Form.FindControl("Label_InsertNameError");

      Session["NavigationName"] = "";
      string SQLStringNavigationName = "SELECT Navigation_Name FROM Administration_Navigation WHERE Navigation_Name = @Navigation_Name AND Navigation_IsActive = @Navigation_IsActive";
      using (SqlCommand SqlCommand_NavigationName = new SqlCommand(SQLStringNavigationName))
      {
        SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_IsActive", 1);
        DataTable DataTable_NavigationName;
        using (DataTable_NavigationName = new DataTable())
        {
          DataTable_NavigationName.Locale = CultureInfo.CurrentCulture;
          DataTable_NavigationName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NavigationName).Copy();
          if (DataTable_NavigationName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_NavigationName.Rows)
            {
              Session["NavigationName"] = DataRow_Row["Navigation_Name"];
            }
          }
          else
          {
            Session["NavigationName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["NavigationName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Navigation with the Name '" + Session["NavigationName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["NavigationName"] = "";
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_Navigation_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_Navigation_Form.FindControl("Label_EditNameError");

      Session["NavigationId"] = "";
      Session["NavigationName"] = "";
      string SQLStringNavigationName = "SELECT Navigation_Id , Navigation_Name FROM Administration_Navigation WHERE Navigation_Name = @Navigation_Name AND Navigation_IsActive = @Navigation_IsActive";
      using (SqlCommand SqlCommand_NavigationName = new SqlCommand(SQLStringNavigationName))
      {
        SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_Name", TextBox_EditName.Text.ToString());
        SqlCommand_NavigationName.Parameters.AddWithValue("@Navigation_IsActive", 1);
        DataTable DataTable_NavigationName;
        using (DataTable_NavigationName = new DataTable())
        {
          DataTable_NavigationName.Locale = CultureInfo.CurrentCulture;
          DataTable_NavigationName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NavigationName).Copy();
          if (DataTable_NavigationName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_NavigationName.Rows)
            {
              Session["NavigationId"] = DataRow_Row["Navigation_Id"];
              Session["NavigationName"] = DataRow_Row["Navigation_Name"];
            }
          }
          else
          {
            Session["NavigationId"] = "";
            Session["NavigationName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["NavigationName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["NavigationId"].ToString() == Request.QueryString["Navigation_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Navigation with the Name '" + Session["NavigationName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["NavigationId"] = "";
      Session["NavigationName"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}