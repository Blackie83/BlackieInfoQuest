
namespace InfoQuestWCF
{
  public partial class Services_MHS : IService_MHS
  {
    public string MHS_Welcome(string name)
    {
      return "Welcome " + name + " to InfoQuest MHS WCF";
    }
  }
}
