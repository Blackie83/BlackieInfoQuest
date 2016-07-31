using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace InfoQuestWCF
{
  public static class InfoQuest_WCF
  {
    public static void WCF_SendEmail_Error(string method, string message)
    {
      if (message != null)
      {
        message = message.Replace("\n", "<br/>");
        string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("57");

        WCF_SendEmail_EmailTo(method, message, EmailTemplate);

        WCF_SendEmail_EmailNotification(method, message, EmailTemplate);
      }
    }

    public static void WCF_SendEmail_Successful(string method, string message)
    {
      if (message != null)
      {
        message = message.Replace("\n", "<br/>");
        string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("58");

        WCF_SendEmail_EmailTo(method, message, EmailTemplate);

        WCF_SendEmail_EmailNotification(method, message, EmailTemplate);
      }
    }

    public static void WCF_SendEmail_EmailTo(string method, string message, string emailTemplate)
    {
      if (!string.IsNullOrEmpty(emailTemplate))
      {
        string HeaderString = InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuest_All.All_EmailFooter();

        string SQLStringEmailTo = "SELECT SecurityUser_Email , SecurityUser_DisplayName FROM ( SELECT SystemAdministrator_Domain + '\\' + SystemAdministrator_UserName AS SystemAdministrator FROM Administration_SystemAdministrator WHERE SystemAdministrator_IsActive = 1 ) AS TempTable LEFT JOIN Administration_SecurityUser ON TempTable.SystemAdministrator = Administration_SecurityUser.SecurityUser_UserName";
        using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
        {
          DataTable DataTable_EmailTo;
          using (DataTable_EmailTo = new DataTable())
          {
            DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailTo = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
            if (DataTable_EmailTo.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_EmailTo in DataTable_EmailTo.Rows)
              {
                string SecurityUserEmail = DataRow_Row_EmailTo["SecurityUser_Email"].ToString();
                string SecurityUserDisplayName = DataRow_Row_EmailTo["SecurityUser_DisplayName"].ToString();

                string BodyString = emailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + SecurityUserDisplayName + "");
                BodyString = BodyString.Replace(";replace;Method;replace;", "" + method + "");
                BodyString = BodyString.Replace(";replace;Message;replace;", "" + message + "");

                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", SecurityUserEmail, "WCF: " + method, EmailBody);

                if (string.IsNullOrEmpty(EmailSend) || !string.IsNullOrEmpty(EmailSend))
                {
                  EmailBody = "";
                }

                EmailSend = "";
                SecurityUserDisplayName = "";
                SecurityUserEmail = "";
              }
            }
          }
        }
      }
    }

    public static void WCF_SendEmail_EmailNotification(string method, string message, string emailTemplate)
    {
      if (!string.IsNullOrEmpty(emailTemplate))
      {
        string HeaderString = InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuest_All.All_EmailFooter();

        string SQLStringEmailNotification = "SELECT EmailNotification_Email FROM Administration_EmailNotification WHERE EmailNotification_Assembly = @EmailNotification_Assembly AND EmailNotification_Method = @EmailNotification_Method AND EmailNotification_IsActive = 1";
        using (SqlCommand SqlCommand_EmailNotification = new SqlCommand(SQLStringEmailNotification))
        {
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Assembly", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString());
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Method", method);
          DataTable DataTable_EmailNotification;
          using (DataTable_EmailNotification = new DataTable())
          {
            DataTable_EmailNotification.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailNotification = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailNotification).Copy();
            if (DataTable_EmailNotification.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_FacilityId in DataTable_EmailNotification.Rows)
              {
                string EmailNotificationEmail = DataRow_Row_FacilityId["EmailNotification_Email"].ToString();

                string BodyString = emailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailNotificationEmail + "");
                BodyString = BodyString.Replace(";replace;Method;replace;", "" + method + "");
                BodyString = BodyString.Replace(";replace;Message;replace;", "" + message + "");

                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailNotificationEmail, "WCF: " + method, EmailBody);

                if (string.IsNullOrEmpty(EmailSend) || !string.IsNullOrEmpty(EmailSend))
                {
                  EmailBody = "";
                }

                EmailSend = "";
                EmailNotificationEmail = "";
              }
            }
          }
        }
      }
    }
  }
}