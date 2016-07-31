<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_SecurityRole" CodeBehind="Administration_SecurityRole.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Role</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SecurityRole.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityRole" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityRole" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityRole" AssociatedUpdatePanelID="UpdatePanel_SecurityRole">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityRole" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Security Role
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_SecurityRole_Form" runat="server" DataKeyNames="SecurityRole_Id" CssClass="FormView" DataSourceID="SqlDataSource_SecurityRole_Form" OnItemInserting="FormView_SecurityRole_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_SecurityRole_Form_ItemCommand" OnItemUpdating="FormView_SecurityRole_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormFormId">Form
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertFormId" runat="server" DataSourceID="SqlDataSource_SecurityRole_InsertFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" SelectedValue='<%# Bind("Form_Id") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                            <asp:ListItem Value="-1">No Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("SecurityRole_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("SecurityRole_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">Rank = 1: Access to be given by InfoQuest Administrator, All Facilities<br />
                          Rank = 2: Access to be given by InfoQuest Administrator, Specific Facility<br />
                          Rank = 3 and higher: Access to be given by Contact Centre
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRank">Rank
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertRank" runat="server" Width="50px" Text='<%# Bind("SecurityRole_Rank") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertRank" runat="server" TargetControlID="TextBox_InsertRank" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("SecurityRole_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("SecurityRole_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("SecurityRole_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("SecurityRole_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("SecurityRole_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Security Role" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("SecurityRole_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFormId">Form
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditFormId" runat="server" DataSourceID="SqlDataSource_SecurityRole_EditFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" SelectedValue='<%# Bind("Form_Id") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                            <asp:ListItem Value="-1">No Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("SecurityRole_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("SecurityRole_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">Rank = 1: Access to be given by InfoQuest Administrator, All Facilities<br />
                          Rank = 2: Access to be given by InfoQuest Administrator, Specific Facility<br />
                          Rank = 3 and higher: Access to be given by Contact Centre
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRank">Rank
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditRank" runat="server" Width="50px" Text='<%# Bind("SecurityRole_Rank") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditRank" runat="server" TargetControlID="TextBox_EditRank" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("SecurityRole_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("SecurityRole_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("SecurityRole_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("SecurityRole_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("SecurityRole_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Security Role" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_SecurityRole_InsertFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_SecurityRole_EditFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_SecurityRole_Form" runat="server" InsertCommand="INSERT INTO Administration_SecurityRole (Form_Id ,SecurityRole_Name ,SecurityRole_Description ,SecurityRole_Rank ,SecurityRole_CreatedDate ,SecurityRole_CreatedBy ,SecurityRole_ModifiedDate ,SecurityRole_ModifiedBy ,SecurityRole_History ,SecurityRole_IsActive) VALUES ( @Form_Id ,@SecurityRole_Name ,@SecurityRole_Description ,@SecurityRole_Rank ,@SecurityRole_CreatedDate ,@SecurityRole_CreatedBy ,@SecurityRole_ModifiedDate ,@SecurityRole_ModifiedBy ,@SecurityRole_History ,@SecurityRole_IsActive); SELECT @SecurityRole_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_SecurityRole WHERE (SecurityRole_Id = @SecurityRole_Id)" UpdateCommand="UPDATE Administration_SecurityRole SET Form_Id = @Form_Id ,SecurityRole_Name = @SecurityRole_Name ,SecurityRole_Description = @SecurityRole_Description ,SecurityRole_Rank = @SecurityRole_Rank ,SecurityRole_ModifiedDate = @SecurityRole_ModifiedDate ,SecurityRole_ModifiedBy = @SecurityRole_ModifiedBy ,SecurityRole_History = @SecurityRole_History ,SecurityRole_IsActive = @SecurityRole_IsActive WHERE SecurityRole_Id = @SecurityRole_Id" OnUpdated="SqlDataSource_SecurityRole_Form_Updated" OnInserted="SqlDataSource_SecurityRole_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="SecurityRole_Id" Type="Int32" />
                    <asp:Parameter Name="Form_Id" Type="Int32" />
                    <asp:Parameter Name="SecurityRole_Name" Type="String" />
                    <asp:Parameter Name="SecurityRole_Description" Type="String" />
                    <asp:Parameter Name="SecurityRole_Rank" Type="Int32" />
                    <asp:Parameter Name="SecurityRole_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityRole_CreatedBy" Type="String" />
                    <asp:Parameter Name="SecurityRole_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityRole_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityRole_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="SecurityRole_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="SecurityRole_Id" QueryStringField="SecurityRole_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Form_Id" Type="Int32" />
                    <asp:Parameter Name="SecurityRole_Name" Type="String" />
                    <asp:Parameter Name="SecurityRole_Description" Type="String" />
                    <asp:Parameter Name="SecurityRole_Rank" Type="Int32" />
                    <asp:Parameter Name="SecurityRole_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="SecurityRole_ModifiedBy" Type="String" />
                    <asp:Parameter Name="SecurityRole_History" Type="String" />
                    <asp:Parameter Name="SecurityRole_IsActive" Type="Boolean" />
                    <asp:Parameter Name="SecurityRole_Id" Type="Int32" />
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
