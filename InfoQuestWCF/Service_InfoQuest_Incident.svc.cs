using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_Incident
  {
    public string Incident_PendingApprovalNotifications_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler.Clear();

        try
        {
          string EmailList_SecurityUserEmail = "";
          string EmailList_SendNotification = "";
          string SQLStringEmailList = "EXECUTE spForm_Execute_Incident_PendingApprovalNotifications_EmailList";
          using (SqlCommand SqlCommand_EmailList = new SqlCommand(SQLStringEmailList))
          {
            DataTable DataTable_EmailList;
            using (DataTable_EmailList = new DataTable())
            {
              DataTable_EmailList.Locale = CultureInfo.CurrentCulture;
              DataTable_EmailList = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailList).Copy();
              if (DataTable_EmailList.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row_EmailList in DataTable_EmailList.Rows)
                {
                  EmailList_SecurityUserEmail = DataRow_Row_EmailList["SecurityUser_Email"].ToString();
                  EmailList_SendNotification = DataRow_Row_EmailList["SendNotification"].ToString();


                  string Notification = "";
                  string Notification_FormName = "";
                  string Notification_SecurityUserDisplayName = "";
                  string Notification_SecurityUserEmail = "";
                  string SQLStringNotification = "EXECUTE spForm_Execute_Incident_PendingApprovalNotifications_Notification @SecurityUserEmail , @SendNotification";
                  using (SqlCommand SqlCommand_Notification = new SqlCommand(SQLStringNotification))
                  {
                    SqlCommand_Notification.Parameters.AddWithValue("@SecurityUserEmail", EmailList_SecurityUserEmail);
                    SqlCommand_Notification.Parameters.AddWithValue("@SendNotification", EmailList_SendNotification);
                    DataTable DataTable_Notification;
                    using (DataTable_Notification = new DataTable())
                    {
                      DataTable_Notification.Locale = CultureInfo.CurrentCulture;
                      DataTable_Notification = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Notification).Copy();
                      if (DataTable_Notification.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row_Notification in DataTable_Notification.Rows)
                        {
                          Notification = Notification + DataRow_Row_Notification["Facility_FacilityDisplayName"].ToString() + " - " + DataRow_Row_Notification["PendingApprovalCount"].ToString() + "<br />";
                          Notification_FormName = DataRow_Row_Notification["Form_Name"].ToString();
                          Notification_SecurityUserDisplayName = DataRow_Row_Notification["SecurityUser_DisplayName"].ToString();
                          Notification_SecurityUserEmail = DataRow_Row_Notification["SecurityUser_Email"].ToString();
                        }
                      }
                      else
                      {
                        Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(Convert.ToString("Notification is Empty", CultureInfo.CurrentCulture));
                      }
                    }
                  }


                  if (!string.IsNullOrEmpty(Notification))
                  {
                    Notification = Notification.Remove(Notification.Length - 5, 5);

                    string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("86");
                    string HeaderString = InfoQuest_All.All_EmailHeader();
                    string FooterString = InfoQuest_All.All_EmailFooter();

                    string BodyString = EmailTemplate;

                    BodyString = BodyString.Replace(";replace;SecurityUserDisplayName;replace;", "" + Notification_SecurityUserDisplayName + "");
                    BodyString = BodyString.Replace(";replace;FormName;replace;", "" + Notification_FormName + "");
                    BodyString = BodyString.Replace(";replace;Notification;replace;", "" + Notification + "");

                    string EmailBody = HeaderString + BodyString + FooterString;

                    string EmailSend = InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Notification_SecurityUserEmail, Notification_FormName, EmailBody);

                    if (EmailSend == "Yes")
                    {
                      Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(Convert.ToString("Notification Send", CultureInfo.CurrentCulture));
                      EmailBody = "";
                    }
                    else if (EmailSend == "No")
                    {
                      Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(Convert.ToString("Notification not Send", CultureInfo.CurrentCulture));
                      Incident_PendingApprovalNotificationsAutomated_Successful = "No";
                      EmailBody = "";
                    }

                    EmailSend = "";
                  }


                  EmailList_SecurityUserEmail = "";
                  EmailList_SendNotification = "";

                  Notification_FormName = "";
                  Notification_SecurityUserDisplayName = "";
                  Notification_SecurityUserEmail = "";
                }
              }
              else
              {
                Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(Convert.ToString("Email List is Empty", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(Convert.ToString("Notifications not Send", CultureInfo.CurrentCulture));
            Incident_PendingApprovalNotificationsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (Incident_PendingApprovalNotificationsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        //else if (Incident_PendingApprovalNotificationsAutomated_Successful == "Yes")
        //{
          //  InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        //}

        Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }


    string Incident_PendingApprovalNotificationsAutomated_Successful = "Yes";

    private static Dictionary<string, string> Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void Incident_PendingApprovalNotifications_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        Incident_PendingApprovalNotifications_Automated_ReturnMessageHandler.Add(ReturnMessage, "Incident_PendingApprovalNotifications_Automated: " + ReturnMessage);
      }
    }
  }
}
