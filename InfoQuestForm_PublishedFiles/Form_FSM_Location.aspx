<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_FSM_Location.aspx.cs" Inherits="InfoQuestForm.Form_FSM_Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Facility Structure Maintenance - Location</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_FSM_Location.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_FSM_Location" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_FSM_Location" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_FSM_Location" AssociatedUpdatePanelID="UpdatePanel_FSM_Location">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_FSM_Location" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">
                <asp:Label ID="Label_Title" runat="server" Text=""></asp:Label>
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableLocation" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_LocationHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_FSM_Location_Form" runat="server" DataKeyNames="LocationKey" CssClass="FormView" DataSourceID="SqlDataSource_FSM_Location_Form" OnItemInserting="FormView_FSM_Location_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_FSM_Location_Form_ItemCommand" OnDataBound="FormView_FSM_Location_Form_DataBound" OnItemUpdating="FormView_FSM_Location_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationName">Name
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:TextBox ID="TextBox_InsertLocationName" runat="server" Text='<%# Bind("LocationName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationAddress">Address
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:TextBox ID="TextBox_InsertLocationAddress" runat="server" Text='<%# Bind("LocationAddress") %>' CssClass="Controls_TextBox" TextMode="MultiLine" Rows="4" Width="700px"></asp:TextBox>
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCountry">Country
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_InsertCountry" runat="server" DataSourceID="SqlDataSource_FSM_Location_InsertCountry" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertCountry_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Country</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormProvinceKey">Province
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_InsertProvinceKey" runat="server" DataSourceID="SqlDataSource_FSM_Location_InsertProvinceKey" AppendDataBoundItems="true" DataTextField="ProvinceName" DataValueField="ProvinceKey" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Province</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" rowspan="2">GPS Co-ordinates
                        </td>
                        <td style="width: 180px;">
                          Longitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:TextBox ID="TextBox_InsertLongitude" runat="server" Text='<%# Bind("Longitude") %>' CssClass="Controls_TextBox" Width="150px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">
                          Latitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:TextBox ID="TextBox_InsertLatitude" runat="server" Text='<%# Bind("Latitude") %>' CssClass="Controls_TextBox" Width="150px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="3">
                          <asp:Button ID="Button_InsertGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_InsertGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Location" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationName">Name
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:TextBox ID="TextBox_EditLocationName" runat="server" Text='<%# Bind("LocationName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationAddress">Address
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:TextBox ID="TextBox_EditLocationAddress" runat="server" Text='<%# Bind("LocationAddress") %>' CssClass="Controls_TextBox" TextMode="MultiLine" Rows="4" Width="700px"></asp:TextBox>
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCountry">Country
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_EditCountry" runat="server" DataSourceID="SqlDataSource_FSM_Location_EditCountry" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditCountry_SelectedIndexChanged" OnDataBound="DropDownList_EditCountry_DataBound">
                            <asp:ListItem Value="">Select Country</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormProvinceKey">Province
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_EditProvinceKey" runat="server" DataSourceID="SqlDataSource_FSM_Location_EditProvinceKey" AppendDataBoundItems="true" DataTextField="ProvinceName" DataValueField="ProvinceKey" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Province</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" rowspan="2">GPS Co-ordinates
                        </td>
                        <td style="width: 180px;">
                          Longitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:TextBox ID="TextBox_EditLongitude" runat="server" Text='<%# Bind("Longitude") %>' CssClass="Controls_TextBox" Width="150px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">
                          Latitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:TextBox ID="TextBox_EditLatitude" runat="server" Text='<%# Bind("Latitude") %>' CssClass="Controls_TextBox" Width="150px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="2">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="3">
                          <asp:Button ID="Button_EditGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_EditGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Location" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3"></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Name
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_ItemLocationName" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Address
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_EditLocationAddress" runat="server" Text='<%# Bind("LocationAddress") %>'></asp:Label>
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCountry">Country
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_EditCountry" runat="server"></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormProvinceKey">Province
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_EditProvinceKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" rowspan="2">GPS Co-ordinates
                        </td>
                        <td style="width: 180px;">
                          Longitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:Label ID="Label_EditLongitude" runat="server" Text='<%# Bind("Longitude") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">
                          Latitude (DDD.DDDDD°)
                        </td>
                        <td style="width: 550px;">
                          <asp:Label ID="Label_EditLatitude" runat="server" Text='<%# Bind("Latitude") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="3">
                          <asp:Button ID="Button_ItemGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_ItemGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_FSM_Location_InsertCountry" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_Location_InsertProvinceKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_Location_EditCountry" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_Location_EditProvinceKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_Location_Form" runat="server" OnInserted="SqlDataSource_FSM_Location_Form_Inserted" OnUpdated="SqlDataSource_FSM_Location_Form_Updated"></asp:SqlDataSource>
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
