using System;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_TopMenu : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

      Menu();
    }

    protected void Menu()
    {
      string MenuContentValue = "";

      if (Request.QueryString["TopMenuParentValue"] == null)
      {
        MenuContentValue = Convert.ToString("<div><table border='0' cellspacing='0' cellpadding='0'><tr></tr></table></div>", CultureInfo.CurrentCulture);

        MenuContent.Text = "" + MenuContentValue + "";
      }
      else
      {
        if (Request.QueryString["TopMenuItemValue"] == null)
        {
          TopMenuItemValue_Empty(MenuContentValue);
        }
        else
        {
          TopMenuItemValue_NotEmpty(MenuContentValue);
        }
      }

      MenuContentValue = "";
    }

    protected void TopMenuItemValue_Empty(string menuContentValue)
    {
      int CountChild = 0;
      string NavigationId;
      string NavigationName;
      string NavigationURL;
      string NavigationTargetFrame;

      string SQLStringNavigation = "SELECT * FROM (SELECT Navigation_Id, Navigation_Name, Navigation_Url, Navigation_Parent, Navigation_Name_Parent, Navigation_TargetFrame, Navigation_SortOrder, Navigation_ShowToAll FROM vControls_Navigation_MenuItems WHERE Navigation_Parent = @TopMenuParentValue and SecurityUsers_Username = @SecurityUser_Username UNION SELECT DISTINCT Navigation_Id, Navigation_Name, Navigation_Url, Navigation_Parent, Navigation_Name_Parent, Navigation_TargetFrame, Navigation_SortOrder,Navigation_ShowToAll FROM vControls_Navigation_SecurityRoles_Viewpage WHERE Navigation_Parent = @TopMenuParentValue AND Navigation_ShowToAll = '1' OR Navigation_Id IN (SELECT DISTINCT  Navigation_Parent FROM vControls_Navigation_SecurityRoles_ViewPage WHERE Navigation_ShowToAll = '1' AND Navigation_Parent <> @TopMenuParentValue)) AS NewTable ORDER BY Navigation_Parent, Navigation_SortOrder";
      using (SqlCommand SqlCommand_Navigation = new SqlCommand(SQLStringNavigation))
      {
        SqlCommand_Navigation.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_Navigation.Parameters.AddWithValue("@TopMenuParentValue", Request.QueryString["TopMenuParentValue"]);
        DataTable DataTable_Navigation;
        using (DataTable_Navigation = new DataTable())
        {
          DataTable_Navigation.Locale = CultureInfo.CurrentCulture;
          DataTable_Navigation = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Navigation).Copy();
          if (DataTable_Navigation.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Navigation.Rows)
            {
              NavigationId = DataRow_Row["Navigation_Id"].ToString();
              NavigationName = DataRow_Row["Navigation_Name"].ToString();
              NavigationURL = "InfoQuest_PageLoading.htm?PageLoadingPage=" + DataRow_Row["Navigation_Name"].ToString() + "&PageLoadingURL=" + DataRow_Row["Navigation_URL"].ToString();
              NavigationTargetFrame = DataRow_Row["Navigation_TargetFrame"].ToString();

              string SQLStringTotalItems = "SELECT COUNT('SELECT COUNT(Navigation_Name) AS CountChild FROM vControls_Navigation_MenuItems WHERE Navigation_Parent = ''@NavigationId'' and SecurityUsers_Username = ''@SecurityUser_Username'' UNION SELECT COUNT(Navigation_Name) AS TotalItems FROM vControls_Navigation_SecurityRoles_Viewpage WHERE Navigation_Parent=''@NavigationId'' and Navigation_ShowToAll = ''1'' and Forms_Id IS NOT null') AS CountChild";
              using (SqlCommand SqlCommand_TotalItems = new SqlCommand(SQLStringTotalItems))
              {
                SqlCommand_TotalItems.Parameters.AddWithValue("@NavigationId", NavigationId);
                SqlCommand_TotalItems.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
                DataTable DataTable_TotalItems;
                using (DataTable_TotalItems = new DataTable())
                {
                  DataTable_TotalItems.Locale = CultureInfo.CurrentCulture;
                  DataTable_TotalItems = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TotalItems).Copy();
                  if (DataTable_TotalItems.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_RowNext in DataTable_TotalItems.Rows)
                    {
                      CountChild = Int32.Parse(DataRow_RowNext["CountChild"].ToString(), CultureInfo.CurrentCulture);
                    }
                  }
                  else
                  {
                    CountChild = 0;
                  }
                }
              }

              if (CountChild == 0)
              {
                if (string.IsNullOrEmpty(NavigationURL))
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"Form_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"Portal_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
                else
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
              }
              else
              {
                if (string.IsNullOrEmpty(NavigationURL))
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"Form_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"Portal_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
                else
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
              }
            }
          }
        }
      }

      menuContentValue = Convert.ToString("<div><table border='0' cellspacing='0' cellpadding='0'><tr>" + menuContentValue + "</tr></table></div>", CultureInfo.CurrentCulture);

      MenuContent.Text = "" + menuContentValue + "";
    }

    protected void TopMenuItemValue_NotEmpty(string menuContentValue)
    {
      int CountChild = 0;
      string NavigationId;
      string NavigationName;
      string NavigationURL;
      string NavigationTargetFrame;

      string SQLStringNavigation = "SELECT * FROM (SELECT Navigation_Id, Navigation_Name, Navigation_Url, Navigation_Parent, Navigation_Name_Parent, Navigation_TargetFrame, Navigation_SortOrder, Navigation_ShowToAll FROM vControls_Navigation_MenuItems WHERE Navigation_Parent = @TopMenuParentValue and SecurityUsers_Username = @SecurityUser_Username UNION SELECT DISTINCT Navigation_Id, Navigation_Name, Navigation_Url, Navigation_Parent, Navigation_Name_Parent, Navigation_TargetFrame, Navigation_SortOrder,Navigation_ShowToAll FROM vControls_Navigation_SecurityRoles_Viewpage WHERE Navigation_Parent = @TopMenuParentValue AND Navigation_ShowToAll = '1' OR Navigation_Id IN (SELECT DISTINCT  Navigation_Parent FROM vControls_Navigation_SecurityRoles_ViewPage WHERE Navigation_ShowToAll = '1' AND Navigation_Parent <> @TopMenuParentValue)) AS NewTable ORDER BY Navigation_Parent, Navigation_SortOrder";
      using (SqlCommand SqlCommand_Navigation = new SqlCommand(SQLStringNavigation))
      {
        SqlCommand_Navigation.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_Navigation.Parameters.AddWithValue("@TopMenuParentValue", Request.QueryString["TopMenuParentValue"]);
        DataTable DataTable_Navigation;
        using (DataTable_Navigation = new DataTable())
        {
          DataTable_Navigation.Locale = CultureInfo.CurrentCulture;
          DataTable_Navigation = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Navigation).Copy();
          if (DataTable_Navigation.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Navigation.Rows)
            {
              NavigationId = DataRow_Row["Navigation_Id"].ToString();
              NavigationName = DataRow_Row["Navigation_Name"].ToString();
              NavigationURL = DataRow_Row["Navigation_URL"].ToString();
              NavigationTargetFrame = DataRow_Row["Navigation_TargetFrame"].ToString();

              if (NavigationURL.Substring(NavigationURL.Length - 1) == "=")
              {
                NavigationURL = "InfoQuest_PageLoading.htm?PageLoadingPage=" + NavigationName + "&PageLoadingURL=" + NavigationURL + Request.QueryString["TopMenuItemValue"];
              }
              else
              {
                NavigationURL = "InfoQuest_PageLoading.htm?PageLoadingPage=" + NavigationName + "&PageLoadingURL=" + NavigationURL;
              }


              string SQLStringTotalItems = "SELECT COUNT('SELECT COUNT(Navigation_Name) AS CountChild FROM vControls_Navigation_MenuItems WHERE Navigation_Parent=''@NavigationId'' and SecurityUsers_Username = ''@SecurityUser_Username'' UNION SELECT COUNT(Navigation_Name) AS TotalItems FROM vControls_Navigation_SecurityRoles_Viewpage WHERE Navigation_Parent=''@NavigationId'' and Navigation_ShowToAll = ''1'' and Forms_Id IS NOT null') AS CountChild";
              using (SqlCommand SqlCommand_TotalItems = new SqlCommand(SQLStringTotalItems))
              {
                SqlCommand_TotalItems.Parameters.AddWithValue("@NavigationId", NavigationId);
                SqlCommand_TotalItems.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
                DataTable DataTable_TotalItems;
                using (DataTable_TotalItems = new DataTable())
                {
                  DataTable_TotalItems.Locale = CultureInfo.CurrentCulture;
                  DataTable_TotalItems = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TotalItems).Copy();
                  if (DataTable_TotalItems.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_RowNext in DataTable_TotalItems.Rows)
                    {
                      CountChild = Int32.Parse(DataRow_RowNext["CountChild"].ToString(), CultureInfo.CurrentCulture);
                    }
                  }
                  else
                  {
                    CountChild = 0;
                  }
                }
              }

              if (CountChild == '0')
              {
                if (string.IsNullOrEmpty(NavigationURL))
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"Form_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"Portal_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
                else
                {
                  string ValueId = NavigationURL.Substring(NavigationURL.Length - 4, 4);
                  if (ValueId == "Value=")
                  {
                    NavigationURL = NavigationURL + "" + Request.QueryString["TopMenuItemValue"];
                  }

                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
              }
              else
              {
                if (string.IsNullOrEmpty(NavigationURL))
                {
                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"Form_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"Portal_PageText.aspx?Id=6\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
                else
                {
                  string ValueId = NavigationURL.Substring(NavigationURL.Length - 4, 4);
                  if (ValueId == "Value=")
                  {
                    NavigationURL = NavigationURL + "" + Request.QueryString["TopMenuItemValue"];
                  }

                  if (string.IsNullOrEmpty(menuContentValue))
                  {
                    menuContentValue = "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                  else
                  {
                    menuContentValue = menuContentValue + "<td nowrap=\"nowrap\"><font face='Verdana' style='font-size: 8pt' color=white><strong>&nbsp;&nbsp;|&nbsp;&nbsp;</strong></font></td>" + "<td nowrap=\"nowrap\"><a href=\"" + NavigationURL + "\" target='" + NavigationTargetFrame + "'>" + NavigationName + "</a></td>";
                  }
                }
              }
            }
          }

        }
      }

      menuContentValue = Convert.ToString("<div><table border='0' cellspacing='0' cellpadding='0'><tr>" + menuContentValue + "</tr></table></div>", CultureInfo.CurrentCulture);

      MenuContent.Text = "" + menuContentValue + "";
    }
  }
}