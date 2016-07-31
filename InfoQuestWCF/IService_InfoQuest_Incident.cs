using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_Incident
  {
    [OperationContract]
    string Incident_PendingApprovalNotifications_Automated(string userName, string password);
  }
}
