using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Facility_Type_Lookup : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Facility_Type_Lookup_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Facility_Type_Lookup, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Facility_Type_Lookup_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Facility_Type_Lookup.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Facility_Type_Lookup_Id"] != null)
      {
        FormView_Facility_Type_Lookup_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Facility_Type_Lookup_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Facility_Type_Lookup_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_Facility_Type_Lookup_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityTypeLookupId"];
      string SearchField2 = Request.QueryString["Search_FacilityTypeLookupIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_FacilityTypeLookup_Id=" + Request.QueryString["Search_FacilityTypeLookupId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_FacilityTypeLookup_IsActive=" + Request.QueryString["Search_FacilityTypeLookupIsActive"] + "&";
      }

      string FinalURL = "Administration_Facility_Type_Lookup_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Type Lookup List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Facility_Type_Lookup_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_History"].DefaultValue = "";
          SqlDataSource_Facility_Type_Lookup_Form.InsertParameters["Facility_Type_Lookup_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertName = (TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_InsertName");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertName.Text))
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
        Session["FacilityTypeLookupName"] = "";
        string SQLStringFacilityTypeLookupName = "SELECT Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup WHERE Facility_Type_Lookup_Name = @Facility_Type_Lookup_Name AND Facility_Type_Lookup_IsActive = @Facility_Type_Lookup_IsActive";
        using (SqlCommand SqlCommand_FacilityTypeLookupName = new SqlCommand(SQLStringFacilityTypeLookupName))
        {
          SqlCommand_FacilityTypeLookupName.Parameters.AddWithValue("@Facility_Type_Lookup_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_FacilityTypeLookupName.Parameters.AddWithValue("@Facility_Type_Lookup_IsActive", 1);
          DataTable DataTable_FacilityTypeLookupName;
          using (DataTable_FacilityTypeLookupName = new DataTable())
          {
            DataTable_FacilityTypeLookupName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityTypeLookupName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityTypeLookupName).Copy();
            if (DataTable_FacilityTypeLookupName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityTypeLookupName.Rows)
              {
                Session["FacilityTypeLookupName"] = DataRow_Row["Facility_Type_Lookup_Name"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityTypeLookupName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Facility Type with the Name '" + Session["FacilityTypeLookupName"].ToString() + "' already exists<br />";
        }

        Session["FacilityTypeLookupName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Facility_Type_Lookup_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Facility_Type_Lookup_Id"] = e.Command.Parameters["@Facility_Type_Lookup_Id"].Value;

        string SearchField1 = "s_FacilityTypeLookup_Id=" + Session["Facility_Type_Lookup_Id"].ToString() + "";
        string FinalURL = "Administration_Facility_Type_Lookup_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Type Lookup List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Facility_Type_Lookup_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDFacilityTypeLookupModifiedDate"] = e.OldValues["Facility_Type_Lookup_ModifiedDate"];
        object OLDFacilityTypeLookupModifiedDate = Session["OLDFacilityTypeLookupModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDFacilityTypeLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFacilityTypeLookup = (DataView)SqlDataSource_Facility_Type_Lookup_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFacilityTypeLookup = DataView_CompareFacilityTypeLookup[0];
        Session["DBFacilityTypeLookupModifiedDate"] = Convert.ToString(DataRowView_CompareFacilityTypeLookup["Facility_Type_Lookup_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBFacilityTypeLookupModifiedBy"] = Convert.ToString(DataRowView_CompareFacilityTypeLookup["Facility_Type_Lookup_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBFacilityTypeLookupModifiedDate = Session["DBFacilityTypeLookupModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBFacilityTypeLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBFacilityTypeLookupModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["Facility_Type_Lookup_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Facility_Type_Lookup_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Facility_Type_Lookup", "Facility_Type_Lookup_Id = " + Request.QueryString["Facility_Type_Lookup_Id"]);

            DataView DataView_FacilityTypeLookup = (DataView)SqlDataSource_Facility_Type_Lookup_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_FacilityTypeLookup = DataView_FacilityTypeLookup[0];
            Session["FacilityTypeLookupHistory"] = Convert.ToString(DataRowView_FacilityTypeLookup["Facility_Type_Lookup_History"], CultureInfo.CurrentCulture);

            Session["FacilityTypeLookupHistory"] = Session["History"].ToString() + Session["FacilityTypeLookupHistory"].ToString();
            e.NewValues["Facility_Type_Lookup_History"] = Session["FacilityTypeLookupHistory"].ToString();

            Session["FacilityTypeLookupHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDFacilityTypeLookupModifiedDate"] = "";
        Session["DBFacilityTypeLookupModifiedDate"] = "";
        Session["DBFacilityTypeLookupModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditName = (TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_EditName");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditName.Text))
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
        Session["FacilityTypeLookupId"] = "";
        Session["FacilityTypeLookupName"] = "";
        string SQLStringFacilityTypeLookupName = "SELECT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup WHERE Facility_Type_Lookup_Name = @Facility_Type_Lookup_Name AND Facility_Type_Lookup_IsActive = @Facility_Type_Lookup_IsActive";
        using (SqlCommand SqlCommand_FacilityTypeLookupName = new SqlCommand(SQLStringFacilityTypeLookupName))
        {
          SqlCommand_FacilityTypeLookupName.Parameters.AddWithValue("@Facility_Type_Lookup_Name", TextBox_EditName.Text.ToString());
          SqlCommand_FacilityTypeLookupName.Parameters.AddWithValue("@Facility_Type_Lookup_IsActive", 1);
          DataTable DataTable_FacilityTypeLookupName;
          using (DataTable_FacilityTypeLookupName = new DataTable())
          {
            DataTable_FacilityTypeLookupName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityTypeLookupName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityTypeLookupName).Copy();
            if (DataTable_FacilityTypeLookupName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityTypeLookupName.Rows)
              {
                Session["FacilityTypeLookupId"] = DataRow_Row["Facility_Type_Lookup_Id"];
                Session["FacilityTypeLookupName"] = DataRow_Row["Facility_Type_Lookup_Name"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityTypeLookupName"].ToString()))
        {
          if (Session["FacilityTypeLookupId"].ToString() != Request.QueryString["Facility_Type_Lookup_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Facility Type with the Name '" + Session["FacilityTypeLookupName"].ToString() + "' already exists<br />";
          }
        }

        Session["FacilityTypeLookupId"] = "";
        Session["FacilityTypeLookupName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Facility_Type_Lookup_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Facility_Type_Lookup_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertName = (TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_InsertNameError");

      Session["FacilityTypeLookupName"] = "";
      string SQLStringFacilityTypeLookup = "SELECT Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup WHERE Facility_Type_Lookup_Name = @Facility_Type_Lookup_Name AND Facility_Type_Lookup_IsActive = @Facility_Type_Lookup_IsActive";
      using (SqlCommand SqlCommand_FacilityTypeLookup = new SqlCommand(SQLStringFacilityTypeLookup))
      {
        SqlCommand_FacilityTypeLookup.Parameters.AddWithValue("@Facility_Type_Lookup_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_FacilityTypeLookup.Parameters.AddWithValue("@Facility_Type_Lookup_IsActive", 1);
        DataTable DataTable_FacilityTypeLookup;
        using (DataTable_FacilityTypeLookup = new DataTable())
        {
          DataTable_FacilityTypeLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityTypeLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityTypeLookup).Copy();
          if (DataTable_FacilityTypeLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityTypeLookup.Rows)
            {
              Session["FacilityTypeLookupName"] = DataRow_Row["Facility_Type_Lookup_Name"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityTypeLookupName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility Type with the Name '" + Session["FacilityTypeLookupName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["FacilityTypeLookupName"] = "";
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_Facility_Type_Lookup_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_Facility_Type_Lookup_Form.FindControl("Label_EditNameError");

      Session["FacilityTypeLookupId"] = "";
      Session["FacilityTypeLookupName"] = "";
      string SQLStringFacilityTypeLookup = "SELECT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup WHERE Facility_Type_Lookup_Name = @Facility_Type_Lookup_Name AND Facility_Type_Lookup_IsActive = @Facility_Type_Lookup_IsActive";
      using (SqlCommand SqlCommand_FacilityTypeLookup = new SqlCommand(SQLStringFacilityTypeLookup))
      {
        SqlCommand_FacilityTypeLookup.Parameters.AddWithValue("@Facility_Type_Lookup_Name", TextBox_EditName.Text.ToString());
        SqlCommand_FacilityTypeLookup.Parameters.AddWithValue("@Facility_Type_Lookup_IsActive", 1);
        DataTable DataTable_FacilityTypeLookup;
        using (DataTable_FacilityTypeLookup = new DataTable())
        {
          DataTable_FacilityTypeLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityTypeLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityTypeLookup).Copy();
          if (DataTable_FacilityTypeLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityTypeLookup.Rows)
            {
              Session["FacilityTypeLookupId"] = DataRow_Row["Facility_Type_Lookup_Id"];
              Session["FacilityTypeLookupName"] = DataRow_Row["Facility_Type_Lookup_Name"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityTypeLookupName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["FacilityTypeLookupId"].ToString() == Request.QueryString["Facility_Type_Lookup_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility Type with the Name '" + Session["FacilityTypeLookupName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["FacilityTypeLookupId"] = "";
      Session["FacilityTypeLookupName"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}