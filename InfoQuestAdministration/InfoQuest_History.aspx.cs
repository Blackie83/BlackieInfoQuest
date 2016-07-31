using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class InfoQuest_History : InfoQuestWCF.Override_SystemWebUIPage
  {
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
        Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Administration_HistorySplit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);


          SqlDataSource_Administration_HistorySplit.SelectParameters["StringToSplit"].DefaultValue = "[START][Facilities_Id=1],[Facilities_FacilityName=Pretoria Urology Hospital],[Facilities_FacilityCode=05],[Facilities_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_CreatedDate=Oct  1 2009  8:00AM],[Facilities_CreatedBy=LHC\\swartj2],[Facilities_ModifiedDate=Dec  9 2009  6:40AM],[Facilities_ModifiedBy=LHC\\swartj2],[Facilities_IsActive=1][END] " +
              "[START][Facilities_Id=1],[Facilities_FacilityName=Pretoria Urology Hospital],[Facilities_FacilityCode=05],[Facilities_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_CreatedDate=Oct  1 2009  8:00AM],[Facilities_CreatedBy=LHC\\swartj2],[Facilities_ModifiedDate=Dec  9 2009  6:40AM],[Facilities_ModifiedBy=LHC\\swartj2],[Facilities_IsActive=1][END]" +
              "[START][Facilities_Id=1],[Facilities_FacilityName=Pretoria Urology Hospital],[Facilities_FacilityCode=05],[Facilities_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_CreatedDate=Oct  1 2009  8:00AM],[Facilities_CreatedBy=LHC\\swartj2],[Facilities_ModifiedDate=Dec  9 2009  6:40AM],[Facilities_ModifiedBy=LHC\\swartj2],[Facilities_IsActive=1][END]" +
              "[START][Facilities_Id=1],[Facilities_FacilityName=Pretoria Urology Hospital],[Facilities_FacilityCode=05],[Facilities_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_CreatedDate=Oct  1 2009  8:00AM],[Facilities_CreatedBy=LHC\\swartj2],[Facilities_ModifiedDate=Dec  9 2009  6:40AM],[Facilities_ModifiedBy=LHC\\swartj2],[Facilities_IsActive=1][END]" +
              "[START][Facilities_Id=1],[Facilities_FacilityName=Pretoria Urology Hospital],[Facilities_FacilityCode=05],[Facilities_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facilities_CreatedDate=Oct  1 2009  8:00AM],[Facilities_CreatedBy=LHC\\swartj2],[Facilities_ModifiedDate=Dec  9 2009  6:40AM],[Facilities_ModifiedBy=LHC\\swartj2],[Facilities_IsActive=1][END]";
        }
      }
    }

    protected void GridView_Administration_HistorySplit_RowCreated(object sender, GridViewRowEventArgs e)
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
  }
}