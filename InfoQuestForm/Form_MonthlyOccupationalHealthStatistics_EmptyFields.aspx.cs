using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyOccupationalHealthStatistics_EmptyFields : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString() + " : Empty Fields", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString() + " Empty Fields", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString() + " Empty Fields", CultureInfo.CurrentCulture);

          if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_Unit_Id"] == null && Request.QueryString["s_PeriodFrom"] == null && Request.QueryString["s_PeriodTo"] == null && Request.QueryString["s_FYPeriodFrom"] == null && Request.QueryString["s_FYPeriodTo"] == null && Request.QueryString["s_EmptyField"] == null)
          {
            DivEmptyFields1.Visible = false;
            DivEmptyFields2.Visible = false;
            TableEmptyFields.Visible = false;
          }
          else
          {
            DivEmptyFields1.Visible = true;
            DivEmptyFields2.Visible = true;
            TableEmptyFields.Visible = true;

            SetFormQueryString();
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
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('182','183'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("46");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MonthlyOccupationalHealthStatistics_EmptyFields.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Occupational Health Statistics", "22");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Facility.SelectParameters.Clear();
      SqlDataSource_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "46");
      SqlDataSource_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_MonthlyOccupationalHealthStatistics");
      SqlDataSource_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Unit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Unit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Unit.SelectParameters.Clear();
      SqlDataSource_Unit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Unit.SelectParameters.Add("Form_Id", TypeCode.String, "46");
      SqlDataSource_Unit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_Unit.SelectParameters.Add("TableSELECT", TypeCode.String, "Unit_Id");
      SqlDataSource_Unit.SelectParameters.Add("TableFROM", TypeCode.String, "Form_MonthlyOccupationalHealthStatistics");
      SqlDataSource_Unit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PeriodFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PeriodFrom.SelectCommand = "SELECT DISTINCT MOHS_Period FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_Period DESC";

      SqlDataSource_PeriodTo.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PeriodTo.SelectCommand = "SELECT DISTINCT MOHS_Period FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_Period DESC";

      SqlDataSource_FYPeriodFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FYPeriodFrom.SelectCommand = "SELECT DISTINCT MOHS_FYPeriod FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_FYPeriod DESC";

      SqlDataSource_FYPeriodTo.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FYPeriodTo.SelectCommand = "SELECT DISTINCT MOHS_FYPeriod FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_FYPeriod DESC";

      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectCommand = "spForm_Get_MonthlyOccupationalHealthStatistics_EmptyFields";
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.CancelSelectOnNullParameter = false;
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Clear();
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("UnitId", TypeCode.String, Request.QueryString["s_Unit_Id"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("PeriodFrom", TypeCode.String, Request.QueryString["s_PeriodFrom"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("PeriodTo", TypeCode.String, Request.QueryString["s_PeriodTo"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("FYPeriodFrom", TypeCode.String, Request.QueryString["s_FYPeriodFrom"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("FYPeriodTo", TypeCode.String, Request.QueryString["s_FYPeriodTo"]);
      SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields.SelectParameters.Add("EmptyField", TypeCode.String, Request.QueryString["s_EmptyField"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];

          DropDownList_Unit.Items.Clear();
          SqlDataSource_Unit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_Unit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
          DropDownList_Unit.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Unit.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Unit_Id"] == null)
        {
          DropDownList_Unit.SelectedValue = "";
        }
        else
        {
          DropDownList_Unit.SelectedValue = Request.QueryString["s_Unit_Id"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_PeriodFrom.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PeriodFrom"] == null)
        {
          DropDownList_PeriodFrom.SelectedValue = "";
        }
        else
        {
          DropDownList_PeriodFrom.SelectedValue = Request.QueryString["s_PeriodFrom"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_PeriodTo.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PeriodTo"] == null)
        {
          DropDownList_PeriodTo.SelectedValue = "";
        }
        else
        {
          DropDownList_PeriodTo.SelectedValue = Request.QueryString["s_PeriodTo"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FYPeriodFrom.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_FYPeriodFrom"] == null)
        {
          DropDownList_FYPeriodFrom.SelectedValue = "";
        }
        else
        {
          DropDownList_FYPeriodFrom.SelectedValue = Request.QueryString["s_FYPeriodFrom"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FYPeriodTo.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_FYPeriodTo"] == null)
        {
          DropDownList_FYPeriodTo.SelectedValue = "";
        }
        else
        {
          DropDownList_FYPeriodTo.SelectedValue = Request.QueryString["s_FYPeriodTo"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_EmptyField.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_EmptyField"] == null)
        {
          DropDownList_EmptyField.SelectedValue = "";
        }
        else
        {
          DropDownList_EmptyField.SelectedValue = Request.QueryString["s_EmptyField"];
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Unit.Items.Clear();
      SqlDataSource_Unit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue;
      DropDownList_Unit.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_Unit.DataBind();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Empty Fields", "Form_MonthlyOccupationalHealthStatistics_EmptyFields.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Unit.SelectedValue;
      string SearchField3 = DropDownList_PeriodFrom.SelectedValue;
      string SearchField4 = DropDownList_PeriodTo.SelectedValue;
      string SearchField5 = DropDownList_FYPeriodFrom.SelectedValue;
      string SearchField6 = DropDownList_FYPeriodTo.SelectedValue;
      string SearchField7 = DropDownList_EmptyField.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3) && string.IsNullOrEmpty(SearchField4) && string.IsNullOrEmpty(SearchField5) && string.IsNullOrEmpty(SearchField6) && string.IsNullOrEmpty(SearchField7))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Empty Fields", "Form_MonthlyOccupationalHealthStatistics_EmptyFields.aspx"), false);
      }
      else
      {
        if (string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_Unit_Id=" + DropDownList_Unit.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_PeriodFrom=" + DropDownList_PeriodFrom.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_PeriodTo=" + DropDownList_PeriodTo.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "";
        }
        else
        {
          SearchField5 = "s_FYPeriodFrom=" + DropDownList_FYPeriodFrom.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "";
        }
        else
        {
          SearchField6 = "s_FYPeriodTo=" + DropDownList_FYPeriodTo.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField7))
        {
          SearchField7 = "";
        }
        else
        {
          SearchField7 = "s_EmptyField=" + DropDownList_EmptyField.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_MonthlyOccupationalHealthStatistics_EmptyFields.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Empty Fields", FinalURL);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List MonthlyOccupationalHealthStatistics_EmptyFields--//
    protected void SqlDataSource_MonthlyOccupationalHealthStatistics_EmptyFields_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_MonthlyOccupationalHealthStatistics_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_MonthlyOccupationalHealthStatistics_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_MonthlyOccupationalHealthStatistics_EmptyFields_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_MonthlyOccupationalHealthStatistics_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize > 20 && GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize > 50 && GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      GridView_MonthlyOccupationalHealthStatistics_EmptyFields_HideColumns();

      for (int i = 0; i < GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows.Count; i++)
      {
        if (GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].RowType == DataControlRowType.DataRow)
        {
          for (int a = 4; a <= 12; a++)
          {
            if (GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].Cells[a].Text == "Required")
            {
              GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].Cells[a].BackColor = Color.FromName("#d46e6e");
              GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].Cells[a].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].Cells[a].BackColor = Color.FromName("#77cf9c");
              GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Rows[i].Cells[a].ForeColor = Color.FromName("#333333");
            }
          }
        }
      }
    }

    protected void GridView_MonthlyOccupationalHealthStatistics_EmptyFields_HideColumns()
    {
      if (Request.QueryString["s_EmptyField"] == "Clinic Visits")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Labour Hours")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Patient Satisfaction Score")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Patient Satisfaction Response Rate")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Comment Cards-Number Received Positive")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Comment Cards-Number Received Suggestions")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Comment Cards-Number Received Negative (P3)")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Care: Total Staff Trained")               
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = true;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = false;
      }
      else if (Request.QueryString["s_EmptyField"] == "Care: Total Staff Employed")
      {
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[4].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[5].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[6].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[7].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[8].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[9].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[10].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[11].Visible = false;
        GridView_MonthlyOccupationalHealthStatistics_EmptyFields.Columns[12].Visible = true;
      }
    }

    protected void GridView_MonthlyOccupationalHealthStatistics_EmptyFields_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_MonthlyOccupationalHealthStatistics_EmptyFields.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_MonthlyOccupationalHealthStatistics_EmptyFields.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_MonthlyOccupationalHealthStatistics_EmptyFields_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List MonthlyOccupationalHealthStatistics_EmptyFields--//
  }
}