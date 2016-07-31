using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_MonthlyOccupationalHealthStatistics
  {
    [OperationContract]
    string MonthlyOccupationalHealthStatistics_CreateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string MonthlyOccupationalHealthStatistics_UpdateMonthlyForms_Automated(string userName, string password);
  }
}
