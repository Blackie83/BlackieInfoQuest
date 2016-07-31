using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

namespace InfoQuestForm
{
  public partial class Form_FSM_BusinessUnitComponent_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_FSM_BusinessUnitComponent_List, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_BusinessUnit.Attributes.Add("OnChange", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Captured Business Unit Components", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search Captured Business Unit Components", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of Captured Business Unit Components", CultureInfo.CurrentCulture);
          Label_GridMappingHeading.Text = Convert.ToString("List of Captured Business Unit Component Mappings", CultureInfo.CurrentCulture);

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
        ((Label)PageUpdateProgress_FSM_BusinessUnitComponent_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_BusinessUnit_BusinessUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_BusinessUnit.SelectCommand = "SELECT BusinessUnitKey , BusinessUnitName FROM BusinessUnit.BusinessUnit ORDER BY BusinessUnitName";

      SqlDataSource_FSM_BusinessUnit_BusinessUnitComponentType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_BusinessUnitComponentType.SelectCommand = "SELECT BusinessUnitComponentTypeKey , BusinessUnitComponentTypeName FROM BusinessUnit.BusinessUnitComponentType ORDER BY BusinessUnitComponentTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectCommand = @" ;WITH CTE_BusinessUnitComponentParent ( BusinessUnitComponentKey , Description ) AS
                                                                      (
                                                                      SELECT	BusinessUnitComponentKey, 
                                                                              CAST(BusinessUnitComponentTypeName + ': ' + BusinessUnitComponentName AS NVARCHAR(MAX))
                                                                      FROM		BusinessUnit.BusinessUnitComponent
				                                                                      INNER JOIN BusinessUnit.BusinessUnitComponentType ON BusinessUnitComponent.BusinessUnitComponentTypeKey = BusinessUnitComponentType.BusinessUnitComponentTypeKey
                                                                      WHERE		ParentBusinessUNitComponentKey IS NULL
				                                                                      AND (BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
                                                                      UNION ALL
                                                                      SELECT	BusinessUnitComponent.BusinessUnitComponentKey,
                                                                              CAST(CTE_BusinessUnitComponentParent.Description + ' - ' + BusinessUnitComponentTypeName + ': ' + BusinessUnitComponent.BusinessUnitComponentName AS NVARCHAR(MAX))
                                                                      FROM		BusinessUnit.BusinessUnitComponent
				                                                                      INNER JOIN CTE_BusinessUnitComponentParent ON BusinessUnitComponent.ParentBusinessUnitComponentKey = CTE_BusinessUnitComponentParent.BusinessUnitComponentKey
				                                                                      INNER JOIN BusinessUnit.BusinessUnitComponentType ON BusinessUnitComponent.BusinessUnitComponentTypeKey = BusinessUnitComponentType.BusinessUnitComponentTypeKey
                                                                      WHERE		(BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
                                                                      )					

                                                                      SELECT		BusinessUnitComponent.BusinessUnitComponentKey ,
					                                                                      BusinessUnit.BusinessUnitName ,
                                                                                BusinessUnitComponent.BusinessUnitComponentName , 
                                                                                BusinessUnitComponentType.BusinessUnitComponentTypeName ,
                                                                                CTE_BusinessUnitComponentParent.Description ,
                                                                                BusinessUnitComponentFinanceMapping.Entity , 
                                                                                BusinessUnitComponentFinanceMapping.CostCentre ,          
                                                                                CASE 
                                                                                  WHEN BusinessUnitComponent.IsActive = 1 THEN 'Yes'
                                                                                  WHEN BusinessUnitComponent.IsActive = 0 THEN 'No'
                                                                                END AS IsActive
                                                                      FROM			BusinessUnit.BusinessUnitComponent
                                                                                LEFT JOIN BusinessUnit.BusinessUnit ON BusinessUnitComponent.BusinessUnitKey = BusinessUnit.BusinessUnitKey
                                                                                LEFT JOIN BusinessUnit.BusinessUnitComponentType ON BusinessUnitComponent.BusinessUnitComponentTypeKey = BusinessUnitComponentType.BusinessUnitComponentTypeKey
                                                                                LEFT JOIN FinanceMapping.BusinessUnitComponentFinanceMapping ON BusinessUnitComponent.BusinessUnitComponentKey = BusinessUnitComponentFinanceMapping.BusinessUnitComponentKey
					                                                                      LEFT JOIN CTE_BusinessUnitComponentParent ON BusinessUnitComponent.BusinessUnitComponentKey = CTE_BusinessUnitComponentParent.BusinessUnitComponentKey
                                                                      WHERE			(BusinessUnit.BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
                                                                                AND (CHARINDEX(@BusinessUnitComponent , BusinessUnitComponent.BusinessUnitComponentName) > 0 OR @BusinessUnitComponent IS NULL)
                                                                                AND (BusinessUnitComponentType.BusinessUnitComponentTypeKey = @BusinessUnitComponentType OR @BusinessUnitComponentType IS NULL)
                                                                                AND (CHARINDEX(@BusinessUnitComponentDescription , CTE_BusinessUnitComponentParent.Description) > 0 OR @BusinessUnitComponentDescription IS NULL)
                                                                                AND (BusinessUnitComponentFinanceMapping.Entity = @Entity OR @Entity IS NULL)
                                                                                AND (BusinessUnitComponentFinanceMapping.CostCentre = @CostCentre OR @CostCentre IS NULL)
                                                                      ORDER BY	CTE_BusinessUnitComponentParent.Description";
      SqlDataSource_FSM_BusinessUnitComponent_List.CancelSelectOnNullParameter = false;
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("BusinessUnit", TypeCode.String, Request.QueryString["s_BusinessUnit"]);
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("BusinessUnitComponent", TypeCode.String, Request.QueryString["s_BusinessUnitComponent"]);      
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("BusinessUnitComponentType", TypeCode.String, Request.QueryString["s_BusinessUnitComponentType"]);
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("BusinessUnitComponentDescription", TypeCode.String, Request.QueryString["s_BusinessUnitComponentDescription"]);
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("Entity", TypeCode.String, Request.QueryString["s_Entity"]);
      SqlDataSource_FSM_BusinessUnitComponent_List.SelectParameters.Add("CostCentre", TypeCode.String, Request.QueryString["s_CostCentre"]);

      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectCommand = @"SELECT		BusinessUnit.BusinessUnitName ,
                                                                                      BusinessUnitComponent.BusinessUnitComponentName , 
                                                                                      SourceSystem.SourceSystemName ,
                                                                                      MappingType.MappingTypeName ,
                                                                                      MappingBusinessUnitComponent.SourceSystemValue
                                                                            FROM			BusinessUnit.BusinessUnit
                                                                                      LEFT JOIN BusinessUnit.BusinessUnitComponent ON BusinessUnit.BusinessUnitKey = BusinessUnitComponent.BusinessUnitKey
                                                                                      LEFT JOIN	Mapping.BusinessUnitComponent AS MappingBusinessUnitComponent ON BusinessUnitComponent.BusinessUnitComponentKey = MappingBusinessUnitComponent.BusinessUnitComponentKey
                                                                                      LEFT JOIN Mapping.SourceSystem ON MappingBusinessUnitComponent.SourceSystemKey = SourceSystem.SourceSystemKey
                                                                                      LEFT JOIN Mapping.MappingType ON SourceSystem.MappingTypeKey = MappingType.MappingTypeKey
                                                                            WHERE			MappingBusinessUnitComponent.BusinessUnitComponentKey IN (
	                                                                            SELECT		BusinessUnitComponent.BusinessUnitComponentKey
	                                                                            FROM			BusinessUnit.BusinessUnitComponent
						                                                                            LEFT JOIN BusinessUnit.BusinessUnit ON BusinessUnitComponent.BusinessUnitKey = BusinessUnit.BusinessUnitKey
						                                                                            LEFT JOIN BusinessUnit.BusinessUnitComponentType ON BusinessUnitComponent.BusinessUnitComponentTypeKey = BusinessUnitComponentType.BusinessUnitComponentTypeKey
						                                                                            LEFT JOIN FinanceMapping.BusinessUnitComponentFinanceMapping ON BusinessUnitComponent.BusinessUnitComponentKey = BusinessUnitComponentFinanceMapping.BusinessUnitComponentKey
	                                                                            WHERE			(BusinessUnit.BusinessUnitKey = @BusinessUnit OR @BusinessUnit IS NULL)
						                                                                            AND (CHARINDEX(@BusinessUnitComponent , BusinessUnitComponent.BusinessUnitComponentName) > 0 OR @BusinessUnitComponent IS NULL)
						                                                                            AND (BusinessUnitComponentType.BusinessUnitComponentTypeKey = @BusinessUnitComponentType OR @BusinessUnitComponentType IS NULL)
						                                                                            AND (BusinessUnitComponentFinanceMapping.Entity = @Entity OR @Entity IS NULL)
						                                                                            AND (BusinessUnitComponentFinanceMapping.CostCentre = @CostCentre OR @CostCentre IS NULL)
                                                                                      )
                                                                            ORDER BY	BusinessUnit.BusinessUnitName ,
                                                                                      BusinessUnitComponent.BusinessUnitComponentName ,
                                                                                      SourceSystem.SourceSystemName";
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.CancelSelectOnNullParameter = false;
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Add("BusinessUnit", TypeCode.String, Request.QueryString["s_BusinessUnit"]);
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Add("BusinessUnitComponent", TypeCode.String, Request.QueryString["s_BusinessUnitComponent"]);
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Add("BusinessUnitComponentType", TypeCode.String, Request.QueryString["s_BusinessUnitComponentType"]);
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Add("Entity", TypeCode.String, Request.QueryString["s_Entity"]);
      SqlDataSource_FSM_BusinessUnitComponent_Mapping_List.SelectParameters.Add("CostCentre", TypeCode.String, Request.QueryString["s_CostCentre"]);
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

      if (string.IsNullOrEmpty(TextBox_BusinessUnitComponent.Text.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitComponent"] == null)
        {
          TextBox_BusinessUnitComponent.Text = "";
        }
        else
        {
          TextBox_BusinessUnitComponent.Text = Request.QueryString["s_BusinessUnitComponent"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_BusinessUnitComponentType.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitComponentType"] == null)
        {
          DropDownList_BusinessUnitComponentType.SelectedValue = "";
        }
        else
        {
          DropDownList_BusinessUnitComponentType.SelectedValue = Request.QueryString["s_BusinessUnitComponentType"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_BusinessUnitComponentDescription.Text.ToString()))
      {
        if (Request.QueryString["s_BusinessUnitComponentDescription"] == null)
        {
          TextBox_BusinessUnitComponentDescription.Text = "";
        }
        else
        {
          TextBox_BusinessUnitComponentDescription.Text = Request.QueryString["s_BusinessUnitComponentDescription"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Entity.Text.ToString()))
      {
        if (Request.QueryString["s_Entity"] == null)
        {
          TextBox_Entity.Text = "";
        }
        else
        {
          TextBox_Entity.Text = Request.QueryString["s_Entity"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_CostCentre.Text.ToString()))
      {
        if (Request.QueryString["s_CostCentre"] == null)
        {
          TextBox_CostCentre.Text = "";
        }
        else
        {
          TextBox_CostCentre.Text = Request.QueryString["s_CostCentre"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (!string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Label_InvalidSearchMessage.Text = Label_InvalidSearchMessageText;
      }
      else
      {
        string SearchField1 = DropDownList_BusinessUnit.SelectedValue;
        string SearchField2 = Server.HtmlEncode(TextBox_BusinessUnitComponent.Text);
        string SearchField3 = DropDownList_BusinessUnitComponentType.SelectedValue;
        string SearchField4 = Server.HtmlEncode(TextBox_BusinessUnitComponentDescription.Text);
        string SearchField5 = Server.HtmlEncode(TextBox_Entity.Text);
        string SearchField6 = Server.HtmlEncode(TextBox_CostCentre.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_BusinessUnit=" + DropDownList_BusinessUnit.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_BusinessUnitComponent=" + Server.HtmlEncode(TextBox_BusinessUnitComponent.Text.ToString()) + "&";
        }
        
        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_BusinessUnitComponentType=" + DropDownList_BusinessUnitComponentType.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_BusinessUnitComponentDescription=" + Server.HtmlEncode(TextBox_BusinessUnitComponentDescription.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_Entity=" + Server.HtmlEncode(TextBox_Entity.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_CostCentre=" + Server.HtmlEncode(TextBox_CostCentre.Text.ToString()) + "&";
        }

        string FinalURL = "Form_FSM_BusinessUnitComponent_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Unit Components", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected string SearchValidation()
    {
      string InvalidSearch = "No";
      string InvalidSearchMessage = "";

      if (InvalidSearch == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_BusinessUnit.SelectedValue))
        {
          //InvalidSearch = "Yes";
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

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Unit Components", "Form_FSM_BusinessUnitComponent_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_FSM_BusinessUnitComponent_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnitComponent_List.PageSize = Convert.ToInt32(((DropDownList)GridView_FSM_BusinessUnitComponent_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnitComponent_List.PageIndex = ((DropDownList)GridView_FSM_BusinessUnitComponent_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_FSM_BusinessUnitComponent_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_FSM_BusinessUnitComponent_List.PageSize <= 20)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_FSM_BusinessUnitComponent_List.PageSize > 20 && GridView_FSM_BusinessUnitComponent_List.PageSize <= 50)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_FSM_BusinessUnitComponent_List.PageSize > 50 && GridView_FSM_BusinessUnitComponent_List.PageSize <= 100)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_FSM_BusinessUnitComponent_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FSM_BusinessUnitComponent_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FSM_BusinessUnitComponent_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FSM_BusinessUnitComponent_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FSM_BusinessUnitComponent_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit Component", "Form_FSM_BusinessUnitComponent.aspx"), false);
    }

    public string GetLink(object businessUnitComponentKey)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit", "Form_FSM_BusinessUnitComponent.aspx?BusinessUnitComponentKey=" + businessUnitComponentKey + "") + "'>View</a>";

      string SearchField1 = Request.QueryString["s_BusinessUnitComponent"];
      string SearchField2 = Request.QueryString["s_BusinessUnit"];
      string SearchField3 = Request.QueryString["s_BusinessUnitComponentType"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_BusinessUnitComponent=" + Request.QueryString["s_BusinessUnitComponent"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_BusinessUnitName=" + Request.QueryString["s_BusinessUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_BusinessUnitComponentType=" + Request.QueryString["s_BusinessUnitComponentType"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3;
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
    protected void SqlDataSource_FSM_BusinessUnitComponent_Mapping_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Mapping.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Mapping_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize = Convert.ToInt32(((DropDownList)GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Mapping_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_BusinessUnitComponent_Mapping_List.PageIndex = ((DropDownList)GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Mapping")).SelectedIndex;
    }

    protected void GridView_FSM_BusinessUnitComponent_Mapping_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize <= 20)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "20";
        }
        else if (GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize > 20 && GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize <= 50)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "50";
        }
        else if (GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize > 50 && GridView_FSM_BusinessUnitComponent_Mapping_List.PageSize <= 100)
        {
          ((DropDownList)GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Mapping")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_FSM_BusinessUnitComponent_Mapping_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FSM_BusinessUnitComponent_Mapping_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FSM_BusinessUnitComponent_Mapping_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FSM_BusinessUnitComponent_Mapping_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Mapping")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FSM_BusinessUnitComponent_Mapping_List_RowCreated(object sender, GridViewRowEventArgs e)
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