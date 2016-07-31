using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest_Administration
  {
    [OperationContract]
    string Administration_ArchiveRecords_Automated(string userName, string password);

    [OperationContract]
    string Administration_BeingModifiedUnlock_Automated(string userName, string password);

    [OperationContract]
    string Administration_SecurityAccess_CleanUp_Automated(string userName, string password);
  }
}
