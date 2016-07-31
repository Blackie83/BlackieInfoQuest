using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Facility_Unit : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Facility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Facility_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Facility_Unit, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_Facility_List.SelectParameters["FacilityId"].DefaultValue = "0";

          SetFormQueryString();

          if (string.IsNullOrEmpty(Request.QueryString["Facility_Id"]))
          {
            TableFacilityUnit.Visible = false;
          }
          else
          {
            TableFacilityUnit.Visible = true;
          }

          if (TableFacilityUnit.Visible == true)
          {
            FacilityUnit();
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
        //Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Facility_Unit.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_FacilityId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]))
        {
          DropDownList_FacilityId.SelectedValue = "";
        }
        else
        {
          DropDownList_FacilityId.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_FacilityId.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_FacilityId.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Administration_Facility_Unit.aspx?" + SearchField1;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      //String FinalURL = "Administration_Facility_Unit.aspx";
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", "Administration_Facility_Unit.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Facility_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {

      DropDownList DropDownList_PageSize = (DropDownList)GridView_Facility_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Facility_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);

    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {

      DropDownList DropDownList_PageList = (DropDownList)GridView_Facility_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Facility_List.PageIndex = DropDownList_PageList.SelectedIndex;

    }

    protected void GridView_Facility_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Facility_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Facility_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Facility_List.PageSize > 20 && GridView_Facility_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Facility_List.PageSize > 50 && GridView_Facility_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_Facility_List.Rows.Count; i++)
      {
        if (GridView_Facility_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_Facility_List.Rows[i].Cells[2].Text.ToString() == "Yes")
          {
            GridView_Facility_List.Rows[i].Cells[2].BackColor = Color.FromName("#77cf9c");
            GridView_Facility_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Facility_List.Rows[i].Cells[2].Text.ToString() == "No")
          {
            GridView_Facility_List.Rows[i].Cells[2].BackColor = Color.FromName("#d46e6e");
            GridView_Facility_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_Facility_List.Rows[i].Cells[2].BackColor = Color.FromName("#d46e6e");
            GridView_Facility_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_Facility_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_Facility_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Facility_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Facility_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_Facility_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object facility_Id)
    {
      //String LinkURL = "<a href='Administration_Facility_Unit.aspx?Facility_Id=" + facility_Id + "'>Update</a>";
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", "Administration_Facility_Unit.aspx?Facility_Id=" + facility_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Facility_Id"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      string SearchURL = SearchField1;
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


    //--START-- --FacilityUnit--//
    private void FacilityUnit()
    {
      string FacilityFacilityDisplayName = "";
      string SQLStringFacilityName = "SELECT Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName  FROM vAdministration_Facility_All WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_FacilityName = new SqlCommand(SQLStringFacilityName))
      {
        SqlCommand_FacilityName.Parameters.AddWithValue("@Facility_Id", Request.QueryString["Facility_Id"]);
        DataTable DataTable_FacilityName;
        using (DataTable_FacilityName = new DataTable())
        {
          DataTable_FacilityName.Locale = CultureInfo.CurrentCulture;

          DataTable_FacilityName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityName).Copy();
          if (DataTable_FacilityName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityName.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
            }
          }
        }
      }

      Label_FacilityName.Text = FacilityFacilityDisplayName;

      ((CheckBoxList)CheckBoxList_Unit).Attributes.Add("OnClick", "Validation_Form();");

      SqlDataSource_Unit.DataBind();
    }

    protected void CheckBoxList_Unit_DataBound(object sender, EventArgs e)
    {
      if (Request.QueryString["Facility_Id"] != null)
      {
        for (int i = 0; i < CheckBoxList_Unit.Items.Count; i++)
        {
          Session["UnitId"] = "";
          string SQLStringUnitId = "SELECT DISTINCT Unit_Id FROM Administration_Facility_Unit WHERE Facility_Id = @Facility_Id AND Unit_Id = @Unit_Id";
          using (SqlCommand SqlCommand_UnitId = new SqlCommand(SQLStringUnitId))
          {
            SqlCommand_UnitId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["Facility_Id"]);
            SqlCommand_UnitId.Parameters.AddWithValue("@Unit_Id", CheckBoxList_Unit.Items[i].Value);
            DataTable DataTable_UnitId;
            using (DataTable_UnitId = new DataTable())
            {
              DataTable_UnitId.Locale = CultureInfo.CurrentCulture;

              DataTable_UnitId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_UnitId).Copy();
              if (DataTable_UnitId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_UnitId.Rows)
                {
                  Session["UnitId"] = DataRow_Row["Unit_Id"];
                  CheckBoxList_Unit.Items[i].Selected = true;
                }
              }
            }
          }

          Session["UnitId"] = "";
        }
      }
    }

    protected void Button_FacilityUnitClear_Click(object sender, EventArgs e)
    {
      //String FinalURL = "Administration_Facility_Unit.aspx";
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", "Administration_Facility_Unit.aspx");
      Response.Redirect(FinalURL, false);
    }

    protected void Button_FacilityUnitUpdate_Click(object sender, EventArgs e)
    {
      string SQLStringDeleteFacilityUnit = "DELETE FROM Administration_Facility_Unit WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_DeleteFacilityUnit = new SqlCommand(SQLStringDeleteFacilityUnit))
      {
        SqlCommand_DeleteFacilityUnit.Parameters.AddWithValue("@Facility_Id", Request.QueryString["Facility_Id"]);
        InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteFacilityUnit);
      }


      for (int i = 0; i < CheckBoxList_Unit.Items.Count; i++)
      {
        if (CheckBoxList_Unit.Items[i].Selected == true)
        {
          string SQLStringInsertFacilityUnit = "INSERT INTO Administration_Facility_Unit ( Facility_Id ,Unit_Id ,Facility_Unit_CreatedDate ,Facility_Unit_CreatedBy ,Facility_Unit_ModifiedDate ,Facility_Unit_ModifiedBy ) VALUES ( @Facility_Id ,@Unit_Id ,@Facility_Unit_CreatedDate ,@Facility_Unit_CreatedBy ,@Facility_Unit_ModifiedDate ,@Facility_Unit_ModifiedBy )";
          using (SqlCommand SqlCommand_InsertFacilityUnit = new SqlCommand(SQLStringInsertFacilityUnit))
          {
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Facility_Id", Request.QueryString["Facility_Id"]);
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Unit_Id", CheckBoxList_Unit.Items[i].Value.ToString());
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Facility_Unit_CreatedDate", DateTime.Now);
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Facility_Unit_CreatedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Facility_Unit_ModifiedDate", DateTime.Now);
            SqlCommand_InsertFacilityUnit.Parameters.AddWithValue("@Facility_Unit_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFacilityUnit);
          }
        }
      }

      //String FinalURL = "Administration_Facility_Unit.aspx";
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", "Administration_Facility_Unit.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --FacilityUnit--//
  }
}