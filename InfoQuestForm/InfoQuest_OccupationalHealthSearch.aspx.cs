using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_OccupationalHealthSearch : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthSearch, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          TableOccupationalHealthSearch.Visible = true;

          if (string.IsNullOrEmpty(Request.QueryString["s_Name"]))
          {
            TableInfoQuestSearchResults.Visible = false;
            TableMedocSearchResults.Visible = false;
          }
          else
          {
            TableInfoQuestSearchResults.Visible = true;
            TableMedocSearchResults.Visible = true;
          }

          TextBox_SearchName.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_SearchName.Attributes.Add("OnInput", "Validation_Search();");

          SetFormQueryString();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_UserName = @SecurityUser_UserName";
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
          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
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
      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_OccupationalHealthSearch.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_InfoQuestSearch.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfoQuestSearch.SelectCommand = "spInfoQuest_Get_OccupationalHealthSearch_List";
      SqlDataSource_InfoQuestSearch.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_InfoQuestSearch.CancelSelectOnNullParameter = false;
      SqlDataSource_InfoQuestSearch.SelectParameters.Clear();
      SqlDataSource_InfoQuestSearch.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_Name"]);

      ObjectDataSource_MedocSearch.SelectMethod = "DataPatient_Medoc_CompanySearch";
      ObjectDataSource_MedocSearch.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_MedocSearch.SelectParameters.Clear();
      ObjectDataSource_MedocSearch.SelectParameters.Add("company", TypeCode.String, Request.QueryString["s_Name"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_SearchName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Name"]))
        {
          TextBox_SearchName.Text = "";
        }
        else
        {
          TextBox_SearchName.Text = Request.QueryString["s_Name"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_OnClick(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest Occupational Health Search", "InfoQuest_OccupationalHealthSearch.aspx?s_Name=" + Server.HtmlEncode(TextBox_SearchName.Text.ToString()) + ""), false);
      }
      else
      {
        Label_InvalidSearchMessage.Text = Label_InvalidSearchMessageText;
      }
    }

    protected string SearchValidation()
    {
      string InvalidSearch = "No";
      string InvalidSearchMessage = "";

      if (InvalidSearch == "No")
      {
        if (string.IsNullOrEmpty(TextBox_SearchName.Text))
        {
          InvalidSearch = "Yes";
        }
      }

      if (InvalidSearch == "Yes")
      {
        InvalidSearchMessage = "All red fields are required";
      }

      if (InvalidSearch == "No" && string.IsNullOrEmpty(InvalidSearchMessage))
      {

      }

      return InvalidSearchMessage;
    }

    protected void Button_Clear_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest Occupational Health Search", "InfoQuest_OccupationalHealthSearch.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --InfoQuest List--//
    protected void SqlDataSource_InfoQuestSearch_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_InfoQuest.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_InfoQuest(object sender, EventArgs e)
    {
      GridView_InfoQuestSearch.PageSize = Convert.ToInt32(((DropDownList)GridView_InfoQuestSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged_InfoQuest(object sender, EventArgs e)
    {
      GridView_InfoQuestSearch.PageIndex = ((DropDownList)GridView_InfoQuestSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_InfoQuestSearch_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_InfoQuestSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_InfoQuestSearch.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_InfoQuestSearch.PageSize > 20 && GridView_InfoQuestSearch.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_InfoQuestSearch.PageSize > 50 && GridView_InfoQuestSearch.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_InfoQuestSearch_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_InfoQuestSearch.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_InfoQuestSearch.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_InfoQuestSearch.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_InfoQuestSearch_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --InfoQuest List--//


    //--START-- --Medoc List--//
    protected void ObjectDataSource_MedocSearch_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Medoc.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_Medoc(object sender, EventArgs e)
    {
      GridView_MedocSearch.PageSize = Convert.ToInt32(((DropDownList)GridView_MedocSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged_Medoc(object sender, EventArgs e)
    {
      GridView_MedocSearch.PageIndex = ((DropDownList)GridView_MedocSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_MedocSearch_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_MedocSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_MedocSearch.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_MedocSearch.PageSize > 20 && GridView_MedocSearch.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_MedocSearch.PageSize > 50 && GridView_MedocSearch.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_MedocSearch_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_MedocSearch.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_MedocSearch.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_MedocSearch.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_MedocSearch_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --Medoc List--//
  }
}