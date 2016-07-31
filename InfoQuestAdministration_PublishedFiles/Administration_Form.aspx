<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Form" CodeBehind="Administration_Form.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Form</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Form.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Form" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Form" runat="server">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Form" AssociatedUpdatePanelID="UpdatePanel_Form">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Form" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Form</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Form_Form" runat="server" DataKeyNames="Form_Id" CssClass="FormView" DataSourceID="SqlDataSource_Form_Form" OnItemInserting="FormView_Form_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Form_Form_ItemCommand" OnItemUpdating="FormView_Form_Form_ItemUpdating">
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
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("Form_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" TextMode="MultiLine" Rows="3" Width="500px" Text='<%# Bind("Form_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReportNumberIdentifier">Report Number Identifier
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertReportNumberIdentifier" runat="server" Width="500px" Text='<%# Bind("Form_ReportNumberIdentifier") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertReportNumberIdentifier_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertReportNumberIdentifierError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Print
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertPrint" runat="server" Checked='<%# Bind("Form_Print") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Email
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertEmail" runat="server" Checked='<%# Bind("Form_Email") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Admin
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertAdmin" runat="server" Checked='<%# Bind("Form_Admin") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertUnit" runat="server" Checked='<%# Bind("Form_Unit") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Cut of Day
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertCutOffDay" runat="server" Width="100px" Text='<%# Bind("Form_CutOffDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCutOffDay" runat="server" TargetControlID="TextBox_InsertCutOffDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Maintenance
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertMaintenance" runat="server" Checked='<%# Bind("Form_Maintenance") %>' />&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Form_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Form_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Form_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Form_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Form_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Form" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("Form_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("Form_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="3" Width="500px" Text='<%# Bind("Form_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReportNumberIdentifier">Report Number Identifier
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditReportNumberIdentifier" runat="server" Width="500px" Text='<%# Bind("Form_ReportNumberIdentifier") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditReportNumberIdentifier_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditReportNumberIdentifierError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Print
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditPrint" runat="server" Checked='<%# Bind("Form_Print") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Email
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditEmail" runat="server" Checked='<%# Bind("Form_Email") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Admin
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditAdmin" runat="server" Checked='<%# Bind("Form_Admin") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditUnit" runat="server" Checked='<%# Bind("Form_Unit") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Cut of Day
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditCutOffDay" runat="server" Width="100px" Text='<%# Bind("Form_CutOffDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCutOffDay" runat="server" TargetControlID="TextBox_EditCutOffDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Maintenance
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditMaintenance" runat="server" Checked='<%# Bind("Form_Maintenance") %>' />&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Form_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Form_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Form_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Form_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Form_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Form_Form" runat="server" InsertCommand="INSERT INTO Administration_Form (Form_Name ,Form_Description ,Form_ReportNumberIdentifier ,Form_Print ,Form_Email ,Form_Admin ,Form_Unit ,Form_CutOffDay ,Form_Maintenance ,Form_CreatedDate ,Form_CreatedBy ,Form_ModifiedDate ,Form_ModifiedBy ,Form_History ,Form_IsActive) VALUES (@Form_Name ,@Form_Description ,@Form_ReportNumberIdentifier ,@Form_Print ,@Form_Email ,@Form_Admin ,@Form_Unit ,@Form_CutOffDay ,@Form_Maintenance ,@Form_CreatedDate ,@Form_CreatedBy ,@Form_ModifiedDate ,@Form_ModifiedBy ,@Form_History ,@Form_IsActive); SELECT @Form_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_Form WHERE (Form_Id = @Form_Id)" UpdateCommand="UPDATE Administration_Form SET Form_Name = @Form_Name ,Form_Description = @Form_Description ,Form_ReportNumberIdentifier = @Form_ReportNumberIdentifier ,Form_Print = @Form_Print ,Form_Email = @Form_Email ,Form_Admin = @Form_Admin ,Form_Unit = @Form_Unit ,Form_CutOffDay = @Form_CutOffDay ,Form_Maintenance = @Form_Maintenance ,Form_ModifiedDate = @Form_ModifiedDate ,Form_ModifiedBy = @Form_ModifiedBy ,Form_History = @Form_History ,Form_IsActive = @Form_IsActive WHERE Form_Id = @Form_Id" OnUpdated="SqlDataSource_Form_Form_Updated" OnInserted="SqlDataSource_Form_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="Form_Id" Type="Int32" />
                    <asp:Parameter Name="Form_Name" Type="String" />
                    <asp:Parameter Name="Form_Description" Type="String" />
                    <asp:Parameter Name="Form_ReportNumberIdentifier" Type="String" />
                    <asp:Parameter Name="Form_Print" Type="Boolean" />
                    <asp:Parameter Name="Form_Email" Type="Boolean" />
                    <asp:Parameter Name="Form_Admin" Type="Boolean" />
                    <asp:Parameter Name="Form_Unit" Type="Boolean" />
                    <asp:Parameter Name="Form_CutOffDay" Type="Int32" />
                    <asp:Parameter Name="Form_Maintenance" Type="Boolean" />
                    <asp:Parameter Name="Form_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="Form_CreatedBy" Type="String" />
                    <asp:Parameter Name="Form_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Form_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Form_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="Form_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Form_Id" QueryStringField="Form_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Form_Name" Type="String" />
                    <asp:Parameter Name="Form_Description" Type="String" />
                    <asp:Parameter Name="Form_ReportNumberIdentifier" Type="String" />
                    <asp:Parameter Name="Form_Print" Type="Boolean" />
                    <asp:Parameter Name="Form_Email" Type="Boolean" />
                    <asp:Parameter Name="Form_Admin" Type="Boolean" />
                    <asp:Parameter Name="Form_Unit" Type="Boolean" />
                    <asp:Parameter Name="Form_CutOffDay" Type="Int32" />
                    <asp:Parameter Name="Form_Maintenance" Type="Boolean" />
                    <asp:Parameter Name="Form_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Form_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Form_History" Type="String" />
                    <asp:Parameter Name="Form_IsActive" Type="Boolean" />
                    <asp:Parameter Name="Form_Id" Type="Int32" />
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
