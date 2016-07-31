using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_QMSReview_Summary_Print : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " : Summary", CultureInfo.CurrentCulture);

          if (Request.QueryString["QMSReview_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableList.Visible = true;
            Button_Print.Visible = true;

            QMSReviewSummary();
          }
          else
          {
            TableReviewInfo.Visible = false;
            TableList.Visible = false;
            Button_Print.Visible = false;
          }

          if (TableReviewInfo.Visible == true)
          {
            TableReviewInfoVisible();
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

        string SQLStringSecurity = "";
        if (Request.QueryString["QMSReview_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('31')) AND (SecurityRole_Id IN ('122','123') OR Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("31");

      if (PageSecurity() == "1")
      {
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_QMSReview_Summary_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Summary_List.SelectCommand = "spForm_Get_QMSReview_Summary_List";
      SqlDataSource_QMSReview_Summary_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_QMSReview_Summary_List.CancelSelectOnNullParameter = false;
      SqlDataSource_QMSReview_Summary_List.SelectParameters.Clear();
      SqlDataSource_QMSReview_Summary_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_QMSReview_Summary_List.SelectParameters.Add("QMSReviewId", TypeCode.String, Request.QueryString["QMSReview_Id"]);
    }

    private void QMSReviewSummary()
    {
      Session["QMSReviewId"] = "";
      string SQLStringQMSReview = "SELECT QMSReview_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_IsActive = 1";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        DataTable DataTable_QMSReview;
        using (DataTable_QMSReview = new DataTable())
        {
          DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
          if (DataTable_QMSReview.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
            {
              Session["QMSReviewId"] = DataRow_Row["QMSReview_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
      {
        TableReviewInfo.Visible = false;
        TableList.Visible = false;
        Button_Print.Visible = false;
      }
      else
      {
        TableReviewInfo.Visible = true;
        TableList.Visible = true;
        Button_Print.Visible = true;
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["QMSReviewDate"] = "";
      Session["QMSReviewCompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , QMSReview_Date , QMSReview_Completed FROM vForm_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        DataTable DataTable_ReviewInfo;
        using (DataTable_ReviewInfo = new DataTable())
        {
          DataTable_ReviewInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_ReviewInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ReviewInfo).Copy();
          if (DataTable_ReviewInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ReviewInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["QMSReviewDate"] = DataRow_Row["QMSReview_Date"];
              Session["QMSReviewCompleted"] = DataRow_Row["QMSReview_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["QMSReviewDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("QMSReviewDate");
      Session.Remove("QMSReviewCompleted");
    }


    protected void Button_Print_Click(object sender, EventArgs e)
    {
      Button_Print.Visible = false;
      ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Summary_Print, this.GetType(), "Print", "window.print()", true);
      GridView_QMSReview_Summary_List.DataBind();
    }

    protected void GridView_QMSReview_Summary_List_PreRender(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_QMSReview_Summary_List.Rows.Count; i++)
      {
        if (GridView_QMSReview_Summary_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("heading", StringComparison.CurrentCulture) == 0)
          {
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].ColumnSpan = 5;
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].BackColor = Color.FromName("#F2F2F2");
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].ForeColor = Color.FromName("#000000");

            GridView_QMSReview_Summary_List.Rows[i].Cells[1].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[2].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[3].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[4].Visible = false;
          }


          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("subheading", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#F2F2F2");
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("score", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#F2F2F2");
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowsubheading", StringComparison.CurrentCulture) == 0)
          {
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].ColumnSpan = 5;
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].BackColor = Color.FromName("#F2F2F2");
            GridView_QMSReview_Summary_List.Rows[i].Cells[0].ForeColor = Color.FromName("#000000");

            GridView_QMSReview_Summary_List.Rows[i].Cells[1].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[2].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[3].Visible = false;
            GridView_QMSReview_Summary_List.Rows[i].Cells[4].Visible = false;
          }


          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowheading", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              //GridView_QMSReview_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#F2F2F2");
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          if (GridView_QMSReview_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowscore", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#F2F2F2");
              GridView_QMSReview_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          for (int a = 0; a <= 4; a++)
          {
            GridView_QMSReview_Summary_List.Rows[i].Cells[a].Font.Bold = true;
            GridView_QMSReview_Summary_List.Rows[i].Cells[a].Font.Name = "Verdana";
            GridView_QMSReview_Summary_List.Rows[i].Cells[a].BorderColor = Color.FromName("#000000");
          }

          GridView_QMSReview_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
          GridView_QMSReview_Summary_List.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
          GridView_QMSReview_Summary_List.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
          GridView_QMSReview_Summary_List.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;

          GridView_QMSReview_Summary_List.Rows[i].Cells[5].Visible = false;
        }
      }
    }
  }
}