using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CRM_DiscoveryComment_Upload : InfoQuestWCF.Override_SystemWebUIPage
  {
    string EmailFacility = "";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Discovery Comment Upload", CultureInfo.CurrentCulture);
          Label_UploadHeading.Text = Convert.ToString("Upload " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " Discovery Comment Files", CultureInfo.CurrentCulture);
          Label_UploadedHeading.Text = Convert.ToString("List of Uploaded " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " Discovery Comment Files", CultureInfo.CurrentCulture);

          Session["FolderSecurity"] = "1";
          try
          {
            System.Security.AccessControl.FileSecurity fs = System.IO.File.GetAccessControl(UploadPath());
            System.Security.AccessControl.AuthorizationRuleCollection arc = fs.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

            foreach (System.Security.AccessControl.FileSystemAccessRule fsar in arc)
            {
              string IdentityReference = fsar.IdentityReference.Value.ToLower(CultureInfo.CurrentCulture).Replace(" ", "");
              string AcceptControlType = fsar.AccessControlType.ToString().ToLower(CultureInfo.CurrentCulture).Replace(" ", "");
              string FileSystemRights = fsar.FileSystemRights.ToString().ToLower(CultureInfo.CurrentCulture).Replace(" ", "");

              if (Session["FolderSecurity"].ToString() == "1" && IdentityReference == "everyone" && AcceptControlType == "allow" && FileSystemRights == "modify,synchronize")
              {
                Session["FolderSecurity"] = "0";

                TableUpload.Visible = true;
                TableUploaded.Visible = true;

                UploadedFiles();

                DirectoryCleanUp();
              }
              else if (Session["FolderSecurity"].ToString() == "1")
              {
                TableUpload.Visible = false;
                TableUploaded.Visible = false;
              }
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              Session["FolderSecurity"] = "1";
            }
            else
            {
              throw;
            }
          }
          finally
          {
            if (Session["FolderSecurity"].ToString() == "1")
            {
              Label_Title.Text = Convert.ToString("Administrator need to setup folder security", CultureInfo.CurrentCulture);
            }
          }

          Session["FolderSecurity"] = "1";
        }
      }
    }

    protected string PageSecurity()
    {
      string SecurityAllow = "0";

      string SecurityAllowAdministration = "0";

      SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(Request.ServerVariables["LOGON_USER"]);

      if (SecurityAllowAdministration == "1")
      {
        SecurityAllow = "1";
      }
      else
      {
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('146'))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
          Response.End();
        }
      }

      return SecurityAllow;
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("36");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CRM_DiscoveryComment_Upload.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Customer Relationship Management", "4");
      }
    }


    protected void RegisterPostBackControl()
    {
      ScriptManager ScriptManager_Upload = ScriptManager.GetCurrent(Page);

      ScriptManager_Upload.RegisterPostBackControl(Button_Upload);
      ScriptManager_Upload.RegisterPostBackControl(Button_Delete);
      ScriptManager_Upload.RegisterPostBackControl(Button_DeleteAll);
      ScriptManager_Upload.RegisterPostBackControl(Button_Extract);
    }

    private string UploadPath()
    {
      string UploadPath = Server.MapPath("App_Files/Form_CRM_DiscoveryComment_Upload/");

      return UploadPath;
    }

    private string UploadUserFolder()
    {
      string UserFolder = Request.ServerVariables["LOGON_USER"];
      UserFolder = UserFolder.Substring(UserFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
      UserFolder = UserFolder.ToLower(CultureInfo.CurrentCulture);

      return UserFolder;
    }

    private void DirectoryCleanUp()
    {
      if (Directory.Exists(UploadPath() + UploadUserFolder()))
      {
        string[] UploadedFiles = Directory.GetFiles(UploadPath() + UploadUserFolder(), "*.*", SearchOption.AllDirectories);

        if (UploadedFiles.Length == 0)
        {
          Directory.Delete(UploadPath() + UploadUserFolder(), true);
        }
      }
    }

    private void UploadedFiles()
    {
      RegisterPostBackControl();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','36') AND SecurityUser_Username = @SecurityUser_Username";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");

            Session["Security"] = "1";
            Session["ShowHideButtons"] = "1";

            if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
            {
              Session["Security"] = "0";

              try
              {
                if (Directory.Exists(UploadPath()))
                {
                  Session["ShowHideButtons"] = "1";

                  string[] UploadedFiles = Directory.GetFiles(UploadPath(), "*.*", SearchOption.AllDirectories);

                  if (UploadedFiles.Length > 0)
                  {
                    CheckBoxList_UploadedFiles.Items.Clear();
                    Label_TotalFiles.Text = "" + UploadedFiles.Length + "";
                    Label_UploadedFiles.Text = "";

                    foreach (string Files in Directory.GetFiles(UploadPath(), "*.*", SearchOption.AllDirectories))
                    {
                      string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
                      if (UploadedFiles.Length == 1 && FileName == "placeholder.txt")
                      {
                        Session["ShowHideButtons"] = "0";

                        CheckBoxList_UploadedFiles.Items.Clear();
                        Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                        Label_UploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
                      }
                      else if (UploadedFiles.Length > 1 && FileName == "placeholder.txt")
                      {
                        Int32 TotalFiles = UploadedFiles.Length;
                        TotalFiles = TotalFiles - 1;
                        Label_TotalFiles.Text = TotalFiles.ToString(CultureInfo.CurrentCulture);
                      }
                      else
                      {
                        string FileFolder = Files.Replace("\\" + FileName + "", "");
                        FileFolder = FileFolder.Substring(FileFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
                        CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_CRM_DiscoveryComment_Upload\\" + FileFolder + "\\" + FileName + "' target='_blank'>" + FileFolder + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), FileFolder + "\\" + FileName));
                      }
                    }
                  }
                  else
                  {
                    Session["ShowHideButtons"] = "0";

                    CheckBoxList_UploadedFiles.Items.Clear();
                    Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                    Label_UploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
                  }
                }
                else
                {
                  Session["ShowHideButtons"] = "0";

                  CheckBoxList_UploadedFiles.Items.Clear();
                  Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                  Label_UploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  Session["ShowHideButtons"] = "0";
                  CheckBoxList_UploadedFiles.Items.Clear();
                  Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                  Label_UploadedFiles.Text = Convert.ToString("Error Accessing Folders and Files", CultureInfo.CurrentCulture);
                }
                else
                {
                  throw;
                }
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";

              try
              {
                if (Directory.Exists(UploadPath() + UploadUserFolder()))
                {
                  Session["ShowHideButtons"] = "1";

                  string[] UploadedFiles = Directory.GetFiles(UploadPath() + UploadUserFolder(), "*.*", SearchOption.AllDirectories);

                  if (UploadedFiles.Length > 0)
                  {
                    CheckBoxList_UploadedFiles.Items.Clear();
                    Label_TotalFiles.Text = "" + UploadedFiles.Length + "";
                    Label_UploadedFiles.Text = "";

                    foreach (string Files in Directory.GetFiles(UploadPath() + UploadUserFolder(), "*.*", SearchOption.AllDirectories))
                    {
                      string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
                      CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_CRM_DiscoveryComment_Upload\\" + UploadUserFolder() + "\\" + FileName + "' target='_blank'>" + UploadUserFolder() + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), UploadUserFolder() + "\\" + FileName));
                    }
                  }
                  else
                  {
                    Session["ShowHideButtons"] = "0";

                    CheckBoxList_UploadedFiles.Items.Clear();
                    Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                    Label_UploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
                  }
                }
                else
                {
                  Session["ShowHideButtons"] = "0";

                  CheckBoxList_UploadedFiles.Items.Clear();
                  Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                  Label_UploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  Session["ShowHideButtons"] = "0";
                  CheckBoxList_UploadedFiles.Items.Clear();
                  Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                  Label_UploadedFiles.Text = Convert.ToString("Error Accessing Folders and Files", CultureInfo.CurrentCulture);
                }
                else
                {
                  throw;
                }
              }
            }

            if (Session["ShowHideButtons"].ToString() == "1")
            {
              Button_Extract.Visible = true;
              Button_Delete.Visible = true;
              Button_DeleteAll.Visible = true;
            }
            else if (Session["ShowHideButtons"].ToString() == "0")
            {
              Button_Extract.Visible = false;
              Button_Delete.Visible = false;
              Button_DeleteAll.Visible = false;
            }

            Session["Security"] = "1";
            Session["ShowHideButtons"] = "1";
          }
        }
      }
    }

    private void ShowHide(string ButtonClicked)
    {
      if (ButtonClicked == "Upload")
      {
        Label_Delete.Text = "";
        Label_Extract.Text = "";
      }
      else if (ButtonClicked == "Extract")
      {
        Label_Upload.Text = "";
        Label_Delete.Text = "";
      }
      else if (ButtonClicked == "Delete")
      {
        Label_Upload.Text = "";
        Label_Extract.Text = "";
      }
      else
      {
        Label_Upload.Text = "";
        Label_Delete.Text = "";
        Label_Extract.Text = "";
      }
    }

    private string FixInputString(string InputString)
    {
      if (string.IsNullOrEmpty(InputString))
      {
        return string.Empty;
      }
      else
      {
        InputString = InputString.Trim();

        InputString = InputString.Replace("'", "");

        Server.HtmlEncode(InputString);

        Char[] InputStringChar = InputString.ToCharArray();
        if (!string.IsNullOrEmpty(InputString))
        {
          InputStringChar[0] = Char.ToUpper(InputStringChar[0], CultureInfo.CurrentCulture);
        }

        return new string(InputStringChar);
      }
    }


    protected void Button_Upload_Click(object sender, EventArgs e)
    {
      string UploadMessage = "";

      if (FileUpload_Upload.HasFiles)
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_Upload.PostedFiles)
        {
          string FileName = Path.GetFileName(HttpPostedFile_File.FileName);
          string FileExtension = FileName.Substring(FileName.LastIndexOf('.') + 1);
          decimal FileSize = HttpPostedFile_File.ContentLength;
          decimal FileSizeMB = (FileSize / 1024 / 1024);
          string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

          if ((FileExtension == "xls" || FileExtension == "xlsx") && (FileSize < 5242880))
          {
            try
            {
              if (!Directory.Exists(UploadPath() + UploadUserFolder()))
              {
                Directory.CreateDirectory(UploadPath() + UploadUserFolder());
              }

              if (File.Exists(UploadPath() + UploadUserFolder() + "\\" + FileName))
              {
                UploadMessage = "<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>";
              }
              else
              {
                HttpPostedFile_File.SaveAs(UploadPath() + UploadUserFolder() + "\\" + FileName);

                UploadMessage = "<span style='color:#77cf9c;'>File Uploading Successful</span>";
              }
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                UploadMessage = "<span style='color:#d46e6e;'>File Uploading Failed</span>";
              }
              else
              {
                throw;
              }
            }
          }
          else
          {
            if (FileExtension != "xls" && FileExtension != "xlsx")
            {
              UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only xls and xlsx files can be uploaded<br/>File Name: " + FileName + "</span>";
            }

            if (FileSize > 5242880)
            {
              UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>";
            }
          }
        }
      }
      else
      {
        UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>";
      }

      Label_Upload.Text = Convert.ToString(UploadMessage, CultureInfo.CurrentCulture);

      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Show('Label_Upload');</script>", true);
      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressUpload');</script>", true);

      ShowHide("Upload");
      UploadedFiles();
    }

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < CheckBoxList_UploadedFiles.Items.Count; i++)
      {
        if (CheckBoxList_UploadedFiles.Items[i].Selected)
        {
          string UploadedFilesCheckBoxListValues = CheckBoxList_UploadedFiles.Items[i].Value;

          if (!string.IsNullOrEmpty(UploadedFilesCheckBoxListValues))
          {
            try
            {
              if (Directory.Exists(UploadPath()))
              {
                if (File.Exists(UploadPath() + UploadedFilesCheckBoxListValues))
                {
                  File.Delete(UploadPath() + UploadedFilesCheckBoxListValues);
                  DirectoryCleanUp();

                  DeleteMessage = DeleteMessage + "<span style='color:#77cf9c;'>File Deletion Successful<br/>File Name: " + UploadedFilesCheckBoxListValues + "</span><br/><br/>";
                }
              }
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                DeleteMessage = DeleteMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + UploadedFilesCheckBoxListValues + "</span><br/><br/>";
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      Label_Delete.Text = Convert.ToString(DeleteMessage, CultureInfo.CurrentCulture);

      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>", true);
      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>", true);

      ShowHide("Delete");
      UploadedFiles();
    }

    protected void Button_DeleteAll_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < CheckBoxList_UploadedFiles.Items.Count; i++)
      {
        string UploadedFilesCheckBoxListValues = CheckBoxList_UploadedFiles.Items[i].Value;

        if (!string.IsNullOrEmpty(UploadedFilesCheckBoxListValues))
        {
          try
          {
            if (Directory.Exists(UploadPath()))
            {
              if (File.Exists(UploadPath() + UploadedFilesCheckBoxListValues))
              {
                File.Delete(UploadPath() + UploadedFilesCheckBoxListValues);
                DirectoryCleanUp();

                DeleteMessage = DeleteMessage + "<span style='color:#77cf9c;'>File Deletion Successful<br/>File Name: " + UploadedFilesCheckBoxListValues + "</span><br/><br/>";
              }
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              DeleteMessage = DeleteMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + UploadedFilesCheckBoxListValues + "</span><br/><br/>";
            }
            else
            {
              throw;
            }
          }
        }
      }

      Label_Delete.Text = Convert.ToString(DeleteMessage, CultureInfo.CurrentCulture);

      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>", true);
      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>", true);

      ShowHide("Delete");
      UploadedFiles();
    }

    protected void Button_Extract_Click(object sender, EventArgs e)
    {
      string ExtractMessage = "";

      for (int i = 0; i < CheckBoxList_UploadedFiles.Items.Count; i++)
      {
        if (CheckBoxList_UploadedFiles.Items[i].Selected)
        {
          string UploadedFilesCheckBoxListValues = CheckBoxList_UploadedFiles.Items[i].Value;
          string FileName = UploadedFilesCheckBoxListValues.Substring(UploadedFilesCheckBoxListValues.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
          string FileExtension = FileName.Substring(FileName.LastIndexOf('.') + 1);

          if (!string.IsNullOrEmpty(UploadedFilesCheckBoxListValues))
          {
            if (Directory.Exists(UploadPath()))
            {
              if (FileExtension == "xlsx" || FileExtension == "xls")
              {
                string ConnectionExtract = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + UploadPath() + UploadedFilesCheckBoxListValues + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                Session["DeleteFile"] = "1";

                ExtractMessage = ExtractMessage + ExtractData_WithMemberDetails(FileName, ConnectionExtract, ExtractMessage);

                ExtractMessage = ExtractMessage + ExtractData_NoMemberDetails(FileName, ConnectionExtract, ExtractMessage);

                if (string.IsNullOrEmpty(ExtractMessage))
                {
                  ExtractMessage = ExtractMessage + "<span style='color:#77cf9c;'>File Importing Successful<br/>File Name: " + FileName + "<br/><a href='Form_CRM_IncompleteOther.aspx?s_CRM_TypeList=4412' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
                }
                else
                {
                  Session["DeleteFile"] = "0";
                }

                try
                {
                  if (Session["DeleteFile"].ToString() == "1")
                  {
                    File.Delete(UploadPath() + UploadedFilesCheckBoxListValues);
                    DirectoryCleanUp();
                  }
                }
                catch (Exception Exception_Error)
                {
                  if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                  {
                    ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + FileName + "</span><br/><br/>";
                  }
                  else
                  {
                    throw;
                  }
                }

                Session["DeleteFile"] = "";
              }
            }
          }
        }
      }

      Label_Extract.Text = Convert.ToString(ExtractMessage, CultureInfo.CurrentCulture);

      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Show('Label_Extract');</script>", true);
      //ScriptManager.RegisterStartupScript(UpdatePanel_CRM_DiscoveryComment_Upload, this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressExtract');</script>", true);

      ShowHide("Extract");
      UploadedFiles();
    }

    protected string ExtractData_WithMemberDetails(string fileName, string connectionExtract, string extractMessage)
    {
      string ExtractMessage = extractMessage;

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractWithMemberDetails = new OleDbDataAdapter("SELECT F1 , F2 , F3 , F4 , F5 , F6 FROM [With member details$]", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractWithMemberDetails = new DataTable())
            {
              DataTable_ExtractWithMemberDetails.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractWithMemberDetails.Clear();

              using (DataColumn DataColumn_ExtractWithMemberDetails = new DataColumn())
              {
                DataColumn_ExtractWithMemberDetails.DataType = System.Type.GetType("System.Int32");
                DataColumn_ExtractWithMemberDetails.AutoIncrement = true;
                DataColumn_ExtractWithMemberDetails.AutoIncrementSeed = 1;
                DataColumn_ExtractWithMemberDetails.AutoIncrementStep = 1;
                DataColumn_ExtractWithMemberDetails.ColumnName = "Id";

                DataTable_ExtractWithMemberDetails.Columns.Add(DataColumn_ExtractWithMemberDetails);
              }
              DataTable_ExtractWithMemberDetails.Columns.Add("F1");
              DataTable_ExtractWithMemberDetails.Columns.Add("F2");
              DataTable_ExtractWithMemberDetails.Columns.Add("F3");
              DataTable_ExtractWithMemberDetails.Columns.Add("F4");
              DataTable_ExtractWithMemberDetails.Columns.Add("F5");
              DataTable_ExtractWithMemberDetails.Columns.Add("F6");

              OleDbDataAdapter_ExtractWithMemberDetails.Fill(DataTable_ExtractWithMemberDetails);

              DataRow[] DataTable_ExtractWithMemberDetails_Rows = DataTable_ExtractWithMemberDetails.Select("F3 <> 'Facility Number' AND F3 <> 'NA' AND F3 <> '?'", "F3 , Id");

              foreach (DataRow DataRow_Row in DataTable_ExtractWithMemberDetails_Rows)
              {
                string CRMId = "";
                string SQLStringCRMId = "SELECT CRM_Id FROM Form_CRM WHERE CRM_UploadedFrom = 'Discovery Survey' AND CRM_UploadedFromReferenceNumber = @CRM_UploadedFromReferenceNumber";
                using (SqlCommand SqlCommand_CRMId = new SqlCommand(SQLStringCRMId))
                {
                  SqlCommand_CRMId.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", FixInputString("" + DataRow_Row["F5"] + ""));
                  DataTable DataTable_CRMId;
                  using (DataTable_CRMId = new DataTable())
                  {
                    DataTable_CRMId.Locale = CultureInfo.CurrentCulture;
                    DataTable_CRMId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMId).Copy();
                    if (DataTable_CRMId.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row_FacilityId in DataTable_CRMId.Rows)
                      {
                        CRMId = DataRow_Row_FacilityId["CRM_Id"].ToString();
                      }
                    }
                  }
                }

                if (string.IsNullOrEmpty(CRMId))
                {
                  string FacilityId = ExtractData_GetFacilityId(FixInputString("" + DataRow_Row["F3"] + ""));

                  if (!string.IsNullOrEmpty(FacilityId))
                  {
                    string CRM_ReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], FacilityId, "36");

                    string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                    string LastCRMId = "";
                    using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4426); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Discovery Survey");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", FixInputString("" + DataRow_Row["F5"] + ""));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", FixInputString("" + DataRow_Row["F2"] + ""));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", FixInputString("" + DataRow_Row["F1"] + ""));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", DBNull.Value); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", DBNull.Value); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Discovery Survey: Event Number: " + FixInputString("" + DataRow_Row["F5"] + "") + " : " + FixInputString("" + DataRow_Row["F6"] + ""));
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved"); 
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived" , 0);
                      LastCRMId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                    }

                    if (!string.IsNullOrEmpty(LastCRMId))
                    {
                      if (EmailFacility != FacilityId)
                      {
                        EmailFacility = FacilityId;
                        ExtractData_SendEmail(LastCRMId, "Discovery Survey: With Member Details");
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for With Member Details<br/>File Name: " + fileName + "<br/>File is in the wrong format<br/>" + Exception_Error.ToString() + "</span><br/><br/>";
        }
        else
        {
          throw;
        }
      }

      return ExtractMessage;
    }

    protected string ExtractData_NoMemberDetails(string fileName, string connectionExtract, string extractMessage)
    {
      string ExtractMessage = extractMessage;

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractNoMemberDetails = new OleDbDataAdapter("SELECT F1 , F2 , F3 FROM [No member details$]", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractNoMemberDetails = new DataTable())
            {
              DataTable_ExtractNoMemberDetails.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractNoMemberDetails.Clear();

              using (DataColumn DataColumn_ExtractNoMemberDetails = new DataColumn())
              {
                DataColumn_ExtractNoMemberDetails.DataType = System.Type.GetType("System.Int32");
                DataColumn_ExtractNoMemberDetails.AutoIncrement = true;
                DataColumn_ExtractNoMemberDetails.AutoIncrementSeed = 1;
                DataColumn_ExtractNoMemberDetails.AutoIncrementStep = 1;
                DataColumn_ExtractNoMemberDetails.ColumnName = "Id";

                DataTable_ExtractNoMemberDetails.Columns.Add(DataColumn_ExtractNoMemberDetails);
              }
              DataTable_ExtractNoMemberDetails.Columns.Add("F1");
              DataTable_ExtractNoMemberDetails.Columns.Add("F2");
              DataTable_ExtractNoMemberDetails.Columns.Add("F3");

              OleDbDataAdapter_ExtractNoMemberDetails.Fill(DataTable_ExtractNoMemberDetails);

              DataRow[] DataTable_ExtractNoMemberDetails_Rows = DataTable_ExtractNoMemberDetails.Select("F1 <> 'Facility Number' AND F1 <> 'NA' AND F1 <> '?'", "F1 , Id");

              foreach (DataRow DataRow_Row in DataTable_ExtractNoMemberDetails_Rows)
              {
                string FacilityId = ExtractData_GetFacilityId(FixInputString("" + DataRow_Row["F1"] + ""));

                if (!string.IsNullOrEmpty(FacilityId))
                {
                  string CRMId = "";
                  string SQLStringCRMId = "SELECT CRM_Id FROM Form_CRM WHERE CRM_UploadedFrom = 'Discovery Survey' AND CRM_Comment_Description = @CRM_Comment_Description AND Facility_Id = @Facility_Id";
                  using (SqlCommand SqlCommand_CRMId = new SqlCommand(SQLStringCRMId))
                  {
                    SqlCommand_CRMId.Parameters.AddWithValue("@CRM_Comment_Description", "Discovery Survey: " + FixInputString("" + DataRow_Row["F3"] + "") + "");
                    SqlCommand_CRMId.Parameters.AddWithValue("@Facility_Id", FacilityId);
                    DataTable DataTable_CRMId;
                    using (DataTable_CRMId = new DataTable())
                    {
                      DataTable_CRMId.Locale = CultureInfo.CurrentCulture;
                      DataTable_CRMId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMId).Copy();
                      if (DataTable_CRMId.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row_FacilityId in DataTable_CRMId.Rows)
                        {
                          CRMId = DataRow_Row_FacilityId["CRM_Id"].ToString();
                        }
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(CRMId))
                  {
                    string CRM_ReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], FacilityId, "36");

                    string SQLStringInsertCRMComment = "INSERT INTO Form_CRM ( Facility_Id , CRM_ReportNumber , CRM_DateReceived , CRM_DateForwarded , CRM_OriginatedAt_List , CRM_Type_List , CRM_ReceivedVia_List , CRM_ReceivedFrom_List , CRM_EscalatedForm , CRM_UploadedFrom , CRM_UploadedFromReferenceNumber , CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber , CRM_PatientName , CRM_PatientDateOfAdmission , CRM_PatientEmail , CRM_PatientContactNumber , CRM_Complaint_CloseOut , CRM_Compliment_Acknowledge , CRM_Compliment_CloseOut , CRM_Query_Acknowledge , CRM_Query_CloseOut , CRM_Suggestion_Acknowledge , CRM_Suggestion_CloseOut , CRM_Comment_Description , CRM_Comment_Unit_Id , CRM_Comment_Type_List , CRM_Comment_Acknowledge , CRM_Comment_AcknowledgeDate , CRM_Comment_AcknowledgeBy , CRM_Comment_CloseOut , CRM_Comment_CloseOutDate , CRM_Comment_CloseOutBy , CRM_Status , CRM_StatusDate , CRM_StatusRejectedReason , CRM_CreatedDate , CRM_CreatedBy , CRM_ModifiedDate , CRM_ModifiedBy , CRM_History , CRM_Archived ) VALUES ( @Facility_Id , @CRM_ReportNumber , @CRM_DateReceived , @CRM_DateForwarded , @CRM_OriginatedAt_List , @CRM_Type_List , @CRM_ReceivedVia_List , @CRM_ReceivedFrom_List , @CRM_EscalatedForm , @CRM_UploadedFrom , @CRM_UploadedFromReferenceNumber , @CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber , @CRM_PatientName , @CRM_PatientDateOfAdmission , @CRM_PatientEmail , @CRM_PatientContactNumber , @CRM_Complaint_CloseOut , @CRM_Compliment_Acknowledge , @CRM_Compliment_CloseOut , @CRM_Query_Acknowledge , @CRM_Query_CloseOut , @CRM_Suggestion_Acknowledge , @CRM_Suggestion_CloseOut , @CRM_Comment_Description , @CRM_Comment_Unit_Id , @CRM_Comment_Type_List , @CRM_Comment_Acknowledge , @CRM_Comment_AcknowledgeDate , @CRM_Comment_AcknowledgeBy , @CRM_Comment_CloseOut , @CRM_Comment_CloseOutDate , @CRM_Comment_CloseOutBy , @CRM_Status , @CRM_StatusDate , @CRM_StatusRejectedReason , @CRM_CreatedDate , @CRM_CreatedBy , @CRM_ModifiedDate , @CRM_ModifiedBy , @CRM_History , @CRM_Archived ); SELECT SCOPE_IDENTITY()";
                    string LastCRMId = "";
                    using (SqlCommand SqlCommand_InsertCRMComment = new SqlCommand(SQLStringInsertCRMComment))
                    {
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReportNumber", CRM_ReportNumber);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateReceived", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_DateForwarded", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_OriginatedAt_List", 4407);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Type_List", 4412);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedVia_List", 4426);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ReceivedFrom_List", 4798);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_EscalatedForm", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFrom", "Discovery Survey");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_UploadedFromReferenceNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerName", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerEmail", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CustomerContactNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientVisitNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientName", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientDateOfAdmission", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientEmail", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_PatientContactNumber", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Complaint_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Compliment_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Query_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Description", "Discovery Survey: " + FixInputString("" + DataRow_Row["F3"] + "") + "");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_Acknowledge", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOut", 0);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Status", "Approved");
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_History", DBNull.Value);
                      SqlCommand_InsertCRMComment.Parameters.AddWithValue("@CRM_Archived", 0);
                      LastCRMId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCRMComment);
                    }

                    if (!string.IsNullOrEmpty(LastCRMId))
                    {
                      if (EmailFacility != FacilityId)
                      {
                        EmailFacility = FacilityId;
                        ExtractData_SendEmail(LastCRMId, "Discovery Survey: No Member Details");
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for No Member Details<br/>File Name: " + fileName + "<br/>File is in the wrong format<br/>" + Exception_Error.ToString() + "</span><br/><br/>";
        }
        else
        {
          throw;
        }
      }

      return ExtractMessage;
    }

    protected static string ExtractData_GetFacilityId(string facility_FacilityCode)
    {
      string FacilityId = "";
      string SQLStringFacilityId = "SELECT Facility_Id FROM vAdministration_Facility_Form_Active WHERE Form_Id = @Form_Id AND Facility_FacilityCode = @Facility_FacilityCode";
      using (SqlCommand SqlCommand_FacilityId = new SqlCommand(SQLStringFacilityId))
      {
        SqlCommand_FacilityId.Parameters.AddWithValue("@Form_Id", "36");
        SqlCommand_FacilityId.Parameters.AddWithValue("@Facility_FacilityCode", facility_FacilityCode);
        DataTable DataTable_FacilityId;
        using (DataTable_FacilityId = new DataTable())
        {
          DataTable_FacilityId.Locale = CultureInfo.CurrentCulture;
          DataTable_FacilityId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityId).Copy();
          if (DataTable_FacilityId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_FacilityId in DataTable_FacilityId.Rows)
            {
              FacilityId = DataRow_Row_FacilityId["Facility_Id"].ToString();
            }
          }
        }
      }

      return FacilityId;
    }

    protected void ExtractData_SendEmail(string lastCRMId, string commentType)
    {
      Session["CommentType"] = commentType;

      Session["EmailTemplate"] = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("55");

      Session["URLAuthority"] = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();

      Session["FormName"] = InfoQuestWCF.InfoQuest_All.All_FormName("36");

      Session["CRMId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["CRMTypeName"] = "";
      string SQLStringCRM = "SELECT CRM_Id, Facility_FacilityDisplayName, CRM_Type_Name FROM vForm_CRM WHERE CRM_Id = @CRM_Id";
      using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
      {
        SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", lastCRMId);
        DataTable DataTable_CRM;
        using (DataTable_CRM = new DataTable())
        {
          DataTable_CRM.Locale = CultureInfo.CurrentCulture;
          DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
          if (DataTable_CRM.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_CRM in DataTable_CRM.Rows)
            {
              Session["CRMId"] = DataRow_Row_CRM["CRM_Id"];
              Session["FacilityFacilityDisplayName"] = DataRow_Row_CRM["Facility_FacilityDisplayName"];
              Session["CRMTypeName"] = DataRow_Row_CRM["CRM_Type_Name"];
            }
          }
        }
      }

      string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();

      string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

      Session["SecurityUserDisplayName"] = "";
      Session["SecurityUserEmail"] = "";
      string SQLStringEmailTo = "SELECT ISNULL(SecurityUser_DisplayName,'') AS SecurityUser_DisplayName, ISNULL(SecurityUser_Email,'') AS SecurityUser_Email FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('36') AND SecurityRole_Id IN ('148') AND Facility_Id IN (SELECT Facility_Id FROM Form_CRM WHERE CRM_Id = @CRM_Id) AND SecurityUser_Email IS NOT NULL";
      using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
      {
        SqlCommand_EmailTo.Parameters.AddWithValue("@CRM_Id", lastCRMId);
        DataTable DataTable_EmailTo;
        using (DataTable_EmailTo = new DataTable())
        {
          DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
          if (DataTable_EmailTo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row_EmailTo in DataTable_EmailTo.Rows)
            {
              Session["SecurityUserDisplayName"] = DataRow_Row_EmailTo["SecurityUser_DisplayName"];
              Session["SecurityUserEmail"] = DataRow_Row_EmailTo["SecurityUser_Email"];

              string BodyString = Session["EmailTemplate"].ToString();

              BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + Session["SecurityUserDisplayName"].ToString() + "");
              BodyString = BodyString.Replace(";replace;FormName;replace;", "" + Session["FormName"].ToString() + "");
              BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + Session["FacilityFacilityDisplayName"].ToString() + "");
              BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + Session["CRMId"].ToString() + "");
              BodyString = BodyString.Replace(";replace;CRMTypeName;replace;", "" + Session["CRMTypeName"].ToString() + "");
              BodyString = BodyString.Replace(";replace;CommentType;replace;", "" + Session["CommentType"].ToString() + "");
              BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + Session["URLAuthority"].ToString() + "");

              Session["EmailBody"] = HeaderString + BodyString + FooterString;

              Session["EmailSend"] = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Session["SecurityUserEmail"].ToString(), Session["FormName"].ToString(), Session["EmailBody"].ToString());

              if (Session["EmailSend"].ToString() == "Yes")
              {
                Session["EmailBody"] = "";
              }
              else
              {
                Session["EmailBody"] = "";
              }

              Session["EmailSend"] = "";
              Session["SecurityUserDisplayName"] = "";
              Session["SecurityUserEmail"] = "";
            }
          }
          else
          {
            Session["SecurityUserDisplayName"] = "";
            Session["SecurityUserEmail"] = "";
          }
        }
      }

      Session["SecurityUserDisplayName"] = "";
      Session["SecurityUserEmail"] = "";

      Session["EmailTemplate"] = "";
      Session["URLAuthority"] = "";
      Session["FormName"] = "";
      Session["CRMId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["CRMTypeName"] = "";
      Session["CommentType"] = "";
    }
  }
}