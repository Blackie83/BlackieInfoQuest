using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace InfoQuestWCF
{
  [ServiceContract(Namespace = "http://lifehealthcare.co.za/InfoQuest")]
  public interface IService_MHS_PXM
  {
    //Event
    //[OperationContract]
    //string PXM_Event_GetEventData(DateTime startDate, DateTime endDate, string facilityId);

    //[OperationContract]
    //string PXM_Event_CheckGetEventData();

    //[OperationContract]
    //string PXM_Event_SendEventData();


    //[OperationContract]
    //PXM_ExternalResponse TestEventReceiveEventResponse();

    [OperationContract]
    PXM_ExternalResponse PXM_Event_ReceiveEventResponse(PXM_EventResponse eventResponse);


    //ReceivedFiles
    //[OperationContract]
    //PXM_ExternalResponse TestEscalationFile();

    [OperationContract]
    PXM_ExternalResponse PXM_ReceivedFiles_Escalation_ReceiveData(PXM_EscalationFile escalationFile);


    //[OperationContract]
    //Services_MHS.PXM_ExternalResponse TestReportFile();

    [OperationContract]
    PXM_ExternalResponse PXM_ReceivedFiles_Report_ReceiveData(PXM_ReportFile reportFile);


    //[OperationContract]
    //Services_MHS.PXM_ExternalResponse TestTouchPointFile();

    [OperationContract]
    PXM_ExternalResponse PXM_ReceivedFiles_TouchPoint_ReceiveData(PXM_TouchPointFile touchPointFile);


    //[OperationContract]
    //Services_MHS.PXM_ExternalResponse TestDoctorQuestionsFile();

    [OperationContract]
    PXM_ExternalResponse PXM_ReceivedFiles_DoctorQuestions_ReceiveData(PXM_DoctorQuestionsFile doctorQuestionsFile);


    //[OperationContract]
    //string PXM_ReceivedFiles_ProcessData();

    //[OperationContract]
    //string PXM_ReceivedFiles_CheckReceiveData();
  }


  //--BEGIN-- --ExternalResponse--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "ExternalResponse")]
  public class PXM_ExternalResponse
  {
    [DataMember(Order = 0, IsRequired = true, Name = "ExternalResponse_Body")]
    public PXM_ExternalResponse_Body_ExternalResponse Body { get; set; }
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "ExternalResponse_Body")]
  public class PXM_ExternalResponse_Body_ExternalResponse
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string FileNumber { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public bool IsSuccessful { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string ErrorMessage { get; set; }
  }


  //--BEGIN-- --EventResponse--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "EventResponse")]
  public class PXM_EventResponse
  {
    [DataMember(Order = 0, IsRequired = true, Name = "EventResponse_Body")]
    public List<PXM_EventResponse_Body_EventResponse> Body;
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "EventResponse_Body")]
  public class PXM_EventResponse_Body_EventResponse
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string HospitalCode { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string VisitNumber { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public bool IsSuccessful { get; set; }
  }


  //--BEGIN-- --Escalation--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "EscalationFile")]
  public class PXM_EscalationFile
  {
    [DataMember(Order = 0, IsRequired = true, Name = "EscalationFile_Header")]
    public PXM_EscalationFile_Header Header { get; set; }

    [DataMember(Order = 1, IsRequired = true, Name = "EscalationFile_Body")]
    public List<PXM_EscalationFile_Body_Escalation> Body;
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "EscalationFile_Header")]
  public class PXM_EscalationFile_Header
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string To { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string From { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Payload { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string EscalationFileNumber { get; set; }
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "EscalationFile_Body_Escalation")]
  public class PXM_EscalationFile_Body_Escalation
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string Value { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string ValueLabel { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Comment { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string CreatedDate { get; set; }

    [DataMember(Order = 4, IsRequired = true)]
    public string SurveyKey { get; set; }

    [DataMember(Order = 5, IsRequired = true)]
    public string Label { get; set; }

    [DataMember(Order = 6, IsRequired = true)]
    public string Text { get; set; }

    [DataMember(Order = 7, IsRequired = true)]
    public string HospitalCode { get; set; }

    [DataMember(Order = 8, IsRequired = true)]
    public string Hospital { get; set; }

    [DataMember(Order = 9, IsRequired = true)]
    public string PreferredChannel { get; set; }

    [DataMember(Order = 10, IsRequired = true)]
    public string EmailAddress { get; set; }

    [DataMember(Order = 11, IsRequired = true)]
    public string PatientMobileNumber { get; set; }

    [DataMember(Order = 12, IsRequired = true)]
    public string PatientFirstname { get; set; }

    [DataMember(Order = 13, IsRequired = true)]
    public string PatientSurname { get; set; }

    [DataMember(Order = 14, IsRequired = true)]
    public string PatientTitle { get; set; }

    [DataMember(Order = 15, IsRequired = true)]
    public string AdmissionDate { get; set; }

    [DataMember(Order = 16, IsRequired = true)]
    public string DischargeWard { get; set; }

    [DataMember(Order = 17, IsRequired = true)]
    public string EmergencyContactPersonEmail { get; set; }

    [DataMember(Order = 18, IsRequired = true)]
    public string EmergencyContactPersonFirstname { get; set; }

    [DataMember(Order = 19, IsRequired = true)]
    public string EmergencyContactPersonMobileNumber { get; set; }

    [DataMember(Order = 20, IsRequired = true)]
    public string EmergencyContactPersonSurname { get; set; }

    [DataMember(Order = 21, IsRequired = true)]
    public string EventId { get; set; }

    [DataMember(Order = 22, IsRequired = true)]
    public string VisitNumber { get; set; }
  }


  //--BEGIN-- --Report--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "ReportFile")]
  public class PXM_ReportFile
  {
    [DataMember(Order = 0, IsRequired = true, Name = "ReportFile_Header")]
    public PXM_ReportFile_Header Header { get; set; }

    [DataMember(Order = 1, IsRequired = true, Name = "ReportFile_Body")]
    public List<PXM_ReportFile_Body_Report> Body;
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "ReportFile_Header")]
  public class PXM_ReportFile_Header
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string To { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string From { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Payload { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string ReportFileNumber { get; set; }
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "ReportFile_Body_Report")]
  public class PXM_ReportFile_Body_Report
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string Hospital { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string HospitalCode { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string VisitNumber { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string SurveyStatus { get; set; }

    [DataMember(Order = 4, IsRequired = true)]
    public string ReportCompleteDate { get; set; }

    [DataMember(Order = 5, IsRequired = true)]
    public string LoyaltyValue { get; set; }

    [DataMember(Order = 6, IsRequired = true)]
    public string PatientExperienceValue { get; set; }

    [DataMember(Order = 7, IsRequired = true)]
    public string PatientExperienceScore { get; set; }

    [DataMember(Order = 8, IsRequired = true)]
    public string SurveyType { get; set; }
  }


  //--BEGIN-- --TouchPoint--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "TouchPointFile")]
  public class PXM_TouchPointFile
  {
    [DataMember(Order = 0, IsRequired = true, Name = "TouchPointFile_Header")]
    public PXM_TouchPointFile_Header Header { get; set; }

    [DataMember(Order = 1, IsRequired = true, Name = "TouchPointFile_Body")]
    public List<PXM_TouchPointFile_Body_TouchPoint> Body;
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "TouchPointFile_Header")]
  public class PXM_TouchPointFile_Header
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string To { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string From { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Payload { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string TouchPointFileNumber { get; set; }
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "TouchPointFile_Body_TouchPoint")]
  public class PXM_TouchPointFile_Body_TouchPoint
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string Hospital { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string HospitalCode { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Date { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string TouchPoint_Doctor { get; set; }

    [DataMember(Order = 4, IsRequired = true)]
    public string TouchPoint_Facilities { get; set; }

    [DataMember(Order = 5, IsRequired = true)]
    public string TouchPoint_Food { get; set; }

    [DataMember(Order = 6, IsRequired = true)]
    public string TouchPoint_Medication { get; set; }

    [DataMember(Order = 7, IsRequired = true)]
    public string TouchPoint_Nursing { get; set; }

    [DataMember(Order = 8, IsRequired = true)]
    public string TouchPoint_ReceptionStaff { get; set; }

    [DataMember(Order = 9, IsRequired = true)]
    public string TouchPoint_Responsiveness { get; set; }
  }


  //--BEGIN-- --DoctorQuestions--
  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "DoctorQuestionsFile")]
  public class PXM_DoctorQuestionsFile
  {
    [DataMember(Order = 0, IsRequired = true, Name = "DoctorQuestionsFile_Header")]
    public PXM_DoctorQuestionsFile_Header Header { get; set; }

    [DataMember(Order = 1, IsRequired = true, Name = "DoctorQuestionsFile_Body")]
    public List<PXM_DoctorQuestionsFile_Body_DoctorQuestions> Body;
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "DoctorQuestionsFile_Header")]
  public class PXM_DoctorQuestionsFile_Header
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string To { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string From { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Payload { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string DoctorQuestionsFileNumber { get; set; }
  }

  [DataContract(Namespace = "http://lifehealthcare.co.za/InfoQuest", Name = "DoctorQuestionsFile_Body_DoctorQuestions")]
  public class PXM_DoctorQuestionsFile_Body_DoctorQuestions
  {
    [DataMember(Order = 0, IsRequired = true)]
    public string VisitNumber { get; set; }

    [DataMember(Order = 1, IsRequired = true)]
    public string Question { get; set; }

    [DataMember(Order = 2, IsRequired = true)]
    public string Value { get; set; }

    [DataMember(Order = 3, IsRequired = true)]
    public string ValueLabel { get; set; }

    [DataMember(Order = 4, IsRequired = true)]
    public string Score { get; set; }

    [DataMember(Order = 5, IsRequired = true)]
    public string Hospital { get; set; }

    [DataMember(Order = 6, IsRequired = true)]
    public string HospitalCode { get; set; }
  }

}
