using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Net;
using System.Security;

namespace InfoQuestWCF
{
  public static class InfoQuest_DataEmployee
  {
    private static string EmptyParameter = "Yes";

    private static PrincipalContext AD_AccountManagement_PrincipalContext()
    {
      FromDataBase_Impersonation FromDataBase_Impersonation_Current = GetImpersonation();
      string ImpersonationUserName = FromDataBase_Impersonation_Current.ImpersonationUserName;
      string ImpersonationPassword = FromDataBase_Impersonation_Current.ImpersonationPassword;
      string ImpersonationDomain = FromDataBase_Impersonation_Current.ImpersonationDomain;

      string Server_Production = InfoQuest_All.All_SystemServer("2").ToString();
      string Server_DomainController = InfoQuest_All.All_SystemServer("6").ToString();
      string Server_Current = Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture);

      PrincipalContext PrincipalContext_AD;

      if ((!string.IsNullOrEmpty(Server_Production)) && (Server_Production == Server_Current))
      {
        PrincipalContext_AD = new PrincipalContext(ContextType.Domain, System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName);
      }
      else
      {
        PrincipalContext_AD = new PrincipalContext(ContextType.Domain, Server_DomainController, ImpersonationDomain + @"\" + ImpersonationUserName, ImpersonationPassword);
      }
      
      return PrincipalContext_AD;
    }

    private class FromDataBase_Impersonation
    {
      public string ImpersonationUserName { get; set; }
      public string ImpersonationPassword { get; set; }
      public string ImpersonationDomain { get; set; }
    }

    private static FromDataBase_Impersonation GetImpersonation()
    {
      FromDataBase_Impersonation FromDataBase_Impersonation_New = new FromDataBase_Impersonation();

      string SQLStringForm = "SELECT SystemAccount_Domain , SystemAccount_UserName , SystemAccount_Password FROM Administration_SystemAccount WHERE SystemAccount_Id = 1";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;
          DataTable_Form = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              FromDataBase_Impersonation_New.ImpersonationUserName = DataRow_Row["SystemAccount_UserName"].ToString();
              FromDataBase_Impersonation_New.ImpersonationPassword = DataRow_Row["SystemAccount_Password"].ToString();
              FromDataBase_Impersonation_New.ImpersonationDomain = DataRow_Row["SystemAccount_Domain"].ToString();
            }
          }
        }
      }

      return FromDataBase_Impersonation_New;
    }

    private static string ValidateParameter(string parameter)
    {
      string NewParameter = "";

      if (!string.IsNullOrEmpty(parameter))
      {
        NewParameter = parameter.ToLower(CultureInfo.CurrentCulture);
        if (!string.IsNullOrEmpty(NewParameter) && NewParameter.IndexOf(@"lhc\", StringComparison.CurrentCulture) == 0)
        {
          NewParameter = NewParameter.Remove(0, 4);
        }

        EmptyParameter = "No";
      }

      return NewParameter;
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_AccountManagement_FindGroupMembers(string groupName)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //Guid            : AD_UserGUID         :

      string NewGroupName = ValidateParameter(groupName);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Group Data");
        }
        else
        {
          try
          {
            PrincipalContext PrincipalContext_AD = AD_AccountManagement_PrincipalContext();
            GroupPrincipal GroupPrincipal_AD = GroupPrincipal.FindByIdentity(PrincipalContext_AD, NewGroupName);

            if (GroupPrincipal_AD != null)
            {
              PrincipalSearchResult<Principal> PrincipalSearchResult_Result = GroupPrincipal_AD.GetMembers();

              if (PrincipalSearchResult_Result.Count() <= 0)
              {
                DataTable_AD.Reset();
                DataTable_AD.Columns.Add("Error", typeof(string));
                DataTable_AD.Rows.Add("No Group Members");
              }
              else
              {
                DataTable_AD.Reset();
                DataTable_AD.Columns.Add("UserName", typeof(string));
                DataTable_AD.Columns.Add("UserGuid", typeof(string));

                foreach (UserPrincipal UserPrincipal_Result in PrincipalSearchResult_Result)
                {
                  if (UserPrincipal_Result.Enabled == true)
                  {
                    if (UserPrincipal_Result.SamAccountName != null)
                    {
                      string AD_UserName = UserPrincipal_Result.SamAccountName;
                      string AD_UserGUID = UserPrincipal_Result.Guid.ToString();

                      DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_UserGUID.ToLower(CultureInfo.CurrentCulture));
                    }
                  }
                }
              }
            }
            else
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Group does not exist");
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_AccountManagement_FindUserGroups(string userName)
    {
      //COLUMNS
      //Name            : AD_GroupName         : MOSS InfoQuest HAI Flora
      //Guid            : AD_GroupGUID         :

      string NewUserName = ValidateParameter(userName);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No User Data");
        }
        else
        {
          try
          {
            PrincipalContext PrincipalContext_AD = AD_AccountManagement_PrincipalContext();
            UserPrincipal UserPrincipal_AD = UserPrincipal.FindByIdentity(PrincipalContext_AD, NewUserName);

            if (UserPrincipal_AD != null)
            {
              PrincipalSearchResult<Principal> PrincipalSearchResult_Result = UserPrincipal_AD.GetGroups();
              if (PrincipalSearchResult_Result.Count() <= 0)
              {
                DataTable_AD.Reset();
                DataTable_AD.Columns.Add("Error", typeof(string));
                DataTable_AD.Rows.Add("No Groups");
              }
              else
              {
                DataTable_AD.Reset();
                DataTable_AD.Columns.Add("GroupName", typeof(string));
                DataTable_AD.Columns.Add("GroupGuid", typeof(string));

                foreach (Principal Principal_Result in PrincipalSearchResult_Result)
                {
                  if (Principal_Result.Name != null)
                  {
                    string AD_GroupName = Principal_Result.Name;
                    string AD_GroupGUID = Principal_Result.Guid.ToString();

                    DataTable_AD.Rows.Add(AD_GroupName.ToLower(CultureInfo.CurrentCulture), AD_GroupGUID.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
            else
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("User does not exist");
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    [SecurityCritical]
    public static bool DataEmployee_AD_AccountManagement_UserGroupMember(string userName, string groupName)
    {
      PrincipalContext PrincipalContext_AD = AD_AccountManagement_PrincipalContext();
      UserPrincipal UserPrincipal_AD = UserPrincipal.FindByIdentity(PrincipalContext_AD, userName);
      GroupPrincipal GroupPrincipal_AD = GroupPrincipal.FindByIdentity(PrincipalContext_AD, groupName);

      if (UserPrincipal_AD != null && GroupPrincipal_AD != null)
      {
        if (UserPrincipal_AD.IsMemberOf(GroupPrincipal_AD))
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_AccountManagement_FindAll(string userName, string displayName, string employeeNumber, string email)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //displayName     : AD_DisplayName      : swart,jacobus
      //givenName       : AD_FirstName        : jacobus
      //sn              : AD_LastName         : swart
      //employeeID      : AD_EmployeeNumber   : 9006814
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za
      //manager         : AD_ManagerUserName  : rogersa

      string NewUserName = ValidateParameter(userName);
      string NewDisplayName = ValidateParameter(displayName);
      string NewEmployeeNumber = ValidateParameter(employeeNumber);
      string NewEmail = ValidateParameter(email);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            using (UserPrincipal UserPrincipal_AD = new UserPrincipal(AD_AccountManagement_PrincipalContext()))
            {
              if (!string.IsNullOrEmpty(NewUserName))
              {
                UserPrincipal_AD.SamAccountName = "*" + NewUserName + "*";
              }

              if (!string.IsNullOrEmpty(NewDisplayName))
              {
                UserPrincipal_AD.DisplayName = "*" + NewDisplayName + "*";
              }

              if (!string.IsNullOrEmpty(NewEmployeeNumber))
              {
                UserPrincipal_AD.EmployeeId = "*" + NewEmployeeNumber + "*";
              }

              if (!string.IsNullOrEmpty(NewEmail))
              {
                UserPrincipal_AD.EmailAddress = "*" + NewEmail + "*";
              }

              UserPrincipal_AD.Enabled = true;

              using (PrincipalSearcher PrincipalSearcher_AD = new PrincipalSearcher(UserPrincipal_AD))
              {
                PrincipalSearchResult<Principal> PrincipalSearchResult_Result = PrincipalSearcher_AD.FindAll();

                if (PrincipalSearchResult_Result.Count() <= 0)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("UserName", typeof(string));
                  DataTable_AD.Columns.Add("DisplayName", typeof(string));
                  DataTable_AD.Columns.Add("FirstName", typeof(string));
                  DataTable_AD.Columns.Add("LastName", typeof(string));
                  DataTable_AD.Columns.Add("EmployeeNumber", typeof(string));
                  DataTable_AD.Columns.Add("Email", typeof(string));
                  DataTable_AD.Columns.Add("ManagerUserName", typeof(string));

                  foreach (UserPrincipal UserPrincipal_Result in PrincipalSearchResult_Result)
                  {
                    string AD_UserName = "";
                    string AD_DisplayName = "";
                    string AD_FirstName = "";
                    string AD_LastName = "";
                    string AD_EmployeeNumber = "";
                    string AD_Email = "";
                    string AD_ManagerUserName = "";

                    if (UserPrincipal_Result.SamAccountName != null)
                    {
                      AD_UserName = UserPrincipal_Result.SamAccountName;
                    }

                    if (UserPrincipal_Result.DisplayName != null)
                    {
                      AD_DisplayName = UserPrincipal_Result.DisplayName;
                    }

                    if (UserPrincipal_Result.GivenName != null)
                    {
                      AD_FirstName = UserPrincipal_Result.GivenName;
                    }

                    if (UserPrincipal_Result.Surname != null)
                    {
                      AD_LastName = UserPrincipal_Result.Surname;
                    }

                    if (UserPrincipal_Result.EmployeeId != null)
                    {
                      AD_EmployeeNumber = UserPrincipal_Result.EmployeeId;
                    }

                    if (UserPrincipal_Result.EmailAddress != null)
                    {
                      AD_Email = UserPrincipal_Result.EmailAddress;
                    }

                    using (PrincipalContext PrincipalContext_Manager = AD_AccountManagement_PrincipalContext())
                    {
                      UserPrincipal UserPrincipal_User = UserPrincipal.FindByIdentity(PrincipalContext_Manager, AD_UserName);
                      if (UserPrincipal_User != null)
                      {
                        DirectoryEntry DirectoryEntry_Manager = UserPrincipal_User.GetUnderlyingObject() as DirectoryEntry;

                        if (DirectoryEntry_Manager != null)
                        {
                          if (DirectoryEntry_Manager.Properties["manager"] != null && DirectoryEntry_Manager.Properties["manager"].Count > 0)
                          {
                            string ManagerDN = DirectoryEntry_Manager.Properties["manager"][0].ToString();
                            AD_ManagerUserName = UserPrincipal.FindByIdentity(PrincipalContext_Manager, ManagerDN).ToString();
                          }
                        }
                      }
                    }

                    DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_DisplayName, AD_FirstName, AD_LastName, AD_EmployeeNumber, AD_Email.ToLower(CultureInfo.CurrentCulture), AD_ManagerUserName.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_AccountManagement_FindOne_UserName(string userName)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //displayName     : AD_DisplayName      : swart,jacobus
      //givenName       : AD_FirstName        : jacobus
      //sn              : AD_LastName         : swart
      //employeeID      : AD_EmployeeNumber   : 9006814
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za
      //manager         : AD_ManagerUserName  : rogersa

      string NewUserName = ValidateParameter(userName);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            using (UserPrincipal UserPrincipal_AD = new UserPrincipal(AD_AccountManagement_PrincipalContext()))
            {
              if (!string.IsNullOrEmpty(NewUserName))
              {
                UserPrincipal_AD.SamAccountName = "" + NewUserName + "";
              }

              UserPrincipal_AD.Enabled = true;

              using (PrincipalSearcher PrincipalSearcher_AD = new PrincipalSearcher(UserPrincipal_AD))
              {
                PrincipalSearchResult<Principal> PrincipalSearchResult_Result = PrincipalSearcher_AD.FindAll();

                if (PrincipalSearchResult_Result.Count() <= 0)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("UserName", typeof(string));
                  DataTable_AD.Columns.Add("DisplayName", typeof(string));
                  DataTable_AD.Columns.Add("FirstName", typeof(string));
                  DataTable_AD.Columns.Add("LastName", typeof(string));
                  DataTable_AD.Columns.Add("EmployeeNumber", typeof(string));
                  DataTable_AD.Columns.Add("Email", typeof(string));
                  DataTable_AD.Columns.Add("ManagerUserName", typeof(string));

                  foreach (UserPrincipal UserPrincipal_Result in PrincipalSearchResult_Result)
                  {
                    string AD_UserName = "";
                    string AD_DisplayName = "";
                    string AD_FirstName = "";
                    string AD_LastName = "";
                    string AD_EmployeeNumber = "";
                    string AD_Email = "";
                    string AD_ManagerUserName = "";

                    if (UserPrincipal_Result.SamAccountName != null)
                    {
                      AD_UserName = UserPrincipal_Result.SamAccountName;
                    }

                    if (UserPrincipal_Result.DisplayName != null)
                    {
                      AD_DisplayName = UserPrincipal_Result.DisplayName;
                    }

                    if (UserPrincipal_Result.GivenName != null)
                    {
                      AD_FirstName = UserPrincipal_Result.GivenName;
                    }

                    if (UserPrincipal_Result.Surname != null)
                    {
                      AD_LastName = UserPrincipal_Result.Surname;
                    }

                    if (UserPrincipal_Result.EmployeeId != null)
                    {
                      AD_EmployeeNumber = UserPrincipal_Result.EmployeeId;
                    }

                    if (UserPrincipal_Result.EmailAddress != null)
                    {
                      AD_Email = UserPrincipal_Result.EmailAddress;
                    }

                    using (PrincipalContext PrincipalContext_Manager = AD_AccountManagement_PrincipalContext())
                    {
                      UserPrincipal UserPrincipal_User = UserPrincipal.FindByIdentity(PrincipalContext_Manager, AD_UserName);
                      if (UserPrincipal_User != null)
                      {
                        DirectoryEntry DirectoryEntry_Manager = UserPrincipal_User.GetUnderlyingObject() as DirectoryEntry;

                        if (DirectoryEntry_Manager != null)
                        {
                          if (DirectoryEntry_Manager.Properties["manager"] != null && DirectoryEntry_Manager.Properties["manager"].Count > 0)
                          {
                            string ManagerDN = DirectoryEntry_Manager.Properties["manager"][0].ToString();
                            AD_ManagerUserName = UserPrincipal.FindByIdentity(PrincipalContext_Manager, ManagerDN).ToString();
                          }
                        }
                      }
                    }

                    DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_DisplayName, AD_FirstName, AD_LastName, AD_EmployeeNumber, AD_Email.ToLower(CultureInfo.CurrentCulture), AD_ManagerUserName.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_AccountManagement_FindOne_EmployeeNumber(string employeeNumber)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //displayName     : AD_DisplayName      : swart,jacobus
      //givenName       : AD_FirstName        : jacobus
      //sn              : AD_LastName         : swart
      //employeeID      : AD_EmployeeNumber   : 9006814
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za
      //manager         : AD_ManagerUserName  : rogersa

      string NewEmployeeNumber = ValidateParameter(employeeNumber);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            using (UserPrincipal UserPrincipal_AD = new UserPrincipal(AD_AccountManagement_PrincipalContext()))
            {
              if (!string.IsNullOrEmpty(NewEmployeeNumber))
              {
                UserPrincipal_AD.EmployeeId = "" + NewEmployeeNumber + "";
              }

              UserPrincipal_AD.Enabled = true;

              using (PrincipalSearcher PrincipalSearcher_AD = new PrincipalSearcher(UserPrincipal_AD))
              {
                PrincipalSearchResult<Principal> PrincipalSearchResult_Result = PrincipalSearcher_AD.FindAll();

                if (PrincipalSearchResult_Result.Count() <= 0)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("UserName", typeof(string));
                  DataTable_AD.Columns.Add("DisplayName", typeof(string));
                  DataTable_AD.Columns.Add("FirstName", typeof(string));
                  DataTable_AD.Columns.Add("LastName", typeof(string));
                  DataTable_AD.Columns.Add("EmployeeNumber", typeof(string));
                  DataTable_AD.Columns.Add("Email", typeof(string));
                  DataTable_AD.Columns.Add("ManagerUserName", typeof(string));

                  foreach (UserPrincipal UserPrincipal_Result in PrincipalSearchResult_Result)
                  {
                    string AD_UserName = "";
                    string AD_DisplayName = "";
                    string AD_FirstName = "";
                    string AD_LastName = "";
                    string AD_EmployeeNumber = "";
                    string AD_Email = "";
                    string AD_ManagerUserName = "";

                    if (UserPrincipal_Result.SamAccountName != null)
                    {
                      AD_UserName = UserPrincipal_Result.SamAccountName;
                    }

                    if (UserPrincipal_Result.DisplayName != null)
                    {
                      AD_DisplayName = UserPrincipal_Result.DisplayName;
                    }

                    if (UserPrincipal_Result.GivenName != null)
                    {
                      AD_FirstName = UserPrincipal_Result.GivenName;
                    }

                    if (UserPrincipal_Result.Surname != null)
                    {
                      AD_LastName = UserPrincipal_Result.Surname;
                    }

                    if (UserPrincipal_Result.EmployeeId != null)
                    {
                      AD_EmployeeNumber = UserPrincipal_Result.EmployeeId;
                    }

                    if (UserPrincipal_Result.EmailAddress != null)
                    {
                      AD_Email = UserPrincipal_Result.EmailAddress;
                    }

                    using (PrincipalContext PrincipalContext_Manager = AD_AccountManagement_PrincipalContext())
                    {
                      UserPrincipal UserPrincipal_User = UserPrincipal.FindByIdentity(PrincipalContext_Manager, AD_UserName);
                      if (UserPrincipal_User != null)
                      {
                        DirectoryEntry DirectoryEntry_Manager = UserPrincipal_User.GetUnderlyingObject() as DirectoryEntry;

                        if (DirectoryEntry_Manager != null)
                        {
                          if (DirectoryEntry_Manager.Properties["manager"] != null && DirectoryEntry_Manager.Properties["manager"].Count > 0)
                          {
                            string ManagerDN = DirectoryEntry_Manager.Properties["manager"][0].ToString();
                            AD_ManagerUserName = UserPrincipal.FindByIdentity(PrincipalContext_Manager, ManagerDN).ToString();
                          }
                        }
                      }
                    }

                    DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_DisplayName, AD_FirstName, AD_LastName, AD_EmployeeNumber, AD_Email.ToLower(CultureInfo.CurrentCulture), AD_ManagerUserName.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    public static DataTable DataEmployee_AD_AccountManagement_FindAll_InfoQuest_Email_GetEmailAddressList(string email)
    {
      //COLUMNS
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za

      string NewEmail = ValidateParameter(email);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            using (UserPrincipal UserPrincipal_AD = new UserPrincipal(AD_AccountManagement_PrincipalContext()))
            {
              if (!string.IsNullOrEmpty(NewEmail))
              {
                UserPrincipal_AD.EmailAddress = "" + NewEmail + "*";
              }

              UserPrincipal_AD.Enabled = true;

              using (PrincipalSearcher PrincipalSearcher_AD = new PrincipalSearcher(UserPrincipal_AD))
              {
                PrincipalSearchResult<Principal> PrincipalSearchResult_Result = PrincipalSearcher_AD.FindAll();

                if (PrincipalSearchResult_Result.Count() <= 0)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Email", typeof(string));

                  foreach (UserPrincipal UserPrincipal_Result in PrincipalSearchResult_Result)
                  {
                    string AD_Email = "";

                    if (UserPrincipal_Result.EmailAddress != null)
                    {
                      AD_Email = UserPrincipal_Result.EmailAddress;
                    }

                    DataTable_AD.Rows.Add(AD_Email.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }


    [SecurityCritical]
    public static DataTable DataEmployee_AD_DirectoryServices_FindAll(string userName, string displayName, string employeeNumber, string email)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //displayName     : AD_DisplayName      : swart,jacobus
      //givenName       : AD_FirstName        : jacobus
      //sn              : AD_LastName         : swart
      //employeeID      : AD_EmployeeNumber   : 9006814
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za
      //manager         : AD_ManagerUserName  : rogersa

      string NewUserName = ValidateParameter(userName);
      string NewDisplayName = ValidateParameter(displayName);
      string NewEmployeeNumber = ValidateParameter(employeeNumber);
      string NewEmail = ValidateParameter(email);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;

        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            DataTable_AD.Reset();

            DirectoryEntry DirectoryEntry_AD;
            using (DirectoryEntry_AD = new DirectoryEntry())
            {
              DirectoryEntry_AD.Path = "LDAP://DC=lhc,DC=local";
              DirectoryEntry_AD.AuthenticationType = AuthenticationTypes.Secure;
              DirectorySearcher DirectorySearcher_AD;
              using (DirectorySearcher_AD = new DirectorySearcher(DirectoryEntry_AD))
              {
                DirectorySearcher_AD.SearchScope = SearchScope.Subtree;
                DirectorySearcher_AD.ReferralChasing = ReferralChasingOption.All;

                string SearchFilters = "";
                string SearchFilters_UserName = "";
                string SearchFilters_DisplayName = "";
                string SearchFilters_EmployeeNumber = "";
                string SearchFilters_Email = "";

                if (!string.IsNullOrEmpty(NewUserName))
                {
                  SearchFilters_UserName = "(samAccountName=*" + NewUserName + "*)";
                }

                if (!string.IsNullOrEmpty(NewDisplayName))
                {
                  SearchFilters_DisplayName = "(displayName=*" + NewDisplayName + "*)";
                }

                if (!string.IsNullOrEmpty(NewEmployeeNumber))
                {
                  SearchFilters_EmployeeNumber = "(employeeID=*" + NewEmployeeNumber + "*)";
                }

                if (!string.IsNullOrEmpty(NewEmail))
                {
                  SearchFilters_Email = "(mail=*" + NewEmail + "*)";
                }

                SearchFilters = SearchFilters_UserName + SearchFilters_DisplayName + SearchFilters_EmployeeNumber + SearchFilters_Email;

                DirectorySearcher_AD.Filter = string.Format(CultureInfo.CurrentCulture, "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)" + SearchFilters + ")");

                SearchResult SearchResult_AD = DirectorySearcher_AD.FindOne();
                SearchResultCollection SearchResultCollection_AD = DirectorySearcher_AD.FindAll();

                if (SearchResult_AD == null)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("UserName", typeof(string));
                  DataTable_AD.Columns.Add("DisplayName", typeof(string));
                  DataTable_AD.Columns.Add("FirstName", typeof(string));
                  DataTable_AD.Columns.Add("LastName", typeof(string));
                  DataTable_AD.Columns.Add("EmployeeNumber", typeof(string));
                  DataTable_AD.Columns.Add("Email", typeof(string));
                  DataTable_AD.Columns.Add("ManagerUserName", typeof(string));

                  foreach (SearchResult SearchResult_All in SearchResultCollection_AD)
                  {
                    DirectoryEntry_AD = SearchResult_All.GetDirectoryEntry();

                    string AD_UserName = "";
                    string AD_DisplayName = "";
                    string AD_FirstName = "";
                    string AD_LastName = "";
                    string AD_EmployeeNumber = "";
                    string AD_Email = "";
                    string AD_ManagerUserName = "";

                    if (DirectoryEntry_AD.Properties.Contains("samAccountName") == true)
                    {
                      AD_UserName = DirectoryEntry_AD.Properties["samAccountName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("displayName") == true)
                    {
                      AD_DisplayName = DirectoryEntry_AD.Properties["displayName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("givenName") == true)
                    {
                      AD_FirstName = DirectoryEntry_AD.Properties["givenName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("sn") == true)
                    {
                      AD_LastName = DirectoryEntry_AD.Properties["sn"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("employeeID") == true)
                    {
                      AD_EmployeeNumber = DirectoryEntry_AD.Properties["employeeID"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("mail") == true)
                    {
                      AD_Email = DirectoryEntry_AD.Properties["mail"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("manager") == true)
                    {
                      AD_ManagerUserName = DirectoryEntry_AD.Properties["manager"][0].ToString();
                      int IndexAD_ManagerUserName = AD_ManagerUserName.IndexOfAny(new char[] { ',' });
                      int LengthAD_ManagerUserName = AD_ManagerUserName.Length;

                      AD_ManagerUserName = AD_ManagerUserName.Remove(IndexAD_ManagerUserName, LengthAD_ManagerUserName - IndexAD_ManagerUserName);
                      AD_ManagerUserName = AD_ManagerUserName.Remove(0, 3);

                      AD_ManagerUserName = "LHC\\" + AD_ManagerUserName.ToString();
                    }

                    DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_DisplayName, AD_FirstName, AD_LastName, AD_EmployeeNumber, AD_Email.ToLower(CultureInfo.CurrentCulture), AD_ManagerUserName.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }

    [SecurityCritical]
    public static DataTable DataEmployee_AD_DirectoryServices_FindOne(string userName)
    {
      //COLUMNS
      //samAccountName  : AD_UserName         : swartj2
      //displayName     : AD_DisplayName      : swart,jacobus
      //givenName       : AD_FirstName        : jacobus
      //sn              : AD_LastName         : swart
      //employeeID      : AD_EmployeeNumber   : 9006814
      //mail            : AD_Email            : jacobus.swart@lifehealthcare.co.za
      //manager         : AD_ManagerUserName  : rogersa

      string NewUserName = ValidateParameter(userName);

      using (DataTable DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_AD.Reset();
          DataTable_AD.Columns.Add("Error", typeof(string));
          DataTable_AD.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            DataTable_AD.Reset();

            DirectoryEntry DirectoryEntry_AD;
            using (DirectoryEntry_AD = new DirectoryEntry())
            {
              DirectoryEntry_AD.Path = "LDAP://DC=lhc,DC=local";
              DirectoryEntry_AD.AuthenticationType = AuthenticationTypes.Secure;
              DirectorySearcher DirectorySearcher_AD;
              using (DirectorySearcher_AD = new DirectorySearcher(DirectoryEntry_AD))
              {
                DirectorySearcher_AD.SearchScope = SearchScope.Subtree;
                DirectorySearcher_AD.ReferralChasing = ReferralChasingOption.All;

                string SearchFilters = "";
                string SearchFilters_UserName = "";
                string SearchFilters_DisplayName = "";
                string SearchFilters_EmployeeNumber = "";
                string SearchFilters_Email = "";

                if (!string.IsNullOrEmpty(NewUserName))
                {
                  SearchFilters_UserName = "(samAccountName=" + NewUserName + ")";
                }

                SearchFilters = SearchFilters_UserName + SearchFilters_DisplayName + SearchFilters_EmployeeNumber + SearchFilters_Email;

                DirectorySearcher_AD.Filter = string.Format(CultureInfo.CurrentCulture, "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)" + SearchFilters + ")");

                SearchResult SearchResult_AD = DirectorySearcher_AD.FindOne();
                SearchResultCollection SearchResultCollection_AD = DirectorySearcher_AD.FindAll();

                if (SearchResult_AD == null)
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("Error", typeof(string));
                  DataTable_AD.Rows.Add("No Employee Data");
                }
                else
                {
                  DataTable_AD.Reset();
                  DataTable_AD.Columns.Add("UserName", typeof(string));
                  DataTable_AD.Columns.Add("DisplayName", typeof(string));
                  DataTable_AD.Columns.Add("FirstName", typeof(string));
                  DataTable_AD.Columns.Add("LastName", typeof(string));
                  DataTable_AD.Columns.Add("EmployeeNumber", typeof(string));
                  DataTable_AD.Columns.Add("Email", typeof(string));
                  DataTable_AD.Columns.Add("ManagerUserName", typeof(string));

                  foreach (SearchResult SearchResult_All in SearchResultCollection_AD)
                  {
                    DirectoryEntry_AD = SearchResult_All.GetDirectoryEntry();

                    string AD_UserName = "";
                    string AD_DisplayName = "";
                    string AD_FirstName = "";
                    string AD_LastName = "";
                    string AD_EmployeeNumber = "";
                    string AD_Email = "";
                    string AD_ManagerUserName = "";

                    if (DirectoryEntry_AD.Properties.Contains("samAccountName") == true)
                    {
                      AD_UserName = DirectoryEntry_AD.Properties["samAccountName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("displayName") == true)
                    {
                      AD_DisplayName = DirectoryEntry_AD.Properties["displayName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("givenName") == true)
                    {
                      AD_FirstName = DirectoryEntry_AD.Properties["givenName"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("sn") == true)
                    {
                      AD_LastName = DirectoryEntry_AD.Properties["sn"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("employeeID") == true)
                    {
                      AD_EmployeeNumber = DirectoryEntry_AD.Properties["employeeID"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("mail") == true)
                    {
                      AD_Email = DirectoryEntry_AD.Properties["mail"][0].ToString();
                    }

                    if (DirectoryEntry_AD.Properties.Contains("manager") == true)
                    {
                      AD_ManagerUserName = DirectoryEntry_AD.Properties["manager"][0].ToString();
                      int IndexAD_ManagerUserName = AD_ManagerUserName.IndexOfAny(new char[] { ',' });
                      int LengthAD_ManagerUserName = AD_ManagerUserName.Length;

                      AD_ManagerUserName = AD_ManagerUserName.Remove(IndexAD_ManagerUserName, LengthAD_ManagerUserName - IndexAD_ManagerUserName);
                      AD_ManagerUserName = AD_ManagerUserName.Remove(0, 3);

                      AD_ManagerUserName = "LHC\\" + AD_ManagerUserName.ToString();
                    }

                    DataTable_AD.Rows.Add(AD_UserName.ToLower(CultureInfo.CurrentCulture), AD_DisplayName, AD_FirstName, AD_LastName, AD_EmployeeNumber, AD_Email.ToLower(CultureInfo.CurrentCulture), AD_ManagerUserName.ToLower(CultureInfo.CurrentCulture));
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_AD.Reset();
              DataTable_AD.Columns.Add("Error", typeof(string));
              DataTable_AD.Rows.Add("Employee data could not be retrieved from AD, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_AD;
      }
    }


    public static DataTable DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(string employeeNumber)
    {
      //COLUMNS
      //KnownAsName + ',' +	Surname AS DisplayName
      //EmployeeNo AS EmployeeNumber

      string NewEmployeeNumber = ValidateParameter(employeeNumber);

      DataTable DataTable_Vision_FindDisplayName_SearchEmployeeNumber;
      using (DataTable_Vision_FindDisplayName_SearchEmployeeNumber = new DataTable())
      {
        DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Reset();
          DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
          DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            string SQLStringVision = "SELECT KnownAsName + ',' +	Surname AS DisplayName, EmployeeNo AS EmployeeNumber FROM MAS_EmpListing WHERE EmployeeNo = @EmployeeNo";
            string SQLConnectionVision = InfoQuest_Connections.Connections("EmployeeDetailVision");

            if (string.IsNullOrEmpty(SQLConnectionVision))
            {
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Rows.Add("No Employee Data Connection String");
            }
            else
            {
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Columns.Add("DisplayName", typeof(string));
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Columns.Add("EmployeeNumber", typeof(string));

              using (SqlCommand SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber = new SqlCommand(SQLStringVision))
              {
                SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber.Parameters.AddWithValue("@EmployeeNo", NewEmployeeNumber);

                using (SqlConnection SQLConnection_Vision_FindDisplayName_SearchEmployeeNumber = new SqlConnection(SQLConnectionVision))
                {
                  using (SqlDataAdapter SqlDataAdapter_Vision_FindDisplayName_SearchEmployeeNumber = new SqlDataAdapter())
                  {
                    foreach (SqlParameter SqlParameter_Value in SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber.Parameters)
                    {
                      if (SqlParameter_Value.Value == null)
                      {
                        SqlParameter_Value.Value = DBNull.Value;
                      }
                    }

                    SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber.CommandType = CommandType.Text;
                    SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber.Connection = SQLConnection_Vision_FindDisplayName_SearchEmployeeNumber;
                    SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber.CommandTimeout = 600;
                    SQLConnection_Vision_FindDisplayName_SearchEmployeeNumber.Open();
                    SqlDataAdapter_Vision_FindDisplayName_SearchEmployeeNumber.SelectCommand = SqlCommand_Vision_FindDisplayName_SearchEmployeeNumber;
                    SqlDataAdapter_Vision_FindDisplayName_SearchEmployeeNumber.Fill(DataTable_Vision_FindDisplayName_SearchEmployeeNumber);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindDisplayName_SearchEmployeeNumber.Rows.Add("Employee data could not be retrieved from Vision, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_Vision_FindDisplayName_SearchEmployeeNumber;
      }
    }

    public static DataTable DataEmployee_Vision_FindEmployeeInfo_SearchEmployeeNumber(string employeeNumber)
    {
      //COLUMNS
      //EmployeeNo AS EmployeeNumber
      //FirstName AS FirstName
      //Surname AS LastName
      //JobDescription AS JobTitle
      //BusUnit_Desc AS Department
      //PositionNo AS PositionNo
      //ReportsTo AS ReportsTo
      //EmployeeNo AS ManagerEmployeeNumber
      //FirstName + ' ' + Surname AS ManagerName

      string NewEmployeeNumber = ValidateParameter(employeeNumber);

      DataTable DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber;
      using (DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber = new DataTable())
      {
        DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Reset();
          DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
          DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            string SQLStringVision = @"SELECT	a.EmployeeNo AS EmployeeNumber ,
				                                      a.FirstName AS FirstName ,
				                                      a.Surname AS LastName ,
				                                      a.JobDescription AS JobTitle , 
				                                      b.BusUnit_Desc AS Department ,
				                                      a.PositionNo AS PositionNo ,
				                                      a.ReportsTo AS ReportsTo ,
				                                      c.EmployeeNo AS ManagerEmployeeNumber ,
				                                      c.FirstName + ' ' + c.Surname AS ManagerName
                                      FROM		MAS_ORG_PLU AS a
				                                      LEFT JOIN MAS_EmpListing AS b ON a.EmployeeNo = b.EmployeeNo
				                                      LEFT JOIN MAS_ORG_PLU AS c ON c.PositionNo = a.ReportsTo
                                      WHERE		a.EmployeeNo = @EmployeeNo
				                                      AND a.EmployeeNo != ''";
            string SQLConnectionVision = InfoQuest_Connections.Connections("EmployeeDetailVision");

            if (string.IsNullOrEmpty(SQLConnectionVision))
            {
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Rows.Add("No Employee Data Connection String");
            }
            else
            {
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("EmployeeNumber", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("FirstName", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("LastName", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("JobTitle", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("Department", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("PositionNo", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("ReportsTo", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("ManagerEmployeeNumber", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("ManagerName", typeof(string));

              using (SqlCommand SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber = new SqlCommand(SQLStringVision))
              {
                SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber.Parameters.AddWithValue("@EmployeeNo", NewEmployeeNumber);

                using (SqlConnection SQLConnection_Vision_FindEmployeeInfo_SearchEmployeeNumber = new SqlConnection(SQLConnectionVision))
                {
                  using (SqlDataAdapter SqlDataAdapter_Vision_FindEmployeeInfo_SearchEmployeeNumber = new SqlDataAdapter())
                  {
                    foreach (SqlParameter SqlParameter_Value in SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber.Parameters)
                    {
                      if (SqlParameter_Value.Value == null)
                      {
                        SqlParameter_Value.Value = DBNull.Value;
                      }
                    }

                    SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber.CommandType = CommandType.Text;
                    SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber.Connection = SQLConnection_Vision_FindEmployeeInfo_SearchEmployeeNumber;
                    SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber.CommandTimeout = 600;
                    SQLConnection_Vision_FindEmployeeInfo_SearchEmployeeNumber.Open();
                    SqlDataAdapter_Vision_FindEmployeeInfo_SearchEmployeeNumber.SelectCommand = SqlCommand_Vision_FindEmployeeInfo_SearchEmployeeNumber;
                    SqlDataAdapter_Vision_FindEmployeeInfo_SearchEmployeeNumber.Fill(DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber.Rows.Add("Employee data could not be retrieved from Vision, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_Vision_FindEmployeeInfo_SearchEmployeeNumber;
      }
    }

    public static DataTable DataEmployee_Vision_FindManagerEmployees_SearchEmployeeNumber(string employeeNumber)
    {
      //COLUMNS
      //EmployeeNo
      //Manager

      string NewEmployeeNumber = ValidateParameter(employeeNumber);

      DataTable DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber;
      using (DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber = new DataTable())
      {
        DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Reset();
          DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
          DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            string SQLStringVision = @"SELECT	EmployeeNo , 
				                                      Manager 
                                      FROM		(
					                                      SELECT	EmployeeNo , 
									                                      1 AS Manager
					                                      FROM		MAS_ORG_PLU
					                                      WHERE		EmployeeNo = @EmployeeNo
									                                      AND EmployeeNo != ''
				                                      UNION
					                                      SELECT	EmployeeNo , 
									                                      0
					                                      FROM		MAS_ORG_PLU
					                                      WHERE		ReportsTo IN (
										                                      SELECT	PositionNo
										                                      FROM		MAS_ORG_PLU
										                                      WHERE		EmployeeNo = @EmployeeNo
														                                      AND EmployeeNo != ''
									                                      ) 
									                                      AND EmployeeNo != ''
				                                      ) AS Temp";
            string SQLConnectionVision = InfoQuest_Connections.Connections("EmployeeDetailVision");

            if (string.IsNullOrEmpty(SQLConnectionVision))
            {
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Rows.Add("No Employee Data Connection String");
            }
            else
            {
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Columns.Add("EmployeeNo", typeof(string));
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Columns.Add("Manager", typeof(string));

              using (SqlCommand SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber = new SqlCommand(SQLStringVision))
              {
                SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber.Parameters.AddWithValue("@EmployeeNo", NewEmployeeNumber);

                using (SqlConnection SQLConnection_Vision_FindManagerEmployees_SearchEmployeeNumber = new SqlConnection(SQLConnectionVision))
                {
                  using (SqlDataAdapter SqlDataAdapter_Vision_FindManagerEmployees_SearchEmployeeNumber = new SqlDataAdapter())
                  {
                    foreach (SqlParameter SqlParameter_Value in SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber.Parameters)
                    {
                      if (SqlParameter_Value.Value == null)
                      {
                        SqlParameter_Value.Value = DBNull.Value;
                      }
                    }

                    SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber.CommandType = CommandType.Text;
                    SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber.Connection = SQLConnection_Vision_FindManagerEmployees_SearchEmployeeNumber;
                    SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber.CommandTimeout = 600;
                    SQLConnection_Vision_FindManagerEmployees_SearchEmployeeNumber.Open();
                    SqlDataAdapter_Vision_FindManagerEmployees_SearchEmployeeNumber.SelectCommand = SqlCommand_Vision_FindManagerEmployees_SearchEmployeeNumber;
                    SqlDataAdapter_Vision_FindManagerEmployees_SearchEmployeeNumber.Fill(DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Reset();
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Columns.Add("Error", typeof(string));
              DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber.Rows.Add("Employee data could not be retrieved from Vision, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_Vision_FindManagerEmployees_SearchEmployeeNumber;
      }
    }

    public static DataTable DataEmployee_Vision_TransparencyRegister_EmployeeManager(string transparencyRegisterEmployeeNumber, string transparencyRegisterManagerEmployeeNumber, string currentEmployeeNumber)
    {
      //COLUMNS
      //EmployeeManager

      string NewTransparencyRegisterEmployeeNumber = ValidateParameter(transparencyRegisterEmployeeNumber);
      string NewTransparencyRegisterManagerEmployeeNumber = ValidateParameter(transparencyRegisterManagerEmployeeNumber);
      string NewCurrentEmployeeNumber = ValidateParameter(currentEmployeeNumber);

      DataTable DataTable_Vision_TransparencyRegister_EmployeeManager;
      using (DataTable_Vision_TransparencyRegister_EmployeeManager = new DataTable())
      {
        DataTable_Vision_TransparencyRegister_EmployeeManager.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_Vision_TransparencyRegister_EmployeeManager.Reset();
          DataTable_Vision_TransparencyRegister_EmployeeManager.Columns.Add("Error", typeof(string));
          DataTable_Vision_TransparencyRegister_EmployeeManager.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            string SQLStringVision = @" SELECT	Employee.EmployeeNo AS EmployeeManager
                                        FROM		MAS_ORG_PLU Employee
                                                LEFT JOIN MAS_ORG_PLU Manager ON Employee.ReportsTo = Manager.PositionNo
                                        WHERE		Employee.EmployeeNo = @EmployeeNo_TransparencyRegisterEmployeeNumber
                                                AND Manager.EmployeeNo = @EmployeeNo_TransparencyRegisterManagerEmployeeNumber
                                                AND @EmployeeNo_TransparencyRegisterManagerEmployeeNumber = @EmployeeNo_CurrentEmployeeNumber
                                        UNION
                                        SELECT	Employee.EmployeeNo AS EmployeeManager
                                        FROM		MAS_ORG_PLU Employee
                                                LEFT JOIN MAS_ORG_PLU Manager ON Employee.ReportsTo = Manager.PositionNo
                                        WHERE		Employee.EmployeeNo = @EmployeeNo_TransparencyRegisterEmployeeNumber
                                                AND Manager.EmployeeNo = @EmployeeNo_CurrentEmployeeNumber
                                        UNION
                                        SELECT	CASE 
					                                        WHEN @EmployeeNo_TransparencyRegisterManagerEmployeeNumber = @EmployeeNo_CurrentEmployeeNumber THEN @EmployeeNo_TransparencyRegisterEmployeeNumber
				                                        END AS EmployeeManager
                                        WHERE		@EmployeeNo_TransparencyRegisterManagerEmployeeNumber = @EmployeeNo_CurrentEmployeeNumber";
            string SQLConnectionVision = InfoQuest_Connections.Connections("EmployeeDetailVision");

            if (string.IsNullOrEmpty(SQLConnectionVision))
            {
              DataTable_Vision_TransparencyRegister_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_EmployeeManager.Columns.Add("Error", typeof(string));
              DataTable_Vision_TransparencyRegister_EmployeeManager.Rows.Add("No Employee Data Connection String");
            }
            else
            {
              DataTable_Vision_TransparencyRegister_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_EmployeeManager.Columns.Add("EmployeeManager", typeof(string));

              using (SqlCommand SqlCommand_Vision_TransparencyRegister_EmployeeManager = new SqlCommand(SQLStringVision))
              {
                SqlCommand_Vision_TransparencyRegister_EmployeeManager.Parameters.AddWithValue("@EmployeeNo_TransparencyRegisterEmployeeNumber", NewTransparencyRegisterEmployeeNumber);
                SqlCommand_Vision_TransparencyRegister_EmployeeManager.Parameters.AddWithValue("@EmployeeNo_TransparencyRegisterManagerEmployeeNumber", NewTransparencyRegisterManagerEmployeeNumber);
                SqlCommand_Vision_TransparencyRegister_EmployeeManager.Parameters.AddWithValue("@EmployeeNo_CurrentEmployeeNumber", NewCurrentEmployeeNumber);

                using (SqlConnection SQLConnection_Vision_TransparencyRegister_EmployeeManager = new SqlConnection(SQLConnectionVision))
                {
                  using (SqlDataAdapter SqlDataAdapter_Vision_TransparencyRegister_EmployeeManager = new SqlDataAdapter())
                  {
                    foreach (SqlParameter SqlParameter_Value in SqlCommand_Vision_TransparencyRegister_EmployeeManager.Parameters)
                    {
                      if (SqlParameter_Value.Value == null)
                      {
                        SqlParameter_Value.Value = DBNull.Value;
                      }
                    }

                    SqlCommand_Vision_TransparencyRegister_EmployeeManager.CommandType = CommandType.Text;
                    SqlCommand_Vision_TransparencyRegister_EmployeeManager.Connection = SQLConnection_Vision_TransparencyRegister_EmployeeManager;
                    SqlCommand_Vision_TransparencyRegister_EmployeeManager.CommandTimeout = 600;
                    SQLConnection_Vision_TransparencyRegister_EmployeeManager.Open();
                    SqlDataAdapter_Vision_TransparencyRegister_EmployeeManager.SelectCommand = SqlCommand_Vision_TransparencyRegister_EmployeeManager;
                    SqlDataAdapter_Vision_TransparencyRegister_EmployeeManager.Fill(DataTable_Vision_TransparencyRegister_EmployeeManager);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_Vision_TransparencyRegister_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_EmployeeManager.Columns.Add("Error", typeof(string));
              DataTable_Vision_TransparencyRegister_EmployeeManager.Rows.Add("Employee data could not be retrieved from Vision, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_Vision_TransparencyRegister_EmployeeManager;
      }
    }

    public static DataTable DataEmployee_Vision_TransparencyRegister_List_EmployeeManager(string currentEmployeeNumber)
    {
      //COLUMNS
      //EmployeeNo
      //EmployeeManager

      string NewCurrentEmployeeNumber = ValidateParameter(currentEmployeeNumber);

      DataTable DataTable_Vision_TransparencyRegister_List_EmployeeManager;
      using (DataTable_Vision_TransparencyRegister_List_EmployeeManager = new DataTable())
      {
        DataTable_Vision_TransparencyRegister_List_EmployeeManager.Locale = CultureInfo.CurrentCulture;
        if (EmptyParameter == "Yes")
        {
          DataTable_Vision_TransparencyRegister_List_EmployeeManager.Reset();
          DataTable_Vision_TransparencyRegister_List_EmployeeManager.Columns.Add("Error", typeof(string));
          DataTable_Vision_TransparencyRegister_List_EmployeeManager.Rows.Add("No Employee Data");
        }
        else
        {
          try
          {
            string SQLStringVision = @"SELECT	EmployeeNo ,
				                                      EmployeeManager 
                                      FROM		(
					                                      SELECT	EmployeeNo , 
									                                      2 AS EmployeeManager
					                                      FROM		MAS_ORG_PLU
					                                      WHERE		EmployeeNo = @EmployeeNo
									                                      AND EmployeeNo != ''
				                                      UNION
					                                      SELECT	EmployeeNo , 
									                                      1 AS EmployeeManager
					                                      FROM		MAS_ORG_PLU
					                                      WHERE		ReportsTo IN (
										                                      SELECT	PositionNo
										                                      FROM		MAS_ORG_PLU
										                                      WHERE		EmployeeNo = @EmployeeNo
														                                      AND EmployeeNo != ''
									                                      ) 
									                                      AND EmployeeNo != ''
				                                      ) AS Temp";
            string SQLConnectionVision = InfoQuest_Connections.Connections("EmployeeDetailVision");

            if (string.IsNullOrEmpty(SQLConnectionVision))
            {
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Columns.Add("Error", typeof(string));
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Rows.Add("No Employee Data Connection String");
            }
            else
            {
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Columns.Add("EmployeeNo", typeof(string));
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Columns.Add("EmployeeManager", typeof(string));

              using (SqlCommand SqlCommand_Vision_TransparencyRegister_List_EmployeeManager = new SqlCommand(SQLStringVision))
              {
                SqlCommand_Vision_TransparencyRegister_List_EmployeeManager.Parameters.AddWithValue("@EmployeeNo", NewCurrentEmployeeNumber);

                using (SqlConnection SQLConnection_Vision_TransparencyRegister_List_EmployeeManager = new SqlConnection(SQLConnectionVision))
                {
                  using (SqlDataAdapter SqlDataAdapter_Vision_TransparencyRegister_List_EmployeeManager = new SqlDataAdapter())
                  {
                    foreach (SqlParameter SqlParameter_Value in SqlCommand_Vision_TransparencyRegister_List_EmployeeManager.Parameters)
                    {
                      if (SqlParameter_Value.Value == null)
                      {
                        SqlParameter_Value.Value = DBNull.Value;
                      }
                    }

                    SqlCommand_Vision_TransparencyRegister_List_EmployeeManager.CommandType = CommandType.Text;
                    SqlCommand_Vision_TransparencyRegister_List_EmployeeManager.Connection = SQLConnection_Vision_TransparencyRegister_List_EmployeeManager;
                    SqlCommand_Vision_TransparencyRegister_List_EmployeeManager.CommandTimeout = 600;
                    SQLConnection_Vision_TransparencyRegister_List_EmployeeManager.Open();
                    SqlDataAdapter_Vision_TransparencyRegister_List_EmployeeManager.SelectCommand = SqlCommand_Vision_TransparencyRegister_List_EmployeeManager;
                    SqlDataAdapter_Vision_TransparencyRegister_List_EmployeeManager.Fill(DataTable_Vision_TransparencyRegister_List_EmployeeManager);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (!string.IsNullOrEmpty(ex.ToString()))
            {
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Reset();
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Columns.Add("Error", typeof(string));
              DataTable_Vision_TransparencyRegister_List_EmployeeManager.Rows.Add("Employee data could not be retrieved from Vision, Please try again later");
            }
            else
            {
              throw;
            }
          }
        }

        return DataTable_Vision_TransparencyRegister_List_EmployeeManager;
      }
    }
  }
}