using System;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace InfoQuestWCF
{
  public static class InfoQuest_Impersonate
  {
    public const int SECURITY_IMPERSONATION_LEVEL_SecurityAnonymous = 0;
    public const int SECURITY_IMPERSONATION_LEVEL_SecurityIdentification = 1;
    public const int SECURITY_IMPERSONATION_LEVEL_SecurityImpersonation = 2;
    public const int SECURITY_IMPERSONATION_LEVEL_SecurityDelegation = 3;

    public const int LOGON32_PROVIDER_DEFAULT = 0;
    public const int LOGON32_PROVIDER_WINNT35 = 1;
    public const int LOGON32_PROVIDER_WINNT40 = 2;
    public const int LOGON32_PROVIDER_WINNT50 = 3;

    public const int LOGON32_LOGON_INTERACTIVE = 2;
    public const int LOGON32_LOGON_NETWORK = 3;
    public const int LOGON32_LOGON_BATCH = 4;
    public const int LOGON32_LOGON_SERVICE = 5;
    public const int LOGON32_LOGON_UNLOCK = 7;
    public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
    public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

    public const int ERROR_LOGON_FAILURE = 1326;

    private static WindowsImpersonationContext WindowsImpersonationContext_Impersonate;


    public static bool ImpersonateUser(string userName, string domain, string password)
    {
      WindowsIdentity WindowsIdentity_Temp;
      IntPtr IntPtr_Token = IntPtr.Zero;
      IntPtr IntPtr_TokenDuplicate = IntPtr.Zero;

      if (NativeMethods.RevertToSelf())
      {
        if (NativeMethods.LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref IntPtr_Token) != 0)
        {
          if (NativeMethods.DuplicateToken(IntPtr_Token, 2, ref IntPtr_TokenDuplicate) != 0)
          {
            using (WindowsIdentity_Temp = new WindowsIdentity(IntPtr_TokenDuplicate))
            {
              WindowsImpersonationContext_Impersonate = WindowsIdentity_Temp.Impersonate();
              if (WindowsImpersonationContext_Impersonate != null)
              {
                NativeMethods.CloseHandle(IntPtr_Token);
                NativeMethods.CloseHandle(IntPtr_TokenDuplicate);
                return true;
              }
            }
          }
        }
      }

      if (IntPtr_Token != IntPtr.Zero)
      {
        NativeMethods.CloseHandle(IntPtr_Token);
      }

      if (IntPtr_TokenDuplicate != IntPtr.Zero)
      {
        NativeMethods.CloseHandle(IntPtr_TokenDuplicate);
      }

      return false;
    }

    public static void UndoImpersonation()
    {
      WindowsImpersonationContext_Impersonate.Undo();
    }
  }

  internal static class NativeMethods
  {
    [DllImport("advapi32.dll")]
    internal static extern int LogonUserA(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool RevertToSelf();

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseHandle(IntPtr handle);
  }
}