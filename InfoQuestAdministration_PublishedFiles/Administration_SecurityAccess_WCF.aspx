<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_SecurityAccess_WCF.aspx.cs" Inherits="InfoQuestAdministration.Administration_SecurityAccess_WCF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Access WCF</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SecurityAccess_WCF.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityAccess_WCF" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityAccess_WCF" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityAccess_WCF" AssociatedUpdatePanelID="UpdatePanel_SecurityAccess_WCF">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityAccess_WCF" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Security Access WCF</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SecurityAccess_WCF_Form" runat="server" DataKeyNames="SecurityAccess_WCF_Id" CssClass="FormView" DataSourceID="SqlDataSource_SecurityAccess_WCF_Form" OnItemInserting="FormView_SecurityAccess_WCF_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SecurityAccess_WCF_Form_ItemCommand" OnItemUpdating="FormView_SecurityAccess_WCF_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormMethod">Method
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertMethod" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_Method") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUserName">UserName
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertUserName" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_UserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPassword">Password
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertPassword" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_Password") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SecurityAccess_WCF_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SecurityAccess_WCF_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SecurityAccess_WCF_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SecurityAccess_WCF_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SecurityAccess_WCF_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Security Access WCF" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SecurityAccess_WCF_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormMethod">Method
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditMethod" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_Method") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUserName">UserName
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditUserName" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_UserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPassword">Password
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditPassword" runat="server" Width="500px" Text='<%# Bind("SecurityAccess_WCF_Password") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SecurityAccess_WCF_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SecurityAccess_WCF_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SecurityAccess_WCF_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SecurityAccess_WCF_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SecurityAccess_WCF_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Security Access WCF" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SecurityAccess_WCF_Form" runat="server" InsertCommand="INSERT INTO Administration_SecurityAccess_WCF (SecurityAccess_WCF_Method ,SecurityAccess_WCF_UserName , SecurityAccess_WCF_Password , SecurityAccess_WCF_CreatedDate ,SecurityAccess_WCF_CreatedBy ,SecurityAccess_WCF_ModifiedDate ,SecurityAccess_WCF_ModifiedBy ,SecurityAccess_WCF_History ,SecurityAccess_WCF_IsActive) VALUES (@SecurityAccess_WCF_Method ,@SecurityAccess_WCF_UserName , @SecurityAccess_WCF_Password , @SecurityAccess_WCF_CreatedDate ,@SecurityAccess_WCF_CreatedBy ,@SecurityAccess_WCF_ModifiedDate ,@SecurityAccess_WCF_ModifiedBy ,@SecurityAccess_WCF_History ,@SecurityAccess_WCF_IsActive); SELECT @SecurityAccess_WCF_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SecurityAccess_WCF WHERE (SecurityAccess_WCF_Id = @SecurityAccess_WCF_Id)" UpdateCommand="UPDATE Administration_SecurityAccess_WCF SET SecurityAccess_WCF_Method = @SecurityAccess_WCF_Method ,SecurityAccess_WCF_UserName = @SecurityAccess_WCF_UserName , SecurityAccess_WCF_Password = @SecurityAccess_WCF_Password ,SecurityAccess_WCF_ModifiedDate = @SecurityAccess_WCF_ModifiedDate ,SecurityAccess_WCF_ModifiedBy = @SecurityAccess_WCF_ModifiedBy ,SecurityAccess_WCF_History = @SecurityAccess_WCF_History ,SecurityAccess_WCF_IsActive = @SecurityAccess_WCF_IsActive WHERE SecurityAccess_WCF_Id = @SecurityAccess_WCF_Id" OnUpdated="SqlDataSource_SecurityAccess_WCF_Form_Updated" OnInserted="SqlDataSource_SecurityAccess_WCF_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SecurityAccess_WCF_Id" Type="Int32" />
                    <asp:Parameter Name="SecurityAccess_WCF_Method" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_UserName" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_Password" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityAccess_WCF_CreatedBy" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityAccess_WCF_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SecurityAccess_WCF_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SecurityAccess_WCF_Id" QueryStringField="SecurityAccess_WCF_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SecurityAccess_WCF_Method" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_UserName" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_Password" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityAccess_WCF_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_History" Type="String" />
                    <asp:Parameter Name="SecurityAccess_WCF_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SecurityAccess_WCF_Id" Type="Int32" />
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
