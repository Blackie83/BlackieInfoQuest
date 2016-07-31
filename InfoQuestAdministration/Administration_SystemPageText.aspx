<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_SystemPageText.aspx.cs" Inherits="InfoQuestAdministration.Administration_SystemPageText" %>

<%@ Register Namespace="InfoQuest" TagPrefix="AjaxHTMLEditor" Assembly="InfoQuestAdministration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - System Page Text</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SystemPageText.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SystemPageText" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SystemPageText" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SystemPageText" AssociatedUpdatePanelID="UpdatePanel_SystemPageText">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SystemPageText" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>System Page Text
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SystemPageText_Form" runat="server" DataKeyNames="SystemPageText_Id" CssClass="FormView" DataSourceID="SqlDataSource_SystemPageText_Form" OnItemInserting="FormView_SystemPageText_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SystemPageText_Form_ItemCommand" OnItemUpdating="FormView_SystemPageText_Form_ItemUpdating">
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
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("SystemPageText_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertDescription_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertDescriptionError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormText">Text
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_InsertText" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" onChange="Validation_Form();" Content='<%# Bind("SystemPageText_Text") %>' />
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SystemPageText_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SystemPageText_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SystemPageText_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SystemPageText_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SystemPageText_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add System Page Text" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SystemPageText_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("SystemPageText_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditDescription_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditDescriptionError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormText">Text
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_EditText" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" Content='<%# Bind("SystemPageText_Text") %>' />
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SystemPageText_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SystemPageText_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SystemPageText_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SystemPageText_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SystemPageText_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update System Page Text" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SystemPageText_Form" runat="server" InsertCommand="INSERT INTO Administration_SystemPageText (SystemPageText_Description ,SystemPageText_Text ,SystemPageText_CreatedDate ,SystemPageText_CreatedBy ,SystemPageText_ModifiedDate ,SystemPageText_ModifiedBy ,SystemPageText_History ,SystemPageText_IsActive) VALUES (@SystemPageText_Description ,@SystemPageText_Text ,@SystemPageText_CreatedDate ,@SystemPageText_CreatedBy ,@SystemPageText_ModifiedDate ,@SystemPageText_ModifiedBy ,@SystemPageText_History ,@SystemPageText_IsActive); SELECT @SystemPageText_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SystemPageText WHERE (SystemPageText_Id = @SystemPageText_Id)" UpdateCommand="UPDATE Administration_SystemPageText SET SystemPageText_Description = @SystemPageText_Description ,SystemPageText_Text = @SystemPageText_Text ,SystemPageText_ModifiedDate = @SystemPageText_ModifiedDate ,SystemPageText_ModifiedBy = @SystemPageText_ModifiedBy ,SystemPageText_History = @SystemPageText_History ,SystemPageText_IsActive = @SystemPageText_IsActive WHERE SystemPageText_Id = @SystemPageText_Id" OnUpdated="SqlDataSource_SystemPageText_Form_Updated" OnInserted="SqlDataSource_SystemPageText_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SystemPageText_Id" Type="Int32" />
                    <asp:Parameter Name="SystemPageText_Description" Type="String" />
                    <asp:Parameter Name="SystemPageText_Text" Type="String" />
                    <asp:Parameter Name="SystemPageText_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemPageText_CreatedBy" Type="String" />
                    <asp:Parameter Name="SystemPageText_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemPageText_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemPageText_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SystemPageText_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SystemPageText_Id" QueryStringField="SystemPageText_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SystemPageText_Description" Type="String" />
                    <asp:Parameter Name="SystemPageText_Text" Type="String" />
                    <asp:Parameter Name="SystemPageText_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SystemPageText_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SystemPageText_History" Type="String" />
                    <asp:Parameter Name="SystemPageText_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SystemPageText_Id" Type="Int32" />
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
