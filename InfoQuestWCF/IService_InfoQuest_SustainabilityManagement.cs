using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_SustainabilityManagement
  {
    [OperationContract]
    string SustainabilityManagement_CreateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string SustainabilityManagement_UpdateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string SustainabilityManagement_MappingMissing_Automated(string userName, string password);
  }
}
