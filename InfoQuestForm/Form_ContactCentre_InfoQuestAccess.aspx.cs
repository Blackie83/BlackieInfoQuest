using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Permissions;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_ContactCentre_InfoQuestAccess : InfoQuestWCF.Override_SystemWebUIPage
  {
    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_ContactCentre_InfoQuestAccess, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("20")).ToString();

          SetFormQueryString();

          if (string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_UserName"]) && string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_DisplayName"]) && string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_EmployeeNumber"]))
          {
            TableSecurityUsers.Visible = false;
            TableSecurityAccess1.Visible = false;
            TableSecurityAccess2.Visible = false;
            TableSecurityAccess3.Visible = false;
          }
          else
          {
            TableSecurityUsers.Visible = true;

            if (string.IsNullOrEmpty(Request.QueryString["SecurityUser_Id"]))
            {
              SecurityUserDetail();

              SecurityUserActive();

              TableSecurityAccess1.Visible = false;
              TableSecurityAccess2.Visible = false;
              TableSecurityAccess3.Visible = false;
            }
            else
            {
              TableSecurityAccess1.Visible = true;
              TableSecurityAccess2.Visible = true;
              TableSecurityAccess3.Visible = true;
            }

            if (TableSecurityAccess1.Visible == true && TableSecurityAccess2.Visible == true && TableSecurityAccess3.Visible == true)
            {
              SecurityAccess();
            }
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('20'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("20");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_ContactCentre_InfoQuestAccess.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_SecurityUser_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SecurityUser_List.SelectCommand = "spAdministration_Get_SecurityUser_List";
      SqlDataSource_SecurityUser_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_SecurityUser_List.CancelSelectOnNullParameter = false;
      SqlDataSource_SecurityUser_List.SelectParameters.Clear();
      SqlDataSource_SecurityUser_List.SelectParameters.Add("UserName", TypeCode.String, Request.QueryString["s_SecurityAccess_UserName"]);
      SqlDataSource_SecurityUser_List.SelectParameters.Add("DisplayName", TypeCode.String, Request.QueryString["s_SecurityAccess_DisplayName"]);
      SqlDataSource_SecurityUser_List.SelectParameters.Add("EmployeeNumber", TypeCode.String, Request.QueryString["s_SecurityAccess_EmployeeNumber"]);
      SqlDataSource_SecurityUser_List.SelectParameters.Add("ManagerUserName", TypeCode.String, Request.QueryString["Empty"]);
      SqlDataSource_SecurityUser_List.SelectParameters.Add("IsActive", TypeCode.String, Request.QueryString["Empty"]);

      SqlDataSource_SecurityAccess_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SecurityAccess_List.SelectCommand = "SELECT DISTINCT SecurityUser_Id , SecurityUser_UserName , Facility_Id , Facility_FacilityDisplayName FROM vAdministration_SecurityAccess_All WHERE SecurityUser_Id = @SecurityUser_Id AND SecurityRole_Rank > 2 ORDER BY Facility_FacilityDisplayName";
      SqlDataSource_SecurityAccess_List.SelectParameters.Clear();
      SqlDataSource_SecurityAccess_List.SelectParameters.Add("SecurityUser_Id", TypeCode.String, Request.QueryString["SecurityUser_Id"]);

      SqlDataSource_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Facility.SelectCommand = "SELECT Facility_Id , Facility_FacilityDisplayName + ' : ' + CASE CAST(Facility_IsActive AS NVARCHAR(MAX)) WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName";

      SqlDataSource_SecurityRole.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SecurityRole.SelectCommand = "SELECT SecurityRole_Id , '<strong>' + Form_Name + '</strong> -- ' + REPLACE(SecurityRole_Name, Form_Name + ' ', '') + ' -- (' + CAST(SecurityRole_Rank AS NVARCHAR(MAX)) + ')' AS SecurityRole_Name FROM vAdministration_SecurityRole_All WHERE Form_Id IN ( SELECT Form_Id FROM Administration_Facility_Form WHERE Facility_Id = @FacilityId ) AND Form_Id IN ( SELECT Form_Id FROM Administration_Form WHERE Form_IsActive = 1 ) AND SecurityRole_Rank NOT IN (1,2) AND SecurityRole_IsActive = 1 ORDER BY Form_Name , SecurityRole_Rank , SecurityRole_Name";
      SqlDataSource_SecurityRole.SelectParameters.Clear();
      SqlDataSource_SecurityRole.SelectParameters.Add("FacilityId", TypeCode.String, "");

      SqlDataSource_CurrentAccess.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CurrentAccess.SelectCommand = "SELECT DISTINCT SecurityUser_UserName , SecurityUser_DisplayName ,CASE WHEN Facility_Id = '0' THEN 'All Facilities' ELSE Facility_FacilityDisplayName END AS Facility_FacilityDisplayName ,CASE WHEN Form_Id = '-1' THEN 'InfoQuest' ELSE Form_Name END AS Form_Name ,CASE WHEN REPLACE(SecurityRole_Name, Form_Name + ' ', '') IS NULL THEN SecurityRole_Name + ' -- ' + CAST(SecurityRole_Rank AS NVARCHAR(MAX)) ELSE REPLACE(SecurityRole_Name, Form_Name + ' ', '') + ' -- ' + CAST(SecurityRole_Rank AS NVARCHAR(MAX)) END	AS SecurityRole_Name , SecurityRole_Rank FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_Id = @SecurityUser_Id ORDER BY SecurityUser_UserName ,Facility_FacilityDisplayName ,Form_Name , SecurityRole_Rank";
      SqlDataSource_CurrentAccess.SelectParameters.Clear();
      SqlDataSource_CurrentAccess.SelectParameters.Add("SecurityUser_Id", TypeCode.String, Request.QueryString["SecurityUser_Id"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_UserName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_Username"]))
        {
          TextBox_UserName.Text = "";
        }
        else
        {
          TextBox_UserName.Text = Request.QueryString["s_SecurityAccess_Username"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_DisplayName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_DisplayName"]))
        {
          TextBox_DisplayName.Text = "";
        }
        else
        {
          TextBox_DisplayName.Text = Request.QueryString["s_SecurityAccess_DisplayName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_EmployeeNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityAccess_EmployeeNumber"]))
        {
          TextBox_EmployeeNumber.Text = "";
        }
        else
        {
          TextBox_EmployeeNumber.Text = Request.QueryString["s_SecurityAccess_EmployeeNumber"];
        }
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    private void SecurityUserDetail()
    {
      string AD_UserName = "";
      string AD_DisplayName = "";
      string AD_FirstName = "";
      string AD_LastName = "";
      string AD_EmployeeNumber = "";
      string AD_Email = "";
      string AD_ManagerUserName = "";
      string AD_Error = "";

      DataTable DataTable_AD;
      using (DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;
        DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll(Request.QueryString["s_SecurityAccess_UserName"], Request.QueryString["s_SecurityAccess_DisplayName"], Request.QueryString["s_SecurityAccess_EmployeeNumber"], "").Copy();
        if (DataTable_AD.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_AD.Rows)
          {
            AD_Error = DataRow_Row["Error"].ToString();
          }
        }
        else if (DataTable_AD.Columns.Count != 1)
        {
          if (DataTable_AD.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AD.Rows)
            {
              AD_UserName = DataRow_Row["UserName"].ToString();
              AD_DisplayName = DataRow_Row["DisplayName"].ToString();
              AD_FirstName = DataRow_Row["FirstName"].ToString();
              AD_LastName = DataRow_Row["LastName"].ToString();
              AD_EmployeeNumber = DataRow_Row["EmployeeNumber"].ToString();
              AD_Email = DataRow_Row["Email"].ToString();
              AD_ManagerUserName = DataRow_Row["ManagerUserName"].ToString();

              if (!string.IsNullOrEmpty(AD_UserName))
              {
                AD_UserName = "LHC\\" + AD_UserName;
              }

              if (!string.IsNullOrEmpty(AD_ManagerUserName))
              {
                AD_ManagerUserName = "LHC\\" + AD_ManagerUserName;
              }
              else
              {
                AD_ManagerUserName = "No Manager UserName";
              }

              if (string.IsNullOrEmpty(AD_EmployeeNumber))
              {
                AD_EmployeeNumber = "No Employee Number";
              }

              string SecurityUserId = "";
              string SQLStringSecurityUser = "SELECT SecurityUser_Id FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_Email = @SecurityUser_Email";
              using (SqlCommand SqlCommand_SecurityUser = new SqlCommand(SQLStringSecurityUser))
              {
                SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_UserName", AD_UserName);
                SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_Email", AD_Email);
                DataTable DataTable_SecurityUser;
                using (DataTable_SecurityUser = new DataTable())
                {
                  DataTable_SecurityUser.Locale = CultureInfo.CurrentCulture;
                  DataTable_SecurityUser = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUser).Copy();
                  if (DataTable_SecurityUser.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row_SecurityUser in DataTable_SecurityUser.Rows)
                    {
                      SecurityUserId = DataRow_Row_SecurityUser["SecurityUser_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(SecurityUserId))
              {
                string SQLStringInsertSecurityUser = "INSERT INTO Administration_SecurityUser ( SecurityUser_UserName ,SecurityUser_DisplayName ,SecurityUser_FirstName ,SecurityUser_LastName ,SecurityUser_EmployeeNumber ,SecurityUser_Email ,SecurityUser_ManagerUserName ,SecurityUser_CreatedDate ,SecurityUser_CreatedBy ,SecurityUser_ModifiedDate ,SecurityUser_ModifiedBy ,SecurityUser_History ,SecurityUser_IsActive ) VALUES ( @SecurityUser_UserName ,@SecurityUser_DisplayName ,@SecurityUser_FirstName ,@SecurityUser_LastName ,@SecurityUser_EmployeeNumber ,@SecurityUser_Email ,@SecurityUser_ManagerUserName ,@SecurityUser_CreatedDate ,@SecurityUser_CreatedBy ,@SecurityUser_ModifiedDate ,@SecurityUser_ModifiedBy ,@SecurityUser_History ,@SecurityUser_IsActive )";
                using (SqlCommand SqlCommand_InsertSecurityUser = new SqlCommand(SQLStringInsertSecurityUser))
                {
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_UserName", AD_UserName);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_DisplayName", AD_DisplayName);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_FirstName", AD_FirstName);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_LastName", AD_LastName);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_EmployeeNumber", AD_EmployeeNumber);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_Email", AD_Email);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_ManagerUserName", AD_ManagerUserName);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_CreatedDate", DateTime.Now);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_ModifiedDate", DateTime.Now);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_History", DBNull.Value);
                  SqlCommand_InsertSecurityUser.Parameters.AddWithValue("@SecurityUser_IsActive", true);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSecurityUser);
                }
              }
              else
              {
                string SQLStringEditSecurityUser = "UPDATE Administration_SecurityUser SET SecurityUser_DisplayName = @SecurityUser_DisplayName ,SecurityUser_FirstName = @SecurityUser_FirstName ,SecurityUser_LastName = @SecurityUser_LastName ,SecurityUser_EmployeeNumber = @SecurityUser_EmployeeNumber ,SecurityUser_Email = @SecurityUser_Email ,SecurityUser_ManagerUserName = @SecurityUser_ManagerUserName WHERE	SecurityUser_Id = @SecurityUser_Id";
                using (SqlCommand SqlCommand_EditSecurityUser = new SqlCommand(SQLStringEditSecurityUser))
                {
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_DisplayName", AD_DisplayName);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_FirstName", AD_FirstName);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_LastName", AD_LastName);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_EmployeeNumber", AD_EmployeeNumber);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Email", AD_Email);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_ManagerUserName", AD_ManagerUserName);
                  SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserId);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditSecurityUser);
                }
              }

              AD_UserName = "";
              AD_DisplayName = "";
              AD_FirstName = "";
              AD_LastName = "";
              AD_EmployeeNumber = "";
              AD_Email = "";
              AD_ManagerUserName = "";
              AD_Error = "";
            }
          }
          else
          {
            AD_Error = Convert.ToString("No Employee Data", CultureInfo.CurrentCulture);
          }
        }
      }

      if (string.IsNullOrEmpty(AD_Error))
      {
        Label_SearchErrorMessage.Text = Convert.ToString("", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_SearchErrorMessage.Text = AD_Error;
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    private void SecurityUserActive()
    {
      string SecurityUserId = "";
      string SecurityUserUserName = "";
      string SQLStringSecurityUser = "SELECT SecurityUser_Id,	SecurityUser_UserName FROM Administration_SecurityUser WHERE (CHARINDEX(@SecurityUser_UserName , SecurityUser_UserName) > 0 OR @SecurityUser_UserName IS NULL) AND (CHARINDEX(@SecurityUser_DisplayName , SecurityUser_DisplayName) > 0 OR @SecurityUser_DisplayName IS NULL) AND (CHARINDEX(@SecurityUser_EmployeeNumber , SecurityUser_EmployeeNumber) > 0 OR @SecurityUser_EmployeeNumber IS NULL)";
      using (SqlCommand SqlCommand_SecurityUser = new SqlCommand(SQLStringSecurityUser))
      {
        SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_UserName", Request.QueryString["s_SecurityAccess_Username"]);
        SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_DisplayName", Request.QueryString["s_SecurityAccess_DisplayName"]);
        SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_EmployeeNumber", Request.QueryString["s_SecurityAccess_EmployeeNumber"]);
        DataTable DataTable_SecurityUser;
        using (DataTable_SecurityUser = new DataTable())
        {
          DataTable_SecurityUser.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityUser = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUser).Copy();
          if (DataTable_SecurityUser.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_SecurityUser in DataTable_SecurityUser.Rows)
            {
              SecurityUserId = DataRow_Row_SecurityUser["SecurityUser_Id"].ToString();
              SecurityUserUserName = DataRow_Row_SecurityUser["SecurityUser_UserName"].ToString();

              string AD_UserName = "";
              string AD_Email = "";
              DataTable DataTable_AD;
              using (DataTable_AD = new DataTable())
              {
                DataTable_AD.Locale = CultureInfo.CurrentCulture;
                DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(SecurityUserUserName).Copy();
                if (DataTable_AD.Columns.Count == 1)
                {
                  foreach (DataRow DataRow_Row in DataTable_AD.Rows)
                  {
                    string Error = DataRow_Row["Error"].ToString();

                    if (Error == "No Employee Data")
                    {
                      string SQLStringEditSecurityUser = "UPDATE Administration_SecurityUser SET SecurityUser_IsActive = 0 WHERE SecurityUser_Id = @SecurityUser_Id";
                      using (SqlCommand SqlCommand_EditSecurityUser = new SqlCommand(SQLStringEditSecurityUser))
                      {
                        SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserId);
                        InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditSecurityUser);
                      }
                    }
                  }
                }
                else if (DataTable_AD.Columns.Count != 1)
                {
                  if (DataTable_AD.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_AD.Rows)
                    {
                      AD_UserName = DataRow_Row["UserName"].ToString();
                      AD_Email = DataRow_Row["Email"].ToString();

                      if (!string.IsNullOrEmpty(AD_UserName))
                      {
                        AD_UserName = "LHC\\" + AD_UserName;
                      }

                      string SecurityUserIdValid = "";
                      string SQLStringSecurityUserValid = "SELECT SecurityUser_Id FROM Administration_SecurityUser WHERE SecurityUser_Id = @SecurityUser_Id AND SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_Email = @SecurityUser_Email";
                      using (SqlCommand SqlCommand_SecurityUserValid = new SqlCommand(SQLStringSecurityUserValid))
                      {
                        SqlCommand_SecurityUserValid.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserId);
                        SqlCommand_SecurityUserValid.Parameters.AddWithValue("@SecurityUser_UserName", AD_UserName);
                        SqlCommand_SecurityUserValid.Parameters.AddWithValue("@SecurityUser_Email", AD_Email);
                        DataTable DataTable_SecurityUserValid;
                        using (DataTable_SecurityUserValid = new DataTable())
                        {
                          DataTable_SecurityUserValid.Locale = CultureInfo.CurrentCulture;
                          DataTable_SecurityUserValid = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUserValid).Copy();
                          if (DataTable_SecurityUserValid.Rows.Count > 0)
                          {
                            foreach (DataRow DataRow_Row_SecurityUserValid in DataTable_SecurityUserValid.Rows)
                            {
                              SecurityUserIdValid = DataRow_Row_SecurityUserValid["SecurityUser_Id"].ToString();

                              string SQLStringEditSecurityUser = "UPDATE Administration_SecurityUser SET SecurityUser_IsActive = 1 WHERE SecurityUser_Id = @SecurityUser_Id";
                              using (SqlCommand SqlCommand_EditSecurityUser = new SqlCommand(SQLStringEditSecurityUser))
                              {
                                SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserIdValid);
                                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditSecurityUser);
                              }
                            }
                          }
                          else
                          {
                            string SQLStringEditSecurityUser = "UPDATE Administration_SecurityUser SET SecurityUser_IsActive = 0 WHERE SecurityUser_Id = @SecurityUser_Id";
                            using (SqlCommand SqlCommand_EditSecurityUser = new SqlCommand(SQLStringEditSecurityUser))
                            {
                              SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserId);
                              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditSecurityUser);
                            }
                          }
                        }
                      }

                      AD_UserName = "";
                      AD_Email = "";
                    }
                  }
                  else
                  {
                    string SQLStringEditSecurityUser = "UPDATE Administration_SecurityUser SET SecurityUser_IsActive = 0 WHERE SecurityUser_Id = @SecurityUser_Id";
                    using (SqlCommand SqlCommand_EditSecurityUser = new SqlCommand(SQLStringEditSecurityUser))
                    {
                      SqlCommand_EditSecurityUser.Parameters.AddWithValue("@SecurityUser_Id", SecurityUserId);
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditSecurityUser);
                    }
                  }
                }
              }

              SecurityUserId = "";
              SecurityUserUserName = "";
            }
          }
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = Server.HtmlEncode(TextBox_UserName.Text);
      string SearchField2 = Server.HtmlEncode(TextBox_DisplayName.Text);
      string SearchField3 = Server.HtmlEncode(TextBox_EmployeeNumber.Text);

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityAccess_UserName=" + Server.HtmlEncode(TextBox_UserName.Text).ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityAccess_DisplayName=" + Server.HtmlEncode(TextBox_DisplayName.Text).ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityAccess_EmployeeNumber=" + Server.HtmlEncode(TextBox_EmployeeNumber.Text).ToString() + "&";
      }

      string FinalURL = "Form_ContactCentre_InfoQuestAccess.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", "Form_ContactCentre_InfoQuestAccess.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --SecurityUser List--//
    protected void SqlDataSource_SecurityUser_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_SecurityUser.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SecurityUser_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SecurityUser_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SecurityUser_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SecurityUser_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SecurityUser_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SecurityUser_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SecurityUser_List.PageSize <= 5)
        {
          DropDownList_PageSize.SelectedValue = "5";
        }
        else if (GridView_SecurityUser_List.PageSize <= 5 && GridView_SecurityUser_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SecurityUser_List.PageSize > 20 && GridView_SecurityUser_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SecurityUser_List.PageSize > 50 && GridView_SecurityUser_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_SecurityUser_List.Rows.Count; i++)
      {
        if (GridView_SecurityUser_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_SecurityUser_List.Rows[i].Cells[6].Text.ToString() == "Yes")
          {
            GridView_SecurityUser_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_SecurityUser_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_SecurityUser_List.Rows[i].Cells[6].Text.ToString() == "No")
          {
            GridView_SecurityUser_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_SecurityUser_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_SecurityUser_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_SecurityUser_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_SecurityUser_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_SecurityUser_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SecurityUser_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SecurityUser_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_SecurityUser_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object securityUser_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", "Form_ContactCentre_InfoQuestAccess.aspx?SecurityUser_Id=" + securityUser_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_SecurityAccess_UserName"];
      string SearchField2 = Request.QueryString["s_SecurityAccess_DisplayName"];
      string SearchField3 = Request.QueryString["s_SecurityAccess_EmployeeNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityAccess_UserName=" + Request.QueryString["s_SecurityAccess_UserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityAccess_DisplayName=" + Request.QueryString["s_SecurityAccess_DisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityAccess_EmployeeNumber=" + Request.QueryString["s_SecurityAccess_EmployeeNumber"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3;
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


    //--START-- --SecurityAccess List--//
    protected void SqlDataSource_SecurityAccess_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_SecurityAccess.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_SecurityAccess_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_SecurityAccess_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ClearUser_Click(object sender, EventArgs e)
    {
      string SearchField1 = Request.QueryString["s_SecurityAccess_UserName"];
      string SearchField2 = Request.QueryString["s_SecurityAccess_DisplayName"];
      string SearchField3 = Request.QueryString["s_SecurityAccess_EmployeeNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityAccess_UserName=" + Request.QueryString["s_SecurityAccess_UserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityAccess_DisplayName=" + Request.QueryString["s_SecurityAccess_DisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityAccess_EmployeeNumber=" + Request.QueryString["s_SecurityAccess_EmployeeNumber"] + "&";
      }

      string FinalURL = "Form_ContactCentre_InfoQuestAccess.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    public string GetLinkSecurityAccess(object securityUser_Id, object facility_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", "Form_ContactCentre_InfoQuestAccess.aspx?SecurityUser_Id=" + securityUser_Id + "&Facility_Id=" + facility_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_SecurityAccess_UserName"];
      string SearchField2 = Request.QueryString["s_SecurityAccess_DisplayName"];
      string SearchField3 = Request.QueryString["s_SecurityAccess_EmployeeNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityAccess_UserName=" + Request.QueryString["s_SecurityAccess_UserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityAccess_DisplayName=" + Request.QueryString["s_SecurityAccess_DisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityAccess_EmployeeNumber=" + Request.QueryString["s_SecurityAccess_EmployeeNumber"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3;
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
    //---END--- --SecurityAccess List--//


    //--START-- --SecurityAccess--//
    private void SecurityAccess()
    {
      string SecurityUserDisplayName = "";
      string SQLStringSecurityUser = "SELECT SecurityUser_DisplayName FROM Administration_SecurityUser WHERE SecurityUser_Id = @SecurityUser_Id";
      using (SqlCommand SqlCommand_SecurityUser = new SqlCommand(SQLStringSecurityUser))
      {
        SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
        DataTable DataTable_SecurityUser;
        using (DataTable_SecurityUser = new DataTable())
        {
          DataTable_SecurityUser.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityUser = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUser).Copy();
          if (DataTable_SecurityUser.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SecurityUser.Rows)
            {
              SecurityUserDisplayName = DataRow_Row["SecurityUser_DisplayName"].ToString();
            }
          }
        }
      }

      Label_SecurityUser.Text = SecurityUserDisplayName;

      if (Request.QueryString["Facility_Id"] != null)
      {
        DropDownList_Facility.SelectedValue = Request.QueryString["Facility_Id"];
        SqlDataSource_SecurityRole.SelectParameters["FacilityId"].DefaultValue = Request.QueryString["Facility_Id"];
      }

      Button_SecurityAccessAdd.Visible = false;
      Button_SecurityAccessDelete.Visible = false;
      Button_SecurityAccessUpdate.Visible = false;

      ((DropDownList)DropDownList_Facility).Attributes.Add("OnChange", "Validation_Form();");
      ((CheckBoxList)CheckBoxList_SecurityRole).Attributes.Add("OnClick", "Validation_Form();");
    }

    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckBoxList_SecurityRole.Items.Clear();
      SqlDataSource_SecurityRole.SelectParameters["FacilityId"].DefaultValue = DropDownList_Facility.SelectedValue;
      SqlDataSource_SecurityRole.DataBind();

      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue))
      {
        Button_SecurityAccessAdd.Visible = false;
        Button_SecurityAccessDelete.Visible = false;
        Button_SecurityAccessUpdate.Visible = false;
      }
    }

    protected void CheckBoxList_SecurityRole_DataBound(object sender, EventArgs e)
    {
      if (Request.QueryString["SecurityUser_Id"] != null && !string.IsNullOrEmpty(DropDownList_Facility.SelectedValue))
      {
        string AddSecurityRole = "Yes";

        for (int i = 0; i < CheckBoxList_SecurityRole.Items.Count; i++)
        {
          string SecurityRoleId = "";
          string SQLStringSecurityRoleId = "SELECT DISTINCT SecurityRole_Id FROM Administration_SecurityAccess WHERE SecurityUser_Id = @SecurityUser_Id AND Facility_Id = @Facility_Id AND SecurityRole_Id = @SecurityRole_Id AND SecurityRole_Id IN (SELECT SecurityRole_Id FROM Administration_SecurityRole WHERE SecurityRole_Rank > 1)";
          using (SqlCommand SqlCommand_SecurityRoleId = new SqlCommand(SQLStringSecurityRoleId))
          {
            SqlCommand_SecurityRoleId.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
            SqlCommand_SecurityRoleId.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
            SqlCommand_SecurityRoleId.Parameters.AddWithValue("@SecurityRole_Id", CheckBoxList_SecurityRole.Items[i].Value);
            DataTable DataTable_SecurityRoleId;
            using (DataTable_SecurityRoleId = new DataTable())
            {
              DataTable_SecurityRoleId.Locale = CultureInfo.CurrentCulture;
              DataTable_SecurityRoleId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRoleId).Copy();
              if (DataTable_SecurityRoleId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SecurityRoleId.Rows)
                {
                  SecurityRoleId = DataRow_Row["SecurityRole_Id"].ToString();
                  if (!string.IsNullOrEmpty(SecurityRoleId))
                  {
                    CheckBoxList_SecurityRole.Items[i].Selected = true;
                    AddSecurityRole = "No";
                  }
                }
              }
            }
          }
        }

        if (CheckBoxList_SecurityRole.Items.Count > 0)
        {
          if (AddSecurityRole == "Yes")
          {
            Button_SecurityAccessAdd.Visible = true;
            Button_SecurityAccessDelete.Visible = false;
            Button_SecurityAccessUpdate.Visible = false;
          }
          else
          {
            Button_SecurityAccessAdd.Visible = false;
            Button_SecurityAccessDelete.Visible = true;
            Button_SecurityAccessUpdate.Visible = true;
          }
        }
        else
        {
          Button_SecurityAccessAdd.Visible = false;
          Button_SecurityAccessDelete.Visible = false;
          Button_SecurityAccessUpdate.Visible = false;
        }
      }
    }

    protected void Button_SecurityAccessClear_Click(object sender, EventArgs e)
    {
      SecurityAccessClear();
    }

    protected void Button_SecurityAccessAdd_Click(object sender, EventArgs e)
    {
      string InvalidFormMessage = FormValidation();

      if (!string.IsNullOrEmpty(InvalidFormMessage))
      {
        ToolkitScriptManager_ContactCentre_InfoQuestAccess.SetFocus(UpdatePanel_ContactCentre_InfoQuestAccess);

        Label_InvalidFormMessage.Text = InvalidFormMessage;
      }
      else if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        for (int i = 0; i < CheckBoxList_SecurityRole.Items.Count; i++)
        {
          if (CheckBoxList_SecurityRole.Items[i].Selected == true)
          {
            string SQLStringInsertSecurityAccess = "INSERT INTO Administration_SecurityAccess ( SecurityUser_Id ,SecurityRole_Id ,Facility_Id ,SecurityAccess_CreatedDate ,SecurityAccess_CreatedBy ,SecurityAccess_ModifiedDate ,SecurityAccess_ModifiedBy ) VALUES ( @SecurityUser_Id ,@SecurityRole_Id ,@Facility_Id ,@SecurityAccess_CreatedDate ,@SecurityAccess_CreatedBy ,@SecurityAccess_ModifiedDate ,@SecurityAccess_ModifiedBy )";
            using (SqlCommand SqlCommand_InsertSecurityAccess = new SqlCommand(SQLStringInsertSecurityAccess))
            {
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityRole_Id", CheckBoxList_SecurityRole.Items[i].Value.ToString());
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_CreatedDate", DateTime.Now);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_ModifiedDate", DateTime.Now);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSecurityAccess);
            }
          }
        }

        SecurityAccessClear();
      }
    }

    protected void Button_SecurityAccessDelete_Click(object sender, EventArgs e)
    {
      //String InvalidFormMessage = FormValidation();
      string InvalidFormMessage = "";

      if (!string.IsNullOrEmpty(InvalidFormMessage))
      {
        ToolkitScriptManager_ContactCentre_InfoQuestAccess.SetFocus(UpdatePanel_ContactCentre_InfoQuestAccess);

        Label_InvalidFormMessage.Text = InvalidFormMessage;
      }
      else if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        string SQLStringDeleteSecurityAccess = "DELETE FROM Administration_SecurityAccess WHERE SecurityUser_Id = @SecurityUser_Id AND Facility_Id = @Facility_Id AND SecurityRole_Id IN (SELECT SecurityRole_Id FROM Administration_SecurityRole WHERE SecurityRole_Rank > 2)";
        using (SqlCommand SqlCommand_DeleteSecurityAccess = new SqlCommand(SQLStringDeleteSecurityAccess))
        {
          SqlCommand_DeleteSecurityAccess.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
          SqlCommand_DeleteSecurityAccess.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteSecurityAccess);
        }

        SecurityAccessClear();
      }
    }

    protected void Button_SecurityAccessUpdate_Click(object sender, EventArgs e)
    {
      string InvalidFormMessage = FormValidation();

      if (!string.IsNullOrEmpty(InvalidFormMessage))
      {
        ToolkitScriptManager_ContactCentre_InfoQuestAccess.SetFocus(UpdatePanel_ContactCentre_InfoQuestAccess);

        Label_InvalidFormMessage.Text = InvalidFormMessage;
      }
      else if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        string SQLStringDeleteSecurityAccess = "DELETE FROM Administration_SecurityAccess WHERE SecurityUser_Id = @SecurityUser_Id AND Facility_Id = @Facility_Id AND SecurityRole_Id IN (SELECT SecurityRole_Id FROM Administration_SecurityRole WHERE SecurityRole_Rank > 2)";
        using (SqlCommand SqlCommand_DeleteSecurityAccess = new SqlCommand(SQLStringDeleteSecurityAccess))
        {
          SqlCommand_DeleteSecurityAccess.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
          SqlCommand_DeleteSecurityAccess.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteSecurityAccess);
        }

        for (int i = 0; i < CheckBoxList_SecurityRole.Items.Count; i++)
        {
          if (CheckBoxList_SecurityRole.Items[i].Selected == true)
          {
            string SQLStringInsertSecurityAccess = "INSERT INTO Administration_SecurityAccess ( SecurityUser_Id ,SecurityRole_Id ,Facility_Id ,SecurityAccess_CreatedDate ,SecurityAccess_CreatedBy ,SecurityAccess_ModifiedDate ,SecurityAccess_ModifiedBy ) VALUES ( @SecurityUser_Id ,@SecurityRole_Id ,@Facility_Id ,@SecurityAccess_CreatedDate ,@SecurityAccess_CreatedBy ,@SecurityAccess_ModifiedDate ,@SecurityAccess_ModifiedBy )";
            using (SqlCommand SqlCommand_InsertSecurityAccess = new SqlCommand(SQLStringInsertSecurityAccess))
            {
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityRole_Id", CheckBoxList_SecurityRole.Items[i].Value.ToString());
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_CreatedDate", DateTime.Now);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_ModifiedDate", DateTime.Now);
              SqlCommand_InsertSecurityAccess.Parameters.AddWithValue("@SecurityAccess_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSecurityAccess);
            }
          }
        }

        SecurityAccessClear();
      }
    }


    protected string FormValidation()
    {
      string InvalidForm = "No";
      string MultipleSecurityRolePerForm = "No";
      string MultipleSecurityRolePerForm_LastForm = "";
      string MultipleSecurityRolePerForm_AdminForm = "";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        string SecurityRoleInvalid = "Yes";
        string LastForm = "";
        for (int i = 0; i < CheckBoxList_SecurityRole.Items.Count; i++)
        {
          if (CheckBoxList_SecurityRole.Items[i].Selected == true)
          {
            if (MultipleSecurityRolePerForm == "No")
            {
              string SecurityRoleText = CheckBoxList_SecurityRole.Items[i].Text;
              string[] SecurityRoleSplit = SecurityRoleText.Split(new string[] { " -- " }, StringSplitOptions.None);
              string CurrentForm = "";
              if (SecurityRoleSplit.Length > 1)
              {
                CurrentForm = SecurityRoleSplit[0];
              }

              if (!string.IsNullOrEmpty(CurrentForm))
              {
                string DatabaseAdminFormName = "";
                string SQLStringFormName = "SELECT DISTINCT '<strong>' + Form_Name + '</strong>' AS Form_Name FROM vAdministration_SecurityAccess_All WHERE SecurityUser_Id = @SecurityUser_Id AND Form_Id in ( SELECT Form_Id FROM Administration_Form WHERE Form_Name = @Form_Name ) AND SecurityRole_Rank IN (1,2) AND Facility_Id = @Facility_Id";
                using (SqlCommand SqlCommand_FormName = new SqlCommand(SQLStringFormName))
                {
                  SqlCommand_FormName.Parameters.AddWithValue("@SecurityUser_Id", Request.QueryString["SecurityUser_Id"]);
                  SqlCommand_FormName.Parameters.AddWithValue("@Form_Name", CurrentForm.Replace("<strong>", "").Replace("</strong>", "").ToString());
                  SqlCommand_FormName.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
                  DataTable DataTable_FormName;
                  using (DataTable_FormName = new DataTable())
                  {
                    DataTable_FormName.Locale = CultureInfo.CurrentCulture;
                    DataTable_FormName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormName).Copy();
                    if (DataTable_FormName.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row in DataTable_FormName.Rows)
                      {
                        DatabaseAdminFormName = DataRow_Row["Form_Name"].ToString();
                      }
                    }
                  }
                }

                if (CurrentForm == DatabaseAdminFormName)
                {
                  MultipleSecurityRolePerForm = "Yes";
                  MultipleSecurityRolePerForm_AdminForm = CurrentForm;
                }
                else
                {
                  if (string.Compare(LastForm, CurrentForm, StringComparison.CurrentCulture) == 0)
                  {
                    MultipleSecurityRolePerForm = "Yes";
                    MultipleSecurityRolePerForm_LastForm = LastForm;
                  }
                }
              }

              LastForm = CurrentForm;

              SecurityRoleInvalid = "No";
            }
          }
        }

        if (SecurityRoleInvalid == "Yes")
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (MultipleSecurityRolePerForm == "Yes")
      {
        if (!string.IsNullOrEmpty(MultipleSecurityRolePerForm_LastForm))
        {
          InvalidFormMessage = "Only one Security Role can be selected per " + MultipleSecurityRolePerForm_LastForm + "";
        }

        if (!string.IsNullOrEmpty(MultipleSecurityRolePerForm_AdminForm))
        {
          InvalidFormMessage = "Only one Security Role can be selected per " + MultipleSecurityRolePerForm_AdminForm + ", an Administrator Security Role has already been selected for this form, please refer to the List of Current User Security Access at the bottom of page";
        }
      }

      return InvalidFormMessage;
    }

    private void SecurityAccessClear()
    {
      string SearchField1 = Request.QueryString["SecurityUser_Id"];
      string SearchField2 = Request.QueryString["s_SecurityAccess_UserName"];
      string SearchField3 = Request.QueryString["s_SecurityAccess_DisplayName"];
      string SearchField4 = Request.QueryString["s_SecurityAccess_EmployeeNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "SecurityUser_Id=" + Request.QueryString["SecurityUser_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityAccess_UserName=" + Request.QueryString["s_SecurityAccess_UserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityAccess_DisplayName=" + Request.QueryString["s_SecurityAccess_DisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_SecurityAccess_EmployeeNumber=" + Request.QueryString["s_SecurityAccess_EmployeeNumber"] + "&";
      }

      string FinalURL = "Form_ContactCentre_InfoQuestAccess.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre InfoQuest Access", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --SecurityAccess--//


    //--START-- --CurrentAccess List--//
    protected void SqlDataSource_CurrentAccess_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_CurrentAccess.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_CurrentAccess_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_CurrentAccess_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --CurrentAccess List--//
  }
}