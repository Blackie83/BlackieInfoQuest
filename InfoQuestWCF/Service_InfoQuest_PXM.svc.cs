using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO.Compression;

namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest_PXM
  {
    //--START-- --PXM PDCH Event--//
    public string PXM_PDCH_Event_CreateFile_Manual(string userName, string password, DateTime startDate, DateTime endDate, string facilityId)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler.Clear();

        DateTime CurrentDate = DateTime.Now;
        string FacilityId = InfoQuest_DataPatient.DataPatient_ODS_ImpiloUnitId(facilityId);

        DataTable DataTable_ExportDataToFile;
        using (DataTable_ExportDataToFile = new DataTable())
        {
          DataTable_ExportDataToFile.Locale = CultureInfo.CurrentCulture;
          DataTable_ExportDataToFile = InfoQuest_DataPatient.DataPatient_ODS_PXM_PostDischargeSurvey(startDate, endDate, facilityId).Copy();
          if (DataTable_ExportDataToFile.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_ExportDataToFile.Rows)
            {
              PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandlers(Convert.ToString(DataRow_Row["Error"].ToString(), CultureInfo.CurrentCulture));
            }

            PXM_PDCH_EventCreateFileManual_Successful = "No";
          }
          else if (DataTable_ExportDataToFile.Columns.Count != 1)
          {
            string InvalidColumns = InfoQuest_All.All_FileInvalidColumns(DataTable_ExportDataToFile, "PXM_PDCH_Event_", "Form_PXM_PDCH_Event");

            if (string.IsNullOrEmpty(InvalidColumns))
            {
              string CreateFile_ReturnMessage = Convert.ToString(PXM_PDCH_CreateFile_TXT(DataTable_ExportDataToFile, false, "PXM_PDCH_Event", "PXM PDCHEvent", "PDCH", CurrentDate, startDate, endDate, FacilityId), CultureInfo.CurrentCulture);

              if (string.IsNullOrEmpty(CreateFile_ReturnMessage))
              {
                PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandlers(Convert.ToString("File created at " + @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\PXM_PDCH_Event\", CultureInfo.CurrentCulture));
              }
              else
              {
                PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandlers(Convert.ToString(CreateFile_ReturnMessage, CultureInfo.CurrentCulture));
                PXM_PDCH_EventCreateFileManual_Successful = "No";
                InfoQuest_Exceptions.Exceptions_OwnMessage(CreateFile_ReturnMessage, "", System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
              }
            }
            else
            {
              PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandlers(Convert.ToString("File could not be created, column: " + InvalidColumns + " do not match mapping table", CultureInfo.CurrentCulture));
              PXM_PDCH_EventCreateFileManual_Successful = "No";
            }
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EventCreateFileManual_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EventCreateFileManual_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string PXM_PDCH_Event_CreateFile_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler.Clear();

        DateTime CurrentDate = DateTime.Now;
        DateTime StartDate = CurrentDate.AddDays(-1);
        DateTime EndDate = CurrentDate.AddDays(-1);
        string FacilityId = "ALL";

        string PXMPDCHEventFileCreatedId = "";
        string SQLStringForm = "SELECT PXM_PDCH_Event_FileCreated_Id FROM Form_PXM_PDCH_Event_FileCreated WHERE PXM_PDCH_Event_FileCreated_StartDate = @PXM_PDCH_Event_FileCreated_StartDate";
        using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
        {
          SqlCommand_Form.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_StartDate", StartDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
          DataTable DataTable_Form;
          using (DataTable_Form = new DataTable())
          {
            DataTable_Form.Locale = CultureInfo.CurrentCulture;
            DataTable_Form = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
            if (DataTable_Form.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Form.Rows)
              {
                PXMPDCHEventFileCreatedId = DataRow_Row["PXM_PDCH_Event_FileCreated_Id"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(PXMPDCHEventFileCreatedId))
        {
          DataTable DataTable_ExportDataToFile;
          using (DataTable_ExportDataToFile = new DataTable())
          {
            DataTable_ExportDataToFile.Locale = CultureInfo.CurrentCulture;
            DataTable_ExportDataToFile = InfoQuest_DataPatient.DataPatient_ODS_PXM_PostDischargeSurvey(StartDate, EndDate, null).Copy();
            if (DataTable_ExportDataToFile.Columns.Count == 1)
            {
              foreach (DataRow DataRow_Row in DataTable_ExportDataToFile.Rows)
              {
                PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(Convert.ToString(DataRow_Row["Error"].ToString(), CultureInfo.CurrentCulture));
              }

              PXM_PDCH_EventCreateFileAutomated_Successful = "No";
            }
            else if (DataTable_ExportDataToFile.Columns.Count != 1)
            {
              string InvalidColumns = InfoQuest_All.All_FileInvalidColumns(DataTable_ExportDataToFile, "PXM_PDCH_Event_", "Form_PXM_PDCH_Event");

              if (string.IsNullOrEmpty(InvalidColumns))
              {
                string CreateFile_ReturnMessage = Convert.ToString(PXM_PDCH_CreateFile_TXT(DataTable_ExportDataToFile, true, "PXM_PDCH_Event", "PXM PDCHEvent", "PDCH", CurrentDate, StartDate, EndDate, FacilityId), CultureInfo.CurrentCulture);

                if (string.IsNullOrEmpty(CreateFile_ReturnMessage))
                {
                  PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(Convert.ToString("File created at " + @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\PXM_PDCH_Event\", CultureInfo.CurrentCulture));
                }
                else
                {
                  PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(Convert.ToString(CreateFile_ReturnMessage, CultureInfo.CurrentCulture));
                  PXM_PDCH_EventCreateFileAutomated_Successful = "No";
                  InfoQuest_Exceptions.Exceptions_OwnMessage(CreateFile_ReturnMessage, "", System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
                }
              }
              else
              {
                PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(Convert.ToString("File could not be created, column: " + InvalidColumns + " do not match mapping table", CultureInfo.CurrentCulture));
                PXM_PDCH_EventCreateFileAutomated_Successful = "No";
              }
            }
          }
        }
        else
        {
          PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(Convert.ToString("PXM PDCH Event file already created for previous day", CultureInfo.CurrentCulture));
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EventCreateFileAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EventCreateFileAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    public string PXM_PDCH_Event_CreateFile_AutomatedMissing(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler.Clear();

        string SQLStringFile = ";WITH	PXMPDCHMissing(DAY) AS ( SELECT CAST(CONVERT(NVARCHAR(MAX), DATEADD(DAY, -30, GETDATE()), 110) AS DATETIME) AS DAY UNION ALL SELECT DAY + 1 FROM PXMPDCHMissing WHERE DAY < CAST(DATEADD(DAY, -2, GETDATE()) AS DATETIME) ) SELECT PXMPDCHMissing.DAY AS 'Missing' FROM PXMPDCHMissing LEFT JOIN Form_PXM_PDCH_Event_FileCreated ON Form_PXM_PDCH_Event_FileCreated.PXM_PDCH_Event_FileCreated_StartDate = PXMPDCHMissing.DAY GROUP BY PXMPDCHMissing.DAY HAVING COUNT(PXM_PDCH_Event_FileCreated_StartDate) = 0";
        using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
        {
          DataTable DataTable_File;
          using (DataTable_File = new DataTable())
          {
            DataTable_File.Locale = CultureInfo.CurrentCulture;
            DataTable_File = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_File).Copy();
            if (DataTable_File.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_File.Rows)
              {
                DateTime Missing = Convert.ToDateTime(DataRow_Row["Missing"], CultureInfo.CurrentCulture);
                DateTime CurrentDate = DateTime.Now;
                DateTime StartDate = Missing;
                DateTime EndDate = Missing;
                string FacilityId = "ALL";

                DataTable DataTable_ExportDataToFile;
                using (DataTable_ExportDataToFile = new DataTable())
                {
                  DataTable_ExportDataToFile.Locale = CultureInfo.CurrentCulture;
                  DataTable_ExportDataToFile = InfoQuest_DataPatient.DataPatient_ODS_PXM_PostDischargeSurvey(StartDate, EndDate, null).Copy();
                  if (DataTable_ExportDataToFile.Columns.Count == 1)
                  {
                    foreach (DataRow DataRow_RowExport in DataTable_ExportDataToFile.Rows)
                    {
                      if (DataRow_RowExport.Table.Columns.Contains("Error"))
                      {
                        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString(DataRow_RowExport["Error"].ToString(), CultureInfo.CurrentCulture));
                      }
                      else
                      {
                        foreach (DataColumn DataColumn_ExportDataToFile in DataTable_ExportDataToFile.Columns)
                        {
                          PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString(DataRow_RowExport["" + DataColumn_ExportDataToFile.ColumnName.ToString() + ""].ToString(), CultureInfo.CurrentCulture));
                        }
                      }
                    }

                    PXM_PDCH_EventCreateFileAutomatedMissing_Successful = "No";
                  }
                  else if (DataTable_ExportDataToFile.Columns.Count != 1)
                  {
                    string InvalidColumns = InfoQuest_All.All_FileInvalidColumns(DataTable_ExportDataToFile, "PXM_PDCH_Event_", "Form_PXM_PDCH_Event");

                    if (string.IsNullOrEmpty(InvalidColumns))
                    {
                      string CreateFile_ReturnMessage = Convert.ToString(PXM_PDCH_CreateFile_TXT(DataTable_ExportDataToFile, true, "PXM_PDCH_Event", "PXM PDCHEvent", "PDCH", CurrentDate, StartDate, EndDate, FacilityId), CultureInfo.CurrentCulture);

                      if (string.IsNullOrEmpty(CreateFile_ReturnMessage))
                      {
                        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString("Files created at " + @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\PXM_PDCH_Event\", CultureInfo.CurrentCulture));
                      }
                      else
                      {
                        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString(CreateFile_ReturnMessage, CultureInfo.CurrentCulture));
                        PXM_PDCH_EventCreateFileAutomatedMissing_Successful = "No";
                        InfoQuest_Exceptions.Exceptions_OwnMessage(CreateFile_ReturnMessage, "", System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
                      }
                    }
                    else
                    {
                      PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString("Files could not be created, column: " + InvalidColumns + " do not match mapping table", CultureInfo.CurrentCulture));
                      PXM_PDCH_EventCreateFileAutomatedMissing_Successful = "No";
                    }
                  }
                }
              }
            }
            else
            {
              PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(Convert.ToString("No missing PXM PDCH Event files", CultureInfo.CurrentCulture));
            }
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EventCreateFileAutomatedMissing_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EventCreateFileAutomatedMissing_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    string PXM_PDCH_EventCreateFileManual_Successful = "Yes";
    string PXM_PDCH_EventCreateFileAutomated_Successful = "Yes";
    string PXM_PDCH_EventCreateFileAutomatedMissing_Successful = "Yes";

    private static Dictionary<string, string> PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Event_CreateFile_Manual_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Event_CreateFile_Manual: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Event_CreateFile_Automated_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Event_CreateFile_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Event_CreateFile_AutomatedMissing_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Event_CreateFile_AutomatedMissing: " + ReturnMessage);
      }
    }
    //---END--- --PXM PDCH Event--//



    //--START-- --PXM PDCH Escalation--//
    //--START-- --PXM PDCH Escalation CheckFileProcessing--//
    public string PXM_PDCH_Escalation_CheckFileProcessing(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler.Clear();

        string SQLStringFile = "SELECT	TempTableA.FileToProcess , " +
                               "         TempTableB.FileProcessed " +
                               " FROM		( " +
                               "           SELECT 'PXM PDCH Escalation' AS FileToProcess , 'PXM PDCHEscalation' AS FileProcessed UNION " +
                               "           SELECT 'PXM PDWS Escalation' AS FileToProcess , 'PXM PDWSEscalation' AS FileProcessed " +
                               "         ) AS TempTableA " +
                               "         LEFT JOIN " +
                               "         ( " +
                               "           SELECT DISTINCT SUBSTRING(PXM_PDCH_Escalation_FileUploaded_FileName,0,CHARINDEX('-',PXM_PDCH_Escalation_FileUploaded_FileName)) AS FileProcessed " +
                               "           FROM Form_PXM_PDCH_Escalation_FileUploaded " +
                               "           WHERE PXM_PDCH_Escalation_FileUploaded_CurrentDate >= CAST(CONVERT(NVARCHAR(MAX), DATEADD(DAY, -1, GETDATE()), 110) AS DATETIME) " +
                               "         ) AS TempTableB " +
                               "         ON TempTableA.FileProcessed = TempTableB.FileProcessed";
        using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
        {
          DataTable DataTable_File;
          using (DataTable_File = new DataTable())
          {
            DataTable_File.Locale = CultureInfo.CurrentCulture;
            DataTable_File = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_File).Copy();
            if (DataTable_File.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_File.Rows)
              {
                string FileToProcess = DataRow_Row["FileToProcess"].ToString();
                string FileProcessed = DataRow_Row["FileProcessed"].ToString();

                if (string.IsNullOrEmpty(FileProcessed))
                {
                  PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString(FileToProcess + " file not received yesterday", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationCheckFileProcessing_Successful = "No";
                }
              }
            }
            else
            {
              PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString("No PXM Escalation file received yesterday", CultureInfo.CurrentCulture));
              PXM_PDCH_EscalationCheckFileProcessing_Successful = "No";
            }
          }
        }

        if (PXM_PDCH_EscalationCheckFileProcessing_Successful == "Yes")
        {
          PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString("PXM Escalation file received yesterday", CultureInfo.CurrentCulture));
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EscalationCheckFileProcessing_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EscalationCheckFileProcessing_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    string PXM_PDCH_EscalationCheckFileProcessing_Successful = "Yes";

    private static Dictionary<string, string> PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Escalation_CheckFileProcessing_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Escalation_CheckFileProcessing: " + ReturnMessage);
      }
    }
    //---END--- --PXM PDCH Escalation CheckFileProcessing--//


    //--START-- --PXM PDCH Escalation CheckSurveyProcessing--//
    public string PXM_PDCH_Escalation_CheckSurveyProcessing(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler.Clear();

        string SQLStringSurvey = @"SELECT	CONVERT(VARCHAR(10),TempTable_Survey.Date_Date,111) AS Survey_Date, 
                                          TempTable_Survey.Survey_Name, 
                                          ISNULL(TempTable_CRM.Survey_Count,'0') AS Survey_Count
                                  FROM		(
                                            SELECT	Date_Date , 
                                                    Survey_Name
                                            FROM		Administration_Date
                                                    CROSS JOIN (
                                                      SELECT	DISTINCT 
                                                              CRM_UploadedFrom AS Survey_Name
                                                      FROM		Form_CRM
                                                      WHERE		CRM_UploadedFrom IS NOT NULL
                                                              AND CRM_UploadedFrom NOT IN ('Discovery Survey','Post Discharge Survey','Website Survey : PDWS Web')
                                                    ) AS TempTable_CRM
                                            WHERE		Date_Date BETWEEN (GETDATE() - 2) AND (GETDATE() - 1)
                                                    AND TempTable_CRM.Survey_Name IS NOT NULL
                                                    AND Survey_Name NOT IN ('Discovery Survey','Post Discharge Survey','Website Survey : PDWS Web')
                                          ) AS TempTable_Survey 
                                          LEFT JOIN (
                                            SELECT		CRM_UploadedFrom AS Survey_Name , 
                                                      CONVERT(VARCHAR(8),CRM_CreatedDate,112) AS Survey_Date , 
                                                      COUNT(CRM_Id) AS Survey_Count				
                                            FROM			Form_CRM
                                            WHERE			CRM_UploadedFrom IS NOT NULL
                                                      AND CONVERT(VARCHAR(8),CRM_CreatedDate,112) >= (GETDATE() - 2)
                                            GROUP BY	CRM_UploadedFrom , 
                                                      CONVERT(VARCHAR(8),CRM_CreatedDate,112)
                                          ) AS TempTable_CRM
                                            ON TempTable_Survey.Date_Date = TempTable_CRM.Survey_Date
                                            AND TempTable_Survey.Survey_Name = TempTable_CRM.Survey_Name
                                  ORDER BY	TempTable_Survey.Date_Date DESC , 
                                            TempTable_Survey.Survey_Name";
        using (SqlCommand SqlCommand_Survey = new SqlCommand(SQLStringSurvey))
        {
          DataTable DataTable_Survey;
          using (DataTable_Survey = new DataTable())
          {
            DataTable_Survey.Locale = CultureInfo.CurrentCulture;
            DataTable_Survey = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Survey).Copy();
            if (DataTable_Survey.Rows.Count > 0)
            {
              PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandlers(Convert.ToString("PXM Escalation surveys processed yesterday", CultureInfo.CurrentCulture));

              foreach (DataRow DataRow_Row_FacilityId in DataTable_Survey.Rows)
              {
                string SurveyDate = DataRow_Row_FacilityId["Survey_Date"].ToString();
                string SurveyName = DataRow_Row_FacilityId["Survey_Name"].ToString();
                string SurveyCount = DataRow_Row_FacilityId["Survey_Count"].ToString();

                PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandlers(Convert.ToString("Date: " + SurveyDate + "; Survey: " + SurveyName + "; Count: " + SurveyCount + ";", CultureInfo.CurrentCulture));
              }
            }
            else
            {
              PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandlers(Convert.ToString("No PXM Escalation surveys processed yesterday", CultureInfo.CurrentCulture));
              PXM_PDCH_EscalationCheckSurveyProcessing_Successful = "No";
            }
          }
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EscalationCheckSurveyProcessing_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EscalationCheckSurveyProcessing_Successful == "Yes")
        {
          InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    string PXM_PDCH_EscalationCheckSurveyProcessing_Successful = "Yes";

    private static Dictionary<string, string> PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Escalation_CheckSurveyProcessing_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Escalation_CheckSurveyProcessing: " + ReturnMessage);
      }
    }
    //---END--- --PXM PDCH Escalation CheckSurveyProcessing--//


    //--START-- --PXM PDCH Escalation ExportData--//
    public string PXM_PDCH_Escalation_ExportData_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);
      
      if (AccessValid == true)
      {
        PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler.Clear();
        PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler.Clear();

        string ExportPath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\PXM_PDCH_Escalation\";

        try
        {
          PXM_PDCH_FromDataBase_Impersonation FromDataBase_Impersonation_Current = PXM_PDCH_GetImpersonation();
          string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
          string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
          string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

          if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
          {
            if (Directory.Exists(ExportPath))
            {
              string[] UploadedFiles = Directory.GetFiles(ExportPath, "*.*", SearchOption.AllDirectories);

              if (UploadedFiles.Length > 0)
              {
                foreach (string Files in Directory.GetFiles(ExportPath, "*.*", SearchOption.AllDirectories))
                {
                  PXM_PDCH_Escalation_DeleteFiles = "Yes";
                  bool ProcessFile = InfoQuest_All.All_FileAccessible(Files);
                  string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);

                  if (ProcessFile == true)
                  {
                    string FileExtension = FileName.Substring(FileName.LastIndexOf('.') + 1);

                    if (FileExtension == "txt")
                    {
                      if (UploadedFiles.Length == 1 && FileName == "placeholder.txt")
                      {
                        PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("No Files at " + ExportPath + " , only placeholder.txt file", CultureInfo.CurrentCulture));
                        PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                      }
                      else if (UploadedFiles.Length >= 1 && FileName != "placeholder.txt")
                      {
                        DataTable DataTable_File;
                        using (DataTable_File = new DataTable())
                        {
                          DataTable_File.Locale = CultureInfo.CurrentCulture;
                          DataTable_File = PXM_PDCH_Escalation_ExtractData(FileName, ExportPath).Copy();

                          if (DataTable_File.Columns.Count == 0)
                          {
                            PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format, data extraction failed for file name: " + FileName, CultureInfo.CurrentCulture));
                            PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                            PXM_PDCH_Escalation_DeleteFiles = "No";
                          }
                          else
                          {
                            string InvalidColumns = InfoQuest_All.All_FileInvalidColumns(DataTable_File, "", "Form_PXM_PDCH_Escalation");

                            if (string.IsNullOrEmpty(InvalidColumns))
                            {
                              PXM_PDCH_Escalation_ProcessData(DataTable_File, FileName);

                              PXM_PDCH_Escalation_DeleteFile(DataTable_File, FileName, ExportPath);
                            }
                            else
                            {
                              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File " + FileName + " could not be processed, column: " + InvalidColumns + " do not match mapping table", CultureInfo.CurrentCulture));
                              PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                            }
                          }
                        }
                      }
                    }
                  }
                  else
                  {
                    PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File " + FileName + " could not be processed", CultureInfo.CurrentCulture));
                    PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                  }
                }
              }
              else
              {
                PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("No Files at " + ExportPath, CultureInfo.CurrentCulture));
                //PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                PXM_PDCH_Escalation_ImportData = "No";
              }
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Folder could not be accessed at " + ExportPath, CultureInfo.CurrentCulture));
              PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
            }

            InfoQuest_Impersonate.UndoImpersonation();
          }
          else
          {
            PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("User Impersination Failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture));
            PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            InfoQuest_Impersonate.UndoImpersonation();

            PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File could not be accessed at " + ExportPath, CultureInfo.CurrentCulture));
            PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        if (PXM_PDCH_EscalationExportDataAutomated_Successful == "Yes" && PXM_PDCH_Escalation_ImportData == "Yes")
        {
          PXM_PDCH_Escalation_ImportData_CRMComment_PDCH_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDCH_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDCM_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDCM_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDEU_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDEU_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDEM_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDEM_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDWS_Web();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDCU_Web();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDRB_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDRB_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_USSD();

          PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_Email();
          PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_USSD();

          PXM_PDCH_Escalation_SendEmail();
        }

        PXM_PDCH_Escalation_ImportData_CleanUp();

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_EscalationExportDataAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_EscalationExportDataAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler.Clear();
        PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    private DataTable PXM_PDCH_Escalation_ExtractData(string fileName, string exportPath)
    {
      Int32 RowNumber = 1;
      DataTable DataTable_File;
      using (DataTable_File = new DataTable())
      {
        DataTable_File.Locale = CultureInfo.CurrentCulture;

        try
        {
          using (StreamReader StreamReader_File = new StreamReader(exportPath + fileName, true))
          {
            DateTime CurrentDateTime = DateTime.Now;

            while (!StreamReader_File.EndOfStream)
            {
              string FileLine = StreamReader_File.ReadLine();
              if (!string.IsNullOrEmpty(FileLine.Trim()))
              {
                if (RowNumber == 1)
                {
                  DataTable_File.Columns.Add("PXM_PDCH_Escalation_Id");

                  string[] ColumnNames = FileLine.Split("|".ToCharArray(), StringSplitOptions.None);
                  foreach (string ColumnName in ColumnNames)
                  {
                    string ColumnNameNew = ColumnName;
                    ColumnNameNew = ColumnNameNew.Replace(" ", "");
                    ColumnNameNew = ColumnNameNew.Replace(".", "");
                    ColumnNameNew = "PXM_PDCH_Escalation_" + ColumnNameNew;

                    DataTable_File.Columns.Add(ColumnNameNew);
                  }

                  DataTable_File.Columns.Add("PXM_PDCH_Escalation_InfoQuestUploadUser");
                  DataTable_File.Columns.Add("PXM_PDCH_Escalation_InfoQuestUploadFrom");
                  DataTable_File.Columns.Add("PXM_PDCH_Escalation_InfoQuestUploadDate");
                }
                else if (RowNumber > 1)
                {
                  FileLine = RowNumber + "|" + FileLine + "|WCF|Automated|" + CurrentDateTime;
                  string[] Data = FileLine.Split("|".ToCharArray(), StringSplitOptions.None);
                  DataTable_File.Rows.Add(Data);
                }

                RowNumber = RowNumber + 1;
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          PXM_PDCH_Escalation_DeleteFiles = "No";

          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format for file name: " + fileName + ", Error was on Row Number: " + RowNumber.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            DataTable_File.Clear();
          }
          else
          {
            throw;
          }
        }
      }

      return DataTable_File;
    }

    private void PXM_PDCH_Escalation_ProcessData(DataTable dataTable_File, string fileName)
    {
      if (PXM_PDCH_Escalation_DeleteFiles == "Yes")
      {
        if (dataTable_File.Columns.Count > 1)
        {
          if (dataTable_File.Rows.Count > 0)
          {
            string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
            using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
            {
              SqlConnection_BulkCopy.Open();

              using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
              {
                SqlBulkCopy_File.DestinationTableName = "Form_PXM_PDCH_Escalation";

                foreach (DataColumn DataColumn_ColumnNames in dataTable_File.Columns)
                {
                  string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_PDCH_Escalation') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
                  using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
                  {
                    SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
                    DataTable DataTable_Column;
                    using (DataTable_Column = new DataTable())
                    {
                      DataTable_Column.Locale = CultureInfo.CurrentCulture;
                      DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Column).Copy();
                      if (DataTable_Column.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row_FacilityId in DataTable_Column.Rows)
                        {
                          string name = DataRow_Row_FacilityId["name"].ToString();

                          SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                          SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                        }
                      }
                    }
                  }
                }


                try
                {
                  SqlBulkCopy_File.WriteToServer(dataTable_File);
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data successful for file name: " + fileName, CultureInfo.CurrentCulture));
                }
                catch (Exception Exception_Error)
                {
                  PXM_PDCH_Escalation_DeleteFiles = "No";

                  if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                  {
                    PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data failed for file name: " + fileName, CultureInfo.CurrentCulture));
                    PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                    InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
                  }
                  else
                  {
                    throw;
                  }
                }
              }
            }
          }
          else
          {
            PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data successful for file name: " + fileName, CultureInfo.CurrentCulture));
          }
        }
        else
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format for file name: " + fileName, CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          PXM_PDCH_Escalation_DeleteFiles = "No";
        }
      }
    }

    private void PXM_PDCH_Escalation_DeleteFile(DataTable dataTable_File, string fileName, string exportPath)
    {
      try
      {
        if (PXM_PDCH_Escalation_DeleteFiles == "Yes")
        {
          string TXTFileName = fileName;
          string ZIPFileName = fileName.Replace(".txt", ".zip");

          string TXTFilePathAndName = exportPath + TXTFileName;
          string ZIPFilePathAndName = exportPath + ZIPFileName;

          using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
          {
            ZipArchive_PathAndName.CreateEntryFromFile(TXTFilePathAndName, TXTFileName);
          }

          using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
          {
            string ZIPFileContentType = "application/zip";
            BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
            Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

            string SQLStringInsertPXMPDCHEscalationFileUploaded = "INSERT INTO Form_PXM_PDCH_Escalation_FileUploaded ( PXM_PDCH_Escalation_FileUploaded_FileName , PXM_PDCH_Escalation_FileUploaded_ZipFileName , PXM_PDCH_Escalation_FileUploaded_ContentType , PXM_PDCH_Escalation_FileUploaded_Data , PXM_PDCH_Escalation_FileUploaded_Records , PXM_PDCH_Escalation_FileUploaded_CurrentDate , PXM_PDCH_Escalation_FileUploaded_From ) VALUES ( @PXM_PDCH_Escalation_FileUploaded_FileName , @PXM_PDCH_Escalation_FileUploaded_ZipFileName , @PXM_PDCH_Escalation_FileUploaded_ContentType , @PXM_PDCH_Escalation_FileUploaded_Data , @PXM_PDCH_Escalation_FileUploaded_Records , @PXM_PDCH_Escalation_FileUploaded_CurrentDate , @PXM_PDCH_Escalation_FileUploaded_From )";
            using (SqlCommand SqlCommand_InsertPXMPDCHEscalationFileUploaded = new SqlCommand(SQLStringInsertPXMPDCHEscalationFileUploaded))
            {
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_FileName", TXTFileName);
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_ZipFileName", ZIPFileName);
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_ContentType", ZIPFileContentType);
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_Data", Byte_ZIPFile);
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_Records", dataTable_File.Rows.Count.ToString(CultureInfo.CurrentCulture));
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_CurrentDate", DateTime.Now);
              SqlCommand_InsertPXMPDCHEscalationFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_From", "Automated");
              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPXMPDCHEscalationFileUploaded);
            }
          }

          File.Delete(TXTFilePathAndName);
          File.Delete(ZIPFilePathAndName);
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File deletion failed for file name: " + fileName, CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private static void PXM_PDCH_Escalation_ImportData_CleanUp()
    {
      string SQLStringCleanUpPXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser OR PXM_PDCH_Escalation_InfoQuestUploadDate IS NULL";
      using (SqlCommand SqlCommand_CleanUpPXMPDCH = new SqlCommand(SQLStringCleanUpPXMPDCH))
      {
        SqlCommand_CleanUpPXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CleanUpPXMPDCH);
      }
    }

    private static void PXM_PDCH_Escalation_SendEmail()
    {
      foreach (KeyValuePair<string, string> KeyValuePair_Facility in PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler)
      {
        string FacilityId = KeyValuePair_Facility.Key;
        string FacilityFacilityDisplayName = KeyValuePair_Facility.Value;

        string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("55");
        string FormName = InfoQuest_All.All_FormName("36");
        string HeaderString = InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuest_All.All_EmailFooter();

        string SecurityUserDisplayName = "";
        string SecurityUserEmail = "";
        string SQLStringEmailTo = "SELECT DISTINCT SecurityUser_DisplayName , SecurityUser_Email , EmailOrder FROM ( " +
                                    "  SELECT * , RANK() OVER (ORDER BY EmailOrder) AS EmailRank FROM ( " +
                                    "    SELECT	ListItem_Name AS SecurityUser_DisplayName , " +
                                    "            ListItem_Name AS SecurityUser_Email , " +
                                    "            '1' AS EmailOrder " +
                                    "    FROM		vAdministration_ListItem_Active " +
                                    "    WHERE		ListCategory_Id = 167 " +
                                    "            AND ListItem_Parent IN ( " +
                                    "              SELECT	ListItem_Id " +
                                    "              FROM		vAdministration_ListItem_Active " +
                                    "              WHERE		ListCategory_Id = 166 " +
                                    "                      AND ListItem_Name = @Facility_Id " +
                                    "            ) " +
                                    "    UNION " +
                                    "    SELECT	ISNULL(SecurityUser_DisplayName,'') AS SecurityUser_DisplayName , " +
                                    "            ISNULL(SecurityUser_Email,'') AS SecurityUser_Email , " +
                                    "            '2' AS EmailOrder " +
                                    "    FROM		vAdministration_SecurityAccess_Active " +
                                    "    WHERE		Form_Id IN ('36') " +
                                    "            AND SecurityRole_Id IN ('148') " +
                                    "            AND Facility_Id = @Facility_Id " +
                                    "            AND SecurityUser_Email IS NOT NULL " +
                                    "  ) AS TempTableA " +
                                    ") AS TempTableB " +
                                    "WHERE EmailRank = 1 " +
                                    "ORDER BY EmailOrder";
        using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
        {
          SqlCommand_EmailTo.Parameters.AddWithValue("@Facility_Id", FacilityId);
          DataTable DataTable_EmailTo;
          using (DataTable_EmailTo = new DataTable())
          {
            DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailTo = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
            if (DataTable_EmailTo.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_EmailTo in DataTable_EmailTo.Rows)
              {
                SecurityUserDisplayName = DataRow_Row_EmailTo["SecurityUser_DisplayName"].ToString();
                SecurityUserEmail = DataRow_Row_EmailTo["SecurityUser_Email"].ToString();

                if (!string.IsNullOrEmpty(SecurityUserDisplayName) && !string.IsNullOrEmpty(SecurityUserEmail))
                {
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + SecurityUserDisplayName + "");
                  BodyString = BodyString.Replace(";replace;FormName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");

                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", SecurityUserEmail, FormName, EmailBody);

                  if (string.IsNullOrEmpty(EmailSend) || !string.IsNullOrEmpty(EmailSend))
                  {
                    EmailBody = "";
                  }

                  EmailSend = "";
                }

                SecurityUserDisplayName = "";
                SecurityUserEmail = "";
              }
            }
          }
        }

        SecurityUserDisplayName = "";
        SecurityUserEmail = "";
        EmailTemplate = "";
        FormName = "";

        FacilityId = "";
        FacilityFacilityDisplayName = "";
      }

      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler.Clear();
    }

    private static void PXM_PDCH_Escalation_SendEmail_WebsiteContactUs_SecurityUsers(string crmId, string crmTypeName, string crmReportNumber, string facilityId, string facilityFacilityDisplayName)
    {
      string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("65");
      string FormName = InfoQuest_All.All_FormName("36");
      string HeaderString = InfoQuest_All.All_EmailHeader();
      string FooterString = InfoQuest_All.All_EmailFooter();

      string DisplayName = "";
      string Email = "";
      string SQLStringEmailTo = "SELECT DISTINCT DisplayName , Email , EmailOrder FROM ( " +
                                "  SELECT * , RANK() OVER (ORDER BY EmailOrder) AS EmailRank FROM ( " +
                                "    SELECT	ListItem_Name AS DisplayName , " +
                                "            ListItem_Name AS Email , " +
                                "            '1' AS EmailOrder " +
                                "    FROM		vAdministration_ListItem_Active " +
                                "    WHERE		ListCategory_Id = 170 " +
                                "            AND ListItem_ParentName = @RouteToUnit " +
                                "            AND ListItem_Parent IN ( " +
                                "              SELECT	ListItem_Id " +
                                "              FROM		vAdministration_ListItem_Active " +
                                "              WHERE		ListCategory_Id = 169 " +
                                "                      AND ListItem_Parent IN ( " +
                                "                        SELECT	ListItem_Id " +
                                "                        FROM		vAdministration_ListItem_Active " +
                                "                        WHERE		ListCategory_Id = 168 " +
                                "                                AND ListItem_Name = @RouteToFacility " +
                                "                      ) " +
                                "            ) " +
                                "    UNION " +
                                "    SELECT	ListItem_Name AS DisplayName , " +
                                "            ListItem_Name AS Email , " +
                                "            '2' AS EmailOrder " +
                                "    FROM		vAdministration_ListItem_Active " +
                                "    WHERE		ListCategory_Id = 167 " +
                                "            AND ListItem_Parent IN ( " +
                                "              SELECT	ListItem_Id " +
                                "              FROM		vAdministration_ListItem_Active " +
                                "              WHERE		ListCategory_Id = 166 " +
                                "                      AND ListItem_Name = @RouteToFacility " +
                                "            ) " +
                                "    UNION " +
                                "    SELECT	ISNULL(SecurityUser_DisplayName,'') AS DisplayName , " +
                                "            ISNULL(SecurityUser_Email,'') AS Email , " +
                                "            '3' AS EmailOrder " +
                                "    FROM		vAdministration_SecurityAccess_Active " +
                                "    WHERE		Form_Id IN ('36') " +
                                "            AND SecurityRole_Id IN ('148') " +
                                "            AND Facility_Id = @RouteToFacility " +
                                "            AND SecurityUser_Email IS NOT NULL " +
                                "  ) AS TempTableA " +
                                ") AS TempTableB " +
                                "WHERE EmailRank = 1 " +
                                "ORDER BY EmailOrder";
      using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
      {
        SqlCommand_EmailTo.Parameters.AddWithValue("@RouteToUnit", crmTypeName);
        SqlCommand_EmailTo.Parameters.AddWithValue("@RouteToFacility", facilityId);
        SqlCommand_EmailTo.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_EmailTo;
        using (DataTable_EmailTo = new DataTable())
        {
          DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailTo = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
          if (DataTable_EmailTo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_EmailTo in DataTable_EmailTo.Rows)
            {
              DisplayName = DataRow_Row_EmailTo["DisplayName"].ToString();
              Email = DataRow_Row_EmailTo["Email"].ToString();

              if (!string.IsNullOrEmpty(DisplayName) && !string.IsNullOrEmpty(Email))
              {
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + DisplayName + "");
                BodyString = BodyString.Replace(";replace;FormName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + facilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;CRMTypeName;replace;", "" + crmTypeName + "");
                BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + crmReportNumber + "");
                BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + crmId + "");

                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Email, FormName, EmailBody);

                if (string.IsNullOrEmpty(EmailSend) || !string.IsNullOrEmpty(EmailSend))
                {
                  EmailBody = "";
                }

                EmailSend = "";
              }

              DisplayName = "";
              Email = "";
            }
          }
        }
      }

      DisplayName = "";
      Email = "";
      EmailTemplate = "";
      FormName = "";
    }

    private static void PXM_PDCH_Escalation_SendEmail_WebsiteContactUs_Customer(string crmTypeName, string crmReportNumber, string pxmpdchEscalationContactEmailAddress, string pxmpdchEscalationContactNameSurname, string pxmpdchEscalationComment, string pxmpdchEscalationCreatedDate)
    {
      string EmailTemplate = InfoQuest_All.All_SystemEmailTemplate("66");

      string FromEmailAddress = "";
      string SQLStringListItem = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id";
      using (SqlCommand SqlCommand_ListItem = new SqlCommand(SQLStringListItem))
      {
        SqlCommand_ListItem.Parameters.AddWithValue("@ListItem_Id", "5383");
        DataTable DataTable_ListItem;
        using (DataTable_ListItem = new DataTable())
        {
          DataTable_ListItem.Locale = CultureInfo.CurrentCulture;
          DataTable_ListItem = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItem).Copy();
          if (DataTable_ListItem.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItem.Rows)
            {
              FromEmailAddress = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      string BodyString = EmailTemplate;

      BodyString = BodyString.Replace(";replace;PXMPDCHEscalationContactNameSurname;replace;", "" + pxmpdchEscalationContactNameSurname + "");
      BodyString = BodyString.Replace(";replace;CRMTypeName;replace;", "" + crmTypeName + "");
      BodyString = BodyString.Replace(";replace;PXMPDCHEscalationComment;replace;", "" + pxmpdchEscalationComment + "");
      BodyString = BodyString.Replace(";replace;PXMPDCHEscalationCreatedDate;replace;", "" + pxmpdchEscalationCreatedDate.Substring(0, 10).Replace("-", "/") + "");
      BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + crmReportNumber + "");

      string EmailBody = BodyString;

      string EmailSend = InfoQuest_All.All_SendEmail(FromEmailAddress, pxmpdchEscalationContactEmailAddress, "Life Healthcare Feedback / Enquiry Reference Number", EmailBody);

      if (string.IsNullOrEmpty(EmailSend) || !string.IsNullOrEmpty(EmailSend))
      {
        EmailBody = "";
      }

      EmailSend = "";
    }


    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDCH_Email()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCH");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");

                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;
                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }

                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;

                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDCH Email");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: Email: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCH");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCH Email, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCH Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCH Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCH Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDCH_USSD()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCH");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDCH USSD");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: USSD: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCH");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCH USSD, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCH USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCH USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCH USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDCM_Email()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        string PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        string PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        string PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCM");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDCM Email");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonEmail));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: Email: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCM");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCM Email, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCM Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCM Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCM Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDCM_USSD()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        string PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        string PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        string PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCM");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDCM USSD");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonEmail));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: USSD: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCM");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCM USSD, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCM USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCM USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCM USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }


    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDEU_Email()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEU");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Emergency Unit Survey : PDEU Email");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Emergency Unit Survey: Email: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEU");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEU Email, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEU Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEU Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEU Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDEU_USSD()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEU");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Emergency Unit Survey : PDEU USSD");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Emergency Unit Survey: USSD: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEU");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEU USSD, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEU USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEU USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEU USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDEM_Email()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        string PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        string PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        string PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEM");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Emergency Unit Survey : PDEM Email");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonEmail));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Emergency Unit Survey: Email: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEM");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEM Email, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEM Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEM Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEM Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDEM_USSD()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        string PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        string PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        string PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEM");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Emergency Unit Survey : PDEM USSD");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonEmail));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventEmergencyContactPersonMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Emergency Unit Survey: USSD: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDEM");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEM USSD, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEM USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDEM USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDEM USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }


    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDWS_Web()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string CRMTypeList = "";
        string PXMPDCHEscalationComment = "";
        string ContactPatient = "";
        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        //String PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        //String PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = "SELECT	DISTINCT " +
                              "        Administration_ListItem.ListItem_Id AS CRM_Type_List , " +
                              "        TempTableC.Q01 AS CRM_Type_Name , " +
                              "        TempTableC.Q03 AS PXM_PDCH_Escalation_Comment , " +
                              "        TempTableC.Q04 AS ContactPatient , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , " +
                              "        PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM		( " +
                              "  SELECT * FROM ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , CASE WHEN PXM_PDCH_Escalation_ValueLabel = 'Compliment' THEN PXM_PDCH_Escalation_ValueLabel WHEN PXM_PDCH_Escalation_ValueLabel = 'Complaint' THEN PXM_PDCH_Escalation_ValueLabel ELSE 'Comment' END AS PXM_PDCH_Escalation_ValueLabel , PXM_PDCH_Escalation_Label " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Label = 'Q01' " +
                              "    UNION " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Comment , PXM_PDCH_Escalation_Label " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Label = 'Q03' " +
                              "    UNION " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_ValueLabel , PXM_PDCH_Escalation_Label " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Label = 'Q04' " +
                              "  ) AS TempTableA PIVOT ( " +
                              "    MAX(PXM_PDCH_Escalation_ValueLabel) " +
                              "    FOR PXM_PDCH_Escalation_Label in ([Q01],[Q03],[Q04]) " +
                              "  ) AS TempTableB " +
                              ") AS TempTableC " +
                              "        LEFT JOIN Form_PXM_PDCH_Escalation ON TempTableC.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey " +
                              "        LEFT JOIN Administration_ListItem ON TempTableC.Q01 = Administration_ListItem.ListItem_Name " +
                              "WHERE		Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "        AND Q03 != '' " +
                              "        AND Administration_ListItem.ListCategory_Id = 110";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDWS");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                CRMTypeList = DataRow_Row_PXM["CRM_Type_List"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                ContactPatient = DataRow_Row_PXM["ContactPatient"].ToString();
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                //PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                //PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = " INSERT INTO Form_CRM " +
                                                     " ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName , CRM_CustomerEmail , CRM_CustomerContactNumber , CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Complaint_Description , CRM_Complaint_ContactPatient , CRM_Complaint_Unit_Id , CRM_Complaint_CloseOut , CRM_Compliment_Description , CRM_Compliment_ContactPatient , CRM_Compliment_Unit_Id , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Acknowledge , CRM_Comment_CloseOut , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) " +
                                                     " VALUES " +
                                                     " ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName , @CRM_CustomerEmail , @CRM_CustomerContactNumber , @CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Complaint_Description , @CRM_Complaint_ContactPatient , @CRM_Complaint_Unit_Id , @CRM_Complaint_CloseOut , @CRM_Compliment_Description , @CRM_Compliment_ContactPatient , @CRM_Compliment_Unit_Id , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Acknowledge , @CRM_Comment_CloseOut , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ) " +
                                                     " ; SELECT SCOPE_IDENTITY() ";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4419);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", CRMTypeList);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Website Survey : PDWS Web");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);

                    if (CRMTypeList == "4395")
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", "Website Survey: Web: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", ContactPatient);
                      if (string.IsNullOrEmpty(UnitId))
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      }
                      else
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", UnitId);
                      }
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    }
                    else if (CRMTypeList == "4406")
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", "Website Survey: Web: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", ContactPatient);
                      if (string.IsNullOrEmpty(UnitId))
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      }
                      else
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", UnitId);
                      }
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Website Survey: Web: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                      if (string.IsNullOrEmpty(UnitId))
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      }
                      else
                      {
                        SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                      }
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    }

                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT		CRM_Id , " +
                                                             "           Question , " +
                                                             "           Answer " +
                                                             " FROM			( " +
                                                             "   SELECT	DISTINCT " +
                                                             "           @CRMId AS CRM_Id , " +
                                                             "           PXM_PDCH_Escalation_SurveyKey , " +
                                                             "           PXM_PDCH_Escalation_Label , " +
                                                             "           REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , " +
                                                             "           CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM		Form_PXM_PDCH_Escalation " +
                                                             "   WHERE		PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID " +
                                                             "           AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser " +
                                                             "           AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel " +
                                                             "           AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL " +
                                                             "           AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '') " +
                                                             "           AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY	PXM_PDCH_Escalation_SurveyKey , " +
                                                             "           PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDWS");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDWS Web, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDWS Web into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDWS Web into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDWS Web", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDCU_Web()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string CRMTypeList = "";
        string CRMTypeName = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationCreatedDate = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactNameSurname = "";
        string PXMPDCHEscalationContactSurnameName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";

        string SQLStringPXM = "SELECT	DISTINCT " +
                              "        Administration_ListItem.ListItem_Id AS CRM_Type_List , " +
                              "        TempTableC.[Q.A] AS CRM_Type_Name , " +
                              "        TempTableC.[Q20] AS PXM_PDCH_Escalation_Comment , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CreatedDate , " +
                              "        CASE " +
                              "          WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode != '' THEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode " +
                              "          WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode = '' AND TempTableC.[Please select a hospital.] IS NOT NULL THEN TempTableC.[Please select a hospital.] " +
                              "          WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode = '' AND TempTableC.[Please select a hospital.] IS NULL THEN 'H01' " +
                              "        END AS PXM_PDCH_Escalation_BusinessUnitHospitalCode , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , " +
                              "        CASE WHEN PXM_PDCH_Escalation_ContactPatientTitle = '' THEN '' ELSE ' ' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname = '' THEN '' ELSE ' ' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname AS PXM_PDCH_Escalation_ContactNameSurname , " +
                              "        CASE WHEN PXM_PDCH_Escalation_ContactPatientTitle = '' THEN '' ELSE ' ' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactSurnameName , " +
                              "        Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate " +
                              "FROM		( " +
                              "          SELECT	* " +
                              "          FROM		( " +
                              "                    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_ValueLabel , PXM_PDCH_Escalation_Label " +
                              "                    FROM Form_PXM_PDCH_Escalation " +
                              "                    WHERE PXM_PDCH_Escalation_Label = 'Q.A' " +
                              "                    UNION " +
                              "                    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Comment , PXM_PDCH_Escalation_Label " +
                              "                    FROM Form_PXM_PDCH_Escalation " +
                              "                    WHERE PXM_PDCH_Escalation_Label = 'Q20' " +
                              "                    UNION " +
                              "                    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_ValueLabel , PXM_PDCH_Escalation_Text " +
                              "                    FROM Form_PXM_PDCH_Escalation " +
                              "                    WHERE PXM_PDCH_Escalation_Text = 'Please select a hospital.' " +
                              "                    ) AS TempTableA PIVOT ( " +
                              "                    MAX(PXM_PDCH_Escalation_ValueLabel) " +
                              "                    FOR PXM_PDCH_Escalation_Label in ([Q.A],[Q20],[Please select a hospital.]) " +
                              "                  ) AS TempTableB " +
                              "        ) AS TempTableC " +
                              "        LEFT JOIN Form_PXM_PDCH_Escalation ON TempTableC.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey " +
                              "        LEFT JOIN Administration_ListItem ON CASE WHEN TempTableC.[Q.A] = 'Compliment' THEN TempTableC.[Q.A] WHEN TempTableC.[Q.A] = 'Complaint' THEN TempTableC.[Q.A] WHEN TempTableC.[Q.A] = 'Other' THEN 'Comment' ELSE 'Enquiry' END = Administration_ListItem.ListItem_Name " +
                              "WHERE		Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL " +
                              "        AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "        AND [Q20] != '' " +
                              "        AND Administration_ListItem.ListCategory_Id = 110";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCU");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                CRMTypeList = DataRow_Row_PXM["CRM_Type_List"].ToString();
                CRMTypeName = DataRow_Row_PXM["CRM_Type_Name"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationCreatedDate = DataRow_Row_PXM["PXM_PDCH_Escalation_CreatedDate"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactNameSurname = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactNameSurname"].ToString();
                PXMPDCHEscalationContactSurnameName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactSurnameName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                string FacilityFacilityDisplayName = FromDataBase_Facility_Current.FacilityFacilityDisplayName;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");

                  string SQLStringInsertCRMComment = " INSERT INTO Form_CRM " +
                                                     " ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName , CRM_CustomerEmail , CRM_CustomerContactNumber , CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Complaint_Description , CRM_Complaint_ContactPatient , CRM_Complaint_Unit_Id , CRM_Complaint_CloseOut , CRM_Compliment_Description , CRM_Compliment_ContactPatient , CRM_Compliment_Unit_Id , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Acknowledge , CRM_Comment_CloseOut , CRM_Query_Description , CRM_Query_ContactPatient , CRM_Query_Unit_Id , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) " +
                                                     " VALUES " +
                                                     " ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName , @CRM_CustomerEmail , @CRM_CustomerContactNumber , @CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Complaint_Description , @CRM_Complaint_ContactPatient , @CRM_Complaint_Unit_Id , @CRM_Complaint_CloseOut , @CRM_Compliment_Description , @CRM_Compliment_ContactPatient , @CRM_Compliment_Unit_Id , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Acknowledge , @CRM_Comment_CloseOut , @CRM_Query_Description , @CRM_Query_ContactPatient , @CRM_Query_Unit_Id , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ) " +
                                                     " ; SELECT SCOPE_IDENTITY() ";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4419);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", CRMTypeList);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 5388);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 5387);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Website Contact Us : PDCU Web");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactSurnameName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);

                    if (CRMTypeList == "4395") //Complaint
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", "Website Contact Us: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + "");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", "Yes");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    }
                    else if (CRMTypeList == "4406") //Compliment
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", "Website Contact Us: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + "");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", "Yes");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    }
                    else if (CRMTypeList == "4412") //Comment
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Website Contact Us: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + "");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", "Yes");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    }
                    else //Enquiry
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);

                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Description", "Website Contact Us: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + "");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_ContactPatient", "Yes");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    }

                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT		CRM_Id , " +
                                                             "           Question , " +
                                                             "           Answer " +
                                                             " FROM			( " +
                                                             "             SELECT	CRM_Id , " +
                                                             "                     Question , " +
                                                             "                     CASE WHEN Facility_FacilityDisplayName IS NULL THEN Answer ELSE Facility_FacilityDisplayName END AS Answer " +
                                                             "             FROM		( " +
                                                             "                       SELECT	DISTINCT " +
                                                             "                               @CRMId AS CRM_Id , " +
                                                             "                               PXM_PDCH_Escalation_SurveyKey , " +
                                                             "                               PXM_PDCH_Escalation_Label , " +
                                                             "                               REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , " +
                                                             "                               CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Comment ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "                       FROM		Form_PXM_PDCH_Escalation " +
                                                             "                       WHERE		PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID " +
                                                             "                               AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser " +
                                                             "                               AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel " +
                                                             "                               AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL " +
                                                             "                               AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '' OR PXM_PDCH_Escalation_Comment <> '') " +
                                                             "                               AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             "                     ) AS TempTable1 " +
                                                             "                     LEFT JOIN vAdministration_Facility_All ON Answer = Facility_FacilityCode " +
                                                             "           ) AS TempTable2 " +
                                                             " ORDER BY	Question";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDCU");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    PXM_PDCH_Escalation_SendEmail_WebsiteContactUs_SecurityUsers(LastCRMId, CRMTypeName, CRM_ReportNumber, FacilityId, FacilityFacilityDisplayName);
                    PXM_PDCH_Escalation_SendEmail_WebsiteContactUs_Customer(CRMTypeName, CRM_ReportNumber, PXMPDCHEscalationContactEmailAddress, PXMPDCHEscalationContactNameSurname, PXMPDCHEscalationComment, PXMPDCHEscalationCreatedDate);
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCU Web, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCU Web into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDCU Web into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDCU Web", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }


    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDRB_Email()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDRB");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");

                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;
                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }

                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;

                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDRB Email");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: Email: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDRB");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "EMAIL");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRB Email, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDRB Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDRB Email into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRB Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDRB_USSD()
    {
      try
      {
        PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_Current = PXM_PDCH_GetSystemAccount();
        string SystemAccountDomain = FromDataBase_SystemAccount_Current.SystemAccountDomain;
        string SystemAccountUserName = FromDataBase_SystemAccount_Current.SystemAccountUserName;

        string SystemAccount = SystemAccountDomain + "\\" + SystemAccountUserName;

        string PXMPDCHEscalationSurveyKey = "";
        string PXMPDCHEscalationComment = "";
        string PXMPDCHEscalationBusinessUnitHospitalCode = "";
        string PXMPDCHEscalationContactEmailAddress = "";
        string PXMPDCHEscalationContactPatientMobileNumber = "";
        string PXMPDCHEscalationContactName = "";
        string PXMPDCHEscalationEventAdmissionDate = "";
        string PXMPDCHEscalationEventDischargeWard = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonEmail = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = "";
        //String PXMPDCHEscalationEventEmergencyContactPersonName = "";
        string PXMPDCHEscalationEventVisitNumber = "";

        string SQLStringPXM = ";WITH	CTE_Table AS ( " +
                              "    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              "    FROM Form_PXM_PDCH_Escalation " +
                              "    WHERE PXM_PDCH_Escalation_Comment <> '' " +
                              "    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              //"  UNION " +
                              //"    SELECT PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label , ROW_NUMBER() OVER( PARTITION BY PXM_PDCH_Escalation_SurveyKey ORDER BY PXM_PDCH_Escalation_SurveyKey DESC ) AS RowNumber " +
                              //"    FROM Form_PXM_PDCH_Escalation " +
                              //"    WHERE PXM_PDCH_Escalation_Comment = '' AND PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT PXM_PDCH_Escalation_SurveyKey FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Comment <> '' ) " +
                              //"    GROUP BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label " +
                              ") " +
                              "SELECT DISTINCT Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Comment , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactEmailAddress , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientMobileNumber , PXM_PDCH_Escalation_ContactPatientTitle + ' ' + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_ContactPatientFirstname AS PXM_PDCH_Escalation_ContactName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventAdmissionDate , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventDischargeWard , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonEmail , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname + CASE WHEN Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonSurname = '' THEN '' ELSE ',' END + Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEmergencyContactPersonFirstname AS PXM_PDCH_Escalation_EventEmergencyContactPersonName , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventVisitNumber " +
                              "FROM CTE_Table LEFT JOIN Form_PXM_PDCH_Escalation ON CTE_Table.PXM_PDCH_Escalation_SurveyKey = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey AND CTE_Table.PXM_PDCH_Escalation_Label = Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_Label " +
                              "WHERE RowNumber = 1 AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey NOT IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_UploadedFromReferenceNumber IS NOT NULL ) " +
                              "ORDER BY Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_BusinessUnitHospitalCode , Form_PXM_PDCH_Escalation.PXM_PDCH_Escalation_SurveyKey";
        using (SqlCommand SqlCommand_PXM = new SqlCommand(SQLStringPXM))
        {
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDRB");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
          SqlCommand_PXM.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
          DataTable DataTable_PXM;
          using (DataTable_PXM = new DataTable())
          {
            DataTable_PXM.Locale = CultureInfo.CurrentCulture;
            DataTable_PXM = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM).Copy();
            if (DataTable_PXM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row_PXM in DataTable_PXM.Rows)
              {
                PXMPDCHEscalationSurveyKey = DataRow_Row_PXM["PXM_PDCH_Escalation_SurveyKey"].ToString();
                PXMPDCHEscalationComment = DataRow_Row_PXM["PXM_PDCH_Escalation_Comment"].ToString();
                PXMPDCHEscalationBusinessUnitHospitalCode = DataRow_Row_PXM["PXM_PDCH_Escalation_BusinessUnitHospitalCode"].ToString();
                PXMPDCHEscalationContactEmailAddress = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactEmailAddress"].ToString();
                PXMPDCHEscalationContactPatientMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactPatientMobileNumber"].ToString();
                PXMPDCHEscalationContactName = DataRow_Row_PXM["PXM_PDCH_Escalation_ContactName"].ToString();
                PXMPDCHEscalationEventAdmissionDate = DataRow_Row_PXM["PXM_PDCH_Escalation_EventAdmissionDate"].ToString();
                PXMPDCHEscalationEventDischargeWard = DataRow_Row_PXM["PXM_PDCH_Escalation_EventDischargeWard"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonEmail = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonEmail"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonMobileNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonMobileNumber"].ToString();
                //PXMPDCHEscalationEventEmergencyContactPersonName = DataRow_Row_PXM["PXM_PDCH_Escalation_EventEmergencyContactPersonName"].ToString();
                PXMPDCHEscalationEventVisitNumber = DataRow_Row_PXM["PXM_PDCH_Escalation_EventVisitNumber"].ToString();

                PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_Current = PXM_PDCH_GetFacility(PXMPDCHEscalationBusinessUnitHospitalCode);
                string FacilityId = FromDataBase_Facility_Current.FacilityId;
                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRM_ReportNumber = InfoQuest_All.All_ReportNumber(SystemAccount, FacilityId, "36");


                  PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_Current = PXM_PDCH_GetContactPatient(PXMPDCHEscalationSurveyKey);
                  string ContactPatient = FromDataBase_ContactPatient_Current.ContactPatient;

                  if (string.IsNullOrEmpty(ContactPatient))
                  {
                    ContactPatient = "Yes";
                  }


                  PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_Current = PXM_PDCH_GetUnitId(FacilityId, PXMPDCHEscalationEventDischargeWard);
                  string UnitId = FromDataBase_UnitId_Current.UnitId;


                  string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_ContactPatient , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_ContactPatient , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                  string LastCRMId = "";
                  using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                  {
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4435);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4396);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Post Discharge Survey : PDRB USSD");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationSurveyKey));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventVisitNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactName));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventAdmissionDate));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactEmailAddress));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", InfoQuest_All.All_FixInputString(PXMPDCHEscalationContactPatientMobileNumber));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Post Discharge Survey: USSD: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationComment) + " " + Environment.NewLine + "Unit: " + InfoQuest_All.All_FixInputString(PXMPDCHEscalationEventDischargeWard));
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_ContactPatient", ContactPatient);
                    if (string.IsNullOrEmpty(UnitId))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                    }
                    else
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", UnitId);
                    }
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", SystemAccount);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                    SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                    LastCRMId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                  }


                  if (!string.IsNullOrEmpty(LastCRMId))
                  {
                    string SQLStringInsertCRMPXMPDCHResult = "INSERT INTO Form_CRM_PXM_PDCH_Result ( " +
                                                             "   CRM_Id , " +
                                                             "   CRM_PXM_PDCH_Result_Question , " +
                                                             "   CRM_PXM_PDCH_Result_Answer " +
                                                             " ) " +
                                                             " SELECT CRM_Id , Question , Answer " +
                                                             " FROM ( " +
                                                             "   SELECT DISTINCT @CRMId AS CRM_Id , PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label ,	REPLACE(REPLACE(REPLACE(REPLACE(PXM_PDCH_Escalation_Text, '[PDCH.HospitalCode.Name]' , PXM_PDCH_Escalation_BusinessUnitHospital ), 'CHAR(13) + CHAR(10) ', ' ' ), '{Name}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'you' ELSE PXM_PDCH_Escalation_ContactPatientFirstname END ), '{Name''s}', CASE WHEN PXM_PDCH_Escalation_EventEventID IN ('PDCH','PDEU') THEN 'your' ELSE PXM_PDCH_Escalation_ContactPatientFirstname + '''s' END ) AS Question , CASE WHEN PXM_PDCH_Escalation_ValueLabel = '' THEN PXM_PDCH_Escalation_Value WHEN PXM_PDCH_Escalation_ValueLabel = PXM_PDCH_Escalation_Value THEN PXM_PDCH_Escalation_Value ELSE PXM_PDCH_Escalation_ValueLabel END AS Answer " +
                                                             "   FROM Form_PXM_PDCH_Escalation " +
                                                             "   WHERE PXM_PDCH_Escalation_EventEventID = @PXM_PDCH_Escalation_EventEventID AND PXM_PDCH_Escalation_InfoQuestUploadUser = @PXM_PDCH_Escalation_InfoQuestUploadUser AND PXM_PDCH_Escalation_CommunicationChannelPreferredChannel = @PXM_PDCH_Escalation_CommunicationChannelPreferredChannel AND PXM_PDCH_Escalation_InfoQuestUploadDate IS NOT NULL AND (PXM_PDCH_Escalation_Value <> '' OR PXM_PDCH_Escalation_ValueLabel <> '')	AND PXM_PDCH_Escalation_SurveyKey IN ( SELECT DISTINCT CRM_UploadedFromReferenceNumber FROM Form_CRM WHERE CRM_Id = @CRMId ) " +
                                                             " ) AS TempTable " +
                                                             " ORDER BY PXM_PDCH_Escalation_SurveyKey , PXM_PDCH_Escalation_Label";
                    using (SqlCommand SqlCommand_InsertCRMPXMPDCHResult = new SqlCommand(SQLStringInsertCRMPXMPDCHResult))
                    {
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_EventEventID", "PDRB");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_InfoQuestUploadUser", "WCF");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@PXM_PDCH_Escalation_CommunicationChannelPreferredChannel", "USSD");
                      SqlCommand_InsertCRMPXMPDCHResult.Parameters.AddWithValue("@CRMId", LastCRMId);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMPXMPDCHResult);
                    }


                    string SQLStringDeletePXMPDCH = "DELETE FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
                    using (SqlCommand SqlCommand_DeletePXMPDCH = new SqlCommand(SQLStringDeletePXMPDCH))
                    {
                      SqlCommand_DeletePXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", PXMPDCHEscalationSurveyKey);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePXMPDCH);
                    }


                    if (PXM_PDCH_Escalation_EmailFacility != FacilityId)
                    {
                      PXM_PDCH_Escalation_EmailFacility = FacilityId;
                      PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(FacilityId);
                    }
                  }
                }
                else
                {
                  PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRB USSD, Hospital Code: " + PXMPDCHEscalationBusinessUnitHospitalCode + " not added as facility", CultureInfo.CurrentCulture));
                  PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
                }
              }

              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDRB USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
            else
            {
              PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing successful for PDRB USSD into CRM, Forms added: " + DataTable_PXM.Rows.Count.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRB USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_Email()
    {
      try
      {
        //Rehab Survey : PDRM Email
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRM Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_USSD()
    {
      try
      {
        //Rehab Survey : PDRM USSD
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDRM USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }


    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_Email()
    {
      try
      {
        //Mental Health Survey : PDMH Email
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDMH Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_USSD()
    {
      try
      {
        //Mental Health Survey : PDMH USSD
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDMH USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_Email()
    {
      try
      {
        //Mental Health Survey : PDMM Email
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDMM Email", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private void PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_USSD()
    {
      try
      {
        //Mental Health Survey : PDMM USSD
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Data importing failed for PDMM USSD", CultureInfo.CurrentCulture));
          PXM_PDCH_EscalationExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }


    string PXM_PDCH_EscalationExportDataAutomated_Successful = "Yes";
    string PXM_PDCH_Escalation_ImportData = "Yes";
    string PXM_PDCH_Escalation_DeleteFiles = "Yes";
    string PXM_PDCH_Escalation_EmailFacility = "";

    private static Dictionary<string, string> PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Escalation_ExportData_Automated_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Escalation_ExportData_Automated: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandlers(string facilityId)
    {
      string FacilityId = "";
      string FacilityFacilityDisplayName = "";
      string SQLStringFacility = "SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_All WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_Facility in DataTable_Facility.Rows)
            {
              FacilityId = DataRow_Row_Facility["Facility_Id"].ToString();
              FacilityFacilityDisplayName = DataRow_Row_Facility["Facility_FacilityDisplayName"].ToString();
            }
          }
        }
      }

      if (!PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler.ContainsKey(FacilityId))
      {
        PXM_PDCH_Escalation_ExportData_Automated_EmailFacilityIdHandler.Add(FacilityId, FacilityFacilityDisplayName);
      }
    }
    //---END--- --PXM PDCH Escalation ExportData--//
    //---END--- --PXM PDCH Escalation--//



    //--START-- --PXM PDCH Report--//
    //--START-- --PXM PDCH Report CheckFileProcessing--//
    public string PXM_PDCH_Report_CheckFileProcessing(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler.Clear();

        string SQLStringFile = "SELECT	TempTableA.FileToProcess , " +
                               "         TempTableB.FileProcessed " +
                               " FROM		( " +
                               "           SELECT 'PXM PDCH Report' AS FileToProcess , 'PXM PDCHReport' AS FileProcessed " + //UNION " +
                                                                                                                             //"           SELECT 'PXM PDEU Report' AS FileToProcess , 'PXM PDEUReport' AS FileProcessed " +
                               "         ) AS TempTableA " +
                               "         LEFT JOIN " +
                               "         ( " +
                               "           SELECT DISTINCT SUBSTRING(PXM_PDCH_Report_FileUploaded_FileName,0,CHARINDEX('-',PXM_PDCH_Report_FileUploaded_FileName)) AS FileProcessed " +
                               "           FROM Form_PXM_PDCH_Report_FileUploaded " +
                               "           WHERE PXM_PDCH_Report_FileUploaded_CurrentDate >= CAST(CONVERT(NVARCHAR(MAX), DATEADD(DAY, -30, GETDATE()), 110) AS DATETIME) " +
                               "         ) AS TempTableB " +
                               "         ON TempTableA.FileProcessed = TempTableB.FileProcessed";
        using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
        {
          DataTable DataTable_File;
          using (DataTable_File = new DataTable())
          {
            DataTable_File.Locale = CultureInfo.CurrentCulture;
            DataTable_File = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_File).Copy();
            if (DataTable_File.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_File.Rows)
              {
                string FileToProcess = DataRow_Row["FileToProcess"].ToString();
                string FileProcessed = DataRow_Row["FileProcessed"].ToString();

                if (string.IsNullOrEmpty(FileProcessed))
                {
                  PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString(FileToProcess + " file not received in the last 30 days", CultureInfo.CurrentCulture));
                  PXM_PDCH_ReportCheckFileProcessing_Successful = "No";
                }
              }
            }
            else
            {
              PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString("No PXM Report file received in the last 30 days", CultureInfo.CurrentCulture));
              PXM_PDCH_ReportCheckFileProcessing_Successful = "No";
            }
          }
        }

        if (PXM_PDCH_EscalationCheckFileProcessing_Successful == "Yes")
        {
          PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandlers(Convert.ToString("PXM Report file received in the last 30 days", CultureInfo.CurrentCulture));
        }

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_ReportCheckFileProcessing_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_ReportCheckFileProcessing_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    string PXM_PDCH_ReportCheckFileProcessing_Successful = "Yes";

    private static Dictionary<string, string> PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Report_CheckFileProcessing_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Report_CheckFileProcessing: " + ReturnMessage);
      }
    }
    //---END--- --PXM PDCH Report CheckFileProcessing--//


    //--START-- --PXM PDCH Report ExportData--//
    public string PXM_PDCH_Report_ExportData_Automated(string userName, string password)
    {
      bool AccessValid = InfoQuest_Security.Security_WCF(System.Reflection.MethodBase.GetCurrentMethod().Name, userName, password);

      if (AccessValid == true)
      {
        PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler.Clear();

        string ExportPath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\PXM_PDCH_Report\";

        try
        {
          PXM_PDCH_FromDataBase_Impersonation FromDataBase_Impersonation_Current = PXM_PDCH_GetImpersonation();
          string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
          string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
          string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

          if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
          {
            if (Directory.Exists(ExportPath))
            {
              string[] UploadedFiles = Directory.GetFiles(ExportPath, "*.*", SearchOption.AllDirectories);

              if (UploadedFiles.Length > 0)
              {
                foreach (string Files in Directory.GetFiles(ExportPath, "*.*", SearchOption.AllDirectories))
                {
                  PXM_PDCH_Report_DeleteFiles = "Yes";
                  bool ProcessFile = InfoQuest_All.All_FileAccessible(Files);
                  string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);

                  if (ProcessFile == true)
                  {
                    string FileExtension = FileName.Substring(FileName.LastIndexOf('.') + 1);

                    if (FileExtension == "txt")
                    {
                      if (UploadedFiles.Length == 1 && FileName == "placeholder.txt")
                      {
                        PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("No Files at " + ExportPath + " , only placeholder.txt file", CultureInfo.CurrentCulture));
                        PXM_PDCH_ReportExportDataAutomated_Successful = "No";
                      }
                      else if (UploadedFiles.Length >= 1 && FileName != "placeholder.txt")
                      {
                        DataTable DataTable_File;
                        using (DataTable_File = new DataTable())
                        {
                          DataTable_File.Locale = CultureInfo.CurrentCulture;
                          DataTable_File = PXM_PDCH_Report_ExtractData(FileName, ExportPath).Copy();

                          if (DataTable_File.Columns.Count == 0)
                          {
                            PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format, data extraction failed for file name: " + FileName, CultureInfo.CurrentCulture));
                            PXM_PDCH_ReportExportDataAutomated_Successful = "No";
                            PXM_PDCH_Report_DeleteFiles = "No";
                          }
                          else
                          {
                            string InvalidColumns = InfoQuest_All.All_FileInvalidColumns(DataTable_File, "", "Form_PXM_PDCH_Report");

                            if (string.IsNullOrEmpty(InvalidColumns))
                            {
                              PXM_PDCH_Report_ProcessData(DataTable_File, FileName);

                              PXM_PDCH_Report_DeleteFile(DataTable_File, FileName, ExportPath);
                            }
                            else
                            {
                              PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File " + FileName + " could not be processed, column: " + InvalidColumns + " do not match mapping table", CultureInfo.CurrentCulture));
                              PXM_PDCH_ReportExportDataAutomated_Successful = "No";
                            }
                          }
                        }
                      }
                    }
                  }
                  else
                  {
                    PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File " + FileName + " could not be processed", CultureInfo.CurrentCulture));
                    PXM_PDCH_ReportExportDataAutomated_Successful = "No";
                  }
                }
              }
              else
              {
                PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("No Files at " + ExportPath, CultureInfo.CurrentCulture));
                //PXM_PDCH_ReportExportDataAutomated_Successful = "No";
              }
            }
            else
            {
              PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Folder could not be accessed at " + ExportPath, CultureInfo.CurrentCulture));
              PXM_PDCH_ReportExportDataAutomated_Successful = "No";
            }

            InfoQuest_Impersonate.UndoImpersonation();
          }
          else
          {
            PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("User Impersination Failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture));
            PXM_PDCH_ReportExportDataAutomated_Successful = "No";
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            InfoQuest_Impersonate.UndoImpersonation();

            PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File could not be accessed at " + ExportPath, CultureInfo.CurrentCulture));
            PXM_PDCH_ReportExportDataAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }

        PXM_PDCH_Report_ImportData_CleanUp();

        string ReturnMessage = "";
        foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler)
        {
          ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
        }

        if (PXM_PDCH_ReportExportDataAutomated_Successful == "No")
        {
          InfoQuest_WCF.WCF_SendEmail_Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }
        else if (PXM_PDCH_ReportExportDataAutomated_Successful == "Yes")
        {
          //InfoQuest_WCF.WCF_SendEmail_Successful(System.Reflection.MethodBase.GetCurrentMethod().Name, ReturnMessage);
        }

        PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler.Clear();

        return ReturnMessage;
      }
      else
      {
        return "Access Denied";
      }
    }

    private DataTable PXM_PDCH_Report_ExtractData(string fileName, string exportPath)
    {
      Int32 RowNumber = 1;
      DataTable DataTable_File;
      using (DataTable_File = new DataTable())
      {
        DataTable_File.Locale = CultureInfo.CurrentCulture;

        try
        {
          using (StreamReader StreamReader_File = new StreamReader(exportPath + fileName, true))
          {
            DateTime CurrentDateTime = DateTime.Now;

            while (!StreamReader_File.EndOfStream)
            {
              string FileLine = StreamReader_File.ReadLine();
              if (!string.IsNullOrEmpty(FileLine.Trim()))
              {
                if (RowNumber == 1)
                {
                  DataTable_File.Columns.Add("PXM_PDCH_Report_Id");

                  string[] ColumnNames = FileLine.Split("|".ToCharArray(), StringSplitOptions.None);
                  foreach (string ColumnName in ColumnNames)
                  {
                    string ColumnNameNew = ColumnName;
                    ColumnNameNew = ColumnNameNew.Replace(" ", "");
                    ColumnNameNew = ColumnNameNew.Replace(".", "");
                    ColumnNameNew = "PXM_PDCH_Report_" + ColumnNameNew;

                    DataTable_File.Columns.Add(ColumnNameNew);
                  }

                  DataTable_File.Columns.Add("PXM_PDCH_Report_InfoQuestUploadUser");
                  DataTable_File.Columns.Add("PXM_PDCH_Report_InfoQuestUploadFrom");
                  DataTable_File.Columns.Add("PXM_PDCH_Report_InfoQuestUploadDate");
                }
                else if (RowNumber > 1)
                {
                  FileLine = RowNumber + "|" + FileLine + "|WCF|Automated|" + CurrentDateTime;
                  string[] Data = FileLine.Split("|".ToCharArray(), StringSplitOptions.None);
                  DataTable_File.Rows.Add(Data);
                }

                RowNumber = RowNumber + 1;
              }
            }
          }
        }
        catch (Exception Exception_Error)
        {
          PXM_PDCH_Report_DeleteFiles = "No";

          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format for file name: " + fileName + ", Error was on Row Number: " + RowNumber.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture));
            PXM_PDCH_ReportExportDataAutomated_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            DataTable_File.Clear();
          }
          else
          {
            throw;
          }
        }
      }

      return DataTable_File;
    }

    private void PXM_PDCH_Report_ProcessData(DataTable dataTable_File, string fileName)
    {
      if (PXM_PDCH_Report_DeleteFiles == "Yes")
      {
        if (dataTable_File.Columns.Count > 1)
        {
          if (dataTable_File.Rows.Count > 0)
          {
            string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
            using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
            {
              SqlConnection_BulkCopy.Open();

              using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
              {
                SqlBulkCopy_File.DestinationTableName = "Form_PXM_PDCH_Report";

                foreach (DataColumn DataColumn_ColumnNames in dataTable_File.Columns)
                {
                  string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_PDCH_Report') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
                  using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
                  {
                    SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
                    DataTable DataTable_Column;
                    using (DataTable_Column = new DataTable())
                    {
                      DataTable_Column.Locale = CultureInfo.CurrentCulture;
                      DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Column).Copy();
                      if (DataTable_Column.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row_FacilityId in DataTable_Column.Rows)
                        {
                          string name = DataRow_Row_FacilityId["name"].ToString();

                          SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                          SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                        }
                      }
                    }
                  }
                }


                try
                {
                  SqlBulkCopy_File.WriteToServer(dataTable_File);
                  PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data successful for file name: " + fileName, CultureInfo.CurrentCulture));
                }
                catch (Exception Exception_Error)
                {
                  PXM_PDCH_Report_DeleteFiles = "No";

                  if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                  {
                    PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data failed for file name: " + fileName, CultureInfo.CurrentCulture));
                    PXM_PDCH_ReportExportDataAutomated_Successful = "No";
                    InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
                  }
                  else
                  {
                    throw;
                  }
                }
              }
            }
          }
          else
          {
            PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("Process data successful for file name: " + fileName, CultureInfo.CurrentCulture));
          }
        }
        else
        {
          PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File is in the wrong format for file name: " + fileName, CultureInfo.CurrentCulture));
          PXM_PDCH_ReportExportDataAutomated_Successful = "No";
          PXM_PDCH_Report_DeleteFiles = "No";
        }
      }
    }

    private void PXM_PDCH_Report_DeleteFile(DataTable dataTable_File, string fileName, string exportPath)
    {
      try
      {
        if (PXM_PDCH_Report_DeleteFiles == "Yes")
        {
          string TXTFileName = fileName;
          string ZIPFileName = fileName.Replace(".txt", ".zip");

          string TXTFilePathAndName = exportPath + TXTFileName;
          string ZIPFilePathAndName = exportPath + ZIPFileName;

          using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
          {
            ZipArchive_PathAndName.CreateEntryFromFile(TXTFilePathAndName, TXTFileName);
          }

          using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
          {
            string ZIPFileContentType = "application/zip";
            BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
            Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

            string SQLStringInsertPXMPDCHReportFileUploaded = "INSERT INTO Form_PXM_PDCH_Report_FileUploaded ( PXM_PDCH_Report_FileUploaded_FileName , PXM_PDCH_Report_FileUploaded_ZipFileName , PXM_PDCH_Report_FileUploaded_ContentType , PXM_PDCH_Report_FileUploaded_Data , PXM_PDCH_Report_FileUploaded_Records , PXM_PDCH_Report_FileUploaded_CurrentDate , PXM_PDCH_Report_FileUploaded_From ) VALUES ( @PXM_PDCH_Report_FileUploaded_FileName , @PXM_PDCH_Report_FileUploaded_ZipFileName , @PXM_PDCH_Report_FileUploaded_ContentType , @PXM_PDCH_Report_FileUploaded_Data , @PXM_PDCH_Report_FileUploaded_Records , @PXM_PDCH_Report_FileUploaded_CurrentDate , @PXM_PDCH_Report_FileUploaded_From )";
            using (SqlCommand SqlCommand_InsertPXMPDCHReportFileUploaded = new SqlCommand(SQLStringInsertPXMPDCHReportFileUploaded))
            {
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_FileName", TXTFileName);
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_ZipFileName", ZIPFileName);
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_ContentType", ZIPFileContentType);
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_Data", Byte_ZIPFile);
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_Records", dataTable_File.Rows.Count.ToString(CultureInfo.CurrentCulture));
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_CurrentDate", DateTime.Now);
              SqlCommand_InsertPXMPDCHReportFileUploaded.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_From", "Automated");
              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPXMPDCHReportFileUploaded);
            }
          }

          File.Delete(TXTFilePathAndName);
          File.Delete(ZIPFilePathAndName);
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(Convert.ToString("File deletion failed for file name: " + fileName, CultureInfo.CurrentCulture));
          PXM_PDCH_ReportExportDataAutomated_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }
    }

    private static void PXM_PDCH_Report_ImportData_CleanUp()
    {
      string SQLStringCleanUpPXMPDCH = "DELETE FROM Form_PXM_PDCH_Report WHERE PXM_PDCH_Report_Id IN ( SELECT MAX(PXM_PDCH_Report_Id) FROM Form_PXM_PDCH_Report TableA WHERE EXISTS ( SELECT PXM_PDCH_Report_BusinessUnitHospitalCode , PXM_PDCH_Report_EventVisitNumber FROM Form_PXM_PDCH_Report TableB GROUP BY PXM_PDCH_Report_BusinessUnitHospitalCode , PXM_PDCH_Report_EventVisitNumber HAVING COUNT(PXM_PDCH_Report_EventVisitNumber) > 1 AND TableA.PXM_PDCH_Report_BusinessUnitHospitalCode = TableB.PXM_PDCH_Report_BusinessUnitHospitalCode AND TableA.PXM_PDCH_Report_EventVisitNumber = TableB.PXM_PDCH_Report_EventVisitNumber ) AND (PXM_PDCH_Report_InfoQuestUploadUser = @PXM_PDCH_Report_InfoQuestUploadUser OR PXM_PDCH_Report_InfoQuestUploadDate IS NULL) GROUP BY TableA.PXM_PDCH_Report_BusinessUnitHospitalCode , TableA.PXM_PDCH_Report_EventVisitNumber HAVING COUNT(PXM_PDCH_Report_EventVisitNumber) > 1 UNION SELECT PXM_PDCH_Report_Id FROM Form_PXM_PDCH_Report WHERE PXM_PDCH_Report_BusinessUnitHospital = '' OR PXM_PDCH_Report_BusinessUnitHospitalCode = '' OR PXM_PDCH_Report_EventVisitNumber = '' )";
      using (SqlCommand SqlCommand_CleanUpPXMPDCH = new SqlCommand(SQLStringCleanUpPXMPDCH))
      {
        SqlCommand_CleanUpPXMPDCH.Parameters.AddWithValue("@PXM_PDCH_Report_InfoQuestUploadUser", "WCF");
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CleanUpPXMPDCH);
      }
    }


    string PXM_PDCH_ReportExportDataAutomated_Successful = "Yes";
    string PXM_PDCH_Report_DeleteFiles = "Yes";

    private static Dictionary<string, string> PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_PDCH_Report_ExportData_Automated_ReturnMessageHandler.Add(ReturnMessage, "PXM_PDCH_Report_ExportData_Automated: " + ReturnMessage);
      }
    }
    //---END--- --PXM PDCH Report ExportData--//
    //---END--- --PXM PDCH Report--//



    //--START-- --PXM PDCH--//
    #region CreateFile_CSV --Future Expansion--
    //private static String PXM_PDCH_CreateFile_CSV(DataTable dataTable_File, Boolean automated, String shareFolder, String fileName, String eventType, DateTime currentDate, DateTime startDate, DateTime endDate, String facilityId)
    //{
    //  String ReturnMessage = "";

    //  if (dataTable_File == null)
    //  {
    //    ReturnMessage = Convert.ToString("DataTable is empty", CultureInfo.CurrentCulture);
    //  }
    //  else
    //  {
    //    String SavePath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\" + shareFolder + @"\" + fileName + "-" + currentDate.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + ".csv";

    //    try
    //    {
    //      FromDataBase_Impersonation FromDataBase_Impersonation_Current = GetImpersonation();
    //      String ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
    //      String ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
    //      String ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

    //      if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
    //      {
    //        using (StreamWriter StreamWriter_File = new StreamWriter(SavePath, true))
    //        {
    //          int ColumnCount = dataTable_File.Columns.Count;
    //          for (int i = 0; i < ColumnCount; i++)
    //          {
    //            StreamWriter_File.Write(dataTable_File.Columns[i].ToString());

    //            if (i < ColumnCount - 1)
    //            {
    //              StreamWriter_File.Write("|");
    //            }
    //          }

    //          StreamWriter_File.Write(StreamWriter_File.NewLine);

    //          foreach (DataRow DataRow_File in dataTable_File.Rows)
    //          {
    //            for (int i = 0; i < ColumnCount; i++)
    //            {
    //              if (!Convert.IsDBNull(DataRow_File[i]))
    //              {
    //                StreamWriter_File.Write(DataRow_File[i].ToString());
    //              }

    //              if (i < ColumnCount - 1)
    //              {
    //                StreamWriter_File.Write("|");
    //              }
    //            }

    //            StreamWriter_File.Write(StreamWriter_File.NewLine);
    //          }
    //        }

    //        InfoQuest_Impersonate.UndoImpersonation();

    //        if (automated == true && eventType == "PDCH")
    //        {
    //          String SQLStringInsertPXMPDCHEventFile = "INSERT INTO Form_PXM_PDCH_Event_FileCreated ( PXM_PDCH_Event_FileCreated_Path , PXM_PDCH_Event_FileCreated_CurrentDate , PXM_PDCH_Event_FileCreated_StartDate , PXM_PDCH_Event_FileCreated_EndDate , PXM_PDCH_Event_FileCreated_Facility_Id ) VALUES ( @PXM_PDCH_Event_FileCreated_Path , @PXM_PDCH_Event_FileCreated_CurrentDate , @PXM_PDCH_Event_FileCreated_StartDate , @PXM_PDCH_Event_FileCreated_EndDate , @PXM_PDCH_Event_FileCreated_Facility_Id )";
    //          using (SqlCommand SqlCommand_InsertPXMPDCHEventFile = new SqlCommand(SQLStringInsertPXMPDCHEventFile))
    //          {
    //            SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Path", SavePath);
    //            SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_CurrentDate", currentDate);
    //            SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_StartDate", startDate);
    //            SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_EndDate", endDate);
    //            SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Facility_Id", facilityId);
    //            InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPXMPDCHEventFile);
    //          }
    //        }
    //      }
    //      else
    //      {
    //        ReturnMessage = Convert.ToString(shareFolder + " user impersination failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture);
    //      }
    //    }
    //    catch (Exception Exception_Error)
    //    {
    //      if (!String.IsNullOrEmpty(Exception_Error.ToString()))
    //      {
    //        InfoQuest_Impersonate.UndoImpersonation();

    //        ReturnMessage = Convert.ToString(shareFolder + " file could not be created at " + SavePath + "; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture);
    //      }
    //      else
    //      {
    //        throw;
    //      }
    //    }
    //  }

    //  return ReturnMessage;
    //}
    #endregion

    private static string PXM_PDCH_CreateFile_TXT(DataTable dataTable_File, bool automated, string shareFolder, string fileName, string eventType, DateTime currentDate, DateTime startDate, DateTime endDate, string facilityId)
    {
      string ReturnMessage = "";

      if (dataTable_File == null)
      {
        ReturnMessage = Convert.ToString("DataTable is empty", CultureInfo.CurrentCulture);
      }
      else
      {
        string SavePath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\" + shareFolder + @"\" + fileName + "-" + currentDate.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + ".txt";

        try
        {
          PXM_PDCH_FromDataBase_Impersonation FromDataBase_Impersonation_Current = PXM_PDCH_GetImpersonation();
          string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
          string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
          string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

          if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
          {
            using (StreamWriter StreamWriter_File = new StreamWriter(SavePath, true))
            {
              int ColumnCount = dataTable_File.Columns.Count;
              for (int i = 0; i < ColumnCount; i++)
              {
                StreamWriter_File.Write(dataTable_File.Columns[i].ToString());

                if (i < ColumnCount - 1)
                {
                  StreamWriter_File.Write("|");
                }
              }

              StreamWriter_File.Write(StreamWriter_File.NewLine);

              foreach (DataRow DataRow_File in dataTable_File.Rows)
              {
                for (int i = 0; i < ColumnCount; i++)
                {
                  if (!Convert.IsDBNull(DataRow_File[i]))
                  {
                    StreamWriter_File.Write(DataRow_File[i].ToString());
                  }

                  if (i < ColumnCount - 1)
                  {
                    StreamWriter_File.Write("|");
                  }
                }

                StreamWriter_File.Write(StreamWriter_File.NewLine);
              }
            }

            if (automated == true && eventType == "PDCH")
            {
              string TXTFileName = SavePath.Substring(SavePath.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
              string ZIPFileName = TXTFileName.Replace(".txt", ".zip");

              string TXTFilePathAndName = SavePath;
              string ZIPFilePathAndName = SavePath.Replace(".txt", ".zip");

              using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
              {
                ZipArchive_PathAndName.CreateEntryFromFile(TXTFilePathAndName, TXTFileName);
              }

              using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
              {
                string ZIPFileContentType = "application/zip";
                BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
                Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

                string SQLStringInsertPXMPDCHEventFile = "INSERT INTO Form_PXM_PDCH_Event_FileCreated ( PXM_PDCH_Event_FileCreated_Path , PXM_PDCH_Event_FileCreated_CurrentDate , PXM_PDCH_Event_FileCreated_StartDate , PXM_PDCH_Event_FileCreated_EndDate , PXM_PDCH_Event_FileCreated_Facility_Id , PXM_PDCH_Event_FileCreated_ZipFileName , PXM_PDCH_Event_FileCreated_ContentType , PXM_PDCH_Event_FileCreated_Data ) VALUES ( @PXM_PDCH_Event_FileCreated_Path , @PXM_PDCH_Event_FileCreated_CurrentDate , @PXM_PDCH_Event_FileCreated_StartDate , @PXM_PDCH_Event_FileCreated_EndDate , @PXM_PDCH_Event_FileCreated_Facility_Id , @PXM_PDCH_Event_FileCreated_ZipFileName , @PXM_PDCH_Event_FileCreated_ContentType , @PXM_PDCH_Event_FileCreated_Data )";
                using (SqlCommand SqlCommand_InsertPXMPDCHEventFile = new SqlCommand(SQLStringInsertPXMPDCHEventFile))
                {
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Path", SavePath);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_CurrentDate", currentDate);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_StartDate", startDate);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_EndDate", endDate);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Facility_Id", facilityId);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_ZipFileName", ZIPFileName);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_ContentType", ZIPFileContentType);
                  SqlCommand_InsertPXMPDCHEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Data", Byte_ZIPFile);
                  InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPXMPDCHEventFile);
                }
              }

              File.Delete(ZIPFilePathAndName);
            }

            InfoQuest_Impersonate.UndoImpersonation();
          }
          else
          {
            ReturnMessage = Convert.ToString(shareFolder + " user impersination failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture);
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            InfoQuest_Impersonate.UndoImpersonation();

            ReturnMessage = Convert.ToString(shareFolder + " file could not be created at " + SavePath + "; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture);
          }
          else
          {
            throw;
          }
        }
      }

      return ReturnMessage;
    }

    private class PXM_PDCH_FromDataBase_ContactPatient
    {
      public string ContactPatient { get; set; }
    }

    private static PXM_PDCH_FromDataBase_ContactPatient PXM_PDCH_GetContactPatient(string pxmpdchEscalationSurveyKey)
    {
      PXM_PDCH_FromDataBase_ContactPatient FromDataBase_ContactPatient_New = new PXM_PDCH_FromDataBase_ContactPatient();

      string SQLStringContactPatient = "SELECT PXM_PDCH_Escalation_ValueLabel FROM Form_PXM_PDCH_Escalation WHERE PXM_PDCH_Escalation_Label = 'Q.B.1' AND PXM_PDCH_Escalation_SurveyKey = @PXM_PDCH_Escalation_SurveyKey";
      using (SqlCommand SqlCommand_ContactPatient = new SqlCommand(SQLStringContactPatient))
      {
        SqlCommand_ContactPatient.Parameters.AddWithValue("@PXM_PDCH_Escalation_SurveyKey", pxmpdchEscalationSurveyKey);
        DataTable DataTable_ContactPatient;
        using (DataTable_ContactPatient = new DataTable())
        {
          DataTable_ContactPatient.Locale = CultureInfo.CurrentCulture;
          DataTable_ContactPatient = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ContactPatient).Copy();
          if (DataTable_ContactPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_ContactPatient in DataTable_ContactPatient.Rows)
            {
              FromDataBase_ContactPatient_New.ContactPatient = DataRow_Row_ContactPatient["PXM_PDCH_Escalation_ValueLabel"].ToString();
            }
          }
        }
      }

      return FromDataBase_ContactPatient_New;
    }

    private class PXM_PDCH_FromDataBase_UnitId
    {
      public string UnitId { get; set; }
    }

    private static PXM_PDCH_FromDataBase_UnitId PXM_PDCH_GetUnitId(string facilityId, string escalationEventDischargeWard)
    {
      PXM_PDCH_FromDataBase_UnitId FromDataBase_UnitId_New = new PXM_PDCH_FromDataBase_UnitId();

      string SQLStringUnitId = "SELECT Unit_Id FROM vAdministration_Facility_Unit_Active WHERE Facility_Id = @Facility_Id AND Unit_Name = @Unit_Name";
      using (SqlCommand SqlCommand_UnitId = new SqlCommand(SQLStringUnitId))
      {
        SqlCommand_UnitId.Parameters.AddWithValue("@Facility_Id", facilityId);
        SqlCommand_UnitId.Parameters.AddWithValue("@Unit_Name", escalationEventDischargeWard);
        DataTable DataTable_UnitId;
        using (DataTable_UnitId = new DataTable())
        {
          DataTable_UnitId.Locale = CultureInfo.CurrentCulture;
          DataTable_UnitId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_UnitId).Copy();
          if (DataTable_UnitId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_UnitId in DataTable_UnitId.Rows)
            {
              FromDataBase_UnitId_New.UnitId = DataRow_Row_UnitId["Unit_Id"].ToString();
            }
          }
        }
      }

      return FromDataBase_UnitId_New;
    }

    private class PXM_PDCH_FromDataBase_Impersonation
    {
      public string ImpersonationUserName { get; set; }
      public string ImpersonationPassword { get; set; }
      public string ImpersonationDomain { get; set; }
    }

    private static PXM_PDCH_FromDataBase_Impersonation PXM_PDCH_GetImpersonation()
    {
      PXM_PDCH_FromDataBase_Impersonation FromDataBase_Impersonation_New = new PXM_PDCH_FromDataBase_Impersonation();

      string SQLStringForm = "SELECT SystemAccount_Domain , SystemAccount_UserName , SystemAccount_Password FROM Administration_SystemAccount WHERE SystemAccount_Id = 1";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;
          DataTable_Form = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              FromDataBase_Impersonation_New.ImpersonationUserName = DataRow_Row["SystemAccount_UserName"].ToString();
              FromDataBase_Impersonation_New.ImpersonationPassword = DataRow_Row["SystemAccount_Password"].ToString();
              FromDataBase_Impersonation_New.ImpersonationDomain = DataRow_Row["SystemAccount_Domain"].ToString();
            }
          }
        }
      }

      return FromDataBase_Impersonation_New;
    }

    private class PXM_PDCH_FromDataBase_Facility
    {
      public string FacilityId { get; set; }
      public string FacilityFacilityDisplayName { get; set; }
    }

    private static PXM_PDCH_FromDataBase_Facility PXM_PDCH_GetFacility(string facility_FacilityCode)
    {
      PXM_PDCH_FromDataBase_Facility FromDataBase_Facility_New = new PXM_PDCH_FromDataBase_Facility();

      string SQLStringFacility = "SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Form_Active WHERE Form_Id = 36 AND Facility_FacilityCode = @Facility_FacilityCode";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@Facility_FacilityCode", facility_FacilityCode);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_Facility in DataTable_Facility.Rows)
            {
              FromDataBase_Facility_New.FacilityId = DataRow_Row_Facility["Facility_Id"].ToString();
              FromDataBase_Facility_New.FacilityFacilityDisplayName = DataRow_Row_Facility["Facility_FacilityDisplayName"].ToString();
            }
          }
        }
      }

      return FromDataBase_Facility_New;
    }

    private class PXM_PDCH_FromDataBase_SystemAccount
    {
      public string SystemAccountDomain { get; set; }
      public string SystemAccountUserName { get; set; }
    }

    private static PXM_PDCH_FromDataBase_SystemAccount PXM_PDCH_GetSystemAccount()
    {
      PXM_PDCH_FromDataBase_SystemAccount FromDataBase_SystemAccount_New = new PXM_PDCH_FromDataBase_SystemAccount();

      string SQLStringSystemAccount = "SELECT SystemAccount_Domain , SystemAccount_UserName FROM Administration_SystemAccount WHERE SystemAccount_Id = 1";
      using (SqlCommand SqlCommand_SystemAccount = new SqlCommand(SQLStringSystemAccount))
      {
        DataTable DataTable_SystemAccount;
        using (DataTable_SystemAccount = new DataTable())
        {
          DataTable_SystemAccount.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemAccount = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemAccount).Copy();
          if (DataTable_SystemAccount.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemAccount.Rows)
            {
              FromDataBase_SystemAccount_New.SystemAccountDomain = DataRow_Row["SystemAccount_Domain"].ToString();
              FromDataBase_SystemAccount_New.SystemAccountUserName = DataRow_Row["SystemAccount_UserName"].ToString();
            }
          }
        }
      }

      return FromDataBase_SystemAccount_New;
    }
    //---END--- --PXM PDCH--//
  }
}
