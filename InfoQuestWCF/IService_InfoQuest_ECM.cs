using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_ECM
  {
    [OperationContract]
    string ECM_CreateMonthlyForms_Automated(string userName, string password);

    [OperationContract]
    string ECM_UpdateMonthlyForms_Automated(string userName, string password);
  }
}
