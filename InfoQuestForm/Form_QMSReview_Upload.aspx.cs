using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.IO.Compression;

namespace InfoQuestForm
{
  public partial class Form_QMSReview_Upload : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " : Upload", CultureInfo.CurrentCulture);
          Label_UploadHeading.Text = Convert.ToString("Upload " + (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " Files", CultureInfo.CurrentCulture);
          Label_UploadedHeading.Text = Convert.ToString("List of Uploaded " + (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " Files", CultureInfo.CurrentCulture);

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

    private string PageSecurity()
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('122'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("31");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_QMSReview_Upload.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Internal Quality Audit", "12");
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
      string UploadPath = Server.MapPath("App_Files/Form_QMSReview_Upload/");

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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','31') AND SecurityUser_Username = @SecurityUser_Username";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '122'");

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
                        CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_QMSReview_Upload\\" + FileFolder + "\\" + FileName + "' target='_blank'>" + FileFolder + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), FileFolder + "\\" + FileName));
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
                      CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_QMSReview_Upload\\" + UploadUserFolder() + "\\" + FileName + "' target='_blank'>" + UploadUserFolder() + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), UploadUserFolder() + "\\" + FileName));
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


    string QMSReviewId = "";

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

          if ((FileExtension == "xls" || FileExtension == "xlsx" || FileExtension == "xlsm") && (FileSize < 5242880))
          {
            try
            {
              if (!Directory.Exists(UploadPath() + UploadUserFolder()))
              {
                Directory.CreateDirectory(UploadPath() + UploadUserFolder());
              }

              if (File.Exists(UploadPath() + UploadUserFolder() + "\\" + FileName))
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span><br/>";
              }
              else
              {
                HttpPostedFile_File.SaveAs(UploadPath() + UploadUserFolder() + "\\" + FileName);

                UploadMessage = UploadMessage + "<span style='color:#77cf9c;'>File Uploading Successful</span><br/>";
              }
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed</span><br/>";
              }
              else
              {
                throw;
              }
            }
          }
          else
          {
            if (FileExtension != "xls" && FileExtension != "xlsx" && FileExtension != "xlsm")
            {
              UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only xls, xlsx and xlsm files can be uploaded<br/>File Name: " + FileName + "</span><br/>";
            }

            if (FileSize > 5242880)
            {
              UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span><br/>";
            }
          }
        }
      }
      else
      {
        UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span><br/>";
      }

      Label_Upload.Text = Convert.ToString(UploadMessage, CultureInfo.CurrentCulture);

      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Upload');</script>");
      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressUpload');</script>");

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

      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>");
      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>");

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

      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>");
      //ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>");

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
              if (FileExtension == "xls" || FileExtension == "xlsx" || FileExtension == "xlsm")
              {
                string ConnectionExtract = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + UploadPath() + UploadedFilesCheckBoxListValues + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                string ExtractFacilityCode = ExtractHeaderFacilityCode(ConnectionExtract);
                DateTime ExtractDate = ExtractHeaderDate(ConnectionExtract);

                Session["DeleteFile"] = "1";

                if (!string.IsNullOrEmpty(ExtractFacilityCode) && ExtractDate.ToString() != "0001/01/01 12:00:00 AM")
                {
                  string FacilityId = "";
                  string FacilityFacilityDisplayName = "";
                  string SQLStringFacilitiesId = "SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Form_Active WHERE Form_Id = 31 AND Facility_FacilityCode = @Facility_FacilityCode";
                  using (SqlCommand SqlCommand_FacilitiesId = new SqlCommand(SQLStringFacilitiesId))
                  {
                    SqlCommand_FacilitiesId.Parameters.AddWithValue("@Facility_FacilityCode", ExtractFacilityCode);
                    DataTable DataTable_FacilitiesId;
                    using (DataTable_FacilitiesId = new DataTable())
                    {
                      DataTable_FacilitiesId.Locale = CultureInfo.CurrentCulture;
                      DataTable_FacilitiesId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilitiesId).Copy();
                      if (DataTable_FacilitiesId.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_FacilitiesId.Rows)
                        {
                          FacilityId = DataRow_Row["Facility_Id"].ToString();
                          FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                        }
                      }
                    }
                  }

                  if (!string.IsNullOrEmpty(FacilityId))
                  {
                    ExtractMessage = ExtractMessage + ExtractData(FileName, ConnectionExtract, ExtractDate, FacilityId, FacilityFacilityDisplayName);
                  }
                  else
                  {
                    Session["DeleteFile"] = "0";
                    ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + FileName + "<br/>" + FacilityFacilityDisplayName + " is not linked to form</span><br/><br/>";
                  }

                  FacilityId = "";
                  FacilityFacilityDisplayName = "";
                }
                else
                {
                  Session["DeleteFile"] = "0";
                  ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + FileName + "<br/>Facility Code or Date could not be extracted</span><br/><br/>";
                }


                try
                {
                  if (Session["DeleteFile"].ToString() == "1")
                  {
                    if (!string.IsNullOrEmpty(QMSReviewId))
                    {
                      string TXTFileName = UploadedFilesCheckBoxListValues.Substring(UploadedFilesCheckBoxListValues.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
                      string ZIPFileName = TXTFileName.Substring(0, TXTFileName.LastIndexOf(".", StringComparison.CurrentCulture)) + ".zip";

                      string TXTFilePathAndName = UploadPath() + UploadedFilesCheckBoxListValues;
                      string ZIPFilePathAndName = TXTFilePathAndName.Substring(0, TXTFilePathAndName.LastIndexOf(".", StringComparison.CurrentCulture)) + ".zip";

                      using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(ZIPFilePathAndName, ZipArchiveMode.Update))
                      {
                        ZipArchive_PathAndName.CreateEntryFromFile(TXTFilePathAndName, TXTFileName);
                      }

                      using (FileStream FileStream_ZIPFile = new FileStream(ZIPFilePathAndName, FileMode.Open, FileAccess.Read))
                      {
                        string ZIPFileContentType = "application/zip";
                        BinaryReader BinaryReader_ZIPFile = new BinaryReader(FileStream_ZIPFile);
                        Byte[] Byte_ZIPFile = BinaryReader_ZIPFile.ReadBytes((Int32)FileStream_ZIPFile.Length);

                        string SQLStringQMSReviewUpdate = "UPDATE InfoQuest_Form_QMSReview SET QMSReview_ZipFileName = @QMSReview_ZipFileName , QMSReview_ContentType = @QMSReview_ContentType , QMSReview_Data = @QMSReview_Data WHERE QMSReview_Id = @QMSReview_Id";
                        using (SqlCommand SqlCommand_QMSReviewUpdate = new SqlCommand(SQLStringQMSReviewUpdate))
                        {
                          SqlCommand_QMSReviewUpdate.Parameters.AddWithValue("@QMSReview_ZipFileName", ZIPFileName);
                          SqlCommand_QMSReviewUpdate.Parameters.AddWithValue("@QMSReview_ContentType", ZIPFileContentType);
                          SqlCommand_QMSReviewUpdate.Parameters.AddWithValue("@QMSReview_Data", Byte_ZIPFile);
                          SqlCommand_QMSReviewUpdate.Parameters.AddWithValue("@QMSReview_Id", QMSReviewId);
                          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_QMSReviewUpdate);
                        }
                      }

                      File.Delete(ZIPFilePathAndName);
                      Session.Remove("QMSReviewId");
                    }
                    
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

      ShowHide("Extract");
      UploadedFiles();
    }

    protected static string ExtractHeaderFacilityCode(string connectionExtract)
    {
      string ExtractFacilityCode = "";

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractFacility = new OleDbDataAdapter("SELECT F3 FROM [Cover Sheet$] WHERE F2 = 'Facility Code:'", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractFacility = new DataTable())
            {
              DataTable_ExtractFacility.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractFacility.Clear();
              DataTable_ExtractFacility.Columns.Add("F3");

              OleDbDataAdapter_ExtractFacility.Fill(DataTable_ExtractFacility);

              foreach (DataRow DataRow_Row in DataTable_ExtractFacility.Rows)
              {
                ExtractFacilityCode = DataRow_Row["F3"].ToString();
              }
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          ExtractFacilityCode = "";
        }
        else
        {
          throw;
        }
      }

      return ExtractFacilityCode;
    }

    protected static DateTime ExtractHeaderDate(string connectionExtract)
    {
      DateTime ExtractDate = new DateTime();

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractDate = new OleDbDataAdapter("SELECT F3 FROM [Cover Sheet$] WHERE F2 = 'Date:'", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractDate = new DataTable())
            {
              DataTable_ExtractDate.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractDate.Clear();
              DataTable_ExtractDate.Columns.Add("F3");

              OleDbDataAdapter_ExtractDate.Fill(DataTable_ExtractDate);

              foreach (DataRow DataRow_Row in DataTable_ExtractDate.Rows)
              {
                ExtractDate = Convert.ToDateTime(Convert.ToDateTime(DataRow_Row["F3"].ToString(), CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
              }
            }
          }
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          ExtractDate = new DateTime();
        }
        else
        {
          throw;
        }
      }

      return ExtractDate;
    }

    protected string ExtractData(string fileName, string connectionExtract, DateTime extractDate, string facilitiesId, string facilityDisplayName)
    {
      string ExtractMessage = "";

      QMSReviewId = "";
      string QMSReviewDate = "";
      string QMSReviewCompleted = "";
      string SQLStringQMSReviewId = "SELECT TOP 1 QMSReview_Id , CASE WHEN QMSReview_Date = @QMSReview_Date THEN QMSReview_Date ELSE NULL END AS QMSReview_Date, CASE WHEN QMSReview_Date = @QMSReview_Date THEN NULL ELSE QMSReview_Completed END AS QMSReview_Completed FROM InfoQuest_Form_QMSReview WHERE Facility_Id = @Facility_Id AND QMSReview_IsActive = 1 AND (QMSReview_Completed = 0 OR QMSReview_Date >= @QMSReview_Date) ORDER BY QMSReview_Id DESC";
      using (SqlCommand SqlCommand_QMSReviewId = new SqlCommand(SQLStringQMSReviewId))
      {
        SqlCommand_QMSReviewId.Parameters.AddWithValue("@Facility_Id", facilitiesId);
        SqlCommand_QMSReviewId.Parameters.AddWithValue("@QMSReview_Date", extractDate);
        DataTable DataTable_QMSReviewId;
        using (DataTable_QMSReviewId = new DataTable())
        {
          DataTable_QMSReviewId.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReviewId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReviewId).Copy();
          if (DataTable_QMSReviewId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReviewId.Rows)
            {
              QMSReviewId = DataRow_Row["QMSReview_Id"].ToString();
              QMSReviewDate = DataRow_Row["QMSReview_Date"].ToString();
              QMSReviewCompleted = DataRow_Row["QMSReview_Completed"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(QMSReviewId))
      {
        string SQLStringInsertQMSReview = "INSERT INTO InfoQuest_Form_QMSReview ( Facility_Id ,QMSReview_Date ,QMSReview_Completed ,QMSReview_CreatedDate ,QMSReview_CreatedBy ,QMSReview_ModifiedDate ,QMSReview_ModifiedBy ,QMSReview_IsActive ,QMSReview_Archived ) VALUES ( @Facility_Id ,@QMSReview_Date ,@QMSReview_Completed ,@QMSReview_CreatedDate ,@QMSReview_CreatedBy ,@QMSReview_ModifiedDate ,@QMSReview_ModifiedBy ,@QMSReview_IsActive ,@QMSReview_Archived ); SELECT SCOPE_IDENTITY()";
        using (SqlCommand SqlCommand_InsertQMSReview = new SqlCommand(SQLStringInsertQMSReview))
        {
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@Facility_Id", facilitiesId);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_Date", extractDate);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_Completed", 0);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_CreatedDate", DateTime.Now);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_CreatedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_ModifiedDate", DateTime.Now);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_IsActive", 1);
          SqlCommand_InsertQMSReview.Parameters.AddWithValue("@QMSReview_Archived", 0);
          QMSReviewId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertQMSReview);
        }

        if (!string.IsNullOrEmpty(QMSReviewId))
        {
          ExtractMessage = ExtractMessage + ExtractData_AuditFindings(fileName, connectionExtract, QMSReviewId);

          if (string.IsNullOrEmpty(ExtractMessage))
          {
            ExtractMessage = ExtractMessage + ExtractData_SummarySheet(fileName, connectionExtract, QMSReviewId);

            if (string.IsNullOrEmpty(ExtractMessage))
            {
              ExtractMessage = ExtractMessage + "<span style='color:#77cf9c;'>File Importing Successful<br/>File Name: " + fileName + "<br/><a href='Form_QMSReview_List.aspx?s_Facility_Id=" + facilitiesId + "' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
            }
            else
            {
              Session["DeleteFile"] = "0";
            }
          }
          else
          {
            Session["DeleteFile"] = "0";
          }
        }
        else
        {
          Session["DeleteFile"] = "0";
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + fileName + "<br/>Data could not be Inserted</span><br/><br/>";
        }
      }
      else
      {
        Session["DeleteFile"] = "0";
        if (string.IsNullOrEmpty(QMSReviewDate))
        {
          if (QMSReviewCompleted == "False")
          {
            ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + fileName + "<br/>There is a open Review for Facility: " + facilityDisplayName + "<br/><a href='Form_QMSReview_List.aspx?s_Facility_Id=" + facilitiesId + "' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
          }
          else if (QMSReviewCompleted == "True")
          {
            ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + fileName + "<br/>A newer review exists, older reviews cannot be added for Facility: " + facilityDisplayName + "<br/><a href='Form_QMSReview_List.aspx?s_Facility_Id=" + facilitiesId + "' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
          }
        }
        else
        {
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + fileName + "<br/>There was already a Review uploaded for Facility: " + facilityDisplayName + " and Date: " + Convert.ToDateTime(QMSReviewDate, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "<br/><a href='Form_QMSReview_List.aspx?s_Facility_Id=" + facilitiesId + "' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
        }
      }

      QMSReviewDate = "";
      QMSReviewCompleted = "";

      return ExtractMessage;
    }

    protected string ExtractData_AuditFindings(string fileName, string connectionExtract, string qmsReviewId)
    {
      string ExtractMessage = "";

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractFindings = new OleDbDataAdapter("SELECT F1 , F2 , F3 , F4 , F5 , F6 , F7 , F8 , F9 FROM [Data$]", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractFindings = new DataTable())
            {
              DataTable_ExtractFindings.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractFindings.Clear();

              using (DataColumn DataColumn_ExtractFindings = new DataColumn())
              {
                DataColumn_ExtractFindings.DataType = System.Type.GetType("System.Int32");
                DataColumn_ExtractFindings.AutoIncrement = true;
                DataColumn_ExtractFindings.AutoIncrementSeed = 1;
                DataColumn_ExtractFindings.AutoIncrementStep = 1;
                DataColumn_ExtractFindings.ColumnName = "Id";

                DataTable_ExtractFindings.Columns.Add(DataColumn_ExtractFindings);
              }
              DataTable_ExtractFindings.Columns.Add("F1");
              DataTable_ExtractFindings.Columns.Add("F2");
              DataTable_ExtractFindings.Columns.Add("F3");
              DataTable_ExtractFindings.Columns.Add("F4");
              DataTable_ExtractFindings.Columns.Add("F5");
              DataTable_ExtractFindings.Columns.Add("F6");
              DataTable_ExtractFindings.Columns.Add("F7");
              DataTable_ExtractFindings.Columns.Add("F8");
              DataTable_ExtractFindings.Columns.Add("F9");

              OleDbDataAdapter_ExtractFindings.Fill(DataTable_ExtractFindings);

              DataRow[] DataTable_ExtractFindings_Rows = DataTable_ExtractFindings.Select("F7 <> '' AND F7 <> 'Category NC/CNC' AND F7 <> '0'", "Id");

              foreach (DataRow DataRow_Row in DataTable_ExtractFindings_Rows)
              {
                string SQLStringInsertQMSReviewFindings = "INSERT INTO InfoQuest_Form_QMSReview_Findings ( QMSReview_Id , QMSReview_Findings_System , QMSReview_Findings_Element , QMSReview_Findings_CriteriaNo , QMSReview_Findings_MeasurementCriteria , QMSReview_Findings_Category , QMSReview_Findings_Comments , QMSReview_Findings_Tracking_List , QMSReview_Findings_TrackingDate , QMSReview_Findings_Score , QMSReview_Findings_CoreStandard , QMSReview_Findings_Legal , QMSReview_Findings_NCS , QMSReview_Findings_LHC , QMSReview_Findings_CreatedDate , QMSReview_Findings_CreatedBy , QMSReview_Findings_ModifiedDate , QMSReview_Findings_ModifiedBy ) VALUES ( @QMSReview_Id , @QMSReview_Findings_System , @QMSReview_Findings_Element , @QMSReview_Findings_CriteriaNo , @QMSReview_Findings_MeasurementCriteria , @QMSReview_Findings_Category , @QMSReview_Findings_Comments , @QMSReview_Findings_Tracking_List , @QMSReview_Findings_TrackingDate , @QMSReview_Findings_Score , @QMSReview_Findings_CoreStandard , @QMSReview_Findings_Legal ,@QMSReview_Findings_NCS , @QMSReview_Findings_LHC , @QMSReview_Findings_CreatedDate , @QMSReview_Findings_CreatedBy , @QMSReview_Findings_ModifiedDate , @QMSReview_Findings_ModifiedBy )";
                using (SqlCommand SqlCommand_InsertQMSReviewFindings = new SqlCommand(SQLStringInsertQMSReviewFindings))
                {
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_System", FixInputString("" + DataRow_Row["F1"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Element", DBNull.Value);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_CriteriaNo", FixInputString("" + DataRow_Row["F5"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_MeasurementCriteria", FixInputString("" + DataRow_Row["F6"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Category", FixInputString("" + DataRow_Row["F7"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Comments", FixInputString("" + DataRow_Row["F9"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Tracking_List", 4314);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_TrackingDate", DateTime.Now);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Score", FixInputString("" + DataRow_Row["F8"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_CoreStandard", DBNull.Value);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_Legal", FixInputString("" + DataRow_Row["F2"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_NCS", FixInputString("" + DataRow_Row["F3"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_LHC", FixInputString("" + DataRow_Row["F4"] + ""));
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_CreatedDate", DateTime.Now);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_ModifiedDate", DateTime.Now);
                  SqlCommand_InsertQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Findings_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertQMSReviewFindings);
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
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for Audit Findings<br/>File Name: " + fileName + "<br/>File is in the wrong format<br/>" + Exception_Error.ToString() + "</span><br/><br/>";

          string SQLStringDeleteQMSReview = "DELETE FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReview = new SqlCommand(SQLStringDeleteQMSReview))
          {
            SqlCommand_DeleteQMSReview.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReview);
          }

          string SQLStringDeleteQMSReviewFindings = "DELETE FROM InfoQuest_Form_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReviewFindings = new SqlCommand(SQLStringDeleteQMSReviewFindings))
          {
            SqlCommand_DeleteQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReviewFindings);
          }

          string SQLStringDeleteQMSReviewSummary = "DELETE FROM InfoQuest_Form_QMSReview_Summary WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReviewSummary = new SqlCommand(SQLStringDeleteQMSReviewSummary))
          {
            SqlCommand_DeleteQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReviewSummary);
          }
        }
        else
        {
          throw;
        }
      }

      return ExtractMessage;
    }

    protected string ExtractData_SummarySheet(string fileName, string connectionExtract, string qmsReviewId)
    {
      string ExtractMessage = "";

      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_ExtractSummary = new OleDbDataAdapter("SELECT F1 , F2 , F3 , F4 , F5 , F6 FROM [Summary$]", OleDbConnection_Extract))
          {
            using (DataTable DataTable_ExtractSummary = new DataTable())
            {
              DataTable_ExtractSummary.Locale = CultureInfo.CurrentCulture;
              DataTable_ExtractSummary.Clear();

              using (DataColumn DataColumn_ExtractSummary = new DataColumn())
              {
                DataColumn_ExtractSummary.DataType = System.Type.GetType("System.Int32");
                DataColumn_ExtractSummary.AutoIncrement = true;
                DataColumn_ExtractSummary.AutoIncrementSeed = 1;
                DataColumn_ExtractSummary.AutoIncrementStep = 1;
                DataColumn_ExtractSummary.ColumnName = "Id";

                DataTable_ExtractSummary.Columns.Add(DataColumn_ExtractSummary);
              }
              DataTable_ExtractSummary.Columns.Add("F1");
              DataTable_ExtractSummary.Columns.Add("F2");
              DataTable_ExtractSummary.Columns.Add("F3");
              DataTable_ExtractSummary.Columns.Add("F4");
              DataTable_ExtractSummary.Columns.Add("F5");
              DataTable_ExtractSummary.Columns.Add("F6");

              OleDbDataAdapter_ExtractSummary.Fill(DataTable_ExtractSummary);

              DataRow[] DataTable_ExtractSummary_Rows = DataTable_ExtractSummary.Select("F6 <> ''", "Id");
              foreach (DataRow DataRow_Row in DataTable_ExtractSummary_Rows)
              {
                string SQLStringInsertQMSReviewSummary = "INSERT INTO InfoQuest_Form_QMSReview_Summary ( QMSReview_Id ,QMSReview_Summary_Contribution ,QMSReview_Summary_Element , QMSReview_Summary_SubElementScore ,QMSReview_Summary_ElementScore ,QMSReview_Summary_TotalScore ,QMSReview_Summary_Identifier ,QMSReview_Summary_CreatedDate ,QMSReview_Summary_CreatedBy ,QMSReview_Summary_ModifiedDate ,QMSReview_Summary_ModifiedBy ) VALUES ( @QMSReview_Id , @QMSReview_Summary_Contribution , @QMSReview_Summary_Element , @QMSReview_Summary_SubElementScore , @QMSReview_Summary_ElementScore , @QMSReview_Summary_TotalScore , @QMSReview_Summary_Identifier , @QMSReview_Summary_CreatedDate , @QMSReview_Summary_CreatedBy , @QMSReview_Summary_ModifiedDate , @QMSReview_Summary_ModifiedBy )";
                using (SqlCommand SqlCommand_InsertQMSReviewSummary = new SqlCommand(SQLStringInsertQMSReviewSummary))
                {
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_Contribution", DataRow_Row["F1"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_Element", DataRow_Row["F2"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_SubElementScore", DataRow_Row["F3"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_ElementScore", DataRow_Row["F4"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_TotalScore", DataRow_Row["F5"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_Identifier", DataRow_Row["F6"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_CreatedDate", DateTime.Now);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_ModifiedDate", DateTime.Now);
                  SqlCommand_InsertQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Summary_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertQMSReviewSummary);
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
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for Summary Sheet<br/>File Name: " + fileName + "<br/>File is in the wrong format<br/>" + Exception_Error.ToString() + "</span><br/><br/>";

          string SQLStringDeleteQMSReview = "DELETE FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReview = new SqlCommand(SQLStringDeleteQMSReview))
          {
            SqlCommand_DeleteQMSReview.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReview);
          }

          string SQLStringDeleteQMSReviewFindings = "DELETE FROM InfoQuest_Form_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReviewFindings = new SqlCommand(SQLStringDeleteQMSReviewFindings))
          {
            SqlCommand_DeleteQMSReviewFindings.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReviewFindings);
          }

          string SQLStringDeleteQMSReviewSummary = "DELETE FROM InfoQuest_Form_QMSReview_Summary WHERE QMSReview_Id = @QMSReview_Id";
          using (SqlCommand SqlCommand_DeleteQMSReviewSummary = new SqlCommand(SQLStringDeleteQMSReviewSummary))
          {
            SqlCommand_DeleteQMSReviewSummary.Parameters.AddWithValue("@QMSReview_Id", qmsReviewId);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteQMSReviewSummary);
          }
        }
        else
        {
          throw;
        }
      }

      return ExtractMessage;
    }
  }
}