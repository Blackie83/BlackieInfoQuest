using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoQuestWCF;
using System;

namespace InfoQuestWCF.Tests
{
  [TestClass()]
  public class InfoQuest_ConnectionsTests
  {
    [TestMethod()]
    public void ConnectionsTest()
    {
      string Connection = InfoQuest_Connections.Connections("InfoQuest");
      Assert.AreEqual(@"Data Source=DSQLOTH01\OTH_INS;Initial Catalog=InfoQuestMOSS;Persist Security Info=True;User ID=InfoQuestUser;Password=1nf0qu3st", Connection);
    }
  }
}