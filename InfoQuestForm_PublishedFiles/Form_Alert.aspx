<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Alert" CodeBehind="Form_Alert.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Alert</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Alert.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Alert" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Alert" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Alert" AssociatedUpdatePanelID="UpdatePanel_Alert">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Alert" runat="server">
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
          <table id="TableFacility" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FacilityHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 175px;">Facility
                    </td>
                    <td style="width: 825px;">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Alert_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Alert_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td>
                      <asp:Button ID="Button_Cancel" runat="server" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_Cancel_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TableForm" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_AlertHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Alert_Form" runat="server" Width="1000px" DataKeyNames="Alert_Id" CssClass="FormView" DataSourceID="SqlDataSource_Alert_Form" OnItemInserting="FormView_Alert_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Alert_Form_ItemCommand" OnDataBound="FormView_Alert_Form_DataBound" OnItemUpdating="FormView_Alert_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("Alert_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Alert_InsertFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertFacility_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityFrom">Facility Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertFacilityFrom" runat="server" DataSourceID="SqlDataSource_Alert_InsertFacilityFrom" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# Bind("Alert_FacilityFrom_Facility") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDate">Date of Alert<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("Alert_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormOriginator">Person Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertOriginator" runat="server" Width="300px" Text='<%# Bind("Alert_Originator") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitFromUnit">Unit Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertUnitFromUnit" runat="server" DataSourceID="SqlDataSource_Alert_InsertUnitFromUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Alert_UnitFrom_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitToUnit">Unit Issued To
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertUnitToUnit" runat="server" DataSourceID="SqlDataSource_Alert_InsertUnitToUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Alert_UnitTo_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Immediate Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Alert
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Alert_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormActionTaken">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertActionTaken" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Alert_ActionTaken") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel1List">Immediate Cause Level 1
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertLevel1List" runat="server" DataSourceID="SqlDataSource_Alert_InsertLevel1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Alert_Level1_List") %>' CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertLevel1List_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Level 1</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel2List">Immediate Cause Level 2
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertLevel2List" runat="server" DataSourceID="SqlDataSource_Alert_InsertLevel2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                            <asp:ListItem Value="">Select Level 2</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormNumberOfInstances">Number of Instances
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertNumberOfInstances" runat="server" Width="75px" Text='<%# Bind("Alert_NumberOfInstances") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertNumberOfInstances" runat="server" TargetControlID="TextBox_InsertNumberOfInstances" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertStatus" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Alert_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Alert_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Alert_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Alert_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Alert" CssClass="Controls_Button" ValidationGroup="Alert_Form" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" />&nbsp;
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
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("Alert_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityFrom">Facility Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditFacilityFrom" runat="server" DataSourceID="SqlDataSource_Alert_EditFacilityFrom" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# Bind("Alert_FacilityFrom_Facility") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDate">Date of Alert<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("Alert_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormOriginator">Person Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditOriginator" runat="server" Width="300px" Text='<%# Bind("Alert_Originator") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitFromUnit">Unit Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditUnitFromUnit" runat="server" DataSourceID="SqlDataSource_Alert_EditUnitFromUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Alert_UnitFrom_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitToUnit">Unit Issued To
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditUnitToUnit" runat="server" DataSourceID="SqlDataSource_Alert_EditUnitToUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Alert_UnitTo_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Immediate Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Alert
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Alert_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormActionTaken">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditActionTaken" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Alert_ActionTaken") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel1List">Immediate Cause Level 1
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditLevel1List" runat="server" DataSourceID="SqlDataSource_Alert_EditLevel1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Alert_Level1_List") %>' CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditLevel1List_SelectedIndexChanged" OnDataBound="DropDownList_EditLevel1List_DataBound">
                            <asp:ListItem Value="">Select Level 1</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel2List">Immediate Cause Level 2
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditLevel2List" runat="server" DataSourceID="SqlDataSource_Alert_EditLevel2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                            <asp:ListItem Value="">Select Level 2</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormNumberOfInstances">Number of Instances
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditNumberOfInstances" runat="server" Width="75px" Text='<%# Bind("Alert_NumberOfInstances") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditNumberOfInstances" runat="server" TargetControlID="TextBox_EditNumberOfInstances" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("Alert_Status") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                          </asp:DropDownList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditStatusDate" runat="server" Text='<%# Bind("Alert_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="AlertStatusRejectedReason">
                        <td style="width: 175px;" id="FormStatusRejectedReason">Rejected Reason
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditStatusRejectedReason" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Alert_StatusRejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Alert_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Alert_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Alert_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Alert_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print Alert" CssClass="Controls_Button" ValidationGroup="Alert_Form" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" ValidationGroup="Alert_Form" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Alert" CssClass="Controls_Button" ValidationGroup="Alert_Form" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("Alert_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFacilityFrom" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date of Alert<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("Alert_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Person Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemOriginator" runat="server" Text='<%# Bind("Alert_Originator") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit Originated From
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemUnitFromUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit Issued To
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemUnitToUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Immediate Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Description of Alert
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemDescription" runat="server" Text='<%# Bind("Alert_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Alert_ActionTaken") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Immediate Cause Level 1
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemLevel1List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Immediate Cause Level 2
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemLevel2List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Number of Instances
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("Alert_NumberOfInstances") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemStatus" runat="server" Text='<%# Bind("Alert_Status") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemStatus" runat="server" Value='<%# Bind("Alert_Status") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemStatusDate" runat="server" Text='<%# Bind("Alert_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="AlertStatusRejectedReason">
                        <td style="width: 175px;">Rejected Reason
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemStatusRejectedReason" runat="server" Text='<%# Bind("Alert_StatusRejectedReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Alert_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Alert_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Alert_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Alert_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Alert" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemClear_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertFacility" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertFacilityFrom" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertUnitFromUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertUnitToUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertLevel1List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_InsertLevel2List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_EditFacilityFrom" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_EditUnitFromUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_EditUnitToUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_EditLevel1List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_EditLevel2List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Alert_Form" runat="server" OnInserted="SqlDataSource_Alert_Form_Inserted" OnUpdated="SqlDataSource_Alert_Form_Updated"></asp:SqlDataSource>
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
