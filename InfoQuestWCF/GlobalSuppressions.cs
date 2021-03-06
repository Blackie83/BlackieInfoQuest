// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

#region CA1020
//--BEGIN-- --CA1020--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "InfoQuestWCF")]
//---END--- --CA1020--//
#endregion

#region CA1502
//--BEGIN-- --CA1502--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member", Target = "InfoQuestWCF.Services_MHS.#PXM_Event_CheckGetEventData_ProcessData(System.String)")]
//---END--- --CA1502--//
#endregion

#region CA1505
//--BEGIN-- --CA1505--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDCU_Web()")]
//---END--- --CA1505--//
#endregion

#region CA1506
//--BEGIN-- --CA1506--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Event_CreateFile_AutomatedMissing(System.String,System.String)")]
//---END--- --CA1506--//
#endregion

#region CA1702
//--BEGIN-- --CA1702--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_PROVIDER_WINNT35")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_PROVIDER_WINNT50")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_PROVIDER_DEFAULT")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_PROVIDER_WINNT40")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_INTERACTIVE")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_NETWORK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_BATCH")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_SERVICE")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_UNLOCK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_NETWORK_CLEARTEXT")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#LOGON32_LOGON_NEW_CREDENTIALS")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON", Scope = "member", Target = "InfoQuestWCF.InfoQuest_Impersonate.#ERROR_LOGON_FAILURE")]
//---END--- --CA1702--//
#endregion

#region CA1822
//--BEGIN-- --CA1822--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_Email()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_Email()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDRM_USSD()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDMH_USSD()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_Email()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "InfoQuestWCF.Services_InfoQuest.#PXM_PDCH_Escalation_ImportData_CRMComment_PDMM_USSD()")]
//---END--- --CA1822--//
#endregion

#region CA2101
//--BEGIN-- --CA2101--//
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0", Scope = "member", Target = "InfoQuestWCF.NativeMethods.#LogonUserA(System.String,System.String,System.String,System.Int32,System.Int32,System.IntPtr&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "1", Scope = "member", Target = "InfoQuestWCF.NativeMethods.#LogonUserA(System.String,System.String,System.String,System.Int32,System.Int32,System.IntPtr&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "2", Scope = "member", Target = "InfoQuestWCF.NativeMethods.#LogonUserA(System.String,System.String,System.String,System.Int32,System.Int32,System.IntPtr&)")]
//---END--- --CA2101--//
#endregion

