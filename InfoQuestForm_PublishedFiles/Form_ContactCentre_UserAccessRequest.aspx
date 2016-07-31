<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_ContactCentre_UserAccessRequest" CodeBehind="Form_ContactCentre_UserAccessRequest.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Contact Centre User Access Request</title>
  <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
</head>
<body style="margin-top: 0px;">
  <form id="form_ContactCentre_UserAccessRequest" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div style="max-width: 700px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ContactCentre_UserAccessRequest" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ContactCentre_UserAccessRequest" AssociatedUpdatePanelID="UpdatePanel_ContactCentre_UserAccessRequest">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ContactCentre_UserAccessRequest" runat="server">
        <ContentTemplate>
          <table style="width:700px; padding:0px; border-spacing:0px;" border="0">
            <tr>
              <td>
                <img alt="" src="App_Images/Logos/Life Healthcare/14_logo_1_col_black.jpg" style="border: 0; height: 50px" />
              </td>
              <td style="width: 25px"></td>
              <td style="color: #000000; font-size: 14px; font-weight: bold; font-family: Verdana; padding-top: 15px; padding-bottom: 10px">InfoQuest User Access Request
              </td>
              <td style="width: 25px"></td>
              <td style="text-align:right;">
                <asp:Button ID="Button_New" runat="server" Text="New Request" Font-Names="Verdana" Font-Size="11px" OnClick="Button_New_Click" />&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table style="width:700px; padding:0px; border-spacing:0px;" border="0">
            <tr>
              <td>
                <table style="width:100%; padding:0px; border-spacing:0px;">
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 0px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;" colspan="2">
                      <asp:Button ID="Button_PrintTop" runat="server" Text="Print" Font-Names="Verdana" Font-Size="11px" OnClick="Button_Print_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 1px; border-bottom-width: 1px; border-style: solid;">Facility:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; border-style: solid;">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" Font-Names="Verdana" Font-Size="11px" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                        <asp:ListItem Value="0">All Facilities (Form Owners and Form Administrators)</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility" runat="server"></asp:SqlDataSource>
                      <asp:Label ID="Label_Facility" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Requested By:&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Name and Surname:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">
                      <asp:Label ID="Label_RequestedByName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Email:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">
                      <asp:Label ID="Label_RequestedByEmail" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Person to be given access:&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Name and Surname:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Network ID (Username):&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Employee Number:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Contact Number:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Does this person replace someone:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; padding: 0px;">
                      <table style="height: 30px; width:100%; padding:0px; border-spacing:0px;" border="0">
                        <tr>
                          <td style="width: 50%; border-color: Black; border-left-width: 0px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;">Yes</td>
                          <td style="width: 50%; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;">No</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">If yes, Network ID (Username) of person being replaced:
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; vertical-align:top;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Has the person been transferred from another facility:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; padding: 0px;">
                      <table style="height: 40px; width:100%; padding:0px; border-spacing:0px;" border="0">
                        <tr>
                          <td style="width: 50%; border-color: Black; border-left-width: 0px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;">Yes</td>
                          <td style="width: 50%; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;">No</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">If yes, Facility transferred from:
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; vertical-align:top;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Access&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">InfoQuest security roles the user should have access to - Indicate with a X in the access column&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">Security Roles:&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; padding: 0px;" colspan="2">
                      <asp:GridView ID="GridView_ContactCentre_UserAccessRequest_SecurityRole" runat="server" CellPadding="3" Font-Names="Verdana" RowStyle-BorderColor="#000000" RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole" ShowHeaderWhenEmpty="true" PageSize="1000">
                        <EmptyDataTemplate>
                          <table style="padding:0px; border-spacing:0px;">
                            <tr>
                              <td>No Security Roles, Please select a facility
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="SecurityRole_Name" HeaderText="Security Role" ReadOnly="True" SortExpression="SecurityRole_Name" ItemStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                          <asp:TemplateField HeaderText="Description" SortExpression="SecurityRole_Description" ItemStyle-Width="400px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                              <asp:Label ID="Label_Description" runat="server" Text='<%#Eval("SecurityRole_Description")%>'></asp:Label>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Access" SortExpression="Form_Name" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                              <asp:HiddenField ID="HiddenField_Form" runat="server" Value='<%#Eval("Form_Name")%>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Authorized By:&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Name and Surname:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Position:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">Date:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; width: 200px; height: 30px; border-color: Black; border-left-width: 1px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; vertical-align:top;">Signature:&nbsp;
                    </td>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: normal; width: 500px; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid;" colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Please email the signed request to the Contact Centre (ContactCentre@lifehealthcare.co.za)&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 1px; border-right-width: 1px; border-top-width: 0px; border-bottom-width: 1px; border-style: solid; text-align:center;" colspan="2">Please obtain authorization from all relevant parties on the blocks above before forwarding the form to Contact centre.
                    </td>
                  </tr>
                  <tr>
                    <td style="font-family: Verdana; font-size: 11px; font-weight: bold; border-color: Black; border-left-width: 0px; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-style: solid; text-align:center;" colspan="2">
                      <asp:Button ID="Button_PrintBottom" runat="server" Text="Print" Font-Names="Verdana" Font-Size="11px" OnClick="Button_Print_Click" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </form>
</body>
</html>
