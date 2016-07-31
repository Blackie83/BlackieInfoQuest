using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Facility : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Facility_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Facility_InsertFacilityType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Facility_EditFacilityType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Facility, GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Facility_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Facility.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Facility_Id"] != null)
      {
        FormView_Facility_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Facility_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Facility_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((DropDownList)FormView_Facility_Form.FindControl("DropDownList_InsertFacilityType")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_InsertImpiloUnitId")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_InsertImpiloCountryId")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_InsertIMEDSConnectionString")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_Facility_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((DropDownList)FormView_Facility_Form.FindControl("DropDownList_EditFacilityType")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_EditImpiloUnitId")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_EditImpiloCountryId")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Facility_Form.FindControl("TextBox_EditIMEDSConnectionString")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_FacilityFacilityCode"];
      string SearchField3 = Request.QueryString["Search_FacilityFacilityType"];
      string SearchField4 = Request.QueryString["Search_FacilityIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_FacilityCode=" + Request.QueryString["Search_FacilityFacilityCode"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Facility_FacilityType=" + Request.QueryString["Search_FacilityFacilityType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Facility_IsActive=" + Request.QueryString["Search_FacilityIsActive"] + "&";
      }

      string FinalURL = "Administration_Facility_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Facility_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Facility_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_Facility_Form.InsertParameters["Facility_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Facility_Form.InsertParameters["Facility_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Facility_Form.InsertParameters["Facility_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Facility_Form.InsertParameters["Facility_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Facility_Form.InsertParameters["Facility_History"].DefaultValue = "";
          SqlDataSource_Facility_Form.InsertParameters["Facility_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertFacilityName = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityName");
      TextBox TextBox_InsertFacilityCode = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityCode");
      DropDownList DropDownList_InsertFacilityType = (DropDownList)FormView_Facility_Form.FindControl("DropDownList_InsertFacilityType");
      TextBox TextBox_InsertImpiloUnitId = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertImpiloUnitId");
      TextBox TextBox_InsertImpiloCountryId = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertImpiloCountryId");
      TextBox TextBox_InsertIMEDSConnectionString = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertIMEDSConnectionString");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertFacilityName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertFacilityCode.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertFacilityType.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertImpiloUnitId.Text))
        {
          //InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertImpiloCountryId.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertIMEDSConnectionString.Text))
        {
          //InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["FacilityFacilityName"] = "";
        string SQLStringFacilityName = "SELECT Facility_FacilityName FROM Administration_Facility WHERE Facility_FacilityName = @Facility_FacilityName AND Facility_IsActive = @Facility_IsActive";
        using (SqlCommand SqlCommand_FacilityName = new SqlCommand(SQLStringFacilityName))
        {
          SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_FacilityName", TextBox_InsertFacilityName.Text.ToString());
          SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_IsActive", 1);
          DataTable DataTable_FacilityName;
          using (DataTable_FacilityName = new DataTable())
          {
            DataTable_FacilityName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityName).Copy();
            if (DataTable_FacilityName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityName.Rows)
              {
                Session["FacilityFacilityName"] = DataRow_Row["Facility_FacilityName"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityFacilityName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Facility with the Name '" + Session["FacilityFacilityName"].ToString() + "' already exists<br />";
        }

        Session["FacilityFacilityName"] = "";


        Session["FacilityFacilityCode"] = "";
        string SQLStringFacilityCode = "SELECT Facility_FacilityCode FROM Administration_Facility WHERE Facility_FacilityCode = @Facility_FacilityCode AND Facility_IsActive = @Facility_IsActive";
        using (SqlCommand SqlCommand_FacilityCode = new SqlCommand(SQLStringFacilityCode))
        {
          SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_FacilityCode", TextBox_InsertFacilityCode.Text.ToString());
          SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_IsActive", 1);
          DataTable DataTable_FacilityCode;
          using (DataTable_FacilityCode = new DataTable())
          {
            DataTable_FacilityCode.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityCode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityCode).Copy();
            if (DataTable_FacilityCode.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityCode.Rows)
              {
                Session["FacilityFacilityCode"] = DataRow_Row["Facility_FacilityCode"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityFacilityCode"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Facility Code with the Name '" + Session["FacilityFacilityCode"].ToString() + "' already exists<br />";
        }

        Session["FacilityFacilityCode"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Facility_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Facility_Id"] = e.Command.Parameters["@Facility_Id"].Value;

        string SearchField1 = "s_Facility_Id=" + Session["Facility_Id"].ToString() + "";
        string FinalURL = "Administration_Facility_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Facility_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDFacilityModifiedDate"] = e.OldValues["Facility_ModifiedDate"];
        object OLDFacilityModifiedDate = Session["OLDFacilityModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDFacilityModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFacility = (DataView)SqlDataSource_Facility_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFacility = DataView_CompareFacility[0];
        Session["DBFacilityModifiedDate"] = Convert.ToString(DataRowView_CompareFacility["Facility_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBFacilityModifiedBy"] = Convert.ToString(DataRowView_CompareFacility["Facility_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBFacilityModifiedDate = Session["DBFacilityModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBFacilityModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBFacilityModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Facility_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Facility_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_Facility_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Facility_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["Facility_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Facility_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Facility", "Facility_Id = " + Request.QueryString["Facility_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_Facility_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["FacilityHistory"] = Convert.ToString(DataRowView_Form["Facility_History"], CultureInfo.CurrentCulture);

            Session["FacilityHistory"] = Session["History"].ToString() + Session["FacilityHistory"].ToString();
            e.NewValues["Facility_History"] = Session["FacilityHistory"].ToString();

            Session["FacilityHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDFacilityModifiedDate"] = "";
        Session["DBFacilityModifiedDate"] = "";
        Session["DBFacilityModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditFacilityName = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityName");
      TextBox TextBox_EditFacilityCode = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityCode");
      DropDownList DropDownList_EditFacilityType = (DropDownList)FormView_Facility_Form.FindControl("DropDownList_EditFacilityType");
      TextBox TextBox_EditImpiloUnitId = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditImpiloUnitId");
      TextBox TextBox_EditImpiloCountryId = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditImpiloCountryId");
      TextBox TextBox_EditIMEDSConnectionString = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditIMEDSConnectionString");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditFacilityName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditFacilityCode.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditFacilityType.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditImpiloUnitId.Text))
        {
          //InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditImpiloCountryId.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditIMEDSConnectionString.Text))
        {
          //InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["FacilityId"] = "";
        Session["FacilityFacilityName"] = "";
        string SQLStringFacilityName = "SELECT Facility_Id , Facility_FacilityName FROM Administration_Facility WHERE Facility_FacilityName = @Facility_FacilityName AND Facility_IsActive = @Facility_IsActive";
        using (SqlCommand SqlCommand_FacilityName = new SqlCommand(SQLStringFacilityName))
        {
          SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_FacilityName", TextBox_EditFacilityName.Text.ToString());
          SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_IsActive", 1);
          DataTable DataTable_FacilityName;
          using (DataTable_FacilityName = new DataTable())
          {
            DataTable_FacilityName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityName).Copy();
            if (DataTable_FacilityName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityName.Rows)
              {
                Session["FacilityId"] = DataRow_Row["Facility_Id"];
                Session["FacilityFacilityName"] = DataRow_Row["Facility_FacilityName"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityFacilityName"].ToString()))
        {
          if (Session["FacilityId"].ToString() != Request.QueryString["Facility_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Facility with the Name '" + Session["FacilityFacilityName"].ToString() + "' already exists<br />";
          }
        }

        Session["FacilityId"] = "";
        Session["FacilityFacilityName"] = "";


        Session["FacilityId"] = "";
        Session["FacilityFacilityCode"] = "";
        string SQLStringFacilityCode = "SELECT Facility_Id , Facility_FacilityCode FROM Administration_Facility WHERE Facility_FacilityCode = @Facility_FacilityCode AND Facility_IsActive = @Facility_IsActive";
        using (SqlCommand SqlCommand_FacilityCode = new SqlCommand(SQLStringFacilityCode))
        {
          SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_FacilityCode", TextBox_EditFacilityCode.Text.ToString());
          SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_IsActive", 1);
          DataTable DataTable_FacilityCode;
          using (DataTable_FacilityCode = new DataTable())
          {
            DataTable_FacilityCode.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityCode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityCode).Copy();
            if (DataTable_FacilityCode.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityCode.Rows)
              {
                Session["FacilityId"] = DataRow_Row["Facility_Id"];
                Session["FacilityFacilityCode"] = DataRow_Row["Facility_FacilityCode"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FacilityFacilityCode"].ToString()))
        {
          if (Session["FacilityId"].ToString() != Request.QueryString["Facility_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Facility Code with the Name '" + Session["FacilityFacilityCode"].ToString() + "' already exists<br />";
          }
        }

        Session["FacilityId"] = "";
        Session["FacilityFacilityCode"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Facility_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Facility_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void TextBox_InsertFacilityName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertFacilityName = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityName");
      Label Label_InsertFacilityNameError = (Label)FormView_Facility_Form.FindControl("Label_InsertFacilityNameError");

      Session["FacilityFacilityName"] = "";
      string SQLStringFacilityName = "SELECT Facility_FacilityName FROM Administration_Facility WHERE Facility_FacilityName = @Facility_FacilityName AND Facility_IsActive = @Facility_IsActive";
      using (SqlCommand SqlCommand_FacilityName = new SqlCommand(SQLStringFacilityName))
      {
        SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_FacilityName", TextBox_InsertFacilityName.Text.ToString());
        SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_IsActive", 1);
        DataTable DataTable_FacilityName;
        using (DataTable_FacilityName = new DataTable())
        {
          DataTable_FacilityName.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityName).Copy();
          if (DataTable_FacilityName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityName.Rows)
            {
              Session["FacilityFacilityName"] = DataRow_Row["Facility_FacilityName"];
            }
          }
          else
          {
            Session["FacilityFacilityName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityFacilityName"].ToString()))
      {
        Label_InsertFacilityNameError.Text = "";
      }
      else
      {
        Label_InsertFacilityNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility with the Name '" + Session["FacilityFacilityName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["FacilityFacilityName"] = "";
    }

    protected void TextBox_InsertFacilityCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertFacilityCode = (TextBox)FormView_Facility_Form.FindControl("TextBox_InsertFacilityCode");
      Label Label_InsertFacilityCodeError = (Label)FormView_Facility_Form.FindControl("Label_InsertFacilityCodeError");

      Session["FacilityFacilityCode"] = "";
      string SQLStringFacilityCode = "SELECT Facility_FacilityCode FROM Administration_Facility WHERE Facility_FacilityCode = @Facility_FacilityCode AND Facility_IsActive = @Facility_IsActive";
      using (SqlCommand SqlCommand_FacilityCode = new SqlCommand(SQLStringFacilityCode))
      {
        SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_FacilityCode", TextBox_InsertFacilityCode.Text.ToString());
        SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_IsActive", 1);
        DataTable DataTable_FacilityCode;
        using (DataTable_FacilityCode = new DataTable())
        {
          DataTable_FacilityCode.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityCode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityCode).Copy();
          if (DataTable_FacilityCode.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityCode.Rows)
            {
              Session["FacilityFacilityCode"] = DataRow_Row["Facility_FacilityCode"];
            }
          }
          else
          {
            Session["FacilityFacilityCode"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityFacilityCode"].ToString()))
      {
        Label_InsertFacilityCodeError.Text = "";
      }
      else
      {
        Label_InsertFacilityCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility Code with the Name '" + Session["FacilityFacilityCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["FacilityFacilityCode"] = "";
    }

    protected void TextBox_EditFacilityName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditFacilityName = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityName");
      Label Label_EditFacilityNameError = (Label)FormView_Facility_Form.FindControl("Label_EditFacilityNameError");

      Session["FacilityId"] = "";
      Session["FacilityFacilityName"] = "";
      string SQLStringFacilityName = "SELECT Facility_Id , Facility_FacilityName FROM Administration_Facility WHERE Facility_FacilityName = @Facility_FacilityName AND Facility_IsActive = @Facility_IsActive";
      using (SqlCommand SqlCommand_FacilityName = new SqlCommand(SQLStringFacilityName))
      {
        SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_FacilityName", TextBox_EditFacilityName.Text.ToString());
        SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_IsActive", 1);
        DataTable DataTable_FacilityName;
        using (DataTable_FacilityName = new DataTable())
        {
          DataTable_FacilityName.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityName).Copy();
          if (DataTable_FacilityName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityName.Rows)
            {
              Session["FacilityId"] = DataRow_Row["Facility_Id"];
              Session["FacilityFacilityName"] = DataRow_Row["Facility_FacilityName"];
            }
          }
          else
          {
            Session["FacilityId"] = "";
            Session["FacilityFacilityName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityFacilityName"].ToString()))
      {
        Label_EditFacilityNameError.Text = "";
      }
      else
      {
        if (Session["FacilityId"].ToString() == Request.QueryString["Facility_Id"])
        {
          Label_EditFacilityNameError.Text = "";
        }
        else
        {
          Label_EditFacilityNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility with the Name '" + Session["FacilityFacilityName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["FacilityId"] = "";
      Session["FacilityFacilityName"] = "";
    }

    protected void TextBox_EditFacilityCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditFacilityCode = (TextBox)FormView_Facility_Form.FindControl("TextBox_EditFacilityCode");
      Label Label_EditFacilityCodeError = (Label)FormView_Facility_Form.FindControl("Label_EditFacilityCodeError");

      Session["FacilityId"] = "";
      Session["FacilityFacilityCode"] = "";
      string SQLStringFacilityCode = "SELECT Facility_Id , Facility_FacilityCode FROM Administration_Facility WHERE Facility_FacilityCode = @Facility_FacilityCode AND Facility_IsActive = @Facility_IsActive";
      using (SqlCommand SqlCommand_FacilityCode = new SqlCommand(SQLStringFacilityCode))
      {
        SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_FacilityCode", TextBox_EditFacilityCode.Text.ToString());
        SqlCommand_FacilityCode.Parameters.AddWithValue("@Facility_IsActive", 1);
        DataTable DataTable_FacilityCode;
        using (DataTable_FacilityCode = new DataTable())
        {
          DataTable_FacilityCode.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityCode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityCode).Copy();
          if (DataTable_FacilityCode.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityCode.Rows)
            {
              Session["FacilityId"] = DataRow_Row["Facility_Id"];
              Session["FacilityFacilityCode"] = DataRow_Row["Facility_FacilityCode"];
            }
          }
          else
          {
            Session["FacilityId"] = "";
            Session["FacilityFacilityCode"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FacilityFacilityCode"].ToString()))
      {
        Label_EditFacilityCodeError.Text = "";
      }
      else
      {
        if (Session["FacilityId"].ToString() == Request.QueryString["Facility_Id"])
        {
          Label_EditFacilityCodeError.Text = "";
        }
        else
        {
          Label_EditFacilityCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Facility Code with the Name '" + Session["FacilityFacilityCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["FacilityId"] = "";
      Session["FacilityFacilityCode"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}