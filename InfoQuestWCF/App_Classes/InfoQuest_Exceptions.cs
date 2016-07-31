using System;
using System.Web;
using System.Data.SqlClient;

namespace InfoQuestWCF
{
  public static class InfoQuest_Exceptions
  {
    public static void Exceptions(Exception exception_Error, string title, string link, string user, string extraInformation)
    {
      if (exception_Error != null && link != null)
      {
        string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_ExtraInformation ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_ExtraInformation ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
        using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
        {
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", title);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", link);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", exception_Error.Message.ToString());
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", exception_Error.StackTrace);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ExtraInformation", extraInformation);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
          InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
        }

        HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=4", false);
      }
    }

    public static void Exceptions_NoRedirect(Exception exception_Error, string title, string link, string user, string extraInformation)
    {
      if (exception_Error != null && link != null)
      {
        string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_ExtraInformation ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_ExtraInformation ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
        using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
        {
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", title);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", link);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", exception_Error.Message.ToString());
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", exception_Error.StackTrace.ToString());
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ExtraInformation", extraInformation);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
          InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
        }
      }
    }

    public static void Exceptions_OwnMessage(string exceptions_ErrorMessage, string exceptions_StackTrace, string title, string link, string user, string extraInformation)
    {
      if (exceptions_ErrorMessage != null && link != null)
      {
        string SQLStringInsertExceptions = "INSERT INTO Administration_Exceptions ( Exceptions_Page ,Exceptions_URL ,Exceptions_ErrorMessage ,Exceptions_StackTrace ,Exceptions_ExtraInformation ,Exceptions_UserName ,Exceptions_Date ,Exceptions_Description ,Exceptions_Completed, Exceptions_CompletedDate ,Exceptions_ModifiedDate ,Exceptions_ModifiedBy ,Exceptions_History ) VALUES ( @Exceptions_Page ,@Exceptions_URL ,@Exceptions_ErrorMessage ,@Exceptions_StackTrace ,@Exceptions_ExtraInformation ,@Exceptions_UserName ,@Exceptions_Date ,@Exceptions_Description ,@Exceptions_Completed, @Exceptions_CompletedDate ,@Exceptions_ModifiedDate ,@Exceptions_ModifiedBy ,@Exceptions_History )";
        using (SqlCommand SqlCommand_InsertExceptions = new SqlCommand(SQLStringInsertExceptions))
        {
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Page", title);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_URL", link);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ErrorMessage", exceptions_ErrorMessage);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_StackTrace", exceptions_StackTrace);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ExtraInformation", extraInformation);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_UserName", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Date", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Description", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_Completed", false);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_CompletedDate", DBNull.Value);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedDate", DateTime.Now);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_ModifiedBy", user);
          SqlCommand_InsertExceptions.Parameters.AddWithValue("@Exceptions_History", DBNull.Value);
          InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertExceptions);
        }
      }
    }
  }
}