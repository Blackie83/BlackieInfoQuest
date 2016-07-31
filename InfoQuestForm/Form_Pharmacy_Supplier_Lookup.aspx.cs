using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Pharmacy_Supplier_Lookup : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_Pharmacy_Supplier_Lookup, this.GetType(), "UpdateProgress_Start", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString("Pharmacy : Supplier Lookup", CultureInfo.CurrentCulture);

          SetFormQueryString();

          SetFormVisibility();

          TableFormVisible();
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
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('6','12','13','14','16','18','21','33')) AND (SecurityRole_Id IN (23,24,25,26,33,43,65,135))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
          Response.End();
        }
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Pharmacy_Supplier_Lookup.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy: New Product Code Request", "6");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Pharmacy_Supplier_Lookup_Code.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_Supplier_Lookup_Code.SelectCommand = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Code FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Code IS NOT NULL ORDER BY Pharmacy_Supplier_Lookup_Code";
      SqlDataSource_Pharmacy_Supplier_Lookup_Code.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_Supplier_Lookup_Description.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_Supplier_Lookup_Description.SelectCommand = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup ORDER BY Pharmacy_Supplier_Lookup_Description";
      SqlDataSource_Pharmacy_Supplier_Lookup_Description.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_Supplier_Lookup_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectCommand = "spForm_Get_Pharmacy_Supplier_Lookup_List";
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Pharmacy_Supplier_Lookup_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectParameters.Clear();
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectParameters.Add("Code", TypeCode.String, Request.QueryString["s_Code"]);
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectParameters.Add("Description", TypeCode.String, Request.QueryString["s_Description"]);
      SqlDataSource_Pharmacy_Supplier_Lookup_List.SelectParameters.Add("IsActive", TypeCode.String, Request.QueryString["s_IsActive"]);

      SqlDataSource_Pharmacy_Supplier_Lookup_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertCommand="INSERT INTO Form_Pharmacy_Supplier_Lookup (Pharmacy_Supplier_Lookup_Code ,Pharmacy_Supplier_Lookup_Description ,Pharmacy_Supplier_Lookup_CreatedDate ,Pharmacy_Supplier_Lookup_CreatedBy ,Pharmacy_Supplier_Lookup_ModifiedDate ,Pharmacy_Supplier_Lookup_ModifiedBy ,Pharmacy_Supplier_Lookup_History ,Pharmacy_Supplier_Lookup_IsActive) VALUES ( @Pharmacy_Supplier_Lookup_Code , @Pharmacy_Supplier_Lookup_Description ,@Pharmacy_Supplier_Lookup_CreatedDate ,@Pharmacy_Supplier_Lookup_CreatedBy ,@Pharmacy_Supplier_Lookup_ModifiedDate ,@Pharmacy_Supplier_Lookup_ModifiedBy ,@Pharmacy_Supplier_Lookup_History ,@Pharmacy_Supplier_Lookup_IsActive); SELECT @Pharmacy_Supplier_Lookup_Id = SCOPE_IDENTITY()";
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.SelectCommand="SELECT * FROM Form_Pharmacy_Supplier_Lookup WHERE (Pharmacy_Supplier_Lookup_Id = @Pharmacy_Supplier_Lookup_Id)";
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateCommand="UPDATE Form_Pharmacy_Supplier_Lookup SET Pharmacy_Supplier_Lookup_Code = @Pharmacy_Supplier_Lookup_Code ,Pharmacy_Supplier_Lookup_Description = @Pharmacy_Supplier_Lookup_Description ,Pharmacy_Supplier_Lookup_ModifiedDate = @Pharmacy_Supplier_Lookup_ModifiedDate ,Pharmacy_Supplier_Lookup_ModifiedBy = @Pharmacy_Supplier_Lookup_ModifiedBy ,Pharmacy_Supplier_Lookup_History = @Pharmacy_Supplier_Lookup_History ,Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive WHERE Pharmacy_Supplier_Lookup_Id = @Pharmacy_Supplier_Lookup_Id";
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Clear();
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_Id", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_Code", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_Description", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_CreatedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_History", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters.Add("Pharmacy_Supplier_Lookup_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.SelectParameters.Clear();
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.SelectParameters.Add("Pharmacy_Supplier_Lookup_Id", TypeCode.Int32, Request.QueryString["Pharmacy_Supplier_Lookup_Id"]);
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Clear();
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_Code", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_Description", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_History", TypeCode.String, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_Supplier_Lookup_Form.UpdateParameters.Add("Pharmacy_Supplier_Lookup_Id", TypeCode.Int32, "");
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Code.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Code"]))
        {
          DropDownList_Code.SelectedValue = "";
        }
        else
        {
          DropDownList_Code.SelectedValue = Request.QueryString["s_Code"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Description.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Description"]))
        {
          DropDownList_Description.SelectedValue = "";
        }
        else
        {
          DropDownList_Description.SelectedValue = Request.QueryString["s_Description"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IsActive"]))
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_IsActive"];
        }
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Pharmacy_Supplier_Lookup_Id"] != null)
      {
        FormView_Pharmacy_Supplier_Lookup_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Pharmacy_Supplier_Lookup_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Pharmacy_Supplier_Lookup_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertCode")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (FormView_Pharmacy_Supplier_Lookup_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditCode")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["s_Code"];
      string SearchField2 = Request.QueryString["s_Description"];
      string SearchField3 = Request.QueryString["s_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Code=" + Request.QueryString["s_Code"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Description=" + Request.QueryString["s_Description"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_IsActive=" + Request.QueryString["s_IsActive"] + "&";
      }

      string FinalURL = "Form_Pharmacy_Supplier_Lookup.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Supplier Lookup", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Code.SelectedValue;
      string SearchField2 = DropDownList_Description.SelectedValue;
      string SearchField3 = DropDownList_IsActive.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Code=" + DropDownList_Code.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Description=" + DropDownList_Description.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_Pharmacy_Supplier_Lookup.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Supplier Lookup", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Supplier Lookup", "Form_Pharmacy_Supplier_Lookup.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Pharmacy_Supplier_Lookup_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Pharmacy_Supplier_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Pharmacy_Supplier_Lookup_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_Pharmacy_Supplier_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Pharmacy_Supplier_Lookup_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_Pharmacy_Supplier_Lookup_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Pharmacy_Supplier_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Pharmacy_Supplier_Lookup_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Pharmacy_Supplier_Lookup_List.PageSize > 20 && GridView_Pharmacy_Supplier_Lookup_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Pharmacy_Supplier_Lookup_List.PageSize > 50 && GridView_Pharmacy_Supplier_Lookup_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_Pharmacy_Supplier_Lookup_List.Rows.Count; i++)
      {
        if (GridView_Pharmacy_Supplier_Lookup_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].Text.ToString() == "Yes")
          {
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].Text.ToString() == "No")
          {
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_Pharmacy_Supplier_Lookup_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_Pharmacy_Supplier_Lookup_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Pharmacy_Supplier_Lookup_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Pharmacy_Supplier_Lookup_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Pharmacy_Supplier_Lookup_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Pharmacy_Supplier_Lookup_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_CaptureNew_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Supplier Lookup", "Form_Pharmacy_Supplier_Lookup.aspx"), false);
    }

    public string GetLink(object pharmacy_Supplier_Lookup_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Supplier Lookup", "Form_Pharmacy_Supplier_Lookup.aspx?Pharmacy_Supplier_Lookup_Id=" + pharmacy_Supplier_Lookup_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Code"];
      string SearchField2 = Request.QueryString["s_Description"];
      string SearchField3 = Request.QueryString["s_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Code=" + Request.QueryString["s_Code"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Description=" + Request.QueryString["s_Description"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_IsActive=" + Request.QueryString["s_IsActive"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
      }
      else
      {
        FinalURL = LinkURL;
      }

      return FinalURL;
    }
    //---END--- --List--//


    //--START-- --Form--//
    protected void FormView_Pharmacy_Supplier_Lookup_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_History"].DefaultValue = "";
          SqlDataSource_Pharmacy_Supplier_Lookup_Form.InsertParameters["Pharmacy_Supplier_Lookup_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertCode = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertCode");
      TextBox TextBox_InsertDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertDescription");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertCode.Text))
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
        Session["PharmacySupplierLookupCode"] = "";
        string SQLStringCode = "SELECT Pharmacy_Supplier_Lookup_Code FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Code = @Pharmacy_Supplier_Lookup_Code AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
        using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
        {
          SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Code", TextBox_InsertCode.Text.ToString());
          SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
          DataTable DataTable_Code;
          using (DataTable_Code = new DataTable())
          {
            DataTable_Code.Locale = CultureInfo.CurrentCulture;
            DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
            if (DataTable_Code.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Code.Rows)
              {
                Session["PharmacySupplierLookupCode"] = DataRow_Row["Pharmacy_Supplier_Lookup_Code"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["PharmacySupplierLookupCode"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Supplier with the Code '" + Session["PharmacySupplierLookupCode"].ToString() + "' already exists<br />";
        }

        Session.Remove("PharmacySupplierLookupCode");


        Session["PharmacySupplierLookupDescription"] = "";
        string SQLStringDescription = "SELECT Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Description = @Pharmacy_Supplier_Lookup_Description AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
        using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
        {
          SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Description", TextBox_InsertDescription.Text.ToString());
          SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
          DataTable DataTable_Description;
          using (DataTable_Description = new DataTable())
          {
            DataTable_Description.Locale = CultureInfo.CurrentCulture;
            DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
            if (DataTable_Description.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Description.Rows)
              {
                Session["PharmacySupplierLookupDescription"] = DataRow_Row["Pharmacy_Supplier_Lookup_Description"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["PharmacySupplierLookupDescription"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Supplier with the Description '" + Session["PharmacySupplierLookupDescription"].ToString() + "' already exists<br />";
        }

        Session.Remove("PharmacySupplierLookupDescription");
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Pharmacy_Supplier_Lookup_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Id"] = e.Command.Parameters["@Pharmacy_Supplier_Lookup_Id"].Value;

        string FinalURL = "Form_Pharmacy_Supplier_Lookup.aspx";
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Supplier Lookup", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Pharmacy_Supplier_Lookup_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDPharmacySupplierLookupModifiedDate"] = e.OldValues["Pharmacy_Supplier_Lookup_ModifiedDate"];
        object OLDPharmacySupplierLookupModifiedDate = Session["OLDPharmacySupplierLookupModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDPharmacySupplierLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePharmacySupplierLookup = (DataView)SqlDataSource_Pharmacy_Supplier_Lookup_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePharmacySupplierLookup = DataView_ComparePharmacySupplierLookup[0];
        Session["DBPharmacySupplierLookupModifiedDate"] = Convert.ToString(DataRowView_ComparePharmacySupplierLookup["Pharmacy_Supplier_Lookup_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBPharmacySupplierLookupModifiedBy"] = Convert.ToString(DataRowView_ComparePharmacySupplierLookup["Pharmacy_Supplier_Lookup_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBPharmacySupplierLookupModifiedDate = Session["DBPharmacySupplierLookupModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBPharmacySupplierLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBPharmacySupplierLookupModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["Pharmacy_Supplier_Lookup_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Pharmacy_Supplier_Lookup_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_Pharmacy_Supplier_Lookup", "Pharmacy_Supplier_Lookup_Id = " + Request.QueryString["Pharmacy_Supplier_Lookup_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_Pharmacy_Supplier_Lookup_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["PharmacySupplierLookupHistory"] = Convert.ToString(DataRowView_Form["Pharmacy_Supplier_Lookup_History"], CultureInfo.CurrentCulture);

            Session["PharmacySupplierLookupHistory"] = Session["History"].ToString() + Session["PharmacySupplierLookupHistory"].ToString();
            e.NewValues["Pharmacy_Supplier_Lookup_History"] = Session["PharmacySupplierLookupHistory"].ToString();

            Session["PharmacySupplierLookupHistory"] = "";
            Session["History"] = "";
          }
        }

        Session.Remove("OLDPharmacySupplierLookupModifiedDate");
        Session.Remove("DBPharmacySupplierLookupModifiedDate");
        Session.Remove("DBPharmacySupplierLookupModifiedBy");
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditCode = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditCode");
      TextBox TextBox_EditDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditDescription");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditCode.Text))
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
        Session["PharmacySupplierLookupId"] = "";
        Session["PharmacySupplierLookupCode"] = "";
        string SQLStringCode = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Code FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Code = @Pharmacy_Supplier_Lookup_Code AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
        using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
        {
          SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Code", TextBox_EditCode.Text.ToString());
          SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
          DataTable DataTable_Code;
          using (DataTable_Code = new DataTable())
          {
            DataTable_Code.Locale = CultureInfo.CurrentCulture;
            DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
            if (DataTable_Code.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Code.Rows)
              {
                Session["PharmacySupplierLookupId"] = DataRow_Row["Pharmacy_Supplier_Lookup_Id"];
                Session["PharmacySupplierLookupCode"] = DataRow_Row["Pharmacy_Supplier_Lookup_Code"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["PharmacySupplierLookupCode"].ToString()))
        {
          if (Session["PharmacySupplierLookupId"].ToString() != Request.QueryString["Pharmacy_Supplier_Lookup_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Supplier with the Code '" + Session["PharmacySupplierLookupCode"].ToString() + "' already exists<br />";
          }
        }

        Session.Remove("PharmacySupplierLookupId");
        Session.Remove("PharmacySupplierLookupCode");


        Session["PharmacySupplierLookupId"] = "";
        Session["PharmacySupplierLookupDescription"] = "";
        string SQLStringDescription = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Description = @Pharmacy_Supplier_Lookup_Description AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
        using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
        {
          SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Description", TextBox_EditDescription.Text.ToString());
          SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
          DataTable DataTable_Description;
          using (DataTable_Description = new DataTable())
          {
            DataTable_Description.Locale = CultureInfo.CurrentCulture;
            DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
            if (DataTable_Description.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Description.Rows)
              {
                Session["PharmacySupplierLookupId"] = DataRow_Row["Pharmacy_Supplier_Lookup_Id"];
                Session["PharmacySupplierLookupDescription"] = DataRow_Row["Pharmacy_Supplier_Lookup_Description"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["PharmacySupplierLookupDescription"].ToString()))
        {
          if (Session["PharmacySupplierLookupId"].ToString() != Request.QueryString["Pharmacy_Supplier_Lookup_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Supplier with the Description '" + Session["PharmacySupplierLookupDescription"].ToString() + "' already exists<br />";
          }
        }

        Session.Remove("PharmacySupplierLookupId");
        Session.Remove("PharmacySupplierLookupDescription");
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Pharmacy_Supplier_Lookup_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Pharmacy_Supplier_Lookup_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void TextBox_InsertCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertCode = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertCode");
      Label Label_InsertCodeError = (Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_InsertCodeError");
      TextBox TextBox_InsertDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertDescription");

      Session["PharmacySupplierLookupCode"] = "";
      string SQLStringCode = "SELECT Pharmacy_Supplier_Lookup_Code FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Code = @Pharmacy_Supplier_Lookup_Code AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
      using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
      {
        SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Code", TextBox_InsertCode.Text.ToString());
        SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
        DataTable DataTable_Code;
        using (DataTable_Code = new DataTable())
        {
          DataTable_Code.Locale = CultureInfo.CurrentCulture;
          DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
          if (DataTable_Code.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Code.Rows)
            {
              Session["PharmacySupplierLookupCode"] = DataRow_Row["Pharmacy_Supplier_Lookup_Code"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["PharmacySupplierLookupCode"].ToString()))
      {
        Label_InsertCodeError.Text = "";
        ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_InsertDescription);
      }
      else
      {
        Label_InsertCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Supplier with the Code '" + Session["PharmacySupplierLookupCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_InsertCode);
      }

      Session.Remove("PharmacySupplierLookupCode");
    }

    protected void TextBox_InsertDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_InsertDescription");
      Label Label_InsertDescriptionError = (Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_InsertDescriptionError");

      Session["PharmacySupplierLookupDescription"] = "";
      string SQLStringDescription = "SELECT Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Description = @Pharmacy_Supplier_Lookup_Description AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
      using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
      {
        SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Description", TextBox_InsertDescription.Text.ToString());
        SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
        DataTable DataTable_Description;
        using (DataTable_Description = new DataTable())
        {
          DataTable_Description.Locale = CultureInfo.CurrentCulture;

          DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
          if (DataTable_Description.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Description.Rows)
            {
              Session["PharmacySupplierLookupDescription"] = DataRow_Row["Pharmacy_Supplier_Lookup_Description"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["PharmacySupplierLookupDescription"].ToString()))
      {
        Label_InsertDescriptionError.Text = "";
      }
      else
      {
        Label_InsertDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Supplier with the Description '" + Session["PharmacySupplierLookupDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_InsertDescription);
      }

      Session.Remove("PharmacySupplierLookupDescription");
    }

    protected void TextBox_EditCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditCode = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditCode");
      Label Label_EditCodeError = (Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditCodeError");
      TextBox TextBox_EditDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditDescription");

      Session["PharmacySupplierLookupId"] = "";
      Session["PharmacySupplierLookupCode"] = "";
      string SQLStringCode = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Code FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Code = @Pharmacy_Supplier_Lookup_Code AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
      using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
      {
        SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Code", TextBox_EditCode.Text.ToString());
        SqlCommand_Code.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
        DataTable DataTable_Code;
        using (DataTable_Code = new DataTable())
        {
          DataTable_Code.Locale = CultureInfo.CurrentCulture;

          DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
          if (DataTable_Code.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Code.Rows)
            {
              Session["PharmacySupplierLookupId"] = DataRow_Row["Pharmacy_Supplier_Lookup_Id"];
              Session["PharmacySupplierLookupCode"] = DataRow_Row["Pharmacy_Supplier_Lookup_Code"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["PharmacySupplierLookupCode"].ToString()))
      {
        Label_EditCodeError.Text = "";
      }
      else
      {
        if (Session["PharmacySupplierLookupId"].ToString() == Request.QueryString["Pharmacy_Supplier_Lookup_Id"])
        {
          Label_EditCodeError.Text = "";
          ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_EditDescription);
        }
        else
        {
          Label_EditCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Supplier with the Code '" + Session["PharmacySupplierLookupCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
          ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_EditCode);
        }
      }

      Session.Remove("PharmacySupplierLookupId");
      Session.Remove("PharmacySupplierLookupCode");
    }

    protected void TextBox_EditDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditDescription = (TextBox)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("TextBox_EditDescription");
      Label Label_EditDescriptionError = (Label)FormView_Pharmacy_Supplier_Lookup_Form.FindControl("Label_EditDescriptionError");

      Session["PharmacySupplierLookupId"] = "";
      Session["PharmacySupplierLookupDescription"] = "";
      string SQLStringDescription = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_Description = @Pharmacy_Supplier_Lookup_Description AND Pharmacy_Supplier_Lookup_IsActive = @Pharmacy_Supplier_Lookup_IsActive";
      using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
      {
        SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_Description", TextBox_EditDescription.Text.ToString());
        SqlCommand_Description.Parameters.AddWithValue("@Pharmacy_Supplier_Lookup_IsActive", 1);
        DataTable DataTable_Description;
        using (DataTable_Description = new DataTable())
        {
          DataTable_Description.Locale = CultureInfo.CurrentCulture;

          DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
          if (DataTable_Description.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Description.Rows)
            {
              Session["PharmacySupplierLookupId"] = DataRow_Row["Pharmacy_Supplier_Lookup_Id"];
              Session["PharmacySupplierLookupDescription"] = DataRow_Row["Pharmacy_Supplier_Lookup_Description"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["PharmacySupplierLookupDescription"].ToString()))
      {
        Label_EditDescriptionError.Text = "";
      }
      else
      {
        if (Session["PharmacySupplierLookupId"].ToString() == Request.QueryString["Pharmacy_Supplier_Lookup_Id"])
        {
          Label_EditDescriptionError.Text = "";
        }
        else
        {
          Label_EditDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Supplier with the Description '" + Session["PharmacySupplierLookupDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
          ToolkitScriptManager_Pharmacy_Supplier_Lookup.SetFocus(TextBox_EditDescription);
        }
      }

      Session.Remove("PharmacySupplierLookupId");
      Session.Remove("PharmacySupplierLookupDescription");
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}