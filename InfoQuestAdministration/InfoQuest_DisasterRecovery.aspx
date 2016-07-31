<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoQuest_DisasterRecovery.aspx.cs" Inherits="InfoQuestAdministration.InfoQuest_DisasterRecovery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Disaster Recovery</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_DisasterRecovery.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_DisasterRecovery" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_DisasterRecovery" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_DisasterRecovery" AssociatedUpdatePanelID="UpdatePanel_DisasterRecovery">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_DisasterRecovery" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">Info<strong style="color: #b0262e">Q</strong>uest Disaster Recovery
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>InfoQuest</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody" style="width: 100%">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from InfoQuest<br />
                      If data is not returned see if database user does have access on the server and database for this connection string
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_InfoQuestConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">System Administrator
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <table class="Table_Body">
                        <tr>
                          <td>Total Records:
                            <asp:Label ID="Label_InfoQuestSystemAdministratorTotalRecords" runat="server" Text=""></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td style="padding: 0px;">
                            <asp:GridView ID="GridView_InfoQuestSystemAdministrator" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_InfoQuestSystemAdministrator" CssClass="GridView" AllowPaging="False" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_InfoQuestSystemAdministrator_PreRender" OnRowCreated="GridView_InfoQuestSystemAdministrator_RowCreated">
                              <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                              <PagerTemplate>
                                <table class="GridView_PagerStyle">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </PagerTemplate>
                              <RowStyle CssClass="GridView_RowStyle" />
                              <FooterStyle CssClass="GridView_FooterStyle" />
                              <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                              <EmptyDataTemplate>
                                <table class="GridView_EmptyDataStyle">
                                  <tr>
                                    <td colspan="2">No System Administrator
                                    </td>
                                  </tr>
                                  <tr class="GridView_EmptyDataStyle_FooterStyle">
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </EmptyDataTemplate>
                              <Columns>
                                <asp:BoundField DataField="SystemAdministrator" HeaderText="System Administrator" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="SystemAdministrator" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_InfoQuestSystemAdministrator" runat="server" SelectCommand="SELECT SystemAdministrator_Domain + '\' + SystemAdministrator_UserName AS SystemAdministrator FROM Administration_SystemAdministrator ORDER BY SystemAdministrator_Domain , SystemAdministrator_UserName" SelectCommandType="Text" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_InfoQuestSystemAdministrator_Selected"></asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">System Server
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <table class="Table_Body">
                        <tr>
                          <td>Total Records:
                            <asp:Label ID="Label_InfoQuestSystemServerTotalRecords" runat="server" Text=""></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td style="padding: 0px;">
                            <asp:GridView ID="GridView_InfoQuestSystemServer" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_InfoQuestSystemServer" CssClass="GridView" AllowPaging="False" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_InfoQuestSystemServer_PreRender" OnRowCreated="GridView_InfoQuestSystemServer_RowCreated">
                              <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                              <PagerTemplate>
                                <table class="GridView_PagerStyle">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </PagerTemplate>
                              <RowStyle CssClass="GridView_RowStyle" />
                              <FooterStyle CssClass="GridView_FooterStyle" />
                              <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                              <EmptyDataTemplate>
                                <table class="GridView_EmptyDataStyle">
                                  <tr>
                                    <td colspan="2">No System Server
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </EmptyDataTemplate>
                              <Columns>
                                <asp:BoundField DataField="SystemServer_Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="SystemServer_Description" />
                                <asp:BoundField DataField="SystemServer_Server" HeaderText="Server" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="SystemServer_Server" />
                                <asp:BoundField DataField="SystemServer_DNS_Alias" HeaderText="DNS Alias" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="SystemServer_DNS_Alias" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_InfoQuestSystemServer" runat="server" SelectCommand="SELECT SystemServer_Description , SystemServer_Server , SystemServer_DNS_Alias FROM Administration_SystemServer ORDER BY SystemServer_Description , SystemServer_Server , SystemServer_DNS_Alias" SelectCommandType="Text" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_InfoQuestSystemServer_Selected"></asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
            <hr style="height: 5px; width: 80%; background-color: #68c0ff; border: none;" />
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Employee Data</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody" style="width: 100%">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if Employee data is returned for UserName provided, if UserName is valid
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">UserName
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_EmployeeUserName" Width="300px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;&nbsp;
                      <asp:Label ID="Label_EmployeeUserNameInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_EmployeeClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_EmployeeClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_Employee" runat="server" Text="Get Employee Data" CssClass="Controls_Button" OnClick="Button_Employee_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Active Directory
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from Active Directory for UserName provided, if UserName is valid
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Error
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADError" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">UserName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADUserName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">DisplayName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADDisplayName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">FirstName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADFirstName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">LastName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADLastName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">EmployeeNumber
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADEmployeeNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Email
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADEmail" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">ManagerUserName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeADManagerUserName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Vision : Using employee number from Active Directory
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from Vision for UserName provided, if UserName is valid<br />
                      If data is not returned see if database user does have access on the server and database for this connection string
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_EmployeeVisionConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Error
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeVisionError" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">DisplayName
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeVisionDisplayName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">EmployeeNumber
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmployeeVisionEmployeeNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
            <hr style="height: 5px; width: 80%; background-color: #c3c3c3; border: none;" />
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Patient Data</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from EDW for Facility and Visit Number provided, if Visit Number is valid
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_PatientEDWConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Facility<br />
                      (Brenthurst Clinic (10))
                    </td>
                    <td style="width: 750px;">
                      <asp:DropDownList ID="DropDownList_PatientEDWFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PatientEDWFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PatientEDWFacility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Active ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientEDWFacilityInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Visit Number<br />
                      (429374)
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_PatientEDWVisitNumber" Width="300px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientEDWVisitNumberInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_PatientEDWClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_PatientEDWClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_PatientEDW" runat="server" Text="Get EDW Patient Data" CssClass="Controls_Button" OnClick="Button_PatientEDW_Click" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - Patient Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Patient_Information_sp @FacilityCode , @VisitNumber 
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWPatientInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWPatientInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWPatientInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW Patient Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - Visit Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.Visit_Information_sp @FacilityCode , @VisitNumber 
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWVisitInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWVisitInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWVisitInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW Visit Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - Theatre Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.Theatre_Information_sp @FacilityCode , @VisitNumber 
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWTheatreInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWTheatreInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWTheatreInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW Theatre Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - IPS Visit Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Visit_Information_sp @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWIPSVisitInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWIPSVisitInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWIPSVisitInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW IPS Visit Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - IPS Theatre Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Theatre_Information_sp @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWIPSTheatreInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWIPSTheatreInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWIPSTheatreInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW IPS Theatre Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - IPS Coding Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Coding_Information_sp @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWIPSCodingInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWIPSCodingInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWIPSCodingInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW IPS Coding Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - IPS Accommodation Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Accommodation_Information_sp @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWIPSAccommodationInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWIPSAccommodationInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWIPSAccommodationInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW IPS Accommodation Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">EDW - IPS Antibiotic Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE InfoQuest.IPS_Antibiotic_Information_sp @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientEDWIPSAntibioticInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientEDWIPSAntibioticInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientEDWIPSAntibioticInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No EDW IPS Antibiotic Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #c3c3c3; border: none;" />
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Patient Data</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from ODS for Facility and Visit Number provided, if Visit Number is valid
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_PatientODSConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Facility<br />
                      (Brenthurst Clinic (10))
                    </td>
                    <td style="width: 750px;">
                      <asp:DropDownList ID="DropDownList_PatientODSFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PatientODSFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PatientODSFacility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Active ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSFacilityInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Visit Number<br />
                      (429374)
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_PatientODSVisitNumber" Width="300px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSVisitNumberInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_PatientODSClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_PatientODSClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_PatientODS" runat="server" Text="Get ODS Patient Data" CssClass="Controls_Button" OnClick="Button_PatientODS_Click" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Patient Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.PatientInformationSP @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSPatientInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSPatientInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSPatientInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Patient Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Visit Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.VisitInformationSP @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSVisitInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSVisitInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSVisitInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Visit Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Coding Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.CodingInformationSP @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSCodingInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSCodingInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSCodingInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Coding Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Accommodation Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.AccommodationInformationSP @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSAccommodationInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSAccommodationInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSAccommodationInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Accommodation Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Practitioner Information
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.PractitionerInformationSP @FacilityCode , @VisitNumber
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSPractitionerInformationTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSPractitionerInformation" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSPractitionerInformation_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Practitioner Information Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Patient Data</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from ODS for Facility, Start Date and End Date provided, if Facility and Dates are valid
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_PatientODSPXMPostDischargeSurveyConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS PXM Post Discharge Survey
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Facility<br />
                      (Brenthurst Clinic (10))
                    </td>
                    <td style="width: 750px;">
                      <asp:DropDownList ID="DropDownList_PatientODSPXMPostDischargeSurveyFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PatientODSPXMPostDischargeSurveyFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                        <asp:ListItem Value="All">All Facilities</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PatientODSPXMPostDischargeSurveyFacility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Active ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSPXMPostDischargeSurveyFacilityInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Start Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_PatientODSPXMPostDischargeSurveyStartDate" Width="75px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_PatientODSPXMPostDischargeSurveyStartDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_PatientODSPXMPostDischargeSurveyStartDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_PatientODSPXMPostDischargeSurveyStartDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_PatientODSPXMPostDischargeSurveyStartDate">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_PatientODSPXMPostDischargeSurveyStartDate" runat="server" TargetControlID="TextBox_PatientODSPXMPostDischargeSurveyStartDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">End Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_PatientODSPXMPostDischargeSurveyEndDate" Width="75px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_PatientODSPXMPostDischargeSurveyEndDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_PatientODSPXMPostDischargeSurveyEndDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_PatientODSPXMPostDischargeSurveyEndDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_PatientODSPXMPostDischargeSurveyEndDate">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_PatientODSPXMPostDischargeSurveyEndDate" runat="server" TargetControlID="TextBox_PatientODSPXMPostDischargeSurveyEndDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_PatientODSPXMPostDischargeSurveyClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_PatientODSPXMPostDischargeSurveyClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_PatientODSPXMPostDischargeSurvey" runat="server" Text="Get ODS Patient Data" CssClass="Controls_Button" OnClick="Button_PatientODSPXMPostDischargeSurvey_Click" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - PXM Post Discharge Survey
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE Nursing.VOCDischargeData @startDate , @endDate , @HospitalUnitId
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSPXMPostDischargeSurveyTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSPXMPostDischargeSurvey" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSPXMPostDischargeSurvey_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS PXM Post Discharge Survey Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Patient Data</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if data is returned from ODS for Facility and Patient provided, if Facility is valid
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Connection String
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_PatientODSPatientSearchConnectionString" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS Patient Search
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Facility<br />
                      (Brenthurst Clinic (10))
                    </td>
                    <td style="width: 750px;">
                      <asp:DropDownList ID="DropDownList_PatientODSPatientSearchFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PatientODSPatientSearchFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                        <asp:ListItem Value="All">All Facilities</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PatientODSPatientSearchFacility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Active ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSPatientSearchFacilityInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Patient<br />
                      (Swart)
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_PatientODSPatientSearchPatient" Width="300px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      &nbsp;&nbsp;<asp:Label ID="Label_PatientODSPatientSearchPatientInvalidMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_PatientODSPatientSearchClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_PatientODSPatientSearchClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_PatientODSPatientSearch" runat="server" Text="Get ODS Patient Data" CssClass="Controls_Button" OnClick="Button_PatientODSPatientSearch_Click" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">ODS - Patient Search
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Query
                    </td>
                    <td style="width: 100%; background-color: #77cf9c; color: #333333;">EXECUTE ODS_Reports.InfoQuest.PatientSearchSP @FacilityCode , @Patient
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Records: <asp:Label ID="Label_PatientODSPatientSearchTotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_PatientODSPatientSearch" runat="server" Width="900px" CssClass="GridView" OnRowDataBound="GridView_PatientODSPatientSearch_RowDataBound">
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="2">No ODS Patient Search Data
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
            <hr style="height: 5px; width: 80%; background-color: #77cf9c; border: none;" />
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Email :  Internal and External Sending</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody" style="width: 100%">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if email can be sent to Internal and External Email Addresses, if Email Address is valid<br />
                      If email is not send see if server is given permission on Exchange to send out internal and external email from that server
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Server
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_EmailServer" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Email
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Email Address
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_Email" Width="300px" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Email Description
                    </td>
                    <td style="width: 750px;">
                      <asp:TextBox ID="TextBox_EmailDescription" Width="700px" TextMode="MultiLine" Rows="4" runat="server" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_EmailClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_EmailClear_Click" />&nbsp;&nbsp;
                      <asp:Button ID="Button_Email" runat="server" Text="Send Email" CssClass="Controls_Button" OnClick="Button_Email_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Email Send
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Error
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmailError" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Email Address
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmailAddress" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Email Description
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmailDescription" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Success
                    </td>
                    <td style="width: 750px;">
                      <asp:Label ID="Label_EmailSuccess" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
            <hr style="height: 5px; width: 80%; background-color: #d46e6e; border: none;" />
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 910px;">
            <tr>
              <td style="background-color: #d46e6e; width: 5px;">&nbsp;</td>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>File : Uploading and Retrieving</td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #d46e6e; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td style="background-color: #d46e6e; width: 5px;">&nbsp;</td>
              <td>
                <table class="FormView_TableBody" style="width: 100%">
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Test
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">See if file can be uploaded and retrieved<br />
                      If a file cannot be uploaded see if the folder exist and has the correct permissions
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Folder
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">
                      <asp:Label ID="Label_File_Folder" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px; background-color: #77cf9c; color: #333333;">Permissions
                    </td>
                    <td style="width: 750px; background-color: #77cf9c; color: #333333;">On Folder Security add "Everyone" with permissions to "Modify, Read & execute, List folder contents, Read and Write"
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">File Upload
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">When uploading a document<br />
                      Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff), TXT (.txt), Outlook (.msg), Images (.jpeg / .jpg / .gif / .png)<br />
                      Only files smaller then 5 MB can be uploaded
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:FileUpload ID="FileUpload_FileUpload" runat="server" CssClass="Controls_FileUpload" Width="800px" />
                    </td>
                  </tr>
                  <tr>
                    <td style="vertical-align: middle; text-align: center;" colspan="2">
                      <span style="color: #77cf9c"></span>
                      <span style="color: #d46e6e"></span>
                      <asp:Label ID="Label_FileUpload" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_FileUpload" runat="server" Text="Upload File" CssClass="Controls_Button" OnClick="Button_FileUpload_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">File List
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">Total Files:&nbsp;<asp:Label ID="Label_TotalFiles" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="FormView_TableBodyHeaderWhite" colspan="2">File Name
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_FileUploadedFiles" runat="server" Text=""></asp:Label>
                      <asp:BulletedList ID="BulletedList_FileUploadedFiles" runat="server" DisplayMode="HyperLink"></asp:BulletedList>
                    </td>
                  </tr>
                  <tr>
                    <td style="vertical-align: middle; text-align: center;" colspan="2">
                      <asp:Label ID="Label_FileDelete" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="FormView_TableBodyHeaderWhite" colspan="2">
                      <asp:Button ID="Button_FileDeleteAll" runat="server" CssClass="Controls_Button" Text="Delete All Files" OnClick="Button_FileDeleteAll_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #d46e6e; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <asp:LinkButton ID="LinkButton_File" runat="server"></asp:LinkButton>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
