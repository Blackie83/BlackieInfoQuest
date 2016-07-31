using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_BusinessUnit_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Captured Business Units", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search Captured Business Units", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of Captured Business Units", CultureInfo.CurrentCulture);
          Label_GridMappingHeading.Text = Convert.ToString("List of Captured Business Unit Mappings", CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('41'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("41");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_FSM_BusinessUnit_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_BusinessUnit_BusinessUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_BusinessUnit.SelectCommand = "SELECT BusinessUnitKey , BusinessUnitName FROM BusinessUnit.BusinessUnit ORDER BY BusinessUnitName";

      SqlDataSource_FSM_BusinessUnit_BusinessUnitType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_BusinessUnitType.SelectCommand = "SELECT BusinessUnitTypeKey , BusinessUnitTypeName FROM BusinessUnit.BusinessUnitType ORDER BY BusinessUnitTypeName";

      SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.SelectCommand = "SELECT BusinessUnitReportingGroupKey , BusinessUnitReportingGroupName FROM BusinessUnit.BusinessUnitReportingGroup WHERE BusinessUnitTypeKey = @BusinessUnitTypeKey ORDER BY BusinessUnitReportingGroupName";
      SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.SelectParameters.Add("BusinessUnitTypeKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnit_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_List.SelectCommand = @"SELECT		BusinessUnit.BusinessUnitKey ,
						                                                          BusinessUnit.BusinessUnitName , 
						                                                          BusinessUnitType.BusinessUnitTypeName ,
						                                                          BusinessUnitReportingGroup.BusinessUnitReportingGroupName ,
						                                                          BusinessUnit.BusinessUnitDefaultEntity , 
						                                                          CASE 
							                                                          WHEN BusinessUnit.IsActive = 1 THEN 'Yes'
							                                                          WHEN BusinessUnit.IsActive = 0 THEN 'No'
						                                                          END AS IsActive
	                                                          FROM			BusinessUnit.BusinessUnit
						                                                          LEFT JOIN BusinessUnit.BusinessUnitType ON BusinessUnit.BusinessUnitTypeKey = BusinessUnitType.BusinessUnitTypeKey
						                                                          LEFT JOIN BusinessUnit.BusinessUnitReportingGroup ON BusinessUnit.BusinessUnitReportingGroupKey = BusinessUnitReportingGroup.BusinessUnitReportingGroupKey
	                                                          WHERE			(BusinessUnit.BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
						                                                          AND (BusinessUnitType.BusinessUnitTypeKey = @BusinessUnitType OR @BusinessUnitType IS NULL)
						                                                          AND (BusinessUnitReportingGroup.BusinessUnitReportingGroupKey = @BusinessUnitReportingGroup OR @BusinessUnitReportingGroup IS NULL)
						                                                          AND (CHARINDEX(@BusinessUnitDefaultEntity , BusinessUnit.BusinessUnitDefaultEntity) > 0 OR @BusinessUnitDefaultEntity IS NULL)
	                                                          ORDER BY	BusinessUnit.BusinessUnitName";
      SqlDataSource_FSM_BusinessUnit_List.CancelSelectOnNullParameter = false;
      SqlDataSource_FSM_BusinessUnit_List.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_List.SelectParameters.Add("BusinessUnit", TypeCode.String, Request.QueryString["s_BusinessUnit"]);
      SqlDataSource_FSM_BusinessUnit_List.SelectParameters.Add("BusinessUnitType", TypeCode.String, Request.QueryString["s_BusinessUnitType"]);
      SqlDataSource_FSM_BusinessUnit_List.SelectParameters.Add("BusinessUnitDefaultEntity", TypeCode.String, Request.QueryString["s_BusinessUnitDefaultEntity"]);
      SqlDataSource_FSM_BusinessUnit_List.SelectParameters.Add("BusinessUnitReportingGroup", TypeCode.String, Request.QueryString["s_BusinessUnitReportingGroup"]);

      SqlDataSource_FSM_BusinessUnit_Mapping_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectCommand = @"SELECT		BusinessUnit.BusinessUnitKey , 
                                                                              BusinessUnit.BusinessUnitName , 
                                                                              SourceSystem.SourceSystemName , 
                                                                              MappingBusinessUnit.SourceSystemValue 
                                                                    FROM			BusinessUnit.BusinessUnit 
                                                                              LEFT JOIN Mapping.BusinessUnit AS MappingBusinessUnit ON BusinessUnit.BusinessUnitKey = MappingBusinessUnit.BusinessUnitKey 
                                                                              LEFT JOIN Mapping.SourceSystem ON MappingBusinessUnit.SourceSystemKey = SourceSystem.SourceSystemKey 
                                                                    WHERE			MappingBusinessUnit.BusinessUnitKey IS NOT NULL 
					                                                                    AND MappingBusinessUnit.BusinessUnitKey IN (
						                                                                    SELECT		BusinessUnit.BusinessUnitKey
						                                                                    FROM			BusinessUnit.BusinessUnit
											                                                                    LEFT JOIN BusinessUnit.BusinessUnitType ON BusinessUnit.BusinessUnitTypeKey = BusinessUnitType.BusinessUnitTypeKey
											                                                                    LEFT JOIN BusinessUnit.BusinessUnitReportingGroup ON BusinessUnit.BusinessUnitReportingGroupKey = BusinessUnitReportingGroup.BusinessUnitReportingGroupKey
						                                                                    WHERE			(BusinessUnit.BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
											                                                                    AND (BusinessUnitType.BusinessUnitTypeKey = @BusinessUnitType OR @BusinessUnitType IS NULL)
											                                                                    AND (BusinessUnitReportingGroup.BusinessUnitReportingGroupKey = @BusinessUnitReportingGroup OR @BusinessUnitReportingGroup IS NULL)
											                                                                    AND (CHARINDEX(@BusinessUnitDefaultEntity , BusinessUnit.BusinessUnitDefaultEntity) > 0 OR @BusinessUnitDefaultEntity IS NULL)
                                                                              ) 
                                                                    ORDER BY	BusinessUnit.BusinessUnitName";
      SqlDataSource_FSM_BusinessUnit_Mapping_List.CancelSelectOnNullParameter = false;
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectParameters.Add("BusinessUnit", TypeCode.String, Request.QueryString["s_BusinessUnit"]);
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectParameters.Add("BusinessUnitType", TypeCode.String, Request.QueryString["s_BusinessUnitType"]);
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectParameters.Add("BusinessUnitDefaultEntity", TypeCode.String, Request.QueryString["s_BusinessUnitDefaultEntity"]);
      SqlDataSource_FSM_BusinessUnit_Mapping_List.SelectParameters.Add("BusinessUnitReportingGroup", TypeCode.String, Request.QueryString["s_BusinessUnitReportingGroup"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_BusinessUnit.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_BusinessUnit"] == null)
        {
          DropDownList_BusinessUnit.SelectedValue = "";
        }
        else
        {
          DropDownList_BusinessUnit.SelectedValue = Request.QueryString["s_BusinessUnit"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_BusinessUnitType.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitType"] == null)
        {
          DropDownList_BusinessUnitType.SelectedValue = "";
        }
        else
        {
          DropDownList_BusinessUnitType.SelectedValue = Request.QueryString["s_BusinessUnitType"];

          DropDownList_BusinessUnitReportingGroup.Items.Clear();
          SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.SelectParameters["BusinessUnitTypeKey"].DefaultValue = Request.QueryString["s_BusinessUnitType"];
          DropDownList_BusinessUnitReportingGroup.Items.Insert(0, new ListItem(Convert.ToString("Select Reporting Group", CultureInfo.CurrentCulture), ""));
          DropDownList_BusinessUnitReportingGroup.DataBind();
        }
      }

      if (string.IsNullOrEmpty(TextBox_BusinessUnitDefaultEntity.Text.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitDefaultEntity"] == null)
        {
          TextBox_BusinessUnitDefaultEntity.Text = "";
        }
        else
        {
          TextBox_BusinessUnitDefaultEntity.Text = Request.QueryString["s_BusinessUnitDefaultEntity"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_BusinessUnitReportingGroup.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitReportingGroup"] == null)
        {
          DropDownList_BusinessUnitReportingGroup.SelectedValue = "";
        }
        else
        {
          DropDownList_BusinessUnitReportingGroup.SelectedValue = Request.QueryString["s_BusinessUnitReportingGroup"];
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_BusinessUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_BusinessUnitReportingGroup.Items.Clear();
      SqlDataSource_FSM_BusinessUnit_BusinessUnitReportingGroup.SelectParameters["BusinessUnitTypeKey"].DefaultValue = DropDownList_BusinessUnitType.SelectedValue;
      DropDownList_BusinessUnitReportingGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Reporting Group", CultureInfo.CurrentCulture), ""));
      DropDownList_BusinessUnitReportingGroup.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_BusinessUnit.SelectedValue;
      string SearchField2 = DropDownList_BusinessUnitType.SelectedValue;
      string SearchField3 = Server.HtmlEncode(TextBox_BusinessUnitDefaultEntity.Text);
      string SearchField4 = DropDownList_BusinessUnitReportingGroup.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_BusinessUnit=" + DropDownList_BusinessUnit.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_BusinessUnitType=" + DropDownList_BusinessUnitType.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_BusinessUnitDefaultEntity=" + Server.HtmlEncode(TextBox_BusinessUnitDefaultEntity.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_BusinessUnitReportingGroup=" + DropDownList_BusinessUnitReportingGroup.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_FSM_BusinessUnit_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Units", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Units", "Form_FSM_BusinessUnit_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_FSM_BusinessUnit_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnit_List.PageSize = Convert.ToInt32(((DropDownList)GridView_FSM_BusinessUnit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnit_List.PageIndex = ((DropDownList)GridView_FSM_BusinessUnit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_FSM_BusinessUnit_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_FSM_BusinessUnit_List.PageSize <= 20)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_FSM_BusinessUnit_List.PageSize > 20 && GridView_FSM_BusinessUnit_List.PageSize <= 50)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_FSM_BusinessUnit_List.PageSize > 50 && GridView_FSM_BusinessUnit_List.PageSize <= 100)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_FSM_BusinessUnit_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FSM_BusinessUnit_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FSM_BusinessUnit_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FSM_BusinessUnit_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FSM_BusinessUnit_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit", "Form_FSM_BusinessUnit.aspx"), false);
    }

    public string GetLink(object businessUnitKey)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit", "Form_FSM_BusinessUnit.aspx?BusinessUnitKey=" + businessUnitKey + "") + "'>View</a>";

      string SearchField1 = Request.QueryString["s_BusinessUnit"];
      string SearchField2 = Request.QueryString["s_BusinessUnitType"];
      string SearchField3 = Request.QueryString["s_BusinessUnitDefaultEntity"];
      string SearchField4 = Request.QueryString["s_BusinessUnitReportingGroup"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_BusinessUnit=" + Request.QueryString["s_BusinessUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_BusinessUnitType=" + Request.QueryString["s_BusinessUnitType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_BusinessUnitDefaultEntity=" + Request.QueryString["s_BusinessUnitDefaultEntity"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_BusinessUnitReportingGroup=" + Request.QueryString["s_BusinessUnitReportingGroup"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
      }
      else
      {
        FinalURL = LinkURL;
      }

      return FinalURL;
    }
    //---END--- --List--//


    //--START-- --List_Mapping//
    protected void SqlDataSource_FSM_BusinessUnit_Mapping_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Mapping.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Mapping_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnit_Mapping_List.PageSize = Convert.ToInt32(((DropDownList)GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Mapping_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnit_Mapping_List.PageIndex = ((DropDownList)GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Mapping")).SelectedIndex;
    }

    protected void GridView_FSM_BusinessUnit_Mapping_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_FSM_BusinessUnit_Mapping_List.PageSize <= 20)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "20";
        }
        else if (GridView_FSM_BusinessUnit_Mapping_List.PageSize > 20 && GridView_FSM_BusinessUnit_Mapping_List.PageSize <= 50)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "50";
        }
        else if (GridView_FSM_BusinessUnit_Mapping_List.PageSize > 50 && GridView_FSM_BusinessUnit_Mapping_List.PageSize <= 100)
        {
          ((DropDownList)GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_FSM_BusinessUnit_Mapping_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FSM_BusinessUnit_Mapping_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FSM_BusinessUnit_Mapping_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FSM_BusinessUnit_Mapping_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Mapping")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FSM_BusinessUnit_Mapping_List_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List_Mapping--//
  }
}