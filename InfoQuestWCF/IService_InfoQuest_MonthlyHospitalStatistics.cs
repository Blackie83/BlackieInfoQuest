using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_MonthlyHospitalStatistics
  {
    [OperationContract]
    string MonthlyHospitalStatistics_CreateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string MonthlyHospitalStatistics_UpdateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string MonthlyHospitalStatistics_Organisms_InsertOrganisms_Automated(string userName, string password);

    [OperationContract]
    string MonthlyHospitalStatistics_Organisms_UpdateOrganisms_Automated(string userName, string password);
  }
}
