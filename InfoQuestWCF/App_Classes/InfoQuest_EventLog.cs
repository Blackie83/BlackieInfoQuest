using System.Diagnostics;

namespace InfoQuestWCF
{
  public static class InfoQuest_EventLog
  {
    public static void EventLog_CreateEventLog(string source, string logName)
    {
      using (EventLog EventLog_InfoQuest = new EventLog())
      {
        if (!EventLog.SourceExists(source))
        {
          EventLog.CreateEventSource(source, logName);
        }
      }
    }
  }
}
