using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Unit : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Unit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Unit, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Unit_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Unit.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Unit_Id"] != null)
      {
        FormView_Unit_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Unit_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Unit_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Unit_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Unit_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_Unit_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Unit_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Unit_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_UnitId"];
      string SearchField2 = Request.QueryString["Search_UnitIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Unit_Id=" + Request.QueryString["Search_UnitId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Unit_IsActive=" + Request.QueryString["Search_UnitIsActive"] + "&";
      }

      string FinalURL = "Administration_Unit_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Unit List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Unit_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Unit_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_Unit_Form.InsertParameters["Unit_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Unit_Form.InsertParameters["Unit_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Unit_Form.InsertParameters["Unit_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Unit_Form.InsertParameters["Unit_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Unit_Form.InsertParameters["Unit_History"].DefaultValue = "";
          SqlDataSource_Unit_Form.InsertParameters["Unit_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertName = (TextBox)FormView_Unit_Form.FindControl("TextBox_InsertName");
      TextBox TextBox_InsertDescription = (TextBox)FormView_Unit_Form.FindControl("TextBox_InsertDescription");

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
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["UnitName"] = "";
        string SQLStringUnitName = "SELECT Unit_Name FROM Administration_Unit WHERE Unit_Name = @Unit_Name AND Unit_IsActive = @Unit_IsActive";
        using (SqlCommand SqlCommand_UnitName = new SqlCommand(SQLStringUnitName))
        {
          SqlCommand_UnitName.Parameters.AddWithValue("@Unit_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_UnitName.Parameters.AddWithValue("@Unit_IsActive", 1);
          DataTable DataTable_UnitName;
          using (DataTable_UnitName = new DataTable())
          {
            DataTable_UnitName.Locale = CultureInfo.CurrentCulture;
            DataTable_UnitName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_UnitName).Copy();
            if (DataTable_UnitName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_UnitName.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
            else
            {
              Session["UnitName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["UnitName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Unit with the Name '" + Session["UnitName"].ToString() + "' already exists<br />";
        }

        Session["UnitName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Unit_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Unit_Id"] = e.Command.Parameters["@Unit_Id"].Value;

        string SearchField1 = "s_Unit_Id=" + Session["Unit_Id"].ToString() + "";
        string FinalURL = "Administration_Unit_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Unit List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Unit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDUnitModifiedDate"] = e.OldValues["Unit_ModifiedDate"];
        object OLDUnitModifiedDate = Session["OLDUnitModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDUnitModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareUnit = (DataView)SqlDataSource_Unit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareUnit = DataView_CompareUnit[0];
        Session["DBUnitModifiedDate"] = Convert.ToString(DataRowView_CompareUnit["Unit_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBUnitModifiedBy"] = Convert.ToString(DataRowView_CompareUnit["Unit_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBUnitModifiedDate = Session["DBUnitModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBUnitModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBUnitModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Unit_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Unit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_Unit_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Unit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["Unit_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Unit_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Unit", "Unit_Id = " + Request.QueryString["Unit_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_Unit_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["UnitHistory"] = Convert.ToString(DataRowView_Form["Unit_History"], CultureInfo.CurrentCulture);

            Session["UnitHistory"] = Session["History"].ToString() + Session["UnitHistory"].ToString();
            e.NewValues["Unit_History"] = Session["UnitHistory"].ToString();

            Session["UnitHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDUnitModifiedDate"] = "";
        Session["DBUnitModifiedDate"] = "";
        Session["DBUnitModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditName = (TextBox)FormView_Unit_Form.FindControl("TextBox_EditName");
      TextBox TextBox_EditDescription = (TextBox)FormView_Unit_Form.FindControl("TextBox_EditDescription");

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
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["UnitId"] = "";
        Session["UnitName"] = "";
        string SQLStringUnitName = "SELECT Unit_Id , Unit_Name FROM Administration_Unit WHERE Unit_Name = @Unit_Name AND Unit_IsActive = @Unit_IsActive";
        using (SqlCommand SqlCommand_UnitName = new SqlCommand(SQLStringUnitName))
        {
          SqlCommand_UnitName.Parameters.AddWithValue("@Unit_Name", TextBox_EditName.Text.ToString());
          SqlCommand_UnitName.Parameters.AddWithValue("@Unit_IsActive", 1);
          DataTable DataTable_UnitName;
          using (DataTable_UnitName = new DataTable())
          {
            DataTable_UnitName.Locale = CultureInfo.CurrentCulture;
            DataTable_UnitName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_UnitName).Copy();
            if (DataTable_UnitName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_UnitName.Rows)
              {
                Session["UnitId"] = DataRow_Row["Unit_Id"];
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
            else
            {
              Session["UnitId"] = "";
              Session["UnitName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["UnitName"].ToString()))
        {
          if (Session["UnitId"].ToString() != Request.QueryString["Unit_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Unit with the Name '" + Session["UnitName"].ToString() + "' already exists<br />";
          }
        }

        Session["UnitId"] = "";
        Session["UnitName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Unit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Unit_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertName = (TextBox)FormView_Unit_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_Unit_Form.FindControl("Label_InsertNameError");

      Session["UnitName"] = "";
      string SQLStringUnit = "SELECT Unit_Name FROM Administration_Unit WHERE Unit_Name = @Unit_Name AND Unit_IsActive = @Unit_IsActive";
      using (SqlCommand SqlCommand_Unit = new SqlCommand(SQLStringUnit))
      {
        SqlCommand_Unit.Parameters.AddWithValue("@Unit_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_Unit.Parameters.AddWithValue("@Unit_IsActive", 1);
        DataTable DataTable_Unit;
        using (DataTable_Unit = new DataTable())
        {
          DataTable_Unit.Locale = CultureInfo.CurrentCulture;
          DataTable_Unit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Unit).Copy();
          if (DataTable_Unit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Unit.Rows)
            {
              Session["UnitName"] = DataRow_Row["Unit_Name"];
            }
          }
          else
          {
            Session["UnitName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["UnitName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Unit with the Name '" + Session["UnitName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["UnitName"] = "";
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_Unit_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_Unit_Form.FindControl("Label_EditNameError");

      Session["UnitId"] = "";
      Session["UnitName"] = "";
      string SQLStringUnit = "SELECT Unit_Id , Unit_Name FROM Administration_Unit WHERE Unit_Name = @Unit_Name AND Unit_IsActive = @Unit_IsActive";
      using (SqlCommand SqlCommand_Unit = new SqlCommand(SQLStringUnit))
      {
        SqlCommand_Unit.Parameters.AddWithValue("@Unit_Name", TextBox_EditName.Text.ToString());
        SqlCommand_Unit.Parameters.AddWithValue("@Unit_IsActive", 1);
        DataTable DataTable_Unit;
        using (DataTable_Unit = new DataTable())
        {
          DataTable_Unit.Locale = CultureInfo.CurrentCulture;
          DataTable_Unit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Unit).Copy();
          if (DataTable_Unit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Unit.Rows)
            {
              Session["UnitId"] = DataRow_Row["Unit_Id"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
            }
          }
          else
          {
            Session["UnitId"] = "";
            Session["UnitName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["UnitName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["UnitId"].ToString() == Request.QueryString["Unit_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Unit with the Name '" + Session["UnitName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["UnitId"] = "";
      Session["UnitName"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}