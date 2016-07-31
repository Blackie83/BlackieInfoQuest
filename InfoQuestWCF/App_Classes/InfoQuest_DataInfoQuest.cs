using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Diagnostics;

namespace InfoQuestWCF
{
  public static class InfoQuest_DataInfoQuest
  {
    public static DataTable DataInfoQuest_SqlGetData(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("InfoQuest");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  try
                  {
                    string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                    using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                    {
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetData Class");
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                    }

                    HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                  }
                  catch (Exception ex)
                  {
                    if (!string.IsNullOrEmpty(ex.ToString()))
                    {
                      HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                    }
                    else
                    {
                      throw;
                    }
                  }
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static void DataInfoQuest_SqlExecute(SqlCommand sqlCommand_SqlString)
    {
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              sqlCommand_SqlString.ExecuteNonQuery();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlExecute Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }
    }

    public static string DataInfoQuest_SqlGetLastId(SqlCommand sqlCommand_SqlString)
    {
      string LastId = "";
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              LastId = sqlCommand_SqlString.ExecuteScalar().ToString();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetLastId Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      return LastId;
    }


    public static DataTable DataInfoQuest_SqlGetData_WCF(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("InfoQuest");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
          DataTable_GetData.Columns.Add("Error", typeof(string));
          DataTable_GetData.Rows.Add("Error: No InfoQuest Connection String");

          InfoQuest_Exceptions.Exceptions_OwnMessage("Error: No InfoQuest Connection String", "", System.Reflection.MethodBase.GetCurrentMethod().Name, HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  DataTable_GetData.Reset();
                  DataTable_GetData.Columns.Add("Error", typeof(string));
                  DataTable_GetData.Rows.Add("Error: Data could not be retrieved from InfoQuest");

                  InfoQuest_Exceptions.Exceptions_OwnMessage("Error: Data could not be retrieved from InfoQuest; Exception Message: " + Exception_Error.Message.ToString() + ";", Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static DataTable DataInfoQuest_SqlExecute_WCF(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (string.IsNullOrEmpty(ConnectionStringExecute))
        {
          DataTable_GetData.Reset();
          DataTable_GetData.Columns.Add("Error", typeof(string));
          DataTable_GetData.Rows.Add("Error: No InfoQuest Connection String");

          InfoQuest_Exceptions.Exceptions_OwnMessage("Error: No InfoQuest Connection String", "", System.Reflection.MethodBase.GetCurrentMethod().Name, HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
        }
        else
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              if (sqlCommand_SqlString != null)
              {
                foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                {
                  if (SqlParameter_Value.Value == null)
                  {
                    SqlParameter_Value.Value = DBNull.Value;
                  }
                }

                sqlCommand_SqlString.CommandType = CommandType.Text;
                sqlCommand_SqlString.Connection = SQLConnection_Execute;
                sqlCommand_SqlString.CommandTimeout = 600;
                SQLConnection_Execute.Open();
                sqlCommand_SqlString.ExecuteNonQuery();

                DataTable_GetData.Reset();
              }
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                DataTable_GetData.Reset();
                DataTable_GetData.Columns.Add("Error", typeof(string));
                DataTable_GetData.Rows.Add("Error: Data could not be retrieved from InfoQuest");

                InfoQuest_Exceptions.Exceptions_OwnMessage("Error: Data could not be retrieved from InfoQuest; Exception Message: " + Exception_Error.Message.ToString() + ";", Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static string DataInfoQuest_SqlGetLastId_WCF(SqlCommand sqlCommand_SqlString)
    {
      string LastId = "";
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              LastId = sqlCommand_SqlString.ExecuteScalar().ToString();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                LastId = "Error: Data could not be retrieved from InfoQuest";

                InfoQuest_Exceptions.Exceptions_OwnMessage("Error: Data could not be retrieved from InfoQuest; Exception Message: " + Exception_Error.Message.ToString() + ";", Exception_Error.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "");
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      return LastId;
    }


    private static EventLog EventLog_InfoQuest = new EventLog();

    public static DataTable DataInfoQuest_SqlGetData_WindowsService(SqlCommand sqlCommand_SqlString)
    {
      InfoQuest_EventLog.EventLog_CreateEventLog("InfoQuest_Source", "InfoQuest_WS_Log");
      EventLog_InfoQuest.Source = "InfoQuest_Source";
      EventLog_InfoQuest.Log = "InfoQuest_WS_Log";

      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("InfoQuest");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  try
                  {
                    string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                    using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                    {
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetData_WindowsService Class");
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", "InfoQuest Windows Service");
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                    }

                    EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                  }
                  catch (Exception ex)
                  {
                    if (!string.IsNullOrEmpty(ex.ToString()))
                    {
                      EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                    }
                    else
                    {
                      throw;
                    }
                  }
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static void DataInfoQuest_SqlExecute_WindowsService(SqlCommand sqlCommand_SqlString)
    {
      InfoQuest_EventLog.EventLog_CreateEventLog("InfoQuest_Source", "InfoQuest_WS_Log");
      EventLog_InfoQuest.Source = "InfoQuest_Source";
      EventLog_InfoQuest.Log = "InfoQuest_WS_Log";

      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              sqlCommand_SqlString.ExecuteNonQuery();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlExecute_WindowsService Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", "InfoQuest Windows Service");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }
    }

    public static string DataInfoQuest_SqlGetLastId_WindowsService(SqlCommand sqlCommand_SqlString)
    {
      InfoQuest_EventLog.EventLog_CreateEventLog("InfoQuest_Source", "InfoQuest_WS_Log");
      EventLog_InfoQuest.Source = "InfoQuest_Source";
      EventLog_InfoQuest.Log = "InfoQuest_WS_Log";

      string LastId = "";
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections("InfoQuest");

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              LastId = sqlCommand_SqlString.ExecuteScalar().ToString();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetLastId_WindowsService Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", "InfoQuest Windows Service");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    EventLog_InfoQuest.WriteEntry(Convert.ToString("InfoQuest Windows Service; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: Exception;", CultureInfo.CurrentCulture), EventLogEntryType.Error, 99);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      return LastId;
    }


    public static DataTable DataInfoQuest_SqlGetData_Other(SqlCommand sqlCommand_SqlString, string connection)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections(connection);

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  try
                  {
                    string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                    using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                    {
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetData_Other Class");
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                      SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                      InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                    }

                    HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                  }
                  catch (Exception ex)
                  {
                    if (!string.IsNullOrEmpty(ex.ToString()))
                    {
                      HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                    }
                    else
                    {
                      throw;
                    }
                  }
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static void DataInfoQuest_SqlExecute_Other(SqlCommand sqlCommand_SqlString, string connection)
    {
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections(connection);

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              sqlCommand_SqlString.ExecuteNonQuery();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlExecute_Other Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }
    }

    public static string DataInfoQuest_SqlGetLastId_Other(SqlCommand sqlCommand_SqlString, string connection)
    {
      string LastId = "";
      if (sqlCommand_SqlString != null)
      {
        string ConnectionStringExecute = InfoQuest_Connections.Connections(connection);

        if (!string.IsNullOrEmpty(ConnectionStringExecute))
        {
          using (SqlConnection SQLConnection_Execute = new SqlConnection(ConnectionStringExecute))
          {
            try
            {
              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
              {
                if (SqlParameter_Value.Value == null)
                {
                  SqlParameter_Value.Value = DBNull.Value;
                }
              }

              sqlCommand_SqlString.CommandType = CommandType.Text;
              sqlCommand_SqlString.Connection = SQLConnection_Execute;
              sqlCommand_SqlString.CommandTimeout = 600;
              SQLConnection_Execute.Open();
              LastId = sqlCommand_SqlString.ExecuteScalar().ToString();
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                try
                {
                  string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
                  using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
                  {
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", "DataInfoQuest_SqlGetLastId_Other Class");
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", HttpContext.Current.Request.Url.AbsoluteUri);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", Exception_Error.Message.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", Exception_Error.StackTrace.ToString());
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
                    InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
                  }

                  HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", true);
                }
                catch (Exception ex)
                {
                  if (!string.IsNullOrEmpty(ex.ToString()))
                  {
                    HttpContext.Current.Response.Redirect("InfoQuest_Error.aspx", true);
                  }
                  else
                  {
                    throw;
                  }
                }
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      return LastId;
    }
  }
}