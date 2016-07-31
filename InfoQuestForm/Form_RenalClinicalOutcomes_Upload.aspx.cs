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
  public partial class Form_RenalClinicalOutcomes_Upload : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " : Upload", CultureInfo.CurrentCulture);
          Label_UploadHeading.Text = Convert.ToString("Upload " + (InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " Files", CultureInfo.CurrentCulture);
          Label_UploadedHeading.Text = Convert.ToString("List of Uploaded " + (InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " Files", CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('200'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("49");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_RenalClinicalOutcomes_Upload.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Renal Clinical Outcomes", "26");
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
      string UploadPath = Server.MapPath("App_Files/Form_RenalClinicalOutcomes_Upload/");

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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','50') AND SecurityUser_Username = @SecurityUser_Username";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '200'");

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
                        CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_RenalClinicalOutcomes_Upload\\" + FileFolder + "\\" + FileName + "' target='_blank'>" + FileFolder + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), FileFolder + "\\" + FileName));
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
                      CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_RenalClinicalOutcomes_Upload\\" + UploadUserFolder() + "\\" + FileName + "' target='_blank'>" + UploadUserFolder() + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), UploadUserFolder() + "\\" + FileName));
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


    string ExtractMessage = "";
    string CanDeleteFile = "Yes";
    Int32 RenalClinicalOutcomesCount = 0;

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

                ExtractData(FileName, ConnectionExtract);
                ProcessData();
                DeleteFile(FileName, UploadedFilesCheckBoxListValues);
              }
            }
          }
        }
      }

      Label_Extract.Text = Convert.ToString(ExtractMessage, CultureInfo.CurrentCulture);

      ShowHide("Extract");
      UploadedFiles();
    }

    protected void ExtractData(string fileName, string connectionExtract)
    {
      try
      {
        using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(connectionExtract))
        {
          using (OleDbDataAdapter OleDbDataAdapter_Extract = new OleDbDataAdapter())
          {
            using (OleDbCommand OleDbCommand_Extract = new OleDbCommand(@"SELECT  F1 AS RenalClinicalOutcomes_DataUpload_Facility , 
                                                                                  F2 AS RenalClinicalOutcomes_DataUpload_CaptureMonth ,
                                                                                  F3 AS RenalClinicalOutcomes_DataUpload_CaptureYear , 
                                                                                  F4 AS RenalClinicalOutcomes_DataUpload_TreatingDoctorName , 
                                                                                  F5 AS RenalClinicalOutcomes_DataUpload_TreatingDoctorEmail , 
                                                                                  F6 AS RenalClinicalOutcomes_DataUpload_PatientName ,
                                                                                  F7 AS RenalClinicalOutcomes_DataUpload_PatientSurname , 
                                                                                  F8 AS RenalClinicalOutcomes_DataUpload_MedicalAid , 
                                                                                  F9 AS RenalClinicalOutcomes_DataUpload_MedicalAidNumber , 
                                                                                  F10 AS RenalClinicalOutcomes_DataUpload_DateOfBirth , 
                                                                                  F11 AS RenalClinicalOutcomes_DataUpload_TransplantList , 
                                                                                  F12 AS RenalClinicalOutcomes_DataUpload_ModeOfDialysis ,
                                                                                  F13 AS RenalClinicalOutcomes_DataUpload_DialysisType ,
                                                                                  F14 AS RenalClinicalOutcomes_DataUpload_FirstDialysisYear , 
                                                                                  F15 AS RenalClinicalOutcomes_DataUpload_FirstDialysisMonth , 
                                                                                  F16 AS RenalClinicalOutcomes_DataUpload_Diabetic , 
                                                                                  F17 AS RenalClinicalOutcomes_DataUpload_Cardiovascular , 
                                                                                  F18 AS RenalClinicalOutcomes_DataUpload_Hypercholestrolaemia , 
                                                                                  F19 AS RenalClinicalOutcomes_DataUpload_PulmonaryDisease ,
                                                                                  F20 AS RenalClinicalOutcomes_DataUpload_Cancer , 
                                                                                  F21 AS RenalClinicalOutcomes_DataUpload_AutoImmuneDisease , 
                                                                                  F22 AS RenalClinicalOutcomes_DataUpload_Endocrine , 
                                                                                  F23 AS RenalClinicalOutcomes_DataUpload_Haemoglobin , 
                                                                                  F24 AS RenalClinicalOutcomes_DataUpload_Iron , 
                                                                                  F25 AS RenalClinicalOutcomes_DataUpload_SerumCalcium , 
                                                                                  F26 AS RenalClinicalOutcomes_DataUpload_SerumPhosphate , 
                                                                                  F27 AS RenalClinicalOutcomes_DataUpload_SerumAlbumin , 
                                                                                  F28 AS RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pgmlngL , 
                                                                                  F29 AS RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pmlL , 
                                                                                  F30 AS RenalClinicalOutcomes_DataUpload_DialysisAccessType , 
                                                                                  F31 AS RenalClinicalOutcomes_DataUpload_HepatitisSurfaceAbLevelResult , 
                                                                                  F32 AS RenalClinicalOutcomes_DataUpload_KTV ,
                                                                                  @RenalClinicalOutcomesDataUploadInfoQuestUploadUser AS RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser ,
                                                                                  @RenalClinicalOutcomesDataUploadInfoQuestUploadFrom AS RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom ,
                                                                                  @RenalClinicalOutcomesDataUploadInfoQuestUploadDate AS RenalClinicalOutcomes_DataUpload_InfoQuestUploadDate
                                                                          FROM    [Capture Fields$]", OleDbConnection_Extract))
            {
              OleDbCommand_Extract.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadUser", Request.ServerVariables["LOGON_USER"]);
              OleDbCommand_Extract.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadFrom", "User Upload");
              OleDbCommand_Extract.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadDate", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture));

              OleDbDataAdapter_Extract.SelectCommand = OleDbCommand_Extract;

              using (DataTable DataTable_Extract = new DataTable())
              {
                DataTable_Extract.Locale = CultureInfo.CurrentCulture;
                DataTable_Extract.Clear();

                using (DataColumn DataColumn_Extract = new DataColumn())
                {
                  DataColumn_Extract.DataType = System.Type.GetType("System.Int32");
                  DataColumn_Extract.AutoIncrement = true;
                  DataColumn_Extract.AutoIncrementSeed = 1;
                  DataColumn_Extract.AutoIncrementStep = 1;
                  DataColumn_Extract.ColumnName = "Id";

                  DataTable_Extract.Columns.Add(DataColumn_Extract);
                }
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Facility");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_CaptureMonth");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_CaptureYear");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_TreatingDoctorName");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_TreatingDoctorEmail");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_PatientName");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_PatientSurname");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_MedicalAid");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_MedicalAidNumber");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_DateOfBirth");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_TransplantList");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_ModeOfDialysis");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_DialysisType");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_FirstDialysisYear");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_FirstDialysisMonth");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Diabetic");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Cardiovascular");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Hypercholestrolaemia");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_PulmonaryDisease");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Cancer");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_AutoImmuneDisease");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Endocrine");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Haemoglobin");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_Iron");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumCalcium");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumPhosphate");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumAlbumin");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pgmlngL");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pmlL");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_DialysisAccessType");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_HepatitisSurfaceAbLevelResult");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_KTV");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom");
                DataTable_Extract.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadDate");

                OleDbDataAdapter_Extract.Fill(DataTable_Extract);

                using (DataTable DataTable_Select = new DataTable())
                {
                  DataTable_Select.Locale = CultureInfo.CurrentCulture;
                  DataTable_Select.Clear();

                  using (DataColumn DataColumn_Select = new DataColumn())
                  {
                    DataColumn_Select.DataType = System.Type.GetType("System.Int32");
                    DataColumn_Select.AutoIncrement = true;
                    DataColumn_Select.AutoIncrementSeed = 1;
                    DataColumn_Select.AutoIncrementStep = 1;
                    DataColumn_Select.ColumnName = "Id";

                    DataTable_Select.Columns.Add(DataColumn_Select);
                  }
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Facility");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_CaptureMonth");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_CaptureYear");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_TreatingDoctorName");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_TreatingDoctorEmail");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_PatientName");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_PatientSurname");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_MedicalAid");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_MedicalAidNumber");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_DateOfBirth");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_TransplantList");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_ModeOfDialysis");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_DialysisType");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_FirstDialysisYear");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_FirstDialysisMonth");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Diabetic");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Cardiovascular");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Hypercholestrolaemia");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_PulmonaryDisease");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Cancer");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_AutoImmuneDisease");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Endocrine");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Haemoglobin");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_Iron");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumCalcium");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumPhosphate");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_SerumAlbumin");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pgmlngL");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pmlL");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_DialysisAccessType");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_HepatitisSurfaceAbLevelResult");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_KTV");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom");
                  DataTable_Select.Columns.Add("RenalClinicalOutcomes_DataUpload_InfoQuestUploadDate");

                  DataRow[] DataTable_Extract_Rows = DataTable_Extract.Select("RenalClinicalOutcomes_DataUpload_Facility <> '' AND RenalClinicalOutcomes_DataUpload_Facility <> 'Facility'", "Id");

                  foreach (DataRow DataRow_Row in DataTable_Extract_Rows)
                  {
                    DataTable_Select.ImportRow(DataRow_Row);
                  }

                  RenalClinicalOutcomesCount = DataTable_Select.Rows.Count;

                  if (DataTable_Select.Columns.Count > 1)
                  {
                    if (DataTable_Select.Rows.Count > 0)
                    {
                      try
                      {
                        string BulkCopyConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
                        using (SqlConnection SqlConnection_BulkCopy = new SqlConnection(BulkCopyConnectionString))
                        {
                          SqlConnection_BulkCopy.Open();

                          using (SqlBulkCopy SqlBulkCopy_File = new SqlBulkCopy(SqlConnection_BulkCopy))
                          {
                            SqlBulkCopy_File.DestinationTableName = "Form_RenalClinicalOutcomes_DataUpload";

                            foreach (DataColumn DataColumn_ColumnNames in DataTable_Select.Columns)
                            {
                              string SQLStringColumn = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Form_RenalClinicalOutcomes_DataUpload') AND CONVERT(VARBINARY(MAX), name) = CONVERT(VARBINARY(MAX), @name) ORDER BY column_id";
                              using (SqlCommand SqlCommand_Column = new SqlCommand(SQLStringColumn))
                              {
                                SqlCommand_Column.Parameters.AddWithValue("@name", DataColumn_ColumnNames.ColumnName);
                                DataTable DataTable_Column;
                                using (DataTable_Column = new DataTable())
                                {
                                  DataTable_Column.Locale = CultureInfo.CurrentCulture;
                                  DataTable_Column = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Column).Copy();
                                  if (DataTable_Column.Rows.Count > 0)
                                  {
                                    foreach (DataRow DataRow_Row_FacilityId in DataTable_Column.Rows)
                                    {
                                      string name = DataRow_Row_FacilityId["name"].ToString();

                                      SqlBulkCopyColumnMapping SqlBulkCopyColumnMapping_Column = new SqlBulkCopyColumnMapping(name, name);
                                      SqlBulkCopy_File.ColumnMappings.Add(SqlBulkCopyColumnMapping_Column);
                                    }
                                  }
                                }
                              }
                            }


                            SqlBulkCopy_File.WriteToServer(DataTable_Select);
                            ExtractMessage = ExtractMessage + "<span style='color:#77cf9c;'>File Importing Successful<br/>File Name: " + fileName + "<br/><a href='Form_RenalClinicalOutcomes_List.aspx' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
                          }
                        }
                      }
                      catch (Exception Exception_Error)
                      {
                        CanDeleteFile = "No";

                        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                        {
                          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for Renal Clinical Outcomes<br/>File Name: " + fileName + "<br/>File is in the wrong format</span><br/><br/>";
                        }
                        else
                        {
                          throw;
                        }
                      }
                    }
                    else
                    {
                      ExtractMessage = ExtractMessage + "<span style='color:#77cf9c;'>File Importing Successful<br/>File Name: " + fileName + "<br/><a href='Form_RenalClinicalOutcomes_List.aspx' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
                    }
                  }
                  else
                  {
                    ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for Renal Clinical Outcomes<br/>File Name: " + fileName + "<br/>File is in the wrong format</span><br/><br/>";
                    CanDeleteFile = "No";
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
          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed for Renal Clinical Outcomes<br/>File Name: " + fileName + "<br/>File is in the wrong format<br/>" + Exception_Error.ToString() + "</span><br/><br/>";
          CanDeleteFile = "No";
        }
        else
        {
          throw;
        }
      }
    }

    protected void ProcessData()
    {
      if (CanDeleteFile == "Yes")
      {
        string SQLStringRenalClinicalOutcomesInsert = @"INSERT INTO Form_RenalClinicalOutcomes
                                                        SELECT	RenalClinicalOutcomes_DataUpload_Facility , 
				                                                        RenalClinicalOutcomes_DataUpload_CaptureMonth ,
                                                                RenalClinicalOutcomes_DataUpload_CaptureYear , 
				                                                        RenalClinicalOutcomes_DataUpload_TreatingDoctorName , 
				                                                        RenalClinicalOutcomes_DataUpload_TreatingDoctorEmail , 
				                                                        RenalClinicalOutcomes_DataUpload_PatientName , 
				                                                        RenalClinicalOutcomes_DataUpload_PatientSurname , 
				                                                        RenalClinicalOutcomes_DataUpload_MedicalAid , 
				                                                        RenalClinicalOutcomes_DataUpload_MedicalAidNumber , 
				                                                        RenalClinicalOutcomes_DataUpload_DateOfBirth , 
				                                                        RenalClinicalOutcomes_DataUpload_TransplantList , 
				                                                        RenalClinicalOutcomes_DataUpload_ModeOfDialysis , 
				                                                        RenalClinicalOutcomes_DataUpload_DialysisType , 
				                                                        RenalClinicalOutcomes_DataUpload_FirstDialysisYear , 
				                                                        RenalClinicalOutcomes_DataUpload_FirstDialysisMonth , 
				                                                        RenalClinicalOutcomes_DataUpload_Diabetic , 
				                                                        RenalClinicalOutcomes_DataUpload_Cardiovascular , 
				                                                        RenalClinicalOutcomes_DataUpload_Hypercholestrolaemia , 
				                                                        RenalClinicalOutcomes_DataUpload_PulmonaryDisease , 
				                                                        RenalClinicalOutcomes_DataUpload_Cancer , 
				                                                        RenalClinicalOutcomes_DataUpload_AutoImmuneDisease , 
				                                                        RenalClinicalOutcomes_DataUpload_Endocrine , 
				                                                        RenalClinicalOutcomes_DataUpload_Haemoglobin , 
				                                                        RenalClinicalOutcomes_DataUpload_Iron , 
				                                                        RenalClinicalOutcomes_DataUpload_SerumCalcium , 
				                                                        RenalClinicalOutcomes_DataUpload_SerumPhosphate , 
				                                                        RenalClinicalOutcomes_DataUpload_SerumAlbumin , 
				                                                        RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pgmlngL , 
				                                                        RenalClinicalOutcomes_DataUpload_ParathyroidHormone_pmlL , 
				                                                        RenalClinicalOutcomes_DataUpload_DialysisAccessType , 
				                                                        RenalClinicalOutcomes_DataUpload_HepatitisSurfaceAbLevelResult , 
				                                                        RenalClinicalOutcomes_DataUpload_KTV , 
				                                                        RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser , 
				                                                        RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom , 
				                                                        RenalClinicalOutcomes_DataUpload_InfoQuestUploadDate 
                                                        FROM		Form_RenalClinicalOutcomes_DataUpload a
				                                                        LEFT OUTER JOIN Form_RenalClinicalOutcomes b 
					                                                        ON a.RenalClinicalOutcomes_DataUpload_Facility = b.RenalClinicalOutcomes_Facility AND
                                                                  a.RenalClinicalOutcomes_DataUpload_CaptureMonth = b.RenalClinicalOutcomes_CaptureMonth AND
                                                                  a.RenalClinicalOutcomes_DataUpload_CaptureYear = b.RenalClinicalOutcomes_CaptureYear AND
					                                                        a.RenalClinicalOutcomes_DataUpload_TreatingDoctorName = b.RenalClinicalOutcomes_TreatingDoctorName AND
					                                                        a.RenalClinicalOutcomes_DataUpload_TreatingDoctorEmail = b.RenalClinicalOutcomes_TreatingDoctorEmail AND 
					                                                        a.RenalClinicalOutcomes_DataUpload_PatientName = b.RenalClinicalOutcomes_PatientName AND
					                                                        a.RenalClinicalOutcomes_DataUpload_PatientSurname = b.RenalClinicalOutcomes_PatientSurname AND
					                                                        a.RenalClinicalOutcomes_DataUpload_MedicalAid = b.RenalClinicalOutcomes_MedicalAid AND
					                                                        a.RenalClinicalOutcomes_DataUpload_MedicalAidNumber = b.RenalClinicalOutcomes_MedicalAidNumber AND
					                                                        a.RenalClinicalOutcomes_DataUpload_DateOfBirth = b.RenalClinicalOutcomes_DateOfBirth
                                                        WHERE		b.RenalClinicalOutcomes_Facility IS NULL AND
				                                                        a.RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser = @RenalClinicalOutcomesDataUploadInfoQuestUploadUser AND
				                                                        a.RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom = @RenalClinicalOutcomesDataUploadInfoQuestUploadFrom";
        using (SqlCommand SqlCommand_RenalClinicalOutcomesInsert = new SqlCommand(SQLStringRenalClinicalOutcomesInsert))
        {
          SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadUser", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadFrom", "User Upload");
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_RenalClinicalOutcomesInsert);
        }
      }

      string SQLStringRenalClinicalOutcomesDataUploadDelete = @"DELETE 
                                                                FROM		Form_RenalClinicalOutcomes_DataUpload
                                                                WHERE		RenalClinicalOutcomes_DataUpload_InfoQuestUploadUser = @RenalClinicalOutcomesDataUploadInfoQuestUploadUser
				                                                                AND RenalClinicalOutcomes_DataUpload_InfoQuestUploadFrom = @RenalClinicalOutcomesDataUploadInfoQuestUploadFrom";
      using (SqlCommand SqlCommand_RenalClinicalOutcomesDataUploadDelete = new SqlCommand(SQLStringRenalClinicalOutcomesDataUploadDelete))
      {
        SqlCommand_RenalClinicalOutcomesDataUploadDelete.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadUser", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_RenalClinicalOutcomesDataUploadDelete.Parameters.AddWithValue("@RenalClinicalOutcomesDataUploadInfoQuestUploadFrom", "User Upload");
        InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_RenalClinicalOutcomesDataUploadDelete);
      }
    }

    protected void DeleteFile(string fileName, string uploadedFilesCheckBoxListValues)
    {
      if (!string.IsNullOrEmpty(uploadedFilesCheckBoxListValues))
      {
        if (CanDeleteFile == "Yes")
        {
          try
          {
            string TXTFileName = uploadedFilesCheckBoxListValues.Substring(uploadedFilesCheckBoxListValues.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
            string ZIPFileName = TXTFileName.Substring(0, TXTFileName.LastIndexOf(".", StringComparison.CurrentCulture)) + ".zip";

            string TXTFilePathAndName = UploadPath() + uploadedFilesCheckBoxListValues;
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

              string SQLStringRenalClinicalOutcomesInsert = @"INSERT INTO Form_RenalClinicalOutcomes_FileUpload 
                                                              ( RenalClinicalOutcomes_FileUpload_FileName , RenalClinicalOutcomes_FileUpload_ZipFileName , RenalClinicalOutcomes_FileUpload_ContentType , RenalClinicalOutcomes_FileUpload_Data , RenalClinicalOutcomes_FileUpload_Records , RenalClinicalOutcomes_FileUpload_CurrentDate , RenalClinicalOutcomes_FileUpload_From )
                                                              VALUES
                                                              ( @RenalClinicalOutcomes_FileUpload_FileName , @RenalClinicalOutcomes_FileUpload_ZipFileName , @RenalClinicalOutcomes_FileUpload_ContentType , @RenalClinicalOutcomes_FileUpload_Data , @RenalClinicalOutcomes_FileUpload_Records , @RenalClinicalOutcomes_FileUpload_CurrentDate , @RenalClinicalOutcomes_FileUpload_From )";
              using (SqlCommand SqlCommand_RenalClinicalOutcomesInsert = new SqlCommand(SQLStringRenalClinicalOutcomesInsert))
              {
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_FileName", TXTFileName);
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_ZipFileName", ZIPFileName);
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_ContentType", ZIPFileContentType);
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_Data", Byte_ZIPFile);
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_Records", RenalClinicalOutcomesCount.ToString(CultureInfo.CurrentCulture));
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_CurrentDate", DateTime.Now);
                SqlCommand_RenalClinicalOutcomesInsert.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_From", "User Upload");
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_RenalClinicalOutcomesInsert);
              }
            }

            File.Delete(ZIPFilePathAndName);

            File.Delete(UploadPath() + uploadedFilesCheckBoxListValues);
            DirectoryCleanUp();
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + fileName + "</span><br/><br/>";
            }
            else
            {
              throw;
            }
          }
        }

        CanDeleteFile = "Yes";
      }
    }
  }
}