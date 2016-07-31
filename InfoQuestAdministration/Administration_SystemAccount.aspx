<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_SystemAccount.aspx.cs" Inherits="InfoQuestAdministration.Administration_SystemAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - System Account</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SystemAccount.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SystemAccount" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SystemAccount" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SystemAccount" AssociatedUpdatePanelID="UpdatePanel_SystemAccount">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SystemAccount" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>System Account</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SystemAccount_Form" runat="server" DataKeyNames="SystemAccount_Id" CssClass="FormView" DataSourceID="SqlDataSource_SystemAccount_Form" OnItemInserting="FormView_SystemAccount_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SystemAccount_Form_ItemCommand" OnItemUpdating="FormView_SystemAccount_Form_ItemUpdating">
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
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDomain">Domain
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDomain" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Domain") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUserName">UserName
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertUserName" runat="server" Width="500px" Text='<%# Bind("SystemAccount_UserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPassword">Password
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertPassword" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Password") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SystemAccount_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SystemAccount_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SystemAccount_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SystemAccount_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SystemAccount_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add System Account" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SystemAccount_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDomain">Domain
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDomain" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Domain") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUserName">UserName
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditUserName" runat="server" Width="500px" Text='<%# Bind("SystemAccount_UserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPassword">Password
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditPassword" runat="server" Width="500px" Text='<%# Bind("SystemAccount_Password") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SystemAccount_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SystemAccount_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SystemAccount_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SystemAccount_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SystemAccount_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update System Account" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SystemAccount_Form" runat="server" InsertCommand="INSERT INTO Administration_SystemAccount (SystemAccount_Description ,SystemAccount_Domain , SystemAccount_UserName , SystemAccount_Password , SystemAccount_CreatedDate ,SystemAccount_CreatedBy ,SystemAccount_ModifiedDate ,SystemAccount_ModifiedBy ,SystemAccount_History ,SystemAccount_IsActive) VALUES (@SystemAccount_Description ,@SystemAccount_Domain , @SystemAccount_UserName , @SystemAccount_Password , @SystemAccount_CreatedDate ,@SystemAccount_CreatedBy ,@SystemAccount_ModifiedDate ,@SystemAccount_ModifiedBy ,@SystemAccount_History ,@SystemAccount_IsActive); SELECT @SystemAccount_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SystemAccount WHERE (SystemAccount_Id = @SystemAccount_Id)" UpdateCommand="UPDATE Administration_SystemAccount SET SystemAccount_Description = @SystemAccount_Description ,SystemAccount_Domain = @SystemAccount_Domain , SystemAccount_UserName = @SystemAccount_UserName , SystemAccount_Password = @SystemAccount_Password ,SystemAccount_ModifiedDate = @SystemAccount_ModifiedDate ,SystemAccount_ModifiedBy = @SystemAccount_ModifiedBy ,SystemAccount_History = @SystemAccount_History ,SystemAccount_IsActive = @SystemAccount_IsActive WHERE SystemAccount_Id = @SystemAccount_Id" OnUpdated="SqlDataSource_SystemAccount_Form_Updated" OnInserted="SqlDataSource_SystemAccount_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SystemAccount_Id" Type="Int32" />
                    <asp:Parameter Name="SystemAccount_Description" Type="String" />
                    <asp:Parameter Name="SystemAccount_Domain" Type="String" />
                    <asp:Parameter Name="SystemAccount_UserName" Type="String" />
                    <asp:Parameter Name="SystemAccount_Password" Type="String" />
                    <asp:Parameter Name="SystemAccount_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemAccount_CreatedBy" Type="String" />
                    <asp:Parameter Name="SystemAccount_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemAccount_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemAccount_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SystemAccount_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SystemAccount_Id" QueryStringField="SystemAccount_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SystemAccount_Description" Type="String" />
                    <asp:Parameter Name="SystemAccount_Domain" Type="String" />
                    <asp:Parameter Name="SystemAccount_UserName" Type="String" />
                    <asp:Parameter Name="SystemAccount_Password" Type="String" />
                    <asp:Parameter Name="SystemAccount_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemAccount_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemAccount_History" Type="String" />
                    <asp:Parameter Name="SystemAccount_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SystemAccount_Id" Type="Int32" />
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
