using System;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_AAA_Test : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (true)
      {


      }


      try
      {

      }
      catch (Exception)
      {

        throw;
      }

      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          //Body_AAA_Test.Attributes.Add("onload", "PageLoad_ShowPage();");

          //string script = "$(document).ready(function () { $('[id*=Button_TestServerPost]').click(); });";

          //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

          //Label_Title.Text = Convert.ToString("AAA_Test", CultureInfo.CurrentCulture);
          //Label_PCName.Text = GetPCName();
          //Label_PCIP.Text = GetPCIP();
          //Label_ServerIP.Text = GetServerIP();

          //Body_AAA_Test.Attributes.Add("OnLoad", "Validation_Time();Validation_Date();Validation_DateTime();");
          //((DropDownList)((Controls_TimePicker)Page.FindControl("ControlsTimePicker_SelectTime")).FindControl("DropDownList_Hour")).Attributes.Add("OnChange", "Validation_Time();");
          //((DropDownList)((Controls_TimePicker)Page.FindControl("ControlsTimePicker_SelectTime")).FindControl("DropDownList_Minute")).Attributes.Add("OnChange", "Validation_Time();");

          //((DropDownList)((Controls_DatePicker)Page.FindControl("ControlsDatePicker_SelectDate")).FindControl("DropDownList_Year")).Attributes.Add("OnChange", "Validation_Date();");
          //((DropDownList)((Controls_DatePicker)Page.FindControl("ControlsDatePicker_SelectDate")).FindControl("DropDownList_Month")).Attributes.Add("OnChange", "Validation_Date();");
          //((DropDownList)((Controls_DatePicker)Page.FindControl("ControlsDatePicker_SelectDate")).FindControl("DropDownList_Day")).Attributes.Add("OnChange", "Validation_Date();");

          //((DropDownList)((Controls_DateTimePicker)Page.FindControl("ControlsDateTimePicker_SelectDateTime")).FindControl("DropDownList_Year")).Attributes.Add("OnChange", "Validation_DateTime();");
          //((DropDownList)((Controls_DateTimePicker)Page.FindControl("ControlsDateTimePicker_SelectDateTime")).FindControl("DropDownList_Month")).Attributes.Add("OnChange", "Validation_DateTime();");
          //((DropDownList)((Controls_DateTimePicker)Page.FindControl("ControlsDateTimePicker_SelectDateTime")).FindControl("DropDownList_Day")).Attributes.Add("OnChange", "Validation_DateTime();");
          //((DropDownList)((Controls_DateTimePicker)Page.FindControl("ControlsDateTimePicker_SelectDateTime")).FindControl("DropDownList_Hour")).Attributes.Add("OnChange", "Validation_DateTime();");
          //((DropDownList)((Controls_DateTimePicker)Page.FindControl("ControlsDateTimePicker_SelectDateTime")).FindControl("DropDownList_Minute")).Attributes.Add("OnChange", "Validation_DateTime();");



          //RegisterPostBackControl();
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
        //((Label)PageUpdateProgress_AAA_Test.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        //NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
      }
    }


    protected void Button_TestServerPost_Click(object sender, EventArgs e)
    {
      System.Threading.Thread.Sleep(5000);
    }

    //protected void RegisterPostBackControl()
    //{
    //  ScriptManager ScriptManager_RegisterPostBackControl = ScriptManager.GetCurrent(Page);

    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_ClearTime);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_SetTime);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_GetTime);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_ClearDate);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_SetDate);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_GetDate);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_ClearDateTime);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_SetDateTime);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_GetDateTime);

    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_Hide);
    //  ScriptManager_RegisterPostBackControl.RegisterPostBackControl(Button_Show);
    //}


    //private static string GetPCName()
    //{
    //  return Dns.GetHostEntry("").HostName;
    //}

    //private static string GetPCIP()
    //{
    //  string PCIP = string.Empty;
    //  IPAddress[] IPAddress_IPs = Dns.GetHostAddresses(Dns.GetHostName());
    //  foreach (IPAddress IPAddress_IP in IPAddress_IPs)
    //  {
    //    PCIP = PCIP + IPAddress_IP + "<br/>";
    //  }

    //  return PCIP;
    //}

    //private static string GetServerIP()
    //{
    //  IPHostEntry IPHostEntry_Info = Dns.GetHostEntry(Dns.GetHostName());
    //  IPAddress IPAddress_Info = IPHostEntry_Info.AddressList[0];

    //  return IPAddress_Info.ToString();
    //}


    //protected void Button_ClearTime_Click(object sender, EventArgs e)
    //{
    //  ControlsTimePicker_SelectTime.SetGetHour = "";
    //  ControlsTimePicker_SelectTime.SetGetMinute = "";

    //  Label_Time.Text = Convert.ToString("Hour:Minute", CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}

    //protected void Button_SetTime_Click(object sender, EventArgs e)
    //{
    //  ControlsTimePicker_SelectTime.SetGetHour = "21";
    //  ControlsTimePicker_SelectTime.SetGetMinute = "23";

    //  RegisterPostBackControl();
    //}

    //protected void Button_GetTime_Click(object sender, EventArgs e)
    //{
    //  Label_Time.Text = Convert.ToString(ControlsTimePicker_SelectTime.SetGetHour.ToString() + ":" + ControlsTimePicker_SelectTime.SetGetMinute.ToString(), CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}

    //protected void Button_ClearDate_Click(object sender, EventArgs e)
    //{
    //  ControlsDatePicker_SelectDate.SetGetYear = "";
    //  ControlsDatePicker_SelectDate.SetGetMonth = "";
    //  ControlsDatePicker_SelectDate.SetGetDay = "";

    //  Label_Date.Text = Convert.ToString("Year/Month/Day", CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}

    //protected void Button_SetDate_Click(object sender, EventArgs e)
    //{
    //  ControlsDatePicker_SelectDate.SetGetYear = "2016";
    //  ControlsDatePicker_SelectDate.SetGetMonth = "06";
    //  ControlsDatePicker_SelectDate.SetGetDay = "25";

    //  RegisterPostBackControl();
    //}

    //protected void Button_GetDate_Click(object sender, EventArgs e)
    //{
    //  Label_Date.Text = Convert.ToString(ControlsDatePicker_SelectDate.SetGetYear.ToString() + "/" + ControlsDatePicker_SelectDate.SetGetMonth.ToString() + "/" + ControlsDatePicker_SelectDate.SetGetDay.ToString(), CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}

    //protected void Button_ClearDateTime_Click(object sender, EventArgs e)
    //{
    //  ControlsDateTimePicker_SelectDateTime.SetGetYear = "";
    //  ControlsDateTimePicker_SelectDateTime.SetGetMonth = "";
    //  ControlsDateTimePicker_SelectDateTime.SetGetDay = "";
    //  ControlsDateTimePicker_SelectDateTime.SetGetHour = "";
    //  ControlsDateTimePicker_SelectDateTime.SetGetMinute = "";

    //  Label_DateTime.Text = Convert.ToString("Year/Month/Day Hour:Minute", CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}

    //protected void Button_SetDateTime_Click(object sender, EventArgs e)
    //{
    //  ControlsDateTimePicker_SelectDateTime.SetGetYear = "2016";
    //  ControlsDateTimePicker_SelectDateTime.SetGetMonth = "06";
    //  ControlsDateTimePicker_SelectDateTime.SetGetDay = "25";
    //  ControlsDateTimePicker_SelectDateTime.SetGetHour = "21";
    //  ControlsDateTimePicker_SelectDateTime.SetGetMinute = "23";

    //  RegisterPostBackControl();
    //}

    //protected void Button_GetDateTime_Click(object sender, EventArgs e)
    //{
    //  Label_DateTime.Text = Convert.ToString(ControlsDateTimePicker_SelectDateTime.SetGetYear.ToString() + "/" + ControlsDateTimePicker_SelectDateTime.SetGetMonth.ToString() + "/" + ControlsDateTimePicker_SelectDateTime.SetGetDay.ToString() + " " + ControlsDateTimePicker_SelectDateTime.SetGetHour.ToString() + ":" + ControlsDateTimePicker_SelectDateTime.SetGetMinute.ToString(), CultureInfo.CurrentCulture);

    //  RegisterPostBackControl();
    //}


    //protected void Button_Hide_Click(object sender, EventArgs e)
    //{
    //  RegisterPostBackControl();
    //}

    //protected void Button_Show_Click(object sender, EventArgs e)
    //{
    //  RegisterPostBackControl();
    //}
  }
}