using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_Administration
  {
    public string Administration_ArchiveRecords_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        Administration_ArchiveRecords_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spAdministration_Execute_ArchiveRecords";
            using (SqlCommand SqlCommand_Execute = new SqlCommand(SQLStringExecute))
            {
              DataTable_SqlExecute = InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_Execute).Copy();

              if (DataTable_SqlExecute.Columns.Count == 1)
              {
                string Error = "";
                foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                {
                  Error = DataRow_Row["Error"].ToString();
                }

                Administration_ArchiveRecords_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                Administration_ArchiveRecordsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                Administration_ArchiveRecords_Automated_ReturnMessageHandlers(Convert.ToString("Forms Created", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            Administration_ArchiveRecords_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Created", CultureInfo.CurrentCulture));
            Administration_ArchiveRecordsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in Administration_ArchiveRecords_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (Administration_ArchiveRecordsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (Administration_ArchiveRecordsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        Administration_ArchiveRecords_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string Administration_BeingModifiedUnlock_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spAdministration_Execute_BeingModifiedUnlock";
            using (SqlCommand SqlCommand_Execute = new SqlCommand(SQLStringExecute))
            {
              DataTable_SqlExecute = InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_Execute).Copy();

              if (DataTable_SqlExecute.Columns.Count == 1)
              {
                string Error = "";
                foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                {
                  Error = DataRow_Row["Error"].ToString();
                }

                Administration_BeingModifiedUnlock_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                Administration_BeingModifiedUnlockAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                Administration_BeingModifiedUnlock_Automated_ReturnMessageHandlers(Convert.ToString("Forms Unlocked", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            Administration_BeingModifiedUnlock_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Unlocked", CultureInfo.CurrentCulture));
            Administration_BeingModifiedUnlockAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (Administration_BeingModifiedUnlockAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (Administration_BeingModifiedUnlockAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string Administration_SecurityAccess_CleanUp_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spAdministration_Execute_SecurityAccess_CleanUp";
            using (SqlCommand SqlCommand_Execute = new SqlCommand(SQLStringExecute))
            {
              DataTable_SqlExecute = InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_Execute).Copy();

              if (DataTable_SqlExecute.Columns.Count == 1)
              {
                string Error = "";
                foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                {
                  Error = DataRow_Row["Error"].ToString();
                }

                Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                Administration_SecurityAccessCleanUpAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandlers(Convert.ToString("Forms Created", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Created", CultureInfo.CurrentCulture));
            Administration_SecurityAccessCleanUpAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (Administration_SecurityAccessCleanUpAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (Administration_SecurityAccessCleanUpAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }


    string Administration_ArchiveRecordsAutomated_Successful = "Yes";
    string Administration_BeingModifiedUnlockAutomated_Successful = "Yes";
    string Administration_SecurityAccessCleanUpAutomated_Successful = "Yes";

    private static Dictionary<string, string> Administration_ArchiveRecords_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void Administration_ArchiveRecords_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!Administration_ArchiveRecords_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        Administration_ArchiveRecords_Automated_ReturnMessageHandler.Add(ReturnMessage, "Administration_ArchiveRecords_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void Administration_BeingModifiedUnlock_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        Administration_BeingModifiedUnlock_Automated_ReturnMessageHandler.Add(ReturnMessage, "Administration_BeingModifiedUnlock_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        Administration_SecurityAccess_CleanUp_Automated_ReturnMessageHandler.Add(ReturnMessage, "Administration_SecurityAccess_CleanUp_Automated: " + ReturnMessage);
      }
    }
  }
}
