<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_EmailNotification.aspx.cs" Inherits="InfoQuestAdministration.Administration_EmailNotification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Email Notification</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_EmailNotification.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_EmailNotification" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_EmailNotification" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_EmailNotification" AssociatedUpdatePanelID="UpdatePanel_EmailNotification">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_EmailNotification" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Email Notification</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_EmailNotification_Form" runat="server" DataKeyNames="EmailNotification_Id" CssClass="FormView" DataSourceID="SqlDataSource_EmailNotification_Form" OnItemInserting="FormView_EmailNotification_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_EmailNotification_Form_ItemCommand" OnItemUpdating="FormView_EmailNotification_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormAssembly">Assembly
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertAssembly" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Assembly") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormMethod">Method
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertMethod" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Method") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmail">Email
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertEmail" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Email") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertEmail_TextChanged"></asp:TextBox><br />
                          <asp:Label ID="Label_InsertEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("EmailNotification_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("EmailNotification_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("EmailNotification_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("EmailNotification_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("EmailNotification_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Email Notification" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("EmailNotification_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAssembly">Assembly
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditAssembly" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Assembly") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormMethod">Method
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditMethod" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Method") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmail">Email
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditEmail" runat="server" Width="500px" Text='<%# Bind("EmailNotification_Email") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EditEmail_TextChanged"></asp:TextBox><br />
                          <asp:Label ID="Label_EditEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditEmail" runat="server" Value='<%# Bind("EmailNotification_Email") %>' />
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("EmailNotification_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("EmailNotification_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("EmailNotification_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("EmailNotification_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("EmailNotification_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Email Notification" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_EmailNotification_Form" runat="server" InsertCommand="INSERT INTO Administration_EmailNotification (EmailNotification_Assembly , EmailNotification_Method , EmailNotification_Email , EmailNotification_CreatedDate , EmailNotification_CreatedBy , EmailNotification_ModifiedDate , EmailNotification_ModifiedBy , EmailNotification_History , EmailNotification_IsActive) VALUES (@EmailNotification_Assembly , @EmailNotification_Method , @EmailNotification_Email , @EmailNotification_CreatedDate , @EmailNotification_CreatedBy , @EmailNotification_ModifiedDate , @EmailNotification_ModifiedBy , @EmailNotification_History , @EmailNotification_IsActive); SELECT @EmailNotification_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_EmailNotification WHERE (EmailNotification_Id = @EmailNotification_Id)" UpdateCommand="UPDATE Administration_EmailNotification SET EmailNotification_Assembly = @EmailNotification_Assembly , EmailNotification_Method = @EmailNotification_Method , EmailNotification_Email = @EmailNotification_Email , EmailNotification_ModifiedDate = @EmailNotification_ModifiedDate ,EmailNotification_ModifiedBy = @EmailNotification_ModifiedBy ,EmailNotification_History = @EmailNotification_History ,EmailNotification_IsActive = @EmailNotification_IsActive WHERE EmailNotification_Id = @EmailNotification_Id" OnUpdated="SqlDataSource_EmailNotification_Form_Updated" OnInserted="SqlDataSource_EmailNotification_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="EmailNotification_Id" Type="Int32" />
                    <asp:Parameter Name="EmailNotification_Assembly" Type="String" />
                    <asp:Parameter Name="EmailNotification_Method" Type="String" />
                    <asp:Parameter Name="EmailNotification_Email" Type="String" />
                    <asp:Parameter Name="EmailNotification_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="EmailNotification_CreatedBy" Type="String" />
                    <asp:Parameter Name="EmailNotification_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="EmailNotification_ModifiedBy" Type="String" />
                    <asp:Parameter Name="EmailNotification_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="EmailNotification_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="EmailNotification_Id" QueryStringField="EmailNotification_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="EmailNotification_Assembly" Type="String" />
                    <asp:Parameter Name="EmailNotification_Method" Type="String" />
                    <asp:Parameter Name="EmailNotification_Email" Type="String" />
                    <asp:Parameter Name="EmailNotification_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="EmailNotification_ModifiedBy" Type="String" />
                    <asp:Parameter Name="EmailNotification_History" Type="String" />
                    <asp:Parameter Name="EmailNotification_IsActive" Type="Boolean" />
                    <asp:Parameter Name="EmailNotification_Id" Type="Int32" />
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
