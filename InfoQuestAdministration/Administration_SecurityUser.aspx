<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.SecurityUser" CodeBehind="Administration_SecurityUser.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security User</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SecurityUser.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityUser" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityUser" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityUser" AssociatedUpdatePanelID="UpdatePanel_SecurityUser">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityUser" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Security User
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SecurityUser_Form" runat="server" DataKeyNames="SecurityUser_Id" CssClass="FormView" DataSourceID="SqlDataSource_SecurityUser_Form" OnItemInserting="FormView_SecurityUser_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SecurityUser_Form_ItemCommand" OnItemUpdating="FormView_SecurityUser_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormUserName">UserName (LHC\username)
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertUserName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_UserName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertUserName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertUserNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDisplayName">Display Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertDisplayName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_DisplayName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFirstName">First Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertFirstName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_FirstName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLastName">Last Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertLastName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_LastName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertEmployeeNumber" runat="server" Width="500px" Text='<%# Bind("SecurityUser_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmail">Email
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertEmail" runat="server" Width="500px" Text='<%# Bind("SecurityUser_Email") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormManagerUserName">Manager UserName
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_InsertManagerUserName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_ManagerUserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SecurityUser_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SecurityUser_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SecurityUser_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SecurityUser_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SecurityUser_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Security User" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SecurityUser_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUserName">UserName (LHC\username)
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditUserName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_UserName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditUserName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditUserNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDisplayName">Display Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditDisplayName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_DisplayName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFirstName">First Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditFirstName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_FirstName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLastName">Last Name
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditLastName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_LastName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditEmployeeNumber" runat="server" Width="500px" Text='<%# Bind("SecurityUser_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmail">Email
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditEmail" runat="server" Width="500px" Text='<%# Bind("SecurityUser_Email") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormManagerUserName">Manager UserName
                        </td>
                        <td style="width: 525px;">
                          <asp:TextBox ID="TextBox_EditManagerUserName" runat="server" Width="500px" Text='<%# Bind("SecurityUser_ManagerUserName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SecurityUser_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SecurityUser_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SecurityUser_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SecurityUser_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SecurityUser_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Security User" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SecurityUser_Form" runat="server" InsertCommand="INSERT INTO Administration_SecurityUser (SecurityUser_UserName ,SecurityUser_DisplayName ,SecurityUser_FirstName ,SecurityUser_LastName ,SecurityUser_EmployeeNumber ,SecurityUser_Email ,SecurityUser_ManagerUserName ,SecurityUser_CreatedDate ,SecurityUser_CreatedBy ,SecurityUser_ModifiedDate ,SecurityUser_ModifiedBy ,SecurityUser_History ,SecurityUser_IsActive) VALUES (@SecurityUser_UserName ,@SecurityUser_DisplayName ,@SecurityUser_FirstName ,@SecurityUser_LastName ,@SecurityUser_EmployeeNumber ,@SecurityUser_Email ,@SecurityUser_ManagerUserName ,@SecurityUser_CreatedDate ,@SecurityUser_CreatedBy ,@SecurityUser_ModifiedDate ,@SecurityUser_ModifiedBy ,@SecurityUser_History ,@SecurityUser_IsActive); SELECT @SecurityUser_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SecurityUser WHERE (SecurityUser_Id = @SecurityUser_Id)" UpdateCommand="UPDATE Administration_SecurityUser SET SecurityUser_UserName = @SecurityUser_UserName ,SecurityUser_DisplayName = @SecurityUser_DisplayName ,SecurityUser_FirstName = @SecurityUser_FirstName ,SecurityUser_LastName = @SecurityUser_LastName ,SecurityUser_EmployeeNumber = @SecurityUser_EmployeeNumber ,SecurityUser_Email = @SecurityUser_Email ,SecurityUser_ManagerUserName = @SecurityUser_ManagerUserName ,SecurityUser_ModifiedDate = @SecurityUser_ModifiedDate ,SecurityUser_ModifiedBy = @SecurityUser_ModifiedBy ,SecurityUser_History = @SecurityUser_History ,SecurityUser_IsActive = @SecurityUser_IsActive WHERE SecurityUser_Id = @SecurityUser_Id" OnUpdated="SqlDataSource_SecurityUser_Form_Updated" OnInserted="SqlDataSource_SecurityUser_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SecurityUser_Id" Type="Int32" />
                    <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                    <asp:Parameter Name="SecurityUser_DisplayName" Type="String" />
                    <asp:Parameter Name="SecurityUser_FirstName" Type="String" />
                    <asp:Parameter Name="SecurityUser_LastName" Type="String" />
                    <asp:Parameter Name="SecurityUser_EmployeeNumber" Type="String" />
                    <asp:Parameter Name="SecurityUser_Email" Type="String" />
                    <asp:Parameter Name="SecurityUser_ManagerUserName" Type="String" />
                    <asp:Parameter Name="SecurityUser_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityUser_CreatedBy" Type="String" />
                    <asp:Parameter Name="SecurityUser_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityUser_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityUser_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SecurityUser_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SecurityUser_Id" QueryStringField="SecurityUser_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                    <asp:Parameter Name="SecurityUser_DisplayName" Type="String" />
                    <asp:Parameter Name="SecurityUser_FirstName" Type="String" />
                    <asp:Parameter Name="SecurityUser_LastName" Type="String" />
                    <asp:Parameter Name="SecurityUser_EmployeeNumber" Type="String" />
                    <asp:Parameter Name="SecurityUser_Email" Type="String" />
                    <asp:Parameter Name="SecurityUser_ManagerUserName" Type="String" />
                    <asp:Parameter Name="SecurityUser_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityUser_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityUser_History" Type="String" />
                    <asp:Parameter Name="SecurityUser_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SecurityUser_Id" Type="Int32" />
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
