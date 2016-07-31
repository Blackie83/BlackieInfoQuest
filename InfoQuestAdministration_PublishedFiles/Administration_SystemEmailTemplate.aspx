<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_SystemEmailTemplate" CodeBehind="Administration_SystemEmailTemplate.aspx.cs" %>

<%@ Register Assembly="InfoQuestWCF" Namespace="InfoQuestWCF" TagPrefix="AjaxHTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - System Email Template</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SystemEmailTemplate.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SystemEmailTemplate" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SystemEmailTemplate" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SystemEmailTemplate" AssociatedUpdatePanelID="UpdatePanel_SystemEmailTemplate">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SystemEmailTemplate" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>System Email Template
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SystemEmailTemplate_Form" runat="server" DataKeyNames="SystemEmailTemplate_Id" CssClass="FormView" DataSourceID="SqlDataSource_SystemEmailTemplate_Form" OnItemInserting="FormView_SystemEmailTemplate_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SystemEmailTemplate_Form_ItemCommand" OnItemUpdating="FormView_SystemEmailTemplate_Form_ItemUpdating">
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
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("SystemEmailTemplate_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertDescription_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertDescriptionError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTemplate">Template
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_InsertTemplate" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" onChange="Validation_Form();" Content='<%# Bind("SystemEmailTemplate_Template") %>' />
                          &nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SystemEmailTemplate_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SystemEmailTemplate_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SystemEmailTemplate_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SystemEmailTemplate_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SystemEmailTemplate_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add System Email Template" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SystemEmailTemplate_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("SystemEmailTemplate_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditDescription_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditDescriptionError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTemplate">Template
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_EditTemplate" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" Content='<%# Bind("SystemEmailTemplate_Template") %>' />
                          &nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SystemEmailTemplate_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SystemEmailTemplate_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SystemEmailTemplate_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SystemEmailTemplate_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SystemEmailTemplate_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update System Email Template" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SystemEmailTemplate_Form" runat="server" InsertCommand="INSERT INTO Administration_SystemEmailTemplate (SystemEmailTemplate_Description ,SystemEmailTemplate_Template ,SystemEmailTemplate_CreatedDate ,SystemEmailTemplate_CreatedBy ,SystemEmailTemplate_ModifiedDate ,SystemEmailTemplate_ModifiedBy ,SystemEmailTemplate_History ,SystemEmailTemplate_IsActive) VALUES (@SystemEmailTemplate_Description ,@SystemEmailTemplate_Template ,@SystemEmailTemplate_CreatedDate ,@SystemEmailTemplate_CreatedBy ,@SystemEmailTemplate_ModifiedDate ,@SystemEmailTemplate_ModifiedBy ,@SystemEmailTemplate_History ,@SystemEmailTemplate_IsActive); SELECT @SystemEmailTemplate_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SystemEmailTemplate WHERE (SystemEmailTemplate_Id = @SystemEmailTemplate_Id)" UpdateCommand="UPDATE Administration_SystemEmailTemplate SET SystemEmailTemplate_Description = @SystemEmailTemplate_Description ,SystemEmailTemplate_Template = @SystemEmailTemplate_Template ,SystemEmailTemplate_ModifiedDate = @SystemEmailTemplate_ModifiedDate ,SystemEmailTemplate_ModifiedBy = @SystemEmailTemplate_ModifiedBy ,SystemEmailTemplate_History = @SystemEmailTemplate_History ,SystemEmailTemplate_IsActive = @SystemEmailTemplate_IsActive WHERE SystemEmailTemplate_Id = @SystemEmailTemplate_Id" OnUpdated="SqlDataSource_SystemEmailTemplate_Form_Updated" OnInserted="SqlDataSource_SystemEmailTemplate_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SystemEmailTemplate_Id" Type="Int32" />
                    <asp:Parameter Name="SystemEmailTemplate_Description" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_Template" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemEmailTemplate_CreatedBy" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemEmailTemplate_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SystemEmailTemplate_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SystemEmailTemplate_Id" QueryStringField="SystemEmailTemplate_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SystemEmailTemplate_Description" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_Template" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemEmailTemplate_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_History" Type="String" />
                    <asp:Parameter Name="SystemEmailTemplate_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SystemEmailTemplate_Id" Type="Int32" />
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
