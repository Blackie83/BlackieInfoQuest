<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_CRM" CodeBehind="Form_CRM.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Customer Relationship Management</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_CRM.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CRM" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CRM" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CRM" AssociatedUpdatePanelID="UpdatePanel_CRM">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CRM" runat="server">
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
          <table id="TableForm" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FormHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_CRM_Form" runat="server" Width="900px" DataKeyNames="CRM_Id" CssClass="FormView" DataSourceID="SqlDataSource_CRM_Form" OnItemInserting="FormView_CRM_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_CRM_Form_ItemCommand" OnDataBound="FormView_CRM_Form_DataBound" OnItemUpdating="FormView_CRM_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertSecurityRole" runat="server" OnDataBinding="HiddenField_InsertSecurityRole_DataBinding" />
                          <asp:HiddenField ID="HiddenField_InsertCRMIdTemp" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertAdmin" runat="server" OnDataBinding="HiddenField_InsertAdmin_DataBinding" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" DataSourceID="SqlDataSource_CRM_InsertFacility" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertFacility_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateReceived">Date Received<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDateReceived" runat="server" Width="75px" Text='<%# Bind("CRM_DateReceived","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDateReceived" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDateReceived" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDateReceived" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDateReceived">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDateReceived" runat="server" TargetControlID="TextBox_InsertDateReceived" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date Forwarded to Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertDateForwarded" runat="server" Text='<%# Bind("CRM_DateForwarded","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormOriginatedAtList">Originated at
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertOriginatedAtList" runat="server" DataSourceID="SqlDataSource_CRM_InsertOriginatedAtList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_OriginatedAt_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Originated</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_InsertOriginatedAtList" runat="server"></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTypeList">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertTypeList" runat="server" DataSourceID="SqlDataSource_CRM_InsertTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedViaList">Received Via
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReceivedViaList" runat="server" DataSourceID="SqlDataSource_CRM_InsertReceivedViaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_ReceivedVia_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Via</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedFromList">Received From
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReceivedFromList" runat="server" DataSourceID="SqlDataSource_CRM_InsertReceivedFromList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_ReceivedFrom_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select From</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated1">
                        <td style="width: 175px;">Escalated form
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertEscalatedForm" runat="server" Checked='<%# Bind("CRM_EscalatedForm") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated2">
                        <td style="width: 175px;">Report Number of Original Complaint<br />
                          (e.g. 60/CRM/2011/00161)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertEscalatedReportNumber" runat="server" Width="300px" Text='<%# Bind("CRM_EscalatedReportNumber") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertEscalatedReportNumber_TextChanged"></asp:TextBox>
                          <Ajax:AutoCompleteExtender ID="AutoCompleteExtender_InsertEscalatedReportNumber" runat="server" TargetControlID="TextBox_InsertEscalatedReportNumber" ServiceMethod="ServiceMethod_InsertEscalatedReportNumber" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="10" EnableCaching="true" UseContextKey="True">
                          </Ajax:AutoCompleteExtender>
                          &nbsp;
                        <br />
                          <asp:Label ID="Label_InsertEscalatedReportNumberError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="CustomerInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="CustomerInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Customer Information
                        </td>
                      </tr>
                      <tr id="CustomerInformation3">
                        <td style="width: 175px;" id="FormCustomerName">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCustomerName" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCustomerName" runat="server" TargetControlID="TextBox_InsertCustomerName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CustomerInformation4">
                        <td style="width: 175px;" id="FormCustomerEmail">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCustomerEmail" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerEmail") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertCustomerEmail_TextChanged"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCustomerEmail" runat="server" TargetControlID="TextBox_InsertCustomerEmail" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <br />
                          <asp:Label ID="Label_InsertCustomerEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="CustomerInformation5">
                        <td style="width: 175px;" id="FormCustomerContactNumber">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCustomerContactNumber" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCustomerContactNumber" runat="server" TargetControlID="TextBox_InsertCustomerContactNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Information
                        </td>
                      </tr>
                      <tr id="PatientInformation3">
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientVisitNumber" runat="server" Width="200px" Text='<%# Bind("CRM_PatientVisitNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientVisitNumber" runat="server" TargetControlID="TextBox_InsertPatientVisitNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <asp:Button ID="Button_InsertFindPatient" runat="server" OnClick="Button_InsertFindPatient_OnClick" Text="Find Patient Information" CssClass="Controls_Button" />&nbsp;
                          <br />
                          <asp:Label ID="Label_InsertPatientError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientInformation4">
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientName" runat="server" Width="300px" Text='<%# Bind("CRM_PatientName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertPatientName_TextChanged"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientName" runat="server" TargetControlID="TextBox_InsertPatientName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation5">
                        <td style="width: 175px;" id="FormPatientDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientDateOfAdmission" runat="server" Width="300px" Text='<%# Bind("CRM_PatientDateOfAdmission") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientDateOfAdmission" runat="server" TargetControlID="TextBox_InsertPatientDateOfAdmission" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation6">
                        <td style="width: 175px;" id="FormPatientEmail">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientEmail" runat="server" Width="300px" Text='<%# Bind("CRM_PatientEmail") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertPatientEmail_TextChanged"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientEmail" runat="server" TargetControlID="TextBox_InsertPatientEmail" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <br />
                          <asp:Label ID="Label_InsertPatientEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientInformation7">
                        <td style="width: 175px;" id="FormPatientContactNumber">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientContactNumber" runat="server" Width="300px" Text='<%# Bind("CRM_PatientContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientContactNumber" runat="server" TargetControlID="TextBox_InsertPatientContactNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Detail
                        </td>
                      </tr>
                      <tr id="ComplaintDetail2">
                        <td style="width: 175px;" id="FormComplaintDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Complaint_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertComplaintDescription" runat="server" TargetControlID="TextBox_InsertComplaintDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail4">
                        <td style="width: 175px;" id="FormComplaintContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Complaint_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail5">
                        <td style="width: 175px;" id="FormComplaintUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertComplaintUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail6">
                        <td style="width: 175px;" id="FormComplaintDateOccurred">Date Complaint Occurred<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintDateOccurred" runat="server" Width="75px" Text='<%# Bind("CRM_Complaint_DateOccurred","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertComplaintDateOccurred" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                        <Ajax:CalendarExtender ID="CalendarExtender_InsertComplaintDateOccurred" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertComplaintDateOccurred" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertComplaintDateOccurred">
                        </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertComplaintDateOccurred" runat="server" TargetControlID="TextBox_InsertComplaintDateOccurred" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail7">
                        <td style="width: 175px;" id="FormComplaintTimeOccured">Time Complaint Occurred<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintTimeOccuredHours" runat="server" SelectedValue='<%# Bind("CRM_Complaint_TimeOccuredHours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Hour</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;:&nbsp;
                          <asp:DropDownList ID="DropDownList_InsertComplaintTimeOccuredMinutes" runat="server" SelectedValue='<%# Bind("CRM_Complaint_TimeOccuredMinutes") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Minute</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="49">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail8">
                        <td style="width: 175px;" id="FormComplaintPriorityList">Complaint Priority
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintPriorityList" runat="server" DataSourceID="SqlDataSource_CRM_InsertComplaintPriorityList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Complaint_Priority_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertComplaintPriorityList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Priority</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail9">
                        <td style="width: 175px;" id="FormComplaintCategoryItemList">Complaint Category
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="InsertComplaintCategoryItemList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_InsertComplaintCategoryItemList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_CRM_InsertComplaintCategoryItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_InsertComplaintCategoryItemListTotal" runat="server" OnDataBinding="HiddenField_InsertComplaintCategoryItemListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="ComplaintDetail10">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail11">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Investigation
                        </td>
                      </tr>
                      <tr id="ComplaintDetail12">
                        <td style="width: 175px;" id="FormComplaintWithin24Hours">Acknowledged within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintWithin24Hours" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within24Hours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail13">
                        <td style="width: 175px;" id="FormComplaintWithin24HoursReason">Reason for not acknowledging within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintWithin24HoursReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within24HoursReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail14">
                        <td style="width: 175px;">Method used to acknowledge within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintWithin24HoursMethodList" runat="server" DataSourceID="SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Complaint_Within24HoursMethod_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Method</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail15">
                        <td style="width: 175px;" id="FormComplaintWithin5Days">Follow up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintWithin5Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within5Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail16">
                        <td style="width: 175px;" id="FormComplaintWithin5DaysReason">Reason for not following up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintWithin5DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within5DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail17">
                        <td style="width: 175px;" id="FormComplaintWithin10Days">Close out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintWithin10Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within10Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail18">
                        <td style="width: 175px;" id="FormComplaintWithin10DaysReason">Reason for not closing out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintWithin10DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within10DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail19">
                        <td style="width: 175px;" id="FormComplaintWithin3Days">Close out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintWithin3Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within3Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail20">
                        <td style="width: 175px;" id="FormComplaintWithin3DaysReason">Reason for not closing out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintWithin3DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within3DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail21">
                        <td style="width: 175px;" id="FormComplaintCustomerSatisfied">Customer satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplaintCustomerSatisfied" runat="server" SelectedValue='<%# Bind("CRM_Complaint_CustomerSatisfied") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail22">
                        <td style="width: 175px;" id="FormComplaintCustomerSatisfiedReason">Reason for customer not being satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintCustomerSatisfiedReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_CustomerSatisfiedReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail23">
                        <td style="width: 175px;" id="FormComplaintInvestigatorName">Investigator name
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintInvestigatorName" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_InvestigatorName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail24">
                        <td style="width: 175px;" id="FormComplaintInvestigatorDesignation">Investigator designation
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintInvestigatorDesignation" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_InvestigatorDesignation") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail25">
                        <td style="width: 175px;" id="FormComplaintRootCause">Root Cause
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintRootCause" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_RootCause") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail26">
                        <td style="width: 175px;">Action
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplaintAction" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Action") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail27">
                        <td colspan="4">Complaint Close Out can only be done for P1 by Hospital Manager and P2 by NSM
                        </td>
                      </tr>
                      <tr id="ComplaintDetail28">
                        <td style="width: 175px;" id="FormComplaintCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertComplaintCloseOut" runat="server" Checked='<%# Bind("CRM_Complaint_CloseOut") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail29">
                        <td style="width: 175px;" id="FormComplaintCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplaintCloseOutDate" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail30">
                        <td style="width: 175px;" id="FormComplaintCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplaintCloseOutBy" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Compliment Detail
                        </td>
                      </tr>
                      <tr id="ComplimentDetail2">
                        <td style="width: 175px;" id="FormComplimentDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertComplimentDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Compliment_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertComplimentDescription" runat="server" TargetControlID="TextBox_InsertComplimentDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail10">
                        <td style="width: 175px;" id="FormComplimentContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplimentContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Compliment_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail3">
                        <td style="width: 175px;" id="FormComplimentUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertComplimentUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertComplimentUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail4">
                        <td style="width: 175px;" id="FormComplimentAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertComplimentAcknowledge" runat="server" Checked='<%# Bind("CRM_Compliment_Acknowledge") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail5">
                        <td style="width: 175px;" id="FormComplimentAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplimentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail6">
                        <td style="width: 175px;" id="FormComplimentAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplimentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail7">
                        <td style="width: 175px;" id="FormComplimentCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertComplimentCloseOut" runat="server" Checked='<%# Bind("CRM_Compliment_CloseOut") %>' />
                          <asp:Label ID="Label_InsertComplimentCloseOut" runat="server" Text="&nbsp;"></asp:Label>
                        </td>
                      </tr>
                      <tr id="ComplimentDetail8">
                        <td style="width: 175px;" id="FormComplimentCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplimentCloseOutDate" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail9">
                        <td style="width: 175px;" id="FormComplimentCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertComplimentCloseOutBy" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Comment Detail
                        </td>
                      </tr>
                      <tr id="CommentDetail2">
                        <td style="width: 175px;" id="FormCommentDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCommentDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Comment_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCommentDescription" runat="server" TargetControlID="TextBox_InsertCommentDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail14">
                        <td style="width: 175px;" id="FormCommentContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertCommentContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Comment_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail3">
                        <td style="width: 175px;" id="FormCommentTypeList">Type
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentTypeList" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;" id="FormCommentAdditionalTypeList">Type 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentAdditionalTypeList" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentAdditionalTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalType_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail4">
                        <td style="width: 175px;" id="FormCommentUnitId">Unit
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;" id="FormCommentAdditionalUnitId">Unit 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentAdditionalUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentAdditionalUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail5">
                        <td style="width: 175px;">Category
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentCategoryList" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Category_List") %>' CssClass="Controls_DropDownList" Width="250px">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;">Category 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_InsertCommentAdditionalCategoryList" runat="server" DataSourceID="SqlDataSource_CRM_InsertCommentAdditionalCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalCategory_List") %>' CssClass="Controls_DropDownList" Width="250px">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail6">
                        <td style="width: 175px;" id="FormCommentAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertCommentAcknowledge" runat="server" Checked='<%# Bind("CRM_Comment_Acknowledge") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail7">
                        <td style="width: 175px;" id="FormCommentAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertCommentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail8">
                        <td style="width: 175px;" id="FormCommentAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertCommentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail9">
                        <td style="width: 175px;" id="FormCommentCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertCommentCloseOut" runat="server" Checked='<%# Bind("CRM_Comment_CloseOut") %>' />
                          <asp:Label ID="Label_InsertCommentCloseOut" runat="server" Text="&nbsp;"></asp:Label>
                        </td>
                      </tr>
                      <tr id="CommentDetail10">
                        <td style="width: 175px;" id="FormCommentCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertCommentCloseOutDate" runat="server" Text='<%# Bind("CRM_Comment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail11">
                        <td style="width: 175px;" id="FormCommentCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertCommentCloseOutBy" runat="server" Text='<%# Bind("CRM_Comment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Enquiry Detail
                        </td>
                      </tr>
                      <tr id="QueryDetail2">
                        <td style="width: 175px;" id="FormQueryDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertQueryDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Query_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertQueryDescription" runat="server" TargetControlID="TextBox_InsertQueryDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail10">
                        <td style="width: 175px;" id="FormQueryContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertQueryContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Query_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail3">
                        <td style="width: 175px;" id="FormQueryUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertQueryUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertQueryUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail4">
                        <td style="width: 175px;" id="FormQueryAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertQueryAcknowledge" runat="server" Checked='<%# Bind("CRM_Query_Acknowledge") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail5">
                        <td style="width: 175px;" id="FormQueryAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertQueryAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail6">
                        <td style="width: 175px;" id="FormQueryAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertQueryAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail7">
                        <td style="width: 175px;" id="FormQueryCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertQueryCloseOut" runat="server" Checked='<%# Bind("CRM_Query_CloseOut") %>' />
                          <asp:Label ID="Label_InsertQueryCloseOut" runat="server" Text="&nbsp;"></asp:Label>
                        </td>
                      </tr>
                      <tr id="QueryDetail8">
                        <td style="width: 175px;" id="FormQueryCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertQueryCloseOutDate" runat="server" Text='<%# Bind("CRM_Query_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail9">
                        <td style="width: 175px;" id="FormQueryCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertQueryCloseOutBy" runat="server" Text='<%# Bind("CRM_Query_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Suggestion Detail
                        </td>
                      </tr>
                      <tr id="SuggestionDetail2">
                        <td style="width: 175px;" id="FormSuggestionDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertSuggestionDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Suggestion_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertSuggestionDescription" runat="server" TargetControlID="TextBox_InsertSuggestionDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail10">
                        <td style="width: 175px;" id="FormSuggestionContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSuggestionContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Suggestion_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail3">
                        <td style="width: 175px;" id="FormSuggestionUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSuggestionUnitId" runat="server" DataSourceID="SqlDataSource_CRM_InsertSuggestionUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail4">
                        <td style="width: 175px;" id="FormSuggestionAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertSuggestionAcknowledge" runat="server" Checked='<%# Bind("CRM_Suggestion_Acknowledge") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail5">
                        <td style="width: 175px;" id="FormSuggestionAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertSuggestionAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail6">
                        <td style="width: 175px;" id="FormSuggestionAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertSuggestionAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail7">
                        <td style="width: 175px;" id="FormSuggestionCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertSuggestionCloseOut" runat="server" Checked='<%# Bind("CRM_Suggestion_CloseOut") %>' />
                          <asp:Label ID="Label_InsertSuggestionCloseOut" runat="server" Text="&nbsp;"></asp:Label>
                        </td>
                      </tr>
                      <tr id="SuggestionDetail8">
                        <td style="width: 175px;" id="FormSuggestionCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertSuggestionCloseOutDate" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail9">
                        <td style="width: 175px;" id="FormSuggestionCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertSuggestionCloseOutBy" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">Upload Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">When uploading a document<br />
                          Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff), TXT (.txt), Outlook (.msg), Images (.jpeg / .jpg / .gif / .png)<br />
                          Only files smaller then 5 MB can be uploaded
                        <asp:HiddenField ID="HiddenField_InsertFile" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertMessageFile" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">File
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:FileUpload ID="FileUpload_InsertFile" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="text-align: center;">
                          <asp:Button ID="Button_InsertUploadFile" runat="server" OnClick="Button_InsertUploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_InsertFile" OnDataBinding="Button_InsertUploadFile_DataBinding" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_InsertFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_File_InsertFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_InsertFile_RowCreated" OnPreRender="GridView_InsertFile_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>
                                    <asp:Button ID="Button_InsertDeleteFile" runat="server" OnClick="Button_InsertDeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" OnDataBinding="Button_InsertDeleteFile_DataBinding" />&nbsp;
                                  <asp:Button ID="Button_InsertDeleteAllFile" runat="server" OnClick="Button_InsertDeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" OnDataBinding="Button_InsertDeleteAllFile_DataBinding" />&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Files
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertFile" runat="server" CssClass='<%# Eval("CRM_File_Id") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Uploaded File">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_InsertFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("CRM_File_Name")) %>' CommandArgument='<%# Eval("CRM_File_Id") %>' OnDataBinding="LinkButton_InsertFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="CRM_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="CRM_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertStatus" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;" colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("CRM_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("CRM_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("CRM_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Form" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertIncompleteOther" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Bulk Forms" CssClass="Controls_Button" OnClick="Button_InsertIncompleteOther_Click" />&nbsp;
                          <asp:Button ID="Button_InsertIncompleteComplaints" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Incomplete Complaints" CssClass="Controls_Button" OnClick="Button_InsertIncompleteComplaints_Click" />&nbsp;
                          <asp:Button ID="Button_InsertCaptured" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_InsertCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditSecurityRole" runat="server" OnDataBinding="HiddenField_EditSecurityRole_DataBinding" />
                          <asp:HiddenField ID="HiddenField_EditAdmin" runat="server" OnDataBinding="HiddenField_EditAdmin_DataBinding" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateReceived">Date Received<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDateReceived" runat="server" Width="75px" Text='<%# Bind("CRM_DateReceived","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDateReceived" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                        <Ajax:CalendarExtender ID="CalendarExtender_EditDateReceived" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDateReceived" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDateReceived">
                        </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDateReceived" runat="server" TargetControlID="TextBox_EditDateReceived" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date Forwarded to Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditDateForwarded" runat="server" Text='<%# Bind("CRM_DateForwarded","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormOriginatedAtList">Originated at
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditOriginatedAtList" runat="server" DataSourceID="SqlDataSource_CRM_EditOriginatedAtList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_OriginatedAt_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Originated</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditOriginatedAtList" runat="server"></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTypeList">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditTypeList" runat="server" DataSourceID="SqlDataSource_CRM_EditTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedViaList">Received Via
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReceivedViaList" runat="server" DataSourceID="SqlDataSource_CRM_EditReceivedViaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_ReceivedVia_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Via</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedFromList">Received From
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReceivedFromList" runat="server" DataSourceID="SqlDataSource_CRM_EditReceivedFromList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_ReceivedFrom_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select From</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated1">
                        <td style="width: 175px;">Escalated form
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditEscalatedForm" runat="server" Checked='<%# Bind("CRM_EscalatedForm") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated2">
                        <td style="width: 175px;">Report Number of Original Complaint<br />
                          (e.g. 60/CRM/2011/00161)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditEscalatedReportNumber" runat="server" Width="300px" Text='<%# Bind("CRM_EscalatedReportNumber") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditEscalatedReportNumber_TextChanged"></asp:TextBox>
                          <Ajax:AutoCompleteExtender ID="AutoCompleteExtender_EditEscalatedReportNumber" runat="server" TargetControlID="TextBox_EditEscalatedReportNumber" ServiceMethod="ServiceMethod_EditEscalatedReportNumber" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="10" EnableCaching="true" UseContextKey="True">
                          </Ajax:AutoCompleteExtender>
                          &nbsp;
                        <br />
                          <asp:Label ID="Label_EditEscalatedReportNumberError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="CustomerInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="CustomerInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Customer Information
                        </td>
                      </tr>
                      <tr id="CustomerInformation3">
                        <td style="width: 175px;" id="FormCustomerName">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCustomerName" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCustomerName" runat="server" TargetControlID="TextBox_EditCustomerName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CustomerInformation4">
                        <td style="width: 175px;" id="FormCustomerEmail">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCustomerEmail" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerEmail") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EditCustomerEmail_TextChanged"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCustomerEmail" runat="server" TargetControlID="TextBox_EditCustomerEmail" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <br />
                          <asp:Label ID="Label_EditCustomerEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="CustomerInformation5">
                        <td style="width: 175px;" id="FormCustomerContactNumber">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCustomerContactNumber" runat="server" Width="300px" Text='<%# Bind("CRM_CustomerContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCustomerContactNumber" runat="server" TargetControlID="TextBox_EditCustomerContactNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Information
                        </td>
                      </tr>
                      <tr id="PatientInformation3">
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientVisitNumber" runat="server" Width="200px" Text='<%# Bind("CRM_PatientVisitNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientVisitNumber" runat="server" TargetControlID="TextBox_EditPatientVisitNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <asp:Button ID="Button_EditFindPatient" runat="server" OnClick="Button_EditFindPatient_OnClick" Text="Find Patient Information" CssClass="Controls_Button" />&nbsp;
                          <br />
                          <asp:Label ID="Label_EditPatientError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientInformation4">
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientName" runat="server" Width="300px" Text='<%# Bind("CRM_PatientName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditPatientName_TextChanged"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientName" runat="server" TargetControlID="TextBox_EditPatientName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation5">
                        <td style="width: 175px;" id="FormPatientDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientDateOfAdmission" runat="server" Width="300px" Text='<%# Bind("CRM_PatientDateOfAdmission") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientDateOfAdmission" runat="server" TargetControlID="TextBox_EditPatientDateOfAdmission" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientInformation6">
                        <td style="width: 175px;" id="FormPatientEmail">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientEmail" runat="server" Width="300px" Text='<%# Bind("CRM_PatientEmail") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EditPatientEmail_TextChanged"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientEmail" runat="server" TargetControlID="TextBox_EditPatientEmail" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                          <br />
                          <asp:Label ID="Label_EditPatientEmailError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientInformation7">
                        <td style="width: 175px;" id="FormPatientContactNumber">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientContactNumber" runat="server" Width="300px" Text='<%# Bind("CRM_PatientContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientContactNumber" runat="server" TargetControlID="TextBox_EditPatientContactNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#"></Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="SurveyResults1" runat="server">
                        <td colspan="4" class="FormView_TableBodyHeader">Survey Results
                        </td>
                      </tr>
                      <tr id="SurveyResults2" style="text-align: center;" runat="server">
                        <td colspan="4">
                          <asp:Button ID="Button_EditSurveyResults" runat="server" OnClick="Button_EditSurveyResults_OnClick" Text="Survey Results" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="SurveyResults3" runat="server">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Detail
                        </td>
                      </tr>
                      <tr id="ComplaintDetail2">
                        <td style="width: 175px;" id="FormComplaintDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Complaint_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditComplaintDescription" runat="server" TargetControlID="TextBox_EditComplaintDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail4">
                        <td style="width: 175px;" id="FormComplaintContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Complaint_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail5">
                        <td style="width: 175px;" id="FormComplaintUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditComplaintUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail6">
                        <td style="width: 175px;" id="FormComplaintDateOccurred">Date Complaint Occurred<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintDateOccurred" runat="server" Width="75px" Text='<%# Bind("CRM_Complaint_DateOccurred","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditComplaintDateOccurred" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                        <Ajax:CalendarExtender ID="CalendarExtender_EditComplaintDateOccurred" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditComplaintDateOccurred" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditComplaintDateOccurred">
                        </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditComplaintDateOccurred" runat="server" TargetControlID="TextBox_EditComplaintDateOccurred" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail7">
                        <td style="width: 175px;" id="FormComplaintTimeOccured">Time Complaint Occurred<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintTimeOccuredHours" runat="server" SelectedValue='<%# Bind("CRM_Complaint_TimeOccuredHours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Hour</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;:&nbsp;
                          <asp:DropDownList ID="DropDownList_EditComplaintTimeOccuredMinutes" runat="server" SelectedValue='<%# Bind("CRM_Complaint_TimeOccuredMinutes") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Minute</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="49">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail8">
                        <td style="width: 175px;" id="FormComplaintPriorityList">Complaint Priority
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintPriorityList" runat="server" DataSourceID="SqlDataSource_CRM_EditComplaintPriorityList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Complaint_Priority_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditComplaintPriorityList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Priority</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail9">
                        <td style="width: 175px;" id="FormComplaintCategoryItemList">Complaint Category
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 0px; border-top-width: 1px;" colspan="3">
                          <div style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_EditComplaintCategoryItemList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_CRM_EditComplaintCategoryItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" OnDataBound="CheckBoxList_EditComplaintCategoryItemList_DataBound">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_EditComplaintCategoryItemListTotal" runat="server" OnDataBinding="HiddenField_EditComplaintCategoryItemListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="ComplaintDetail10">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail11">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Investigation
                        </td>
                      </tr>
                      <tr id="ComplaintDetail12">
                        <td style="width: 175px;" id="FormComplaintWithin24Hours">Acknowledged within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintWithin24Hours" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within24Hours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail13">
                        <td style="width: 175px;" id="FormComplaintWithin24HoursReason">Reason for not acknowledging within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintWithin24HoursReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within24HoursReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail14">
                        <td style="width: 175px;">Method used to acknowledge within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintWithin24HoursMethodList" runat="server" DataSourceID="SqlDataSource_CRM_EditComplaintWithin24HoursMethodList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Complaint_Within24HoursMethod_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Method</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail15">
                        <td style="width: 175px;" id="FormComplaintWithin5Days">Follow up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintWithin5Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within5Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail16">
                        <td style="width: 175px;" id="FormComplaintWithin5DaysReason">Reason for not following up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintWithin5DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within5DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail17">
                        <td style="width: 175px;" id="FormComplaintWithin10Days">Close out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintWithin10Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within10Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail18">
                        <td style="width: 175px;" id="FormComplaintWithin10DaysReason">Reason for not closing out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintWithin10DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within10DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail19">
                        <td style="width: 175px;" id="FormComplaintWithin3Days">Close out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintWithin3Days" runat="server" SelectedValue='<%# Bind("CRM_Complaint_Within3Days") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail20">
                        <td style="width: 175px;" id="FormComplaintWithin3DaysReason">Reason for not closing out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintWithin3DaysReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Within3DaysReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail21">
                        <td style="width: 175px;" id="FormComplaintCustomerSatisfied">Customer satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplaintCustomerSatisfied" runat="server" SelectedValue='<%# Bind("CRM_Complaint_CustomerSatisfied") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail22">
                        <td style="width: 175px;" id="FormComplaintCustomerSatisfiedReason">Reason for customer not being satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintCustomerSatisfiedReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_CustomerSatisfiedReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail23">
                        <td style="width: 175px;" id="FormComplaintInvestigatorName">Investigator name
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintInvestigatorName" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_InvestigatorName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail24">
                        <td style="width: 175px;" id="FormComplaintInvestigatorDesignation">Investigator designation
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintInvestigatorDesignation" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_InvestigatorDesignation") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail25">
                        <td style="width: 175px;" id="FormComplaintRootCause">Root Cause
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintRootCause" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_RootCause") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail26">
                        <td style="width: 175px;">Action
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplaintAction" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_Complaint_Action") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail27">
                        <td colspan="4">Complaint Close Out can only be done for P1 by Hospital Manager and P2 by NSM
                        </td>
                      </tr>
                      <tr id="ComplaintDetail28">
                        <td style="width: 175px;" id="FormComplaintCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditComplaintCloseOut" runat="server" Checked='<%# Bind("CRM_Complaint_CloseOut") %>' />&nbsp;
                          <asp:Label ID="Label_EditComplaintCloseOut" runat="server" Text="&nbsp;"></asp:Label>
                        </td>
                      </tr>
                      <tr id="ComplaintDetail29">
                        <td style="width: 175px;" id="FormComplaintCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplaintCloseOutDate" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail30">
                        <td style="width: 175px;" id="FormComplaintCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplaintCloseOutBy" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Compliment Detail
                        </td>
                      </tr>
                      <tr id="ComplimentDetail2">
                        <td style="width: 175px;" id="FormComplimentDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditComplimentDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Compliment_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditComplimentDescription" runat="server" TargetControlID="TextBox_EditComplimentDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail10">
                        <td style="width: 175px;" id="FormComplimentContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplimentContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Compliment_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail3">
                        <td style="width: 175px;" id="FormComplimentUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditComplimentUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditComplimentUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail4">
                        <td style="width: 175px;" id="FormComplimentAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditComplimentAcknowledge" runat="server" Checked='<%# Bind("CRM_Compliment_Acknowledge") %>' />&nbsp;
                          <asp:Label ID="Label_EditComplimentAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Compliment_Acknowledge"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="ComplimentDetail5">
                        <td style="width: 175px;" id="FormComplimentAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplimentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail6">
                        <td style="width: 175px;" id="FormComplimentAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplimentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail7">
                        <td style="width: 175px;" id="FormComplimentCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditComplimentCloseOut" runat="server" Checked='<%# Bind("CRM_Compliment_CloseOut") %>' />
                          <asp:Label ID="Label_EditComplimentCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Compliment_CloseOut"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="ComplimentDetail8">
                        <td style="width: 175px;" id="FormComplimentCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplimentCloseOutDate" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail9">
                        <td style="width: 175px;" id="FormComplimentCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditComplimentCloseOutBy" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Comment Detail
                        </td>
                      </tr>
                      <tr id="CommentDetail12" style="text-align: center;">
                        <td colspan="4">
                          <asp:Button ID="Button_EditConvertToComplaint" runat="server" OnClick="Button_EditConvertToComplaint_OnClick" Text="Convert To Complaint" CssClass="Controls_Button" OnDataBinding="Button_EditConvertToComplaint_DataBinding" />&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail2">
                        <td style="width: 175px;" id="FormCommentDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCommentDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Comment_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCommentDescription" runat="server" TargetControlID="TextBox_EditCommentDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail14">
                        <td style="width: 175px;" id="FormCommentContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditCommentContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Comment_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail3">
                        <td style="width: 175px;" id="FormCommentTypeList">Type
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentTypeList" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;" id="FormCommentAdditionalTypeList">Type 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentAdditionalTypeList" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalType_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail4">
                        <td style="width: 175px;" id="FormCommentUnitId">Unit
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;" id="FormCommentAdditionalUnitId">Unit 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentAdditionalUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail5">
                        <td style="width: 175px;">Category
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentCategoryList" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Category_List") %>' CssClass="Controls_DropDownList" Width="250px">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                        <td style="width: 70px;">Category 2
                        </td>
                        <td style="width: 320px;">
                          <asp:DropDownList ID="DropDownList_EditCommentAdditionalCategoryList" runat="server" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalCategory_List") %>' CssClass="Controls_DropDownList" Width="250px">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail6">
                        <td style="width: 175px;" id="FormCommentAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditCommentAcknowledge" runat="server" Checked='<%# Bind("CRM_Comment_Acknowledge") %>' />&nbsp;
                          <asp:Label ID="Label_EditCommentAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Comment_Acknowledge"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="CommentDetail7">
                        <td style="width: 175px;" id="FormCommentAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditCommentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail8">
                        <td style="width: 175px;" id="FormCommentAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditCommentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail9">
                        <td style="width: 175px;" id="FormCommentCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditCommentCloseOut" runat="server" Checked='<%# Bind("CRM_Comment_CloseOut") %>' />
                          <asp:Label ID="Label_EditCommentCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Comment_CloseOut"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="CommentDetail10">
                        <td style="width: 175px;" id="FormCommentCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditCommentCloseOutDate" runat="server" Text='<%# Bind("CRM_Comment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail11">
                        <td style="width: 175px;" id="FormCommentCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditCommentCloseOutBy" runat="server" Text='<%# Bind("CRM_Comment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Enquiry Detail
                        </td>
                      </tr>
                      <tr id="QueryDetail2">
                        <td style="width: 175px;" id="FormQueryDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditQueryDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Query_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditQueryDescription" runat="server" TargetControlID="TextBox_EditQueryDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail10">
                        <td style="width: 175px;" id="FormQueryContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditQueryContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Query_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail3">
                        <td style="width: 175px;" id="FormQueryUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditQueryUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditQueryUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail4">
                        <td style="width: 175px;" id="FormQueryAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditQueryAcknowledge" runat="server" Checked='<%# Bind("CRM_Query_Acknowledge") %>' />
                          <asp:Label ID="Label_EditQueryAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Query_Acknowledge"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="QueryDetail5">
                        <td style="width: 175px;" id="FormQueryAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditQueryAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail6">
                        <td style="width: 175px;" id="FormQueryAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditQueryAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail7">
                        <td style="width: 175px;" id="FormQueryCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditQueryCloseOut" runat="server" Checked='<%# Bind("CRM_Query_CloseOut") %>' />
                          <asp:Label ID="Label_EditQueryCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Query_CloseOut"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="QueryDetail8">
                        <td style="width: 175px;" id="FormQueryCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditQueryCloseOutDate" runat="server" Text='<%# Bind("CRM_Query_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail9">
                        <td style="width: 175px;" id="FormQueryCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditQueryCloseOutBy" runat="server" Text='<%# Bind("CRM_Query_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Suggestion Detail
                        </td>
                      </tr>
                      <tr id="SuggestionDetail2">
                        <td style="width: 175px;" id="FormSuggestionDescription">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditSuggestionDescription" runat="server" TextMode="MultiLine" Rows="8" Width="700px" Text='<%# Bind("CRM_Suggestion_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditSuggestionDescription" runat="server" TargetControlID="TextBox_EditSuggestionDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail10">
                        <td style="width: 175px;" id="FormSuggestionContactPatient">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSuggestionContactPatient" runat="server" SelectedValue='<%# Bind("CRM_Suggestion_ContactPatient") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail3">
                        <td style="width: 175px;" id="FormSuggestionUnitId">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSuggestionUnitId" runat="server" DataSourceID="SqlDataSource_CRM_EditSuggestionUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail4">
                        <td style="width: 175px;" id="FormSuggestionAcknowledge">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditSuggestionAcknowledge" runat="server" Checked='<%# Bind("CRM_Suggestion_Acknowledge") %>' />&nbsp;
                          <asp:Label ID="Label_EditSuggestionAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Suggestion_Acknowledge"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="SuggestionDetail5">
                        <td style="width: 175px;" id="FormSuggestionAcknowledgeDate">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditSuggestionAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail6">
                        <td style="width: 175px;" id="FormSuggestionAcknowledgeBy">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditSuggestionAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail7">
                        <td style="width: 175px;" id="FormSuggestionCloseOut">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditSuggestionCloseOut" runat="server" Checked='<%# Bind("CRM_Suggestion_CloseOut") %>' />
                          <asp:Label ID="Label_EditSuggestionCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Suggestion_CloseOut"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="SuggestionDetail8">
                        <td style="width: 175px;" id="FormSuggestionCloseOutDate">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditSuggestionCloseOutDate" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail9">
                        <td style="width: 175px;" id="FormSuggestionCloseOutBy">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditSuggestionCloseOutBy" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Route
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRouteRoute">Route Form
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditRouteRoute" runat="server" />
                          <asp:Label ID="Label_EditRouteRoute" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Route1">
                        <td style="width: 175px;" id="FormRouteFacility">To Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditRouteFacility" runat="server" DataSourceID="SqlDataSource_CRM_EditRouteFacility" AppendDataBoundItems="true" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditRouteFacility_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditRouteFacility" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Route2">
                        <td style="width: 175px;" id="FormRouteUnit">To Unit
                        </td>
                        <td style="width: 725px; padding: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_EditRouteUnit" runat="server" DataSourceID="SqlDataSource_CRM_EditRouteUnit" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" RepeatDirection="Vertical" RepeatLayout="Table" Width="100%">
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_EditRouteUnitTotal" runat="server" />
                          <div style="padding: 3px;"><asp:Label ID="Label_EditRouteUnit" runat="server"></asp:Label></div>
                        </td>
                      </tr>
                      <tr id="Route3">
                        <td style="width: 175px;" id="FormRouteComment">Comment
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditRouteComment" runat="server" TextMode="MultiLine" Rows="8" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Route4">
                        <td style="width: 175px;" id="FormRouteComplete">Complete
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditRouteComplete" runat="server" />
                          <asp:Label ID="Label_EditRouteComplete" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Route5">
                        <td style="width: 175px;" id="FormRouteCompleteDate">Complete Date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditRouteCompleteDate" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Route List
                        </td>
                        <td style="width: 725px; padding: 0px;" colspan="3">
                          <asp:GridView ID="GridView_EditRouteList" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_EditRouteList" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditRouteList_RowCreated" OnPreRender="GridView_EditRouteList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Routing Captured
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="CRM_Route_CreatedDate" HeaderText="Routed Date" ReadOnly="true" SortExpression="CRM_Route_CreatedDate" />
                              <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="To Facility" ReadOnly="true" SortExpression="Facility_FacilityDisplayName" />
                              <asp:BoundField DataField="CRM_Route_ToUnit_Name" HeaderText="To Unit" ReadOnly="true" SortExpression="CRM_Route_ToUnit_Name" />
                              <asp:BoundField DataField="CRM_Route_Comment" HeaderText="Comment" ReadOnly="true" SortExpression="CRM_Route_Comment" />
                              <asp:BoundField DataField="CRM_Route_Complete" HeaderText="Complete" ReadOnly="true" SortExpression="CRM_Route_Complete" />
                              <asp:BoundField DataField="CRM_Route_CompleteDate" HeaderText="Completed Date" ReadOnly="true" SortExpression="CRM_Route_CompleteDate" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">Upload Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">When uploading a document<br />
                          Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff), TXT (.txt), Outlook (.msg), Images (.jpeg / .jpg / .gif / .png)<br />
                          Only files smaller then 5 MB can be uploaded
                        <asp:HiddenField ID="HiddenField_EditFile" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditMessageFile" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">File
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:FileUpload ID="FileUpload_EditFile" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="text-align: center;">
                          <asp:Button ID="Button_EditUploadFile" runat="server" OnClick="Button_EditUploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_EditFile" OnDataBinding="Button_EditUploadFile_DataBinding" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_EditFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_File_EditFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditFile_RowCreated" OnPreRender="GridView_EditFile_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>
                                    <asp:Button ID="Button_EditDeleteFile" runat="server" OnClick="Button_EditDeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_EditFile" OnDataBinding="Button_EditDeleteFile_DataBinding" />&nbsp;
                                  <asp:Button ID="Button_EditDeleteAllFile" runat="server" OnClick="Button_EditDeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_EditFile" OnDataBinding="Button_EditDeleteAllFile_DataBinding" />&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Files
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_EditFile" runat="server" CssClass='<%# Eval("CRM_File_Id") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Uploaded File">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_EditFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("CRM_File_Name")) %>' CommandArgument='<%# Eval("CRM_File_Id") %>' OnDataBinding="LinkButton_EditFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="CRM_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="CRM_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("CRM_Status") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditStatus_SelectedIndexChanged">
                            <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditStatus" runat="server" Text='<%# Bind("CRM_Status") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditStatusDate" runat="server" Text='<%# Bind("CRM_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CRMStatusRejectedReason">
                        <td style="width: 175px;" id="FormStatusRejectedReason">Rejected Reason
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditStatusRejectedReason" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CRM_StatusRejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("CRM_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("CRM_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("CRM_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditIncompleteOther" runat="server" CausesValidation="False" Text="Go to Bulk Forms" CssClass="Controls_Button" OnClick="Button_EditIncompleteOther_Click" />&nbsp;
                          <asp:Button ID="Button_EditIncompleteComplaints" runat="server" CausesValidation="False" Text="Go to Incomplete Complaints" CssClass="Controls_Button" OnClick="Button_EditIncompleteComplaints_Click" />&nbsp;
                          <asp:Button ID="Button_EditCaptured" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_EditCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4"></td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          <asp:HiddenField ID="HiddenField_ItemSecurityRole" runat="server" OnDataBinding="HiddenField_ItemSecurityRole_DataBinding" />
                          <asp:HiddenField ID="HiddenField_ItemAdmin" runat="server" OnDataBinding="HiddenField_ItemAdmin_DataBinding" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date Received<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemDateReceived" runat="server" Text='<%# Bind("CRM_DateReceived","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date Forwarded to Facility
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemDateForwarded" runat="server" Text='<%# Bind("CRM_DateForwarded","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Originated at
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemOriginatedAtList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTypeList">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemTypeList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemTypeList" runat="server" Value='<%# Eval("CRM_Type_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedViaList">Received Via
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemReceivedViaList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReceivedFromList">Received From
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemReceivedFromList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemReceivedFromList" runat="server" Value='<%# Eval("CRM_ReceivedFrom_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated1">
                        <td style="width: 175px;">Escalated form
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemEscalatedForm" runat="server" Text='<%# (bool)(Eval("CRM_EscalatedForm"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemEscalatedForm" runat="server" Value='<%# Eval("CRM_EscalatedForm") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="Escalated2">
                        <td style="width: 175px;">Report Number of Original Complaint<br />
                          (e.g. 60/CRM/2011/00161)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemEscalatedReportNumber" runat="server" Text='<%# Bind("CRM_EscalatedReportNumber") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="CustomerInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="CustomerInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Customer Information
                        </td>
                      </tr>
                      <tr id="CustomerInformation3">
                        <td style="width: 175px;">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCustomerName" runat="server" Text='<%# Bind("CRM_CustomerName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CustomerInformation4">
                        <td style="width: 175px;">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCustomerEmail" runat="server" Text='<%# Bind("CRM_CustomerEmail") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CustomerInformation5">
                        <td style="width: 175px;">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCustomerContactNumber" runat="server" Text='<%# Bind("CRM_CustomerContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation1">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation2">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Information
                        </td>
                      </tr>
                      <tr id="PatientInformation3">
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientVisitNumber" runat="server" Text='<%# Bind("CRM_PatientVisitNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation4">
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientName" runat="server" Text='<%# Bind("CRM_PatientName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation5">
                        <td style="width: 175px;" id="FormPatientDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientDateOfAdmission" runat="server" Text='<%# Bind("CRM_PatientDateOfAdmission") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation6">
                        <td style="width: 175px;" id="FormPatientEmail">Email
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientEmail" runat="server" Text='<%# Bind("CRM_PatientEmail") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientInformation7">
                        <td style="width: 175px;" id="FormPatientContactNumber">Telephone Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientContactNumber" runat="server" Text='<%# Bind("CRM_PatientContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="SurveyResults1" runat="server">
                        <td colspan="4" class="FormView_TableBodyHeader">Survey Results
                        </td>
                      </tr>                      
                      <tr id="SurveyResults2" style="text-align: center;" runat="server">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemSurveyResults" runat="server" OnClick="Button_ItemSurveyResults_OnClick" Text="Survey Results" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="SurveyResults3" runat="server">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Detail
                        </td>
                      </tr>
                      <tr id="ComplaintDetail2">
                        <td style="width: 175px;">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintDescription" runat="server" Text='<%# Bind("CRM_Complaint_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail4">
                        <td style="width: 175px;">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintContactPatient" runat="server" Text='<%# Bind("CRM_Complaint_ContactPatient") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail5">
                        <td style="width: 175px;">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail6">
                        <td style="width: 175px;">Date Complaint Occurred<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintDateOccurred" runat="server" Text='<%# Bind("CRM_Complaint_DateOccurred","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail7">
                        <td style="width: 175px;">Time Complaint Occurred<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintTimeOccuredHours" runat="server" Text='<%# Bind("CRM_Complaint_TimeOccuredHours") %>'></asp:Label>&nbsp;:&nbsp;<asp:Label ID="Label_ItemComplaintTimeOccuredMinutes" runat="server" Text='<%# Bind("CRM_Complaint_TimeOccuredMinutes") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail8">
                        <td style="width: 175px;">Complaint Priority
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintPriorityList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail9">
                        <td style="width: 175px;">Complaint Category
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 1px; border-top-width: 1px;" colspan="3">
                          <asp:GridView ID="GridView_ItemComplaintCategory" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_ItemComplaintCategory" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemComplaintCategory_RowCreated" OnPreRender="GridView_ItemComplaintCategory_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Complaint Categories
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="CRM_Complaint_Category_Item_Name" ReadOnly="True" />
                            </Columns>
                          </asp:GridView>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail10">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail11">
                        <td colspan="4" class="FormView_TableBodyHeader">Complaint Investigation
                        </td>
                      </tr>
                      <tr id="ComplaintDetail12">
                        <td style="width: 175px;">Acknowledged within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin24Hours" runat="server" Text='<%# Bind("CRM_Complaint_Within24Hours") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemComplaintWithin24Hours" runat="server" Value='<%# Eval("CRM_Complaint_Within24Hours") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail13">
                        <td style="width: 175px;">Reason for not acknowledging within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin24HoursReason" runat="server" Text='<%# Bind("CRM_Complaint_Within24HoursReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail14">
                        <td style="width: 175px;">Method used to acknowledge within 24 hours
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin24HoursMethodList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail15">
                        <td style="width: 175px;">Follow up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin5Days" runat="server" Text='<%# Bind("CRM_Complaint_Within5Days") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemComplaintWithin5Days" runat="server" Value='<%# Eval("CRM_Complaint_Within5Days") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail16">
                        <td style="width: 175px;">Reason for not following up within 5 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin5DaysReason" runat="server" Text='<%# Bind("CRM_Complaint_Within5DaysReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail17">
                        <td style="width: 175px;">Close out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin10Days" runat="server" Text='<%# Bind("CRM_Complaint_Within10Days") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemComplaintWithin10Days" runat="server" Value='<%# Eval("CRM_Complaint_Within10Days") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail18">
                        <td style="width: 175px;">Reason for not closing out within 10 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin10DaysReason" runat="server" Text='<%# Bind("CRM_Complaint_Within10DaysReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail19">
                        <td style="width: 175px;">Close out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin3Days" runat="server" Text='<%# Bind("CRM_Complaint_Within3Days") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemComplaintWithin3Days" runat="server" Value='<%# Eval("CRM_Complaint_Within3Days") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail20">
                        <td style="width: 175px;">Reason for not closing out within 3 days
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintWithin3DaysReason" runat="server" Text='<%# Bind("CRM_Complaint_Within3DaysReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail21">
                        <td style="width: 175px;">Customer satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintCustomerSatisfied" runat="server" Text='<%# Bind("CRM_Complaint_CustomerSatisfied") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemComplaintCustomerSatisfied" runat="server" Value='<%# Eval("CRM_Complaint_CustomerSatisfied") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail22">
                        <td style="width: 175px;">Reason for customer not being satisfied
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintCustomerSatisfiedReason" runat="server" Text='<%# Bind("CRM_Complaint_CustomerSatisfiedReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail23">
                        <td style="width: 175px;">Investigator name
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintInvestigatorName" runat="server" Text='<%# Bind("CRM_Complaint_InvestigatorName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail24">
                        <td style="width: 175px;">Investigator designation
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintInvestigatorDesignation" runat="server" Text='<%# Bind("CRM_Complaint_InvestigatorDesignation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail25">
                        <td style="width: 175px;">Root Cause
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintRootCause" runat="server" Text='<%# Bind("CRM_Complaint_RootCause") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail26">
                        <td style="width: 175px;">Action
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintAction" runat="server" Text='<%# Bind("CRM_Complaint_Action") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail27">
                        <td colspan="4">Complaint Close Out can only be done for P1 by Hospital Manager and P2 by NSM
                        </td>
                      </tr>
                      <tr id="ComplaintDetail28">
                        <td style="width: 175px;">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Complaint_CloseOut"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail29">
                        <td style="width: 175px;">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintCloseOutDate" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplaintDetail30">
                        <td style="width: 175px;">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplaintCloseOutBy" runat="server" Text='<%# Bind("CRM_Complaint_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Compliment Detail
                        </td>
                      </tr>
                      <tr id="ComplimentDetail2">
                        <td style="width: 175px;">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentDescription" runat="server" Text='<%# Bind("CRM_Compliment_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail10">
                        <td style="width: 175px;">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentContactPatient" runat="server" Text='<%# Bind("CRM_Compliment_ContactPatient") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail3">
                        <td style="width: 175px;">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail4">
                        <td style="width: 175px;">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Compliment_Acknowledge"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail5">
                        <td style="width: 175px;">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail6">
                        <td style="width: 175px;">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Compliment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail7">
                        <td style="width: 175px;">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Compliment_CloseOut"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail8">
                        <td style="width: 175px;">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentCloseOutDate" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ComplimentDetail9">
                        <td style="width: 175px;">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemComplimentCloseOutBy" runat="server" Text='<%# Bind("CRM_Compliment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Comment Detail
                        </td>
                      </tr>
                      <tr id="CommentDetail2">
                        <td style="width: 175px;">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentDescription" runat="server" Text='<%# Bind("CRM_Comment_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail14">
                        <td style="width: 175px;">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentContactPatient" runat="server" Text='<%# Bind("CRM_Comment_ContactPatient") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail3">
                        <td style="width: 175px;">Type
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentTypeList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCommentTypeList" runat="server" Value='<%# Eval("CRM_Comment_Type_List") %>' />
                          &nbsp;
                        </td>
                        <td style="width: 70px;">Type 2
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentAdditionalTypeList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCommentAdditionalTypeList" runat="server" Value='<%# Eval("CRM_Comment_AdditionalType_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail4">
                        <td style="width: 175px;">Unit
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td style="width: 70px;">Unit 2
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentAdditionalUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail5">
                        <td style="width: 175px;">Category
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentCategoryList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td style="width: 70px;">Category 2
                        </td>
                        <td style="width: 320px;">
                          <asp:Label ID="Label_ItemCommentAdditionalCategoryList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail6">
                        <td style="width: 175px;">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Comment_Acknowledge"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail7">
                        <td style="width: 175px;">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail8">
                        <td style="width: 175px;">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Comment_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail9">
                        <td style="width: 175px;">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Comment_CloseOut"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail10">
                        <td style="width: 175px;">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentCloseOutDate" runat="server" Text='<%# Bind("CRM_Comment_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CommentDetail11">
                        <td style="width: 175px;">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemCommentCloseOutBy" runat="server" Text='<%# Bind("CRM_Comment_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Enquiry Detail
                        </td>
                      </tr>
                      <tr id="QueryDetail2">
                        <td style="width: 175px;">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryDescription" runat="server" Text='<%# Bind("CRM_Query_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail10">
                        <td style="width: 175px;">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryContactPatient" runat="server" Text='<%# Bind("CRM_Query_ContactPatient") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail3">
                        <td style="width: 175px;">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail4">
                        <td style="width: 175px;">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Query_Acknowledge"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail5">
                        <td style="width: 175px;">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail6">
                        <td style="width: 175px;">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryAcknowledgeBY" runat="server" Text='<%# Bind("CRM_Query_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail7">
                        <td style="width: 175px;">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Query_CloseOut"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail8">
                        <td style="width: 175px;">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryCloseOutDate" runat="server" Text='<%# Bind("CRM_Query_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="QueryDetail9">
                        <td style="width: 175px;">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemQueryCloseOutBy" runat="server" Text='<%# Bind("CRM_Query_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail1">
                        <td colspan="4" class="FormView_TableBodyHeader">Suggestion Detail
                        </td>
                      </tr>
                      <tr id="SuggestionDetail2">
                        <td style="width: 175px;">Description
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionDescription" runat="server" Text='<%# Bind("CRM_Suggestion_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail10">
                        <td style="width: 175px;">Patient contact allowed
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionContactPatient" runat="server" Text='<%# Bind("CRM_Suggestion_ContactPatient") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail3">
                        <td style="width: 175px;">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionUnitId" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail4">
                        <td style="width: 175px;">Acknowledge
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Suggestion_Acknowledge"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail5">
                        <td style="width: 175px;">Acknowledge date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionAcknowledgeDate" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail6">
                        <td style="width: 175px;">Acknowledge by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionAcknowledgeBy" runat="server" Text='<%# Bind("CRM_Suggestion_AcknowledgeBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail7">
                        <td style="width: 175px;">Close out
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionCloseOut" runat="server" Text='<%# (bool)(Eval("CRM_Suggestion_CloseOut"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail8">
                        <td style="width: 175px;">Close out date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionCloseOutDate" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="SuggestionDetail9">
                        <td style="width: 175px;">Close out by
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemSuggestionCloseOutBy" runat="server" Text='<%# Bind("CRM_Suggestion_CloseOutBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Route
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Route List
                        </td>
                        <td style="width: 725px; padding: 0px;" colspan="3">
                          <asp:GridView ID="GridView_ItemRouteList" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_ItemRouteList" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemRouteList_RowCreated" OnPreRender="GridView_ItemRouteList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Routing Captured
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="CRM_Route_CreatedDate" HeaderText="Routed Date" ReadOnly="true" SortExpression="CRM_Route_CreatedDate" />
                              <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="To Facility" ReadOnly="true" SortExpression="Facility_FacilityDisplayName" />
                              <asp:BoundField DataField="CRM_Route_ToUnit_Name" HeaderText="To Unit" ReadOnly="true" SortExpression="CRM_Route_ToUnit_Name" />
                              <asp:BoundField DataField="CRM_Route_Comment" HeaderText="Comment" ReadOnly="true" SortExpression="CRM_Route_Comment" />
                              <asp:BoundField DataField="CRM_Route_Complete" HeaderText="Complete" ReadOnly="true" SortExpression="CRM_Route_Complete" />
                              <asp:BoundField DataField="CRM_Route_CompleteDate" HeaderText="Completed Date" ReadOnly="true" SortExpression="CRM_Route_CompleteDate" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">Uploaded Files
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_ItemFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_CRM_File_ItemFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemFile_RowCreated" OnPreRender="GridView_ItemFile_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Files
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="Uploaded File">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_ItemFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("CRM_File_Name")) %>' CommandArgument='<%# Eval("CRM_File_Id") %>' OnDataBinding="LinkButton_ItemFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="CRM_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="CRM_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemStatus" runat="server" Text='<%# Bind("CRM_Status") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemStatus" runat="server" Value='<%# Bind("CRM_Status") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemStatusDate" runat="server" Text='<%# Bind("CRM_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="CRMStatusRejectedReason">
                        <td style="width: 175px;">Rejected Reason
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemStatusRejectedReason" runat="server" Text='<%# Bind("CRM_StatusRejectedReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("CRM_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("CRM_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("CRM_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Form" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemClear_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemIncompleteOther" runat="server" CausesValidation="False" Text="Go to Bulk Forms" CssClass="Controls_Button" OnClick="Button_ItemIncompleteOther_Click" />&nbsp;
                        <asp:Button ID="Button_ItemIncompleteComplaints" runat="server" CausesValidation="False" Text="Go to Incomplete Complaints" CssClass="Controls_Button" OnClick="Button_ItemIncompleteComplaints_Click" />&nbsp;
                        <asp:Button ID="Button_ItemCaptured" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_ItemCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertFacility" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertOriginatedAtList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertReceivedViaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertReceivedFromList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertComplaintUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertComplaintPriorityList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertComplaintCategoryItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertComplimentUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentAdditionalUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentAdditionalTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertCommentAdditionalCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertQueryUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_InsertSuggestionUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_File_InsertFile" runat="server" OnSelected="SqlDataSource_CRM_InsertFile_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditOriginatedAtList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditReceivedViaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditReceivedFromList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditComplaintUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditComplaintPriorityList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditComplaintCategoryItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditComplaintWithin24HoursMethodList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditComplimentUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditQueryUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditSuggestionUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditRouteFacility" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditRouteUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_EditRouteList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_File_EditFile" runat="server" OnSelected="SqlDataSource_CRM_EditFile_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_ItemComplaintCategory" runat="server" OnSelected="SqlDataSource_CRM_ItemComplaintCategory_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_ItemRouteList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_File_ItemFile" runat="server" OnSelected="SqlDataSource_CRM_ItemFile_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CRM_Form" runat="server" OnInserted="SqlDataSource_CRM_Form_Inserted" OnUpdated="SqlDataSource_CRM_Form_Updated"></asp:SqlDataSource>
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
