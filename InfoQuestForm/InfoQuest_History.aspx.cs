using System;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_History : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_InfoQuest_History.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);


          SqlDataSource_InfoQuest_History.SelectParameters["StringToSplit"].DefaultValue = "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END] " +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]" +
              "[START][Facility_Id=1],[Facility_FacilityName=Pretoria Urology Hospital],[Facility_FacilityCode=05],[Facility_IMEDS_ConnectionStringProduction=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_IMEDS_ConnectionStringDevelopment=SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = urol)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = urol))); uid = ahs; pwd = ahs;],[Facility_CreatedDate=Oct  1 2009  8:00AM],[Facility_CreatedBy=LHC\\swartj2],[Facility_ModifiedDate=Dec  9 2009  6:40AM],[Facility_ModifiedBy=LHC\\swartj2],[Facility_IsActive=1][END]";
        }
      }
    }

    private static string PageSecurity()
    {
      //String SecurityAllow = "0";
      string SecurityAllow = "1";

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
        ((Label)PageUpdateProgress_History.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }


    protected void GridView_InfoQuest_History_RowCreated(object sender, GridViewRowEventArgs e)
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