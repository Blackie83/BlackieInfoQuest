using System;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Generic;

namespace InfoQuestWS
{
  public partial class InfoQuestWindowsService : ServiceBase
  {
    static void Main(string[] args)
    {
      using (InfoQuestWindowsService InfoQuestWindowsService = new InfoQuestWindowsService())
      {
        if (Environment.UserInteractive)
        {
          InfoQuestWindowsService.OnStart(args);
          InfoQuestWindowsService.OnStop();
        }
        else
        {
          Run(new InfoQuestWindowsService());
        }
      }
    }


    //--START-- --Eventlog EventId Codes--//
    //--It must be in the range between '0' and '65535'.
    private Dictionary<string, string> EventIdDescriptionHandler = new Dictionary<string, string>();

    protected void EventIdDescriptionHandlers()
    {
      EventIdDescriptionHandler.Add("1", "InfoQuest Windows Service Started");
      EventIdDescriptionHandler.Add("2", "InfoQuest Windows Service Running");
      EventIdDescriptionHandler.Add("3", "InfoQuest Windows Service Stopped");
      EventIdDescriptionHandler.Add("11", "PXM_PDCH_Event_CreateFile_Automated");
      EventIdDescriptionHandler.Add("12", "PXM_PDCH_Event_CreateFile_AutomatedMissing");
      EventIdDescriptionHandler.Add("13", "PXM_PDCH_Escalation_ExportData_Automated");
      EventIdDescriptionHandler.Add("14", "PXM_PDCH_Report_ExportData_Automated");
      EventIdDescriptionHandler.Add("15", "PXM_PDCH_Escalation_CheckFileProcessing");
      EventIdDescriptionHandler.Add("16", "PXM_PDCH_Report_CheckFileProcessing");
      EventIdDescriptionHandler.Add("17", "MonthlyHospitalStatistics_CreateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("18", "MonthlyHospitalStatistics_UpdateFormData_Automated");
      EventIdDescriptionHandler.Add("19", "MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated");
      EventIdDescriptionHandler.Add("110", "MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated");
      EventIdDescriptionHandler.Add("111", "MonthlyPharmacyStatistics_CreateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("112", "MonthlyPharmacyStatistics_UpdateFormData_Automated");
      //EventIdDescriptionHandler.Add("113", "ECM_CreateMonthlyForms_Automated");
      //EventIdDescriptionHandler.Add("114", "ECM_UpdateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("115", "Administration_ArchiveRecords_Automated");
      EventIdDescriptionHandler.Add("116", "Administration_BeingModifiedUnlock_Automated");
      EventIdDescriptionHandler.Add("117", "Administration_SecurityAccess_CleanUp_Automated");
      EventIdDescriptionHandler.Add("118", "PXM_PDCH_Escalation_CheckSurveyProcessing");
      EventIdDescriptionHandler.Add("119", "SustainabilityManagement_CreateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("120", "SustainabilityManagement_UpdateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("121", "SustainabilityManagement_MappingMissing_Automated");
      EventIdDescriptionHandler.Add("122", "MonthlyOccupationalHealthStatistics_CreateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("123", "MonthlyOccupationalHealthStatistics_UpdateMonthlyForms_Automated");
      EventIdDescriptionHandler.Add("124", "Alert_PendingApprovalNotifications_Automated");
      EventIdDescriptionHandler.Add("125", "Incident_PendingApprovalNotifications_Automated");
      EventIdDescriptionHandler.Add("126", "PXM_Event_GetEventData");
      EventIdDescriptionHandler.Add("127", "PXM_Event_CheckGetEventData");
      EventIdDescriptionHandler.Add("128", "PXM_Event_SendEventData");
      EventIdDescriptionHandler.Add("129", "PXM_ReceivedFiles_ProcessData");
      EventIdDescriptionHandler.Add("130", "PXM_ReceivedFiles_CheckReceiveData");
      //EventIdDescriptionHandler.Add("131", "");
    }

    protected string EventIdDescription(string eventId)
    {
      EventIdDescriptionHandlers();

      if (EventIdDescriptionHandler.ContainsKey(eventId))
      {
        string EventIdDescription = EventIdDescriptionHandler[eventId];
        EventIdDescriptionHandler.Clear();
        return EventIdDescription;
      }
      else
      {
        return "No Event Id Name";
      }
    }
    //---END--- --Eventlog EventId Codes--//    


    private EventLog EventLog_InfoQuest = new EventLog();

    private static Timer Timer_ExecuteEveryHour;
    private static Timer Timer_ExecuteEvery2AMDaily;
    private static Timer Timer_ExecuteEvery6AMDaily;
    private static Timer Timer_ExecuteEvery8AMDaily;

    public InfoQuestWindowsService()
    {
      InitializeComponent();

      InfoQuestWCF.InfoQuest_EventLog.EventLog_CreateEventLog("InfoQuest_Source", "InfoQuest_WS_Log");
      EventLog_InfoQuest.Source = "InfoQuest_Source";
      EventLog_InfoQuest.Log = "InfoQuest_WS_Log";
    }

    protected override void OnStart(string[] args)
    {
      int EventID = 1;

      try
      {
        string Message = EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture));
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));

        Thread Thread_ExecuteEveryHour = new Thread(new ThreadStart(StartTimer_ExecuteEveryHour));
        Thread Thread_ExecuteEvery2AMDaily = new Thread(new ThreadStart(StartTimer_ExecuteEvery2AMDaily));
        Thread Thread_ExecuteEvery6AMDaily = new Thread(new ThreadStart(StartTimer_ExecuteEvery6AMDaily));
        Thread Thread_ExecuteEvery8AMDaily = new Thread(new ThreadStart(StartTimer_ExecuteEvery8AMDaily));

        Thread_ExecuteEveryHour.Start();
        Thread_ExecuteEvery2AMDaily.Start();
        Thread_ExecuteEvery6AMDaily.Start();
        Thread_ExecuteEvery8AMDaily.Start();

        Thread_ExecuteEveryHour.Join();
        Thread_ExecuteEvery2AMDaily.Join();
        Thread_ExecuteEvery6AMDaily.Join();
        Thread_ExecuteEvery8AMDaily.Join();
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    protected override void OnStop()
    {
      int EventID = 3;

      try
      {
        Timer_ExecuteEveryHour.Dispose();
        Timer_ExecuteEvery2AMDaily.Dispose();
        Timer_ExecuteEvery6AMDaily.Dispose();
        Timer_ExecuteEvery8AMDaily.Dispose();

        string Message = EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture));
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--START-- --FromDataBase--//
    private class FromDataBase_SecurityAccessWCF
    {
      public string SecurityAccessWCFUserName { get; set; }
      public string SecurityAccessWCFPassword { get; set; }
    }

    private static FromDataBase_SecurityAccessWCF GetSecurityAccessWCF(string method)
    {
      FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_New = new FromDataBase_SecurityAccessWCF();

      string SQLStringSecurityAccessWCF = "SELECT SecurityAccess_WCF_UserName , SecurityAccess_WCF_Password FROM Administration_SecurityAccess_WCF WHERE SecurityAccess_WCF_Method = @SecurityAccess_WCF_Method";
      using (SqlCommand SqlCommand_SecurityAccessWCF = new SqlCommand(SQLStringSecurityAccessWCF))
      {
        SqlCommand_SecurityAccessWCF.Parameters.AddWithValue("@SecurityAccess_WCF_Method", method);
        DataTable DataTable_SecurityAccessWCF;
        using (DataTable_SecurityAccessWCF = new DataTable())
        {
          DataTable_SecurityAccessWCF.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityAccessWCF = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_WindowsService(SqlCommand_SecurityAccessWCF).Copy();
          if (DataTable_SecurityAccessWCF.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SecurityAccessWCF.Rows)
            {
              FromDataBase_SecurityAccessWCF_New.SecurityAccessWCFUserName = DataRow_Row["SecurityAccess_WCF_UserName"].ToString();
              FromDataBase_SecurityAccessWCF_New.SecurityAccessWCFPassword = DataRow_Row["SecurityAccess_WCF_Password"].ToString();
            }
          }
        }
      }

      return FromDataBase_SecurityAccessWCF_New;
    }
    //---END--- --FromDataBase--//


    //--START-- --StartTimer--//
    private void StartTimer_ExecuteEveryHour()
    {
      DateTime CurrentDate = DateTime.Now;

      TimeSpan TimeSpan_DueTime_ExecuteEveryHour = new TimeSpan(0, 0, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      TimeSpan TimeSpan_Period_ExecuteEveryHour = new TimeSpan(0, 1, 0, 0);
      TimerCallback TimerCallback_ExecuteEveryHour = new TimerCallback(StartFunction_ExecuteEveryHour);
      Timer_ExecuteEveryHour = new Timer(TimerCallback_ExecuteEveryHour, null, TimeSpan_DueTime_ExecuteEveryHour, TimeSpan_Period_ExecuteEveryHour);
      TimerCallback_ExecuteEveryHour.Invoke(1);
    }

    private void StartTimer_ExecuteEvery2AMDaily()
    {
      DateTime CurrentDate = DateTime.Now;

      TimeSpan TimeSpan_DueTime_ExecuteEvery2AMDaily;
      if (CurrentDate.Hour >= 2 && CurrentDate.Hour <= 23)
      {
        TimeSpan_DueTime_ExecuteEvery2AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 2, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }
      else
      {
        TimeSpan_DueTime_ExecuteEvery2AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 2 - 24, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }

      TimeSpan TimeSpan_Period_ExecuteEvery2AMDaily = new TimeSpan(1, 0, 0, 0);
      TimerCallback TimerCallback_ExecuteEvery2AMDaily = new TimerCallback(StartFunction_ExecuteEvery2AMDaily);
      Timer_ExecuteEvery2AMDaily = new Timer(TimerCallback_ExecuteEvery2AMDaily, null, TimeSpan_DueTime_ExecuteEvery2AMDaily, TimeSpan_Period_ExecuteEvery2AMDaily);
      TimerCallback_ExecuteEvery2AMDaily.Invoke(1);
    }

    private void StartTimer_ExecuteEvery6AMDaily()
    {
      DateTime CurrentDate = DateTime.Now;

      TimeSpan TimeSpan_DueTime_ExecuteEvery6AMDaily;
      if (CurrentDate.Hour >= 6 && CurrentDate.Hour <= 23)
      {
        TimeSpan_DueTime_ExecuteEvery6AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 6, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }
      else
      {
        TimeSpan_DueTime_ExecuteEvery6AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 6 - 24, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }

      TimeSpan TimeSpan_Period_ExecuteEvery6AMDaily = new TimeSpan(1, 0, 0, 0);
      TimerCallback TimerCallback_ExecuteEvery6AMDaily = new TimerCallback(StartFunction_ExecuteEvery6AMDaily);
      Timer_ExecuteEvery6AMDaily = new Timer(TimerCallback_ExecuteEvery6AMDaily, null, TimeSpan_DueTime_ExecuteEvery6AMDaily, TimeSpan_Period_ExecuteEvery6AMDaily);
      TimerCallback_ExecuteEvery6AMDaily.Invoke(1);
    }

    private void StartTimer_ExecuteEvery8AMDaily()
    {
      DateTime CurrentDate = DateTime.Now;

      TimeSpan TimeSpan_DueTime_ExecuteEvery8AMDaily;
      if (CurrentDate.Hour >= 8 && CurrentDate.Hour <= 23)
      {
        TimeSpan_DueTime_ExecuteEvery8AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 8, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }
      else
      {
        TimeSpan_DueTime_ExecuteEvery8AMDaily = new TimeSpan(0, 23 - CurrentDate.Hour + 8 - 24, 59 - CurrentDate.Minute, 60 - CurrentDate.Second);
      }

      TimeSpan TimeSpan_Period_ExecuteEvery8AMDaily = new TimeSpan(1, 0, 0, 0);
      TimerCallback TimerCallback_ExecuteEvery8AMDaily = new TimerCallback(StartFunction_ExecuteEvery8AMDaily);
      Timer_ExecuteEvery8AMDaily = new Timer(TimerCallback_ExecuteEvery8AMDaily, null, TimeSpan_DueTime_ExecuteEvery8AMDaily, TimeSpan_Period_ExecuteEvery8AMDaily);
      TimerCallback_ExecuteEvery8AMDaily.Invoke(1);
    }
    //---END--- --StartTimer--//


    //--START-- --StartFunction--//
    private void StartFunction_ExecuteEveryHour(object state)
    {
      Thread Thread_PXMClient_PXMPDCHEventCreateFileAutomatedMissing = new Thread(new ThreadStart(PXMClient_PXMPDCHEventCreateFileAutomatedMissing));
      Thread Thread_PXMClient_PXMPDCHEscalationExportDataAutomated = new Thread(new ThreadStart(PXMClient_PXMPDCHEscalationExportDataAutomated));
      Thread Thread_PXMClient_PXMPDCHReportExportDataAutomated = new Thread(new ThreadStart(PXMClient_PXMPDCHReportExportDataAutomated));
      Thread Thread_AdministrationClient_AdministrationBeingModifiedUnlockAutomated = new Thread(new ThreadStart(AdministrationClient_AdministrationBeingModifiedUnlockAutomated));
      Thread Thread_PXMClient_PXMEventCheckGetEventData = new Thread(new ThreadStart(PXMClient_PXMEventCheckGetEventData));
      Thread Thread_PXMClient_PXMReceivedFilesProcessData = new Thread(new ThreadStart(PXMClient_PXMReceivedFilesProcessData));

      Thread_PXMClient_PXMPDCHEventCreateFileAutomatedMissing.Start();
      Thread_PXMClient_PXMPDCHEscalationExportDataAutomated.Start();
      Thread_PXMClient_PXMPDCHReportExportDataAutomated.Start();
      Thread_AdministrationClient_AdministrationBeingModifiedUnlockAutomated.Start();
      Thread_PXMClient_PXMEventCheckGetEventData.Start();
      Thread_PXMClient_PXMReceivedFilesProcessData.Start();

      Thread_PXMClient_PXMPDCHEventCreateFileAutomatedMissing.Join();
      Thread_PXMClient_PXMPDCHEscalationExportDataAutomated.Join();
      Thread_PXMClient_PXMPDCHReportExportDataAutomated.Join();
      Thread_AdministrationClient_AdministrationBeingModifiedUnlockAutomated.Join();
      Thread_PXMClient_PXMEventCheckGetEventData.Join();
      Thread_PXMClient_PXMReceivedFilesProcessData.Join();
    }

    private void StartFunction_ExecuteEvery2AMDaily(object state)
    {
      Thread Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsCreateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsCreateMonthlyFormsAutomated));
      Thread Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsCreateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsCreateMonthlyFormsAutomated));
      //Thread Thread_ECMClient_ECMCreateMonthlyFormsAutomated = new Thread(new ThreadStart(ECMClient_ECMCreateMonthlyFormsAutomated));
      Thread Thread_AdministrationClient_AdministrationArchiveRecordsAutomated = new Thread(new ThreadStart(AdministrationClient_AdministrationArchiveRecordsAutomated));
      Thread Thread_AdministrationClient_AdministrationSecurityAccessCleanUpAutomated = new Thread(new ThreadStart(AdministrationClient_AdministrationSecurityAccessCleanUpAutomated));
      Thread Thread_SustainabilityManagementClient_SustainabilityManagementCreateMonthlyFormsAutomated = new Thread(new ThreadStart(SustainabilityManagementClient_SustainabilityManagementCreateMonthlyFormsAutomated));
      Thread Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsCreateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsCreateMonthlyFormsAutomated));

      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsCreateMonthlyFormsAutomated.Start();
      Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsCreateMonthlyFormsAutomated.Start();
      //Thread_ECMClient_ECMCreateMonthlyFormsAutomated.Start();
      Thread_AdministrationClient_AdministrationArchiveRecordsAutomated.Start();
      Thread_AdministrationClient_AdministrationSecurityAccessCleanUpAutomated.Start();
      Thread_SustainabilityManagementClient_SustainabilityManagementCreateMonthlyFormsAutomated.Start();
      Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsCreateMonthlyFormsAutomated.Start();

      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsCreateMonthlyFormsAutomated.Join();
      Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsCreateMonthlyFormsAutomated.Join();
      //Thread_ECMClient_ECMCreateMonthlyFormsAutomated.Join();
      Thread_AdministrationClient_AdministrationArchiveRecordsAutomated.Join();
      Thread_AdministrationClient_AdministrationSecurityAccessCleanUpAutomated.Join();
      Thread_SustainabilityManagementClient_SustainabilityManagementCreateMonthlyFormsAutomated.Join();
      Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsCreateMonthlyFormsAutomated.Join();
    }

    private void StartFunction_ExecuteEvery6AMDaily(object state)
    {
      Thread Thread_InfoQuest_WindowsService_Running = new Thread(new ThreadStart(InfoQuest_WindowsService_Running));
      Thread Thread_PXMClient_PXMPDCHEventCreateFileAutomated = new Thread(new ThreadStart(PXMClient_PXMPDCHEventCreateFileAutomated));
      Thread Thread_PXMClient_PXMPDCHEscalationCheckFileProcessing = new Thread(new ThreadStart(PXMClient_PXMPDCHEscalationCheckFileProcessing));
      Thread Thread_PXMClient_PXMPDCHEscalationCheckSurveyProcessing = new Thread(new ThreadStart(PXMClient_PXMPDCHEscalationCheckSurveyProcessing));
      Thread Thread_PXMClient_PXMPDCHReportCheckFileProcessing = new Thread(new ThreadStart(PXMClient_PXMPDCHReportCheckFileProcessing));
      Thread Thread_SustainabilityManagementClient_SustainabilityManagementMappingMissingAutomated = new Thread(new ThreadStart(SustainabilityManagementClient_SustainabilityManagementMappingMissingAutomated));
      Thread Thread_AlertClient_AlertPendingApprovalNotificationsAutomated = new Thread(new ThreadStart(AlertClient_AlertPendingApprovalNotificationsAutomated));
      Thread Thread_IncidentClient_IncidentPendingApprovalNotificationsAutomated = new Thread(new ThreadStart(IncidentClient_IncidentPendingApprovalNotificationsAutomated));
      Thread Thread_PXMClient_PXMEventGetEventData = new Thread(new ThreadStart(PXMClient_PXMEventGetEventData));
      Thread Thread_PXMClient_PXMReceivedFilesCheckReceiveData = new Thread(new ThreadStart(PXMClient_PXMReceivedFilesCheckReceiveData));

      Thread_InfoQuest_WindowsService_Running.Start();
      Thread_PXMClient_PXMPDCHEventCreateFileAutomated.Start();
      Thread_PXMClient_PXMPDCHEscalationCheckFileProcessing.Start();
      Thread_PXMClient_PXMPDCHEscalationCheckSurveyProcessing.Start();
      Thread_PXMClient_PXMPDCHReportCheckFileProcessing.Start();
      Thread_SustainabilityManagementClient_SustainabilityManagementMappingMissingAutomated.Start();
      Thread_AlertClient_AlertPendingApprovalNotificationsAutomated.Start();
      Thread_IncidentClient_IncidentPendingApprovalNotificationsAutomated.Start();
      Thread_PXMClient_PXMEventGetEventData.Start();
      Thread_PXMClient_PXMReceivedFilesCheckReceiveData.Start();

      Thread_InfoQuest_WindowsService_Running.Join();
      Thread_PXMClient_PXMPDCHEventCreateFileAutomated.Join();
      Thread_PXMClient_PXMPDCHEscalationCheckFileProcessing.Join();
      Thread_PXMClient_PXMPDCHEscalationCheckSurveyProcessing.Join();
      Thread_PXMClient_PXMPDCHReportCheckFileProcessing.Join();
      Thread_SustainabilityManagementClient_SustainabilityManagementMappingMissingAutomated.Join();
      Thread_AlertClient_AlertPendingApprovalNotificationsAutomated.Join();
      Thread_IncidentClient_IncidentPendingApprovalNotificationsAutomated.Join();
      Thread_PXMClient_PXMEventGetEventData.Join();
      Thread_PXMClient_PXMReceivedFilesCheckReceiveData.Join();
    }

    private void StartFunction_ExecuteEvery8AMDaily(object state)
    {
      Thread Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsUpdateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsUpdateMonthlyFormsAutomated));
      Thread Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsInsertOrganismsAutomated = new Thread(new ThreadStart(MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsInsertOrganismsAutomated));
      Thread Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsUpdateOrganismsAutomated = new Thread(new ThreadStart(MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsUpdateOrganismsAutomated));
      Thread Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsUpdateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsUpdateMonthlyFormsAutomated));
      //Thread Thread_ECMClient_ECMUpdateMonthlyFormsAutomated = new Thread(new ThreadStart(ECMClient_ECMUpdateMonthlyFormsAutomated));
      Thread Thread_SustainabilityManagementClient_SustainabilityManagementUpdateMonthlyFormsAutomated = new Thread(new ThreadStart(SustainabilityManagementClient_SustainabilityManagementUpdateMonthlyFormsAutomated));
      Thread Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsUpdateMonthlyFormsAutomated = new Thread(new ThreadStart(MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsUpdateMonthlyFormsAutomated));
      Thread Thread_PXMClient_PXMEventSendEventData = new Thread(new ThreadStart(PXMClient_PXMEventSendEventData));

      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsUpdateMonthlyFormsAutomated.Start();
      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsInsertOrganismsAutomated.Start();
      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsUpdateOrganismsAutomated.Start();
      Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsUpdateMonthlyFormsAutomated.Start();
      //Thread_ECMClient_ECMUpdateMonthlyFormsAutomated.Start();
      Thread_SustainabilityManagementClient_SustainabilityManagementUpdateMonthlyFormsAutomated.Start();
      Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsUpdateMonthlyFormsAutomated.Start();
      Thread_PXMClient_PXMEventSendEventData.Start();

      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsUpdateMonthlyFormsAutomated.Join();
      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsInsertOrganismsAutomated.Join();
      Thread_MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsUpdateOrganismsAutomated.Join();
      Thread_MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsUpdateMonthlyFormsAutomated.Join();
      //Thread_ECMClient_ECMUpdateMonthlyFormsAutomated.Join();
      Thread_SustainabilityManagementClient_SustainabilityManagementUpdateMonthlyFormsAutomated.Join();
      Thread_MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsUpdateMonthlyFormsAutomated.Join();
      Thread_PXMClient_PXMEventSendEventData.Join();
    }
    //---END--- --StartFunction--//


    //--START-- --Function--//
    //--InfoQuest
    private void InfoQuest_WindowsService_Running()
    {
      int EventID = 2;

      string Message = EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture));
      EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
      InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    }


    //--PXM PDCH
    #region PXMClient_PXMPDCHEventCreateFileAutomated
    //private void PXMClient_PXMPDCHEventCreateFileAutomated()
    //{
    //  int EventID = 11;

    //  try
    //  {
    //    TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
    //    TimeSpan StartTime = new TimeSpan(5, 59, 0);
    //    TimeSpan EndTime = new TimeSpan(6, 9, 0);
    //    if (CurrentTime > StartTime && CurrentTime < EndTime)
    //    {
    //      using (Service_InfoQuest_PXMClient ServiceClient_PXM = new Service_InfoQuest_PXMClient())
    //      {
    //        FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Event_CreateFile_Automated");
    //        string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
    //        string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

    //        ServiceClient_PXM.Endpoint.Address = new EndpointAddress(new Uri("http://" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + ":5008/Service_InfoQuest.svc"));
    //        string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + ServiceClient_PXM.PXM_PDCH_Event_CreateFile_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);

    //        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //        //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //      }
    //    }
    //    else
    //    {
    //      string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
    //      EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //      //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //    }
    //  }
    //  catch (Exception Exception_Error)
    //  {
    //    if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
    //    {
    //      string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
    //      EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
    //      InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //    }
    //    else
    //    {
    //      throw;
    //    }
    //  }
    //}
    #endregion
    private void PXMClient_PXMPDCHEventCreateFileAutomated()
    {
      int EventID = 11;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Event_CreateFile_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Event_CreateFile_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);

          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHEventCreateFileAutomatedMissing()
    {
      int EventID = 12;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(6, 59, 0);
        TimeSpan EndTime = new TimeSpan(23, 59, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Event_CreateFile_AutomatedMissing");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Event_CreateFile_AutomatedMissing(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes between 06:59:00 and 23:59:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHEscalationExportDataAutomated()
    {
      int EventID = 13;

      try
      {
        FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Escalation_ExportData_Automated");
        string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
        string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

        InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
        string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Escalation_ExportData_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHReportExportDataAutomated()
    {
      int EventID = 14;

      try
      {
        FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Report_ExportData_Automated");
        string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
        string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

        InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
        string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Report_ExportData_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHEscalationCheckFileProcessing()
    {
      int EventID = 15;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Escalation_CheckFileProcessing");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Escalation_CheckFileProcessing(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHEscalationCheckSurveyProcessing()
    {
      int EventID = 118;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Escalation_CheckSurveyProcessing");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Escalation_CheckSurveyProcessing(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMPDCHReportCheckFileProcessing()
    {
      int EventID = 16;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("PXM_PDCH_Report_CheckFileProcessing");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_PXM IServiceInfoQuestPXM = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestPXM.PXM_PDCH_Report_CheckFileProcessing(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Monthly Hospital Statistics
    private void MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsCreateMonthlyFormsAutomated()
    {
      int EventID = 17;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyHospitalStatistics_CreateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyHospitalStatistics IServiceInfoQuestMonthlyHospitalStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyHospitalStatistics.MonthlyHospitalStatistics_CreateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsUpdateMonthlyFormsAutomated()
    {
      int EventID = 18;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyHospitalStatistics_UpdateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyHospitalStatistics IServiceInfoQuestMonthlyHospitalStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyHospitalStatistics.MonthlyHospitalStatistics_UpdateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsInsertOrganismsAutomated()
    {
      int EventID = 19;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyHospitalStatistics IServiceInfoQuestMonthlyHospitalStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyHospitalStatistics.MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void MonthlyHospitalStatisticsClient_MonthlyHospitalStatisticsOrganismsUpdateOrganismsAutomated()
    {
      int EventID = 110;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyHospitalStatistics IServiceInfoQuestMonthlyHospitalStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyHospitalStatistics.MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Monthly Pharmacy Statistics
    private void MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsCreateMonthlyFormsAutomated()
    {
      int EventID = 111;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyPharmacyStatistics_CreateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyPharmacyStatistics IServiceInfoQuestMonthlyPharmacyStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyPharmacyStatistics.MonthlyPharmacyStatistics_CreateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void MonthlyPharmacyStatisticsClient_MonthlyPharmacyStatisticsUpdateMonthlyFormsAutomated()
    {
      int EventID = 112;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyPharmacyStatistics_UpdateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyPharmacyStatistics IServiceInfoQuestMonthlyPharmacyStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyPharmacyStatistics.MonthlyPharmacyStatistics_UpdateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--ECM
    #region ECM
    //private void ECMClient_ECMCreateMonthlyFormsAutomated()
    //{
    //  //Int32 EventID = 113;

    //  //try
    //  //{
    //  //  Int32 CurrentDay = DateTime.Now.Day;
    //  //  if (CurrentDay == 1)
    //  //  {
    //  //    using (Service_InfoQuest_ECMClient ServiceClient_ECMClient = new Service_InfoQuest_ECMClient())
    //  //    {
    //  //      FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("ECM_CreateMonthlyForms_Automated");
    //  //      String SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
    //  //      String SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

    //  //      ServiceClient_ECMClient.Endpoint.Address = new EndpointAddress(new Uri("http://" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + ":5008/Service_InfoQuest.svc"));
    //  //      String Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + ServiceClient_ECMClient.ECM_CreateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
    //  //      EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //  //      //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //    }
    //  //  }
    //  //  else
    //  //  {
    //  //    String Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
    //  //    EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //  //    //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //  }
    //  //}
    //  //catch (Exception Exception_Error)
    //  //{
    //  //  if (!String.IsNullOrEmpty(Exception_Error.ToString()) || String.IsNullOrEmpty(Exception_Error.ToString()))
    //  //  {
    //  //    String Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
    //  //    EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
    //  //    InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //  }
    //  //  else
    //  //  {
    //  //    throw;
    //  //  }
    //  //}
    //}

    //private void ECMClient_ECMUpdateMonthlyFormsAutomated()
    //{
    //  //Int32 EventID = 114;

    //  //try
    //  //{
    //  //  TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
    //  //  TimeSpan StartTime = new TimeSpan(7, 59, 0);
    //  //  TimeSpan EndTime = new TimeSpan(8, 9, 0);
    //  //  if (CurrentTime > StartTime && CurrentTime < EndTime)
    //  //  {
    //  //    using (Service_InfoQuest_ECMClient ServiceClient_ECMClient = new Service_InfoQuest_ECMClient())
    //  //    {
    //  //      FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("ECM_UpdateMonthlyForms_Automated");
    //  //      String SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
    //  //      String SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

    //  //      ServiceClient_ECMClient.Endpoint.Address = new EndpointAddress(new Uri("http://" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() + ":5008/Service_InfoQuest.svc"));
    //  //      String Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + ServiceClient_ECMClient.ECM_UpdateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
    //  //      EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //  //      //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //    }
    //  //  }
    //  //  else
    //  //  {
    //  //    String Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
    //  //    EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
    //  //    //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //  }
    //  //}
    //  //catch (Exception Exception_Error)
    //  //{
    //  //  if (!String.IsNullOrEmpty(Exception_Error.ToString()) || String.IsNullOrEmpty(Exception_Error.ToString()))
    //  //  {
    //  //    String Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
    //  //    EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
    //  //    InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
    //  //  }
    //  //  else
    //  //  {
    //  //    throw;
    //  //  }
    //  //}
    //}
    #endregion

    //--Administration
    private void AdministrationClient_AdministrationArchiveRecordsAutomated()
    {
      int EventID = 115;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("Administration_ArchiveRecords_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_Administration IServiceInfoQuestAdministration = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestAdministration.Administration_ArchiveRecords_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void AdministrationClient_AdministrationBeingModifiedUnlockAutomated()
    {
      int EventID = 116;

      try
      {
        FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("Administration_BeingModifiedUnlock_Automated");
        string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
        string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

        InfoQuestWCF.IService_InfoQuest_Administration IServiceInfoQuestAdministration = new InfoQuestWCF.Services_InfoQuest();
        string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestAdministration.Administration_BeingModifiedUnlock_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void AdministrationClient_AdministrationSecurityAccessCleanUpAutomated()
    {
      int EventID = 117;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("Administration_SecurityAccess_CleanUp_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_Administration IServiceInfoQuestAdministration = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestAdministration.Administration_SecurityAccess_CleanUp_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Monthly Sustainability Management
    private void SustainabilityManagementClient_SustainabilityManagementCreateMonthlyFormsAutomated()
    {
      int EventID = 119;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("SustainabilityManagement_CreateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_SustainabilityManagement IServiceInfoQuestSustainabilityManagement = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestSustainabilityManagement.SustainabilityManagement_CreateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void SustainabilityManagementClient_SustainabilityManagementUpdateMonthlyFormsAutomated()
    {
      int EventID = 120;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("SustainabilityManagement_UpdateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_SustainabilityManagement IServiceInfoQuestSustainabilityManagement = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestSustainabilityManagement.SustainabilityManagement_UpdateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void SustainabilityManagementClient_SustainabilityManagementMappingMissingAutomated()
    {
      int EventID = 121;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("SustainabilityManagement_MappingMissing_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_SustainabilityManagement IServiceInfoQuestSustainabilityManagement = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestSustainabilityManagement.SustainabilityManagement_MappingMissing_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Monthly Occupational Health Statistics
    private void MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsCreateMonthlyFormsAutomated()
    {
      int EventID = 122;

      try
      {
        int CurrentDay = DateTime.Now.Day;
        if (CurrentDay == 1)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyOccupationalHealthStatistics_CreateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyOccupationalHealthStatistics IServiceInfoQuestMonthlyOccupationalHealthStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyOccupationalHealthStatistics.MonthlyOccupationalHealthStatistics_CreateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes on the 1st of the Month;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void MonthlyOccupationalHealthStatisticsClient_MonthlyOccupationalHealthStatisticsUpdateMonthlyFormsAutomated()
    {
      int EventID = 123;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("MonthlyOccupationalHealthStatistics_UpdateMonthlyForms_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_MonthlyOccupationalHealthStatistics IServiceInfoQuestMonthlyOccupationalHealthStatistics = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestMonthlyOccupationalHealthStatistics.MonthlyOccupationalHealthStatistics_UpdateMonthlyForms_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Alert
    private void AlertClient_AlertPendingApprovalNotificationsAutomated()
    {
      int EventID = 124;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("Alert_PendingApprovalNotifications_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_Alert IServiceInfoQuestAlert = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestAlert.Alert_PendingApprovalNotifications_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //--Incident
    private void IncidentClient_IncidentPendingApprovalNotificationsAutomated()
    {
      int EventID = 125;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          FromDataBase_SecurityAccessWCF FromDataBase_SecurityAccessWCF_Current = GetSecurityAccessWCF("Incident_PendingApprovalNotifications_Automated");
          string SecurityAccessWCFUserName = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFUserName;
          string SecurityAccessWCFPassword = FromDataBase_SecurityAccessWCF_Current.SecurityAccessWCFPassword;

          InfoQuestWCF.IService_InfoQuest_Incident IServiceInfoQuestIncident = new InfoQuestWCF.Services_InfoQuest();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceInfoQuestIncident.Incident_PendingApprovalNotifications_Automated(SecurityAccessWCFUserName, SecurityAccessWCFPassword) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }


    //PXM
    private void PXMClient_PXMEventCheckGetEventData()
    {
      int EventID = 127;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(6, 59, 0);
        TimeSpan EndTime = new TimeSpan(23, 59, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          InfoQuestWCF.Services_MHS IServiceMHSPXM = new InfoQuestWCF.Services_MHS();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceMHSPXM.PXM_Event_CheckGetEventData() + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes between 06:59:00 and 23:59:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMReceivedFilesProcessData()
    {
      int EventID = 129;

      try
      {
        InfoQuestWCF.Services_MHS IServiceMHSPXM = new InfoQuestWCF.Services_MHS();
        string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceMHSPXM.PXM_ReceivedFiles_ProcessData() + ";", CultureInfo.CurrentCulture);
        EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
        //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMEventGetEventData()
    {
      int EventID = 126;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          DateTime CurrentDate = DateTime.Now;
          DateTime StartDate = CurrentDate.AddDays(-1);
          DateTime EndDate = CurrentDate.AddDays(-1);
          string FacilityId = "";

          InfoQuestWCF.Services_MHS IServiceMHSPXM = new InfoQuestWCF.Services_MHS();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceMHSPXM.PXM_Event_GetEventData(StartDate, EndDate, FacilityId) + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMReceivedFilesCheckReceiveData()
    {
      int EventID = 130;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(5, 59, 0);
        TimeSpan EndTime = new TimeSpan(6, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          InfoQuestWCF.Services_MHS IServiceMHSPXM = new InfoQuestWCF.Services_MHS();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceMHSPXM.PXM_ReceivedFiles_CheckReceiveData() + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 06:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }

    private void PXMClient_PXMEventSendEventData()
    {
      int EventID = 128;

      try
      {
        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        TimeSpan StartTime = new TimeSpan(7, 59, 0);
        TimeSpan EndTime = new TimeSpan(8, 9, 0);
        if (CurrentTime > StartTime && CurrentTime < EndTime)
        {
          InfoQuestWCF.Services_MHS IServiceMHSPXM = new InfoQuestWCF.Services_MHS();
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: " + IServiceMHSPXM.PXM_Event_SendEventData() + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method:  " + MethodBase.GetCurrentMethod().Name + "; Message: Service only executes at 08:00:00 daily;", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Information, EventID);
          //InfoQuest_WindowsService.WindowsService_SendEmail_Successful(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()) || string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          string Message = Convert.ToString("InfoQuest Windows Service; Method: " + MethodBase.GetCurrentMethod().Name + "; Message: " + Exception_Error.Message + ";", CultureInfo.CurrentCulture);
          EventLog_InfoQuest.WriteEntry(Message, EventLogEntryType.Error, EventID);
          InfoQuestWCF.InfoQuest_WindowsService.WindowsService_SendEmail_Error(MethodBase.GetCurrentMethod().Name, Message, EventID, EventIdDescription(EventID.ToString(CultureInfo.CurrentCulture)));
        }
        else
        {
          throw;
        }
      }
    }
    //---END--- --Function--//
  }
}
