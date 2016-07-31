using System.Configuration;

namespace InfoQuestWCF
{
  public static class InfoQuest_Connections
  {
    public static string Connections(string sqlConnection)
    {
      string ConnectionString = "";
      if (sqlConnection == "InfoQuest")
      {
        //ConnectionString = ConfigurationManager.ConnectionStrings["InfoQuestConnectionString"].ConnectionString;
        ConnectionString = ConfigurationManager.ConnectionStrings["InfoQuestMOSSConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "PatientDetailEDW")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["PatientDetailEDWConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "PatientDetailEDWStaging")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["PatientDetailEDWStagingConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "PatientDetailODS")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["PatientDetailODSConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "PatientDetailFacilityStructure")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["PatientDetailFacilityStructureConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "PatientDetailMedoc")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["PatientDetailMedocConnectionString"].ConnectionString;
      }
      else if (sqlConnection == "EmployeeDetailVision")
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDetailVisionConnectionString"].ConnectionString;
      }
      else
      {
        ConnectionString = "";
      }

      return ConnectionString;
    }
  }
}