using System.ServiceModel;

namespace InfoQuestWCF
{
  [ServiceContract]
  public interface IService_MHS
  {
    [OperationContract]
    string MHS_Welcome(string name);
  }
}
