<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_Facility_Type_Lookup.aspx.cs" Inherits="InfoQuestAdministration.Administration_Facility_Type_Lookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Facility Type Lookup</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Facility_Type_Lookup.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Facility_Type_Lookup" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Facility_Type_Lookup" runat="server">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Facility_Type_Lookup" AssociatedUpdatePanelID="UpdatePanel_Facility_Type_Lookup">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Facility_Type_Lookup" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Facility Type Lookup</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Facility_Type_Lookup_Form" runat="server" DataKeyNames="Facility_Type_Lookup_Id" CssClass="FormView" DataSourceID="SqlDataSource_Facility_Type_Lookup_Form" OnItemInserting="FormView_Facility_Type_Lookup_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Facility_Type_Lookup_Form_ItemCommand" OnItemUpdating="FormView_Facility_Type_Lookup_Form_ItemUpdating">
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
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("Facility_Type_Lookup_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Facility_Type_Lookup_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Facility_Type_Lookup_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Facility_Type_Lookup_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Facility_Type_Lookup_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Facility_Type_Lookup_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Facility Type Lookup" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("Facility_Type_Lookup_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("Facility_Type_Lookup_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Facility_Type_Lookup_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Facility_Type_Lookup_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Facility_Type_Lookup_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Facility_Type_Lookup_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Facility_Type_Lookup_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Facility Type Lookup" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Facility_Type_Lookup_Form" runat="server" InsertCommand="INSERT INTO Administration_Facility_Type_Lookup (Facility_Type_Lookup_Name , Facility_Type_Lookup_CreatedDate ,Facility_Type_Lookup_CreatedBy ,Facility_Type_Lookup_ModifiedDate ,Facility_Type_Lookup_ModifiedBy ,Facility_Type_Lookup_History ,Facility_Type_Lookup_IsActive) VALUES (@Facility_Type_Lookup_Name , @Facility_Type_Lookup_CreatedDate ,@Facility_Type_Lookup_CreatedBy ,@Facility_Type_Lookup_ModifiedDate ,@Facility_Type_Lookup_ModifiedBy ,@Facility_Type_Lookup_History ,@Facility_Type_Lookup_IsActive); SELECT @Facility_Type_Lookup_Id = SCOPE_IDENTITY()" 
                  SelectCommand="SELECT * FROM Administration_Facility_Type_Lookup WHERE (Facility_Type_Lookup_Id = @Facility_Type_Lookup_Id)" UpdateCommand="UPDATE Administration_Facility_Type_Lookup SET Facility_Type_Lookup_Name = @Facility_Type_Lookup_Name , Facility_Type_Lookup_ModifiedDate = @Facility_Type_Lookup_ModifiedDate ,Facility_Type_Lookup_ModifiedBy = @Facility_Type_Lookup_ModifiedBy ,Facility_Type_Lookup_History = @Facility_Type_Lookup_History ,Facility_Type_Lookup_IsActive = @Facility_Type_Lookup_IsActive WHERE Facility_Type_Lookup_Id = @Facility_Type_Lookup_Id" OnUpdated="SqlDataSource_Facility_Type_Lookup_Form_Updated" OnInserted="SqlDataSource_Facility_Type_Lookup_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="Facility_Type_Lookup_Id" Type="Int32" />
                    <asp:Parameter Name="Facility_Type_Lookup_Name" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_Type_Lookup_CreatedBy" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_Type_Lookup_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="Facility_Type_Lookup_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Facility_Type_Lookup_Id" QueryStringField="Facility_Type_Lookup_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Facility_Type_Lookup_Name" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_Type_Lookup_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_History" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup_IsActive" Type="Boolean" />
                    <asp:Parameter Name="Facility_Type_Lookup_Id" Type="Int32" />
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
