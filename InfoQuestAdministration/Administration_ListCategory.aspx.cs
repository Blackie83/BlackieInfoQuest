using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_ListCategory : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_ListCategory_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_InsertFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_InsertParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_EditFormId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_EditParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_ListCategory, this.GetType(), "UpdateProgress", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["ListCategory_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_ListCategory.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["ListCategory_Id"] != null)
      {
        FormView_ListCategory_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_ListCategory_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_ListCategory_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_ListCategory_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_ListCategory_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((CheckBox)FormView_ListCategory_Form.FindControl("CheckBox_InsertLinkedCategory")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryChild")).Attributes.Add("OnChange", "Validation_Form();");
      }

      if (FormView_ListCategory_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditFormId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_ListCategory_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_ListCategory_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((CheckBox)FormView_ListCategory_Form.FindControl("CheckBox_EditLinkedCategory")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryChild")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FormId"];
      string SearchField2 = Request.QueryString["Search_ListCategoryId"];
      string SearchField3 = Request.QueryString["Search_ListCategoryParent"];
      string SearchField4 = Request.QueryString["Search_ListCategoryLinkedCategory"];
      string SearchField5 = Request.QueryString["Search_ListCategoryIsActive"];

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
        SearchField3 = "s_ListCategory_Parent=" + Request.QueryString["Search_ListCategoryParent"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_ListCategory_LinkedCategory=" + Request.QueryString["Search_ListCategoryLinkedCategory"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_ListCategory_IsActive=" + Request.QueryString["Search_ListCategoryIsActive"] + "&";
      }

      string FinalURL = "Administration_ListCategory_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("List Category List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_ListCategory_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_ListCategory_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertParent");
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_Parent"].DefaultValue = DropDownList_InsertParent.SelectedValue.ToString();

          DropDownList DropDownList_InsertLinkedCategoryList = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryList");
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_LinkedCategory_List"].DefaultValue = DropDownList_InsertLinkedCategoryList.SelectedValue.ToString();

          DropDownList DropDownList_InsertLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryParent");
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_LinkedCategory_List_ListCategory_Parent"].DefaultValue = DropDownList_InsertLinkedCategoryListListCategoryParent.SelectedValue.ToString();

          DropDownList DropDownList_InsertLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryChild");
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_LinkedCategory_List_ListCategory_Child"].DefaultValue = DropDownList_InsertLinkedCategoryListListCategoryChild.SelectedValue.ToString();

          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_History"].DefaultValue = "";
          SqlDataSource_ListCategory_Form.InsertParameters["ListCategory_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertFormId");
      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertParent");
      TextBox TextBox_InsertName = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_InsertName");
      TextBox TextBox_InsertDescription = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_InsertDescription");
      CheckBox CheckBox_InsertLinkedCategory = (CheckBox)FormView_ListCategory_Form.FindControl("CheckBox_InsertLinkedCategory");
      DropDownList DropDownList_InsertLinkedCategoryList = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryList");
      DropDownList DropDownList_InsertLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryParent");
      DropDownList DropDownList_InsertLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryChild");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertParent.SelectedValue))
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

        if (CheckBox_InsertLinkedCategory.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertLinkedCategoryList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else
          {
            if (DropDownList_InsertLinkedCategoryList.SelectedValue == "List Category")
            {
              if (string.IsNullOrEmpty(DropDownList_InsertLinkedCategoryListListCategoryParent.SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(DropDownList_InsertLinkedCategoryListListCategoryChild.SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["ListCategoryName"] = "";
        string SQLStringListCategoryName = "SELECT ListCategory_Name FROM Administration_ListCategory WHERE ListCategory_Name = @ListCategory_Name AND ListCategory_IsActive = @ListCategory_IsActive";
        using (SqlCommand SqlCommand_ListCategoryName = new SqlCommand(SQLStringListCategoryName))
        {
          SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_IsActive", 1);
          DataTable DataTable_ListCategoryName;
          using (DataTable_ListCategoryName = new DataTable())
          {
            DataTable_ListCategoryName.Locale = CultureInfo.CurrentCulture;

            DataTable_ListCategoryName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListCategoryName).Copy();
            if (DataTable_ListCategoryName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ListCategoryName.Rows)
              {
                Session["ListCategoryName"] = DataRow_Row["ListCategory_Name"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["ListCategoryName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A List Category with the Name '" + Session["ListCategoryName"].ToString() + "' already exists<br />";
        }

        Session["ListCategoryName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_ListCategory_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["ListCategory_Id"] = e.Command.Parameters["@ListCategory_Id"].Value;

        string SearchField1 = "s_ListCategory_Id=" + Session["ListCategory_Id"].ToString() + "";
        string FinalURL = "Administration_ListCategory_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("List Category List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_ListCategory_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDListCategoryModifiedDate"] = e.OldValues["ListCategory_ModifiedDate"];
        object OLDListCategoryModifiedDate = Session["OLDListCategoryModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDListCategoryModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareListCategory = (DataView)SqlDataSource_ListCategory_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareListCategory = DataView_CompareListCategory[0];
        Session["DBListCategoryModifiedDate"] = Convert.ToString(DataRowView_CompareListCategory["ListCategory_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBListCategoryModifiedBy"] = Convert.ToString(DataRowView_CompareListCategory["ListCategory_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBListCategoryModifiedDate = Session["DBListCategoryModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBListCategoryModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBListCategoryModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_ListCategory_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_ListCategory_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_ListCategory_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_ListCategory_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            DropDownList DropDownList_EditParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditParent");
            e.NewValues["ListCategory_Parent"] = DropDownList_EditParent.SelectedValue.ToString();

            DropDownList DropDownList_EditLinkedCategoryList = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryList");
            e.NewValues["ListCategory_LinkedCategory_List"] = DropDownList_EditLinkedCategoryList.SelectedValue.ToString();

            DropDownList DropDownList_EditLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent");
            e.NewValues["ListCategory_LinkedCategory_List_ListCategory_Parent"] = DropDownList_EditLinkedCategoryListListCategoryParent.SelectedValue.ToString();

            DropDownList DropDownList_EditLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryChild");
            e.NewValues["ListCategory_LinkedCategory_List_ListCategory_Child"] = DropDownList_EditLinkedCategoryListListCategoryChild.SelectedValue.ToString();

            e.NewValues["ListCategory_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ListCategory_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_ListCategory", "ListCategory_Id = " + Request.QueryString["ListCategory_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_ListCategory_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["ListCategoryHistory"] = Convert.ToString(DataRowView_Form["ListCategory_History"], CultureInfo.CurrentCulture);

            Session["ListCategoryHistory"] = Session["History"].ToString() + Session["ListCategoryHistory"].ToString();
            e.NewValues["ListCategory_History"] = Session["ListCategoryHistory"].ToString();

            Session["ListCategoryHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDListCategoryModifiedDate"] = "";
        Session["DBListCategoryModifiedDate"] = "";
        Session["DBListCategoryModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditFormId");
      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditParent");
      TextBox TextBox_EditName = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_EditName");
      TextBox TextBox_EditDescription = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_EditDescription");
      CheckBox CheckBox_EditLinkedCategory = (CheckBox)FormView_ListCategory_Form.FindControl("CheckBox_EditLinkedCategory");
      DropDownList DropDownList_EditLinkedCategoryList = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryList");
      DropDownList DropDownList_EditLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent");
      DropDownList DropDownList_EditLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryChild");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditParent.SelectedValue))
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

        if (CheckBox_EditLinkedCategory.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditLinkedCategoryList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else
          {
            if (DropDownList_EditLinkedCategoryList.SelectedValue == "List Category")
            {
              if (string.IsNullOrEmpty(DropDownList_EditLinkedCategoryListListCategoryParent.SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(DropDownList_EditLinkedCategoryListListCategoryChild.SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["ListCategoryId"] = "";
        Session["ListCategoryName"] = "";
        string SQLStringListCategoryName = "SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE ListCategory_Name = @ListCategory_Name AND ListCategory_IsActive = @ListCategory_IsActive";
        using (SqlCommand SqlCommand_ListCategoryName = new SqlCommand(SQLStringListCategoryName))
        {
          SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_Name", TextBox_EditName.Text.ToString());
          SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_IsActive", 1);
          DataTable DataTable_ListCategoryName;
          using (DataTable_ListCategoryName = new DataTable())
          {
            DataTable_ListCategoryName.Locale = CultureInfo.CurrentCulture;

            DataTable_ListCategoryName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListCategoryName).Copy();
            if (DataTable_ListCategoryName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ListCategoryName.Rows)
              {
                Session["ListCategoryId"] = DataRow_Row["ListCategory_Id"];
                Session["ListCategoryName"] = DataRow_Row["ListCategory_Name"];
              }
            }
            else
            {
              Session["ListCategoryId"] = "";
              Session["ListCategoryName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["ListCategoryName"].ToString()))
        {
          if (Session["ListCategoryId"].ToString() != Request.QueryString["ListCategory_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A List Category with the Name '" + Session["ListCategoryName"].ToString() + "' already exists<br />";
          }
        }

        Session["ListCategoryId"] = "";
        Session["ListCategoryName"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_ListCategory_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_ListCategory_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void DropDownList_InsertFormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFormId = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertFormId");


      DropDownList DropDownList_InsertParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertParent");
      DropDownList_InsertParent.Items.Clear();
      SqlDataSource_ListCategory_InsertParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_InsertParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
      DropDownList_InsertParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertParent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      DropDownList_InsertParent.DataBind();


      DropDownList DropDownList_InsertLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryParent");
      DropDownList_InsertLinkedCategoryListListCategoryParent.Items.Clear();
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
      DropDownList_InsertLinkedCategoryListListCategoryParent.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertLinkedCategoryListListCategoryParent.DataBind();


      DropDownList DropDownList_InsertLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryChild");
      DropDownList_InsertLinkedCategoryListListCategoryChild.Items.Clear();
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild.SelectParameters["Form_Id"].DefaultValue = DropDownList_InsertFormId.SelectedValue.ToString();
      DropDownList_InsertLinkedCategoryListListCategoryChild.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertLinkedCategoryListListCategoryChild.DataBind();
    }

    protected void TextBox_InsertName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertName = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_ListCategory_Form.FindControl("Label_InsertNameError");

      Session["ListCategoryName"] = "";
      string SQLStringListCategoryName = "SELECT ListCategory_Name FROM Administration_ListCategory WHERE ListCategory_Name = @ListCategory_Name AND ListCategory_IsActive = @ListCategory_IsActive";
      using (SqlCommand SqlCommand_ListCategoryName = new SqlCommand(SQLStringListCategoryName))
      {
        SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_IsActive", 1);
        DataTable DataTable_ListCategoryName;
        using (DataTable_ListCategoryName = new DataTable())
        {
          DataTable_ListCategoryName.Locale = CultureInfo.CurrentCulture;

          DataTable_ListCategoryName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListCategoryName).Copy();
          if (DataTable_ListCategoryName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListCategoryName.Rows)
            {
              Session["ListCategoryName"] = DataRow_Row["ListCategory_Name"];
            }
          }
          else
          {
            Session["ListCategoryName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["ListCategoryName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A List Category with the Name '" + Session["ListCategoryName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["ListCategoryName"] = "";
    }

    protected void DropDownList_InsertLinkedCategoryListListCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryParent");


      DropDownList DropDownList_InsertLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_InsertLinkedCategoryListListCategoryChild");
      DropDownList_InsertLinkedCategoryListListCategoryChild.Items.Clear();
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_InsertLinkedCategoryListListCategoryParent.SelectedValue.ToString();
      DropDownList_InsertLinkedCategoryListListCategoryChild.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertLinkedCategoryListListCategoryChild.DataBind();
    }


    protected void DropDownList_EditFormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditFormId");


      DropDownList DropDownList_EditParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditParent");
      DropDownList_EditParent.Items.Clear();
      SqlDataSource_ListCategory_EditParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_EditParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
      DropDownList_EditParent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_EditParent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      DropDownList_EditParent.DataBind();


      DropDownList DropDownList_EditLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent");
      DropDownList_EditLinkedCategoryListListCategoryParent.Items.Clear();
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
      DropDownList_EditLinkedCategoryListListCategoryParent.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_EditLinkedCategoryListListCategoryParent.DataBind();


      DropDownList DropDownList_EditLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryChild");
      DropDownList_EditLinkedCategoryListListCategoryChild.Items.Clear();
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
      DropDownList_EditLinkedCategoryListListCategoryChild.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_EditLinkedCategoryListListCategoryChild.DataBind();
    }

    protected void DropDownList_EditFormId_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditFormId = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditFormId");
      SqlDataSource_ListCategory_EditParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.SelectParameters["Form_Id"].DefaultValue = DropDownList_EditFormId.SelectedValue.ToString();
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_ListCategory_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_ListCategory_Form.FindControl("Label_EditNameError");

      Session["ListCategoryId"] = "";
      Session["ListCategoryName"] = "";
      string SQLStringListCategoryName = "SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE ListCategory_Name = @ListCategory_Name AND ListCategory_IsActive = @ListCategory_IsActive";
      using (SqlCommand SqlCommand_ListCategoryName = new SqlCommand(SQLStringListCategoryName))
      {
        SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_Name", TextBox_EditName.Text.ToString());
        SqlCommand_ListCategoryName.Parameters.AddWithValue("@ListCategory_IsActive", 1);
        DataTable DataTable_ListCategoryName;
        using (DataTable_ListCategoryName = new DataTable())
        {
          DataTable_ListCategoryName.Locale = CultureInfo.CurrentCulture;

          DataTable_ListCategoryName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListCategoryName).Copy();
          if (DataTable_ListCategoryName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListCategoryName.Rows)
            {
              Session["ListCategoryId"] = DataRow_Row["ListCategory_Id"];
              Session["ListCategoryName"] = DataRow_Row["ListCategory_Name"];
            }
          }
          else
          {
            Session["ListCategoryId"] = "";
            Session["ListCategoryName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["ListCategoryName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["ListCategoryId"].ToString() == Request.QueryString["ListCategory_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A List Category with the Name '" + Session["ListCategoryName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["ListCategoryId"] = "";
      Session["ListCategoryName"] = "";
    }

    protected void DropDownList_EditLinkedCategoryListListCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent");


      DropDownList DropDownList_EditLinkedCategoryListListCategoryChild = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryChild");
      DropDownList_EditLinkedCategoryListListCategoryChild.Items.Clear();
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditLinkedCategoryListListCategoryParent.SelectedValue.ToString();
      DropDownList_EditLinkedCategoryListListCategoryChild.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_EditLinkedCategoryListListCategoryChild.DataBind();
    }

    protected void DropDownList_EditLinkedCategoryListListCategoryParent_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLinkedCategoryListListCategoryParent = (DropDownList)FormView_ListCategory_Form.FindControl("DropDownList_EditLinkedCategoryListListCategoryParent");
      SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_EditLinkedCategoryListListCategoryParent.SelectedValue.ToString();
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}