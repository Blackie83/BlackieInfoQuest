using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_InfoQuest
  {
    [OperationContract]
    string InfoQuest_Welcome(string name);
  }
}