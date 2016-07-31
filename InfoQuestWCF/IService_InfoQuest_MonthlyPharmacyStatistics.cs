using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_MonthlyPharmacyStatistics
  {
    [OperationContract]
    string MonthlyPharmacyStatistics_CreateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string MonthlyPharmacyStatistics_UpdateMonthlyForms_Automated(string userName, string password);
  }
}
