﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CollegeLearningAudit_Summary : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString() + " : Summary", CultureInfo.CurrentCulture);
          Label_ReviewHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString() + " Summary", CultureInfo.CurrentCulture);

          if (Request.QueryString["CLA_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableButtons.Visible = true;
            TableList.Visible = true;

            CollegeLearningAuditSummary();
          }
          else
          {
            TableReviewInfo.Visible = false;
            TableButtons.Visible = false;
            TableList.Visible = false;
          }

          if (TableReviewInfo.Visible == true)
          {
            TableReviewInfoVisible();
          }

          if (TableButtons.Visible == true)
          {
            TableButtonsVisible();
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
        if (Request.QueryString["CLA_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('49')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("49");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CollegeLearningAudit_Summary.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Internal Quality Audit", "24");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CollegeLearningAudit_Summary_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Summary_List.SelectCommand = "spForm_Get_CollegeLearningAudit_Summary_List";
      SqlDataSource_CollegeLearningAudit_Summary_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CollegeLearningAudit_Summary_List.CancelSelectOnNullParameter = false;
      SqlDataSource_CollegeLearningAudit_Summary_List.SelectParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Summary_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CollegeLearningAudit_Summary_List.SelectParameters.Add("CLAId", TypeCode.String, Request.QueryString["CLA_Id"]);
    }

    private void CollegeLearningAuditSummary()
    {
      Session["CLAId"] = "";
      string SQLStringCollegeLearningAudit = "SELECT CLA_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id AND CLA_IsActive = 1";
      using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
      {
        SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_CollegeLearningAudit;
        using (DataTable_CollegeLearningAudit = new DataTable())
        {
          DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
          if (DataTable_CollegeLearningAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
            {
              Session["CLAId"] = DataRow_Row["CLA_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["CLAId"].ToString()))
      {
        TableReviewInfo.Visible = false;
        TableButtons.Visible = false;
        TableList.Visible = false;
      }
      else
      {
        TableReviewInfo.Visible = true;
        TableButtons.Visible = true;
        TableList.Visible = true;
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["CLADate"] = "";
      Session["CLACompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , CLA_Date , CLA_Completed FROM vForm_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
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
              Session["CLADate"] = DataRow_Row["CLA_Date"];
              Session["CLACompleted"] = DataRow_Row["CLA_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["CLADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["CLACompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["CLACompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("CLADate");
      Session.Remove("CLACompleted");
    }

    private void TableButtonsVisible()
    {
      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 49";
      using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
      {
        DataTable DataTable_Email;
        using (DataTable_Email = new DataTable())
        {
          DataTable_Email.Locale = CultureInfo.CurrentCulture;
          DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
          if (DataTable_Email.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Email.Rows)
            {
              Email = DataRow_Row["Form_Email"].ToString();
              Print = DataRow_Row["Form_Print"].ToString();
            }
          }
        }
      }

      if (Print == "False")
      {
        Button_Print.Visible = false;
      }
      else
      {
        Button_Print.Visible = true;
      }

      if (Email == "False")
      {
        Button_Email.Visible = false;
      }
      else
      {
        Button_Email.Visible = true;
      }

      Email = "";
      Print = "";
    }


    //--START-- --List--//
    protected void Button_Print_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_CollegeLearningAudit_Summary, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Summary Print", "Form_CollegeLearningAudit_Summary_Print.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_CollegeLearningAudit_Summary, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_Email_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_CollegeLearningAudit_Summary, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Summary Email", "InfoQuest_Email.aspx?EmailPage=Form_CollegeLearningAudit_Summary&EmailValue=" + Request.QueryString["CLA_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_CollegeLearningAudit_Summary, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_Back_Click(object sender, EventArgs e)
    {
      if (Request.QueryString["CLA_Id"] != null)
      {
        string FinalURL = "";

        string SearchField1 = Request.QueryString["Search_FacilityId"];
        string SearchField2 = Request.QueryString["Search_CLACompleted"];

        if (SearchField1 == null && SearchField2 == null)
        {
          FinalURL = "Form_CollegeLearningAudit_List.aspx";
        }
        else
        {
          if (SearchField1 == null)
          {
            SearchField1 = "";
          }
          else
          {
            SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
          }

          if (SearchField2 == null)
          {
            SearchField2 = "";
          }
          else
          {
            SearchField2 = "s_CLA_Completed=" + Request.QueryString["Search_CLACompleted"] + "&";
          }

          string SearchURL = "Form_CollegeLearningAudit_List.aspx?" + SearchField1 + SearchField2;
          SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

          FinalURL = SearchURL;
        }

        Response.Redirect(FinalURL, false);
      }
    }

    protected void GridView_CollegeLearningAudit_Summary_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_CollegeLearningAudit_Summary_List_PreRender(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_CollegeLearningAudit_Summary_List.Rows.Count; i++)
      {
        if (GridView_CollegeLearningAudit_Summary_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("heading", StringComparison.CurrentCulture) == 0)
          {
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].ColumnSpan = 5;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].BackColor = Color.FromName("#003768");
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].ForeColor = Color.FromName("#ffffff");

            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[1].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[2].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[3].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[4].Visible = false;
          }


          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("subheading", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#A6A6A6");
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("score", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#b0262e");
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#ffffff");
            }
          }


          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowsubheading", StringComparison.CurrentCulture) == 0)
          {
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].ColumnSpan = 5;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].BackColor = Color.FromName("#DA9694");
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].ForeColor = Color.FromName("#000000");

            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[1].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[2].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[3].Visible = false;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[4].Visible = false;
          }


          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowheading", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              //GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#DA9694");
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#000000");
            }
          }


          if (GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Text.IndexOf("rowscore", StringComparison.CurrentCulture) == 0)
          {
            for (int a = 0; a <= 4; a++)
            {
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].HorizontalAlign = HorizontalAlign.Center;
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].BackColor = Color.FromName("#0070C0");
              GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].ForeColor = Color.FromName("#ffffff");
            }
          }


          for (int a = 0; a <= 4; a++)
          {
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].Font.Bold = true;
            GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[a].BorderColor = Color.FromName("#000000");
          }

          GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
          GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
          GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
          GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;

          GridView_CollegeLearningAudit_Summary_List.Rows[i].Cells[5].Visible = false;
        }
      }
    }
    //---END--- --List--//
  }
}