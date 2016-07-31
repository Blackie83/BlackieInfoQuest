using System;
using System.IO;
using System.Web;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace InfoQuestWCF
{
  public static class InfoQuest_All
  {
    public static bool All_FileAccessible(string file)
    {
      bool isFileAccessible = true;

      try
      {
        using (new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
        {
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          isFileAccessible = false;
        }
        else
        {
          throw;
        }
      }

      return isFileAccessible;
    }

    public static string All_FixInputString(string inputValue)
    {
      if (string.IsNullOrEmpty(inputValue))
      {
        return string.Empty;
      }
      else
      {
        inputValue = inputValue.Trim();

        inputValue = inputValue.Replace("'", "");

        //inputValue = HttpContext.Current.Server.HtmlEncode(inputValue);
        inputValue = HttpUtility.HtmlEncode(inputValue);

        char[] InputStringChar = inputValue.ToCharArray();
        if (!string.IsNullOrEmpty(inputValue))
        {
          InputStringChar[0] = char.ToUpper(InputStringChar[0], CultureInfo.CurrentCulture);
        }

        return new string(InputStringChar);
      }
    }

    public static string All_FileInvalidColumns(DataTable dataTable_File, string preColumnName, string table)
    {
      string InvalidColumns = "";

      if (dataTable_File != null)
      {
        foreach (DataColumn DataColumn_ColumnNames in dataTable_File.Columns)
        {
          string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID(@object_id) AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
          using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
          {
            SqlCommand_Column.Parameters.AddWithValue("@name", preColumnName + DataColumn_ColumnNames.ColumnName.ToString());
            SqlCommand_Column.Parameters.AddWithValue("@object_id", table);
            DataTable DataTable_Column;
            using (DataTable_Column = new DataTable())
            {
              DataTable_Column.Locale = CultureInfo.CurrentCulture;
              DataTable_Column = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Column).Copy();
              if (DataTable_Column.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row_FacilityId in DataTable_Column.Rows)
                {
                  string name = DataRow_Row_FacilityId["name"].ToString();

                  if (string.IsNullOrEmpty(name))
                  {
                    InvalidColumns = InvalidColumns + DataColumn_ColumnNames.ColumnName.ToString() + "; ";
                  }
                }
              }
              else
              {
                InvalidColumns = InvalidColumns + DataColumn_ColumnNames.ColumnName.ToString() + "; ";
              }
            }
          }
        }
      }

      return InvalidColumns;
    }

    public static string All_SendSMS(string cellNumber, string message)
    {
      Int32 SMSId;

      using (ServiceReference_TxtPlus.SMSSoapClient SMSSoapClient = new ServiceReference_TxtPlus.SMSSoapClient())
      {
        SMSId = SMSSoapClient.SendSMS(cellNumber, message, 3, "outsystems_sa", "1", 0, DateTime.Now);
      }

      return "CellNumber: " + cellNumber + "; Message: " + message + "; SMSId: " + SMSId;
    }

    public static void All_Maintenance(string formId)
    {
      string FormMaintenance = "";
      string SQLStringForm = "SELECT Form_Maintenance FROM Administration_Form WHERE Form_Id = @Form_Id";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_Id", formId);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;

          DataTable_Form = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              FormMaintenance = DataRow_Row["Form_Maintenance"].ToString();
            }
          }
          else
          {
            FormMaintenance = "";
          }
        }
      }

      if (FormMaintenance == "True")
      {
        HttpContext.Current.Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=12", false);
        HttpContext.Current.Response.End();
      }
    }

    public static void All_View(string title, string link, string user)
    {
      if (link != null)
      {
        string SQLStringInsertPageView = "INSERT INTO Administration_PageView ( PageView_Page ,PageView_URL ,PageView_Description ,PageView_UserName ,PageView_Date ) VALUES ( @PageView_Page ,@PageView_URL ,@PageView_Description ,@PageView_UserName ,@PageView_Date )";
        using (SqlCommand SqlCommand_InsertPageView = new SqlCommand(SQLStringInsertPageView))
        {
          SqlCommand_InsertPageView.Parameters.AddWithValue("@PageView_Page", title);
          SqlCommand_InsertPageView.Parameters.AddWithValue("@PageView_URL", link);
          SqlCommand_InsertPageView.Parameters.AddWithValue("@PageView_Description", DBNull.Value);
          SqlCommand_InsertPageView.Parameters.AddWithValue("@PageView_UserName", user);
          SqlCommand_InsertPageView.Parameters.AddWithValue("@PageView_Date", DateTime.Now);
          InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPageView);
        }
      }
    }

    public static string All_ReportNumber(string securityUser_UserName, string facility_Id, string form_Id)
    {
      string ReportNumber = "";

      string SQLStringReportNumber = "EXECUTE spAdministration_Execute_ReportNumber @SecurityUser_UserName , @Facility_Id , @Form_Id ";
      using (SqlCommand SqlCommand_ReportNumber = new SqlCommand(SQLStringReportNumber))
      {
        SqlCommand_ReportNumber.Parameters.AddWithValue("@SecurityUser_UserName", securityUser_UserName);
        SqlCommand_ReportNumber.Parameters.AddWithValue("@Facility_Id", facility_Id);
        SqlCommand_ReportNumber.Parameters.AddWithValue("@Form_Id", form_Id);
        DataTable DataTable_ReportNumber;
        using (DataTable_ReportNumber = new DataTable())
        {
          DataTable_ReportNumber.Locale = CultureInfo.CurrentCulture;
          DataTable_ReportNumber = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ReportNumber).Copy();
          foreach (DataRow DataRow_Row in DataTable_ReportNumber.Rows)
          {
            ReportNumber = DataRow_Row["NewReportNumber"].ToString();
          }
        }
      }

      return ReportNumber;
    }

    public static string All_HistoryGet(string tableFrom, string tableWhere)
    {
      string History = "";

      string SQLStringHistory = "EXECUTE spAdministration_Execute_HistoryGet @TableFROM , @TableWHERE";
      using (SqlCommand SqlCommand_History = new SqlCommand(SQLStringHistory))
      {
        SqlCommand_History.Parameters.AddWithValue("@TableFROM", tableFrom);
        SqlCommand_History.Parameters.AddWithValue("@TableWHERE", tableWhere);
        DataTable DataTable_History;
        using (DataTable_History = new DataTable())
        {
          DataTable_History.Locale = CultureInfo.CurrentCulture;
          DataTable_History = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_History).Copy();
          if (DataTable_History.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_History.Rows)
            {
              History = DataRow_Row["History"].ToString();
            }
          }
        }
      }

      return History;
    }

    public static string All_RedirectLink(string page, string link)
    {
      return Convert.ToString("InfoQuest_PageLoading.htm?PageLoadingPage=" + page + "&PageLoadingURL=" + link + "", CultureInfo.CurrentCulture);
    }

    public static string All_UpdateProgress()
    {
      string PageUpdateProgress;

      PageUpdateProgress = "<div id=\"Div_UpdateProgress\" style=\"position: fixed; left: 35%; top: 40%; visibility: visible; vertical-align: middle; border-style: solid; border-color: #b0262e; border-width: 10px; background-color: #f7f7f7; width: 30%;\">" +
                           "   <table border=\"0\" style=\"width: 100%; border-color: #003768; border-style: solid; border-width: 10px; background-color: #ffffff;\">" +
                           "     <tr>" +
                           "       <td>&nbsp;</td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td style=\"color: #003768; font-size: 18px; font-weight: bold; vertical-align: middle; text-align: center;\">Loading Page</td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td>&nbsp;</td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td style=\"text-align: center;\">" +
                           "         <div id=\"LoaderDiv\" style=\"position: absolute; top: 50%; left: 50%\">" +
                           "           <script type=\"text/javascript\">Loader()</script>" +
                           "         </div>" +
                           "       </td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td>&nbsp;</td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td style=\"text-align: center;\">" +
                           "         <input id=\"Button_Stop\" class=\"Controls_Button\" type=\"submit\" value=\"Cancel Loading\" onclick=\"Sys.WebForms.PageRequestManager.getInstance().abortPostBack();\" />" +
                           "      </td>" +
                           "     </tr>" +
                           "     <tr>" +
                           "       <td>&nbsp;</td>" +
                           "     </tr>" +
                           "   </table>" +
                           "</div>";

      return PageUpdateProgress;
    }

    public static string All_FileVersion()
    {
      string FileVersion = DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture);

      return FileVersion;
    }

    public static string All_FormHeader(string project)
    {
      string HeaderString = "";
      string Server_Production = All_SystemServer("2").ToString();
      string Server_QA = All_SystemServer("5").ToString();
      string Server_Test = All_SystemServer("4").ToString();
      string Server_Development = All_SystemServer("3").ToString();
      string Server_Current = Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture);

      if ((!string.IsNullOrEmpty(Server_Production)) && (Server_Production == Server_Current))
      {
        if (project == "Administration")
        {
          HeaderString = Convert.ToString("<div style='width:100%; font-size:12pt; background-color:#77cf9c; padding:3px; color:White; font-weight:bold; text-align:center;'>PRODUCTION SERVER: " + Server_Current + "</div>", CultureInfo.CurrentCulture);
        }
        else if (project == "Form")
        {
          HeaderString = "";
        }
      }
      else if ((!string.IsNullOrEmpty(Server_QA)) && (Server_QA == Server_Current))
      {
        HeaderString = Convert.ToString("<div style='width:100%; font-size:12pt; background-color:#b0262e; padding:3px; color:White; font-weight:bold; text-align:center;'>QA SERVER: " + Server_Current + "</div>", CultureInfo.CurrentCulture);
      }
      else if ((!string.IsNullOrEmpty(Server_Test)) && (Server_Test == Server_Current))
      {
        HeaderString = Convert.ToString("<div style='width:100%; font-size:12pt; background-color:#b0262e; padding:3px; color:White; font-weight:bold; text-align:center;'>TESTING SERVER: " + Server_Current + "</div>", CultureInfo.CurrentCulture);
      }
      else if ((!string.IsNullOrEmpty(Server_Development)) && (Server_Development == Server_Current))
      {
        HeaderString = Convert.ToString("<div style='width:100%; font-size:12pt; background-color:#b0262e; padding:3px; color:White; font-weight:bold; text-align:center;'>DEVELOPMENT SERVER: " + Server_Current + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        HeaderString = Convert.ToString("<div style='width:100%; font-size:12pt; background-color:#FF9900; padding:3px; color:White; font-weight:bold; text-align:center;'>UNKNOWN SERVER: " + Server_Current + "</div>", CultureInfo.CurrentCulture);
      }

      return HeaderString;
    }

    public static string All_FormFooter()
    {
      string FooterString = Convert.ToString("<br /><br /><div style='text-align:center'><strong style='color: #003768; font-size: 10px;'>Powered by Info<span style='color: #b0262e;'>Q</span>uest</strong></div><br /><br />", CultureInfo.CurrentCulture);

      return FooterString;
    }

    public static string All_EmailHeader()
    {
      string HeaderString = "<table border='0' cellspacing='0' cellpadding='0' width='100%'> " +
                              "  <tr> " +
                              "    <td colspan='3' style='height: 10px'>&nbsp;</td> " +
                              "  </tr> " +
                              "  <tr> " +
                              "    <td style='width: 2%'>&nbsp;</td> " +
                              "    <td style='width: 96%'> " +
                              "      <table border='0' cellspacing='0' cellpadding='0' width='100%'> " +
                              "        <tr> " +
                              "          <td align='center'><strong style='font-family: Verdana; color: #003768; text-decoration: underline'>This is an e-mail sent from the Info<strong style='color: #b0262e'>Q</strong>uest system</strong></td> " +
                              "        </tr> " +
                              "        <tr> " +
                              "          <td align='center'><div style='font-family: Verdana; font-size: 10px; color: #003768'>Please do not respond to the sender as this mail box does not receive return mail.</div></td> " +
                              "        </tr> " +
                              "        <tr> " +
                              "          <td align='center'><div style='font-family: Verdana; font-size: 10px; color: #003768'>If you do have a problem please use the link at the bottom of the e-mail to reply.</div></td> " +
                              "        </tr> " +
                              "      </table> " +
                              "    </td> " +
                              "    <td style='width: 2%'>&nbsp;</td> " +
                              "  </tr> " +
                              "</table>";

      return HeaderString;
    }

    public static string All_EmailFooter()
    {
      string FooterString = "<table border='0' cellspacing='0' cellpadding='0' width='100%'> " +
                              "  <tr> " +
                              "    <td style='width: 2%'>&nbsp;</td> " +
                              "    <td style='width: 96%'> " +
                              "      <table border='0' cellspacing='0' cellpadding='0' width='100%'> " +
                              "        <tr> " +
                              "          <td align='center'><div style='font-family: Verdana; font-size: 10px; color: #008000'><strong>Think before you print this out. Do you really need a hard copy of this email?</strong></div></td> " +
                              "        </tr> " +
                              "      </table> " +
                              "    </td> " +
                              "    <td style='width: 2%'>&nbsp;</td> " +
                              "  </tr> " +
                              "  <tr> " +
                              "    <td colspan='3' style='height: 10px'>&nbsp;</td> " +
                              "  </tr> " +
                              "</table>";

      return FooterString;
    }

    public static string All_LinkAuthority()
    {
      string LinkAuthority = HttpContext.Current.Request.Url.Authority.ToString();
      string LinkApplicationPath = HttpContext.Current.Request.ApplicationPath.ToString();

      if (LinkApplicationPath != "/")
      {
        LinkAuthority = LinkAuthority + LinkApplicationPath;
      }

      string SystemServerServer = "";
      string SystemServerDNSAlias = "";
      string SQLStringSystemServer = "SELECT SystemServer_Server , SystemServer_DNS_Alias FROM Administration_SystemServer WHERE SystemServer_Server = @SystemServer_Server";
      using (SqlCommand SqlCommand_SystemServer = new SqlCommand(SQLStringSystemServer))
      {
        SqlCommand_SystemServer.Parameters.AddWithValue("@SystemServer_Server", Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture));
        DataTable DataTable_SystemServer;
        using (DataTable_SystemServer = new DataTable())
        {
          DataTable_SystemServer.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemServer = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemServer).Copy();
          if (DataTable_SystemServer.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemServer.Rows)
            {
              SystemServerServer = DataRow_Row["SystemServer_Server"].ToString();
              SystemServerDNSAlias = DataRow_Row["SystemServer_DNS_Alias"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(SystemServerServer) && !string.IsNullOrEmpty(SystemServerDNSAlias))
      {
        LinkAuthority = LinkAuthority.Replace(SystemServerServer, SystemServerDNSAlias);
      }

      return LinkAuthority;
    }

    public static string All_LinkHost()
    {
      string LinkHost = HttpContext.Current.Request.Url.Host.ToString();
      string LinkApplicationPath = HttpContext.Current.Request.ApplicationPath.ToString();

      if (LinkApplicationPath != "/")
      {
        LinkHost = LinkHost + LinkApplicationPath;
      }

      string SystemServerServer = "";
      string SystemServerDNSAlias = "";
      string SQLStringSystemServer = "SELECT SystemServer_Server , SystemServer_DNS_Alias FROM Administration_SystemServer WHERE SystemServer_Server = @SystemServer_Server";
      using (SqlCommand SqlCommand_SystemServer = new SqlCommand(SQLStringSystemServer))
      {
        SqlCommand_SystemServer.Parameters.AddWithValue("@SystemServer_Server", Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture));
        DataTable DataTable_SystemServer;
        using (DataTable_SystemServer = new DataTable())
        {
          DataTable_SystemServer.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemServer = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemServer).Copy();
          if (DataTable_SystemServer.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemServer.Rows)
            {
              SystemServerServer = DataRow_Row["SystemServer_Server"].ToString();
              SystemServerDNSAlias = DataRow_Row["SystemServer_DNS_Alias"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(SystemServerServer) && !string.IsNullOrEmpty(SystemServerDNSAlias))
      {
        LinkHost = LinkHost.Replace(SystemServerServer, SystemServerDNSAlias);
      }

      return LinkHost;
    }

    public static string All_SendEmail(string from, string to, string subject, string body)
    {
      string EmailSend = "No";
      if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(body))
      {
        from = from.Replace(";", ",");
        from = from.Replace(":", ",");

        to = to.Replace(";", ",");
        to = to.Replace(":", ",");

        string Server_Production = All_SystemServer("2").ToString();
        string Server_Current = Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture);

        if ((!string.IsNullOrEmpty(Server_Production)) && (Server_Production == Server_Current))
        {
          using (MailMessage MailMessage_Email = new MailMessage())
          {
            using (SmtpClient SmtpClient_SmtpServer = new SmtpClient(All_SystemServer("1").ToString()))
            {
              try
              {
                MailMessage_Email.From = new MailAddress("" + from + "");
                MailMessage_Email.To.Add("" + to + "");
                MailMessage_Email.Subject = "" + subject + "";
                MailMessage_Email.Body = "" + body + "";
                MailMessage_Email.IsBodyHtml = true;

                SmtpClient_SmtpServer.Send(MailMessage_Email);
                EmailSend = "Yes";

                string SQLStringInsertSendEmail = "INSERT INTO Administration_SendEmail ( SendEmail_From , SendEmail_To , SendEmail_Subject , SendEmail_Body , SendEmail_Date ) VALUES ( @SendEmail_From , @SendEmail_To , @SendEmail_Subject , @SendEmail_Body , @SendEmail_Date )";
                using (SqlCommand SqlCommand_InsertSendEmail = new SqlCommand(SQLStringInsertSendEmail))
                {
                  SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_From", from);
                  SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_To", to);
                  SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Subject", subject);
                  SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Body", body);
                  SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Date", DateTime.Now);
                  InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSendEmail);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  EmailSend = "No";
                  InfoQuest_Exceptions.Exceptions_NoRedirect(Exception_Error, "Form_SendEmail Class", HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, to);
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }
        else
        {
          EmailSend = "Yes";

          string SQLStringInsertSendEmail = "INSERT INTO Administration_SendEmail ( SendEmail_From , SendEmail_To , SendEmail_Subject , SendEmail_Body , SendEmail_Date ) VALUES ( @SendEmail_From , @SendEmail_To , @SendEmail_Subject , @SendEmail_Body , @SendEmail_Date )";
          using (SqlCommand SqlCommand_InsertSendEmail = new SqlCommand(SQLStringInsertSendEmail))
          {
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_From", from);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_To", to);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Subject", subject);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Body", body);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Date", DateTime.Now);
            InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSendEmail);
          }
        }
      }

      return EmailSend;
    }

    public static string All_SendEmail_NoSMTPServer(string from, string to, string subject, string body)
    {
      string EmailSend = "No";
      if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(body))
      {
        from = from.Replace(";", ",");
        from = from.Replace(":", ",");

        to = to.Replace(";", ",");
        to = to.Replace(":", ",");

        string Server_Production = All_SystemServer("2").ToString();
        string Server_Current = Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture);

        if ((!string.IsNullOrEmpty(Server_Production)) && (Server_Production == Server_Current))
        {
          try
          {
            ActiveUp.Net.Mail.Message Message_Email = new ActiveUp.Net.Mail.Message();
            Message_Email.From.Email = "" + from + "";
            Message_Email.To.Add("" + to + "");
            Message_Email.Subject = "" + subject + "";
            Message_Email.BodyText.Text = "" + body + "";
            Message_Email.IsHtml = true;

            ActiveUp.Net.Mail.SmtpClient.DirectSend(Message_Email);
            EmailSend = "Yes";

            string SQLStringInsertSendEmail = "INSERT INTO Administration_SendEmail ( SendEmail_From , SendEmail_To , SendEmail_Subject , SendEmail_Body , SendEmail_Date ) VALUES ( @SendEmail_From , @SendEmail_To , @SendEmail_Subject , @SendEmail_Body , @SendEmail_Date )";
            using (SqlCommand SqlCommand_InsertSendEmail = new SqlCommand(SQLStringInsertSendEmail))
            {
              SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_From", from);
              SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_To", to);
              SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Subject", subject);
              SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Body", body);
              SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Date", DateTime.Now);
              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSendEmail);
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              EmailSend = "No";
              InfoQuest_Exceptions.Exceptions_NoRedirect(Exception_Error, "Form_SendEmail_NoSMTPServer Class", HttpContext.Current.Request.Url.AbsoluteUri, System.Security.Principal.WindowsIdentity.GetCurrent().Name, to);
            }
            else
            {
              throw;
            }
          }
        }
        else
        {
          EmailSend = "Yes";

          string SQLStringInsertSendEmail = "INSERT INTO Administration_SendEmail ( SendEmail_From , SendEmail_To , SendEmail_Subject , SendEmail_Body , SendEmail_Date ) VALUES ( @SendEmail_From , @SendEmail_To , @SendEmail_Subject , @SendEmail_Body , @SendEmail_Date )";
          using (SqlCommand SqlCommand_InsertSendEmail = new SqlCommand(SQLStringInsertSendEmail))
          {
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_From", from);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_To", to);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Subject", subject);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Body", body);
            SqlCommand_InsertSendEmail.Parameters.AddWithValue("@SendEmail_Date", DateTime.Now);
            InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSendEmail);
          }
        }
      }

      return EmailSend;
    }

    public static string All_FormName(string formId)
    {
      string FormName = "";
      string SQLStringForm = "SELECT Form_Name FROM Administration_Form WHERE Form_Id = @Form_Id";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_Id", formId);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;
          DataTable_Form = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              FormName = DataRow_Row["Form_Name"].ToString();
            }
          }
        }
      }

      return FormName;
    }

    public static string All_SystemServer(string systemServerId)
    {
      string SystemServerServer = "";
      string SQLStringSystemServer = "SELECT SystemServer_Server FROM Administration_SystemServer WHERE SystemServer_Id = @SystemServer_Id";
      using (SqlCommand SqlCommand_SystemServer = new SqlCommand(SQLStringSystemServer))
      {
        SqlCommand_SystemServer.Parameters.AddWithValue("@SystemServer_Id", systemServerId);
        DataTable DataTable_SystemServer;
        using (DataTable_SystemServer = new DataTable())
        {
          DataTable_SystemServer.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemServer = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemServer).Copy();
          if (DataTable_SystemServer.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemServer.Rows)
            {
              SystemServerServer = DataRow_Row["SystemServer_Server"].ToString().ToLower(CultureInfo.CurrentCulture);
            }
          }
        }
      }

      return SystemServerServer;
    }

    public static string All_SystemPageText(string systemPageTextId)
    {
      string SystemPageTextText = "";
      string SQLStringSystemPageText = "SELECT SystemPageText_Text FROM Administration_SystemPageText WHERE SystemPageText_Id = @SystemPageText_Id";
      using (SqlCommand SqlCommand_SystemPageText = new SqlCommand(SQLStringSystemPageText))
      {
        SqlCommand_SystemPageText.Parameters.AddWithValue("@SystemPageText_Id", systemPageTextId);
        DataTable DataTable_SystemPageText;
        using (DataTable_SystemPageText = new DataTable())
        {
          DataTable_SystemPageText.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemPageText = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemPageText).Copy();
          if (DataTable_SystemPageText.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemPageText.Rows)
            {
              SystemPageTextText = DataRow_Row["SystemPageText_Text"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(SystemPageTextText))
      {
        SystemPageTextText = SystemPageTextText.Replace(";replace;URLAuthority;replace;", "" + All_LinkAuthority() + "");
        SystemPageTextText = SystemPageTextText.Replace(";replace;URLHost;replace;", "" + All_LinkHost() + "");
      }

      return SystemPageTextText;
    }

    public static string All_SystemEmailTemplate(string systemEmailTemplateId)
    {
      string SystemEmailTemplateTemplate = "";
      string SQLStringSystemEmailTemplate = "SELECT SystemEmailTemplate_Template FROM Administration_SystemEmailTemplate WHERE SystemEmailTemplate_Id = @SystemEmailTemplate_Id";
      using (SqlCommand SqlCommand_SystemEmailTemplate = new SqlCommand(SQLStringSystemEmailTemplate))
      {
        SqlCommand_SystemEmailTemplate.Parameters.AddWithValue("@SystemEmailTemplate_Id", systemEmailTemplateId);
        DataTable DataTable_SystemEmailTemplate;
        using (DataTable_SystemEmailTemplate = new DataTable())
        {
          DataTable_SystemEmailTemplate.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemEmailTemplate = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemEmailTemplate).Copy();
          if (DataTable_SystemEmailTemplate.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemEmailTemplate.Rows)
            {
              SystemEmailTemplateTemplate = DataRow_Row["SystemEmailTemplate_Template"].ToString();
            }
          }
        }
      }

      if (System.Reflection.Assembly.GetEntryAssembly() == null)
      {
        if (!string.IsNullOrEmpty(SystemEmailTemplateTemplate))
        {
          SystemEmailTemplateTemplate = SystemEmailTemplateTemplate.Replace(";replace;URLAuthority;replace;", "" + All_LinkAuthority() + "");
          SystemEmailTemplateTemplate = SystemEmailTemplateTemplate.Replace(";replace;URLHost;replace;", "" + All_LinkHost() + "");
        }
      }
      else
      {
        if (System.Reflection.Assembly.GetEntryAssembly().GetName().Name.ToString() != "InfoQuestWindowsService")
        {
          if (!string.IsNullOrEmpty(SystemEmailTemplateTemplate))
          {
            SystemEmailTemplateTemplate = SystemEmailTemplateTemplate.Replace(";replace;URLAuthority;replace;", "" + All_LinkAuthority() + "");
            SystemEmailTemplateTemplate = SystemEmailTemplateTemplate.Replace(";replace;URLHost;replace;", "" + All_LinkHost() + "");
          }
        }
      }

      return SystemEmailTemplateTemplate;
    }

    public static string All_NavigationDescription(string navigationId)
    {
      string NavigationDescription = "";
      string SQLStringNavigation = "SELECT Navigation_Description FROM Administration_Navigation WHERE Navigation_Id = @Navigation_Id";
      using (SqlCommand SqlCommand_Navigation = new SqlCommand(SQLStringNavigation))
      {
        SqlCommand_Navigation.Parameters.AddWithValue("@Navigation_Id", navigationId);
        DataTable DataTable_Navigation;
        using (DataTable_Navigation = new DataTable())
        {
          DataTable_Navigation.Locale = CultureInfo.CurrentCulture;
          DataTable_Navigation = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Navigation).Copy();
          if (DataTable_Navigation.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Navigation.Rows)
            {
              NavigationDescription = DataRow_Row["Navigation_Description"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(NavigationDescription))
      {
        NavigationDescription = NavigationDescription.Replace(";replace;URLAuthority;replace;", "" + All_LinkAuthority() + "");
        NavigationDescription = NavigationDescription.Replace(";replace;URLHost;replace;", "" + All_LinkHost() + "");
      }

      return NavigationDescription;
    }


    public static DateTime All_ValidateDate(string dateToValidate)
    {
      DateTime ValidatedDate;
      bool ValidDate;

      ValidDate = DateTime.TryParseExact(dateToValidate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ValidatedDate);

      if (ValidDate == true)
      {
        return ValidatedDate;
      }
      else
      {
        return ValidatedDate;
      }
    }

    public static DateTime All_ValidateDateTime(string dateTimeToValidate)
    {
      DateTime ValidatedDateTime;
      bool ValidDateTime;

      ValidDateTime = DateTime.TryParseExact(dateTimeToValidate, "yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out ValidatedDateTime);

      if (ValidDateTime == true)
      {
        return ValidatedDateTime;
      }
      else
      {
        return ValidatedDateTime;
      }
    }

    public static decimal All_ValidateDecimal(string decimalToValidate)
    {
      decimal ValidatedDecimal;
      bool ValidDecimal;

      if (string.IsNullOrEmpty(decimalToValidate) || decimalToValidate == "0" || decimalToValidate == "0.00")
      {
        ValidDecimal = true;
        ValidatedDecimal = -1;
      }
      else
      {
        ValidDecimal = decimal.TryParse(decimalToValidate, out ValidatedDecimal);
      }

      if (ValidDecimal == true)
      {
        return ValidatedDecimal;
      }
      else
      {
        return ValidatedDecimal;
      }
    }
  }
}
