using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_Alert
  {
    [OperationContract]
    string Alert_PendingApprovalNotifications_Automated(string userName, string password);
  }
}
