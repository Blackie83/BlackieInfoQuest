using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_IPS_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("37").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("37").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("37").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }
    
    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_IPS_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Facility.SelectParameters.Clear();
      SqlDataSource_IPS_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_IPS_VisitInformation");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_Category.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Category.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_Category.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Category.SelectParameters.Clear();
      SqlDataSource_IPS_Category.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_Category.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "119");
      SqlDataSource_IPS_Category.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_Category.SelectParameters.Add("TableSELECT", TypeCode.String, "IPS_Infection_Category_List");
      SqlDataSource_IPS_Category.SelectParameters.Add("TableFROM", TypeCode.String, "Form_IPS_Infection");
      SqlDataSource_IPS_Category.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_Type.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Type.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_Type.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Type.SelectParameters.Clear();
      SqlDataSource_IPS_Type.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_Type.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "120");
      SqlDataSource_IPS_Type.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_Type.SelectParameters.Add("TableSELECT", TypeCode.String, "IPS_Infection_Type_List");
      SqlDataSource_IPS_Type.SelectParameters.Add("TableFROM", TypeCode.String, "Form_IPS_Infection");
      SqlDataSource_IPS_Type.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_List.SelectCommand = "spForm_Get_IPS_List";
      SqlDataSource_IPS_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_List.SelectParameters.Clear();
      SqlDataSource_IPS_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_IPS_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_IPS_ReportNumber"]);
      SqlDataSource_IPS_List.SelectParameters.Add("CategoryList", TypeCode.String, Request.QueryString["s_IPS_CategoryList"]);
      SqlDataSource_IPS_List.SelectParameters.Add("TypeList", TypeCode.String, Request.QueryString["s_IPS_TypeList"]);
      SqlDataSource_IPS_List.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_IPS_PatientVisitNumber"]);
      SqlDataSource_IPS_List.SelectParameters.Add("PatientName", TypeCode.String, Request.QueryString["s_IPS_PatientName"]);
      SqlDataSource_IPS_List.SelectParameters.Add("InfectionCompleted", TypeCode.String, Request.QueryString["s_IPS_InfectionCompleted"]);
      SqlDataSource_IPS_List.SelectParameters.Add("HAI", TypeCode.String, Request.QueryString["s_IPS_HAI"]);
      SqlDataSource_IPS_List.SelectParameters.Add("IsActive", TypeCode.String, Request.QueryString["s_IPS_IsActive"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]))
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_ReportNumber"]))
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_IPS_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_PatientVisitNumber"]))
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_IPS_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_PatientName"]))
        {
          TextBox_PatientName.Text = "";
        }
        else
        {
          TextBox_PatientName.Text = Request.QueryString["s_IPS_PatientName"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Category.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_CategoryList"]))
        {
          DropDownList_Category.SelectedValue = "";
        }
        else
        {
          DropDownList_Category.SelectedValue = Request.QueryString["s_IPS_CategoryList"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Type.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_TypeList"]))
        {
          DropDownList_Type.SelectedValue = "";
        }
        else
        {
          DropDownList_Type.SelectedValue = Request.QueryString["s_IPS_TypeList"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_InfectionCompleted.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_InfectionCompleted"]))
        {
          DropDownList_InfectionCompleted.SelectedValue = "";
        }
        else
        {
          DropDownList_InfectionCompleted.SelectedValue = Request.QueryString["s_IPS_InfectionCompleted"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_HAI.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_HAI"]))
        {
          DropDownList_HAI.SelectedValue = "";
        }
        else
        {
          DropDownList_HAI.SelectedValue = Request.QueryString["s_IPS_HAI"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_IPS_IsActive"]))
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_IPS_IsActive"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_OnClick(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = Server.HtmlEncode(TextBox_ReportNumber.Text);
      string SearchField3 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_PatientName.Text);
      string SearchField5 = DropDownList_Category.SelectedValue;
      string SearchField6 = DropDownList_Type.SelectedValue;
      string SearchField7 = DropDownList_InfectionCompleted.SelectedValue;
      string SearchField8 = DropDownList_HAI.SelectedValue;
      string SearchField9 = DropDownList_IsActive.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_IPS_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_IPS_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_IPS_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_IPS_CategoryList=" + DropDownList_Category.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_IPS_TypeList=" + DropDownList_Type.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_IPS_InfectionCompleted=" + DropDownList_InfectionCompleted.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField8))
      {
        SearchField8 = "s_IPS_HAI=" + DropDownList_HAI.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField9))
      {
        SearchField9 = "s_IPS_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_IPS_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8 + SearchField9;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_OnClick(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Captured Forms", "Form_IPS_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_IPS_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_IPS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_IPS_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_IPS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_IPS_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_IPS_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_IPS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_IPS_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_IPS_List.PageSize > 20 && GridView_IPS_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_IPS_List.PageSize > 50 && GridView_IPS_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_IPS_List.Rows.Count; i++)
      {
        if (GridView_IPS_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_List.Rows[i].Cells[10].Text == "Yes")
          {
            if (GridView_IPS_List.Rows[i].Cells[8].Text == "Incomplete")
            {
              GridView_IPS_List.Rows[i].Cells[8].BackColor = Color.FromName("#d46e6e");
              GridView_IPS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_IPS_List.Rows[i].Cells[8].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
            }
          }
          else
          {
            GridView_IPS_List.Rows[i].Cells[8].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }


          if (GridView_IPS_List.Rows[i].Cells[10].Text == "Yes")
          {
            if (GridView_IPS_List.Rows[i].Cells[9].Text == "Not Required")
            {
              GridView_IPS_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
            }
            else if (GridView_IPS_List.Rows[i].Cells[9].Text == "Complete")
            {
              GridView_IPS_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
            }
            else if (GridView_IPS_List.Rows[i].Cells[9].Text == "Incomplete")
            {
              GridView_IPS_List.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
              GridView_IPS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
            }
          }
          else
          {
            GridView_IPS_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }


          if (GridView_IPS_List.Rows[i].Cells[10].Text == "No")
          {
            GridView_IPS_List.Rows[i].Cells[10].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_IPS_List.Rows[i].Cells[10].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_IPS_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_IPS_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_IPS_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_IPS_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance New Form", "Form_IPS.aspx"), false);
    }

    public static string GetLink(object ips_VisitInformation_Id, object ips_Infection_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance New Form", "Form_IPS.aspx?IPSVisitInformationId=" + ips_VisitInformation_Id + "&IPSInfectionId=" + ips_Infection_Id + "") + "#CurrentInfection'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance New Form", "Form_IPS.aspx?IPSVisitInformationId=" + ips_VisitInformation_Id + "&IPSInfectionId=" + ips_Infection_Id + "") + "#CurrentInfection'>View</a>";
        }
      }

      string FinalURL = LinkURL;

      return FinalURL;
    }
    //---END--- --List--//
  }
}