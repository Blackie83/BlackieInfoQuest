using System;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_Index : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        if (Request.QueryString["IndexParentValue"] == null)
        {
          Response.Write("<frameset framespacing='0' border='0' frameborder='0' rows='25,*'>" +
                           "<frame name='IndexHeader' scrolling='no' noresize='' target='IndexBody' src='InfoQuest_TopMenu.aspx'>" +
                           "<frame name='IndexBody' scrolling='Auto' noresize='' target='_self' src='InfoQuest_PageText.aspx?PageTextValue=5'>" +
                         "</frameset>");
        }
        else
        {
          string IndexBodyPage = "";
          string SQLStringHomePage = "SELECT DISTINCT Navigation_URL FROM vControls_Navigation_MenuItems WHERE Navigation_SortOrder = 1 AND Navigation_Parent = @IndexParentValue";
          using (SqlCommand SqlCommand_HomePage = new SqlCommand(SQLStringHomePage))
          {
            SqlCommand_HomePage.Parameters.AddWithValue("@IndexParentValue", Request.QueryString["IndexParentValue"]);
            DataTable DataTable_HomePage;
            using (DataTable_HomePage = new DataTable())
            {
              DataTable_HomePage.Locale = CultureInfo.CurrentCulture;
              DataTable_HomePage = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HomePage).Copy();
              if (DataTable_HomePage.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_HomePage.Rows)
                {
                  IndexBodyPage = DataRow_Row["Navigation_URL"].ToString();
                }
              }
              else
              {
                IndexBodyPage = "";
              }
            }
          }

          if (Request.QueryString["IndexItemValue"] == null)
          {
            Response.Write("<frameset framespacing='0' border='0' frameborder='0' rows='44,*'>" +
                             "<frame name='IndexHeader' scrolling='no' noresize='' target='IndexBody' src='InfoQuest_TopMenu.aspx?TopMenuParentValue=" + Request.QueryString["IndexParentValue"] + "'>" +
                             "<frame name='IndexBody' scrolling='Auto' noresize='' target='_self' src='InfoQuest_PageLoading.htm?PageLoadingPage=Page&PageLoadingURL=" + IndexBodyPage + "'>" +
                           "</frameset>");
          }
          else
          {
            Response.Write("<frameset framespacing='0' border='0' frameborder='0' rows='44,*'>" +
                             "<frame name='IndexHeader' scrolling='no' noresize='' target='IndexBody' src='InfoQuest_TopMenu.aspx?TopMenuParentValue=" + Request.QueryString["IndexParentValue"] + "&TopMenuItemValue=" + Request.QueryString["IndexItemValue"] + "'>" +
                             "<frame name='IndexBody' scrolling='Auto' noresize='' target='_self' src='InfoQuest_PageLoading.htm?PageLoadingPage=Page&PageLoadingURL=" + IndexBodyPage + "'>" +
                           "</frameset>");
          }

          IndexBodyPage = "";
        }
      }
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }
  }
}