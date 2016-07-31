using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_ECM
  {
    public string ECM_CreateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        ECM_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_ECM_CreateMonthlyForms";
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

                ECM_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                ECM_CreateMonthlyFormsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                ECM_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Created", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            ECM_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Created", CultureInfo.CurrentCulture));
            ECM_CreateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in ECM_CreateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (ECM_CreateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (ECM_CreateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        ECM_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string ECM_UpdateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_ECM_UpdateMonthlyForms";
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

                ECM_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                ECM_UpdateMonthlyFormsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                ECM_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Updated", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            ECM_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Updated", CultureInfo.CurrentCulture));
            ECM_UpdateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (ECM_UpdateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (ECM_UpdateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }


    string ECM_CreateMonthlyFormsAutomated_Successful = "Yes";
    string ECM_UpdateMonthlyFormsAutomated_Successful = "Yes";

    private static Dictionary<string, string> ECM_CreateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void ECM_CreateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!ECM_CreateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        ECM_CreateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "ECM_CreateMonthlyForms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void ECM_UpdateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        ECM_UpdateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "ECM_UpdateMonthlyForms_Automated: " + ReturnMessage);
      }
    }
  }
}
