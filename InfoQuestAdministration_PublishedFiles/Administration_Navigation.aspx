<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Navigation" CodeBehind="Administration_Navigation.aspx.cs" %>

<%@ Register Namespace="InfoQuest" TagPrefix="AjaxHTMLEditor" Assembly="InfoQuestAdministration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Navigation</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Navigation.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Navigation" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Navigation" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Navigation" AssociatedUpdatePanelID="UpdatePanel_Navigation">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Navigation" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Navigation
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Navigation_Form" runat="server" DataKeyNames="Navigation_Id" CssClass="FormView" DataSourceID="SqlDataSource_Navigation_Form" OnItemInserting="FormView_Navigation_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Navigation_Form_ItemCommand" OnItemUpdating="FormView_Navigation_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("Navigation_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_InsertDescription" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" onChange="Validation_Form();" Content='<%# Bind("Navigation_Description") %>' />
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Navigation_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Navigation_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Navigation_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Navigation_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Navigation_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Navigation" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("Navigation_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("Navigation_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <AjaxHTMLEditor:Override_AjaxControlToolkitHtmlEditorEditor ID="Editor_EditDescription" runat="server" CssClass="Controls_AjaxHTMLEditor" Width="800px" Height="500px" ActiveMode="Html" Content='<%# Bind("Navigation_Description") %>' />
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Navigation_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Navigation_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Navigation_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Navigation_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Navigation_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Navigation" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Navigation_Form" runat="server" InsertCommand="INSERT INTO Administration_Navigation (Navigation_Name ,Navigation_Description ,Navigation_CreatedDate ,Navigation_CreatedBy ,Navigation_ModifiedDate ,Navigation_ModifiedBy ,Navigation_History ,Navigation_IsActive) VALUES (@Navigation_Name ,@Navigation_Description ,@Navigation_CreatedDate ,@Navigation_CreatedBy ,@Navigation_ModifiedDate ,@Navigation_ModifiedBy ,@Navigation_History ,@Navigation_IsActive); SELECT @Navigation_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_Navigation WHERE (Navigation_Id = @Navigation_Id)" UpdateCommand="UPDATE Administration_Navigation SET Navigation_Name = @Navigation_Name ,Navigation_Description = @Navigation_Description ,Navigation_ModifiedDate = @Navigation_ModifiedDate ,Navigation_ModifiedBy = @Navigation_ModifiedBy ,Navigation_History = @Navigation_History ,Navigation_IsActive = @Navigation_IsActive WHERE Navigation_Id = @Navigation_Id" OnUpdated="SqlDataSource_Navigation_Form_Updated" OnInserted="SqlDataSource_Navigation_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="Navigation_Id" Type="Int32" />
                    <asp:Parameter Name="Navigation_Name" Type="String" />
                    <asp:Parameter Name="Navigation_Description" Type="String" />
                    <asp:Parameter Name="Navigation_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="Navigation_CreatedBy" Type="String" />
                    <asp:Parameter Name="Navigation_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Navigation_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Navigation_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="Navigation_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Navigation_Id" QueryStringField="Navigation_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Navigation_Name" Type="String" />
                    <asp:Parameter Name="Navigation_Description" Type="String" />
                    <asp:Parameter Name="Navigation_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Navigation_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Navigation_History" Type="String" />
                    <asp:Parameter Name="Navigation_IsActive" Type="Boolean" />
                    <asp:Parameter Name="Navigation_Id" Type="Int32" />
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
