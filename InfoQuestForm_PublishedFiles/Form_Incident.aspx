<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Incident" CodeBehind="Form_Incident.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Incident</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Incident.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Incident" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Incident" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Incident" AssociatedUpdatePanelID="UpdatePanel_Incident">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Incident" runat="server">
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Incident_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_Facility" runat="server"></asp:SqlDataSource>
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
          <table id="TableForm" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_IncidentHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_Incident_Form" runat="server" Width="1000px" DataKeyNames="Incident_Id" CssClass="FormView" DataSourceID="SqlDataSource_Incident_Form" OnItemInserting="FormView_Incident_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Incident_Form_ItemCommand" OnDataBound="FormView_Incident_Form_DataBound" OnItemUpdating="FormView_Incident_Form_ItemUpdating">
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
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("Incident_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertIncidentReportable_TriggerUnit" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertIncidentReportable_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertPatientFalling_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertPatientDetail_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertPharmacy_TriggerLevel" runat="server" />
                          &nbsp;                        
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Incident_InsertFacility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertFacility_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityFrom">Facility Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertFacilityFrom" runat="server" DataSourceID="SqlDataSource_Incident_InsertFacilityFrom" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# Bind("Incident_FacilityFrom_Facility") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDate">Date of Incident<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("Incident_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTime">Time of Incident<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertTimeHours" runat="server" SelectedValue='<%# Bind("Incident_Time_Hours") %>' CssClass="Controls_DropDownList">
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
                          <asp:DropDownList ID="DropDownList_InsertTimeMin" runat="server" SelectedValue='<%# Bind("Incident_Time_Min") %>' CssClass="Controls_DropDownList">
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
                      <tr>
                        <td style="width: 175px;" id="FormReportingPerson">Person Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportingPerson" runat="server" Width="300px" Text='<%# Bind("Incident_ReportingPerson") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitFromUnit">Unit Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnitFromUnit" runat="server" DataSourceID="SqlDataSource_Incident_InsertUnitFromUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_UnitFrom_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitToUnit">Unit Issued To
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnitToUnit" runat="server" DataSourceID="SqlDataSource_Incident_InsertUnitToUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_UnitTo_Unit") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertUnitToUnit_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDisciplineList">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertDisciplineList" runat="server" DataSourceID="SqlDataSource_Incident_InsertDisciplineList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Discipline_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormIncidentCategoryList">Incident Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertIncidentCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_InsertIncidentCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_IncidentCategory_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertIncidentCategoryList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryListSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Customer / Visitor / Relative
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR2">
                        <td style="width: 175px;" id="FormCVRName">Customer / Visitor / Relative Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCVRName" runat="server" Width="300px" Text='<%# Bind("Incident_CVR_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR3">
                        <td style="width: 175px;" id="FormCVRContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertCVRContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_CVR_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Employee
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee2">
                        <td style="width: 175px;" id="FormEEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertEEmployeeNumber" runat="server" Width="300px" Text='<%# Bind("Incident_E_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_InsertFindEEmployeeName" runat="server" OnClick="Button_InsertFindEEmployeeName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee3">
                        <td style="width: 175px;" id="FormEEmployeeName">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertEEmployeeName" runat="server" Width="300px" Text='<%# Bind("Incident_E_EmployeeName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertEEmployeeName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertEEmployeeNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee4">
                        <td style="width: 175px;" id="FormEEmployeeUnitUnit">Employee Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEEmployeeUnitUnit" runat="server" DataSourceID="SqlDataSource_Incident_InsertEEmployeeUnitUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_E_EmployeeUnit_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee5">
                        <td style="width: 175px;" id="FormEEmployeeStatusList">Employee Status
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEEmployeeStatusList" runat="server" DataSourceID="SqlDataSource_Incident_InsertEEmployeeStatusList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_EmployeeStatus_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Status</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee6">
                        <td style="width: 175px;" id="FormEStaffCategoryList">Staff Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEStaffCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_InsertEStaffCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_StaffCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee7">
                        <td style="width: 175px;" id="FormEBodyPartAffectedList">Body Part Affected / Injured
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEBodyPartAffectedList" runat="server" DataSourceID="SqlDataSource_Incident_InsertEBodyPartAffectedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_BodyPartAffected_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Body Part</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee8">
                        <td style="width: 175px;" id="FormETreatmentRequiredList">Treatment Required
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertETreatmentRequiredList" runat="server" DataSourceID="SqlDataSource_Incident_InsertETreatmentRequiredList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_TreatmentRequired_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Treatment</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee9">
                        <td style="width: 175px;" id="FormEDaysOff">Number of Days booked off
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertEDaysOff" runat="server" Width="50px" Text='<%# Bind("Incident_E_DaysOff") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertEDaysOff" runat="server" TargetControlID="TextBox_InsertEDaysOff" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee10">
                        <td style="width: 175px;" id="FormEMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_InsertEMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee11">
                        <td style="width: 175px;" id="FormEMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertEMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_InsertEMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Member of Multi-Disciplinary Team
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT2">
                        <td style="width: 175px;" id="FormMMDTName">Member Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertMMDTName" runat="server" Width="300px" Text='<%# Bind("Incident_MMDT_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT3">
                        <td style="width: 175px;" id="FormMMDTContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertMMDTContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_MMDT_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT4">
                        <td style="width: 175px;" id="FormMMDTDisciplineList">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertMMDTDisciplineList" runat="server" DataSourceID="SqlDataSource_Incident_InsertMMDTDisciplineList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_MMDT_Discipline_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Patient
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient2">
                        <td style="width: 175px;" id="FormPVisitNumber">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPVisitNumber" runat="server" Width="300px" Text='<%# Bind("Incident_P_VisitNumber") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertPVisitNumber_TextChanged"></asp:TextBox>
                          <asp:Button ID="Button_InsertFindPName" runat="server" OnClick="Button_InsertFindPName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient3">
                        <td style="width: 175px;" id="FormPName">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPName" runat="server" Width="300px" Text='<%# Bind("Incident_P_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertPName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertPNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient4">
                        <td style="width: 175px;" id="FormPMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_P_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient5">
                        <td style="width: 175px;" id="FormPMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_P_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Property / Environment / Other
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty2">
                        <td style="width: 175px;" id="FormPropMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPropMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPropMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Prop_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty3">
                        <td style="width: 175px;" id="FormPropMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPropMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPropMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Prop_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Supplier / Service Provider
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS2">
                        <td style="width: 175px;" id="FormSSName">Supplier / Service Provider Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertSSName" runat="server" Width="300px" Text='<%# Bind("Incident_SS_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS3">
                        <td style="width: 175px;" id="FormSSContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertSSContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_SS_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Detail</td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel1List">Type of Incident Level 1
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertLevel1List" runat="server" DataSourceID="SqlDataSource_Incident_InsertLevel1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertLevel1List_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Level 1</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel2List">Type of Incident Level 2
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertLevel2List" runat="server" DataSourceID="SqlDataSource_Incident_InsertLevel2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertLevel2List_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Level 2</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel3List">Type of Incident Level 3
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertLevel3List" runat="server" DataSourceID="SqlDataSource_Incident_InsertLevel3List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertLevel3List_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Level 3</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSeverityList">Incident Severity
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSeverityList" runat="server" DataSourceID="SqlDataSource_Incident_InsertSeverityList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Severity_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Severity</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReportable">Incident Reportable
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertReportable" runat="server" Checked='<%# Bind("Incident_Reportable") %>' AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" class="tt">
                            <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0;"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ReportableInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable1">
                        <td style="width: 175px;">&nbsp;
                        </td>
                        <td style="width: 25px;">&nbsp;
                        </td>
                        <td style="width: 130px;">Date of Report
                        </td>
                        <td style="width: 530px;">Reference Number
                        </td>
                      </tr>
                      <tr id="IncidentReportable2">
                        <td style="width: 175px;" id="FormReportCOID">COID
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportCOID" runat="server" Checked='<%# Bind("Incident_Report_COID") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportCOIDDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_COID_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportCOIDDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportCOIDDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportCOIDDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportCOIDDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportCOIDDate" runat="server" Enabled="true" TargetControlID="TextBox_InsertReportCOIDDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportCOIDNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_COID_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportCOIDCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable3">
                        <td style="width: 175px;" id="FormReportDEAT">DEAT
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportDEAT" runat="server" Checked='<%# Bind("Incident_Report_DEAT") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportDEATDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DEAT_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportDEATDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportDEATDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportDEATDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportDEATDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportDEATDate" runat="server" TargetControlID="TextBox_InsertReportDEATDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportDEATNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DEAT_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportDEATCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable4">
                        <td style="width: 175px;" id="FormReportDepartmentOfHealth">Department of Health
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportDepartmentOfHealth" runat="server" Checked='<%# Bind("Incident_Report_DepartmentOfHealth") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportDepartmentOfHealthDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportDepartmentOfHealthDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportDepartmentOfHealthDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportDepartmentOfHealthDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportDepartmentOfHealthDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportDepartmentOfHealthDate" runat="server" TargetControlID="TextBox_InsertReportDepartmentOfHealthDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportDepartmentOfHealthNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportDepartmentOfHealthCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable5">
                        <td style="width: 175px;" id="FormReportDepartmentOfLabour">Department of Labour
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportDepartmentOfLabour" runat="server" Checked='<%# Bind("Incident_Report_DepartmentOfLabour") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportDepartmentOfLabourDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportDepartmentOfLabourDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportDepartmentOfLabourDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportDepartmentOfLabourDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportDepartmentOfLabourDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportDepartmentOfLabourDate" runat="server" TargetControlID="TextBox_InsertReportDepartmentOfLabourDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportDepartmentOfLabourNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportDepartmentOfLabourCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable6">
                        <td style="width: 175px;" id="FormReportHospitalManager">Hospital Manager
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportHospitalManager" runat="server" Checked='<%# Bind("Incident_Report_HospitalManager") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportHospitalManagerDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_HospitalManager_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportHospitalManagerDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportHospitalManagerDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportHospitalManagerDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportHospitalManagerDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportHospitalManagerDate" runat="server" TargetControlID="TextBox_InsertReportHospitalManagerDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportHospitalManagerNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_HospitalManager_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportHospitalManagerCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable7">
                        <td style="width: 175px;" id="FormReportHPCSA">HPCSA
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportHPCSA" runat="server" Checked='<%# Bind("Incident_Report_HPCSA") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportHPCSADate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_HPCSA_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportHPCSADate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportHPCSADate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportHPCSADate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportHPCSADate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportHPCSADate" runat="server" TargetControlID="TextBox_InsertReportHPCSADate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportHPCSANumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_HPCSA_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportHPCSACompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable8">
                        <td style="width: 175px;" id="FormReportLegalDepartment">Legal Department
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportLegalDepartment" runat="server" Checked='<%# Bind("Incident_Report_LegalDepartment") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportLegalDepartmentDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_LegalDepartment_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportLegalDepartmentDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportLegalDepartmentDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportLegalDepartmentDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportLegalDepartmentDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportLegalDepartmentDate" runat="server" TargetControlID="TextBox_InsertReportLegalDepartmentDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportLegalDepartmentNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_LegalDepartment_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportLegalDepartmentCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable9">
                        <td style="width: 175px;" id="FormReportCEO">CEO
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportCEO" runat="server" Checked='<%# Bind("Incident_Report_CEO") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportCEODate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_CEO_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportCEODate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportCEODate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportCEODate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportCEODate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportCEODate" runat="server" TargetControlID="TextBox_InsertReportCEODate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportCEONumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_CEO_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportCEOCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable10">
                        <td style="width: 175px;" id="FormReportPharmacyCouncil">Pharmacy Council
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportPharmacyCouncil" runat="server" Checked='<%# Bind("Incident_Report_PharmacyCouncil") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportPharmacyCouncilDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_PharmacyCouncil_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportPharmacyCouncilDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportPharmacyCouncilDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportPharmacyCouncilDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportPharmacyCouncilDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportPharmacyCouncilDate" runat="server" TargetControlID="TextBox_InsertReportPharmacyCouncilDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportPharmacyCouncilNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_PharmacyCouncil_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportPharmacyCouncilCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable11">
                        <td style="width: 175px;" id="FormReportQuality">Quality Department
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportQuality" runat="server" Checked='<%# Bind("Incident_Report_Quality") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportQualityDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_Quality_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportQualityDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportQualityDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportQualityDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportQualityDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportQualityDate" runat="server" TargetControlID="TextBox_InsertReportQualityDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportQualityNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_Quality_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportQualityCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable12">
                        <td style="width: 175px;" id="FormReportRM">RM
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportRM" runat="server" Checked='<%# Bind("Incident_Report_RM") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportRMDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_RM_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportRMDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportRMDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportRMDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportRMDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportRMDate" runat="server" TargetControlID="TextBox_InsertReportRMDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportRMNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_RM_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportRMCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable13">
                        <td style="width: 175px;" id="FormReportSANC">SANC
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportSANC" runat="server" Checked='<%# Bind("Incident_Report_SANC") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportSANCDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_SANC_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportSANCDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportSANCDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportSANCDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportSANCDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportSANCDate" runat="server" TargetControlID="TextBox_InsertReportSANCDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportSANCNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_SANC_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportSANCCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable14">
                        <td style="width: 175px;" id="FormReportSAPS">SAPS
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportSAPS" runat="server" Checked='<%# Bind("Incident_Report_SAPS") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportSAPSDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_SAPS_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportSAPSDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportSAPSDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportSAPSDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportSAPSDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportSAPSDate" runat="server" TargetControlID="TextBox_InsertReportSAPSDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportSAPSNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_SAPS_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportSAPSCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable15">
                        <td style="width: 175px;" id="FormReportInternalAudit">Internal Audit
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_InsertReportInternalAudit" runat="server" Checked='<%# Bind("Incident_Report_InternalAudit") %>' AutoPostBack="true" />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_InsertReportInternalAuditDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_InternalAudit_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportInternalAuditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportInternalAuditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportInternalAuditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportInternalAuditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportInternalAuditDate" runat="server" TargetControlID="TextBox_InsertReportInternalAuditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_InsertReportInternalAuditNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_InternalAudit_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertReportInternalAuditCompulsory" runat="server" Value="No"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportableTriggerLevel" runat="server">
                        <td colspan="4">
                          <asp:Label ID="Label_InsertIncidentReportableTriggerLevel" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientFallingSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientFallingHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Falling
                        </td>
                      </tr>
                      <tr id="PatientFalling1">
                        <td style="width: 175px;" id="FormPatientFallingWhereFallOccurList">Where did fall / alleged fall occur
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPatientFallingWhereFallOccurList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_PatientFalling_WhereFallOccur_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="PatientDetailSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientDetailHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Detail
                        </td>
                      </tr>
                      <tr id="PatientDetail1">
                        <td style="width: 175px;" id="FormPatientDetailVisitNumber">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientDetailVisitNumber" runat="server" Width="300px" Text='<%# Bind("Incident_PatientDetail_VisitNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_InsertFindPatientDetailName" runat="server" OnClick="Button_InsertFindPatientDetailName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientDetail2">
                        <td style="width: 175px;" id="FormPatientDetailName">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientDetailName" runat="server" Width="300px" Text='<%# Bind("Incident_PatientDetail_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertPatientDetailName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertPatientDetailNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PharmacySpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PharmacyHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 175px;" id="FormPharmacyInitials">Pharmacy staff member initials
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyInitials" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_Initials") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 175px;" id="FormPharmacyStaffInvolvedList">Staff member involved
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyStaffInvolvedList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyStaffInvolvedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_StaffInvolved_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 175px;" id="FormPharmacyCheckingList">Checking
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyCheckingList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyCheckingList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Checking_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 175px;" id="FormPharmacyLocumOrPermanent">Is the relevant staff member a regular locum or permanent employee working for longer than 3 months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyLocumOrPermanent" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_LocumOrPermanent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 175px;" id="FormPharmacyStaffOnDutyList">Where all staff on duty, If not why
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyStaffOnDutyList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyStaffOnDutyList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_StaffOnDuty_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 175px;" id="FormPharmacyChangeInWorkProcedure">Change in work procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyChangeInWorkProcedure" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_ChangeInWorkProcedure") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 175px;" id="FormPharmacyTypeOfPrescriptionList">Type of prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyTypeOfPrescriptionList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_TypeOfPrescription_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxOnDay">Number of Rx's on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyNumberOfRxOnDay" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfRxOnDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPharmacyNumberOfRxOnDay" runat="server" TargetControlID="TextBox_InsertPharmacyNumberOfRxOnDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy9">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedOnDay">Number of items dispensed on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyNumberOfItemsDispensedOnDay" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedOnDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPharmacyNumberOfItemsDispensedOnDay" runat="server" TargetControlID="TextBox_InsertPharmacyNumberOfItemsDispensedOnDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy10">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxDayBefore">Number of Rx's dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyNumberOfRxDayBefore" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfRxDayBefore") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPharmacyNumberOfRxDayBefore" runat="server" TargetControlID="TextBox_InsertPharmacyNumberOfRxDayBefore" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy11">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedDayBefore">Number of items dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyNumberOfItemsDispensedDayBefore" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedDayBefore") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPharmacyNumberOfItemsDispensedDayBefore" runat="server" TargetControlID="TextBox_InsertPharmacyNumberOfItemsDispensedDayBefore" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy12">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribed">Name of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyDrugPrescribed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugPrescribed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy13">
                        <td style="width: 175px;" id="FormPharmacyDrugDispensed">Name of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyDrugDispensed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugDispensed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy14">
                        <td style="width: 175px;" id="FormPharmacyDrugPacked">Name of drug packed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyDrugPacked" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugPacked") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy15">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugPrescribed">Strength of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyStrengthDrugPrescribed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_StrengthDrugPrescribed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy16">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugDispensed">Strength of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyStrengthDrugDispensed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_StrengthDrugDispensed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy17">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribedNewOnMarket">Is the drug prescribed new on the market (launched in the last 6 months)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyDrugPrescribedNewOnMarket" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_DrugPrescribedNewOnMarket") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy18">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationOnPrescription">Was all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyLegislativeInformationOnPrescription" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_LegislativeInformationOnPrescription") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy19">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationNotOnPrescription">Why wasn't all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPharmacyLegislativeInformationNotOnPrescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_LegislativeInformationNotOnPrescription") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy20">
                        <td style="width: 175px;" id="FormPharmacyDoctorName">Name of Doctor
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyDoctorName" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy21">
                        <td style="width: 175px;" id="FormPharmacyFactorsList">Factor(s) contributing to medication incident (Must provide all details in description of incident)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyFactorsList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyFactorsList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Factors_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy22">
                        <td style="width: 175px;" id="FormPharmacySystemRelatedIssuesList">Were there any system related issues
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacySystemRelatedIssuesList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_SystemRelatedIssues_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy23">
                        <td style="width: 175px;" id="FormPharmacyErgonomicProblemsList">Ergonomic Problems
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyErgonomicProblemsList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_ErgonomicProblems_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy24">
                        <td style="width: 175px;" id="FormPharmacyPatientCounselled">Patient counselled for TTO/Retail
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyPatientCounselled" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_PatientCounselled") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy25">
                        <td style="width: 175px;" id="FormPharmacySimilarIncident">Similar incident in the last two months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacySimilarIncident" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_SimilarIncident") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy26">
                        <td style="width: 175px;" id="FormPharmacyLocationList">Location of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyLocationList" runat="server" DataSourceID="SqlDataSource_Incident_InsertPharmacyLocationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Location_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy27">
                        <td style="width: 175px;" id="FormPharmacyPatientOutcomeAffected">Was the patients outcome affected
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertPharmacyPatientOutcomeAffected" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_PatientOutcomeAffected") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to SAPS
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS1">
                        <td style="width: 175px;" id="FormReportableSAPSPoliceStation">Police Station
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableSAPSPoliceStation" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_PoliceStation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS2">
                        <td style="width: 175px;" id="FormReportableSAPSInvestigationOfficersName">Investigation Officers Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableSAPSInvestigationOfficersName" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_InvestigationOfficersName") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS3">
                        <td style="width: 175px;" id="FormReportableSAPSTelephoneNumber">Telephone Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableSAPSTelephoneNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_TelephoneNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS4">
                        <td style="width: 175px;" id="FormReportableSAPSCaseNumber">Case Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableSAPSCaseNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_CaseNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to Internal Audit
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit1">
                        <td style="width: 175px;" id="FormReportableInternalAuditDateDetected">Date Detected<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditDateDetected" runat="server" Width="75px" Text='<%# Bind("Incident_Reportable_InternalAudit_DateDetected","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportableInternalAuditDateDetected" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportableInternalAuditDateDetected" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportableInternalAuditDateDetected" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportableInternalAuditDateDetected">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportableInternalAuditDateDetected" runat="server" TargetControlID="TextBox_InsertReportableInternalAuditDateDetected" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit2">
                        <td style="width: 175px;" id="FormReportableInternalAuditByWhom">By Whom
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditByWhom" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_ByWhom") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit3">
                        <td style="width: 175px;" id="FormReportableInternalAuditTotalLossValue">Total Loss Value
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditTotalLossValue" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalLossValue") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit4">
                        <td style="width: 175px;" id="FormReportableInternalAuditTotalRecovery">Total Recovery
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditTotalRecovery" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalRecovery") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit5">
                        <td style="width: 175px;" id="FormReportableInternalAuditRecoveryPlan">Recovery Plan
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditRecoveryPlan" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_RecoveryPlan") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit6">
                        <td style="width: 175px;" id="FormReportableInternalAuditStatusOfInvestigation">Status Of Investigation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditStatusOfInvestigation" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_StatusOfInvestigation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit7">
                        <td style="width: 175px;" id="FormReportableInternalAuditSAPSNotReported">Reason why Incident was not reported to SAPS 
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableInternalAuditSAPSNotReported" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_SAPSNotReported") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Root Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRootCategoryList">Root Cause Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertRootCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_InsertRootCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_RootCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRootDescription">Root Cause Description
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertRootDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_RootDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigator">Investigator
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigator" runat="server" Width="300px" Text='<%# Bind("Incident_Investigator") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigatorContactNumber">Investigator Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigatorContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_InvestigatorContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigatorDesignation">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigatorDesignation" runat="server" Width="300px" Text='<%# Bind("Incident_InvestigatorDesignation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationCompleted">Investigation Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertInvestigationCompleted" runat="server" Checked='<%# Bind("Incident_InvestigationCompleted") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigation Completed Date
                        </td>
                        <td style="width: 825px;" colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Degree of Harm
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmList">Degree of Harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-spacing: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_InsertDegreeOfHarmList" runat="server" SelectedValue='<%# Bind("Incident_DegreeOfHarm_DegreeOfHarm_List")%>' RepeatDirection="Vertical" RepeatLayout="Table" Width="100%">
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_InsertDegreeOfHarmListTotal" runat="server" OnDataBinding="HiddenField_InsertDegreeOfHarmListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmImpact">Impact or Potential Impact of error / harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="InsertDegreeOfHarmImpactImpactList" runat="server" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_InsertDegreeOfHarmImpactImpactList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" Width="100%">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_InsertDegreeOfHarmImpactImpactListTotal" runat="server" OnDataBinding="HiddenField_InsertDegreeOfHarmImpactImpactListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmCost">Estimated / Actual cost
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDegreeOfHarmCost" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_DegreeOfHarm_Cost") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmImplications">Potential future implications e.g. antibiotic resistance, infection risk, etc.
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDegreeOfHarmImplications" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_DegreeOfHarm_Implications") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to CEO
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO1">
                        <td style="width: 175px;" id="FormReportableCEOAcknowledgedHM">Acknowledged by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEOAcknowledgedHM" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_AcknowledgedHM") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO13">
                        <td style="width: 175px;" id="FormReportableCEODoctorRelated">Doctor Related
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEODoctorRelated" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_DoctorRelated") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO2">
                        <td style="width: 175px;" id="FormReportableCEOCEONotifiedWithin24Hours">CEO notified within 24hrs
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEOCEONotifiedWithin24Hours" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CEONotifiedWithin24Hours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO3">
                        <td style="width: 175px;" id="FormReportableCEOProgressUpdateSent">Progress report / update sent
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEOProgressUpdateSent" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_ProgressUpdateSent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO4">
                        <td style="width: 175px;" id="FormReportableCEOActionsTakenHM">Actions taken by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableCEOActionsTakenHM" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_CEO_ActionsTakenHM") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO5">
                        <td style="width: 175px;" id="FormReportableCEODate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableCEODate" runat="server" Width="75px" Text='<%# Bind("Incident_Reportable_CEO_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertReportableCEODate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertReportableCEODate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertReportableCEODate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertReportableCEODate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertReportableCEODate" runat="server" TargetControlID="TextBox_InsertReportableCEODate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO6">
                        <td style="width: 175px;" id="FormReportableCEOActionsAgainstEmployee">Actions taken against employee
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertReportableCEOActionsAgainstEmployee" runat="server" Checked='<%# Bind("Incident_Reportable_CEO_ActionsAgainstEmployee") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO7">
                        <td style="width: 175px;" id="FormReportableCEOEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableCEOEmployeeNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_CEO_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_InsertFindReportableCEOEmployeeName" runat="server" OnClick="Button_InsertFindReportableCEOEmployeeName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO8">
                        <td style="width: 175px;" id="FormReportableCEOEmployeeName">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableCEOEmployeeName" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_CEO_EmployeeName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertReportableCEOEmployeeName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertReportableCEOEmployeeNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO9">
                        <td style="width: 175px;" id="FormReportableCEOOutcome">Outcome
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertReportableCEOOutcome" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_CEO_Outcome") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO10">
                        <td style="width: 175px;" id="FormReportableCEOFileScanned">File Scanned
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertReportableCEOFileScanned" runat="server" Checked='<%# Bind("Incident_Reportable_CEO_FileScanned") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO11">
                        <td style="width: 175px;" id="FormReportableCEOCloseOffHM">Close off by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEOCloseOffHM" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CloseOffHM") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO12">
                        <td style="width: 175px;" id="FormReportableCEOCloseOutEmailSend">Close out email sent to CEO
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertReportableCEOCloseOutEmailSend" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CloseOutEmailSend") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
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
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertStatus" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;" colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Incident_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Incident_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Incident_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Incident_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Incident" CssClass="Controls_Button" ValidationGroup="Incident_Form" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" />&nbsp;
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
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("Incident_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditIncidentReportable_TriggerUnit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditIncidentReportable_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditPatientFalling_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditPatientDetail_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditPharmacy_TriggerLevel" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacilityFrom">Facility Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditFacilityFrom" runat="server" DataSourceID="SqlDataSource_Incident_EditFacilityFrom" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# Bind("Incident_FacilityFrom_Facility") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDate">Date of Incident<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("Incident_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormTime">Time of Incident<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditTimeHours" runat="server" SelectedValue='<%# Bind("Incident_Time_Hours") %>' CssClass="Controls_DropDownList">
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
                          <asp:DropDownList ID="DropDownList_EditTimeMin" runat="server" SelectedValue='<%# Bind("Incident_Time_Min") %>' CssClass="Controls_DropDownList">
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
                      <tr>
                        <td style="width: 175px;" id="FormReportingPerson">Person Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportingPerson" runat="server" Width="300px" Text='<%# Bind("Incident_ReportingPerson") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitFromUnit">Unit Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnitFromUnit" runat="server" DataSourceID="SqlDataSource_Incident_EditUnitFromUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_UnitFrom_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitToUnit">Unit Issued To
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnitToUnit" runat="server" DataSourceID="SqlDataSource_Incident_EditUnitToUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_UnitTo_Unit") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditUnitToUnit_SelectedIndexChanged" OnDataBound="DropDownList_EditUnitToUnit_DataBound">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDisciplineList">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditDisciplineList" runat="server" DataSourceID="SqlDataSource_Incident_EditDisciplineList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Discipline_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormIncidentCategoryList">Incident Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditIncidentCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_EditIncidentCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_IncidentCategory_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditIncidentCategoryList_SelectedIndexChanged" OnDataBound="DropDownList_EditIncidentCategoryList_DataBound">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryListSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Customer / Visitor / Relative
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR2">
                        <td style="width: 175px;" id="FormCVRName">Customer / Visitor / Relative Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCVRName" runat="server" Width="300px" Text='<%# Bind("Incident_CVR_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR3">
                        <td style="width: 175px;" id="FormCVRContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditCVRContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_CVR_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Employee
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee2">
                        <td style="width: 175px;" id="FormEEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditEEmployeeNumber" runat="server" Width="300px" Text='<%# Bind("Incident_E_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_EditFindEEmployeeName" runat="server" OnClick="Button_EditFindEEmployeeName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee3">
                        <td style="width: 175px;" id="FormEEmployeeName">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditEEmployeeName" runat="server" Width="300px" Text='<%# Bind("Incident_E_EmployeeName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditEEmployeeName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditEEmployeeNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee4">
                        <td style="width: 175px;" id="FormEEmployeeUnitUnit">Employee Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEEmployeeUnitUnit" runat="server" DataSourceID="SqlDataSource_Incident_EditEEmployeeUnitUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Incident_E_EmployeeUnit_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee5">
                        <td style="width: 175px;" id="FormEEmployeeStatusList">Employee Status
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEEmployeeStatusList" runat="server" DataSourceID="SqlDataSource_Incident_EditEEmployeeStatusList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_EmployeeStatus_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Status</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee6">
                        <td style="width: 175px;" id="FormEStaffCategoryList">Staff Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEStaffCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_EditEStaffCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_StaffCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee7">
                        <td style="width: 175px;" id="FormEBodyPartAffectedList">Body Part Affected / Injured
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEBodyPartAffectedList" runat="server" DataSourceID="SqlDataSource_Incident_EditEBodyPartAffectedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_BodyPartAffected_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Body Part</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee8">
                        <td style="width: 175px;" id="FormETreatmentRequiredList">Treatment Required
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditETreatmentRequiredList" runat="server" DataSourceID="SqlDataSource_Incident_EditETreatmentRequiredList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_TreatmentRequired_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Treatment</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee9">
                        <td style="width: 175px;" id="FormEDaysOff">Number of Days booked off
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditEDaysOff" runat="server" Width="50px" Text='<%# Bind("Incident_E_DaysOff") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditEDaysOff" runat="server" TargetControlID="TextBox_EditEDaysOff" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee10">
                        <td style="width: 175px;" id="FormEMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_EditEMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee11">
                        <td style="width: 175px;" id="FormEMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditEMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_EditEMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_E_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Member of Multi-Disciplinary Team
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT2">
                        <td style="width: 175px;" id="FormMMDTName">Member Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditMMDTName" runat="server" Width="300px" Text='<%# Bind("Incident_MMDT_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT3">
                        <td style="width: 175px;" id="FormMMDTContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditMMDTContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_MMDT_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT4">
                        <td style="width: 175px;" id="FormMMDTDisciplineList">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditMMDTDisciplineList" runat="server" DataSourceID="SqlDataSource_Incident_EditMMDTDisciplineList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_MMDT_Discipline_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Patient
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient2">
                        <td style="width: 175px;" id="FormPVisitNumber">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPVisitNumber" runat="server" Width="300px" Text='<%# Bind("Incident_P_VisitNumber") %>' CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EditPVisitNumber_TextChanged"></asp:TextBox>
                          <asp:Button ID="Button_EditFindPName" runat="server" OnClick="Button_EditFindPName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient3">
                        <td style="width: 175px;" id="FormPName">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPName" runat="server" Width="300px" Text='<%# Bind("Incident_P_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditPName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditPNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient4">
                        <td style="width: 175px;" id="FormPMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_EditPMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_P_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient5">
                        <td style="width: 175px;" id="FormPMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_EditPMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_P_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Property / Environment / Other
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty2">
                        <td style="width: 175px;" id="FormPropMainContributorList">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPropMainContributorList" runat="server" DataSourceID="SqlDataSource_Incident_EditPropMainContributorList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Prop_MainContributor_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Contributor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty3">
                        <td style="width: 175px;" id="FormPropMainContributorStaffList">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPropMainContributorStaffList" runat="server" DataSourceID="SqlDataSource_Incident_EditPropMainContributorStaffList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Prop_MainContributor_Staff_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Member</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Supplier / Service Provider
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS2">
                        <td style="width: 175px;" id="FormSSName">Supplier / Service Provider Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditSSName" runat="server" Width="300px" Text='<%# Bind("Incident_SS_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS3">
                        <td style="width: 175px;" id="FormSSContactNumber">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditSSContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_SS_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Detail
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel1List">Type of Incident Level 1
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditLevel1List" runat="server" DataSourceID="SqlDataSource_Incident_EditLevel1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditLevel1List_SelectedIndexChanged" OnDataBound="DropDownList_EditLevel1List_DataBound">
                            <asp:ListItem Value="">Select Level 1</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel2List">Type of Incident Level 2
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditLevel2List" runat="server" DataSourceID="SqlDataSource_Incident_EditLevel2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditLevel2List_SelectedIndexChanged" OnDataBound="DropDownList_EditLevel2List_DataBound">
                            <asp:ListItem Value="">Select Level 2</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormLevel3List">Type of Incident Level 3
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditLevel3List" runat="server" DataSourceID="SqlDataSource_Incident_EditLevel3List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditLevel3List_SelectedIndexChanged" OnDataBound="DropDownList_EditLevel3List_DataBound">
                            <asp:ListItem Value="">Select Level 3</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSeverityList">Incident Severity
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSeverityList" runat="server" DataSourceID="SqlDataSource_Incident_EditSeverityList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Severity_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Severity</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormReportable">Incident Reportable
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditReportable" runat="server" Checked='<%# Bind("Incident_Reportable") %>' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" class="tt">
                            <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0;"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ReportableInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable1">
                        <td style="width: 175px;">&nbsp;
                        </td>
                        <td style="width: 25px;">&nbsp;
                        </td>
                        <td style="width: 130px;">Date of Report
                        </td>
                        <td style="width: 530px;">Reference Number
                        </td>
                      </tr>
                      <tr id="IncidentReportable2">
                        <td style="width: 175px;" id="FormReportCOID">COID
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportCOID" runat="server" Checked='<%# Bind("Incident_Report_COID") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportCOIDDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_COID_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportCOIDDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportCOIDDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportCOIDDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportCOIDDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportCOIDDate" runat="server" TargetControlID="TextBox_EditReportCOIDDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportCOIDNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_COID_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportCOIDCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable3">
                        <td style="width: 175px;" id="FormReportDEAT">DEAT
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportDEAT" runat="server" Checked='<%# Bind("Incident_Report_DEAT") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportDEATDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DEAT_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportDEATDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportDEATDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportDEATDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportDEATDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportDEATDate" runat="server" TargetControlID="TextBox_EditReportDEATDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportDEATNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DEAT_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportDEATCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable4">
                        <td style="width: 175px;" id="FormReportDepartmentOfHealth">Department of Health
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportDepartmentOfHealth" runat="server" Checked='<%# Bind("Incident_Report_DepartmentOfHealth") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportDepartmentOfHealthDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportDepartmentOfHealthDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportDepartmentOfHealthDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportDepartmentOfHealthDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportDepartmentOfHealthDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportDepartmentOfHealthDate" runat="server" TargetControlID="TextBox_EditReportDepartmentOfHealthDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportDepartmentOfHealthNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportDepartmentOfHealthCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable5">
                        <td style="width: 175px;" id="FormReportDepartmentOfLabour">Department of Labour
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportDepartmentOfLabour" runat="server" Checked='<%# Bind("Incident_Report_DepartmentOfLabour") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportDepartmentOfLabourDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportDepartmentOfLabourDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportDepartmentOfLabourDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportDepartmentOfLabourDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportDepartmentOfLabourDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportDepartmentOfLabourDate" runat="server" TargetControlID="TextBox_EditReportDepartmentOfLabourDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportDepartmentOfLabourNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportDepartmentOfLabourCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable6">
                        <td style="width: 175px;" id="FormReportHospitalManager">Hospital Manager
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportHospitalManager" runat="server" Checked='<%# Bind("Incident_Report_HospitalManager") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportHospitalManagerDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_HospitalManager_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportHospitalManagerDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportHospitalManagerDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportHospitalManagerDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportHospitalManagerDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportHospitalManagerDate" runat="server" TargetControlID="TextBox_EditReportHospitalManagerDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportHospitalManagerNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_HospitalManager_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportHospitalManagerCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable7">
                        <td style="width: 175px;" id="FormReportHPCSA">HPCSA
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportHPCSA" runat="server" Checked='<%# Bind("Incident_Report_HPCSA") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportHPCSADate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_HPCSA_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportHPCSADate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportHPCSADate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportHPCSADate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportHPCSADate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportHPCSADate" runat="server" TargetControlID="TextBox_EditReportHPCSADate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportHPCSANumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_HPCSA_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportHPCSACompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable8">
                        <td style="width: 175px;" id="FormReportLegalDepartment">Legal Department
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportLegalDepartment" runat="server" Checked='<%# Bind("Incident_Report_LegalDepartment") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportLegalDepartmentDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_LegalDepartment_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportLegalDepartmentDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportLegalDepartmentDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportLegalDepartmentDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportLegalDepartmentDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportLegalDepartmentDate" runat="server" TargetControlID="TextBox_EditReportLegalDepartmentDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportLegalDepartmentNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_LegalDepartment_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportLegalDepartmentCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable9">
                        <td style="width: 175px;" id="FormReportCEO">CEO
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportCEO" runat="server" Checked='<%# Bind("Incident_Report_CEO") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportCEODate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_CEO_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportCEODate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportCEODate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportCEODate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportCEODate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportCEODate" runat="server" TargetControlID="TextBox_EditReportCEODate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportCEONumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_CEO_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportCEOCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable10">
                        <td style="width: 175px;" id="FormReportPharmacyCouncil">Pharmacy Council
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportPharmacyCouncil" runat="server" Checked='<%# Bind("Incident_Report_PharmacyCouncil") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportPharmacyCouncilDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_PharmacyCouncil_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportPharmacyCouncilDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportPharmacyCouncilDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportPharmacyCouncilDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportPharmacyCouncilDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportPharmacyCouncilDate" runat="server" TargetControlID="TextBox_EditReportPharmacyCouncilDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportPharmacyCouncilNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_PharmacyCouncil_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportPharmacyCouncilCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable11">
                        <td style="width: 175px;" id="FormReportQuality">Quality Department
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportQuality" runat="server" Checked='<%# Bind("Incident_Report_Quality") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportQualityDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_Quality_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportQualityDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportQualityDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportQualityDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportQualityDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportQualityDate" runat="server" TargetControlID="TextBox_EditReportQualityDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportQualityNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_Quality_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportQualityCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable12">
                        <td style="width: 175px;" id="FormReportRM">RM
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportRM" runat="server" Checked='<%# Bind("Incident_Report_RM") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportRMDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_RM_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportRMDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportRMDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportRMDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportRMDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportRMDate" runat="server" TargetControlID="TextBox_EditReportRMDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportRMNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_RM_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportRMCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable13">
                        <td style="width: 175px;" id="FormReportSANC">SANC
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportSANC" runat="server" Checked='<%# Bind("Incident_Report_SANC") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportSANCDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_SANC_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportSANCDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportSANCDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportSANCDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportSANCDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportSANCDate" runat="server" TargetControlID="TextBox_EditReportSANCDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportSANCNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_SANC_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportSANCCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable14">
                        <td style="width: 175px;" id="FormReportSAPS">SAPS
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportSAPS" runat="server" Checked='<%# Bind("Incident_Report_SAPS") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportSAPSDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_SAPS_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportSAPSDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportSAPSDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportSAPSDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportSAPSDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportSAPSDate" runat="server" TargetControlID="TextBox_EditReportSAPSDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportSAPSNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_SAPS_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportSAPSCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportable15">
                        <td style="width: 175px;" id="FormReportInternalAudit">Internal Audit
                        </td>
                        <td style="width: 25px;">
                          <asp:CheckBox ID="CheckBox_EditReportInternalAudit" runat="server" Checked='<%# Bind("Incident_Report_InternalAudit") %>' />&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:TextBox ID="TextBox_EditReportInternalAuditDate" runat="server" Width="75px" Text='<%# Bind("Incident_Report_InternalAudit_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportInternalAuditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportInternalAuditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportInternalAuditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportInternalAuditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportInternalAuditDate" runat="server" TargetControlID="TextBox_EditReportInternalAuditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 530px;">
                          <asp:TextBox ID="TextBox_EditReportInternalAuditNumber" runat="server" Width="500px" Text='<%# Bind("Incident_Report_InternalAudit_Number") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditReportInternalAuditCompulsory" runat="server" Value="Yes"></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IncidentReportableTriggerLevel" runat="server">
                        <td colspan="4">
                          <asp:Label ID="Label_EditIncidentReportableTriggerLevel" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientFallingSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientFallingHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Falling
                        </td>
                      </tr>
                      <tr id="PatientFalling1">
                        <td style="width: 175px;" id="FormPatientFallingWhereFallOccurList">Where did fall / alleged fall occur
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPatientFallingWhereFallOccurList" runat="server" DataSourceID="SqlDataSource_Incident_EditPatientFallingWhereFallOccurList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_PatientFalling_WhereFallOccur_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="PatientDetailSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientDetailHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Detail
                        </td>
                      </tr>
                      <tr id="PatientDetail1">
                        <td style="width: 175px;" id="FormPatientDetailVisitNumber">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientDetailVisitNumber" runat="server" Width="300px" Text='<%# Bind("Incident_PatientDetail_VisitNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_EditFindPatientDetailName" runat="server" OnClick="Button_EditFindPatientDetailName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientDetail2">
                        <td style="width: 175px;" id="FormPatientDetailName">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPatientDetailName" runat="server" Width="300px" Text='<%# Bind("Incident_PatientDetail_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditPatientDetailName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditPatientDetailNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PharmacySpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PharmacyHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 175px;" id="FormPharmacyInitials">Pharmacy staff member initials
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyInitials" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_Initials") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 175px;" id="FormPharmacyStaffInvolvedList">Staff member involved
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyStaffInvolvedList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyStaffInvolvedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_StaffInvolved_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 175px;" id="FormPharmacyCheckingList">Checking
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyCheckingList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyCheckingList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Checking_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 175px;" id="FormPharmacyLocumOrPermanent">Is the relevant staff member a regular locum or permanent employee working for longer than 3 months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyLocumOrPermanent" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_LocumOrPermanent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 175px;" id="FormPharmacyStaffOnDutyList">Where all staff on duty, If not why
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyStaffOnDutyList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyStaffOnDutyList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_StaffOnDuty_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 175px;" id="FormPharmacyChangeInWorkProcedure">Change in work procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyChangeInWorkProcedure" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_ChangeInWorkProcedure") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 175px;" id="FormPharmacyTypeOfPrescriptionList">Type of prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyTypeOfPrescriptionList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_TypeOfPrescription_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxOnDay">Number of Rx's on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyNumberOfRxOnDay" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfRxOnDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacyNumberOfRxOnDay" runat="server" TargetControlID="TextBox_EditPharmacyNumberOfRxOnDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy9">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedOnDay">Number of items dispensed on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyNumberOfItemsDispensedOnDay" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedOnDay") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacyNumberOfItemsDispensedOnDay" runat="server" TargetControlID="TextBox_EditPharmacyNumberOfItemsDispensedOnDay" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy10">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxDayBefore">Number of Rx's dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyNumberOfRxDayBefore" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfRxDayBefore") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacyNumberOfRxDayBefore" runat="server" TargetControlID="TextBox_EditPharmacyNumberOfRxDayBefore" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy11">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedDayBefore">Number of items dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyNumberOfItemsDispensedDayBefore" runat="server" Width="300px" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedDayBefore") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacyNumberOfItemsDispensedDayBefore" runat="server" TargetControlID="TextBox_EditPharmacyNumberOfItemsDispensedDayBefore" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="Pharmacy12">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribed">Name of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyDrugPrescribed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugPrescribed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy13">
                        <td style="width: 175px;" id="FormPharmacyDrugDispensed">Name of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyDrugDispensed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugDispensed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy14">
                        <td style="width: 175px;" id="FormPharmacyDrugPacked">Name of drug packed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyDrugPacked" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_DrugPacked") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy15">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugPrescribed">Strength of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyStrengthDrugPrescribed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_StrengthDrugPrescribed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy16">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugDispensed">Strength of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyStrengthDrugDispensed" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_StrengthDrugDispensed") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy17">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribedNewOnMarket">Is the drug prescribed new on the market (launched in the last 6 months)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyDrugPrescribedNewOnMarket" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_DrugPrescribedNewOnMarket") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy18">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationOnPrescription">Was all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyLegislativeInformationOnPrescription" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_LegislativeInformationOnPrescription") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy19">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationNotOnPrescription">Why wasn't all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditPharmacyLegislativeInformationNotOnPrescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Pharmacy_LegislativeInformationNotOnPrescription") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy20">
                        <td style="width: 175px;" id="FormPharmacyDoctorName">Name of Doctor
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyDoctorName" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy21">
                        <td style="width: 175px;" id="FormPharmacyFactorsList">Factor(s) contributing to medication incident (Must provide all details in description of incident)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyFactorsList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyFactorsList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Factors_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy22">
                        <td style="width: 175px;" id="FormPharmacySystemRelatedIssuesList">Were there any system related issues
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacySystemRelatedIssuesList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_SystemRelatedIssues_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy23">
                        <td style="width: 175px;" id="FormPharmacyErgonomicProblemsList">Ergonomic Problems
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyErgonomicProblemsList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyErgonomicProblemsList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_ErgonomicProblems_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy24">
                        <td style="width: 175px;" id="FormPharmacyPatientCounselled">Patient counselled for TTO/Retail
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyPatientCounselled" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_PatientCounselled") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy25">
                        <td style="width: 175px;" id="FormPharmacySimilarIncident">Similar incident in the last two months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacySimilarIncident" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_SimilarIncident") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy26">
                        <td style="width: 175px;" id="FormPharmacyLocationList">Location of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyLocationList" runat="server" DataSourceID="SqlDataSource_Incident_EditPharmacyLocationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_Pharmacy_Location_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="Pharmacy27">
                        <td style="width: 175px;" id="FormPharmacyPatientOutcomeAffected">Was the patients outcome affected
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditPharmacyPatientOutcomeAffected" runat="server" SelectedValue='<%# Bind("Incident_Pharmacy_PatientOutcomeAffected") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to SAPS
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS1">
                        <td style="width: 175px;" id="FormReportableSAPSPoliceStation">Police Station
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableSAPSPoliceStation" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_PoliceStation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS2">
                        <td style="width: 175px;" id="FormReportableSAPSInvestigationOfficersName">Investigation Officers Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableSAPSInvestigationOfficersName" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_InvestigationOfficersName") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS3">
                        <td style="width: 175px;" id="FormReportableSAPSTelephoneNumber">Telephone Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableSAPSTelephoneNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_TelephoneNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS4">
                        <td style="width: 175px;" id="FormReportableSAPSCaseNumber">Case Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableSAPSCaseNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_SAPS_CaseNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to Internal Audit
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit1">
                        <td style="width: 175px;" id="FormReportableInternalAuditDateDetected">Date Detected<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditDateDetected" runat="server" Width="75px" Text='<%# Bind("Incident_Reportable_InternalAudit_DateDetected","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportableInternalAuditDateDetected" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportableInternalAuditDateDetected" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportableInternalAuditDateDetected" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportableInternalAuditDateDetected">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportableInternalAuditDateDetected" runat="server" TargetControlID="TextBox_EditReportableInternalAuditDateDetected" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit2">
                        <td style="width: 175px;" id="FormReportableInternalAuditByWhom">By Whom
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditByWhom" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_ByWhom") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit3">
                        <td style="width: 175px;" id="FormReportableInternalAuditTotalLossValue">Total Loss Value
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditTotalLossValue" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalLossValue") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit4">
                        <td style="width: 175px;" id="FormReportableInternalAuditTotalRecovery">Total Recovery
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditTotalRecovery" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalRecovery") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit5">
                        <td style="width: 175px;" id="FormReportableInternalAuditRecoveryPlan">Recovery Plan
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditRecoveryPlan" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_RecoveryPlan") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit6">
                        <td style="width: 175px;" id="FormReportableInternalAuditStatusOfInvestigation">Status Of Investigation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditStatusOfInvestigation" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_StatusOfInvestigation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit7">
                        <td style="width: 175px;" id="FormReportableInternalAuditSAPSNotReported">Reason why Incident was not reported to SAPS 
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableInternalAuditSAPSNotReported" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_InternalAudit_SAPSNotReported") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Root Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRootCategoryList">Root Cause Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditRootCategoryList" runat="server" DataSourceID="SqlDataSource_Incident_EditRootCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Incident_RootCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRootDescription">Root Cause Description
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditRootDescription" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_RootDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigator">Investigator
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigator" runat="server" Width="300px" Text='<%# Bind("Incident_Investigator") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigatorContactNumber">Investigator Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigatorContactNumber" runat="server" Width="300px" Text='<%# Bind("Incident_InvestigatorContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigatorDesignation">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigatorDesignation" runat="server" Width="300px" Text='<%# Bind("Incident_InvestigatorDesignation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationCompleted">Investigation Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditInvestigationCompleted" runat="server" Checked='<%# Bind("Incident_InvestigationCompleted") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigation Completed Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditInvestigationCompletedDate" runat="server" Text='<%# Bind("Incident_InvestigationCompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Degree of Harm
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmList">Degree of Harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-spacing: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_EditDegreeOfHarmList" runat="server" SelectedValue='<%# Bind("Incident_DegreeOfHarm_DegreeOfHarm_List")%>' RepeatDirection="Vertical" RepeatLayout="Table" Width="100%" OnDataBound="RadioButtonList_EditDegreeOfHarmList_DataBound">
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_EditDegreeOfHarmListTotal" runat="server" OnDataBinding="HiddenField_EditDegreeOfHarmListTotal_DataBinding" />
                          <asp:Label ID="Label_EditDegreeOfHarmList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmImpact">Impact or Potential Impact of error / harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="EditDegreeOfHarmImpactImpactList" runat="server" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_EditDegreeOfHarmImpactImpactList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_Incident_EditDegreeOfHarmImpactItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" OnDataBound="CheckBoxList_EditDegreeOfHarmImpactImpactList_DataBound" Width="100%">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_EditDegreeOfHarmImpactImpactListTotal" runat="server" OnDataBinding="HiddenField_EditDegreeOfHarmImpactImpactListTotal_DataBinding" />
                          <asp:GridView ID="GridView_EditDegreeOfHarmImpact" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_Incident_EditDegreeOfHarmImpact" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditDegreeOfHarmImpact_RowCreated" OnPreRender="GridView_EditDegreeOfHarmImpact_PreRender">
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
                                  <td>No Impact or Potential Impact of error / harm
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="Incident_DegreeOfHarm_Impact_Impact_Name" ReadOnly="True" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmCost">Estimated / Actual cost
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDegreeOfHarmCost" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_DegreeOfHarm_Cost") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Label ID="Label_EditDegreeOfHarmCost" runat="server" Text='<%# Eval("Incident_DegreeOfHarm_Cost") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDegreeOfHarmImplications">Potential future implications e.g. antibiotic resistance, infection risk, etc.
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDegreeOfHarmImplications" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_DegreeOfHarm_Implications") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Label ID="Label_EditDegreeOfHarmImplications" runat="server" Text='<%# Eval("Incident_DegreeOfHarm_Implications") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to CEO
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO1">
                        <td style="width: 175px;" id="FormReportableCEOAcknowledgedHM">Acknowledged by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEOAcknowledgedHM" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_AcknowledgedHM") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO13">
                        <td style="width: 175px;" id="FormReportableCEODoctorRelated">Doctor Related
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEODoctorRelated" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_DoctorRelated") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO2">
                        <td style="width: 175px;" id="FormReportableCEOCEONotifiedWithin24Hours">CEO notified within 24hrs
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEOCEONotifiedWithin24Hours" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CEONotifiedWithin24Hours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO3">
                        <td style="width: 175px;" id="FormReportableCEOProgressUpdateSent">Progress report / update sent
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEOProgressUpdateSent" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_ProgressUpdateSent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO4">
                        <td style="width: 175px;" id="FormReportableCEOActionsTakenHM">Actions taken by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableCEOActionsTakenHM" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_CEO_ActionsTakenHM") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO5">
                        <td style="width: 175px;" id="FormReportableCEODate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableCEODate" runat="server" Width="75px" Text='<%# Bind("Incident_Reportable_CEO_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditReportableCEODate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditReportableCEODate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditReportableCEODate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditReportableCEODate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditReportableCEODate" runat="server" TargetControlID="TextBox_EditReportableCEODate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO6">
                        <td style="width: 175px;" id="FormReportableCEOActionsAgainstEmployee">Actions taken against employee
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditReportableCEOActionsAgainstEmployee" runat="server" Checked='<%# Bind("Incident_Reportable_CEO_ActionsAgainstEmployee") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO7">
                        <td style="width: 175px;" id="FormReportableCEOEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableCEOEmployeeNumber" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_CEO_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_EditFindReportableCEOEmployeeName" runat="server" OnClick="Button_EditFindReportableCEOEmployeeName_OnClick" Text="Find Name" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO8">
                        <td style="width: 175px;" id="FormReportableCEOEmployeeName">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableCEOEmployeeName" runat="server" Width="300px" Text='<%# Bind("Incident_Reportable_CEO_EmployeeName") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditReportableCEOEmployeeName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditReportableCEOEmployeeNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO9">
                        <td style="width: 175px;" id="FormReportableCEOOutcome">Outcome
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditReportableCEOOutcome" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_Reportable_CEO_Outcome") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO10">
                        <td style="width: 175px;" id="FormReportableCEOFileScanned">File Scanned
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditReportableCEOFileScanned" runat="server" Checked='<%# Bind("Incident_Reportable_CEO_FileScanned") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO11">
                        <td style="width: 175px;" id="FormReportableCEOCloseOffHM">Close off by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEOCloseOffHM" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CloseOffHM") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO12">
                        <td style="width: 175px;" id="FormReportableCEOCloseOutEmailSend">Close out email sent to CEO
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditReportableCEOCloseOutEmailSend" runat="server" SelectedValue='<%# Bind("Incident_Reportable_CEO_CloseOutEmailSend") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
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
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("Incident_Status") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditStatus_SelectedIndexChanged">
                            <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                          </asp:DropDownList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditStatusDate" runat="server" Text='<%# Bind("Incident_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentStatusRejectedReason">
                        <td style="width: 175px;" id="FormStatusRejectedReason">Rejected Reason
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditStatusRejectedReason" runat="server" TextMode="MultiLine" Rows="4" Width="800px" Text='<%# Bind("Incident_StatusRejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Incident_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Incident_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Incident_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Incident_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" ValidationGroup="Incident_Form" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" ValidationGroup="Incident_Form" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Incident" CssClass="Controls_Button" ValidationGroup="Incident_Form" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
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
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("Incident_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          <asp:HiddenField ID="HiddenField_ItemPatientFalling_TriggerLevel" runat="server" />
                          <asp:HiddenField ID="HiddenField_ItemPatientDetail_TriggerLevel" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemFacilityFrom" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date of Incident<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("Incident_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Time of Incident<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemTimeHours" runat="server" Text='<%# Bind("Incident_Time_Hours") %>'></asp:Label>&nbsp;:&nbsp;<asp:Label ID="Label_ItemTimeMin" runat="server" Text='<%# Bind("Incident_Time_Min") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Person Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportingPerson" runat="server" Text='<%# Bind("Incident_ReportingPerson") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit Originated From
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemUnitFromUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Unit Issued To
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemUnitToUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDisciplineList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Incident Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemIncidentCategoryList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemIncidentCategoryList" runat="server" Value='<%# Eval("Incident_IncidentCategory_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryListSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Customer / Visitor / Relative
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR2">
                        <td style="width: 175px;">Customer / Visitor / Relative Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemCVRName" runat="server" Text='<%# Bind("Incident_CVR_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryCVR3">
                        <td style="width: 175px;">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemCVRContactNumber" runat="server" Text='<%# Bind("Incident_CVR_ContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Employee
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee2">
                        <td style="width: 175px;">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEEmployeeNumber" runat="server" Text='<%# Bind("Incident_E_EmployeeNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee3">
                        <td style="width: 175px;">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEEmployeeName" runat="server" Text='<%# Bind("Incident_E_EmployeeName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee4">
                        <td style="width: 175px;">Employee Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEEmployeeUnitUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee5">
                        <td style="width: 175px;">Employee Status
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEEmployeeStatusList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee6">
                        <td style="width: 175px;">Staff Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEStaffCategoryList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee7">
                        <td style="width: 175px;">Body Part Affected / Injured
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEBodyPartAffectedList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee8">
                        <td style="width: 175px;">Treatment Required
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemETreatmentRequiredList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemETreatmentRequiredList" runat="server" Value='<%# Eval("Incident_E_TreatmentRequired_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee9">
                        <td style="width: 175px;">Number of Days booked off
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEDaysOff" runat="server" Text='<%# Bind("Incident_E_DaysOff") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee10">
                        <td style="width: 175px;">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEMainContributorList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemEMainContributorList" runat="server" Value='<%# Eval("Incident_E_MainContributor_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryEmployee11">
                        <td style="width: 175px;">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemEMainContributorStaffList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemEMainContributorStaffList" runat="server" Value='<%# Eval("Incident_E_MainContributor_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Member of Multi-Disciplinary Team
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT2">
                        <td style="width: 175px;">Member Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemMMDTName" runat="server" Text='<%# Bind("Incident_MMDT_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT3">
                        <td style="width: 175px;">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemMMDTContactNumber" runat="server" Text='<%# Bind("Incident_MMDT_ContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryMMDT4">
                        <td style="width: 175px;">Discipline
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemMMDTDisciplineList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Patient
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient2">
                        <td style="width: 175px;">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPVisitNumber" runat="server" Text='<%# Bind("Incident_P_VisitNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient3">
                        <td style="width: 175px;">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPName" runat="server" Text='<%# Bind("Incident_P_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient4">
                        <td style="width: 175px;">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPMainContributorList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPMainContributorList" runat="server" Value='<%# Eval("Incident_P_MainContributor_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryPatient5">
                        <td style="width: 175px;">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPMainContributorStaffList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPMainContributorStaffList" runat="server" Value='<%# Eval("Incident_P_MainContributor_Staff_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Property / Environment / Other
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty2">
                        <td style="width: 175px;">Main Contributor to Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPropMainContributorList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPropMainContributorList" runat="server" Value='<%# Eval("Incident_Prop_MainContributor_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategoryProperty3">
                        <td style="width: 175px;">Staff Member
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPropMainContributorStaffList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPropMainContributorStaffList" runat="server" Value='<%# Eval("Incident_Prop_MainContributor_Staff_List") %>' />
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS1">
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Category : Supplier / Service Provider
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS2">
                        <td style="width: 175px;">Supplier / Service Provider Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemSSName" runat="server" Text='<%# Bind("Incident_SS_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentCategorySS3">
                        <td style="width: 175px;">Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemSSContactNumber" runat="server" Text='<%# Bind("Incident_SS_ContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Incident Detail
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Description of Incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDescription" runat="server" Text='<%# Bind("Incident_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Type of Incident Level 1
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemLevel1List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Type of Incident Level 2
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemLevel2List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Type of Incident Level 3
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemLevel3List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Incident Severity
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemSeverityList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Incident Reportable
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportable" runat="server" Text='<%# (bool)(Eval("Incident_Reportable"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemReportable" runat="server" Value='<%# Eval("Incident_Reportable") %>' />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" class="tt">
                            <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0;"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ReportableInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable1">
                        <td style="width: 175px;">&nbsp;
                        </td>
                        <td style="width: 25px;">&nbsp;
                        </td>
                        <td style="width: 130px;">Date of Report
                        </td>
                        <td style="width: 530px;">Reference Number
                        </td>
                      </tr>
                      <tr id="IncidentReportable2">
                        <td style="width: 175px;">COID
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportCOID" runat="server" Text='<%# (bool)(Eval("Incident_Report_COID"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportCOIDDate" runat="server" Text='<%# Bind("Incident_Report_COID_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportCOIDNumber" runat="server" Text='<%# Bind("Incident_Report_COID_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable3">
                        <td style="width: 175px;">DEAT
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportDEAT" runat="server" Text='<%# (bool)(Eval("Incident_Report_DEAT"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportDEATDate" runat="server" Text='<%# Bind("Incident_Report_DEAT_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportDEATNumber" runat="server" Text='<%# Bind("Incident_Report_DEAT_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable4">
                        <td style="width: 175px;">Department of Health
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfHealth" runat="server" Text='<%# (bool)(Eval("Incident_Report_DepartmentOfHealth"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfHealthDate" runat="server" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfHealthNumber" runat="server" Text='<%# Bind("Incident_Report_DepartmentOfHealth_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable5">
                        <td style="width: 175px;">Department of Labour
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfLabour" runat="server" Text='<%# (bool)(Eval("Incident_Report_DepartmentOfLabour"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfLabourDate" runat="server" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportDepartmentOfLabourNumber" runat="server" Text='<%# Bind("Incident_Report_DepartmentOfLabour_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable6">
                        <td style="width: 175px;">Hospital Manager
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportHospitalManager" runat="server" Text='<%# (bool)(Eval("Incident_Report_HospitalManager"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportHospitalManagerDate" runat="server" Text='<%# Bind("Incident_Report_HospitalManager_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportHospitalManagerNumber" runat="server" Text='<%# Bind("Incident_Report_HospitalManager_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable7">
                        <td style="width: 175px;">HPCSA
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportHPCSA" runat="server" Text='<%# (bool)(Eval("Incident_Report_HPCSA"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportHPCSADate" runat="server" Text='<%# Bind("Incident_Report_HPCSA_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportHPCSANumber" runat="server" Text='<%# Bind("Incident_Report_HPCSA_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable8">
                        <td style="width: 175px;">Legal Department
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportLegalDepartment" runat="server" Text='<%# (bool)(Eval("Incident_Report_LegalDepartment"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportLegalDepartmentDate" runat="server" Text='<%# Bind("Incident_Report_LegalDepartment_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportLegalDepartmentNumber" runat="server" Text='<%# Bind("Incident_Report_LegalDepartment_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable9">
                        <td style="width: 175px;">CEO
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportCEO" runat="server" Text='<%# (bool)(Eval("Incident_Report_CEO"))?"Yes":"No" %>'></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_ItemReportCEO" runat="server" Value='<%# Eval("Incident_Report_CEO") %>' />
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportCEODate" runat="server" Text='<%# Bind("Incident_Report_CEO_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportCEONumber" runat="server" Text='<%# Bind("Incident_Report_CEO_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable10">
                        <td style="width: 175px;">Pharmacy Council
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportPharmacyCouncil" runat="server" Text='<%# (bool)(Eval("Incident_Report_PharmacyCouncil"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportPharmacyCouncilDate" runat="server" Text='<%# Bind("Incident_Report_PharmacyCouncil_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportPharmacyCouncilNumber" runat="server" Text='<%# Bind("Incident_Report_PharmacyCouncil_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable11">
                        <td style="width: 175px;">Quality Department
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportQuality" runat="server" Text='<%# (bool)(Eval("Incident_Report_Quality"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportQualityDate" runat="server" Text='<%# Bind("Incident_Report_Quality_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportQualityNumber" runat="server" Width="200px" Text='<%# Bind("Incident_Report_Quality_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable12">
                        <td style="width: 175px;">RM
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportRM" runat="server" Text='<%# (bool)(Eval("Incident_Report_RM"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportRMDate" runat="server" Text='<%# Bind("Incident_Report_RM_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportRMNumber" runat="server" Width="200px" Text='<%# Bind("Incident_Report_RM_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable13">
                        <td style="width: 175px;">SANC
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportSANC" runat="server" Text='<%# (bool)(Eval("Incident_Report_SANC"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportSANCDate" runat="server" Text='<%# Bind("Incident_Report_SANC_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportSANCNumber" runat="server" Width="200px" Text='<%# Bind("Incident_Report_SANC_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable14">
                        <td style="width: 175px;">SAPS
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportSAPS" runat="server" Text='<%# (bool)(Eval("Incident_Report_SAPS"))?"Yes":"No" %>'></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_ItemReportSAPS" runat="server" Value='<%# Eval("Incident_Report_SAPS") %>' />
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportSAPSDate" runat="server" Text='<%# Bind("Incident_Report_SAPS_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportSAPSNumber" runat="server" Width="200px" Text='<%# Bind("Incident_Report_SAPS_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportable15">
                        <td style="width: 175px;">Internal Audit
                        </td>
                        <td style="width: 25px;">
                          <asp:Label ID="Label_ItemReportInternalAudit" runat="server" Text='<%# (bool)(Eval("Incident_Report_InternalAudit"))?"Yes":"No" %>'></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_ItemReportInternalAudit" runat="server" Value='<%# Eval("Incident_Report_InternalAudit") %>' />
                        </td>
                        <td style="width: 130px;">
                          <asp:Label ID="Label_ItemReportInternalAuditDate" runat="server" Text='<%# Bind("Incident_Report_InternalAudit_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 530px;">
                          <asp:Label ID="Label_ItemReportInternalAuditNumber" runat="server" Width="200px" Text='<%# Bind("Incident_Report_InternalAudit_Number") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientFallingSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientFallingHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Falling
                        </td>
                      </tr>
                      <tr id="PatientFalling1">
                        <td style="width: 175px;" id="FormPatientFallingWhereFallOccurList">Where did fall / alleged fall occur
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientFallingWhereFallOccurList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="PatientDetailSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PatientDetailHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Patient Detail
                        </td>
                      </tr>
                      <tr id="PatientDetail1">
                        <td style="width: 175px;" id="FormPatientDetailVisitNumber">Patient Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientDetailVisitNumber" runat="server" Text='<%# Bind("Incident_PatientDetail_VisitNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPatientDetailVisitNumber" runat="server" Value='<%# Eval("Incident_PatientDetail_VisitNumber") %>' />
                        </td>
                      </tr>
                      <tr id="PatientDetail2">
                        <td style="width: 175px;" id="FormPatientDetailName">Patient Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientDetailName" runat="server" Text='<%# Bind("Incident_PatientDetail_Name") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="PharmacySpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="PharmacyHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 175px;" id="FormPharmacyInitials">Pharmacy staff member initials
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyInitials" runat="server" Text='<%# Bind("Incident_Pharmacy_Initials") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacyInitials" runat="server" Value='<%# Eval("Incident_Pharmacy_Initials") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 175px;" id="FormPharmacyStaffInvolvedList">Staff member involved
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyStaffInvolvedList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 175px;" id="FormPharmacyCheckingList">Checking
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyCheckingList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 175px;" id="FormPharmacyLocumOrPermanent">Is the relevant staff member a regular locum or permanent employee working for longer than 3 months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyLocumOrPermanent" runat="server" Text='<%# Bind("Incident_Pharmacy_LocumOrPermanent") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 175px;" id="FormPharmacyStaffOnDutyList">Where all staff on duty, If not why
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyStaffOnDutyList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 175px;" id="FormPharmacyChangeInWorkProcedure">Change in work procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyChangeInWorkProcedure" runat="server" Text='<%# Bind("Incident_Pharmacy_ChangeInWorkProcedure") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 175px;" id="FormPharmacyTypeOfPrescriptionList">Type of prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyTypeOfPrescriptionList" runat="server"></asp:Label>                          
                          <asp:HiddenField ID="HiddenField_ItemPharmacyTypeOfPrescriptionList" runat="server" Value='<%# Eval("Incident_Pharmacy_TypeOfPrescription_List") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxOnDay">Number of Rx's on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyNumberOfRxOnDay" runat="server" Text='<%# Bind("Incident_Pharmacy_NumberOfRxOnDay") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy9">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedOnDay">Number of items dispensed on date of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyNumberOfItemsDispensedOnDay" runat="server" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedOnDay") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy10">
                        <td style="width: 175px;" id="FormPharmacyNumberOfRxDayBefore">Number of Rx's dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyNumberOfRxDayBefore" runat="server" Text='<%# Bind("Incident_Pharmacy_NumberOfRxDayBefore") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy11">
                        <td style="width: 175px;" id="FormPharmacyNumberOfItemsDispensedDayBefore">Number of items dispensed day before
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyNumberOfItemsDispensedDayBefore" runat="server" Text='<%# Bind("Incident_Pharmacy_NumberOfItemsDispensedDayBefore") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy12">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribed">Name of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyDrugPrescribed" runat="server" Text='<%# Bind("Incident_Pharmacy_DrugPrescribed") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy13">
                        <td style="width: 175px;" id="FormPharmacyDrugDispensed">Name of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyDrugDispensed" runat="server" Text='<%# Bind("Incident_Pharmacy_DrugDispensed") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy14">
                        <td style="width: 175px;" id="FormPharmacyDrugPacked">Name of drug packed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyDrugPacked" runat="server" Text='<%# Bind("Incident_Pharmacy_DrugPacked") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy15">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugPrescribed">Strength of drug prescribed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyStrengthDrugPrescribed" runat="server" Text='<%# Bind("Incident_Pharmacy_StrengthDrugPrescribed") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy16">
                        <td style="width: 175px;" id="FormPharmacyStrengthDrugDispensed">Strength of drug dispensed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyStrengthDrugDispensed" runat="server" Text='<%# Bind("Incident_Pharmacy_StrengthDrugDispensed") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy17">
                        <td style="width: 175px;" id="FormPharmacyDrugPrescribedNewOnMarket">Is the drug prescribed new on the market (launched in the last 6 months)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyDrugPrescribedNewOnMarket" runat="server" Text='<%# Bind("Incident_Pharmacy_DrugPrescribedNewOnMarket") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy18">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationOnPrescription">Was all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyLegislativeInformationOnPrescription" runat="server" Text='<%# Bind("Incident_Pharmacy_LegislativeInformationOnPrescription") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacyLegislativeInformationOnPrescription" runat="server" Value='<%# Eval("Incident_Pharmacy_LegislativeInformationOnPrescription") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy19">
                        <td style="width: 175px;" id="FormPharmacyLegislativeInformationNotOnPrescription">Why wasn't all the legislative information on the prescription
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyLegislativeInformationNotOnPrescription" runat="server" Text='<%# Bind("Incident_Pharmacy_LegislativeInformationNotOnPrescription") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy20">
                        <td style="width: 175px;" id="FormPharmacyDoctorName">Name of Doctor
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyDoctorName" runat="server" Text='<%# Bind("Incident_Pharmacy_DoctorName") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy21">
                        <td style="width: 175px;" id="FormPharmacyFactorsList">Factor(s) contributing to medication incident (Must provide all details in description of incident)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyFactorsList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy22">
                        <td style="width: 175px;" id="FormPharmacySystemRelatedIssuesList">Were there any system related issues
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacySystemRelatedIssuesList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy23">
                        <td style="width: 175px;" id="FormPharmacyErgonomicProblemsList">Ergonomic Problems
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyErgonomicProblemsList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy24">
                        <td style="width: 175px;" id="FormPharmacyPatientCounselled">Patient counselled for TTO/Retail
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyPatientCounselled" runat="server" Text='<%# Bind("Incident_Pharmacy_PatientCounselled") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy25">
                        <td style="width: 175px;" id="FormPharmacySimilarIncident">Similar incident in the last two months
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacySimilarIncident" runat="server" Text='<%# Bind("Incident_Pharmacy_SimilarIncident") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy26">
                        <td style="width: 175px;" id="FormPharmacyLocationList">Location of incident
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyLocationList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy27">
                        <td style="width: 175px;" id="FormPharmacyPatientOutcomeAffected">Was the patients outcome affected
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPharmacyPatientOutcomeAffected" runat="server" Text='<%# Bind("Incident_Pharmacy_PatientOutcomeAffected") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPSHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to SAPS
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS1">
                        <td style="width: 175px;" id="FormReportableSAPSPoliceStation">Police Station
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableSAPSPoliceStation" runat="server" Text='<%# Bind("Incident_Reportable_SAPS_PoliceStation") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS2">
                        <td style="width: 175px;" id="FormReportableSAPSInvestigationOfficersName">Investigation Officers Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableSAPSInvestigationOfficersName" runat="server" Text='<%# Bind("Incident_Reportable_SAPS_InvestigationOfficersName") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS3">
                        <td style="width: 175px;" id="FormReportableSAPSTelephoneNumber">Telephone Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableSAPSTelephoneNumber" runat="server" Text='<%# Bind("Incident_Reportable_SAPS_TelephoneNumber") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableSAPS4">
                        <td style="width: 175px;" id="FormReportableSAPSCaseNumber">Case Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableSAPSCaseNumber" runat="server" Text='<%# Bind("Incident_Reportable_SAPS_CaseNumber") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAuditHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to Internal Audit
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit1">
                        <td style="width: 175px;">Date Detected<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditDateDetected" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_DateDetected","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit2">
                        <td style="width: 175px;">By Whom
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditByWhom" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_ByWhom") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit3">
                        <td style="width: 175px;">Total Loss Value
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditTotalLossValue" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalLossValue") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit4">
                        <td style="width: 175px;">Total Recovery
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditTotalRecovery" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_TotalRecovery") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit5">
                        <td style="width: 175px;">Recovery Plan
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditRecoveryPlan" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_RecoveryPlan") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit6">
                        <td style="width: 175px;">Status Of Investigation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditStatusOfInvestigation" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_StatusOfInvestigation") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableInternalAudit7">
                        <td style="width: 175px;">Reason why Incident was not reported to SAPS 
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableInternalAuditSAPSNotReported" runat="server" Text='<%# Bind("Incident_Reportable_InternalAudit_SAPSNotReported") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Root Cause Analysis
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Root Cause Category
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemRootCategoryList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Root Cause Description
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemRootDescription" runat="server" Text='<%# Bind("Incident_RootDescription") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigator
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigator" runat="server" Text='<%# Bind("Incident_Investigator") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigator Contact Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigatorContactNumber" runat="server" Text='<%# Bind("Incident_InvestigatorContactNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigatorDesignation" runat="server" Text='<%# Bind("Incident_InvestigatorDesignation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigation Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationCompleted" runat="server" Text='<%# (bool)(Eval("Incident_InvestigationCompleted"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemInvestigationCompleted" runat="server" Value='<%# Eval("Incident_InvestigationCompleted") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Investigation Completed Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationCompletedDate" runat="server" Text='<%# Bind("Incident_InvestigationCompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Degree of Harm
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Degree of Harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-spacing: 0px;" colspan="3">
                          <asp:Label ID="Label_ItemDegreeOfHarmList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Impact or Potential Impact of error / harm
                        </td>
                        <td style="width: 825px; padding: 0px; border-left-width: 1px; border-top-width: 1px;" colspan="3">
                          <asp:GridView ID="GridView_ItemDegreeOfHarmImpact" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_Incident_ItemDegreeOfHarmImpact" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemDegreeOfHarmImpact_RowCreated" OnPreRender="GridView_ItemDegreeOfHarmImpact_PreRender">
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
                                  <td>No Impact or Potential Impact of error / harm
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="Incident_DegreeOfHarm_Impact_Impact_Name" ReadOnly="True" />
                            </Columns>
                          </asp:GridView>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Estimated / Actual cost
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDegreeOfHarmCost" runat="server" Text='<%# Bind("Incident_DegreeOfHarm_Cost") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Potential future implications e.g. antibiotic resistance, infection risk, etc.
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDegreeOfHarmImplications" runat="server" Text='<%# Bind("Incident_DegreeOfHarm_Implications") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOSpace">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEOHeading">
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable to CEO
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO1">
                        <td style="width: 175px;">Acknowledged by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOAcknowledgedHM" runat="server" Text='<%# Bind("Incident_Reportable_CEO_AcknowledgedHM") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO13">
                        <td style="width: 175px;">Doctor Related
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEODoctorRelated" runat="server" Text='<%# Bind("Incident_Reportable_CEO_DoctorRelated") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO2">
                        <td style="width: 175px;">CEO notified within 24hrs
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOCEONotifiedWithin24Hours" runat="server" Text='<%# Bind("Incident_Reportable_CEO_CEONotifiedWithin24Hours") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO3">
                        <td style="width: 175px;">Progress report / update sent
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOProgressUpdateSent" runat="server" Text='<%# Bind("Incident_Reportable_CEO_ProgressUpdateSent") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO4">
                        <td style="width: 175px;">Actions taken by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOActionsTakenHM" runat="server" Text='<%# Bind("Incident_Reportable_CEO_ActionsTakenHM") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO5">
                        <td style="width: 175px;">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEODate" runat="server" Text='<%# Bind("Incident_Reportable_CEO_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO6">
                        <td style="width: 175px;">Actions taken against employee
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOActionsAgainstEmployee" runat="server" Text='<%# (bool)(Eval("Incident_Reportable_CEO_ActionsAgainstEmployee"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO7">
                        <td style="width: 175px;">Employee Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOEmployeeNumber" runat="server" Text='<%# Bind("Incident_Reportable_CEO_EmployeeNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO8">
                        <td style="width: 175px;">Employee Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOEmployeeName" runat="server" Text='<%# Bind("Incident_Reportable_CEO_EmployeeName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO9">
                        <td style="width: 175px;">Outcome
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOOutcome" runat="server" Text='<%# Bind("Incident_Reportable_CEO_Outcome") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO10">
                        <td style="width: 175px;">File Scanned
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOFileScanned" runat="server" Text='<%# (bool)(Eval("Incident_Reportable_CEO_FileScanned"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO11">
                        <td style="width: 175px;">Close off by Hospital Manager
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOCloseOffHM" runat="server" Text='<%# Bind("Incident_Reportable_CEO_CloseOffHM") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentReportableCEO12">
                        <td style="width: 175px;">Close out email sent to CEO
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemReportableCEOCloseOutEmailSend" runat="server" Text='<%# Bind("Incident_Reportable_CEO_CloseOutEmailSend") %>'></asp:Label>&nbsp;
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
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemStatus" runat="server" Text='<%# Bind("Incident_Status") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemStatus" runat="server" Value='<%# Bind("Incident_Status") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemStatusDate" runat="server" Text='<%# Bind("Incident_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="IncidentStatusRejectedReason">
                        <td style="width: 175px;">Rejected Reason
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemStatusRejectedReason" runat="server" Text='<%# Bind("Incident_StatusRejectedReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Incident_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Incident_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Incident_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Incident_ModifiedBy") %>'></asp:Label>&nbsp;
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
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertFacility" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertFacilityFrom" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertUnitFromUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertUnitToUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertIncidentCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEEmployeeUnitUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEEmployeeStatusList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEStaffCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEBodyPartAffectedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertETreatmentRequiredList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertEMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertMMDTDisciplineList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPropMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPropMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertLevel1List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertLevel2List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertLevel3List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertSeverityList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyStaffInvolvedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyCheckingList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyStaffOnDutyList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyFactorsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertPharmacyLocationList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertRootCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_InsertDisciplineList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditFacilityFrom" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditUnitFromUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditUnitToUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditIncidentCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEEmployeeUnitUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEEmployeeStatusList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEStaffCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEBodyPartAffectedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditETreatmentRequiredList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditEMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditMMDTDisciplineList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPropMainContributorList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPropMainContributorStaffList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditLevel1List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditLevel2List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditLevel3List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditSeverityList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPatientFallingWhereFallOccurList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditDegreeOfHarmImpactItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditDegreeOfHarmImpact" runat="server" OnSelected="SqlDataSource_Incident_EditDegreeOfHarmImpact_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyStaffInvolvedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyCheckingList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyStaffOnDutyList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyFactorsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyErgonomicProblemsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditPharmacyLocationList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditRootCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_EditDisciplineList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_ItemDegreeOfHarmImpact" runat="server" OnSelected="SqlDataSource_Incident_ItemDegreeOfHarmImpact_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Incident_Form" runat="server" OnInserted="SqlDataSource_Incident_Form_Inserted" OnUpdated="SqlDataSource_Incident_Form_Updated"></asp:SqlDataSource>
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
