using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO.Compression;
using System.Xml;
using System.Reflection;
using InfoQuestWCF.ServiceReference_MHS_PXM;

namespace InfoQuestWCF
{
  public partial class Services_MHS : IService_MHS_PXM
  {
    //--START-- --PXM Event--//
    public string PXM_Event_GetEventData(DateTime startDate, DateTime endDate, string facilityId)
    {
      PXM_Event_GetEventData_ReturnMessageHandler.Clear();

      string HospitalUnitId = InfoQuest_DataPatient.DataPatient_ODS_ImpiloUnitId(facilityId);

      try
      {
        PXM_Event_GetEventData_ProcessData(startDate, endDate, HospitalUnitId, "ZA");
        PXM_Event_GetEventData_ProcessData(startDate, endDate, HospitalUnitId, "BW");
        PXM_Event_GetEventData_CleanUpData();
        PXM_Event_GetEventData_InsertTracking();
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_Event_GetEventData_CleanUpData();
          PXM_Event_GetEventData_InsertTracking();

          PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Exception Message: " + Exception_Error.Message + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
          PXM_Event_GetEventData_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_Event_GetEventData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_Event_GetEventData_Successful == "No")
      {
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_Event_GetEventData_Successful == "Yes")
      {
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_Event_GetEventData_ReturnMessageHandler.Clear();

      return ReturnMessage;
    }

    private void PXM_Event_GetEventData_ProcessData(DateTime startDate, DateTime endDate, string hospitalUnitId, string country)
    {
      DataTable DataTable_PXMEvent;
      using (DataTable_PXMEvent = new DataTable())
      {
        DataTable_PXMEvent.Locale = CultureInfo.CurrentCulture;
        DataTable_PXMEvent = InfoQuest_DataPatient.DataPatient_ODS_PXM_Event(startDate, endDate, hospitalUnitId, country).Copy();
        if (DataTable_PXMEvent.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_PXMEvent.Rows)
          {
            PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString(DataRow_Row["Error"].ToString(), CultureInfo.CurrentCulture));
          }

          PXM_Event_GetEventData_Successful = "No";
        }
        else if (DataTable_PXMEvent.Columns.Count != 1)
        {
          if (DataTable_PXMEvent.Rows.Count > 0)
          {
            string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
            using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
            {
              SqlConnection_BulkCopy.Open();

              using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
              {
                SqlBulkCopy_File.DestinationTableName = "Form_PXM_Event_ProcessData";

                foreach (DataColumn DataColumn_ColumnNames in DataTable_PXMEvent.Columns)
                {
                  string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_Event_ProcessData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
                  using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
                  {
                    SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
                    DataTable DataTable_Column;
                    using (DataTable_Column = new DataTable())
                    {
                      DataTable_Column.Locale = CultureInfo.CurrentCulture;
                      DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                      if (DataTable_Column.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                        {
                          string name = DataRow_Row_ColumnData["name"].ToString();

                          SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                          SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                        }
                      }
                    }
                  }
                }

                try
                {
                  string ReturnMessage = PXM_Event_GetEventData_SaveDataRequest(DataTable_PXMEvent, country, startDate, endDate, (string.IsNullOrEmpty(hospitalUnitId) ? "All" : hospitalUnitId).ToString());
                  if (!string.IsNullOrEmpty(ReturnMessage))
                  {
                    PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Get " + country + " Event Data failed, for StartDate: " + startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "; EndDate: " + endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + ";" + ReturnMessage, CultureInfo.CurrentCulture));
                    PXM_Event_GetEventData_Successful = "No";
                  }
                  else
                  {
                    SqlBulkCopy_File.WriteToServer(DataTable_PXMEvent);
                    PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Get " + country + " Event Data successful, for StartDate: " + startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "; EndDate: " + endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + ";", CultureInfo.CurrentCulture));
                  }
                }
                catch (Exception Exception_Error)
                {
                  if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                  {
                    PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Get " + country + " Event Data failed, for StartDate: " + startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "; EndDate: " + endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + ";; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
                    PXM_Event_GetEventData_Successful = "No";
                    InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
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
            PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Get " + country + " Event Data successful, for StartDate: " + startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "; EndDate: " + endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + ";", CultureInfo.CurrentCulture));
          }
        }
        else
        {
          PXM_Event_GetEventData_ReturnMessageHandlers(Convert.ToString("Get " + country + " Event Data is in the wrong format", CultureInfo.CurrentCulture));
          PXM_Event_GetEventData_Successful = "No";
        }
      }
    }

    public string PXM_Event_CheckGetEventData()
    {
      PXM_Event_CheckGetEventData_ReturnMessageHandler.Clear();

      try
      {
        PXM_Event_CheckGetEventData_ProcessData("ZA");
        PXM_Event_CheckGetEventData_ProcessData("BW");
        PXM_Event_GetEventData_CleanUpData();
        PXM_Event_GetEventData_InsertTracking();
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_Event_GetEventData_CleanUpData();
          PXM_Event_GetEventData_InsertTracking();

          PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Exception Message: " + Exception_Error.Message + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
          PXM_Event_CheckGetEventData_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_Event_CheckGetEventData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_Event_CheckGetEventData_Successful == "No")
      {
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_Event_CheckGetEventData_Successful == "Yes")
      {
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_Event_CheckGetEventData_ReturnMessageHandler.Clear();

      return ReturnMessage;
    }

    private void PXM_Event_CheckGetEventData_ProcessData(string country)
    {
      string SQLStringMissing = @" ;WITH	PXMEventMissing(DAY) AS ( 
	                                SELECT CAST(CONVERT(NVARCHAR(MAX), DATEADD(DAY, -30, GETDATE()), 110) AS DATETIME) AS DAY 
	                                UNION ALL 
	                                SELECT DAY + 1 
	                                FROM PXMEventMissing 
	                                WHERE DAY < CAST(DATEADD(DAY, -2, GETDATE()) AS DATETIME) 
                                ) 
                                SELECT PXMEventMissing.DAY AS 'Missing' 
                                FROM PXMEventMissing 
                                LEFT JOIN Form_PXM_Event_DataRequest ON Form_PXM_Event_DataRequest.StartDate = PXMEventMissing.DAY AND Country = @Country
                                GROUP BY PXMEventMissing.DAY 
                                HAVING COUNT(StartDate) = 0";
      using (SqlCommand SqlCommand_Missing = new SqlCommand(SQLStringMissing))
      {
        SqlCommand_Missing.Parameters.AddWithValue("@Country", country);
        DataTable DataTable_Missing;
        using (DataTable_Missing = new DataTable())
        {
          DataTable_Missing.Locale = CultureInfo.CurrentCulture;
          DataTable_Missing = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Missing).Copy();
          if (DataTable_Missing.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Missing.Rows)
            {
              DateTime Missing = Convert.ToDateTime(DataRow_Row["Missing"], CultureInfo.CurrentCulture);
              DateTime StartDate = Missing;
              DateTime EndDate = Missing;
              string HospitalUnitId = "";

              DataTable DataTable_PXMEvent;
              using (DataTable_PXMEvent = new DataTable())
              {
                DataTable_PXMEvent.Locale = CultureInfo.CurrentCulture;
                DataTable_PXMEvent = InfoQuest_DataPatient.DataPatient_ODS_PXM_Event(StartDate, EndDate, HospitalUnitId, country).Copy();
                if (DataTable_PXMEvent.Columns.Count == 1)
                {
                  foreach (DataRow DataRow_RowExport in DataTable_PXMEvent.Rows)
                  {
                    if (DataRow_RowExport.Table.Columns.Contains("Error"))
                    {
                      PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString(DataRow_RowExport["Error"].ToString(), CultureInfo.CurrentCulture));
                    }
                  }

                  PXM_Event_CheckGetEventData_Successful = "No";
                }
                else if (DataTable_PXMEvent.Columns.Count != 1)
                {
                  if (DataTable_PXMEvent.Rows.Count > 0)
                  {
                    string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
                    using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
                    {
                      SqlConnection_BulkCopy.Open();

                      using (SqlBulkCopy SqlBulkCopy_Missing = new SqlBulkCopy(SqlConnection_BulkCopy))
                      {
                        SqlBulkCopy_Missing.DestinationTableName = "Form_PXM_Event_ProcessData";

                        foreach (DataColumn DataColumn_ColumnNames in DataTable_PXMEvent.Columns)
                        {
                          string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_Event_ProcessData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
                          using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
                          {
                            SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
                            DataTable DataTable_Column;
                            using (DataTable_Column = new DataTable())
                            {
                              DataTable_Column.Locale = CultureInfo.CurrentCulture;
                              DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                              if (DataTable_Column.Rows.Count > 0)
                              {
                                foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                                {
                                  string name = DataRow_Row_ColumnData["name"].ToString();

                                  SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                                  SqlBulkCopy_Missing.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                                }
                              }
                            }
                          }
                        }

                        try
                        {
                          string ReturnMessage = PXM_Event_GetEventData_SaveDataRequest(DataTable_PXMEvent, country, StartDate, EndDate, (string.IsNullOrEmpty(HospitalUnitId) ? "All" : HospitalUnitId).ToString());
                          if (!string.IsNullOrEmpty(ReturnMessage))
                          {
                            PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data failed, for " + Missing.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "; " + ReturnMessage, CultureInfo.CurrentCulture));
                            PXM_Event_CheckGetEventData_Successful = "No";
                          }
                          else
                          {
                            SqlBulkCopy_Missing.WriteToServer(DataTable_PXMEvent);
                            PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data successful, for " + Missing.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "", CultureInfo.CurrentCulture));
                          }
                        }
                        catch (Exception Exception_Error)
                        {
                          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                          {
                            PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data failed, for " + Missing.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "", CultureInfo.CurrentCulture));
                            PXM_Event_CheckGetEventData_Successful = "No";
                            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
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
                    PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data successful, for " + Missing.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + "", CultureInfo.CurrentCulture));
                  }
                }
                else
                {
                  PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data is in the wrong format", CultureInfo.CurrentCulture));
                  PXM_Event_CheckGetEventData_Successful = "No";
                }
              }
            }
          }
          else
          {
            PXM_Event_CheckGetEventData_ReturnMessageHandlers(Convert.ToString("Check Get " + country + " Event Data successful, No missing PXM Event " + country + " files", CultureInfo.CurrentCulture));
          }
        }
      }
    }

    private static string PXM_Event_GetEventData_SaveDataRequest(DataTable datatable_DataRequest, string country, DateTime startDate, DateTime endDate, string hospitalUnitId)
    {
      string ReturnMessage = "";

      string FileName = "PXM_Event_" + country + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + ".xml";
      string SavePath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\Service_MHS_PXM_Event_Upload\" + FileName + "";

      try
      {
        PXM_FromDataBase_Impersonation FromDataBase_Impersonation_Current = PXM_GetImpersonation();
        string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
        string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
        string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

        if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
        {
          using (StringWriter StringWriter_Event = new StringWriter(CultureInfo.CurrentCulture))
          {
            datatable_DataRequest.TableName = "Event";
            datatable_DataRequest.WriteXml(StringWriter_Event);

            XmlDocument XmlDocument_Event = new XmlDocument();
            XmlDocument_Event.LoadXml(StringWriter_Event.ToString());

            XmlDocument_Event.Save(SavePath);
          }

          string XMLFileName = SavePath.Substring(SavePath.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
          string ZIPFileName = XMLFileName.Replace(".xml", ".zip");

          string XMLFilePathAndName = SavePath;
          string ZIPFilePathAndName = SavePath.Replace(".xml", ".zip");

          using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
          {
            ZipArchive_PathAndName.CreateEntryFromFile(XMLFilePathAndName, XMLFileName);
          }

          using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
          {
            string ZIPFileContentType = "application/zip";
            BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
            Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

            string SQLStringInsertPXMEventDataRequest = @" INSERT INTO dbo.Form_PXM_Event_DataRequest
                                                        ( RequestDate , StartDate , EndDate , HospitalUnitId , Country , ReceivedRecords , FileName , FileContentType , FileData )
                                                        VALUES
                                                        ( @RequestDate , @StartDate , @EndDate , @HospitalUnitId , @Country , @ReceivedRecords , @FileName , @FileContentType , @FileData )";
            using (SqlCommand SqlCommand_InsertPXMEventDataRequest = new SqlCommand(SQLStringInsertPXMEventDataRequest))
            {
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@RequestDate", DateTime.Now);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@StartDate", startDate);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@EndDate", endDate);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@HospitalUnitId", hospitalUnitId);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@Country", country);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@ReceivedRecords", datatable_DataRequest.Rows.Count);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@FileName", ZIPFileName);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@FileContentType", ZIPFileContentType);
              SqlCommand_InsertPXMEventDataRequest.Parameters.AddWithValue("@FileData", Byte_ZIPFile);
              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_InsertPXMEventDataRequest);
            }
          }

          File.Delete(XMLFilePathAndName);
          File.Delete(ZIPFilePathAndName);

          InfoQuest_Impersonate.UndoImpersonation();
        }
        else
        {
          ReturnMessage = Convert.ToString(SavePath + " user impersonation failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture);
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          InfoQuest_Impersonate.UndoImpersonation();

          ReturnMessage = Convert.ToString("'" + FileName + "' could not be created at '" + SavePath + "'; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture);
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      return ReturnMessage;
    }

    private static void PXM_Event_GetEventData_CleanUpData()
    {
      string SQLStringCleanUpData = @"WITH CTE (HospitalCode , VisitNumber , DuplicateCount)
                                      AS (
			                                      SELECT	HospitalCode , 
							                                      VisitNumber ,
							                                      ROW_NUMBER() OVER(PARTITION BY HospitalCode , VisitNumber ORDER BY HospitalCode , VisitNumber) AS DuplicateCount
			                                      FROM		Form_PXM_Event_ProcessData
		                                      )
                                      DELETE 
                                      FROM CTE
                                      WHERE DuplicateCount > 1";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }

    private static void PXM_Event_GetEventData_InsertTracking()
    {
      string SQLStringInsertTracking = @" INSERT INTO Form_PXM_Event_ProcessedData (
							                                            HospitalCode ,
							                                            VisitNumber ,
							                                            InfoQuestUploadDate 
						                                            )
                                            SELECT	Form_PXM_Event_ProcessData.HospitalCode , 
				                                            Form_PXM_Event_ProcessData.VisitNumber , 
				                                            GETDATE()
                                            FROM		Form_PXM_Event_ProcessData
				                                            LEFT JOIN Form_PXM_Event_ProcessedData ON Form_PXM_Event_ProcessData.HospitalCode = Form_PXM_Event_ProcessedData.HospitalCode AND Form_PXM_Event_ProcessData.VisitNumber = Form_PXM_Event_ProcessedData.VisitNumber
                                            WHERE		Form_PXM_Event_ProcessedData.HospitalCode IS NULL";
      using (SqlCommand SqlCommand_InsertTracking = new SqlCommand(SQLStringInsertTracking))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_InsertTracking);
      }
    }


    public string PXM_Event_SendEventData()
    {
      PXM_Event_SendEventData_ReturnMessageHandler.Clear();

      PXM_Event_SendEventData_SITUATScript();

      string ExecutePXMSendDataAgain = "Yes";

      while (ExecutePXMSendDataAgain == "Yes")
      {
        string SQLStringPXMSendData = @"SELECT		TOP 500
					                                        Form_PXM_Event_ProcessData.AdmissionDate ,
					                                        Form_PXM_Event_ProcessData.DischargedDate ,
					                                        Form_PXM_Event_ProcessData.Email ,
					                                        Form_PXM_Event_ProcessData.Mobile ,
					                                        Form_PXM_Event_ProcessData.EventId ,
					                                        Form_PXM_Event_ProcessData.PatientFirstname ,
					                                        Form_PXM_Event_ProcessData.PatientKnownAs ,
					                                        Form_PXM_Event_ProcessData.PatientSurname ,
					                                        Form_PXM_Event_ProcessData.PatientTitle ,
					                                        Form_PXM_Event_ProcessData.PatientMobileNumber ,
					                                        Form_PXM_Event_ProcessData.PatientDateOfBirth ,
					                                        Form_PXM_Event_ProcessData.PatientAge ,
					                                        Form_PXM_Event_ProcessData.EmergencyContactPersonFirstname ,
					                                        Form_PXM_Event_ProcessData.EmergencyContactPersonSurname ,
					                                        Form_PXM_Event_ProcessData.EmergencyContactPersonMobileNumber ,
					                                        Form_PXM_Event_ProcessData.EmergencyContactPersonEmail ,
					                                        Form_PXM_Event_ProcessData.Relationship ,
					                                        Form_PXM_Event_ProcessData.Hospital ,
					                                        Form_PXM_Event_ProcessData.HospitalCode ,
					                                        Form_PXM_Event_ProcessData.IDNumber ,
					                                        Form_PXM_Event_ProcessData.MedicalFunder ,
					                                        Form_PXM_Event_ProcessData.FunderOption ,
					                                        Form_PXM_Event_ProcessData.PreferredChannel ,
					                                        Form_PXM_Event_ProcessData.TreatingDoctor ,
					                                        Form_PXM_Event_ProcessData.CareType ,
					                                        Form_PXM_Event_ProcessData.Lifenumber ,
					                                        Form_PXM_Event_ProcessData.VisitNumber ,
					                                        Form_PXM_Event_ProcessData.DischargeWard ,
					                                        Form_PXM_Event_ProcessData.DepartureTypeId ,
					                                        Form_PXM_Event_ProcessData.DepartureType ,
					                                        Form_PXM_Event_ProcessData.UnderAge
                                        FROM			Form_PXM_Event_ProcessData
					                                        LEFT JOIN Form_PXM_Event_ProcessedData ON Form_PXM_Event_ProcessData.HospitalCode = Form_PXM_Event_ProcessedData.HospitalCode AND Form_PXM_Event_ProcessData.VisitNumber = Form_PXM_Event_ProcessedData.VisitNumber
                                        WHERE			Form_PXM_Event_ProcessedData.EventFileNumber IS NULL
                                        ORDER BY	DischargedDate";
        using (SqlCommand SqlCommand_PXMSendData = new SqlCommand(SQLStringPXMSendData))
        {
          DataTable DataTable_PXMSendData;
          using (DataTable_PXMSendData = new DataTable())
          {
            DataTable_PXMSendData.Locale = CultureInfo.CurrentCulture;
            DataTable_PXMSendData = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_PXMSendData).Copy();
            if (DataTable_PXMSendData.Rows.Count == 0)
            {
              PXM_Event_SendEventData_ReturnMessageHandlers(Convert.ToString("Send Event Data successful, no more data to send", CultureInfo.CurrentCulture));
              PXM_Event_SendEventData_Successful = "Yes";
              ExecutePXMSendDataAgain = "No";
              continue;
            }
          }

          string EventFileNumber = "PXM_Event_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture);
          DateTime EventFileSendDate = DateTime.Now;

          EventFile EventFile_PXMEvent = new EventFile();
          EventFileHeader EventFileHeader_PXMEvent = new EventFileHeader();
          EventFileHeader_PXMEvent.To = "Millward Brown";
          EventFileHeader_PXMEvent.From = "Life Healthcare - InfoQuest";
          EventFileHeader_PXMEvent.Payload = "PXMEvent";
          EventFileHeader_PXMEvent.EventFileNumber = EventFileNumber;

          List<EventFileEvent> EventFileEvent_Events = new List<EventFileEvent>();

          foreach (DataRow DataRow_Row in DataTable_PXMSendData.Rows)
          {
            EventFileEvent EventFileEvent_Event = new EventFileEvent();

            EventFileEvent_Event.AdmissionDate = Convert.ToDateTime(DataRow_Row["AdmissionDate"], CultureInfo.CurrentCulture);
            EventFileEvent_Event.DischargedDate = Convert.ToDateTime(DataRow_Row["DischargedDate"], CultureInfo.CurrentCulture);
            EventFileEvent_Event.Email = DataRow_Row["Email"].ToString();
            EventFileEvent_Event.Mobile = DataRow_Row["Mobile"].ToString();
            EventFileEvent_Event.EventId = DataRow_Row["EventId"].ToString();
            EventFileEvent_Event.PatientFirstname = DataRow_Row["PatientFirstname"].ToString();
            EventFileEvent_Event.PatientKnownAs = DataRow_Row["PatientKnownAs"].ToString();
            EventFileEvent_Event.PatientSurname = DataRow_Row["PatientSurname"].ToString();
            EventFileEvent_Event.PatientTitle = DataRow_Row["PatientTitle"].ToString();
            EventFileEvent_Event.PatientMobileNumber = DataRow_Row["PatientMobileNumber"].ToString();
            EventFileEvent_Event.PatientDateOfBirth = Convert.ToDateTime(DataRow_Row["PatientDateOfBirth"], CultureInfo.CurrentCulture);
            EventFileEvent_Event.PatientAge = DataRow_Row["PatientAge"].ToString();
            EventFileEvent_Event.EmergencyContactPersonFirstname = DataRow_Row["EmergencyContactPersonFirstname"].ToString();
            EventFileEvent_Event.EmergencyContactPersonSurname = DataRow_Row["EmergencyContactPersonSurname"].ToString();
            EventFileEvent_Event.EmergencyContactPersonMobileNumber = DataRow_Row["EmergencyContactPersonMobileNumber"].ToString();
            EventFileEvent_Event.EmergencyContactPersonEmail = DataRow_Row["EmergencyContactPersonEmail"].ToString();
            EventFileEvent_Event.Relationship = DataRow_Row["Relationship"].ToString();
            EventFileEvent_Event.Hospital = DataRow_Row["Hospital"].ToString();
            EventFileEvent_Event.HospitalCode = DataRow_Row["HospitalCode"].ToString();
            EventFileEvent_Event.IDNumber = DataRow_Row["IDNumber"].ToString();
            EventFileEvent_Event.MedicalFunder = DataRow_Row["MedicalFunder"].ToString();
            EventFileEvent_Event.FunderOption = DataRow_Row["FunderOption"].ToString();
            EventFileEvent_Event.PreferredChannel = DataRow_Row["PreferredChannel"].ToString();
            EventFileEvent_Event.TreatingDoctor = DataRow_Row["TreatingDoctor"].ToString();
            EventFileEvent_Event.CareType = DataRow_Row["CareType"].ToString();
            EventFileEvent_Event.Lifenumber = DataRow_Row["Lifenumber"].ToString();
            EventFileEvent_Event.VisitNumber = DataRow_Row["VisitNumber"].ToString();
            EventFileEvent_Event.DischargeWard = DataRow_Row["DischargeWard"].ToString();
            EventFileEvent_Event.DepartureTypeId = DataRow_Row["DepartureTypeId"].ToString();
            EventFileEvent_Event.DepartureType = DataRow_Row["DepartureType"].ToString();
            EventFileEvent_Event.UnderAge = DataRow_Row["UnderAge"].ToString();
            EventFileEvent_Events.Add(EventFileEvent_Event);
          }

          EventFile_PXMEvent.Header = EventFileHeader_PXMEvent;
          EventFile_PXMEvent.Body = EventFileEvent_Events.ToArray();

          PXM_ExternalResponse_Body_ExternalResponse ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();
          DateTime EventFileResponseDate;

          try
          {
            using (QualitySurveyServiceClient QualitySurveyServiceClient_PXMEvent = new QualitySurveyServiceClient())
            {
              EventFileResponse EventFileResponse_PXMEvent = QualitySurveyServiceClient_PXMEvent.SendQualitySurveyDetails(EventFile_PXMEvent);

              ExternalResponse_Body_ExternalResponse_Message.FileNumber = EventFileResponse_PXMEvent.EventFileNumber;
              ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = EventFileResponse_PXMEvent.IsSuccessful;
              ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = EventFileResponse_PXMEvent.ErrorMessage;
              EventFileResponseDate = DateTime.Now;
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              PXM_Event_SendEventData_ReturnMessageHandlers(Convert.ToString("Send " + EventFileNumber + " Event Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
              PXM_Event_SendEventData_Successful = "No";
              InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");

              ExecutePXMSendDataAgain = "No";
              continue;
            }
            else
            {
              throw;
            }
          }


          if (ExternalResponse_Body_ExternalResponse_Message.IsSuccessful == false)
          {
            PXM_Event_SendEventData_ReturnMessageHandlers(Convert.ToString("Send " + EventFileNumber + " Event Data failed, MHS Error: " + ExternalResponse_Body_ExternalResponse_Message.ErrorMessage + ";", CultureInfo.CurrentCulture));
            PXM_Event_SendEventData_Successful = "No";
            ExecutePXMSendDataAgain = "No";
            continue;
          }
          else
          {
            foreach (DataRow DataRow_Row in DataTable_PXMSendData.Rows)
            {
              string HospitalCode = DataRow_Row["HospitalCode"].ToString();
              string VisitNumber = DataRow_Row["VisitNumber"].ToString();

              string SQLStringUpdatePXMEventProcessedData = @"UPDATE	Form_PXM_Event_ProcessedData
                                                              SET			EventFileNumber = @EventFileNumber , 
				                                                              EventFileSendDate = @EventFileSendDate , 
				                                                              EventFileResponse = @EventFileResponse ,
				                                                              EventFileResponseDate = @EventFileResponseDate
                                                              WHERE		HospitalCode = @HospitalCode
				                                                              AND VisitNumber = @VisitNumber";
              using (SqlCommand SqlCommand_UpdatePXMEventProcessedData = new SqlCommand(SQLStringUpdatePXMEventProcessedData))
              {
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventFileNumber", EventFileNumber);
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventFileSendDate", EventFileSendDate);
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventFileResponse", ExternalResponse_Body_ExternalResponse_Message.IsSuccessful);
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventFileResponseDate", EventFileResponseDate);
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@HospitalCode", HospitalCode);
                SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@VisitNumber", VisitNumber);
                InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_UpdatePXMEventProcessedData);
              }
            }

            PXM_Event_SendEventData_ReturnMessageHandlers(Convert.ToString("Send " + EventFileNumber + " Event Data successful", CultureInfo.CurrentCulture));

            //remove
            ExecutePXMSendDataAgain = "No";
            continue;
            //remove
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_Event_SendEventData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_Event_SendEventData_Successful == "No")
      {
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_Event_SendEventData_Successful == "Yes")
      {
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_Event_SendEventData_ReturnMessageHandler.Clear();

      return ReturnMessage;
    }

    public static void PXM_Event_SendEventData_SITUATScript()
    {
      string SQLStringSITUATScript = @" IF (OBJECT_ID('tempdb..#TestUsers') IS NOT NULL)
                                        DROP TABLE #TestUsers;
                                        CREATE TABLE #TestUsers (
	                                        Users_Name NVARCHAR(MAX),
	                                        Users_Surname NVARCHAR(MAX),
	                                        Users_Title NVARCHAR(MAX),
	                                        Users_Mobile NVARCHAR(MAX),
	                                        Users_Email NVARCHAR(MAX)
                                        )

                                        INSERT INTO #TestUsers
                                        SELECT 'Jacobus'		, 'Swart'				, 'Mr'	, '0824271165'	, 'Jacobus.Swart@lifehealthcare.co.za' UNION
                                        SELECT 'Jacqualene' , 'Lapa'				, 'Mrs' , ''						, 'Jacqualene.Lapa@lifehealthcare.co.za' UNION
                                        SELECT 'Venu'				, 'Manchikanti' , 'Mr'	, ''						, 'Venu.Manchikanti@lifehealthcare.co.za'


                                        DECLARE @While_End AS bit = 0

                                        WHILE ( @While_End = 0 )
                                        BEGIN

	                                        ;WITH CurrentTable AS (
			                                        SELECT TOP (SELECT COUNT(1) FROM #TestUsers ) ROW_NUMBER() OVER (ORDER BY Id) CurrentTable_RowNum, *
			                                        FROM Form_PXM_Event_ProcessData (NOLOCK)
			                                        WHERE DepartureTypeId = 1
	                                        ),
	                                        UpdateTable as (
			                                        SELECT ROW_NUMBER() OVER (ORDER BY Users_Email) UpdateTable_RowNum, *
			                                        FROM #TestUsers (NOLOCK)
	                                        )
	
	                                        /*
	                                        SELECT * 
	                                        FROM CurrentTable
	                                        LEFT JOIN UpdateTable ON UpdateTable_RowNum = CurrentTable_RowNum
	                                        */

	                                        UPDATE Form_PXM_Event_ProcessData SET
	                                        Email = UpdateTable.Users_Email ,
	                                        Mobile = UpdateTable.Users_Mobile , 
	                                        PatientFirstname = UpdateTable.Users_Name ,
	                                        PatientSurname = UpdateTable.Users_Surname ,
	                                        PatientTitle = UpdateTable.Users_Title ,
	                                        EmergencyContactPersonFirstname = UpdateTable.Users_Name , 
	                                        EmergencyContactPersonSurname = UpdateTable.Users_Surname ,
	                                        EmergencyContactPersonMobileNumber = UpdateTable.Users_Mobile ,
	                                        EmergencyContactPersonEmail = UpdateTable.Users_Email ,
	                                        DepartureTypeId = 2
	                                        FROM CurrentTable
	                                        LEFT JOIN UpdateTable ON UpdateTable_RowNum = CurrentTable_RowNum
	                                        WHERE Form_PXM_Event_ProcessData.Id = CurrentTable.Id
	
	                                        IF ( SELECT COUNT(1) FROM Form_PXM_Event_ProcessData WHERE DepartureTypeId = 1 ) = 0
		                                        BEGIN
			                                        SET @While_End = 1
		                                        END
	                                        ELSE
		                                        BEGIN
			                                        SET @While_End = 0
		                                        END
	
                                        END

                                        DROP TABLE #TestUsers;

                                        UPDATE Form_PXM_Event_ProcessData SET DepartureTypeId = 1 WHERE DepartureTypeId = 2
                                        ";
      using (SqlCommand SqlCommand_SITUATScript = new SqlCommand(SQLStringSITUATScript))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_SITUATScript);
      }
    }

    public PXM_ExternalResponse TestEventReceiveEventResponse()
    {
      PXM_EventResponse EventResponse = new PXM_EventResponse();

      List<PXM_EventResponse_Body_EventResponse> Body = new List<PXM_EventResponse_Body_EventResponse>();

      PXM_EventResponse_Body_EventResponse Body_Item = new PXM_EventResponse_Body_EventResponse();
      Body_Item.HospitalCode = "1";
      Body_Item.VisitNumber = "1";
      Body_Item.IsSuccessful = true;

      Body.Add(Body_Item);
      
      EventResponse.Body = Body;

      PXM_ExternalResponse PXM_ExternalResponse_Message = PXM_Event_ReceiveEventResponse(EventResponse);

      return PXM_ExternalResponse_Message;
    }

    public PXM_ExternalResponse PXM_Event_ReceiveEventResponse(PXM_EventResponse eventResponse)
    {
      PXM_Event_ReceiveEventResponse_ReturnMessageHandler.Clear();
      PXM_ExternalResponse ExternalResponse_Message = new PXM_ExternalResponse();
      PXM_ExternalResponse_Body_ExternalResponse ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();

      if (eventResponse == null)
      {
        throw new ArgumentNullException("eventResponse");
      }
      else
      {
        if (eventResponse.Body == null || eventResponse.Body.Count == 0)
        {
          PXM_Event_ReceiveEventResponse_ReturnMessageHandlers(Convert.ToString("Event Response failed, Body is empty", CultureInfo.CurrentCulture));
          PXM_Event_ReceiveEventResponse_Successful = "No";
        }
      }

      if (PXM_Event_ReceiveEventResponse_Successful == "Yes")
      {
        try
        {
          foreach (PXM_EventResponse_Body_EventResponse EventResponse_Body_Event in eventResponse.Body)
          {
            string UpdatedId = "";
            string SQLStringUpdatePXMEventProcessedData = @"UPDATE		Form_PXM_Event_ProcessedData
                                                            SET				EventResponse = @EventResponse , 
					                                                            EventResponseDate = @EventResponseDate
                                                            WHERE			HospitalCode = @HospitalCode
					                                                            AND VisitNumber = @VisitNumber; 
                                                            IF @@ROWCOUNT = 0
                                                            BEGIN
                                                            SELECT		'-1' AS Id
                                                            END
                                                            ELSE
                                                            BEGIN
                                                            SELECT		TOP 1 Id 
                                                            FROM			Form_PXM_Event_ProcessedData
                                                            WHERE			HospitalCode = @HospitalCode
					                                                            AND VisitNumber = @VisitNumber
                                                            END";
            using (SqlCommand SqlCommand_UpdatePXMEventProcessedData = new SqlCommand(SQLStringUpdatePXMEventProcessedData))
            {
              SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventResponse", EventResponse_Body_Event.IsSuccessful);
              SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@EventResponseDate", DateTime.Now);
              SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@HospitalCode", EventResponse_Body_Event.HospitalCode);
              SqlCommand_UpdatePXMEventProcessedData.Parameters.AddWithValue("@VisitNumber", EventResponse_Body_Event.VisitNumber);
              UpdatedId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId_WCF(SqlCommand_UpdatePXMEventProcessedData);
            }

            if (UpdatedId.IndexOf("Error", StringComparison.CurrentCulture) > -1 || UpdatedId == "-1")
            {
              PXM_Event_ReceiveEventResponse_ReturnMessageHandlers(Convert.ToString("Event response failed; Hospital Code: " + EventResponse_Body_Event.HospitalCode + " or Visit Number: " + EventResponse_Body_Event.VisitNumber + " is not valid;", CultureInfo.CurrentCulture));
              PXM_Event_ReceiveEventResponse_Successful = "No";
              break;
            }
            else
            {
              PXM_Event_ReceiveEventResponse_ReturnMessageHandlers(Convert.ToString("Event response successful; Hospital Code: " + EventResponse_Body_Event.HospitalCode + " or Visit Number: " + EventResponse_Body_Event.VisitNumber + " is valid;", CultureInfo.CurrentCulture));
              PXM_Event_ReceiveEventResponse_Successful = "Yes";
            }
          }

          PXM_Event_ReceiveEventResponse_CleanUpData();
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_Event_ReceiveEventResponse_CleanUpData();

            PXM_Event_ReceiveEventResponse_ReturnMessageHandlers(Convert.ToString("Event response failed; Exception Message: " + Exception_Error.Message + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
            PXM_Event_ReceiveEventResponse_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_Event_ReceiveEventResponse_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_Event_ReceiveEventResponse_Successful == "No")
      {
        ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = false;
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_Event_ReceiveEventResponse_Successful == "Yes")
      {
        ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = true;
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_Event_ReceiveEventResponse_ReturnMessageHandler.Clear();

      ExternalResponse_Body_ExternalResponse_Message.FileNumber = "";
      ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = ReturnMessage;
      ExternalResponse_Message.Body = ExternalResponse_Body_ExternalResponse_Message;

      return ExternalResponse_Message;
    }

    private static void PXM_Event_ReceiveEventResponse_CleanUpData()
    {
      string SQLStringCleanUpData = @"DELETE
                                      FROM			Form_PXM_Event_ProcessData
                                      WHERE			Id IN (
						                                      SELECT		Form_PXM_Event_ProcessData.Id
						                                      FROM			Form_PXM_Event_ProcessData
											                                      LEFT JOIN Form_PXM_Event_ProcessedData ON Form_PXM_Event_ProcessData.HospitalCode = Form_PXM_Event_ProcessedData.HospitalCode AND Form_PXM_Event_ProcessData.VisitNumber = Form_PXM_Event_ProcessedData.VisitNumber
						                                      WHERE			Form_PXM_Event_ProcessedData.EventResponse = 1
					                                      )";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }


    string PXM_Event_GetEventData_Successful = "Yes";
    string PXM_Event_CheckGetEventData_Successful = "Yes";
    string PXM_Event_SendEventData_Successful = "Yes";
    string PXM_Event_ReceiveEventResponse_Successful = "Yes";

    private static Dictionary<string, string> PXM_Event_GetEventData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_Event_GetEventData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_Event_GetEventData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_Event_GetEventData_ReturnMessageHandler.Add(ReturnMessage, "PXM_Event_GetEventData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_Event_CheckGetEventData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_Event_CheckGetEventData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_Event_CheckGetEventData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_Event_CheckGetEventData_ReturnMessageHandler.Add(ReturnMessage, "PXM_Event_CheckGetEventData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_Event_SendEventData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_Event_SendEventData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_Event_SendEventData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_Event_SendEventData_ReturnMessageHandler.Add(ReturnMessage, "PXM_Event_SendEventData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_Event_ReceiveEventResponse_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_Event_ReceiveEventResponse_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_Event_ReceiveEventResponse_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_Event_ReceiveEventResponse_ReturnMessageHandler.Add(ReturnMessage, "PXM_Event_ReceiveEventResponse: " + ReturnMessage);
      }
    }
    //---END--- --PXM Event--//



    //--START-- --PXM ReceivedFiles--//
    public PXM_ExternalResponse TestEscalationFile()
    {
      PXM_EscalationFile EscalationFile = new PXM_EscalationFile();

      PXM_EscalationFile_Header Header = new PXM_EscalationFile_Header();
      Header.To = "To";
      Header.From = "From";
      Header.Payload = "Payload";
      Header.EscalationFileNumber = "EscalationFileNumber1";

      List<PXM_EscalationFile_Body_Escalation> Escalation = new List<PXM_EscalationFile_Body_Escalation>();

      for (int a = 1; a <= 5; a++)
      {
        PXM_EscalationFile_Body_Escalation Escalation_Item = new PXM_EscalationFile_Body_Escalation();
        Escalation_Item.Value = "Value_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.ValueLabel = "ValueLabel_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.Comment = "Comment_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.CreatedDate = "CreatedDate_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.SurveyKey = "SurveyKey_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.Label = "Label_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.Text = Convert.ToString("Text_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a, CultureInfo.CurrentCulture);
        Escalation_Item.HospitalCode = "HospitalCode_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.Hospital = "Hospital_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.PreferredChannel = "PreferredChannel_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EmailAddress = "EmailAddress_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.PatientMobileNumber = "PatientMobileNumber_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.PatientFirstname = "PatientFirstname_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.PatientSurname = "PatientSurname_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.PatientTitle = "PatientTitle_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.AdmissionDate = "AdmissionDate_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.DischargeWard = "DischargeWard_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EmergencyContactPersonEmail = "EmergencyContactPersonEmail_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EmergencyContactPersonFirstname = "EmergencyContactPersonFirstname_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EmergencyContactPersonMobileNumber = "EmergencyContactPersonMobileNumber_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EmergencyContactPersonSurname = "EmergencyContactPersonSurname_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.EventId = "EventId_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Escalation_Item.VisitNumber = "VisitNumber_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;

        Escalation.Add(Escalation_Item);
      }

      EscalationFile.Header = Header;
      EscalationFile.Body = Escalation;

      PXM_ExternalResponse PXM_ExternalResponse_Message = PXM_ReceivedFiles_Escalation_ReceiveData(EscalationFile);

      return PXM_ExternalResponse_Message;
    }

    public PXM_ExternalResponse PXM_ReceivedFiles_Escalation_ReceiveData(PXM_EscalationFile escalationFile)
    {
      PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler.Clear();
      PXM_ExternalResponse PXM_ExternalResponse_Message = new PXM_ExternalResponse();
      PXM_ExternalResponse_Body_ExternalResponse PXM_ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();

      if (escalationFile == null)
      {
        throw new ArgumentNullException("escalationFile");
      }
      else
      {
        if (escalationFile.Header == null || escalationFile.Body == null || escalationFile.Body.Count == 0)
        {
          PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Escalation Data failed, Either Header or Body is empty", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_Escalation_ReceiveData_Successful = "No";
        }
      }

      if (PXM_ReceivedFiles_Escalation_ReceiveData_Successful == "Yes")
      {
        try
        {
          Type Type_Escalation = typeof(PXM_EscalationFile_Body_Escalation);
          PropertyInfo[] PropertyInfo_Escalation = Type_Escalation.GetProperties();

          using (DataTable DataTable_Escalation = new DataTable())
          {
            DataTable_Escalation.Locale = CultureInfo.CurrentCulture;

            foreach (PropertyInfo PropertyInfo_EscalationItem in PropertyInfo_Escalation)
            {
              DataTable_Escalation.Columns.Add(new DataColumn(PropertyInfo_EscalationItem.Name, Nullable.GetUnderlyingType(PropertyInfo_EscalationItem.PropertyType) ?? PropertyInfo_EscalationItem.PropertyType));
            }

            DataTable_Escalation.Columns.Add(new DataColumn("InfoQuestUploadDate", Type.GetType("System.DateTime")));

            foreach (PXM_EscalationFile_Body_Escalation PXM_EscalationFile_Body_Escalation_Item in escalationFile.Body)
            {
              object[] object_Item = new object[PropertyInfo_Escalation.Length + 1];
              for (int a = 0; a < PropertyInfo_Escalation.Length; a++)
              {
                object_Item[a] = PropertyInfo_Escalation[a].GetValue(PXM_EscalationFile_Body_Escalation_Item);
              }

              object_Item[PropertyInfo_Escalation.Length] = DateTime.Now;

              DataTable_Escalation.Rows.Add(object_Item);
            }

            PXM_ReceivedFiles_Escalation_ProcessData(DataTable_Escalation, escalationFile.Header.EscalationFileNumber);
            PXM_ReceivedFiles_Escalation_CleanUpData();
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_ReceivedFiles_Escalation_CleanUpData();

            PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Escalation Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
            PXM_ReceivedFiles_Escalation_ReceiveData_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_Escalation_ReceiveData_Successful == "No")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = false;
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_Escalation_ReceiveData_Successful == "Yes")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = true;
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler.Clear();

      PXM_ExternalResponse_Body_ExternalResponse_Message.FileNumber = escalationFile.Header.EscalationFileNumber;
      PXM_ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = ReturnMessage;
      PXM_ExternalResponse_Message.Body = PXM_ExternalResponse_Body_ExternalResponse_Message;

      return PXM_ExternalResponse_Message;
    }

    private void PXM_ReceivedFiles_Escalation_ProcessData(DataTable dataTable_Escalation, string escalationFileNumber)
    {
      string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
      using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
      {
        SqlConnection_BulkCopy.Open();

        using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
        {
          SqlBulkCopy_File.DestinationTableName = "Form_PXM_ReceivedFiles_Escalation_ProcessedData";

          foreach (DataColumn DataColumn_ColumnNames in dataTable_Escalation.Columns)
          {
            string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_ReceivedFiles_Escalation_ProcessedData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
            using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
            {
              SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
              DataTable DataTable_Column;
              using (DataTable_Column = new DataTable())
              {
                DataTable_Column.Locale = CultureInfo.CurrentCulture;
                DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                if (DataTable_Column.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                  {
                    string name = DataRow_Row_ColumnData["name"].ToString();

                    SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                    SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                  }
                }
              }
            }
          }

          try
          {
            string SaveDataRequestResponse = PXM_ReceivedFiles_SaveReceivedFiles(dataTable_Escalation, "Escalation", escalationFileNumber);
            if (!string.IsNullOrEmpty(SaveDataRequestResponse))
            {
              PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Escalation Data failed; " + SaveDataRequestResponse, CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_Escalation_ReceiveData_Successful = "No";
            }
            else
            {
              SqlBulkCopy_File.WriteToServer(dataTable_Escalation);
              PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Escalation Data successful", CultureInfo.CurrentCulture));
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Escalation Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_Escalation_ReceiveData_Successful = "No";
              InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            }
            else
            {
              throw;
            }
          }
        }
      }
    }

    private static void PXM_ReceivedFiles_Escalation_CleanUpData()
    {
      string SQLStringCleanUpData = @";WITH CTE ( Value , ValueLabel , Comment , CreatedDate , SurveyKey , Label , Text , HospitalCode , Hospital , PreferredChannel , EmailAddress , PatientMobileNumber , PatientFirstname , PatientSurname , PatientTitle , AdmissionDate , DischargeWard , EmergencyContactPersonEmail , EmergencyContactPersonFirstname , EmergencyContactPersonMobileNumber , EmergencyContactPersonSurname , EventID , VisitNumber , DuplicateCount )
                                      AS (
                                            SELECT	Value , ValueLabel , Comment , CreatedDate , SurveyKey , Label , Text , HospitalCode , Hospital , PreferredChannel , EmailAddress , PatientMobileNumber , PatientFirstname , PatientSurname , PatientTitle , AdmissionDate , DischargeWard , EmergencyContactPersonEmail , EmergencyContactPersonFirstname , EmergencyContactPersonMobileNumber , EmergencyContactPersonSurname , EventID , VisitNumber , 
                                                    ROW_NUMBER() OVER(PARTITION BY Value , ValueLabel , Comment , CreatedDate , SurveyKey , Label , Text , HospitalCode , Hospital , PreferredChannel , EmailAddress , PatientMobileNumber , PatientFirstname , PatientSurname , PatientTitle , AdmissionDate , DischargeWard , EmergencyContactPersonEmail , EmergencyContactPersonFirstname , EmergencyContactPersonMobileNumber , EmergencyContactPersonSurname , EventID , VisitNumber ORDER BY Value , ValueLabel , Comment , CreatedDate , SurveyKey , Label , Text , HospitalCode , Hospital , PreferredChannel , EmailAddress , PatientMobileNumber , PatientFirstname , PatientSurname , PatientTitle , AdmissionDate , DischargeWard , EmergencyContactPersonEmail , EmergencyContactPersonFirstname , EmergencyContactPersonMobileNumber , EmergencyContactPersonSurname , EventID , VisitNumber ) AS DuplicateCount
                                            FROM		Form_PXM_ReceivedFiles_Escalation_ProcessedData
                                          )
                                      DELETE
                                      FROM CTE
                                      WHERE DuplicateCount > 1";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }


    public PXM_ExternalResponse TestReportFile()
    {
      PXM_ReportFile ReportFile = new PXM_ReportFile();

      PXM_ReportFile_Header Header = new PXM_ReportFile_Header();
      Header.To = "To";
      Header.From = "From";
      Header.Payload = "Payload";
      Header.ReportFileNumber = "ReportFileNumber1";


      List<PXM_ReportFile_Body_Report> Report = new List<PXM_ReportFile_Body_Report>();

      for (int a = 1; a <= 5; a++)
      {
        PXM_ReportFile_Body_Report Report_Item = new PXM_ReportFile_Body_Report();
        Report_Item.Hospital = "Hospital_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.HospitalCode = "HospitalCode_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.VisitNumber = "VisitNumber_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.SurveyStatus = "SurveyStatus_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.ReportCompleteDate = "ReportCompleteDate_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.LoyaltyValue = "LoyaltyValue_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.PatientExperienceValue = "PatientExperienceValue_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.PatientExperienceScore = "PatientExperienceScore_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        Report_Item.SurveyType = "SurveyType_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;

        Report.Add(Report_Item);
      }

      ReportFile.Header = Header;
      ReportFile.Body = Report;

      PXM_ExternalResponse PXM_ExternalResponse_Message = PXM_ReceivedFiles_Report_ReceiveData(ReportFile);

      return PXM_ExternalResponse_Message;
    }

    public PXM_ExternalResponse PXM_ReceivedFiles_Report_ReceiveData(PXM_ReportFile reportFile)
    {
      PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler.Clear();
      PXM_ExternalResponse PXM_ExternalResponse_Message = new PXM_ExternalResponse();
      PXM_ExternalResponse_Body_ExternalResponse PXM_ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();

      if (reportFile == null)
      {
        throw new ArgumentNullException("reportFile");
      }
      else
      {
        if (reportFile.Header == null || reportFile.Body == null || reportFile.Body.Count == 0)
        {
          PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Report Data failed, Either Header or Body is empty", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_Report_ReceiveData_Successful = "No";
        }
      }

      if (PXM_ReceivedFiles_Report_ReceiveData_Successful == "Yes")
      {
        try
        {
          Type Type_Report = typeof(PXM_ReportFile_Body_Report);
          PropertyInfo[] PropertyInfo_Report = Type_Report.GetProperties();

          using (DataTable DataTable_Report = new DataTable())
          {
            DataTable_Report.Locale = CultureInfo.CurrentCulture;

            foreach (PropertyInfo PropertyInfo_ReportItem in PropertyInfo_Report)
            {
              DataTable_Report.Columns.Add(new DataColumn(PropertyInfo_ReportItem.Name, Nullable.GetUnderlyingType(PropertyInfo_ReportItem.PropertyType) ?? PropertyInfo_ReportItem.PropertyType));
            }

            DataTable_Report.Columns.Add(new DataColumn("InfoQuestUploadDate", Type.GetType("System.DateTime")));

            foreach (PXM_ReportFile_Body_Report PXM_ReportFile_Body_Report_Item in reportFile.Body)
            {
              object[] object_Item = new object[PropertyInfo_Report.Length + 1];
              for (int a = 0; a < PropertyInfo_Report.Length; a++)
              {
                object_Item[a] = PropertyInfo_Report[a].GetValue(PXM_ReportFile_Body_Report_Item);
              }

              object_Item[PropertyInfo_Report.Length] = DateTime.Now;

              DataTable_Report.Rows.Add(object_Item);
            }

            PXM_ReceivedFiles_Report_ProcessData(DataTable_Report, reportFile.Header.ReportFileNumber);
            PXM_ReceivedFiles_Report_CleanUpData();
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_ReceivedFiles_Report_CleanUpData();

            PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Report Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
            PXM_ReceivedFiles_Report_ReceiveData_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_Report_ReceiveData_Successful == "No")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = false;
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_Report_ReceiveData_Successful == "Yes")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = true;
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler.Clear();

      PXM_ExternalResponse_Body_ExternalResponse_Message.FileNumber = reportFile.Header.ReportFileNumber;
      PXM_ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = ReturnMessage;
      PXM_ExternalResponse_Message.Body = PXM_ExternalResponse_Body_ExternalResponse_Message;

      return PXM_ExternalResponse_Message;
    }

    private void PXM_ReceivedFiles_Report_ProcessData(DataTable dataTable_Report, string reportFileNumber)
    {
      string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
      using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
      {
        SqlConnection_BulkCopy.Open();

        using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
        {
          SqlBulkCopy_File.DestinationTableName = "Form_PXM_ReceivedFiles_Report_ProcessedData";

          foreach (DataColumn DataColumn_ColumnNames in dataTable_Report.Columns)
          {
            string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_ReceivedFiles_Report_ProcessedData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
            using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
            {
              SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
              DataTable DataTable_Column;
              using (DataTable_Column = new DataTable())
              {
                DataTable_Column.Locale = CultureInfo.CurrentCulture;
                DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                if (DataTable_Column.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                  {
                    string name = DataRow_Row_ColumnData["name"].ToString();

                    SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                    SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                  }
                }
              }
            }
          }

          try
          {
            string SaveDataRequestResponse = PXM_ReceivedFiles_SaveReceivedFiles(dataTable_Report, "Report", reportFileNumber);
            if (!string.IsNullOrEmpty(SaveDataRequestResponse))
            {
              PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Report Data failed; " + SaveDataRequestResponse, CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_Report_ReceiveData_Successful = "No";
            }
            else
            {
              SqlBulkCopy_File.WriteToServer(dataTable_Report);
              PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Report Data successful", CultureInfo.CurrentCulture));
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive Report Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_Report_ReceiveData_Successful = "No";
              InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            }
            else
            {
              throw;
            }
          }
        }
      }
    }

    private static void PXM_ReceivedFiles_Report_CleanUpData()
    {
      string SQLStringCleanUpData = @"WITH CTE ( Hospital , HospitalCode , VisitNumber , SurveyStatus , ReportCompleteDate , LoyaltyValue , PatientExperienceValue , PatientExperienceScore , SurveyType , DuplicateCount )
                                      AS (
			                                      SELECT	Hospital , HospitalCode , VisitNumber , SurveyStatus , ReportCompleteDate , LoyaltyValue , PatientExperienceValue , PatientExperienceScore , SurveyType , 
							                                      ROW_NUMBER() OVER(PARTITION BY Hospital , HospitalCode , VisitNumber , SurveyStatus , ReportCompleteDate , LoyaltyValue , PatientExperienceValue , PatientExperienceScore , SurveyType ORDER BY Hospital , HospitalCode , VisitNumber , SurveyStatus , ReportCompleteDate , LoyaltyValue , PatientExperienceValue , PatientExperienceScore , SurveyType ) AS DuplicateCount
			                                      FROM		Form_PXM_ReceivedFiles_Report_ProcessedData
		                                      )
                                      DELETE
                                      FROM CTE
                                      WHERE DuplicateCount > 1";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }


    public PXM_ExternalResponse TestTouchPointFile()
    {
      PXM_TouchPointFile TouchPointFile = new PXM_TouchPointFile();

      PXM_TouchPointFile_Header Header = new PXM_TouchPointFile_Header();
      Header.To = "To";
      Header.From = "From";
      Header.Payload = "Payload";
      Header.TouchPointFileNumber = "TouchPointFileNumber1";


      List<PXM_TouchPointFile_Body_TouchPoint> TouchPoint = new List<PXM_TouchPointFile_Body_TouchPoint>();

      for (int a = 1; a <= 5; a++)
      {
        PXM_TouchPointFile_Body_TouchPoint TouchPoint_Item = new PXM_TouchPointFile_Body_TouchPoint();
        TouchPoint_Item.Hospital = "Hospital_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.HospitalCode = "HospitalCode_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.Date = "Date_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Doctor = "Touchpoint_Doctor_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Facilities = "Touchpoint_Facilities_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Food = "Touchpoint_Food_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Medication = "Touchpoint_Medication_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Nursing = "Touchpoint_Nursing_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_ReceptionStaff = "Touchpoint_ReceptionStaff_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        TouchPoint_Item.TouchPoint_Responsiveness = "Touchpoint_Responsiveness_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;

        TouchPoint.Add(TouchPoint_Item);
      }

      TouchPointFile.Header = Header;
      TouchPointFile.Body = TouchPoint;

      PXM_ExternalResponse PXM_ExternalResponse_Message = PXM_ReceivedFiles_TouchPoint_ReceiveData(TouchPointFile);

      return PXM_ExternalResponse_Message;
    }

    public PXM_ExternalResponse PXM_ReceivedFiles_TouchPoint_ReceiveData(PXM_TouchPointFile touchPointFile)
    {
      PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler.Clear();
      PXM_ExternalResponse PXM_ExternalResponse_Message = new PXM_ExternalResponse();
      PXM_ExternalResponse_Body_ExternalResponse PXM_ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();

      if (touchPointFile == null)
      {
        throw new ArgumentNullException("touchPointFile");
      }
      else
      {
        if (touchPointFile.Header == null || touchPointFile.Body == null || touchPointFile.Body.Count == 0)
        {
          PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive TouchPoint Data failed, Either Header or Body is empty", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful = "No";
        }
      }

      if (PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful == "Yes")
      {
        try
        {
          Type Type_TouchPoint = typeof(PXM_TouchPointFile_Body_TouchPoint);
          PropertyInfo[] PropertyInfo_TouchPoint = Type_TouchPoint.GetProperties();

          using (DataTable DataTable_TouchPoint = new DataTable())
          {
            DataTable_TouchPoint.Locale = CultureInfo.CurrentCulture;

            foreach (PropertyInfo PropertyInfo_TouchPointItem in PropertyInfo_TouchPoint)
            {
              DataTable_TouchPoint.Columns.Add(new DataColumn(PropertyInfo_TouchPointItem.Name, Nullable.GetUnderlyingType(PropertyInfo_TouchPointItem.PropertyType) ?? PropertyInfo_TouchPointItem.PropertyType));
            }

            DataTable_TouchPoint.Columns.Add(new DataColumn("InfoQuestUploadDate", Type.GetType("System.DateTime")));

            foreach (PXM_TouchPointFile_Body_TouchPoint PXM_TouchPointFile_Body_TouchPoint_Item in touchPointFile.Body)
            {
              object[] object_Item = new object[PropertyInfo_TouchPoint.Length + 1];
              for (int a = 0; a < PropertyInfo_TouchPoint.Length; a++)
              {
                object_Item[a] = PropertyInfo_TouchPoint[a].GetValue(PXM_TouchPointFile_Body_TouchPoint_Item);
              }

              object_Item[PropertyInfo_TouchPoint.Length] = DateTime.Now;

              DataTable_TouchPoint.Rows.Add(object_Item);
            }

            PXM_ReceivedFiles_TouchPoint_ProcessData(DataTable_TouchPoint, touchPointFile.Header.TouchPointFileNumber);
            PXM_ReceivedFiles_TouchPoint_CleanUpData();
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_ReceivedFiles_TouchPoint_CleanUpData();

            PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive TouchPoint Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
            PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful == "No")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = false;
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful == "Yes")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = true;
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler.Clear();

      PXM_ExternalResponse_Body_ExternalResponse_Message.FileNumber = touchPointFile.Header.TouchPointFileNumber;
      PXM_ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = ReturnMessage;
      PXM_ExternalResponse_Message.Body = PXM_ExternalResponse_Body_ExternalResponse_Message;

      return PXM_ExternalResponse_Message;
    }

    private void PXM_ReceivedFiles_TouchPoint_ProcessData(DataTable dataTable_TouchPoint, string touchPointFileNumber)
    {
      string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
      using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
      {
        SqlConnection_BulkCopy.Open();

        using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
        {
          SqlBulkCopy_File.DestinationTableName = "Form_PXM_ReceivedFiles_TouchPoint_ProcessedData";

          foreach (DataColumn DataColumn_ColumnNames in dataTable_TouchPoint.Columns)
          {
            string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_ReceivedFiles_TouchPoint_ProcessedData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
            using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
            {
              SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
              DataTable DataTable_Column;
              using (DataTable_Column = new DataTable())
              {
                DataTable_Column.Locale = CultureInfo.CurrentCulture;
                DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                if (DataTable_Column.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                  {
                    string name = DataRow_Row_ColumnData["name"].ToString();

                    SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                    SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                  }
                }
              }
            }
          }

          try
          {
            string SaveDataRequestResponse = PXM_ReceivedFiles_SaveReceivedFiles(dataTable_TouchPoint, "TouchPoint", touchPointFileNumber);
            if (!string.IsNullOrEmpty(SaveDataRequestResponse))
            {
              PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive TouchPoint Data failed; " + SaveDataRequestResponse, CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful = "No";
            }
            else
            {
              SqlBulkCopy_File.WriteToServer(dataTable_TouchPoint);
              PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive TouchPoint Data successful", CultureInfo.CurrentCulture));
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive TouchPoint Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful = "No";
              InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            }
            else
            {
              throw;
            }
          }
        }
      }
    }

    private static void PXM_ReceivedFiles_TouchPoint_CleanUpData()
    {
      string SQLStringCleanUpData = @";WITH CTE ( Hospital , HospitalCode , Date , Touchpoint_Doctor , Touchpoint_Facilities , Touchpoint_Food , Touchpoint_Medication , Touchpoint_Nursing , Touchpoint_ReceptionStaff , Touchpoint_Responsiveness , DuplicateCount )
                                      AS (
                                            SELECT	Hospital , HospitalCode , Date , Touchpoint_Doctor , Touchpoint_Facilities , Touchpoint_Food , Touchpoint_Medication , Touchpoint_Nursing , Touchpoint_ReceptionStaff , Touchpoint_Responsiveness , 
                                                    ROW_NUMBER() OVER(PARTITION BY Hospital , HospitalCode , Date , Touchpoint_Doctor , Touchpoint_Facilities , Touchpoint_Food , Touchpoint_Medication , Touchpoint_Nursing , Touchpoint_ReceptionStaff , Touchpoint_Responsiveness ORDER BY Hospital , HospitalCode , Date , Touchpoint_Doctor , Touchpoint_Facilities , Touchpoint_Food , Touchpoint_Medication , Touchpoint_Nursing , Touchpoint_ReceptionStaff , Touchpoint_Responsiveness ) AS DuplicateCount
                                            FROM		Form_PXM_ReceivedFiles_TouchPoint_ProcessedData
                                          )
                                      DELETE
                                      FROM CTE
                                      WHERE DuplicateCount > 1";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }


    public PXM_ExternalResponse TestDoctorQuestionsFile()
    {
      PXM_DoctorQuestionsFile DoctorQuestionsFile = new PXM_DoctorQuestionsFile();

      PXM_DoctorQuestionsFile_Header Header = new PXM_DoctorQuestionsFile_Header();
      Header.To = "To";
      Header.From = "From";
      Header.Payload = "Payload";
      Header.DoctorQuestionsFileNumber = "DoctorQuestionsFileNumber1";


      List<PXM_DoctorQuestionsFile_Body_DoctorQuestions> DoctorQuestions = new List<PXM_DoctorQuestionsFile_Body_DoctorQuestions>();

      for (int a = 1; a <= 5; a++)
      {
        PXM_DoctorQuestionsFile_Body_DoctorQuestions DoctorQuestions_Item = new PXM_DoctorQuestionsFile_Body_DoctorQuestions();
        DoctorQuestions_Item.VisitNumber = "VisitNumber_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.Question = "Question_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.Value = "Value_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.ValueLabel = "ValueLabel_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.Score = "Score_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.Hospital = "Hospital_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;
        DoctorQuestions_Item.HospitalCode = "HospitalCode_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + "_" + a;

        DoctorQuestions.Add(DoctorQuestions_Item);
      }

      DoctorQuestionsFile.Header = Header;
      DoctorQuestionsFile.Body = DoctorQuestions;

      PXM_ExternalResponse PXM_ExternalResponse_Message = PXM_ReceivedFiles_DoctorQuestions_ReceiveData(DoctorQuestionsFile);

      return PXM_ExternalResponse_Message;
    }

    public PXM_ExternalResponse PXM_ReceivedFiles_DoctorQuestions_ReceiveData(PXM_DoctorQuestionsFile doctorQuestionsFile)
    {
      PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler.Clear();
      PXM_ExternalResponse PXM_ExternalResponse_Message = new PXM_ExternalResponse();
      PXM_ExternalResponse_Body_ExternalResponse PXM_ExternalResponse_Body_ExternalResponse_Message = new PXM_ExternalResponse_Body_ExternalResponse();

      if (doctorQuestionsFile == null)
      {
        throw new ArgumentNullException("doctorQuestionsFile");
      }
      else
      {
        if (doctorQuestionsFile.Header == null || doctorQuestionsFile.Body == null || doctorQuestionsFile.Body.Count == 0)
        {
          PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive DoctorQuestions Data failed, Either Header or Body is empty", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful = "No";
        }
      }

      if (PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful == "Yes")
      {
        try
        {
          Type Type_DoctorQuestions = typeof(PXM_DoctorQuestionsFile_Body_DoctorQuestions);
          PropertyInfo[] PropertyInfo_DoctorQuestions = Type_DoctorQuestions.GetProperties();

          using (DataTable DataTable_DoctorQuestions = new DataTable())
          {
            DataTable_DoctorQuestions.Locale = CultureInfo.CurrentCulture;

            foreach (PropertyInfo PropertyInfo_DoctorQuestionsItem in PropertyInfo_DoctorQuestions)
            {
              DataTable_DoctorQuestions.Columns.Add(new DataColumn(PropertyInfo_DoctorQuestionsItem.Name, Nullable.GetUnderlyingType(PropertyInfo_DoctorQuestionsItem.PropertyType) ?? PropertyInfo_DoctorQuestionsItem.PropertyType));
            }

            DataTable_DoctorQuestions.Columns.Add(new DataColumn("InfoQuestUploadDate", Type.GetType("System.DateTime")));

            foreach (PXM_DoctorQuestionsFile_Body_DoctorQuestions PXM_DoctorQuestionsFile_Body_DoctorQuestions_Item in doctorQuestionsFile.Body)
            {
              object[] object_Item = new object[PropertyInfo_DoctorQuestions.Length + 1];
              for (int a = 0; a < PropertyInfo_DoctorQuestions.Length; a++)
              {
                object_Item[a] = PropertyInfo_DoctorQuestions[a].GetValue(PXM_DoctorQuestionsFile_Body_DoctorQuestions_Item);
              }

              object_Item[PropertyInfo_DoctorQuestions.Length] = DateTime.Now;

              DataTable_DoctorQuestions.Rows.Add(object_Item);
            }

            PXM_ReceivedFiles_DoctorQuestions_ProcessData(DataTable_DoctorQuestions, doctorQuestionsFile.Header.DoctorQuestionsFileNumber);
            PXM_ReceivedFiles_DoctorQuestions_CleanUpData();
          }
        }
        catch (Exception Exception_Error)
        {
          if (!string.IsNullOrEmpty(Exception_Error.ToString()))
          {
            PXM_ReceivedFiles_DoctorQuestions_CleanUpData();

            PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive DoctorQuestions Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
            PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful = "No";
            InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
          }
          else
          {
            throw;
          }
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful == "No")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = false;
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful == "Yes")
      {
        PXM_ExternalResponse_Body_ExternalResponse_Message.IsSuccessful = true;
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler.Clear();

      PXM_ExternalResponse_Body_ExternalResponse_Message.FileNumber = doctorQuestionsFile.Header.DoctorQuestionsFileNumber;
      PXM_ExternalResponse_Body_ExternalResponse_Message.ErrorMessage = ReturnMessage;
      PXM_ExternalResponse_Message.Body = PXM_ExternalResponse_Body_ExternalResponse_Message;

      return PXM_ExternalResponse_Message;
    }

    private void PXM_ReceivedFiles_DoctorQuestions_ProcessData(DataTable dataTable_DoctorQuestions, string doctorQuestionsFileNumber)
    {
      string BulkCopyConnectionString = InfoQuest_Connections.Connections("InfoQuest");
      using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
      {
        SqlConnection_BulkCopy.Open();

        using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
        {
          SqlBulkCopy_File.DestinationTableName = "Form_PXM_ReceivedFiles_DoctorQuestions_ProcessedData";

          foreach (DataColumn DataColumn_ColumnNames in dataTable_DoctorQuestions.Columns)
          {
            string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_PXM_ReceivedFiles_DoctorQuestions_ProcessedData') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
            using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
            {
              SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
              DataTable DataTable_Column;
              using (DataTable_Column = new DataTable())
              {
                DataTable_Column.Locale = CultureInfo.CurrentCulture;
                DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_Column).Copy();
                if (DataTable_Column.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row_ColumnData in DataTable_Column.Rows)
                  {
                    string name = DataRow_Row_ColumnData["name"].ToString();

                    SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                    SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                  }
                }
              }
            }
          }

          try
          {
            string SaveDataRequestResponse = PXM_ReceivedFiles_SaveReceivedFiles(dataTable_DoctorQuestions, "DoctorQuestions", doctorQuestionsFileNumber);
            if (!string.IsNullOrEmpty(SaveDataRequestResponse))
            {
              PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive DoctorQuestions Data failed; " + SaveDataRequestResponse, CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful = "No";
            }
            else
            {
              SqlBulkCopy_File.WriteToServer(dataTable_DoctorQuestions);
              PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive DoctorQuestions Data successful", CultureInfo.CurrentCulture));
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(Convert.ToString("Receive DoctorQuestions Data failed; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
              PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful = "No";
              InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
            }
            else
            {
              throw;
            }
          }
        }
      }
    }

    private static void PXM_ReceivedFiles_DoctorQuestions_CleanUpData()
    {
      string SQLStringCleanUpData = @";WITH CTE ( VisitNumber , Question , Value , ValueLabel , Score , Hospital , HospitalCode , DuplicateCount )
                                      AS (
                                            SELECT	VisitNumber , Question , Value , ValueLabel , Score , Hospital , HospitalCode ,
                                                    ROW_NUMBER() OVER(PARTITION BY VisitNumber , Question , Value , ValueLabel , Score , Hospital , HospitalCode ORDER BY VisitNumber , Question , Value , ValueLabel , Score , Hospital , HospitalCode ) AS DuplicateCount
                                            FROM		Form_PXM_ReceivedFiles_DoctorQuestions_ProcessedData
                                          )
                                      DELETE
                                      FROM CTE
                                      WHERE DuplicateCount > 1";
      using (SqlCommand SqlCommand_CleanUpData = new SqlCommand(SQLStringCleanUpData))
      {
        InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_CleanUpData);
      }
    }


    private static string PXM_ReceivedFiles_SaveReceivedFiles(DataTable dataTable_SaveReceivedFiles, string receivedFile, string fileNumber)
    {
      string ReturnMessage = "";

      string FileName = "PXM_" + receivedFile + "_" + fileNumber + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + ".xml";
      string SavePath = @"\\" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + @"\Service_MHS_PXM_ReceivedFiles_Upload\" + FileName + "";

      try
      {
        PXM_FromDataBase_Impersonation FromDataBase_Impersonation_Current = PXM_GetImpersonation();
        string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
        string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
        string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

        if (InfoQuest_Impersonate.ImpersonateUser(ImpersonationUserName, ImpersonationDomain, ImpersonationPassword))
        {
          using (StringWriter StringWriter_File = new StringWriter(CultureInfo.CurrentCulture))
          {
            dataTable_SaveReceivedFiles.TableName = receivedFile;
            dataTable_SaveReceivedFiles.WriteXml(StringWriter_File);

            XmlDocument XmlDocument_File = new XmlDocument();
            XmlDocument_File.LoadXml(StringWriter_File.ToString());

            XmlDocument_File.Save(SavePath);
          }

          string XMLFileName = SavePath.Substring(SavePath.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
          string ZIPFileName = XMLFileName.Replace(".xml", ".zip");

          string XMLFilePathAndName = SavePath;
          string ZIPFilePathAndName = SavePath.Replace(".xml", ".zip");

          using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
          {
            ZipArchive_PathAndName.CreateEntryFromFile(XMLFilePathAndName, XMLFileName);
          }

          using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
          {
            string ZIPFileContentType = "application/zip";
            BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
            Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

            string SQLStringInsertPXMReceivedFiles = @" INSERT INTO dbo.Form_PXM_ReceivedFiles_FileUploaded
                                                        ( ReceivedFile , ReceivedDate , ReceivedRecords , FileName , FileContentType , FileData )
                                                        VALUES
                                                        ( @ReceivedFile , @ReceivedDate , @ReceivedRecords , @FileName , @FileContentType , @FileData )";
            using (SqlCommand SqlCommand_InsertPXMReceivedFiles = new SqlCommand(SQLStringInsertPXMReceivedFiles))
            {
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@ReceivedFile", receivedFile);
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@ReceivedDate", DateTime.Now);
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@ReceivedRecords", dataTable_SaveReceivedFiles.Rows.Count);
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@FileName", ZIPFileName);
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@FileContentType", ZIPFileContentType);
              SqlCommand_InsertPXMReceivedFiles.Parameters.AddWithValue("@FileData", Byte_ZIPFile);
              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_WCF(SqlCommand_InsertPXMReceivedFiles);
            }
          }

          File.Delete(XMLFilePathAndName);
          File.Delete(ZIPFilePathAndName);

          InfoQuest_Impersonate.UndoImpersonation();
        }
        else
        {
          ReturnMessage = Convert.ToString(SavePath + " user impersonation failed for user: " + ImpersonationUserName, CultureInfo.CurrentCulture);
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          InfoQuest_Impersonate.UndoImpersonation();

          ReturnMessage = Convert.ToString("'" + FileName + "' could not be created at '" + SavePath + "'; Exception Message: " + Exception_Error.Message.ToString() + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture);
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      return ReturnMessage;
    }

    public string PXM_ReceivedFiles_ProcessData()
    {
      PXM_ReceivedFiles_ProcessData_ReturnMessageHandler.Clear();

      try
      {
        PXM_ReceivedFiles_ProcessData_ReturnMessageHandlers(Convert.ToString("Test", CultureInfo.CurrentCulture));
        PXM_ReceivedFiles_ProcessData_Successful = "Yes";

        //PXM_ReceivedFiles_ProcessData_ProcessData_PDInPatient();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDInpatientMinor();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDEmergency();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDEmergencyMinor();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDContactUs();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDRehab();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDRehabMinor();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDMental();
        //PXM_ReceivedFiles_ProcessData_ProcessData_PDMentalMinor();
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          PXM_ReceivedFiles_ProcessData_ReturnMessageHandlers(Convert.ToString("Exception Message: " + Exception_Error.Message + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_ProcessData_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_ProcessData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_ProcessData_Successful == "No")
      {
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_ProcessData_Successful == "Yes")
      {
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_ProcessData_ReturnMessageHandler.Clear();

      return ReturnMessage;
    }

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDInPatient()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDInpatientMinor()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDEmergency()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDEmergencyMinor()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDContactUs()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDRehab()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDRehabMinor()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDMental()
    //{

    //}

    //private void PXM_ReceivedFiles_ProcessData_ProcessData_PDMentalMinor()
    //{

    //}


    public string PXM_ReceivedFiles_CheckReceiveData()
    {
      PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler.Clear();

      try
      {
        string SQLStringFile = @"SELECT		TempTableA.FileToProcess , 
					                              TempTableB.FileProcessed 
                              FROM			( 
						                              SELECT 'Escalation' AS FileToProcess , 'Escalation' AS FileProcessed UNION
						                              SELECT 'Report' AS FileToProcess , 'Report' AS FileProcessed UNION
						                              SELECT 'TouchPoint' AS FileToProcess , 'TouchPoint' AS FileProcessed UNION
						                              SELECT 'DoctorQuestions' AS FileToProcess , 'DoctorQuestions' AS FileProcessed 
					                              ) AS TempTableA 
					                              LEFT JOIN 
					                              ( 
						                              SELECT		DISTINCT 
											                              ReceivedFile AS FileProcessed 
						                              FROM			Form_PXM_ReceivedFiles_FileUploaded 
						                              WHERE		ReceivedDate >= CAST(CONVERT(NVARCHAR(MAX), DATEADD(DAY, -1, GETDATE()), 110) AS DATETIME) 
					                              ) AS TempTableB 
					                              ON TempTableA.FileProcessed = TempTableB.FileProcessed
                              ORDER BY	TempTableA.FileToProcess";
        using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
        {
          DataTable DataTable_File;
          using (DataTable_File = new DataTable())
          {
            DataTable_File.Locale = CultureInfo.CurrentCulture;
            DataTable_File = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_File).Copy();
            if (DataTable_File.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_File.Rows)
              {
                string FileToProcess = DataRow_Row["FileToProcess"].ToString();
                string FileProcessed = DataRow_Row["FileProcessed"].ToString();

                if (string.IsNullOrEmpty(FileProcessed))
                {
                  PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandlers(Convert.ToString(FileToProcess + " file not received yesterday", CultureInfo.CurrentCulture));
                  PXM_ReceivedFiles_CheckReceiveData_Successful = "No";
                }
                else
                {
                  PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandlers(Convert.ToString(FileToProcess + " file received yesterday", CultureInfo.CurrentCulture));
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
          PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandlers(Convert.ToString("Exception Message: " + Exception_Error.Message + "; Exception StackTrace: " + Exception_Error.StackTrace.ToString() + ";", CultureInfo.CurrentCulture));
          PXM_ReceivedFiles_CheckReceiveData_Successful = "No";
          InfoQuest_Exceptions.Exceptions_OwnMessage(Exception_Error.Message.ToString(), Exception_Error.StackTrace.ToString(), MethodBase.GetCurrentMethod().Name, "", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          throw;
        }
      }

      string ReturnMessage = "";
      foreach (KeyValuePair<string, string> KeyValuePair_ReturnMessage in PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler)
      {
        ReturnMessage = ReturnMessage + KeyValuePair_ReturnMessage.Value + "\n";
      }

      if (PXM_ReceivedFiles_CheckReceiveData_Successful == "No")
      {
        InfoQuest_WCF.WCF_SendEmail_Error(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }
      else if (PXM_ReceivedFiles_CheckReceiveData_Successful == "Yes")
      {
        //InfoQuest_WCF.WCF_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, ReturnMessage);
      }

      PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler.Clear();

      return ReturnMessage;
    }


    string PXM_ReceivedFiles_Escalation_ReceiveData_Successful = "Yes";
    string PXM_ReceivedFiles_Report_ReceiveData_Successful = "Yes";
    string PXM_ReceivedFiles_TouchPoint_ReceiveData_Successful = "Yes";
    string PXM_ReceivedFiles_DoctorQuestions_ReceiveData_Successful = "Yes";
    string PXM_ReceivedFiles_ProcessData_Successful = "Yes";
    string PXM_ReceivedFiles_CheckReceiveData_Successful = "Yes";

    private static Dictionary<string, string> PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_Escalation_ReceiveData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_Escalation_ReceiveData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_Report_ReceiveData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_Report_ReceiveData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_TouchPoint_ReceiveData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_TouchPoint_ReceiveData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_DoctorQuestions_ReceiveData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_DoctorQuestions_ReceiveData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_ReceivedFiles_ProcessData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_ProcessData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_ProcessData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_ProcessData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_ProcessData: " + ReturnMessage);
      }
    }

    private static Dictionary<string, string> PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler = new Dictionary<string, string>();
    private static void PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandlers(string ReturnMessage)
    {
      if (!PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler.ContainsKey(ReturnMessage))
      {
        PXM_ReceivedFiles_CheckReceiveData_ReturnMessageHandler.Add(ReturnMessage, "PXM_ReceivedFiles_CheckReceiveData: " + ReturnMessage);
      }
    }
    //---END--- --PXM ReceivedFiles--//



    //--START-- --PXM--//
    private class PXM_FromDataBase_Impersonation
    {
      public string ImpersonationUserName { get; set; }
      public string ImpersonationPassword { get; set; }
      public string ImpersonationDomain { get; set; }
    }

    private static PXM_FromDataBase_Impersonation PXM_GetImpersonation()
    {
      PXM_FromDataBase_Impersonation FromDataBase_Impersonation_New = new PXM_FromDataBase_Impersonation();

      string SQLStringSystemAccount = "SELECT SystemAccount_Domain , SystemAccount_UserName , SystemAccount_Password FROM Administration_SystemAccount WHERE SystemAccount_Id = 1";
      using (SqlCommand SqlCommand_SystemAccount = new SqlCommand(SQLStringSystemAccount))
      {
        DataTable DataTable_SystemAccount;
        using (DataTable_SystemAccount = new DataTable())
        {
          DataTable_SystemAccount.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemAccount = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WCF(SqlCommand_SystemAccount).Copy();
          if (DataTable_SystemAccount.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemAccount.Rows)
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
    //---END--- --PXM--//
  }
}


