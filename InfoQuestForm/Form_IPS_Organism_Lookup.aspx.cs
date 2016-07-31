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
  public partial class Form_IPS_Organism_Lookup : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_IPS_Organism_Lookup, this.GetType(), "UpdateProgress_Start", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (!string.IsNullOrEmpty(Request.QueryString["IPS_Organism_Lookup_Id"]))
          {
            SqlDataSource_IPS_EditTypeList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Organism_Lookup_Type_List";
            SqlDataSource_IPS_EditTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Organism_Lookup";
            SqlDataSource_IPS_EditTypeList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Organism_Lookup_Id = " + Request.QueryString["IPS_Organism_Lookup_Id"] + " ";
          }

          Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Organism Lookup", CultureInfo.CurrentCulture);

          SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters["TableSELECT"].DefaultValue = "IPS_Organism_Lookup_Type_List";
          SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Organism_Lookup";

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37')) AND (SecurityRole_Id = 22)";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_Organism_Lookup.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_Organism_Lookup_Type.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_Organism_Lookup_Type.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Clear();
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "161");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_Organism_Lookup_Type.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_Organism_Lookup_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Organism_Lookup_List.SelectCommand = "spForm_Get_IPS_Organism_Lookup_List";
      SqlDataSource_IPS_Organism_Lookup_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Organism_Lookup_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Clear();
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_Name"]);
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Add("Type", TypeCode.String, Request.QueryString["s_Type"]);
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Add("Resistance", TypeCode.String, Request.QueryString["s_Resistance"]);
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Add("Notifiable", TypeCode.String, Request.QueryString["s_Notifiable"]);
      SqlDataSource_IPS_Organism_Lookup_List.SelectParameters.Add("IsActive", TypeCode.String, Request.QueryString["s_IsActive"]);

      SqlDataSource_IPS_InsertTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "161");
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "161");
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_Organism_Lookup_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertCommand="INSERT INTO Form_IPS_Organism_Lookup (IPS_Organism_Lookup_Code ,IPS_Organism_Lookup_Description , IPS_Organism_Lookup_Type_List ,IPS_Organism_Lookup_Resistance ,IPS_Organism_Lookup_Notifiable ,IPS_Organism_Lookup_CreatedDate ,IPS_Organism_Lookup_CreatedBy ,IPS_Organism_Lookup_ModifiedDate ,IPS_Organism_Lookup_ModifiedBy ,IPS_Organism_Lookup_History ,IPS_Organism_Lookup_IsActive) VALUES ( @IPS_Organism_Lookup_Code , @IPS_Organism_Lookup_Description , @IPS_Organism_Lookup_Type_List , @IPS_Organism_Lookup_Resistance ,@IPS_Organism_Lookup_Notifiable ,@IPS_Organism_Lookup_CreatedDate ,@IPS_Organism_Lookup_CreatedBy ,@IPS_Organism_Lookup_ModifiedDate ,@IPS_Organism_Lookup_ModifiedBy ,@IPS_Organism_Lookup_History ,@IPS_Organism_Lookup_IsActive); SELECT @IPS_Organism_Lookup_Id = SCOPE_IDENTITY()";
      SqlDataSource_IPS_Organism_Lookup_Form.SelectCommand="SELECT * FROM Form_IPS_Organism_Lookup WHERE (IPS_Organism_Lookup_Id = @IPS_Organism_Lookup_Id)";
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateCommand="UPDATE Form_IPS_Organism_Lookup SET IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code ,IPS_Organism_Lookup_Description = @IPS_Organism_Lookup_Description , IPS_Organism_Lookup_Type_List = @IPS_Organism_Lookup_Type_List , IPS_Organism_Lookup_Resistance = @IPS_Organism_Lookup_Resistance ,IPS_Organism_Lookup_Notifiable = @IPS_Organism_Lookup_Notifiable ,IPS_Organism_Lookup_ModifiedDate = @IPS_Organism_Lookup_ModifiedDate ,IPS_Organism_Lookup_ModifiedBy = @IPS_Organism_Lookup_ModifiedBy ,IPS_Organism_Lookup_History = @IPS_Organism_Lookup_History ,IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive WHERE IPS_Organism_Lookup_Id = @IPS_Organism_Lookup_Id";
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Clear();
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Code", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Description", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Resistance", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_Notifiable", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_CreatedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_History", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters.Add("IPS_Organism_Lookup_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.SelectParameters.Clear();
      SqlDataSource_IPS_Organism_Lookup_Form.SelectParameters.Add("IPS_Organism_Lookup_Id", TypeCode.Int32, Request.QueryString["IPS_Organism_Lookup_Id"]);
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Code", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Description", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Resistance", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Notifiable", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_History", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Lookup_Form.UpdateParameters.Add("IPS_Organism_Lookup_Id", TypeCode.Int32, "");
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_Name.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Name"]))
        {
          TextBox_Name.Text = "";
        }
        else
        {
          TextBox_Name.Text = Request.QueryString["s_Name"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Type.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Type"]))
        {
          DropDownList_Type.SelectedValue = "";
        }
        else
        {
          DropDownList_Type.SelectedValue = Request.QueryString["s_Type"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Resistance.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Resistance"]))
        {
          DropDownList_Resistance.SelectedValue = "";
        }
        else
        {
          DropDownList_Resistance.SelectedValue = Request.QueryString["s_Resistance"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Notifiable.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Notifiable"]))
        {
          DropDownList_Notifiable.SelectedValue = "";
        }
        else
        {
          DropDownList_Notifiable.SelectedValue = Request.QueryString["s_Notifiable"];
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
      if (Request.QueryString["IPS_Organism_Lookup_Id"] != null)
      {
        FormView_IPS_Organism_Lookup_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_IPS_Organism_Lookup_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_IPS_Organism_Lookup_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertCode")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_IPS_Organism_Lookup_Form.FindControl("DropDownList_InsertTypeList")).Attributes.Add("OnChange", "Validation_Form();");
      }

      if (FormView_IPS_Organism_Lookup_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditCode")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((DropDownList)FormView_IPS_Organism_Lookup_Form.FindControl("DropDownList_EditTypeList")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["s_Name"];
      string SearchField2 = Request.QueryString["s_Type"];
      string SearchField3 = Request.QueryString["s_Resistance"];
      string SearchField4 = Request.QueryString["s_Notifiable"];
      string SearchField5 = Request.QueryString["s_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Name=" + Request.QueryString["s_Name"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Type=" + Request.QueryString["s_Type"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Resistance=" + Request.QueryString["s_Resistance"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Notifiable=" + Request.QueryString["s_Notifiable"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_IsActive=" + Request.QueryString["s_IsActive"] + "&";
      }

      string FinalURL = "Form_IPS_Organism_Lookup.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("IPS Organism Lookup", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Search--//
    protected void Button_Search_OnClick(object sender, EventArgs e)
    {
      string SearchField1 = TextBox_Name.Text;
      string SearchField2 = DropDownList_Type.SelectedValue;
      string SearchField3 = DropDownList_Resistance.SelectedValue;
      string SearchField4 = DropDownList_Notifiable.SelectedValue;
      string SearchField5 = DropDownList_IsActive.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Name=" + TextBox_Name.Text.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Type=" + DropDownList_Type.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Resistance=" + DropDownList_Resistance.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Notifiable=" + DropDownList_Notifiable.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_IPS_Organism_Lookup.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("IPS Organism Lookup", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_OnClick(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("IPS Organism Lookup", "Form_IPS_Organism_Lookup.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_IPS_Organism_Lookup_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_IPS_Organism_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_IPS_Organism_Lookup_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_IPS_Organism_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_IPS_Organism_Lookup_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_IPS_Organism_Lookup_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_IPS_Organism_Lookup_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_IPS_Organism_Lookup_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_IPS_Organism_Lookup_List.PageSize > 20 && GridView_IPS_Organism_Lookup_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_IPS_Organism_Lookup_List.PageSize > 50 && GridView_IPS_Organism_Lookup_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_IPS_Organism_Lookup_List.Rows.Count; i++)
      {
        if (GridView_IPS_Organism_Lookup_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].Text.ToString() == "Yes")
          {
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].Text.ToString() == "No")
          {
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Organism_Lookup_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_Organism_Lookup_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_IPS_Organism_Lookup_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_IPS_Organism_Lookup_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_IPS_Organism_Lookup_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_IPS_Organism_Lookup_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_CaptureNew_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("IPS Organism Lookup", "Form_IPS_Organism_Lookup.aspx"), false);
    }

    public string GetLink(object ips_Organism_Lookup_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("IPS Organism Lookup", "Form_IPS_Organism_Lookup.aspx?IPS_Organism_Lookup_Id=" + ips_Organism_Lookup_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Name"];
      string SearchField2 = Request.QueryString["s_Type"];
      string SearchField3 = Request.QueryString["s_Resistance"];
      string SearchField4 = Request.QueryString["s_Notifiable"];
      string SearchField5 = Request.QueryString["s_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Name=" + Request.QueryString["s_Name"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Type=" + Request.QueryString["s_Type"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Resistance=" + Request.QueryString["s_Resistance"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Notifiable=" + Request.QueryString["s_Notifiable"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_IsActive=" + Request.QueryString["s_IsActive"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
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
    protected void FormView_IPS_Organism_Lookup_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_History"].DefaultValue = "";
          SqlDataSource_IPS_Organism_Lookup_Form.InsertParameters["IPS_Organism_Lookup_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertCode = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertCode");
      TextBox TextBox_InsertDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertDescription");
      DropDownList DropDownList_InsertTypeList = (DropDownList)FormView_IPS_Organism_Lookup_Form.FindControl("DropDownList_InsertTypeList");

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

        if (string.IsNullOrEmpty(DropDownList_InsertTypeList.SelectedValue))
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
        Session["IPSOrganismLookupCode"] = "";
        string SQLStringCode = "SELECT IPS_Organism_Lookup_Code FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
        using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
        {
          SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_InsertCode.Text.ToString());
          SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
          DataTable DataTable_Code;
          using (DataTable_Code = new DataTable())
          {
            DataTable_Code.Locale = CultureInfo.CurrentCulture;
            DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
            if (DataTable_Code.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Code.Rows)
              {
                Session["IPSOrganismLookupCode"] = DataRow_Row["IPS_Organism_Lookup_Code"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IPSOrganismLookupCode"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Organism with the Code '" + Session["IPSOrganismLookupCode"].ToString() + "' already exists<br />";
        }

        Session.Remove("IPSOrganismLookupCode");


        Session["IPSOrganismLookupDescription"] = "";
        string SQLStringDescription = "SELECT IPS_Organism_Lookup_Description FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Description = @IPS_Organism_Lookup_Description AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
        using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
        {
          SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_Description", TextBox_InsertDescription.Text.ToString());
          SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
          DataTable DataTable_Description;
          using (DataTable_Description = new DataTable())
          {
            DataTable_Description.Locale = CultureInfo.CurrentCulture;
            DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
            if (DataTable_Description.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Description.Rows)
              {
                Session["IPSOrganismLookupDescription"] = DataRow_Row["IPS_Organism_Lookup_Description"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IPSOrganismLookupDescription"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Organism with the Description '" + Session["IPSOrganismLookupDescription"].ToString() + "' already exists<br />";
        }

        Session.Remove("IPSOrganismLookupDescription");
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Organism_Lookup_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Id"] = e.Command.Parameters["@IPS_Organism_Lookup_Id"].Value;

        string FinalURL = "Form_IPS_Organism_Lookup.aspx";
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Organism Lookup", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_IPS_Organism_Lookup_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIPSOrganismLookupModifiedDate"] = e.OldValues["IPS_Organism_Lookup_ModifiedDate"];
        object OLDIPSOrganismLookupModifiedDate = Session["OLDIPSOrganismLookupModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSOrganismLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSOrganismLookup = (DataView)SqlDataSource_IPS_Organism_Lookup_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSOrganismLookup = DataView_CompareIPSOrganismLookup[0];
        Session["DBIPSOrganismLookupModifiedDate"] = Convert.ToString(DataRowView_CompareIPSOrganismLookup["IPS_Organism_Lookup_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSOrganismLookupModifiedBy"] = Convert.ToString(DataRowView_CompareIPSOrganismLookup["IPS_Organism_Lookup_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSOrganismLookupModifiedDate = Session["DBIPSOrganismLookupModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSOrganismLookupModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSOrganismLookupModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["IPS_Organism_Lookup_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_Organism_Lookup_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Organism_Lookup", "IPS_Organism_Lookup_Id = " + Request.QueryString["IPS_Organism_Lookup_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_IPS_Organism_Lookup_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["IPSOrganismLookupHistory"] = Convert.ToString(DataRowView_Form["IPS_Organism_Lookup_History"], CultureInfo.CurrentCulture);

            Session["IPSOrganismLookupHistory"] = Session["History"].ToString() + Session["IPSOrganismLookupHistory"].ToString();
            e.NewValues["IPS_Organism_Lookup_History"] = Session["IPSOrganismLookupHistory"].ToString();

            Session["IPSOrganismLookupHistory"] = "";
            Session["History"] = "";
          }
        }

        Session.Remove("OLDIPSOrganismLookupModifiedDate");
        Session.Remove("DBIPSOrganismLookupModifiedDate");
        Session.Remove("DBIPSOrganismLookupModifiedBy");
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditCode = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditCode");
      TextBox TextBox_EditDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditDescription");
      DropDownList DropDownList_EditTypeList = (DropDownList)FormView_IPS_Organism_Lookup_Form.FindControl("DropDownList_EditTypeList");

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

        if (string.IsNullOrEmpty(DropDownList_EditTypeList.SelectedValue))
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
        Session["IPSOrganismLookupId"] = "";
        Session["IPSOrganismLookupCode"] = "";
        string SQLStringCode = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Code FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
        using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
        {
          SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_EditCode.Text.ToString());
          SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
          DataTable DataTable_Code;
          using (DataTable_Code = new DataTable())
          {
            DataTable_Code.Locale = CultureInfo.CurrentCulture;
            DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
            if (DataTable_Code.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Code.Rows)
              {
                Session["IPSOrganismLookupId"] = DataRow_Row["IPS_Organism_Lookup_Id"];
                Session["IPSOrganismLookupCode"] = DataRow_Row["IPS_Organism_Lookup_Code"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IPSOrganismLookupCode"].ToString()))
        {
          if (Session["IPSOrganismLookupId"].ToString() != Request.QueryString["IPS_Organism_Lookup_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Organism with the Code '" + Session["IPSOrganismLookupCode"].ToString() + "' already exists<br />";
          }
        }

        Session.Remove("IPSOrganismLookupId");
        Session.Remove("IPSOrganismLookupCode");


        Session["IPSOrganismLookupId"] = "";
        Session["IPSOrganismLookupDescription"] = "";
        string SQLStringDescription = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Description FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Description = @IPS_Organism_Lookup_Description AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
        using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
        {
          SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_Description", TextBox_EditDescription.Text.ToString());
          SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
          DataTable DataTable_Description;
          using (DataTable_Description = new DataTable())
          {
            DataTable_Description.Locale = CultureInfo.CurrentCulture;
            DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
            if (DataTable_Description.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Description.Rows)
              {
                Session["IPSOrganismLookupId"] = DataRow_Row["IPS_Organism_Lookup_Id"];
                Session["IPSOrganismLookupDescription"] = DataRow_Row["IPS_Organism_Lookup_Description"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IPSOrganismLookupDescription"].ToString()))
        {
          if (Session["IPSOrganismLookupId"].ToString() != Request.QueryString["IPS_Organism_Lookup_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Organism with the Description '" + Session["IPSOrganismLookupDescription"].ToString() + "' already exists<br />";
          }
        }

        Session.Remove("IPSOrganismLookupId");
        Session.Remove("IPSOrganismLookupDescription");
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Organism_Lookup_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_IPS_Organism_Lookup_Form_ItemCommand(object sender, CommandEventArgs e)
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
      TextBox TextBox_InsertCode = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertCode");
      Label Label_InsertCodeError = (Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_InsertCodeError");
      TextBox TextBox_InsertDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertDescription");

      Session["IPSOrganismLookupCode"] = "";
      string SQLStringCode = "SELECT IPS_Organism_Lookup_Code FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
      using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
      {
        SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_InsertCode.Text.ToString());
        SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
        DataTable DataTable_Code;
        using (DataTable_Code = new DataTable())
        {
          DataTable_Code.Locale = CultureInfo.CurrentCulture;

          DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
          if (DataTable_Code.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Code.Rows)
            {
              Session["IPSOrganismLookupCode"] = DataRow_Row["IPS_Organism_Lookup_Code"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSOrganismLookupCode"].ToString()))
      {
        Label_InsertCodeError.Text = "";
        ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_InsertDescription);
      }
      else
      {
        Label_InsertCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Organism with the Code '" + Session["IPSOrganismLookupCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_InsertCode);
      }

      Session.Remove("IPSOrganismLookupCode");
    }

    protected void TextBox_InsertDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_InsertDescription");
      Label Label_InsertDescriptionError = (Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_InsertDescriptionError");

      Session["IPSOrganismLookupDescription"] = "";
      string SQLStringDescription = "SELECT IPS_Organism_Lookup_Description FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Description = @IPS_Organism_Lookup_Description AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
      using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
      {
        SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_Description", TextBox_InsertDescription.Text.ToString());
        SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
        DataTable DataTable_Description;
        using (DataTable_Description = new DataTable())
        {
          DataTable_Description.Locale = CultureInfo.CurrentCulture;

          DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
          if (DataTable_Description.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Description.Rows)
            {
              Session["IPSOrganismLookupDescription"] = DataRow_Row["IPS_Organism_Lookup_Description"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSOrganismLookupDescription"].ToString()))
      {
        Label_InsertDescriptionError.Text = "";
      }
      else
      {
        Label_InsertDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Organism with the Description '" + Session["IPSOrganismLookupDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_InsertDescription);
      }

      Session.Remove("IPSOrganismLookupDescription");
    }

    protected void TextBox_EditCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditCode = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditCode");
      Label Label_EditCodeError = (Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditCodeError");
      TextBox TextBox_EditDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditDescription");

      Session["IPSOrganismLookupId"] = "";
      Session["IPSOrganismLookupCode"] = "";
      string SQLStringCode = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Code FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
      using (SqlCommand SqlCommand_Code = new SqlCommand(SQLStringCode))
      {
        SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_EditCode.Text.ToString());
        SqlCommand_Code.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
        DataTable DataTable_Code;
        using (DataTable_Code = new DataTable())
        {
          DataTable_Code.Locale = CultureInfo.CurrentCulture;

          DataTable_Code = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Code).Copy();
          if (DataTable_Code.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Code.Rows)
            {
              Session["IPSOrganismLookupId"] = DataRow_Row["IPS_Organism_Lookup_Id"];
              Session["IPSOrganismLookupCode"] = DataRow_Row["IPS_Organism_Lookup_Code"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSOrganismLookupCode"].ToString()))
      {
        Label_EditCodeError.Text = "";
      }
      else
      {
        if (Session["IPSOrganismLookupId"].ToString() == Request.QueryString["IPS_Organism_Lookup_Id"])
        {
          Label_EditCodeError.Text = "";
          ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_EditDescription);
        }
        else
        {
          Label_EditCodeError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Organism with the Code '" + Session["IPSOrganismLookupCode"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
          ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_EditCode);
        }
      }

      Session.Remove("IPSOrganismLookupId");
      Session.Remove("IPSOrganismLookupCode");
    }

    protected void TextBox_EditDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditDescription = (TextBox)FormView_IPS_Organism_Lookup_Form.FindControl("TextBox_EditDescription");
      Label Label_EditDescriptionError = (Label)FormView_IPS_Organism_Lookup_Form.FindControl("Label_EditDescriptionError");

      Session["IPSOrganismLookupId"] = "";
      Session["IPSOrganismLookupDescription"] = "";
      string SQLStringDescription = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Description FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Description = @IPS_Organism_Lookup_Description AND IPS_Organism_Lookup_IsActive = @IPS_Organism_Lookup_IsActive";
      using (SqlCommand SqlCommand_Description = new SqlCommand(SQLStringDescription))
      {
        SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_Description", TextBox_EditDescription.Text.ToString());
        SqlCommand_Description.Parameters.AddWithValue("@IPS_Organism_Lookup_IsActive", 1);
        DataTable DataTable_Description;
        using (DataTable_Description = new DataTable())
        {
          DataTable_Description.Locale = CultureInfo.CurrentCulture;

          DataTable_Description = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Description).Copy();
          if (DataTable_Description.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Description.Rows)
            {
              Session["IPSOrganismLookupId"] = DataRow_Row["IPS_Organism_Lookup_Id"];
              Session["IPSOrganismLookupDescription"] = DataRow_Row["IPS_Organism_Lookup_Description"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSOrganismLookupDescription"].ToString()))
      {
        Label_EditDescriptionError.Text = "";
      }
      else
      {
        if (Session["IPSOrganismLookupId"].ToString() == Request.QueryString["IPS_Organism_Lookup_Id"])
        {
          Label_EditDescriptionError.Text = "";
        }
        else
        {
          Label_EditDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Organism with the Description '" + Session["IPSOrganismLookupDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
          ToolkitScriptManager_IPS_Organism_Lookup.SetFocus(TextBox_EditDescription);
        }
      }

      Session.Remove("IPSOrganismLookupId");
      Session.Remove("IPSOrganismLookupDescription");
    }


    protected void Button_EditUpdate_OnClick(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}