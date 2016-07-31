using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.ComponentModel;

namespace InfoQuestAdministration
{
  public partial class Administration_ListItem : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_ListItem_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertListCategoryId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertName_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertName_ListCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_InsertName_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditListCategoryId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditName_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditName_ListCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_EditName_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_ListItem, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_ListItem_InsertListCategoryId.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_InsertParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";

          SqlDataSource_ListItem_EditListCategoryId.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";

          if (Request.QueryString["ListItem_Id"] != null)
          {
            TableForm.Visible = true;

            SetFormVisibility();

            TextBox TextBox_EditName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName");
            DropDownList DropDownList_EditName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility");
            DropDownList DropDownList_EditName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory");
            DropDownList DropDownList_EditName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit");

            Session["ListCategoryLinkedCategoryList"] = "";
            string SQLStringListItemLinkedCategory = "SELECT ListCategory_LinkedCategory_List FROM Administration_ListCategory WHERE ListCategory_Id IN ( SELECT ListCategory_Id FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id ) AND ListCategory_LinkedCategory = 1";
            using (SqlCommand SqlCommand_ListItemLinkedCategory = new SqlCommand(SQLStringListItemLinkedCategory))
            {
              SqlCommand_ListItemLinkedCategory.Parameters.AddWithValue("@ListItem_Id", Request.QueryString["ListItem_Id"]);
              DataTable DataTable_ListItemLinkedCategory;
              using (DataTable_ListItemLinkedCategory = new DataTable())
              {
                DataTable_ListItemLinkedCategory.Locale = CultureInfo.CurrentCulture;

                DataTable_ListItemLinkedCategory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemLinkedCategory).Copy();
                if (DataTable_ListItemLinkedCategory.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ListItemLinkedCategory.Rows)
                  {
                    Session["ListCategoryLinkedCategoryList"] = DataRow_Row["ListCategory_LinkedCategory_List"];
                  }
                }
              }
            }

            if (Session["ListCategoryLinkedCategoryList"].ToString() == "Facility")
            {
              TextBox_EditName.Visible = false;
              DropDownList_EditName_Facility.Visible = true;
              DropDownList_EditName_ListCategory.Visible = false;
              DropDownList_EditName_Unit.Visible = false;
            }
            else if (Session["ListCategoryLinkedCategoryList"].ToString() == "List Category")
            {
              TextBox_EditName.Visible = false;
              DropDownList_EditName_Facility.Visible = false;
              DropDownList_EditName_ListCategory.Visible = true;
              DropDownList_EditName_Unit.Visible = false;
            }
            else if (Session["ListCategoryLinkedCategoryList"].ToString() == "Unit")
            {
              TextBox_EditName.Visible = false;
              DropDownList_EditName_Facility.Visible = false;
              DropDownList_EditName_ListCategory.Visible = false;
              DropDownList_EditName_Unit.Visible = true;
            }
            else
            {
              TextBox_EditName.Visible = true;
              DropDownList_EditName_Facility.Visible = false;
              DropDownList_EditName_ListCategory.Visible = false;
              DropDownList_EditName_Unit.Visible = false;
            }

            Session.Remove("ListCategoryLinkedCategoryList");
          }
          else
          {
            TableForm.Visible = true;

            TextBox TextBox_InsertName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName");
            DropDownList DropDownList_InsertName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Facility");
            DropDownList DropDownList_InsertName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_ListCategory");
            DropDownList DropDownList_InsertName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Unit");

            TextBox_InsertName.Visible = false;
            DropDownList_InsertName_Facility.Visible = false;
            DropDownList_InsertName_ListCategory.Visible = false;
            DropDownList_InsertName_Unit.Visible = false;
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_ListItem.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["ListItem_Id"] != null)
      {
        FormView_ListItem_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_ListItem_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_ListItem_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Facility")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_ListCategory")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Unit")).Attributes.Add("OnChange", "Validation_Form();");
      }

      if (FormView_ListItem_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FormId"];
      string SearchField2 = Request.QueryString["Search_ListCategoryId"];
      string SearchField3 = Request.QueryString["Search_ListItemParent"];
      string SearchField4 = Request.QueryString["Search_ListItemName"];
      string SearchField5 = Request.QueryString["Search_ListItemIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Form_Id=" + Request.QueryString["Search_FormId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_ListCategory_Id=" + Request.QueryString["Search_ListCategoryId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_ListItem_Parent=" + Request.QueryString["Search_ListItemParent"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_ListItem_Name=" + Request.QueryString["Search_ListItemName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_ListItem_IsActive=" + Request.QueryString["Search_ListItemIsActive"] + "&";
      }

      string FinalURL = "Administration_ListItem_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("List Item List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_ListItem_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_ListItem_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          DropDownList DropDownList_InsertListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId");
          SqlDataSource_ListItem_Form.InsertParameters["ListCategory_Id"].DefaultValue = DropDownList_InsertListCategoryId.SelectedValue.ToString();

          DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent");
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_Parent"].DefaultValue = DropDownList_InsertParent.SelectedValue.ToString();

          TextBox TextBox_InsertName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName");
          DropDownList DropDownList_InsertName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Facility");
          DropDownList DropDownList_InsertName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_ListCategory");
          DropDownList DropDownList_InsertName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Unit");
          if (TextBox_InsertName.Visible == true)
          {
            SqlDataSource_ListItem_Form.InsertParameters["ListItem_Name"].DefaultValue = TextBox_InsertName.Text.ToString();
          }

          if (DropDownList_InsertName_Facility.Visible == true)
          {
            SqlDataSource_ListItem_Form.InsertParameters["ListItem_Name"].DefaultValue = DropDownList_InsertName_Facility.SelectedValue.ToString();
          }

          if (DropDownList_InsertName_ListCategory.Visible == true)
          {
            SqlDataSource_ListItem_Form.InsertParameters["ListItem_Name"].DefaultValue = DropDownList_InsertName_ListCategory.SelectedValue.ToString();
          }

          if (DropDownList_InsertName_Unit.Visible == true)
          {
            SqlDataSource_ListItem_Form.InsertParameters["ListItem_Name"].DefaultValue = DropDownList_InsertName_Unit.SelectedValue.ToString();
          }

          SqlDataSource_ListItem_Form.InsertParameters["ListItem_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_History"].DefaultValue = "";
          SqlDataSource_ListItem_Form.InsertParameters["ListItem_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertFormId");
      DropDownList DropDownList_InsertListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId");
      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent");
      TextBox TextBox_InsertName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName");
      DropDownList DropDownList_InsertName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Facility");
      DropDownList DropDownList_InsertName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_ListCategory");
      DropDownList DropDownList_InsertName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Unit");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertListCategoryId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertParent.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (TextBox_InsertName.Visible == true)
        {
          if (string.IsNullOrEmpty(TextBox_InsertName.Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_InsertName_Facility.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertName_Facility.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_InsertName_ListCategory.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertName_ListCategory.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_InsertName_Unit.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertName_Unit.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["ListItemName"] = "";
        string SQLStringListItemName = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Name = @ListItem_Name AND ListCategory_Id = @ListCategory_Id AND ListItem_Parent = @ListItem_Parent AND ListItem_IsActive = @ListItem_IsActive";
        using (SqlCommand SqlCommand_ListItemName = new SqlCommand(SQLStringListItemName))
        {
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListCategory_Id", DropDownList_InsertListCategoryId.SelectedValue.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Parent", DropDownList_InsertParent.SelectedValue.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_IsActive", 1);
          DataTable DataTable_ListItemName;
          using (DataTable_ListItemName = new DataTable())
          {
            DataTable_ListItemName.Locale = CultureInfo.CurrentCulture;

            DataTable_ListItemName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemName).Copy();
            if (DataTable_ListItemName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ListItemName.Rows)
              {
                Session["ListItemName"] = DataRow_Row["ListItem_Name"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["ListItemName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A List Item with the Name '" + Session["ListItemName"].ToString() + "' already exists for the List Category<br />";
        }

        Session["ListCategoryName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_ListItem_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["ListItem_Id"] = e.Command.Parameters["@ListItem_Id"].Value;

        string SearchField1 = "s_ListItem_Id=" + Session["ListItem_Id"].ToString() + "";
        string FinalURL = "Administration_ListItem_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("List Item List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_ListItem_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDListItemModifiedDate"] = e.OldValues["ListItem_ModifiedDate"];
        object OLDListItemModifiedDate = Session["OLDListItemModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDListItemModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareListItem = (DataView)SqlDataSource_ListItem_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareListItem = DataView_CompareListItem[0];
        Session["DBListItemModifiedDate"] = Convert.ToString(DataRowView_CompareListItem["ListItem_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBListItemModifiedBy"] = Convert.ToString(DataRowView_CompareListItem["ListItem_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBListItemModifiedDate = Session["DBListItemModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBListItemModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBListItemModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_ListItem_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_ListItem_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_ListItem_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_ListItem_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");
            e.NewValues["ListCategory_Id"] = DropDownList_EditListCategoryId.SelectedValue.ToString();

            DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");
            e.NewValues["ListItem_Parent"] = DropDownList_EditParent.SelectedValue.ToString();

            TextBox TextBox_EditName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName");
            DropDownList DropDownList_EditName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility");
            DropDownList DropDownList_EditName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory");
            DropDownList DropDownList_EditName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit");
            if (TextBox_EditName.Visible == true)
            {
              e.NewValues["ListItem_Name"] = TextBox_EditName.Text.ToString();
            }

            if (DropDownList_EditName_Facility.Visible == true)
            {
              e.NewValues["ListItem_Name"] = DropDownList_EditName_Facility.SelectedValue.ToString();
            }

            if (DropDownList_EditName_ListCategory.Visible == true)
            {
              e.NewValues["ListItem_Name"] = DropDownList_EditName_ListCategory.SelectedValue.ToString();
            }

            if (DropDownList_EditName_Unit.Visible == true)
            {
              e.NewValues["ListItem_Name"] = DropDownList_EditName_Unit.SelectedValue.ToString();
            }

            e.NewValues["ListItem_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ListItem_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_ListItem", "ListItem_Id = " + Request.QueryString["ListItem_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_ListItem_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["ListItemHistory"] = Convert.ToString(DataRowView_Form["ListItem_History"], CultureInfo.CurrentCulture);

            Session["ListItemHistory"] = Session["History"].ToString() + Session["ListItemHistory"].ToString();
            e.NewValues["ListItem_History"] = Session["ListItemHistory"].ToString();

            Session["ListItemHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDListItemModifiedDate"] = "";
        Session["DBListItemModifiedDate"] = "";
        Session["DBListItemModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditFormId");
      DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");
      TextBox TextBox_EditName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName");
      DropDownList DropDownList_EditName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility");
      DropDownList DropDownList_EditName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory");
      DropDownList DropDownList_EditName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditListCategoryId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditParent.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (TextBox_EditName.Visible == true)
        {
          if (string.IsNullOrEmpty(TextBox_EditName.Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_EditName_Facility.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditName_Facility.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_EditName_ListCategory.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditName_ListCategory.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (DropDownList_EditName_Unit.Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditName_Unit.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["ListItemId"] = "";
        Session["ListItemName"] = "";
        string SQLStringListItemName = "SELECT ListItem_Id , ListItem_Name FROM Administration_ListItem WHERE ListItem_Name = @ListItem_Name AND ListCategory_Id = @ListCategory_Id AND ListItem_Parent = @ListItem_Parent AND ListItem_IsActive = @ListItem_IsActive";
        using (SqlCommand SqlCommand_ListItemName = new SqlCommand(SQLStringListItemName))
        {
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Name", TextBox_EditName.Text.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListCategory_Id", DropDownList_EditListCategoryId.SelectedValue.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Parent", DropDownList_EditParent.SelectedValue.ToString());
          SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_IsActive", 1);
          DataTable DataTable_ListItemName;
          using (DataTable_ListItemName = new DataTable())
          {
            DataTable_ListItemName.Locale = CultureInfo.CurrentCulture;

            DataTable_ListItemName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemName).Copy();
            if (DataTable_ListItemName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ListItemName.Rows)
              {
                Session["ListItemId"] = DataRow_Row["ListItem_Id"];
                Session["ListItemName"] = DataRow_Row["ListItem_Name"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["ListItemName"].ToString()))
        {
          if (Session["ListItemId"].ToString() != Request.QueryString["ListItem_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A List Item with the Name '" + Session["ListItemName"].ToString() + "' already exists for this List Category<br />";
          }
        }

        Session["ListItemId"] = "";
        Session["ListItemName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_ListItem_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_ListItem_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void FormView_ListItem_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_ListItem_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }
    }

    protected void EditDataBound()
    {
      DropDownList DropDownList_EditName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility");
      DropDownList DropDownList_EditName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory");
      DropDownList DropDownList_EditName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit");

      if (DropDownList_EditName_Facility.Visible == true)
      {
        DataView DataView_ListItemName = (DataView)SqlDataSource_ListItem_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ListItemName = DataView_ListItemName[0];
        DropDownList_EditName_Facility.SelectedValue = Convert.ToString(DataRowView_ListItemName["ListItem_Name"], CultureInfo.CurrentCulture);
      }

      if (DropDownList_EditName_ListCategory.Visible == true)
      {
        DataView DataView_ListItemName = (DataView)SqlDataSource_ListItem_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ListItemName = DataView_ListItemName[0];
        DropDownList_EditName_ListCategory.SelectedValue = Convert.ToString(DataRowView_ListItemName["ListItem_Name"], CultureInfo.CurrentCulture);
      }

      if (DropDownList_EditName_Unit.Visible == true)
      {
        DataView DataView_ListItemName = (DataView)SqlDataSource_ListItem_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ListItemName = DataView_ListItemName[0];
        DropDownList_EditName_Unit.SelectedValue = Convert.ToString(DataRowView_ListItemName["ListItem_Name"], CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_InsertFormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertFormId");
      DropDownList DropDownList_InsertListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId");
      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent");
      Label Label_InsertNameError = (Label)FormView_ListItem_Form.FindControl("Label_InsertNameError");

      DropDownList_InsertListCategoryId.Items.Clear();
      SqlDataSource_ListItem_InsertListCategoryId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      DropDownList_InsertParent.Items.Clear();
      SqlDataSource_ListItem_InsertParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
      {
        SqlDataSource_ListItem_InsertListCategoryId.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_InsertParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        SqlDataSource_ListItem_InsertListCategoryId.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
        SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
        SqlDataSource_ListItem_InsertParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }

      DropDownList_InsertListCategoryId.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertListCategoryId.DataBind();

      DropDownList_InsertParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertParent.DataBind();

      Label_InsertNameError.Text = "";
    }

    protected void DropDownList_InsertListCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertFormId");
      DropDownList DropDownList_InsertListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId");
      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent");
      TextBox TextBox_InsertName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName");
      DropDownList DropDownList_InsertName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Facility");
      DropDownList DropDownList_InsertName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_ListCategory");
      DropDownList DropDownList_InsertName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertName_Unit");
      Label Label_InsertNameError = (Label)FormView_ListItem_Form.FindControl("Label_InsertNameError");

      DropDownList_InsertParent.Items.Clear();
      SqlDataSource_ListItem_InsertParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_InsertListCategoryId.SelectedValue))
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
        {
          SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_InsertParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
        {
          SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_InsertParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_InsertParent.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_InsertListCategoryId.SelectedValue.ToString();
      }

      DropDownList_InsertParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertParent.DataBind();

      if (DropDownList_InsertParent.Items.Count == 1)
      {
        DropDownList_InsertParent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      }


      DropDownList_InsertName_ListCategory.Items.Clear();
      SqlDataSource_ListItem_InsertName_ListCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListItem_InsertName_ListCategory.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_InsertListCategoryId.SelectedValue.ToString();
      DropDownList_InsertName_ListCategory.Items.Insert(0, new ListItem(Convert.ToString("Select Name", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertName_ListCategory.DataBind();


      Label_InsertNameError.Text = "";


      Session["ListCategoryLinkedCategoryList"] = "";
      string SQLStringListItemLinkedCategory = "SELECT ListCategory_LinkedCategory_List FROM Administration_ListCategory WHERE ListCategory_Id = @ListCategory_Id AND ListCategory_LinkedCategory = 1";
      using (SqlCommand SqlCommand_ListItemLinkedCategory = new SqlCommand(SQLStringListItemLinkedCategory))
      {
        SqlCommand_ListItemLinkedCategory.Parameters.AddWithValue("@ListCategory_Id", DropDownList_InsertListCategoryId.SelectedValue.ToString());
        DataTable DataTable_ListItemLinkedCategory;
        using (DataTable_ListItemLinkedCategory = new DataTable())
        {
          DataTable_ListItemLinkedCategory.Locale = CultureInfo.CurrentCulture;

          DataTable_ListItemLinkedCategory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemLinkedCategory).Copy();
          if (DataTable_ListItemLinkedCategory.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItemLinkedCategory.Rows)
            {
              Session["ListCategoryLinkedCategoryList"] = DataRow_Row["ListCategory_LinkedCategory_List"];
            }
          }
        }
      }

      if (Session["ListCategoryLinkedCategoryList"].ToString() == "Facility")
      {
        TextBox_InsertName.Visible = false;
        DropDownList_InsertName_Facility.Visible = true;
        DropDownList_InsertName_ListCategory.Visible = false;
        DropDownList_InsertName_Unit.Visible = false;
      }
      else if (Session["ListCategoryLinkedCategoryList"].ToString() == "List Category")
      {
        TextBox_InsertName.Visible = false;
        DropDownList_InsertName_Facility.Visible = false;
        DropDownList_InsertName_ListCategory.Visible = true;
        DropDownList_InsertName_Unit.Visible = false;
      }
      else if (Session["ListCategoryLinkedCategoryList"].ToString() == "Unit")
      {
        TextBox_InsertName.Visible = false;
        DropDownList_InsertName_Facility.Visible = false;
        DropDownList_InsertName_ListCategory.Visible = false;
        DropDownList_InsertName_Unit.Visible = true;
      }
      else
      {
        TextBox_InsertName.Visible = true;
        DropDownList_InsertName_Facility.Visible = false;
        DropDownList_InsertName_ListCategory.Visible = false;
        DropDownList_InsertName_Unit.Visible = false;
      }

      Session.Remove("ListCategoryLinkedCategoryList");
    }

    protected void TextBox_InsertName_TextChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertListCategoryId");
      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_InsertParent");
      TextBox TextBox_InsertName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_ListItem_Form.FindControl("Label_InsertNameError");

      Session["ListItemName"] = "";
      string SQLStringListItemName = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Name = @ListItem_Name AND ListCategory_Id = @ListCategory_Id AND ListItem_Parent = @ListItem_Parent AND ListItem_IsActive = @ListItem_IsActive";
      using (SqlCommand SqlCommand_ListItemName = new SqlCommand(SQLStringListItemName))
      {
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListCategory_Id", DropDownList_InsertListCategoryId.SelectedValue.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Parent", DropDownList_InsertParent.SelectedValue.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_IsActive", 1);
        DataTable DataTable_ListItemName;
        using (DataTable_ListItemName = new DataTable())
        {
          DataTable_ListItemName.Locale = CultureInfo.CurrentCulture;

          DataTable_ListItemName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemName).Copy();
          if (DataTable_ListItemName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItemName.Rows)
            {
              Session["ListItemName"] = DataRow_Row["ListItem_Name"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["ListItemName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<div style='color:#B0262E;'>A List Item with the Name '" + Session["ListItemName"].ToString() + "' already exists for the List Category</div>", CultureInfo.CurrentCulture);
      }

      Session["ListItemName"] = "";
    }


    protected void DropDownList_EditFormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditFormId");
      DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");
      Label Label_EditNameError = (Label)FormView_ListItem_Form.FindControl("Label_EditNameError");

      DropDownList_EditListCategoryId.Items.Clear();
      SqlDataSource_ListItem_EditListCategoryId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      DropDownList_EditParent.Items.Clear();
      SqlDataSource_ListItem_EditParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
      {
        SqlDataSource_ListItem_EditListCategoryId.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        SqlDataSource_ListItem_EditListCategoryId.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
        SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
        SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }

      DropDownList_EditListCategoryId.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_EditListCategoryId.DataBind();

      DropDownList_EditParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_EditParent.DataBind();

      Label_EditNameError.Text = "";
    }

    protected void DropDownList_EditFormId_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditFormId");

      Session["FormId"] = "";
      string SQLStringFormId = "SELECT Form_Id FROM Administration_ListCategory WHERE ListCategory_Id IN (SELECT ListCategory_Id FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id)";
      using (SqlCommand SqlCommand_FormId = new SqlCommand(SQLStringFormId))
      {
        SqlCommand_FormId.Parameters.AddWithValue("@ListItem_Id", Request.QueryString["ListItem_Id"]);
        DataTable DataTable_FormId;
        using (DataTable_FormId = new DataTable())
        {
          DataTable_FormId.Locale = CultureInfo.CurrentCulture;

          DataTable_FormId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormId).Copy();
          if (DataTable_FormId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormId.Rows)
            {
              Session["FormId"] = DataRow_Row["Form_Id"];
            }
          }
        }
      }

      DropDownList_EditFormId.SelectedValue = Session["FormId"].ToString();

      SqlDataSource_ListItem_EditListCategoryId.SelectParameters["Form_Id"].DefaultValue = Session["FormId"].ToString();
    }

    protected void DropDownList_EditListCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditFormId");
      DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");
      TextBox TextBox_EditName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName");
      DropDownList DropDownList_EditName_Facility = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Facility");
      DropDownList DropDownList_EditName_ListCategory = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_ListCategory");
      DropDownList DropDownList_EditName_Unit = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditName_Unit");
      Label Label_EditNameError = (Label)FormView_ListItem_Form.FindControl("Label_EditNameError");

      DropDownList_EditParent.Items.Clear();
      SqlDataSource_ListItem_EditParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_EditListCategoryId.SelectedValue))
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
        {
          SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
        {
          SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditListCategoryId.SelectedValue.ToString();
      }

      DropDownList_EditParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_EditParent.DataBind();

      if (DropDownList_EditParent.Items.Count == 1)
      {
        DropDownList_EditParent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      }


      DropDownList_EditName_ListCategory.Items.Clear();
      SqlDataSource_ListItem_EditName_ListCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListItem_EditName_ListCategory.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditListCategoryId.SelectedValue.ToString();
      DropDownList_EditName_ListCategory.Items.Insert(0, new ListItem(Convert.ToString("Select Name", CultureInfo.CurrentCulture), ""));
      DropDownList_EditName_ListCategory.DataBind();


      Label_EditNameError.Text = "";


      Session["ListCategoryLinkedCategoryList"] = "";
      string SQLStringListItemLinkedCategory = "SELECT ListCategory_LinkedCategory_List FROM Administration_ListCategory WHERE ListCategory_Id = @ListCategory_Id AND ListCategory_LinkedCategory = 1";
      using (SqlCommand SqlCommand_ListItemLinkedCategory = new SqlCommand(SQLStringListItemLinkedCategory))
      {
        SqlCommand_ListItemLinkedCategory.Parameters.AddWithValue("@ListCategory_Id", DropDownList_EditListCategoryId.SelectedValue.ToString());
        DataTable DataTable_ListItemLinkedCategory;
        using (DataTable_ListItemLinkedCategory = new DataTable())
        {
          DataTable_ListItemLinkedCategory.Locale = CultureInfo.CurrentCulture;

          DataTable_ListItemLinkedCategory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemLinkedCategory).Copy();
          if (DataTable_ListItemLinkedCategory.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItemLinkedCategory.Rows)
            {
              Session["ListCategoryLinkedCategoryList"] = DataRow_Row["ListCategory_LinkedCategory_List"];
            }
          }
        }
      }

      if (Session["ListCategoryLinkedCategoryList"].ToString() == "Facility")
      {
        TextBox_EditName.Visible = false;
        DropDownList_EditName_Facility.Visible = true;
        DropDownList_EditName_ListCategory.Visible = false;
        DropDownList_EditName_Unit.Visible = false;
      }
      else if (Session["ListCategoryLinkedCategoryList"].ToString() == "List Category")
      {
        TextBox_EditName.Visible = false;
        DropDownList_EditName_Facility.Visible = false;
        DropDownList_EditName_ListCategory.Visible = true;
        DropDownList_EditName_Unit.Visible = false;
      }
      else if (Session["ListCategoryLinkedCategoryList"].ToString() == "Unit")
      {
        TextBox_EditName.Visible = false;
        DropDownList_EditName_Facility.Visible = false;
        DropDownList_EditName_ListCategory.Visible = false;
        DropDownList_EditName_Unit.Visible = true;
      }
      else
      {
        TextBox_EditName.Visible = true;
        DropDownList_EditName_Facility.Visible = false;
        DropDownList_EditName_ListCategory.Visible = false;
        DropDownList_EditName_Unit.Visible = false;
      }

      Session.Remove("ListCategoryLinkedCategoryList");
    }

    protected void DropDownList_EditListCategoryId_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");

      Session["FormId"] = "";
      string SQLStringFormId = "SELECT Form_Id FROM Administration_ListCategory WHERE ListCategory_Id IN (SELECT ListCategory_Id FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id)";
      using (SqlCommand SqlCommand_FormId = new SqlCommand(SQLStringFormId))
      {
        SqlCommand_FormId.Parameters.AddWithValue("@ListItem_Id", Request.QueryString["ListItem_Id"]);
        DataTable DataTable_FormId;
        using (DataTable_FormId = new DataTable())
        {
          DataTable_FormId.Locale = CultureInfo.CurrentCulture;

          DataTable_FormId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormId).Copy();
          if (DataTable_FormId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormId.Rows)
            {
              Session["FormId"] = DataRow_Row["Form_Id"];
            }
          }
        }
      }

      SqlDataSource_ListItem_EditParent.SelectParameters["Form_Id"].DefaultValue = Session["FormId"].ToString();
      SqlDataSource_ListItem_EditParent.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditListCategoryId.SelectedValue.ToString();
      SqlDataSource_ListItem_EditName_ListCategory.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditListCategoryId.SelectedValue.ToString();
    }

    protected void DropDownList_EditParent_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");

      if (DropDownList_EditParent.Items.Count > 2)
      {
        if (DropDownList_EditParent.Items.FindByValue("-1") != null)
        {
          DropDownList_EditParent.Items.RemoveAt(DropDownList_EditParent.Items.IndexOf(DropDownList_EditParent.Items.FindByValue("-1")));
        }
      }
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditListCategoryId = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditListCategoryId");
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListItem_Form.FindControl("DropDownList_EditParent");
      TextBox TextBox_EditName = (TextBox)FormView_ListItem_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_ListItem_Form.FindControl("Label_EditNameError");

      Session["ListItemId"] = "";
      Session["ListItemName"] = "";
      string SQLStringListItemName = "SELECT ListItem_Id , ListItem_Name FROM Administration_ListItem WHERE ListItem_Name = @ListItem_Name AND ListCategory_Id = @ListCategory_Id AND ListItem_Parent = @ListItem_Parent AND ListItem_IsActive = @ListItem_IsActive";
      using (SqlCommand SqlCommand_ListItemName = new SqlCommand(SQLStringListItemName))
      {
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Name", TextBox_EditName.Text.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListCategory_Id", DropDownList_EditListCategoryId.SelectedValue.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Parent", DropDownList_EditParent.SelectedValue.ToString());
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_IsActive", 1);
        DataTable DataTable_ListItemName;
        using (DataTable_ListItemName = new DataTable())
        {
          DataTable_ListItemName.Locale = CultureInfo.CurrentCulture;

          DataTable_ListItemName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemName).Copy();
          if (DataTable_ListItemName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItemName.Rows)
            {
              Session["ListItemId"] = DataRow_Row["ListItem_Id"];
              Session["ListItemName"] = DataRow_Row["ListItem_Name"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["ListItemName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["ListItemId"].ToString() == Request.QueryString["ListItem_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<div style='color:#B0262E;'>A List Item with the Name '" + Session["ListItemName"].ToString() + "' already exists for the List Category</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["ListItemId"] = "";
      Session["ListItemName"] = "";
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}