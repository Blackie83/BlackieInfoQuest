using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SecurityRole : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SecurityRole_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_SecurityRole_InsertFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_SecurityRole_EditFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SecurityRole, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SecurityRole_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SecurityRole.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SecurityRole_Id"] != null)
      {
        FormView_SecurityRole_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SecurityRole_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SecurityRole_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_SecurityRole_Form.FindControl("DropDownList_InsertFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertRank")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SecurityRole_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_SecurityRole_Form.FindControl("DropDownList_EditFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditRank")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FormId"];
      string SearchField2 = Request.QueryString["Search_SecurityRoleId"];
      string SearchField3 = Request.QueryString["Search_SecurityRoleIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Form_Id=" + Request.QueryString["Search_FormId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityRole_Id=" + Request.QueryString["Search_SecurityRoleId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityRole_IsActive=" + Request.QueryString["Search_SecurityRoleIsActive"] + "&";
      }

      string FinalURL = "Administration_SecurityRole_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Role List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SecurityRole_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SecurityRole_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_History"].DefaultValue = "";
          SqlDataSource_SecurityRole_Form.InsertParameters["SecurityRole_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_SecurityRole_Form.FindControl("DropDownList_InsertFormId");
      TextBox TextBox_InsertName = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertName");
      TextBox TextBox_InsertDescription = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertDescription");
      TextBox TextBox_InsertRank = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertRank");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertRank.Text))
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
        Session["SecurityRoleName"] = "";
        string SQLStringSecurityRoleName = "SELECT SecurityRole_Name FROM Administration_SecurityRole WHERE SecurityRole_Name = @SecurityRole_Name AND SecurityRole_IsActive = @SecurityRole_IsActive";
        using (SqlCommand SqlCommand_SecurityRoleName = new SqlCommand(SQLStringSecurityRoleName))
        {
          SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_IsActive", 1);
          DataTable DataTable_SecurityRoleName;
          using (DataTable_SecurityRoleName = new DataTable())
          {
            DataTable_SecurityRoleName.Locale = CultureInfo.CurrentCulture;
            DataTable_SecurityRoleName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRoleName).Copy();
            if (DataTable_SecurityRoleName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SecurityRoleName.Rows)
              {
                Session["SecurityRoleName"] = DataRow_Row["SecurityRole_Name"];
              }
            }
            else
            {
              Session["SecurityRoleName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SecurityRoleName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Security Role with the Name '" + Session["SecurityRoleName"].ToString() + "' already exists<br />";
        }

        Session["SecurityRoleName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SecurityRole_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SecurityRole_Id"] = e.Command.Parameters["@SecurityRole_Id"].Value;

        string SearchField1 = "s_SecurityRole_Id=" + Session["SecurityRole_Id"].ToString() + "";
        string FinalURL = "Administration_SecurityRole_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Role List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SecurityRole_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSecurityRoleModifiedDate"] = e.OldValues["SecurityRole_ModifiedDate"];
        object OLDSecurityRoleModifiedDate = Session["OLDSecurityRoleModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSecurityRoleModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSecurityRole = (DataView)SqlDataSource_SecurityRole_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSecurityRole = DataView_CompareSecurityRole[0];
        Session["DBSecurityRoleModifiedDate"] = Convert.ToString(DataRowView_CompareSecurityRole["SecurityRole_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSecurityRoleModifiedBy"] = Convert.ToString(DataRowView_CompareSecurityRole["SecurityRole_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSecurityRoleModifiedDate = Session["DBSecurityRoleModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSecurityRoleModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSecurityRoleModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SecurityRole_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SecurityRole_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SecurityRole_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SecurityRole_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["SecurityRole_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SecurityRole_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SecurityRole", "SecurityRole_Id = " + Request.QueryString["SecurityRole_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SecurityRole_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SecurityRoleHistory"] = Convert.ToString(DataRowView_Form["SecurityRole_History"], CultureInfo.CurrentCulture);

            Session["SecurityRoleHistory"] = Session["History"].ToString() + Session["SecurityRoleHistory"].ToString();
            e.NewValues["SecurityRole_History"] = Session["SecurityRoleHistory"].ToString();

            Session["SecurityRoleHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSecurityRoleModifiedDate"] = "";
        Session["DBSecurityRoleModifiedDate"] = "";
        Session["DBSecurityRoleModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditFormId = (DropDownList)FormView_SecurityRole_Form.FindControl("DropDownList_EditFormId");
      TextBox TextBox_EditName = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditName");
      TextBox TextBox_EditDescription = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditDescription");
      TextBox TextBox_EditRank = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditRank");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditRank.Text))
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
        Session["SecurityRoleId"] = "";
        Session["SecurityRoleName"] = "";
        string SQLStringSecurityRoleName = "SELECT SecurityRole_Id , SecurityRole_Name FROM Administration_SecurityRole WHERE SecurityRole_Name = @SecurityRole_Name AND SecurityRole_IsActive = @SecurityRole_IsActive";
        using (SqlCommand SqlCommand_SecurityRoleName = new SqlCommand(SQLStringSecurityRoleName))
        {
          SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_Name", TextBox_EditName.Text.ToString());
          SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_IsActive", 1);
          DataTable DataTable_SecurityRoleName;
          using (DataTable_SecurityRoleName = new DataTable())
          {
            DataTable_SecurityRoleName.Locale = CultureInfo.CurrentCulture;
            DataTable_SecurityRoleName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRoleName).Copy();
            if (DataTable_SecurityRoleName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SecurityRoleName.Rows)
              {
                Session["SecurityRoleId"] = DataRow_Row["SecurityRole_Id"];
                Session["SecurityRoleName"] = DataRow_Row["SecurityRole_Name"];
              }
            }
            else
            {
              Session["SecurityRoleId"] = "";
              Session["SecurityRoleName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SecurityRoleName"].ToString()))
        {
          if (Session["SecurityRoleId"].ToString() != Request.QueryString["SecurityRole_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Security Role with the Name '" + Session["SecurityRoleName"].ToString() + "' already exists<br />";
          }
        }

        Session["SecurityRoleId"] = "";
        Session["SecurityRoleName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SecurityRole_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SecurityRole_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertName = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_SecurityRole_Form.FindControl("Label_InsertNameError");

      Session["SecurityRoleName"] = "";
      string SQLStringSecurityRoleName = "SELECT SecurityRole_Name FROM Administration_SecurityRole WHERE SecurityRole_Name = @SecurityRole_Name AND SecurityRole_IsActive = @SecurityRole_IsActive";
      using (SqlCommand SqlCommand_SecurityRoleName = new SqlCommand(SQLStringSecurityRoleName))
      {
        SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_IsActive", 1);
        DataTable DataTable_SecurityRoleName;
        using (DataTable_SecurityRoleName = new DataTable())
        {
          DataTable_SecurityRoleName.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityRoleName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRoleName).Copy();
          if (DataTable_SecurityRoleName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SecurityRoleName.Rows)
            {
              Session["SecurityRoleName"] = DataRow_Row["SecurityRole_Name"];
            }
          }
          else
          {
            Session["SecurityRoleName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SecurityRoleName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Security Role with the Name '" + Session["SecurityRoleName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["SecurityRoleName"] = "";
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_SecurityRole_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_SecurityRole_Form.FindControl("Label_EditNameError");

      Session["SecurityRoleId"] = "";
      Session["SecurityRoleName"] = "";
      string SQLStringSecurityRoleName = "SELECT SecurityRole_Id , SecurityRole_Name FROM Administration_SecurityRole WHERE SecurityRole_Name = @SecurityRole_Name AND SecurityRole_IsActive = @SecurityRole_IsActive";
      using (SqlCommand SqlCommand_SecurityRoleName = new SqlCommand(SQLStringSecurityRoleName))
      {
        SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_Name", TextBox_EditName.Text.ToString());
        SqlCommand_SecurityRoleName.Parameters.AddWithValue("@SecurityRole_IsActive", 1);
        DataTable DataTable_SecurityRoleName;
        using (DataTable_SecurityRoleName = new DataTable())
        {
          DataTable_SecurityRoleName.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityRoleName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRoleName).Copy();
          if (DataTable_SecurityRoleName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SecurityRoleName.Rows)
            {
              Session["SecurityRoleId"] = DataRow_Row["SecurityRole_Id"];
              Session["SecurityRoleName"] = DataRow_Row["SecurityRole_Name"];
            }
          }
          else
          {
            Session["SecurityRoleId"] = "";
            Session["SecurityRoleName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SecurityRoleName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["SecurityRoleId"].ToString() == Request.QueryString["SecurityRole_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Security Role with the Name '" + Session["SecurityRoleName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["SecurityRoleId"] = "";
      Session["SecurityRoleName"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}