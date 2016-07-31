using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_Location_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Captured Locations", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search Captured Locations", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of Captured Locations", CultureInfo.CurrentCulture);

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
        ((Label)PageUpdateProgress_FSM_Location_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_Location_Name.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Name.SelectCommand = "SELECT LocationKey , LocationName FROM BusinessUnit.Location ORDER BY LocationName";

      SqlDataSource_FSM_Location_Country.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Country.SelectCommand = "SELECT CountryKey , CountryName FROM Geographic.Country ORDER BY CountryName";

      SqlDataSource_FSM_Location_Province.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Province.SelectCommand = "SELECT ProvinceKey , ProvinceName FROM Geographic.Province WHERE CountryKey = @CountryKey ORDER BY ProvinceName";
      SqlDataSource_FSM_Location_Province.SelectParameters.Clear();
      SqlDataSource_FSM_Location_Province.SelectParameters.Add("CountryKey", TypeCode.String, "");

      SqlDataSource_FSM_Location_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_List.SelectCommand = @"SELECT		Location.LocationKey ,
						                                                      Location.LocationName , 
						                                                      Country.CountryName ,
						                                                      Province.ProvinceName , 
						                                                      CASE 
							                                                      WHEN Location.IsActive = 1 THEN 'Yes'
							                                                      WHEN Location.IsActive = 0 THEN 'No'
						                                                      END AS IsActive
	                                                      FROM			BusinessUnit.Location
						                                                      LEFT JOIN Geographic.Province ON Location.ProvinceKey = Province.ProvinceKey
						                                                      LEFT JOIN Geographic.Country ON Province.CountryKey = Country.CountryKey
	                                                      WHERE			(Location.LocationKey = @Location OR @Location IS NULL)
						                                                      AND (Country.CountryKey = @Country OR @Country IS NULL)
						                                                      AND (Province.ProvinceKey = @Province OR @Province IS NULL)
	                                                      ORDER BY	Location.LocationName";
      SqlDataSource_FSM_Location_List.CancelSelectOnNullParameter = false;
      SqlDataSource_FSM_Location_List.SelectParameters.Clear();
      SqlDataSource_FSM_Location_List.SelectParameters.Add("Location", TypeCode.String, Request.QueryString["s_Location"]);
      SqlDataSource_FSM_Location_List.SelectParameters.Add("Country", TypeCode.String, Request.QueryString["s_Country"]);
      SqlDataSource_FSM_Location_List.SelectParameters.Add("Province", TypeCode.String, Request.QueryString["s_Province"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Name.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Location"] == null)
        {
          DropDownList_Name.SelectedValue = "";
        }
        else
        {
          DropDownList_Name.SelectedValue = Request.QueryString["s_Location"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Country.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Country"] == null)
        {
          DropDownList_Country.SelectedValue = "";
        }
        else
        {
          DropDownList_Country.SelectedValue = Request.QueryString["s_Country"];

          DropDownList_Province.Items.Clear();
          SqlDataSource_FSM_Location_Province.SelectParameters["CountryKey"].DefaultValue = Request.QueryString["s_Country"];
          DropDownList_Province.Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
          DropDownList_Province.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Province.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Province"] == null)
        {
          DropDownList_Province.SelectedValue = "";
        }
        else
        {
          DropDownList_Province.SelectedValue = Request.QueryString["s_Province"];
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Province.Items.Clear();
      SqlDataSource_FSM_Location_Province.SelectParameters["CountryKey"].DefaultValue = DropDownList_Country.SelectedValue;
      DropDownList_Province.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
      DropDownList_Province.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Name.SelectedValue;
      string SearchField2 = DropDownList_Country.SelectedValue;
      string SearchField3 = DropDownList_Province.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Location=" + DropDownList_Name.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Country=" + DropDownList_Country.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Province=" + DropDownList_Province.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_FSM_Location_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Locations", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Locations", "Form_FSM_Location_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_FSM_Location_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_Location_List.PageSize = Convert.ToInt32(((DropDownList)GridView_FSM_Location_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_FSM_Location_List.PageIndex = ((DropDownList)GridView_FSM_Location_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_FSM_Location_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_FSM_Location_List.PageSize <= 20)
        {
          ((DropDownList)GridView_FSM_Location_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_FSM_Location_List.PageSize > 20 && GridView_FSM_Location_List.PageSize <= 50)
        {
          ((DropDownList)GridView_FSM_Location_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_FSM_Location_List.PageSize > 50 && GridView_FSM_Location_List.PageSize <= 100)
        {
          ((DropDownList)GridView_FSM_Location_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_FSM_Location_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FSM_Location_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FSM_Location_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FSM_Location_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FSM_Location_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Location", "Form_FSM_Location.aspx"), false);
    }

    public string GetLink(object locationKey)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Location", "Form_FSM_Location.aspx?LocationKey=" + locationKey + "") + "'>View</a>";

      string SearchField1 = Request.QueryString["s_Location"];
      string SearchField2 = Request.QueryString["s_Country"];
      string SearchField3 = Request.QueryString["s_Province"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_Location=" + Request.QueryString["s_Location"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_Country=" + Request.QueryString["s_Country"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_Province=" + Request.QueryString["s_Province"] + "&";
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
  }
}