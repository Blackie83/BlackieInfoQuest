<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Exceptions" CodeBehind="Administration_Exceptions.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Exceptions Page</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Exceptions.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Exceptions_Page" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Exceptions" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Exceptions" AssociatedUpdatePanelID="UpdatePanel_Exceptions">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Exceptions" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Exceptions</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Exceptions_Form" runat="server" DataKeyNames="Exceptions_Id" CssClass="FormView" DataSourceID="SqlDataSource_Exceptions_Form" DefaultMode="Edit" OnItemCommand="FormView_Exceptions_Form_ItemCommand" OnItemUpdating="FormView_Exceptions_Form_ItemUpdating">
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%#: Bind("Exceptions_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Page
                        </td>
                        <td>
                          <asp:Label ID="Label_EditPage" runat="server" Text='<%#: Bind("Exceptions_Page") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">URL
                        </td>
                        <td>
                          <asp:HyperLink ID="HyperLink_EditURL" NavigateUrl='<%# Bind("Exceptions_URL") %>' Text='<%# Bind("Exceptions_URL") %>' Target="_blank" runat="server"></asp:HyperLink>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Error Message
                        </td>
                        <td>
                          <asp:Label ID="Label_EditErrorMessage" runat="server" Width="800px" CssClass="Controls_WrapText" Text='<%#: Bind("Exceptions_ErrorMessage") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Stack Trace
                        </td>
                        <td>
                          <asp:Label ID="Label_EditStackTrace" runat="server" Width="800px" CssClass="Controls_WrapText" Text='<%#: Bind("Exceptions_StackTrace") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Extra Information
                        </td>
                        <td>
                          <asp:Label ID="Label_EditExtraInformation" runat="server" Text='<%#: Bind("Exceptions_ExtraInformation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">UserName
                        </td>
                        <td>
                          <asp:Label ID="Label_EditUserName" runat="server" Text='<%#: Bind("Exceptions_UserName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditDate" runat="server" Text='<%#: Bind("Exceptions_Date","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="3" Width="500px" Text='<%#: Bind("Exceptions_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Completed
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_EditCompleted" runat="server" SelectedValue='<%#: Bind("Exceptions_Completed") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="True">Yes</asp:ListItem>
                            <asp:ListItem Value="False">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Completed Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCompletedDate" runat="server" Text='<%#: Bind("Exceptions_CompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%#: Bind("Exceptions_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%#: Bind("Exceptions_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" Text="Back To List" CommandName="Cancel" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Exceptions" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Exceptions_Form" runat="server" SelectCommand="SELECT * FROM Administration_Exceptions WHERE (Exceptions_Id = @Exceptions_Id)" UpdateCommand="UPDATE Administration_Exceptions SET Exceptions_Description = @Exceptions_Description ,Exceptions_Completed = @Exceptions_Completed ,Exceptions_CompletedDate = @Exceptions_CompletedDate ,Exceptions_ModifiedDate = @Exceptions_ModifiedDate ,Exceptions_ModifiedBy = @Exceptions_ModifiedBy ,Exceptions_History = @Exceptions_History WHERE Exceptions_Id = @Exceptions_Id" OnUpdated="SqlDataSource_Exceptions_Form_Updated">
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Exceptions_Id" QueryStringField="Exceptions_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Exceptions_Description" Type="String" />
                    <asp:Parameter Name="Exceptions_Completed" Type="Boolean" />
                    <asp:Parameter Name="Exceptions_CompletedDate" Type="DateTime" />
                    <asp:Parameter Name="Exceptions_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Exceptions_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Exceptions_History" Type="String" />
                    <asp:Parameter Name="Exceptions_Id" Type="Int32" />
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
