using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_SustainabilityManagement
  {
    public string SustainabilityManagement_CreateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_SustainabilityManagement_CreateMonthlyForms";
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

                SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                SustainabilityManagement_CreateMonthlyFormsAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Created", CultureInfo.CurrentCulture));
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Created", CultureInfo.CurrentCulture));
            SustainabilityManagement_CreateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (SustainabilityManagement_CreateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (SustainabilityManagement_CreateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string SustainabilityManagement_UpdateMonthlyForms_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);
      
      if (AccessValid == true)
      {
        SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        try
        {
          //System.Web.HttpContext.Current.Session["DataTable_AnaestheticGas"] = "";

          string SQLStringAnaestheticGas = @"SELECT	CASE [MappingCategory]
					                                            WHEN 'Halothane 250Ml' THEN 'Halothane'
					                                            WHEN 'Forane 100Ml' THEN 'Isoflurane'
					                                            WHEN 'Ethrane 250Ml' THEN 'Enflurane'
					                                            WHEN 'Sevoflurane (Sojourn and Ultane) - 250Ml Bottle' THEN 'Sevoflurane'
					                                            WHEN 'Suprane 240Ml' THEN 'Desflurane'
				                                            END AS AnaestheticGas_Name ,
				                                            CASE [MappingCategory]
					                                            WHEN 'Halothane 250Ml' THEN '6122'
					                                            WHEN 'Forane 100Ml' THEN '6123'
					                                            WHEN 'Ethrane 250Ml' THEN '6124'
					                                            WHEN 'Sevoflurane (Sojourn and Ultane) - 250Ml Bottle' THEN '6125'
					                                            WHEN 'Suprane 240Ml' THEN '6126'
				                                            END AS AnaestheticGas_List ,
				                                            [FormattedMonth] AS AnaestheticGas_FormattedMonth ,
				                                            CAST(([Quantity] * [Mls]) / 1000 AS DECIMAL(18,2)) AS AnaestheticGas_Quantity ,
				                                            [HospitalCode] AS AnaestheticGas_FacilityCode
                                            FROM		[EDW_Reports].[ReportMeasures].[AnaestheticGases]
                                            WHERE		[FormattedMonth] = 
				                                            CAST(CASE CAST(MONTH(GETDATE()) AS NVARCHAR(MAX))
					                                            WHEN 1 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) - 1 + '12'
					                                            WHEN 2 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '01'
					                                            WHEN 3 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '02'
					                                            WHEN 4 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '03'
					                                            WHEN 5 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '04'
					                                            WHEN 6 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '05'
					                                            WHEN 7 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '06'
					                                            WHEN 8 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '07'
					                                            WHEN 9 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '08'
					                                            WHEN 10 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '09'
					                                            WHEN 11 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '10'
					                                            WHEN 12 THEN CAST(YEAR(GETDATE()) AS NVARCHAR(MAX)) + '11'
				                                            END AS NVARCHAR(MAX))";
          using (SqlCommand SqlCommand_AnaestheticGas = new SqlCommand(SQLStringAnaestheticGas))
          {
            DataTable DataTable_AnaestheticGas;
            using (DataTable_AnaestheticGas = new DataTable())
            {
              DataTable_AnaestheticGas.Locale = CultureInfo.CurrentCulture;
              DataTable_AnaestheticGas = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_AnaestheticGas, "PatientDetailEDW").Copy();
              //System.Web.HttpContext.Current.Session["DataTable_AnaestheticGas"] = DataTable_AnaestheticGas;
              //  }
              //}


              DataTable DataTable_SqlExecute;
              using (DataTable_SqlExecute = new DataTable())
              {
                DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

                string SQLStringExecute = "EXECUTE spForm_Execute_SustainabilityManagement_UpdateMonthlyForms @AnaestheticGas";
                using (SqlCommand SqlCommand_Execute = new SqlCommand(SQLStringExecute))
                {
                  SqlParameter SqlParameter_AnaestheticGas = SqlCommand_Execute.Parameters.AddWithValue("@AnaestheticGas", DataTable_AnaestheticGas);//System.Web.HttpContext.Current.Session["DataTable_AnaestheticGas"]);
                  SqlParameter_AnaestheticGas.SqlDbType = SqlDbType.Structured;
                  SqlParameter_AnaestheticGas.TypeName = "tForm_SustainabilityManagement_AnaestheticGases";

                  DataTable_SqlExecute = InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_Execute).Copy();

                  if (DataTable_SqlExecute.Columns.Count == 1)
                  {
                    string Error = "";
                    foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                    {
                      Error = DataRow_Row["Error"].ToString();
                    }

                    SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                    SustainabilityManagement_UpdateMonthlyFormsAutomated_Successful = "No";
                    Error = "";
                  }
                  else
                  {
                    SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Updated", CultureInfo.CurrentCulture));
                  }
                }
              }

            }
          }

        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandlers(Convert.ToString("Forms Not Updated", CultureInfo.CurrentCulture));
            SustainabilityManagement_UpdateMonthlyFormsAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (SustainabilityManagement_UpdateMonthlyFormsAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (SustainabilityManagement_UpdateMonthlyFormsAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string SustainabilityManagement_MappingMissing_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler.Clear();

        try
        {
          DataTable DataTable_SqlExecute;
          using (DataTable_SqlExecute = new DataTable())
          {
            DataTable_SqlExecute.Locale = CultureInfo.CurrentCulture;

            string SQLStringExecute = "EXECUTE spForm_Execute_SustainabilityManagement_UpdateMonthlyForms";
            using (SqlCommand SqlCommand_Execute = new SqlCommand(SQLStringExecute))
            {
              DataTable_SqlExecute = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Execute).Copy();

              if (DataTable_SqlExecute.Columns.Count == 1)
              {
                string Error = "";
                foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                {
                  Error = DataRow_Row["Error"].ToString();
                }

                SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(Convert.ToString(Error, CultureInfo.CurrentCulture));
                SustainabilityManagement_MappingMissingAutomated_Successful = "No";
                Error = "";
              }
              else
              {
                if (DataTable_SqlExecute.Rows.Count > 0)
                {
                  SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(Convert.ToString("Sustainability Management Missing Mappings", CultureInfo.CurrentCulture));

                  foreach (DataRow DataRow_Row in DataTable_SqlExecute.Rows)
                  {
                    string SustainabilityManagementItemId = DataRow_Row["SustainabilityManagement_Item_Id"].ToString();
                    string EngineeringDescription = DataRow_Row["Engineering_Description"].ToString();
                    string EngineeringTotal = DataRow_Row["Engineering_Total"].ToString();
                    string EngineeringEngUnit = DataRow_Row["Engineering_EngUnit"].ToString();

                    SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(Convert.ToString("Sustainability Management Item Id: " + SustainabilityManagementItemId + "; Engineering Description: " + EngineeringDescription + "; Engineering Total: " + EngineeringTotal + "; Engineering EngUnit: " + EngineeringEngUnit + ";", CultureInfo.CurrentCulture));
                    SustainabilityManagement_MappingMissingAutomated_Successful = "No";
                  }
                }
                else
                {
                  SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(Convert.ToString("No Missing Mapping", CultureInfo.CurrentCulture));
                }
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(Convert.ToString("Missing Mapping Not Retrieved", CultureInfo.CurrentCulture));
            SustainabilityManagement_MappingMissingAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (SustainabilityManagement_MappingMissingAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (SustainabilityManagement_MappingMissingAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }


    string SustainabilityManagement_CreateMonthlyFormsAutomated_Successful = "Yes";
    string SustainabilityManagement_UpdateMonthlyFormsAutomated_Successful = "Yes";
    string SustainabilityManagement_MappingMissingAutomated_Successful = "Yes";

    private static Dictionary<string, string> SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        SustainabilityManagement_CreateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "SustainabilityManagement_CreateMonthlyForms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        SustainabilityManagement_UpdateMonthlyForms_Automated_ReturnMessageHandler.Add(ReturnMessage, "SustainabilityManagement_UpdateMonthlyForms_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        SustainabilityManagement_MappingMissing_Automated_ReturnMessageHandler.Add(ReturnMessage, "SustainabilityManagement_MappingMissing_Automated: " + ReturnMessage);
      }
    }
  }
}
