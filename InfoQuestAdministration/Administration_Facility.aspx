<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Facility" CodeBehind="Administration_Facility.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Facility</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Facility.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Facility" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Facility" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Facility" AssociatedUpdatePanelID="UpdatePanel_Facility">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Facility" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Facility</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Facility_Form" runat="server" DataKeyNames="Facility_Id" CssClass="FormView" DataSourceID="SqlDataSource_Facility_Form" OnItemInserting="FormView_Facility_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Facility_Form_ItemCommand" OnItemUpdating="FormView_Facility_Form_ItemUpdating">
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
                        <td style="width: 175px;" id="FormFacilityName">Facility Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertFacilityName" runat="server" Width="500px" Text='<%# Bind("Facility_FacilityName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertFacilityName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertFacilityNameError" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityCode">Facility Code
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertFacilityCode" runat="server" Width="500px" Text='<%# Bind("Facility_FacilityCode") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertFacilityCode_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertFacilityCodeError" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityType">Facility Type
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_InsertFacilityType" runat="server" DataSourceID="SqlDataSource_Facility_InsertFacilityType" AppendDataBoundItems="true" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id" SelectedValue='<%# Bind("Facility_Type_Lookup") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormImpiloUnitId">Impilo Unit Id
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertImpiloUnitId" runat="server" Width="500px" Text='<%# Bind("Facility_ImpiloUnitId") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormImpiloCountryId">Impilo Country Id
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertImpiloCountryId" runat="server" Width="500px" Text='<%# Bind("Facility_ImpiloCountryId") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormIMEDSConnectionString">IMeds ConnectionString
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertIMEDSConnectionString" runat="server" Width="500px" Text='<%# Bind("Facility_IMEDS_ConnectionString") %>' CssClass="Controls_TextBox"></asp:TextBox>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Facility_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Facility_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Facility_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Facility_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Facility_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Facility" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("Facility_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityName">Facility Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditFacilityName" runat="server" Width="500px" Text='<%# Bind("Facility_FacilityName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditFacilityName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditFacilityNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityCode">Facility Code
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditFacilityCode" runat="server" Width="500px" Text='<%# Bind("Facility_FacilityCode") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditFacilityCode_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditFacilityCodeError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityType">Facility Type
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_EditFacilityType" runat="server" DataSourceID="SqlDataSource_Facility_EditFacilityType" AppendDataBoundItems="true" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id" SelectedValue='<%# Bind("Facility_Type_Lookup") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormImpiloUnitId">Impilo Unit Id
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditImpiloUnitId" runat="server" Width="500px" Text='<%# Bind("Facility_ImpiloUnitId") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormImpiloCountryId">Impilo Country Id
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditImpiloCountryId" runat="server" Width="500px" Text='<%# Bind("Facility_ImpiloCountryId") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormIMEDSConnectionString">IMeds ConnectionString
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditIMEDSConnectionString" runat="server" Width="500px" Text='<%# Bind("Facility_IMEDS_ConnectionString") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Facility_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Facility_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Facility_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Facility_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Facility_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Facility" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Facility_Form" runat="server" InsertCommand="INSERT INTO Administration_Facility (Facility_FacilityName , Facility_FacilityCode , Facility_Type_Lookup , Facility_ImpiloUnitId , Facility_ImpiloCountryId , Facility_IMEDS_ConnectionString ,Facility_CreatedDate ,Facility_CreatedBy ,Facility_ModifiedDate ,Facility_ModifiedBy ,Facility_History ,Facility_IsActive) VALUES (@Facility_FacilityName , @Facility_FacilityCode , @Facility_Type_Lookup , @Facility_ImpiloUnitId , @Facility_ImpiloCountryId , @Facility_IMEDS_ConnectionString ,@Facility_CreatedDate ,@Facility_CreatedBy ,@Facility_ModifiedDate ,@Facility_ModifiedBy ,@Facility_History ,@Facility_IsActive); SELECT @Facility_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_Facility WHERE (Facility_Id = @Facility_Id)" UpdateCommand="UPDATE Administration_Facility SET Facility_FacilityName = @Facility_FacilityName , Facility_FacilityCode = @Facility_FacilityCode , Facility_Type_Lookup = @Facility_Type_Lookup , Facility_ImpiloUnitId = @Facility_ImpiloUnitId , Facility_ImpiloCountryId = @Facility_ImpiloCountryId ,Facility_IMEDS_ConnectionString = @Facility_IMEDS_ConnectionString ,Facility_ModifiedDate = @Facility_ModifiedDate ,Facility_ModifiedBy = @Facility_ModifiedBy ,Facility_History = @Facility_History ,Facility_IsActive = @Facility_IsActive WHERE Facility_Id = @Facility_Id" OnUpdated="SqlDataSource_Facility_Form_Updated" OnInserted="SqlDataSource_Facility_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="Facility_Id" Type="Int32" />
                    <asp:Parameter Name="Facility_FacilityName" Type="String" />
                    <asp:Parameter Name="Facility_FacilityCode" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup" Type="Int32" />
                    <asp:Parameter Name="Facility_ImpiloUnitId" Type="String" />
                    <asp:Parameter Name="Facility_ImpiloCountryId" Type="String" />
                    <asp:Parameter Name="Facility_IMEDS_ConnectionString" Type="String" />
                    <asp:Parameter Name="Facility_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_CreatedBy" Type="String" />
                    <asp:Parameter Name="Facility_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Facility_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="Facility_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Facility_Id" QueryStringField="Facility_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Facility_FacilityName" Type="String" />
                    <asp:Parameter Name="Facility_FacilityCode" Type="String" />
                    <asp:Parameter Name="Facility_Type_Lookup" Type="Int32" />
                    <asp:Parameter Name="Facility_ImpiloUnitId" Type="String" />
                    <asp:Parameter Name="Facility_ImpiloCountryId" Type="String" />
                    <asp:Parameter Name="Facility_IMEDS_ConnectionString" Type="String" />
                    <asp:Parameter Name="Facility_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Facility_ModifiedBy" Type="String" />
                    <asp:Parameter Name="Facility_History" Type="String" />
                    <asp:Parameter Name="Facility_IsActive" Type="Boolean" />
                    <asp:Parameter Name="Facility_Id" Type="Int32" />
                  </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Facility_InsertFacilityType" runat="server" SelectCommand="SELECT DISTINCT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name + ' (' + CASE WHEN Facility_Type_Lookup_IsActive = 1 THEN 'Yes' WHEN Facility_Type_Lookup_IsActive = 0 THEN 'No' END + ')' AS Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name + ' (' + CASE WHEN Facility_Type_Lookup_IsActive = 1 THEN 'Yes' WHEN Facility_Type_Lookup_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Facility_EditFacilityType" runat="server" SelectCommand="SELECT DISTINCT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name + ' (' + CASE WHEN Facility_Type_Lookup_IsActive = 1 THEN 'Yes' WHEN Facility_Type_Lookup_IsActive = 0 THEN 'No' END + ')' AS Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name + ' (' + CASE WHEN Facility_Type_Lookup_IsActive = 1 THEN 'Yes' WHEN Facility_Type_Lookup_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
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
