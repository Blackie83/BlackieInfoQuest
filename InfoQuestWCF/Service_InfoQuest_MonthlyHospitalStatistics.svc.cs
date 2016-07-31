using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_MonthlyHospitalStatistics
  {
    public string MonthlyHospitalStatistics_CreateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;
            string SQLStringExecute = "EXECUTE spForm_Execute_MonthlyHospitalStatistics_CreateMonthlyForms";
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

                MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                MonthlyHospitalStatistics_CreateMonthlyFormsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Created", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Created", CultureInfo.CurrentCulture));
            MonthlyHospitalStatistics_CreateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (MonthlyHospitalStatistics_CreateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (MonthlyHospitalStatistics_CreateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string MonthlyHospitalStatistics_UpdateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;
            string SQLStringExecute = "EXECUTE spForm_Execute_MonthlyHospitalStatistics_UpdateMonthlyForms";
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

                MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                MonthlyHospitalStatistics_UpdateMonthlyFormsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Updated", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Updated", CultureInfo.CurrentCulture));
            MonthlyHospitalStatistics_UpdateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (MonthlyHospitalStatistics_UpdateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (MonthlyHospitalStatistics_UpdateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_MonthlyHospitalStatistics_Organisms_InsertOrganisms";
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

                MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                MonthlyHospitalStatistics_OrganismsInsertOrganismsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandlers(Convert.ToString("Organisms Inserted", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandlers(Convert.ToString("Organisms Not Inserted", CultureInfo.CurrentCulture));
            MonthlyHospitalStatistics_OrganismsInsertOrganismsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (MonthlyHospitalStatistics_OrganismsInsertOrganismsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (MonthlyHospitalStatistics_OrganismsInsertOrganismsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_MonthlyHospitalStatistics_Organisms_UpdateOrganisms";
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

                MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                MonthlyHospitalStatistics_OrganismsUpdateOrganismsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandlers(Convert.ToString("Organisms Updated", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandlers(Convert.ToString("Organisms Not Updated", CultureInfo.CurrentCulture));
            MonthlyHospitalStatistics_OrganismsUpdateOrganismsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (MonthlyHospitalStatistics_OrganismsUpdateOrganismsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (MonthlyHospitalStatistics_OrganismsUpdateOrganismsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler.Clear();


        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }


    string MonthlyHospitalStatistics_CreateMonthlyFormsAutomated_Successful = "Yes";
    string MonthlyHospitalStatistics_UpdateMonthlyFormsAutomated_Successful = "Yes";
    string MonthlyHospitalStatistics_OrganismsInsertOrganismsAutomated_Successful = "Yes";
    string MonthlyHospitalStatistics_OrganismsUpdateOrganismsAutomated_Successful = "Yes";

    private static Dictionary<string, string> MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        MonthlyHospitalStatistics_CreateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "MonthlyHospitalStatistics_CreateMonthlyForms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        MonthlyHospitalStatistics_UpdateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "MonthlyHospitalStatistics_UpdateMonthlyForms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated_ReturnMessageHandler.Add(ReturnMessage, "MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated_ReturnMessageHandler.Add(ReturnMessage, "MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated: " + ReturnMessage);
      }
    }
  }
}
