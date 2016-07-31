using System;
using System.Collections.Generic;
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
  public class UploadedTemplateFilesList
  {
    public string Values { get; set; }
  }


  public partial class Form_MonthlyHospitalStatistics_Waste_Upload : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_TemplateHeading.Text = Convert.ToString("Template " + (InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString() + " Waste Files", CultureInfo.CurrentCulture);
          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString() + " : Waste Upload", CultureInfo.CurrentCulture);
          Label_UploadHeading.Text = Convert.ToString("Upload " + (InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString() + " Waste Files", CultureInfo.CurrentCulture);
          Label_UploadedHeading.Text = Convert.ToString("List of Uploaded " + (InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString() + " Waste Files", CultureInfo.CurrentCulture);

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

                TableTemplate.Visible = true;
                TableUpload.Visible = true;
                TableUploaded.Visible = true;

                UploadedTemplateFiles();
                UploadedFiles();

                DirectoryCleanUp();
              }
              else if (Session["FolderSecurity"].ToString() == "1")
              {
                TableTemplate.Visible = false;
                TableUpload.Visible = false;
                TableUploaded.Visible = false;
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('55'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("5");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MHS_Waste_Upload.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Hospital Statistics", "19");
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

    private string UploadTemplatePath()
    {
      string UploadTemplatePath = Server.MapPath("App_Files/Form_MonthlyHospitalStatistics_Waste_Upload_Template/");

      return UploadTemplatePath;
    }

    private string UploadPath()
    {
      string UploadPath = Server.MapPath("App_Files/Form_MonthlyHospitalStatistics_Waste_Upload/");

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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','5') AND SecurityUser_Username = @SecurityUser_Username";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '55'");

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
                        CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_MonthlyHospitalStatistics_Waste_Upload\\" + FileFolder + "\\" + FileName + "' target='_blank'>" + FileFolder + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), FileFolder + "\\" + FileName));
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
              catch (Exception ex)
              {
                if (!string.IsNullOrEmpty(ex.ToString()))
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
                      CheckBoxList_UploadedFiles.Items.Add(new ListItem(Convert.ToString("<a href='App_Files\\Form_MonthlyHospitalStatistics_Waste_Upload\\" + UploadUserFolder() + "\\" + FileName + "' target='_blank'>" + UploadUserFolder() + "\\" + FileName + "</a>", CultureInfo.CurrentCulture), UploadUserFolder() + "\\" + FileName));
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
              catch (Exception ex)
              {
                if (!string.IsNullOrEmpty(ex.ToString()))
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

    private void UploadedTemplateFiles()
    {
      try
      {
        if (Directory.Exists(UploadTemplatePath()))
        {
          string[] UploadedTemplateFiles = Directory.GetFiles(UploadTemplatePath(), "*.*", SearchOption.AllDirectories);

          if (UploadedTemplateFiles.Length > 0)
          {
            Label_UploadedTemplateTotalFiles.Text = "" + UploadedTemplateFiles.Length + "";

            Repeater_UploadedTemplateFiles.DataSource = null;
            Repeater_UploadedTemplateFiles.DataBind();
            Label_UploadedTemplateFiles.Text = "";

            List<UploadedTemplateFilesList> List_Values = new List<UploadedTemplateFilesList>();

            foreach (string Files in Directory.GetFiles(UploadTemplatePath(), "*.*", SearchOption.AllDirectories))
            {
              string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
              string FileFolder = Files.Replace("\\" + FileName + "", "");
              FileFolder = FileFolder.Substring(FileFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);

              List_Values.Add(new UploadedTemplateFilesList { Values = "<a href='App_Files\\Form_MonthlyHospitalStatistics_Waste_Upload_Template\\" + FileName + "' target='_blank'>" + FileName + "</a>" });
            }

            Repeater_UploadedTemplateFiles.DataSource = List_Values;
            Repeater_UploadedTemplateFiles.DataBind();
          }
          else
          {
            Label_UploadedTemplateTotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

            Repeater_UploadedTemplateFiles.DataSource = null;
            Repeater_UploadedTemplateFiles.DataBind();
            Label_UploadedTemplateFiles.Text = Convert.ToString("No Uploaded Template Files", CultureInfo.CurrentCulture);
          }
        }
        else
        {
          Label_UploadedTemplateTotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

          Repeater_UploadedTemplateFiles.DataSource = null;
          Repeater_UploadedTemplateFiles.DataBind();
          Label_UploadedTemplateFiles.Text = Convert.ToString("No Uploaded Template Files", CultureInfo.CurrentCulture);
        }
      }
      catch (Exception ex)
      {
        if (!string.IsNullOrEmpty(ex.ToString()))
        {
          Label_UploadedTemplateTotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

          Repeater_UploadedTemplateFiles.DataSource = null;
          Repeater_UploadedTemplateFiles.DataBind();
          Label_UploadedTemplateFiles.Text = Convert.ToString("Error Accessing Folders and Files", CultureInfo.CurrentCulture);
        }
        else
        {
          throw;
        }
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
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
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

      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Upload');</script>");
      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressUpload');</script>");

      ShowHide("Upload");
      UploadedTemplateFiles();
      UploadedFiles();
    }

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < CheckBoxList_UploadedFiles.Items.Count; i++)
      {
        if (CheckBoxList_UploadedFiles.Items[i].Selected)
        {
          string UploadedFilesCheckBoxListValues = "";
          UploadedFilesCheckBoxListValues = CheckBoxList_UploadedFiles.Items[i].Value;

          Session["UploadedFilesCheckBoxListValues"] = UploadedFilesCheckBoxListValues;

          if (!string.IsNullOrEmpty(Session["UploadedFilesCheckBoxListValues"].ToString()))
          {
            try
            {
              if (Directory.Exists(UploadPath()))
              {
                if (File.Exists(UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString()))
                {
                  File.Delete(UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString());
                  DirectoryCleanUp();

                  DeleteMessage = DeleteMessage + "<span style='color:#77cf9c;'>File Deletion Successful<br/>File Name: " + Session["UploadedFilesCheckBoxListValues"].ToString() + "</span><br/><br/>";
                }
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DeleteMessage = DeleteMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + Session["UploadedFilesCheckBoxListValues"].ToString() + "</span><br/><br/>";
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

      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>");
      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>");

      ShowHide("Delete");
      UploadedTemplateFiles();
      UploadedFiles();
    }

    protected void Button_DeleteAll_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < CheckBoxList_UploadedFiles.Items.Count; i++)
      {
        string UploadedFilesCheckBoxListValues = "";
        UploadedFilesCheckBoxListValues = CheckBoxList_UploadedFiles.Items[i].Value;

        Session["UploadedFilesCheckBoxListValues"] = UploadedFilesCheckBoxListValues;

        if (!string.IsNullOrEmpty(Session["UploadedFilesCheckBoxListValues"].ToString()))
        {
          try
          {
            if (Directory.Exists(UploadPath()))
            {
              if (File.Exists(UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString()))
              {
                File.Delete(UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString());
                DirectoryCleanUp();

                DeleteMessage = DeleteMessage + "<span style='color:#77cf9c;'>File Deletion Successful<br/>File Name: " + Session["UploadedFilesCheckBoxListValues"].ToString() + "</span><br/><br/>";
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DeleteMessage = DeleteMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + Session["UploadedFilesCheckBoxListValues"].ToString() + "</span><br/><br/>";
            }
            else
            {
              throw;
            }
          }
        }
      }

      Label_Delete.Text = Convert.ToString(DeleteMessage, CultureInfo.CurrentCulture);

      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Delete');</script>");
      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressDelete');</script>");

      ShowHide("Delete");
      UploadedTemplateFiles();
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

          Session["UploadedFilesCheckBoxListValues"] = UploadedFilesCheckBoxListValues;

          if (!string.IsNullOrEmpty(Session["UploadedFilesCheckBoxListValues"].ToString()))
          {
            try
            {
              if (Directory.Exists(UploadPath()))
              {
                if (FileExtension == "xlsx" || FileExtension == "xls")
                {
                  string ConnectionExtract = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString() + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                  Session["DeleteFile"] = "1";

                  using (OleDbConnection OleDbConnection_Extract = new OleDbConnection(ConnectionExtract))
                  {
                    using (OleDbDataAdapter OleDbDataAdapter_ExtractWaste = new OleDbDataAdapter("SELECT F2 , F3 , F4 , F5 , F6 FROM [Import$]", OleDbConnection_Extract))
                    {
                      using (DataTable DataTable_ExtractWaste = new DataTable())
                      {
                        DataTable_ExtractWaste.Locale = CultureInfo.CurrentCulture;
                        DataTable_ExtractWaste.Clear();

                        using (DataColumn DataColumn_ExtractWaste = new DataColumn())
                        {
                          DataColumn_ExtractWaste.DataType = System.Type.GetType("System.Int32");
                          DataColumn_ExtractWaste.AutoIncrement = true;
                          DataColumn_ExtractWaste.AutoIncrementSeed = 1;
                          DataColumn_ExtractWaste.AutoIncrementStep = 1;
                          DataColumn_ExtractWaste.ColumnName = "Id";

                          DataTable_ExtractWaste.Columns.Add(DataColumn_ExtractWaste);
                        }
                        DataTable_ExtractWaste.Columns.Add("F1");
                        DataTable_ExtractWaste.Columns.Add("F2");
                        DataTable_ExtractWaste.Columns.Add("F3");
                        DataTable_ExtractWaste.Columns.Add("F4");
                        DataTable_ExtractWaste.Columns.Add("F5");
                        DataTable_ExtractWaste.Columns.Add("F6");

                        OleDbDataAdapter_ExtractWaste.Fill(DataTable_ExtractWaste);

                        DataRow[] DataTable_ExtractWaste_Rows = DataTable_ExtractWaste.Select("(F2 <> '' OR F3 <> '' OR F4 <> '' OR F5 <> '' OR F6 <> '') AND (F3 <> 'Year' AND F4 <> 'Month')", "Id");

                        if (Convert.ToInt32(DataTable_ExtractWaste_Rows.Length.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture) > 0)
                        {
                          ExtractMessage = Extract_Data(DataTable_ExtractWaste, ExtractMessage, FileName);
                        }
                        else
                        {
                          Session["DeleteFile"] = "0";
                          ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + FileName + "<br/>Data could not be Imported</span><br/><br/>";
                        }
                      }
                    }
                  }


                  try
                  {
                    if (Session["DeleteFile"].ToString() == "1")
                    {
                      File.Delete(UploadPath() + Session["UploadedFilesCheckBoxListValues"].ToString());
                      DirectoryCleanUp();
                    }
                  }
                  catch (Exception ex)
                  {
                    if (!string.IsNullOrEmpty(ex.ToString()))
                    {
                      ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Deletion Failed<br/>File Name: " + FileName + "</span><br/><br/>";
                    }
                    else
                    {
                      throw;
                    }
                  }


                  Session["DeleteFile"] = "";
                  Session["MHS_Id"] = "";
                }
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                ExtractMessage = ExtractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + FileName + "<br/>File is in the wrong format</span><br/><br/>";
              }
              else
              {
                throw;
              }
            }
          }
        }
      }

      Label_Extract.Text = Convert.ToString(ExtractMessage, CultureInfo.CurrentCulture);

      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Show('Label_Extract');</script>");
      ClientScript.RegisterStartupScript(this.GetType(), "Progress", "<script language='javascript'>Hide('Label_ProgressExtract');</script>");

      ShowHide("Extract");
      UploadedTemplateFiles();
      UploadedFiles();
    }

    protected string Extract_Data(DataTable dataTable_ExtractWaste, string extractMessage, string fileName)
    {
      if (dataTable_ExtractWaste != null)
      {
        DataRow[] DataTable_ExtractWasteFinal_Rows = Compare_Data(dataTable_ExtractWaste).Select("F5 <> ''", "Id");

        if (Convert.ToInt32(DataTable_ExtractWasteFinal_Rows.Length.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture) > 0)
        {
          foreach (DataRow DataRow_Row in DataTable_ExtractWasteFinal_Rows)
          {
            Session["MHS_Id"] = "";
            string SQLStringMHSId = "SELECT MHS_Id FROM vForm_MonthlyHospitalStatistics WHERE Facility_FacilityCode = @Facility_FacilityCode AND LEFT(MHS_Period,4) = @Year AND (CASE RIGHT(LEFT(MHS_Period,7),2) WHEN '01' THEN '1' WHEN '02' THEN '2' WHEN '03' THEN '3' WHEN '04' THEN '4' WHEN '05' THEN '5' WHEN '06' THEN '6' WHEN '07' THEN '7' WHEN '08' THEN '8' WHEN '09' THEN '9' WHEN '10' THEN '10' WHEN '11' THEN '11' WHEN '12' THEN '12' END) = @Month";
            using (SqlCommand SqlCommand_MHSId = new SqlCommand(SQLStringMHSId))
            {
              SqlCommand_MHSId.Parameters.AddWithValue("@Facility_FacilityCode", DataRow_Row["F2"]);
              SqlCommand_MHSId.Parameters.AddWithValue("@Year", Convert.ToInt32(DataRow_Row["F3"], CultureInfo.CurrentCulture));
              SqlCommand_MHSId.Parameters.AddWithValue("@Month", Convert.ToInt32(DataRow_Row["F4"], CultureInfo.CurrentCulture));
              DataTable DataTable_MHSId;
              using (DataTable_MHSId = new DataTable())
              {
                DataTable_MHSId.Locale = CultureInfo.CurrentCulture;
                DataTable_MHSId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHSId).Copy();
                if (DataTable_MHSId.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_MHSIdRow in DataTable_MHSId.Rows)
                  {
                    Session["MHS_Id"] = DataRow_MHSIdRow["MHS_Id"];
                  }
                }
              }
            }


            if (!string.IsNullOrEmpty(Session["MHS_Id"].ToString()))
            {
              Session["MHS_Waste_Id"] = "";
              string SQLStringMHSWasteId = "SELECT DISTINCT MHS_Waste_Id " +
                                           "FROM InfoQuest_Form_MonthlyHospitalStatistics_Waste " +
                                           "WHERE MHS_Id IN ( " +
	                                         "  SELECT MHS_Id " +
	                                         "  FROM vForm_MonthlyHospitalStatistics " +
	                                         "  WHERE Facility_FacilityCode = @Facility_FacilityCode " +
	                                         "  AND LEFT(MHS_Period,4) = @Year " +
	                                         "  AND (CASE RIGHT(LEFT(MHS_Period,7),2) WHEN '01' THEN '1' WHEN '02' THEN '2' WHEN '03' THEN '3' WHEN '04' THEN '4' WHEN '05' THEN '5' WHEN '06' THEN '6' WHEN '07' THEN '7' WHEN '08' THEN '8' WHEN '09' THEN '9' WHEN '10' THEN '10' WHEN '11' THEN '11' WHEN '12' THEN '12' END) = @Month " +
                                           ") " +
                                           "AND MHS_Waste_Identifier_List = @WasteIdentifier " +
                                           "AND MHS_Waste_Value IS NULL";
              using (SqlCommand SqlCommand_MHSWasteId = new SqlCommand(SQLStringMHSWasteId))
              {
                SqlCommand_MHSWasteId.Parameters.AddWithValue("@Facility_FacilityCode", DataRow_Row["F2"]);
                SqlCommand_MHSWasteId.Parameters.AddWithValue("@Year", Convert.ToInt32(DataRow_Row["F3"], CultureInfo.CurrentCulture));
                SqlCommand_MHSWasteId.Parameters.AddWithValue("@Month", Convert.ToInt32(DataRow_Row["F4"], CultureInfo.CurrentCulture));
                SqlCommand_MHSWasteId.Parameters.AddWithValue("@WasteIdentifier", DataRow_Row["F5"]);
                DataTable DataTable_MHSWasteId;
                using (DataTable_MHSWasteId = new DataTable())
                {
                  DataTable_MHSWasteId.Locale = CultureInfo.CurrentCulture;
                  DataTable_MHSWasteId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHSWasteId).Copy();
                  if (DataTable_MHSWasteId.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_MHSWasteIdRow in DataTable_MHSWasteId.Rows)
                    {
                      Session["MHS_Waste_Id"] = DataRow_MHSWasteIdRow["MHS_Waste_Id"];
                    }
                  }
                }
              }

              if (!string.IsNullOrEmpty(Session["MHS_Waste_Id"].ToString()))
              {
                string SQLStringUpdateMHSWaste = "UPDATE InfoQuest_Form_MonthlyHospitalStatistics_Waste SET MHS_Waste_Value = @MHS_Waste_Value , MHS_Waste_PPD = @MHS_Waste_PPD , MHS_Waste_ModifiedDate = @MHS_Waste_ModifiedDate , MHS_Waste_ModifiedBy = @MHS_Waste_ModifiedBy WHERE MHS_Waste_Id = @MHS_Waste_Id";
                using (SqlCommand SqlCommand_UpdateMHSWaste = new SqlCommand(SQLStringUpdateMHSWaste))
                {
                  SqlCommand_UpdateMHSWaste.Parameters.AddWithValue("@MHS_Waste_Id", Session["MHS_Waste_Id"].ToString());
                  SqlCommand_UpdateMHSWaste.Parameters.AddWithValue("@MHS_Waste_Value", DataRow_Row["F6"]);
                  SqlCommand_UpdateMHSWaste.Parameters.AddWithValue("@MHS_Waste_PPD", DBNull.Value);
                  SqlCommand_UpdateMHSWaste.Parameters.AddWithValue("@MHS_Waste_ModifiedDate", DateTime.Now.ToString());
                  SqlCommand_UpdateMHSWaste.Parameters.AddWithValue("@MHS_Waste_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMHSWaste);
                }
              }
            }
            else
            {
              extractMessage = extractMessage + "<span style='color:#d46e6e;'>Item Importing Failed: Facility code : " + DataRow_Row["F1"] + " ; Month : " + DataRow_Row["F4"] + "<br/>" +
                                                "There is no form created for that Facility code in that Month</span><br/><br/>";
            }

            Session["MHS_Id"] = "";
          }

          extractMessage = extractMessage + "<span style='color:#77cf9c;'>File Importing Successful<br/>File Name: " + fileName + "<br/><a href='Form_MonthlyHospitalStatistics_List.aspx' target='_blank' style='color:#003768;'>Click Here</a> to view</span><br/><br/>";
        }
        else
        {
          Session["DeleteFile"] = "0";
          extractMessage = extractMessage + "<span style='color:#d46e6e;'>File Importing Failed<br/>File Name: " + fileName + "<br/>Data could not be Imported</span><br/><br/>";
        }
      }

      return extractMessage;
    }

    protected DataTable Compare_Data(DataTable dataTable_ExtractWaste)
    {
      if (dataTable_ExtractWaste != null)
      {
        string SQLStringWasteIdentifierList = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_All WHERE Form_Id = 5 AND ListCategory_Id = 57";
        DataTable DataTable_WasteIdentifierList;
        using (SqlCommand SqlCommand_WasteIdentifierList = new SqlCommand(SQLStringWasteIdentifierList))
        {
          using (DataTable_WasteIdentifierList = new DataTable())
          {
            DataTable_WasteIdentifierList.Locale = CultureInfo.CurrentCulture;
            DataTable_WasteIdentifierList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_WasteIdentifierList).Copy();
          }
        }

        foreach (DataRow DataRow_Row in dataTable_ExtractWaste.Rows)
        {
          Session["F5Finished"] = "0";
          Session["F5NewValue"] = "";
          foreach (DataRow DataRow_RowWasteIdentifierList in DataTable_WasteIdentifierList.Rows)
          {
            if (DataRow_Row["F5"].ToString() == DataRow_RowWasteIdentifierList["ListItem_Name"].ToString() && Session["F5Finished"].ToString() == "0")
            {
              Session["F5Finished"] = "1";
              Session["F5NewValue"] = DataRow_RowWasteIdentifierList["ListItem_Id"].ToString();
            }
            else
            {
              if (Session["F5Finished"].ToString() == "0")
              {
                Session["F5NewValue"] = "";
              }
            }
          }

          if (Session["F5Finished"].ToString() == "1")
          {
            DataRow_Row["F5"] = Session["F5NewValue"].ToString();
          }
          else if (Session["F5Finished"].ToString() == "0")
          {
            DataRow_Row["F5"] = "";
          }
          Session["F5Finished"] = "0";
        }
      }

      return dataTable_ExtractWaste;
    }
  }
}