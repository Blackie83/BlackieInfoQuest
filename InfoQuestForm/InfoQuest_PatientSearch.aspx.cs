using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_PatientSearch : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PatientSearch, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          TablePatientSearch.Visible = true;

          if (string.IsNullOrEmpty(Request.QueryString["s_FacilityId"]) || string.IsNullOrEmpty(Request.QueryString["s_Patient"]))
          {
            TablePatientSearchResults.Visible = false;
          }
          else
          {
            TablePatientSearchResults.Visible = true;
          }

          DropDownList_SearchFacilityId.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_SearchPatient.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_SearchPatient.Attributes.Add("OnInput", "Validation_Search();");

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_PatientSearch.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_SearchFacilityId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SearchFacilityId.SelectCommand = @"SELECT 		Facility_Id ,
                                                                  Facility_FacilityDisplayName
                                                        FROM			(
                                                                  SELECT	DISTINCT
                                                                          CAST(Facility_Id AS NVARCHAR(MAX)) AS Facility_Id ,
                                                                          Facility_FacilityDisplayName ,
                                                                          2 AS FacilityOrder
                                                                  FROM		vAdministration_SecurityAccess_Active
                                                                  WHERE		SecurityUser_UserName = @SecurityUser_UserName
                                                                  UNION
                                                                  SELECT	DISTINCT
                                                                          CAST(vAdministration_Facility_Active.Facility_Id AS NVARCHAR(MAX)) AS Facility_Id ,
                                                                          vAdministration_Facility_Active.Facility_FacilityDisplayName ,
                                                                          2 AS FacilityOrder
                                                                  FROM		vAdministration_Facility_Active , vAdministration_SecurityAccess_Active
                                                                  WHERE		SecurityUser_UserName = @SecurityUser_UserName AND SecurityRole_Rank = 1
                                                                  UNION
                                                                  SELECT	DISTINCT
                                                                          'All' ,
                                                                          'All Facilities' ,
                                                                          1 AS FacilityOrder
                                                                  FROM		vAdministration_SecurityAccess_Active
                                                                  WHERE		SecurityUser_UserName = @SecurityUser_UserName AND SecurityRole_Rank = 1
                                                                  ) AS TempTable
                                                        WHERE			Facility_Id IS NOT NULL
                                                                  AND Facility_Id NOT IN ('0','58','59','108','118','120','121')
                                                        ORDER BY	FacilityOrder ,
                                                                  Facility_FacilityDisplayName";
      SqlDataSource_SearchFacilityId.SelectParameters.Clear();
      SqlDataSource_SearchFacilityId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);

      ObjectDataSource_PatientSearch.SelectMethod = "DataPatient_ODS_PatientSearch";
      ObjectDataSource_PatientSearch.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_PatientSearch.SelectParameters.Clear();
      ObjectDataSource_PatientSearch.SelectParameters.Add("facilityId", TypeCode.String, Request.QueryString["s_FacilityId"]);
      ObjectDataSource_PatientSearch.SelectParameters.Add("patient", TypeCode.String, Request.QueryString["s_Patient"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_SearchFacilityId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_FacilityId"]))
        {
          DropDownList_SearchFacilityId.SelectedValue = "";
        }
        else
        {
          DropDownList_SearchFacilityId.SelectedValue = Request.QueryString["s_FacilityId"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_SearchPatient.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Patient"]))
        {
          TextBox_SearchPatient.Text = "";
        }
        else
        {
          TextBox_SearchPatient.Text = Request.QueryString["s_Patient"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_OnClick(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest Patient Search", "InfoQuest_PatientSearch.aspx?s_FacilityId=" + DropDownList_SearchFacilityId.SelectedValue.ToString() + "&s_Patient=" + Server.HtmlEncode(TextBox_SearchPatient.Text.ToString()) + ""), false);
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
        if (string.IsNullOrEmpty(DropDownList_SearchFacilityId.SelectedValue))
        {
          InvalidSearch = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_SearchPatient.Text))
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest Patient Search", "InfoQuest_PatientSearch.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void ObjectDataSource_PatientSearch_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_PatientSearch.PageSize = Convert.ToInt32(((DropDownList)GridView_PatientSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_PatientSearch.PageIndex = ((DropDownList)GridView_PatientSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_PatientSearch_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_PatientSearch.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_PatientSearch.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_PatientSearch.PageSize > 20 && GridView_PatientSearch.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_PatientSearch.PageSize > 50 && GridView_PatientSearch.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_PatientSearch_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PatientSearch.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PatientSearch.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PatientSearch.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PatientSearch_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List--//
  }
}