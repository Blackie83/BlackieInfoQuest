<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_SystemServer.aspx.cs" Inherits="InfoQuestAdministration.Administration_SystemServer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - System Server</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SystemServer.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SystemServer" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SystemServer" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SystemServer" AssociatedUpdatePanelID="UpdatePanel_SystemServer">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SystemServer" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>System Server</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SystemServer_Form" runat="server" DataKeyNames="SystemServer_Id" CssClass="FormView" DataSourceID="SqlDataSource_SystemServer_Form" OnItemInserting="FormView_SystemServer_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SystemServer_Form_ItemCommand" OnItemUpdating="FormView_SystemServer_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Id
                        </td>
                        <td>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("SystemServer_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormServer">Server
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertServer" runat="server" Width="500px" Text='<%# Bind("SystemServer_Server") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDNSAlias">DNS Alias
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDNSAlias" runat="server" Width="500px" Text='<%# Bind("SystemServer_DNS_Alias") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SystemServer_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SystemServer_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SystemServer_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SystemServer_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SystemServer_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add System Server" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Id
                        </td>
                        <td>
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SystemServer_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("SystemServer_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormServer">Server
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditServer" runat="server" Width="500px" Text='<%# Bind("SystemServer_Server") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDNSAlias">DNS Alias
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDNSAlias" runat="server" Width="500px" Text='<%# Bind("SystemServer_DNS_Alias") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SystemServer_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SystemServer_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SystemServer_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SystemServer_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SystemServer_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update System Server" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SystemServer_Form" runat="server" InsertCommand="INSERT INTO Administration_SystemServer (SystemServer_Description ,SystemServer_Server , SystemServer_DNS_Alias , SystemServer_CreatedDate ,SystemServer_CreatedBy ,SystemServer_ModifiedDate ,SystemServer_ModifiedBy ,SystemServer_History ,SystemServer_IsActive) VALUES (@SystemServer_Description ,@SystemServer_Server , @SystemServer_DNS_Alias , @SystemServer_CreatedDate ,@SystemServer_CreatedBy ,@SystemServer_ModifiedDate ,@SystemServer_ModifiedBy ,@SystemServer_History ,@SystemServer_IsActive); SELECT @SystemServer_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SystemServer WHERE (SystemServer_Id = @SystemServer_Id)" UpdateCommand="UPDATE Administration_SystemServer SET SystemServer_Description = @SystemServer_Description ,SystemServer_Server = @SystemServer_Server , SystemServer_DNS_Alias = @SystemServer_DNS_Alias , SystemServer_ModifiedDate = @SystemServer_ModifiedDate ,SystemServer_ModifiedBy = @SystemServer_ModifiedBy ,SystemServer_History = @SystemServer_History ,SystemServer_IsActive = @SystemServer_IsActive WHERE SystemServer_Id = @SystemServer_Id" OnUpdated="SqlDataSource_SystemServer_Form_Updated" OnInserted="SqlDataSource_SystemServer_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SystemServer_Id" Type="Int32" />
                    <asp:Parameter Name="SystemServer_Description" Type="String" />
                    <asp:Parameter Name="SystemServer_Server" Type="String" />
                    <asp:Parameter Name="SystemServer_DNS_Alias" Type="String" />
                    <asp:Parameter Name="SystemServer_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemServer_CreatedBy" Type="String" />
                    <asp:Parameter Name="SystemServer_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemServer_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemServer_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SystemServer_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SystemServer_Id" QueryStringField="SystemServer_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SystemServer_Description" Type="String" />
                    <asp:Parameter Name="SystemServer_Server" Type="String" />
                    <asp:Parameter Name="SystemServer_DNS_Alias" Type="String" />
                    <asp:Parameter Name="SystemServer_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemServer_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemServer_History" Type="String" />
                    <asp:Parameter Name="SystemServer_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SystemServer_Id" Type="Int32" />
                  </UpdateParameters>
                </asp:SqlDataSource>
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
