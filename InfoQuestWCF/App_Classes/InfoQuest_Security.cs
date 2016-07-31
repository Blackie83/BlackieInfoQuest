using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestWCF
{
  public static class InfoQuest_Security
  {
    public static string Security_Form_Administration(string userName)
    {
      //--START-- --PageSecurityAdministration-- --//
      string SecurityAllow = "0";

      string SecurityUser = "";
      if (string.IsNullOrEmpty(userName))
      {
        SecurityUser = "";
      }
      else
      {
        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = @SecurityRole_Id)";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", userName);
          SqlCommand_Security.Parameters.AddWithValue("@SecurityRole_Id", 1);
          DataTable DataTable_Security;
          using (DataTable_Security = new DataTable())
          {
            DataTable_Security.Locale = CultureInfo.CurrentCulture;

            DataTable_Security = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Security).Copy();
            if (DataTable_Security.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Security.Rows)
              {
                SecurityUser = DataRow_Row["SecurityUser_UserName"].ToString();
              }
            }
            else
            {
              SecurityUser = "";
            }
          }
        }
      }


      if (string.IsNullOrEmpty(SecurityUser))
      {
        SecurityAllow = "0";
      }
      else
      {
        SecurityAllow = "1";
      }

      SecurityUser = "";

      return SecurityAllow;
      //---END--- --PageSecurityAdministration-- --//
    }

    public static string Security_Form_User(SqlCommand sqlCommand_SqlString)
    {
      //--START-- --PageSecurityForm-- --//
      string SecurityAllow = "0";

      string SecurityUser = "";
      DataTable DataTable_Security;
      using (DataTable_Security = new DataTable())
      {
        DataTable_Security.Locale = CultureInfo.CurrentCulture;

        DataTable_Security = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(sqlCommand_SqlString).Copy();
        if (DataTable_Security.Rows.Count > 0)
        {
          foreach (DataRow DataRow_Row in DataTable_Security.Rows)
          {
            SecurityUser = DataRow_Row["SecurityUser_UserName"].ToString();
          }
        }
        else
        {
          SecurityUser = "";
        }
      }

      if (string.IsNullOrEmpty(SecurityUser))
      {
        SecurityAllow = "0";
      }
      else
      {
        SecurityAllow = "1";
      }

      SecurityUser = "";

      return SecurityAllow;
      //---END--- --PageSecurityForm-- --//
    }

    public static bool Security_WCF(string method, string userName, string password)
    {
      bool AccessValid = false;

      if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
      {
        AccessValid = false;
      }
      else
      {
        string SecurityAccessWCFId = "";
        string SQLStringSecurity = "SELECT SecurityAccess_WCF_Id FROM Administration_SecurityAccess_WCF WHERE SecurityAccess_WCF_Method = @SecurityAccess_WCF_Method AND SecurityAccess_WCF_UserName = @SecurityAccess_WCF_UserName AND SecurityAccess_WCF_Password = @SecurityAccess_WCF_Password AND SecurityAccess_WCF_IsActive = 1";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityAccess_WCF_Method", method);
          SqlCommand_Security.Parameters.AddWithValue("@SecurityAccess_WCF_UserName", userName);
          SqlCommand_Security.Parameters.AddWithValue("@SecurityAccess_WCF_Password", password);
          DataTable DataTable_Security;
          using (DataTable_Security = new DataTable())
          {
            DataTable_Security.Locale = CultureInfo.CurrentCulture;

            DataTable_Security = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Security).Copy();
            if (DataTable_Security.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Security.Rows)
              {
                SecurityAccessWCFId = DataRow_Row["SecurityAccess_WCF_Id"].ToString();

                if (string.IsNullOrEmpty(SecurityAccessWCFId))
                {
                  AccessValid = false;
                }
                else
                {
                  AccessValid = true;
                }
              }
            }
            else
            {
              AccessValid = false;
            }
          }
        }
      }

      return AccessValid;
    }
  }
}