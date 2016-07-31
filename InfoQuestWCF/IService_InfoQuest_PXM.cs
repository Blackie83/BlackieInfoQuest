using System;
using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_PXM
  {
    [OperationContract]
    string PXM_PDCH_Event_CreateFile_Manual(string userName, string password, DateTime startDate, DateTime endDate, string facilityId);

    [OperationContract]
    string PXM_PDCH_Event_CreateFile_Automated(string userName, string password);

    [OperationContract]
    string PXM_PDCH_Event_CreateFile_AutomatedMissing(string userName, string password);


    [OperationContract]
    string PXM_PDCH_Escalation_ExportData_Automated(string userName, string password);

    [OperationContract]
    string PXM_PDCH_Escalation_CheckFileProcessing(string userName, string password);

    [OperationContract]
    string PXM_PDCH_Escalation_CheckSurveyProcessing(string userName, string password);


    [OperationContract]
    string PXM_PDCH_Report_ExportData_Automated(string userName, string password);

    [OperationContract]
    string PXM_PDCH_Report_CheckFileProcessing(string userName, string password);
  }
}
