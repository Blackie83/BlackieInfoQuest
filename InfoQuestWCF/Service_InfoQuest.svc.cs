
namespace InfoQuestWCF
{
  public partial class Services_InfoQuest : IService_InfoQuest
  {
    public string InfoQuest_Welcome(string name)
    {
      return "Welcome " + name + " to InfoQuest WCF";
    }
  }
}
