<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_PharmacyClinicalMetrics.aspx.cs" Inherits="InfoQuestForm.Form_PharmacyClinicalMetrics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy Clinical Metrics</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PharmacyClinicalMetrics.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PharmacyClinicalMetrics" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PharmacyClinicalMetrics" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PharmacyClinicalMetrics" AssociatedUpdatePanelID="UpdatePanel_PharmacyClinicalMetrics">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PharmacyClinicalMetrics" runat="server">
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
          <table class="Table" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SearchHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_InvalidSearchMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchFacility" style="width: 150px">Facility</td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchDate" style="width: 150px">Date</td>
                    <td>
                      <asp:TextBox ID="TextBox_Date" runat="server" Width="75px" CssClass="Controls_TextBox" EnableViewState="false"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_Date" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" EnableViewState="false" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_Date" runat="server" CssClass="Calendar" TargetControlID="TextBox_Date" Format="yyyy/MM/dd" PopupButtonID="ImageButton_Date" EnableViewState="false">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_Date" runat="server" TargetControlID="TextBox_Date" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark" EnableViewState="false">
                      </Ajax:TextBoxWatermarkExtender>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchIntervention" style="width: 150px">Intervention</td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Intervention" runat="server" DataSourceID="SqlDataSource_Intervention" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Intervention</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Intervention" runat="server"></asp:SqlDataSource>&nbsp;&nbsp;
                      <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InterventionIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                    </td>
                  </tr>
                  <tr id="PatientVisitNumber">
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number</td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PatientVisitNumber" runat="server" TargetControlID="TextBox_PatientVisitNumber" FilterType="Numbers">
                      </Ajax:FilteredTextBoxExtender>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" CausesValidation="False" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" CausesValidation="False" />&nbsp;
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
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivTherapeuticInterventionPatientInfo" runat="server">
            &nbsp;
          </div>
          <table id="TableTherapeuticInterventionPatientInfo" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_TherapeuticInterventionPatientInfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 115px">Facility:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Age:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Visit Number:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Admission:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Surname, Name:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Discharge:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivTherapeuticIntervention" runat="server">
            &nbsp;
          </div>
          <table id="TableTherapeuticIntervention" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_TherapeuticInterventionHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form" runat="server" DataKeyNames="PCM_TI_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form" OnItemInserting="FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemCommand" OnDataBound="FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DataBound" OnItemUpdating="FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Date
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_InsertDate" runat="server" Text="" CssClass="Controls_Error"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("PCM_TI_ReportNumber") %>' EnableViewState="false"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertInterventionBy" runat="server" Width="300px" Text='<%# Bind("PCM_TI_InterventionBy") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" AppendDataBoundItems="true" DataTextField="Ward" DataValueField="Ward" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                            <asp:ListItem Value="Pharmacy">Pharmacy</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoctor">Doctor
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDoctor" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoctorOther">
                        <td style="width: 170px;" id="FormDoctorOther">Doctor Other
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertDoctorOther" runat="server" Text='<%# Bind("PCM_TI_DoctorOther") %>' Width="300px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTime">Time (Min)
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertTime" runat="server" Text='<%# Bind("PCM_TI_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertTime" runat="server" TargetControlID="TextBox_InsertTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Indication</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationNoIndication">No indication for drug</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertIndicationNoIndication" runat="server" Checked='<%# Bind("PCM_TI_Indication_NoIndication") %>' /></td>
                      </tr>
                      <tr id="IndicationNoIndicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationNoIndicationIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_NoIndication_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationNoIndicationCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_NoIndication_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertIndicationNoIndicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertIndicationNoIndicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_NoIndication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationDuplication">Therapeutic duplication</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertIndicationDuplication" runat="server" Checked='<%# Bind("PCM_TI_Indication_Duplication") %>' /></td>
                      </tr>
                      <tr id="IndicationDuplicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationDuplicationIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Duplication_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationDuplicationCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Duplication_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertIndicationDuplicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertIndicationDuplicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Duplication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationUntreated">Untreated medical condition</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertIndicationUntreated" runat="server" Checked='<%# Bind("PCM_TI_Indication_Untreated") %>' /></td>
                      </tr>
                      <tr id="IndicationUntreatedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationUntreatedIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Untreated_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertIndicationUntreatedCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Untreated_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertIndicationUntreatedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertIndicationUntreatedComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Untreated_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Dose</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseDose">Dose</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertDoseDose" runat="server" Checked='<%# Bind("PCM_TI_Dose_Dose") %>' /></td>
                      </tr>
                      <tr id="DoseDoseList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseList">Dose</td>
                        <td style="width: 830px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertDoseDoseList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_List")%>' RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%">
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_InsertDoseDoseListTotal" runat="server" />
                        </td>
                      </tr>
                      <tr id="DoseDoseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDoseDoseIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoseDoseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDoseDoseCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertDoseDoseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseDoseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertDoseDoseComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Dose_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseInterval">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertDoseInterval" runat="server" Checked='<%# Bind("PCM_TI_Dose_Interval") %>' /></td>
                      </tr>
                      <tr id="DoseIntervalList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalList">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertDoseIntervalList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_List")%>' RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%">
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_InsertDoseIntervalListTotal" runat="server" />
                        </td>
                      </tr>
                      <tr id="DoseIntervalIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDoseIntervalIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoseIntervalCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDoseIntervalCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertDoseIntervalCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseIntervalComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertDoseIntervalComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Interval_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Efficacy</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormEfficacyChange">Change drug to more effective alternative</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertEfficacyChange" runat="server" Checked='<%# Bind("PCM_TI_Efficacy_Change") %>' /></td>
                      </tr>
                      <tr id="EfficacyChangeIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertEfficacyChangeIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Efficacy_Change_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertEfficacyChangeCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Efficacy_Change_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertEfficacyChangeCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertEfficacyChangeComment" runat="server" Text='<%# Bind("PCM_TI_Efficacy_Change_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Safety</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyAllergic">Allergic reaction occurred</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyAllergic" runat="server" Checked='<%# Bind("PCM_TI_Safety_Allergic") %>' /></td>
                      </tr>
                      <tr id="SafetyAllergicURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_InsertSafetyAllergicURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyAllergicIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Allergic_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyAllergicCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Allergic_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyAllergicCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyAllergicComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Allergic_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyUnwanted">Unwanted side effects occurred</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyUnwanted" runat="server" Checked='<%# Bind("PCM_TI_Safety_Unwanted") %>' /></td>
                      </tr>
                      <tr id="SafetyUnwantedURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_InsertSafetyUnwantedURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyUnwantedIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Unwanted_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyUnwantedCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Unwanted_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyUnwantedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyUnwantedComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Unwanted_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDrug">Drug-Drug interaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyDrugDrug" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDrug") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDrugIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDrugIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDrug_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDrugCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDrug_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyDrugDrugCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyDrugDrugComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDrug_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDiluent">Drug-diluent / Infusion solution incompatibility</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyDrugDiluent" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDiluent") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDiluentIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDiluentIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDiluent_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDiluentCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDiluent_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyDrugDiluentCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyDrugDiluentComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDiluent_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugLab">Drug-lab test contradiction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyDrugLab" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugLab") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugLabIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugLabIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugLab_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugLabCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugLab_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyDrugLabCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyDrugLabComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugLab_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDisease">Drug-disease / medical condition interaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertSafetyDrugDisease" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDisease") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDiseaseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDiseaseIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDisease_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertSafetyDrugDiseaseCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDisease_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertSafetyDrugDiseaseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertSafetyDrugDiseaseComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDisease_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Medication Error</td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:HyperLink ID="HyperLink_InsertMedicationErrorURL_Alert" runat="server" Target="_blank">InfoQuest Alert Form</asp:HyperLink>&nbsp;&nbsp;/&nbsp;&nbsp;
                          <asp:HyperLink ID="HyperLink_InsertMedicationErrorURL_Incident" runat="server" Target="_blank">InfoQuest Incident Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMissed">Missed dose</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorMissed" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Missed") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorMissedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMissedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorMissedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Missed_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrectDrug">Incorrect drug administered</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorIncorrectDrug" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_IncorrectDrug") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorIncorrectDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorIncorrectDrugComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_IncorrectDrug_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrect">Incorrect dose administered</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorIncorrect" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Incorrect") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorIncorrectComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorIncorrectComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Incorrect_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorPrescribed">Medication prescribed in presence of known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorPrescribed" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Prescribed") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorPrescribedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorPrescribedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorPrescribedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Prescribed_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorAdministered">Medication administered in presence of a known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorAdministered" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Administered") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorAdministeredComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorAdministeredComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorAdministeredComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Administered_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMedication">Medication out of stock</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicationErrorMedication" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Medication") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorMedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMedicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationErrorMedicationComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Medication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Cost</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostGeneric">Generic substitution</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCostGeneric" runat="server" Checked='<%# Bind("PCM_TI_Cost_Generic") %>' /></td>
                      </tr>
                      <tr id="CostGenericComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostGenericComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCostGenericComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Generic_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostSubstitution">Therapeutic substitution</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCostSubstitution" runat="server" Checked='<%# Bind("PCM_TI_Cost_Substitution") %>' /></td>
                      </tr>
                      <tr id="CostSubstitutionComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostSubstitutionComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCostSubstitutionComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Substitution_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostDecrease">Intervention lead to a decrease in drug therapy costs</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCostDecrease" runat="server" Checked='<%# Bind("PCM_TI_Cost_Decrease") %>' /></td>
                      </tr>
                      <tr id="CostDecreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostDecreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCostDecreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Decrease_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostIncrease">Intervention lead to an initial increase in drug therapy costs, but improved patient outcomes</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCostIncrease" runat="server" Checked='<%# Bind("PCM_TI_Cost_Increase") %>' /></td>
                      </tr>
                      <tr id="CostIncreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostIncreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCostIncreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Increase_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("PCM_TI_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("PCM_TI_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("PCM_TI_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("PCM_TI_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("PCM_TI_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCancel_TherapeuticIntervention" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertCancel_TherapeuticIntervention_Click" EnableViewState="false" />&nbsp;
                          <asp:Button ID="Button_InsertAdd_TherapeuticIntervention" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Intervention" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
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
                        <td style="width: 170px;">Date
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_EditDate" runat="server" Text="" CssClass="Controls_Error"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("PCM_TI_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditInterventionBy" runat="server" Width="300px" Text='<%# Bind("PCM_TI_InterventionBy") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" AppendDataBoundItems="true" DataTextField="Ward" DataValueField="Ward" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoctor">Doctor
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDoctor" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoctorOther">
                        <td style="width: 170px;" id="FormDoctorOther">Doctor Other
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditDoctorOther" runat="server" Text='<%# Bind("PCM_TI_DoctorOther") %>' Width="300px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTime">Time (Min)
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditTime" runat="server" Text='<%# Bind("PCM_TI_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditTime" runat="server" TargetControlID="TextBox_EditTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Indication</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationNoIndication">No indication for drug</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditIndicationNoIndication" runat="server" Checked='<%# Bind("PCM_TI_Indication_NoIndication") %>' /></td>
                      </tr>
                      <tr id="IndicationNoIndicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationNoIndicationIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_NoIndication_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationNoIndicationCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_NoIndication_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditIndicationNoIndicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditIndicationNoIndicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_NoIndication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationDuplication">Therapeutic duplication</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditIndicationDuplication" runat="server" Checked='<%# Bind("PCM_TI_Indication_Duplication") %>' /></td>
                      </tr>
                      <tr id="IndicationDuplicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationDuplicationIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Duplication_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationDuplicationCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Duplication_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditIndicationDuplicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditIndicationDuplicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Duplication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationUntreated">Untreated medical condition</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditIndicationUntreated" runat="server" Checked='<%# Bind("PCM_TI_Indication_Untreated") %>' /></td>
                      </tr>
                      <tr id="IndicationUntreatedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationUntreatedIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Untreated_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditIndicationUntreatedCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Indication_Untreated_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditIndicationUntreatedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditIndicationUntreatedComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Untreated_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Dose</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseDose">Dose</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditDoseDose" runat="server" Checked='<%# Bind("PCM_TI_Dose_Dose") %>' /></td>
                      </tr>
                      <tr id="DoseDoseList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseList">Dose</td>
                        <td style="width: 830px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditDoseDoseList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_List")%>' RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" OnDataBound="RadioButtonList_EditDoseDoseList_DataBound">
                            <asp:ListItem Text=""></asp:ListItem>
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_EditDoseDoseListTotal" runat="server" />
                        </td>
                      </tr>
                      <tr id="DoseDoseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDoseDoseIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoseDoseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDoseDoseCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Dose_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditDoseDoseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseDoseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditDoseDoseComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Dose_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseInterval">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditDoseInterval" runat="server" Checked='<%# Bind("PCM_TI_Dose_Interval") %>' /></td>
                      </tr>
                      <tr id="DoseIntervalList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalList">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditDoseIntervalList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_List")%>' RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" OnDataBound="RadioButtonList_EditDoseIntervalList_DataBound">
                            <asp:ListItem Text=""></asp:ListItem>
                          </asp:RadioButtonList>
                          <asp:HiddenField ID="HiddenField_EditDoseIntervalListTotal" runat="server" />
                        </td>
                      </tr>
                      <tr id="DoseIntervalIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDoseIntervalIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="DoseIntervalCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDoseIntervalCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Dose_Interval_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditDoseIntervalCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseIntervalComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditDoseIntervalComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Interval_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Efficacy</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormEfficacyChange">Change drug to more effective alternative</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditEfficacyChange" runat="server" Checked='<%# Bind("PCM_TI_Efficacy_Change") %>' /></td>
                      </tr>
                      <tr id="EfficacyChangeIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditEfficacyChangeIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Efficacy_Change_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditEfficacyChangeCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Efficacy_Change_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditEfficacyChangeCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditEfficacyChangeComment" runat="server" Text='<%# Bind("PCM_TI_Efficacy_Change_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Safety</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyAllergic">Allergic reaction occurred</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyAllergic" runat="server" Checked='<%# Bind("PCM_TI_Safety_Allergic") %>' /></td>
                      </tr>
                      <tr id="SafetyAllergicURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_EditSafetyAllergicURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyAllergicIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Allergic_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyAllergicCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Allergic_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyAllergicCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyAllergicComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Allergic_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyUnwanted">Unwanted side effects occurred</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyUnwanted" runat="server" Checked='<%# Bind("PCM_TI_Safety_Unwanted") %>' /></td>
                      </tr>
                      <tr id="SafetyUnwantedURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_EditSafetyUnwantedURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyUnwantedIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Unwanted_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyUnwantedCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_Unwanted_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyUnwantedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyUnwantedComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Unwanted_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDrug">Drug-Drug interaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyDrugDrug" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDrug") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDrugIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDrugIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDrug_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDrugCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDrug_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyDrugDrugCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyDrugDrugComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDrug_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDiluent">Drug-diluent / Infusion solution incompatibility</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyDrugDiluent" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDiluent") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDiluentIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDiluentIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDiluent_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDiluentCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDiluent_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyDrugDiluentCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyDrugDiluentComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDiluent_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugLab">Drug-lab test contradiction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyDrugLab" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugLab") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugLabIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugLabIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugLab_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugLabCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugLab_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyDrugLabCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyDrugLabComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugLab_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDisease">Drug-disease / medical condition interaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditSafetyDrugDisease" runat="server" Checked='<%# Bind("PCM_TI_Safety_DrugDisease") %>' /></td>
                      </tr>
                      <tr id="SafetyDrugDiseaseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDiseaseIRList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDisease_IR_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Response</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditSafetyDrugDiseaseCSList" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PCM_TI_Safety_DrugDisease_CS_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Significance</asp:ListItem>
                          </asp:DropDownList>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditSafetyDrugDiseaseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditSafetyDrugDiseaseComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDisease_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Medication Error</td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:HyperLink ID="HyperLink_EditMedicationErrorURL_Alert" runat="server" Target="_blank">InfoQuest Alert Form</asp:HyperLink>&nbsp;&nbsp;/&nbsp;&nbsp;
                          <asp:HyperLink ID="HyperLink_EditMedicationErrorURL_Incident" runat="server" Target="_blank">InfoQuest Incident Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMissed">Missed dose</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorMissed" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Missed") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorMissedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMissedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorMissedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Missed_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrectDrug">Incorrect drug administered</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorIncorrectDrug" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_IncorrectDrug") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorIncorrectDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorIncorrectDrugComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_IncorrectDrug_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrect">Incorrect dose administered</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorIncorrect" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Incorrect") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorIncorrectComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorIncorrectComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Incorrect_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorPrescribed">Medication prescribed in presence of known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorPrescribed" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Prescribed") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorPrescribedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorPrescribedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorPrescribedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Prescribed_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorAdministered">Medication administered in presence of a known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorAdministered" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Administered") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorAdministeredComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorAdministeredComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorAdministeredComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Administered_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMedication">Medication out of stock</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicationErrorMedication" runat="server" Checked='<%# Bind("PCM_TI_MedicationError_Medication") %>' /></td>
                      </tr>
                      <tr id="MedicationErrorMedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMedicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationErrorMedicationComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Medication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Cost</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostGeneric">Generic substitution</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCostGeneric" runat="server" Checked='<%# Bind("PCM_TI_Cost_Generic") %>' /></td>
                      </tr>
                      <tr id="CostGenericComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostGenericComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCostGenericComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Generic_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostSubstitution">Therapeutic substitution</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCostSubstitution" runat="server" Checked='<%# Bind("PCM_TI_Cost_Substitution") %>' /></td>
                      </tr>
                      <tr id="CostSubstitutionComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostSubstitutionComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCostSubstitutionComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Substitution_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostDecrease">Intervention lead to a decrease in drug therapy costs</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCostDecrease" runat="server" Checked='<%# Bind("PCM_TI_Cost_Decrease") %>' /></td>
                      </tr>
                      <tr id="CostDecreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostDecreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCostDecreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Decrease_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostIncrease">Intervention lead to an initial increase in drug therapy costs, but improved patient outcomes</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCostIncrease" runat="server" Checked='<%# Bind("PCM_TI_Cost_Increase") %>' /></td>
                      </tr>
                      <tr id="CostIncreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostIncreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCostIncreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Increase_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("PCM_TI_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("PCM_TI_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("PCM_TI_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("PCM_TI_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("PCM_TI_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Update" Text="Print Intervention" CssClass="Controls_Button" OnClick="Button_EditPrint_TherapeuticIntervention_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_TherapeuticIntervention_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_TherapeuticIntervention_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Update" Text="Update Intervention" CssClass="Controls_Button" OnClick="Button_EditUpdate_TherapeuticIntervention_Click" />&nbsp;
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
                        <td style="width: 170px;">Date
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDate" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("PCM_TI_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemInterventionBy" runat="server" Text='<%# Bind("PCM_TI_InterventionBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemUnit" runat="server" Text='<%# Bind("PCM_TI_Unit") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoctor">Doctor
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoctor" runat="server" Text='<%# Bind("PCM_TI_Doctor") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDoctor" runat="server" Value='<%# Bind("PCM_TI_Doctor") %>' />
                        </td>
                      </tr>
                      <tr id="DoctorOther">
                        <td style="width: 170px;" id="FormDoctorOther">Doctor Other
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoctorOther" runat="server" Text='<%# Bind("PCM_TI_DoctorOther") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTime">Time (Min)
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemTime" runat="server" Text='<%# Bind("PCM_TI_Time") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Indication</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationNoIndication">No indication for drug</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationNoIndication" runat="server" Text='<%# (bool)(Eval("PCM_TI_Indication_NoIndication"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemIndicationNoIndication" runat="server" Value='<%# Bind("PCM_TI_Indication_NoIndication") %>' />
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationNoIndicationIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationNoIndicationCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemIndicationNoIndicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationNoIndicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationNoIndicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationNoIndicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_NoIndication_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationDuplication">Therapeutic duplication</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationDuplication" runat="server" Text='<%# (bool)(Eval("PCM_TI_Indication_Duplication"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemIndicationDuplication" runat="server" Value='<%# Bind("PCM_TI_Indication_Duplication") %>' />
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationDuplicationIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationDuplicationCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemIndicationDuplicationCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationDuplicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationDuplicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationDuplicationComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Duplication_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormtIndicationUntreated">Untreated medical condition</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationUntreated" runat="server" Text='<%# (bool)(Eval("PCM_TI_Indication_Untreated"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemIndicationUntreated" runat="server" Value='<%# Bind("PCM_TI_Indication_Untreated") %>' />
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationUntreatedIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationUntreatedCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemIndicationUntreatedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="IndicationUntreatedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormIndicationUntreatedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemIndicationUntreatedComment" runat="server" Text='<%# Bind("PCM_TI_Indication_Untreated_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Dose</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseDose">Dose</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseDose" runat="server" Text='<%# (bool)(Eval("PCM_TI_Dose_Dose"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDoseDose" runat="server" Value='<%# Bind("PCM_TI_Dose_Dose") %>' />
                        </td>
                      </tr>
                      <tr id="DoseDoseList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseList">Dose</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseDoseList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="DoseDoseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseDoseIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="DoseDoseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseDoseCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemDoseDoseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseDoseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseDoseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseDoseComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Dose_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoseInterval">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseInterval" runat="server" Text='<%# (bool)(Eval("PCM_TI_Dose_Interval"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDoseInterval" runat="server" Value='<%# Bind("PCM_TI_Dose_Interval") %>' />
                        </td>
                      </tr>
                      <tr id="DoseIntervalList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalList">Dosing Interval</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseIntervalList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="DoseIntervalIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseIntervalIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="DoseIntervalCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseIntervalCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemDoseIntervalCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="DoseIntervalComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormDoseIntervalComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemDoseIntervalComment" runat="server" Text='<%# Bind("PCM_TI_Dose_Interval_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Efficacy</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormEfficacyChange">Change drug to more effective alternative</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemEfficacyChange" runat="server" Text='<%# (bool)(Eval("PCM_TI_Efficacy_Change"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemEfficacyChange" runat="server" Value='<%# Bind("PCM_TI_Efficacy_Change") %>' />
                        </td>
                      </tr>
                      <tr id="EfficacyChangeIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemEfficacyChangeIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemEfficacyChangeCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemEfficacyChangeCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="EfficacyChangeComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormEfficacyChangeComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemEfficacyChangeComment" runat="server" Text='<%# Bind("PCM_TI_Efficacy_Change_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Safety</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyAllergic">Allergic reaction occurred</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyAllergic" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_Allergic"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyAllergic" runat="server" Value='<%# Bind("PCM_TI_Safety_Allergic") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyAllergicURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_ItemSafetyAllergicURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyAllergicIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyAllergicCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyAllergicCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyAllergicComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyAllergicComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyAllergicComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Allergic_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyUnwanted">Unwanted side effects occurred</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyUnwanted" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_Unwanted"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyUnwanted" runat="server" Value='<%# Bind("PCM_TI_Safety_Unwanted") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_ItemSafetyUnwantedURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyUnwantedIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyUnwantedCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyUnwantedCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyUnwantedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyUnwantedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyUnwantedComment" runat="server" Text='<%# Bind("PCM_TI_Safety_Unwanted_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDrug">Drug-Drug interaction</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDrug" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_DrugDrug"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyDrugDrug" runat="server" Value='<%# Bind("PCM_TI_Safety_DrugDrug") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDrugIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDrugCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyDrugDrugCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDrugComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDrug_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDiluent">Drug-diluent / Infusion solution incompatibility</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiluent" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_DrugDiluent"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyDrugDiluent" runat="server" Value='<%# Bind("PCM_TI_Safety_DrugDiluent") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiluentIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiluentCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyDrugDiluentCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiluentComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiluentComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiluentComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDiluent_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugLab">Drug-lab test contradiction</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugLab" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_DrugLab"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyDrugLab" runat="server" Value='<%# Bind("PCM_TI_Safety_DrugLab") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugLabIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugLabCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyDrugLabCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugLabComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugLabComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugLabComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugLab_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormSafetyDrugDisease">Drug-disease / medical condition interaction</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDisease" runat="server" Text='<%# (bool)(Eval("PCM_TI_Safety_DrugDisease"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemSafetyDrugDisease" runat="server" Value='<%# Bind("PCM_TI_Safety_DrugDisease") %>' />
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseIRList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseIRList">Intervention Response</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiseaseIRList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseCSList">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseCSList">Clinical Significance</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiseaseCSList" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                          <a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemSafetyDrugDiseaseCSListIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="SafetyDrugDiseaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormSafetyDrugDiseaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemSafetyDrugDiseaseComment" runat="server" Text='<%# Bind("PCM_TI_Safety_DrugDisease_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Medication Error</td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:HyperLink ID="HyperLink_ItemMedicationErrorURL_Alert" runat="server" Target="_blank">InfoQuest Alert Form</asp:HyperLink>&nbsp;&nbsp;/&nbsp;&nbsp;
                          <asp:HyperLink ID="HyperLink_ItemMedicationErrorURL_Incident" runat="server" Target="_blank">InfoQuest Incident Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMissed">Missed dose</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorMissed" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_Missed"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorMissed" runat="server" Value='<%# Bind("PCM_TI_MedicationError_Missed") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorMissedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMissedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorMissedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Missed_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrectDrug">Incorrect drug administered</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorIncorrectDrug" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_IncorrectDrug"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorIncorrectDrug" runat="server" Value='<%# Bind("PCM_TI_MedicationError_IncorrectDrug") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorIncorrectDrugComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectDrugComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorIncorrectDrugComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_IncorrectDrug_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorIncorrect">Incorrect dose administered</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorIncorrect" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_Incorrect"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorIncorrect" runat="server" Value='<%# Bind("PCM_TI_MedicationError_Incorrect") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorIncorrectComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorIncorrectComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorIncorrectComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Incorrect_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorPrescribed">Medication prescribed in presence of known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorPrescribed" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_Prescribed"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorPrescribed" runat="server" Value='<%# Bind("PCM_TI_MedicationError_Prescribed") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorPrescribedComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorPrescribedComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorPrescribedComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Prescribed_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorAdministered">Medication administered in presence of a known drug allergy</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorAdministered" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_Administered"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorAdministered" runat="server" Value='<%# Bind("PCM_TI_MedicationError_Administered") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorAdministeredComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorAdministeredComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorAdministeredComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Administered_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicationErrorMedication">Medication out of stock</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorMedication" runat="server" Text='<%# (bool)(Eval("PCM_TI_MedicationError_Medication"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicationErrorMedication" runat="server" Value='<%# Bind("PCM_TI_MedicationError_Medication") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationErrorMedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationErrorMedicationComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationErrorMedicationComment" runat="server" Text='<%# Bind("PCM_TI_MedicationError_Medication_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Cost</td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostGeneric">Generic substitution</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostGeneric" runat="server" Text='<%# (bool)(Eval("PCM_TI_Cost_Generic"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCostGeneric" runat="server" Value='<%# Bind("PCM_TI_Cost_Generic") %>' />
                        </td>
                      </tr>
                      <tr id="CostGenericComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostGenericComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostGenericComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Generic_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostSubstitution">Therapeutic substitution</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostSubstitution" runat="server" Text='<%# (bool)(Eval("PCM_TI_Cost_Substitution"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCostSubstitution" runat="server" Value='<%# Bind("PCM_TI_Cost_Substitution") %>' />
                        </td>
                      </tr>
                      <tr id="CostSubstitutionComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostSubstitutionComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostSubstitutionComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Substitution_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostDecrease">Intervention lead to a decrease in drug therapy costs</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostDecrease" runat="server" Text='<%# (bool)(Eval("PCM_TI_Cost_Decrease"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCostDecrease" runat="server" Value='<%# Bind("PCM_TI_Cost_Decrease") %>' />
                        </td>
                      </tr>
                      <tr id="CostDecreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostDecreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostDecreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Decrease_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCostIncrease">Intervention lead to an initial increase in drug therapy costs, but improved patient outcomes</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostIncrease" runat="server" Text='<%# (bool)(Eval("PCM_TI_Cost_Increase"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCostIncrease" runat="server" Value='<%# Bind("PCM_TI_Cost_Increase") %>' />
                        </td>
                      </tr>
                      <tr id="CostIncreaseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCostIncreaseComment">Comments / Details about the intervention</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCostIncreaseComment" runat="server" Text='<%# Bind("PCM_TI_Cost_Increase_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("PCM_TI_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("PCM_TI_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("PCM_TI_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("PCM_TI_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("PCM_TI_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Print" Text="Print Intervention" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel_TherapeuticIntervention" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemCancel_TherapeuticIntervention_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form" runat="server" OnInserted="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form_Inserted" OnUpdated="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivTherapeuticInterventionList" runat="server">
            &nbsp;
          </div>
          <table id="TableTherapeuticInterventionList" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_TherapeuticInterventionListHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords_TherapeuticIntervention" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_PreRender" OnDataBound="GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_DataBound" OnRowCreated="GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize_TherapeuticIntervention" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_TherapeuticIntervention_SelectedIndexChanged">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page_TherapeuticIntervention" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_TherapeuticIntervention_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of <%=GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_CaptureNew_TherapeuticIntervention" runat="server" Text="Capture New Therapeutic Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_TherapeuticIntervention_Click" />&nbsp;
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
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew_TherapeuticIntervention" runat="server" Text="Capture New Therapeutic Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_TherapeuticIntervention_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink_TherapeuticIntervention(Eval("PCM_Intervention_Id"), Eval("PCM_TI_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="PCM_Intervention_Date" HeaderText="Date" ReadOnly="True" SortExpression="PCM_Intervention_Date" />
                          <asp:BoundField DataField="PCM_TI_InterventionBy" HeaderText="By" ReadOnly="True" SortExpression="PCM_TI_InterventionBy" />
                          <asp:BoundField DataField="PCM_TI_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="PCM_TI_ReportNumber" />
                          <asp:BoundField DataField="PCM_TI_Unit" HeaderText="Unit" ReadOnly="True" SortExpression="PCM_TI_Unit" />
                          <asp:BoundField DataField="PCM_TI_Doctor" HeaderText="Doctor" ReadOnly="True" SortExpression="PCM_TI_Doctor" />
                          <asp:BoundField DataField="PCM_TI_Time" HeaderText="Time" ReadOnly="True" SortExpression="PCM_TI_Time" />
                          <asp:BoundField DataField="Summary" HeaderText="Summary" ReadOnly="True" HtmlEncode="false" SortExpression="Summary" />
                          <asp:BoundField DataField="PCM_TI_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="PCM_TI_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List" runat="server" OnSelected="SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivPharmacistTime" runat="server">
            &nbsp;
          </div>
          <table id="TablePharmacistTime" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_PharmacistTimeHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacyClinicalMetrics_PharmacistTime_Form" runat="server" DataKeyNames="PCM_PT_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form" OnItemInserting="FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemCommand" OnDataBound="FormView_PharmacyClinicalMetrics_PharmacistTime_Form_DataBound" OnItemUpdating="FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("PCM_PT_ReportNumber") %>' EnableViewState="false"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertInterventionBy" runat="server" Width="400px" Text='<%# Bind("PCM_PT_InterventionBy") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("PCM_PT_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">Review Patient Information&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertReviewPatientInformationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientFile">Reviewed whole patient file / flow chart including prescription, vitals, etc</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertPatientFile" runat="server" Checked='<%# Bind("PCM_PT_Patient_File") %>' /></td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientLabResults">Reviewed lab results</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertPatientLabResults" runat="server" Checked='<%# Bind("PCM_PT_Patient_LabResults") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertReviewedlabresultsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientPrescription">Reviewed prescription</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertPatientPrescription" runat="server" Checked='<%# Bind("PCM_PT_Patient_Prescription") %>' /></td>
                      </tr>
                      <tr id="PatientTotalTime">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientTotalTime">Total Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertPatientTotalTime" runat="server" Text='<%# Bind("PCM_PT_Patient_TotalTime") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientTotalTime" runat="server" TargetControlID="TextBox_InsertPatientTotalTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientAmount">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientAmount">Amount of Patients</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertPatientAmount" runat="server" Text='<%# Bind("PCM_PT_Patient_Amount") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPatientAmount" runat="server" TargetControlID="TextBox_InsertPatientAmount" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientComments">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientComments">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertPatientComments" runat="server" Text='<%# Bind("PCM_PT_Patient_Comments") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedication">Medication reconciliation</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedication" runat="server" Checked='<%# Bind("PCM_PT_Medication") %>' /></td>
                      </tr>
                      <tr id="MedicationTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationTime" runat="server" Text='<%# Bind("PCM_PT_Medication_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertMedicationTime" runat="server" TargetControlID="TextBox_InsertMedicationTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="MedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicationComment" runat="server" Text='<%# Bind("PCM_PT_Medication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormResearch">Conducted research</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertResearch" runat="server" Checked='<%# Bind("PCM_PT_Research") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertConductedresearchIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="ResearchTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertResearchTime" runat="server" Text='<%# Bind("PCM_PT_Research_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertResearchTime" runat="server" TargetControlID="TextBox_InsertResearchTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="ResearchComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertResearchComment" runat="server" Text='<%# Bind("PCM_PT_Research_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormRounds">Ward rounds with doctor</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertRounds" runat="server" Checked='<%# Bind("PCM_PT_Rounds") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertWardroundswithdoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="RoundsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertRoundsTime" runat="server" Text='<%# Bind("PCM_PT_Rounds_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertRoundsTime" runat="server" TargetControlID="TextBox_InsertRoundsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="RoundsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertRoundsComment" runat="server" Text='<%# Bind("PCM_PT_Rounds_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCounselling">Patient counselling / education</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCounselling" runat="server" Checked='<%# Bind("PCM_PT_Counselling") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertPatientcounsellingeducationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="CounsellingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCounsellingTime" runat="server" Text='<%# Bind("PCM_PT_Counselling_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCounsellingTime" runat="server" TargetControlID="TextBox_InsertCounsellingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CounsellingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCounsellingComment" runat="server" Text='<%# Bind("PCM_PT_Counselling_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTraining">Training of ward / pharmacy staff</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertTraining" runat="server" Checked='<%# Bind("PCM_PT_Training") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertTrainingofwardpharmacystaffIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="TrainingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertTrainingTime" runat="server" Text='<%# Bind("PCM_PT_Training_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertTrainingTime" runat="server" TargetControlID="TextBox_InsertTrainingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="TrainingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertTrainingComment" runat="server" Text='<%# Bind("PCM_PT_Training_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormReporting">Reporting of an adverse drug reaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertReporting" runat="server" Checked='<%# Bind("PCM_PT_Reporting") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertReportingofanadversedrugreactionIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="ReportingURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_InsertReportingURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="ReportingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertReportingTime" runat="server" Text='<%# Bind("PCM_PT_Reporting_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertReportingTime" runat="server" TargetControlID="TextBox_InsertReportingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="ReportingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertReportingComment" runat="server" Text='<%# Bind("PCM_PT_Reporting_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCalculations">Pharmacokinetic calculations</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertCalculations" runat="server" Checked='<%# Bind("PCM_PT_Calculations") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertPharmacokineticcalculationsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="CalculationsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCalculationsTime" runat="server" Text='<%# Bind("PCM_PT_Calculations_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertCalculationsTime" runat="server" TargetControlID="TextBox_InsertCalculationsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CalculationsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertCalculationsComment" runat="server" Text='<%# Bind("PCM_PT_Calculations_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceDoctor">Advice to doctor</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertAdviceDoctor" runat="server" Checked='<%# Bind("PCM_PT_AdviceDoctor") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertAdvicetodoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="AdviceDoctorTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertAdviceDoctorTime" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertAdviceDoctorTime" runat="server" TargetControlID="TextBox_InsertAdviceDoctorTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="AdviceDoctorComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertAdviceDoctorComment" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceNurse">Advice to nurse</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertAdviceNurse" runat="server" Checked='<%# Bind("PCM_PT_AdviceNurse") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_InsertAdvicetonurseIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="AdviceNurseTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertAdviceNurseTime" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertAdviceNurseTime" runat="server" TargetControlID="TextBox_InsertAdviceNurseTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="AdviceNurseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertAdviceNurseComment" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicalHistory">Obtaining medical history</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertMedicalHistory" runat="server" Checked='<%# Bind("PCM_PT_MedicalHistory") %>' /></td>
                      </tr>
                      <tr id="MedicalHistoryTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicalHistoryTime" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertMedicalHistoryTime" runat="server" TargetControlID="TextBox_InsertMedicalHistoryTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="MedicalHistoryComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertMedicalHistoryComment" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormStatistics">Compiling of statistics / reports</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_InsertStatistics" runat="server" Checked='<%# Bind("PCM_PT_Statistics") %>' /></td>
                      </tr>
                      <tr id="StatisticsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertStatisticsTime" runat="server" Text='<%# Bind("PCM_PT_Statistics_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertStatisticsTime" runat="server" TargetControlID="TextBox_InsertStatisticsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="StatisticsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertStatisticsComment" runat="server" Text='<%# Bind("PCM_PT_Statistics_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("PCM_PT_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("PCM_PT_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("PCM_PT_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("PCM_PT_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("PCM_PT_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCancel_PharmacistTime" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertCancel_PharmacistTime_Click" EnableViewState="false" />&nbsp;
                          <asp:Button ID="Button_InsertAdd_PharmacistTime" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Intervention" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
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
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("PCM_PT_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditInterventionBy" runat="server" Width="400px" Text='<%# Bind("PCM_PT_InterventionBy") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("PCM_PT_Unit") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">Review Patient Information&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditReviewPatientInformationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientFile">Reviewed whole patient file / flow chart including prescription, vitals, etc</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditPatientFile" runat="server" Checked='<%# Bind("PCM_PT_Patient_File") %>' /></td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientLabResults">Reviewed lab results</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditPatientLabResults" runat="server" Checked='<%# Bind("PCM_PT_Patient_LabResults") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditReviewedlabresultsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientPrescription">Reviewed prescription</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditPatientPrescription" runat="server" Checked='<%# Bind("PCM_PT_Patient_Prescription") %>' /></td>
                      </tr>
                      <tr id="PatientTotalTime">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientTotalTime">Total Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditPatientTotalTime" runat="server" Text='<%# Bind("PCM_PT_Patient_TotalTime") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientTotalTime" runat="server" TargetControlID="TextBox_EditPatientTotalTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientAmount">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientAmount">Amount of Patients</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditPatientAmount" runat="server" Text='<%# Bind("PCM_PT_Patient_Amount") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPatientAmount" runat="server" TargetControlID="TextBox_EditPatientAmount" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="PatientComments">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientComments">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditPatientComments" runat="server" Text='<%# Bind("PCM_PT_Patient_Comments") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedication">Medication reconciliation</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedication" runat="server" Checked='<%# Bind("PCM_PT_Medication") %>' /></td>
                      </tr>
                      <tr id="MedicationTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationTime" runat="server" Text='<%# Bind("PCM_PT_Medication_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditMedicationTime" runat="server" TargetControlID="TextBox_EditMedicationTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="MedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicationComment" runat="server" Text='<%# Bind("PCM_PT_Medication_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormResearch">Conducted research</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditResearch" runat="server" Checked='<%# Bind("PCM_PT_Research") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditConductedresearchIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="ResearchTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditResearchTime" runat="server" Text='<%# Bind("PCM_PT_Research_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditResearchTime" runat="server" TargetControlID="TextBox_EditResearchTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="ResearchComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditResearchComment" runat="server" Text='<%# Bind("PCM_PT_Research_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormRounds">Ward rounds with doctor</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditRounds" runat="server" Checked='<%# Bind("PCM_PT_Rounds") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditWardroundswithdoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="RoundsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditRoundsTime" runat="server" Text='<%# Bind("PCM_PT_Rounds_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditRoundsTime" runat="server" TargetControlID="TextBox_EditRoundsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="RoundsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditRoundsComment" runat="server" Text='<%# Bind("PCM_PT_Rounds_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCounselling">Patient counselling / education</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCounselling" runat="server" Checked='<%# Bind("PCM_PT_Counselling") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditPatientcounsellingeducationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="CounsellingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCounsellingTime" runat="server" Text='<%# Bind("PCM_PT_Counselling_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCounsellingTime" runat="server" TargetControlID="TextBox_EditCounsellingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CounsellingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCounsellingComment" runat="server" Text='<%# Bind("PCM_PT_Counselling_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTraining">Training of ward / pharmacy staff</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditTraining" runat="server" Checked='<%# Bind("PCM_PT_Training") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditTrainingofwardpharmacystaffIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="TrainingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditTrainingTime" runat="server" Text='<%# Bind("PCM_PT_Training_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditTrainingTime" runat="server" TargetControlID="TextBox_EditTrainingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="TrainingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditTrainingComment" runat="server" Text='<%# Bind("PCM_PT_Training_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormReporting">Reporting of an adverse drug reaction</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditReporting" runat="server" Checked='<%# Bind("PCM_PT_Reporting") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditReportingofanadversedrugreactionIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="ReportingURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_EditReportingURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="ReportingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditReportingTime" runat="server" Text='<%# Bind("PCM_PT_Reporting_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditReportingTime" runat="server" TargetControlID="TextBox_EditReportingTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="ReportingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditReportingComment" runat="server" Text='<%# Bind("PCM_PT_Reporting_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCalculations">Pharmacokinetic calculations</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditCalculations" runat="server" Checked='<%# Bind("PCM_PT_Calculations") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditPharmacokineticcalculationsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="CalculationsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCalculationsTime" runat="server" Text='<%# Bind("PCM_PT_Calculations_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCalculationsTime" runat="server" TargetControlID="TextBox_EditCalculationsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="CalculationsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditCalculationsComment" runat="server" Text='<%# Bind("PCM_PT_Calculations_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceDoctor">Advice to doctor</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditAdviceDoctor" runat="server" Checked='<%# Bind("PCM_PT_AdviceDoctor") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditAdvicetodoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="AdviceDoctorTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditAdviceDoctorTime" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditAdviceDoctorTime" runat="server" TargetControlID="TextBox_EditAdviceDoctorTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="AdviceDoctorComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditAdviceDoctorComment" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceNurse">Advice to nurse</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditAdviceNurse" runat="server" Checked='<%# Bind("PCM_PT_AdviceNurse") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_EditAdvicetonurseIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr id="AdviceNurseTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditAdviceNurseTime" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditAdviceNurseTime" runat="server" TargetControlID="TextBox_EditAdviceNurseTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="AdviceNurseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditAdviceNurseComment" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicalHistory">Obtaining medical history</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditMedicalHistory" runat="server" Checked='<%# Bind("PCM_PT_MedicalHistory") %>' /></td>
                      </tr>
                      <tr id="MedicalHistoryTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicalHistoryTime" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditMedicalHistoryTime" runat="server" TargetControlID="TextBox_EditMedicalHistoryTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="MedicalHistoryComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditMedicalHistoryComment" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormStatistics">Compiling of statistics / reports</td>
                        <td style="width: 830px;">
                          <asp:CheckBox ID="CheckBox_EditStatistics" runat="server" Checked='<%# Bind("PCM_PT_Statistics") %>' /></td>
                      </tr>
                      <tr id="StatisticsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditStatisticsTime" runat="server" Text='<%# Bind("PCM_PT_Statistics_Time") %>' Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditStatisticsTime" runat="server" TargetControlID="TextBox_EditStatisticsTime" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="StatisticsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditStatisticsComment" runat="server" Text='<%# Bind("PCM_PT_Statistics_Comment") %>' TextMode="MultiLine" Rows="3" Width="750px" CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("PCM_PT_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("PCM_PT_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("PCM_PT_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("PCM_PT_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("PCM_PT_IsActive") %>' />
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint_PharmacistTime" runat="server" CausesValidation="False" CommandName="Update" Text="Print Intervention" CssClass="Controls_Button" OnClick="Button_EditPrint_PharmacistTime_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail_PharmacistTime" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_PharmacistTime_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel_PharmacistTime" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_PharmacistTime_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate_PharmacistTime" runat="server" CausesValidation="False" CommandName="Update" Text="Update Intervention" CssClass="Controls_Button" OnClick="Button_EditUpdate_PharmacistTime_Click" />&nbsp;
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
                        <td style="width: 170px;">Report Number
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("PCM_PT_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormInterventionBy">Assessment By
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemInterventionBy" runat="server" Text='<%# Bind("PCM_PT_InterventionBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemUnit" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">Review Patient Information&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemReviewPatientInformationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientFile">Reviewed whole patient file / flow chart including prescription, vitals, etc</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientFile" runat="server" Text='<%# (bool)(Eval("PCM_PT_Patient_File"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPatientFile" runat="server" Value='<%# Bind("PCM_PT_Patient_File") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientLabResults">Reviewed lab results</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientLabResults" runat="server" Text='<%# (bool)(Eval("PCM_PT_Patient_LabResults"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPatientLabResults" runat="server" Value='<%# Bind("PCM_PT_Patient_LabResults") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemReviewedlabresultsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px; padding-left: 10px;" id="FormPatientPrescription">Reviewed prescription</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientPrescription" runat="server" Text='<%# (bool)(Eval("PCM_PT_Patient_Prescription"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPatientPrescription" runat="server" Value='<%# Bind("PCM_PT_Patient_Prescription") %>' />
                        </td>
                      </tr>
                      <tr id="PatientTotalTime">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientTotalTime">Total Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientTotalTime" runat="server" Text='<%# Bind("PCM_PT_Patient_TotalTime") %>'></asp:Label>
                      </tr>
                      <tr id="PatientAmount">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientAmount">Amount of Patients</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientAmount" runat="server" Text='<%# Bind("PCM_PT_Patient_Amount") %>'></asp:Label>
                      </tr>
                      <tr id="PatientComments">
                        <td style="width: 170px; padding-left: 20px;" id="FormPatientComments">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemPatientComments" runat="server" Text='<%# Bind("PCM_PT_Patient_Comments") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedication">Medication reconciliation</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedication" runat="server" Text='<%# (bool)(Eval("PCM_PT_Medication"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedication" runat="server" Value='<%# Bind("PCM_PT_Medication") %>' />
                        </td>
                      </tr>
                      <tr id="MedicationTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationTime" runat="server" Text='<%# Bind("PCM_PT_Medication_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="MedicationComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicationComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicationComment" runat="server" Text='<%# Bind("PCM_PT_Medication_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormResearch">Conducted research</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemResearch" runat="server" Text='<%# (bool)(Eval("PCM_PT_Research"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemResearch" runat="server" Value='<%# Bind("PCM_PT_Research") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemConductedresearchIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="ResearchTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemResearchTime" runat="server" Text='<%# Bind("PCM_PT_Research_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="ResearchComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormResearchComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemResearchComment" runat="server" Text='<%# Bind("PCM_PT_Research_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormRounds">Ward rounds with doctor</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemRounds" runat="server" Text='<%# (bool)(Eval("PCM_PT_Rounds"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemRounds" runat="server" Value='<%# Bind("PCM_PT_Rounds") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemWardroundswithdoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="RoundsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemRoundsTime" runat="server" Text='<%# Bind("PCM_PT_Rounds_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="RoundsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormRoundsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemRoundsComment" runat="server" Text='<%# Bind("PCM_PT_Rounds_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCounselling">Patient counselling / education</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCounselling" runat="server" Text='<%# (bool)(Eval("PCM_PT_Counselling"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCounselling" runat="server" Value='<%# Bind("PCM_PT_Counselling") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemPatientcounsellingeducationIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="CounsellingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCounsellingTime" runat="server" Text='<%# Bind("PCM_PT_Counselling_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="CounsellingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCounsellingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCounsellingComment" runat="server" Text='<%# Bind("PCM_PT_Counselling_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormTraining">Training of ward / pharmacy staff</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemTraining" runat="server" Text='<%# (bool)(Eval("PCM_PT_Training"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemTraining" runat="server" Value='<%# Bind("PCM_PT_Training") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemTrainingofwardpharmacystaffIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="TrainingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemTrainingTime" runat="server" Text='<%# Bind("PCM_PT_Training_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="TrainingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormTrainingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemTrainingComment" runat="server" Text='<%# Bind("PCM_PT_Training_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormReporting">Reporting of an adverse drug reaction</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemReporting" runat="server" Text='<%# (bool)(Eval("PCM_PT_Reporting"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemReporting" runat="server" Value='<%# Bind("PCM_PT_Reporting") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemReportingofanadversedrugreactionIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="ReportingURL">
                        <td colspan="2" style="padding-left: 10px;">
                          <asp:HyperLink ID="HyperLink_ItemReportingURL" runat="server" Target="_blank">Adverse Drug Reaction Reporting Form</asp:HyperLink>
                        </td>
                      </tr>
                      <tr id="ReportingTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemReportingTime" runat="server" Text='<%# Bind("PCM_PT_Reporting_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="ReportingComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormReportingComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemReportingComment" runat="server" Text='<%# Bind("PCM_PT_Reporting_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCalculations">Pharmacokinetic calculations</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCalculations" runat="server" Text='<%# (bool)(Eval("PCM_PT_Calculations"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemCalculations" runat="server" Value='<%# Bind("PCM_PT_Calculations") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemPharmacokineticcalculationsIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="CalculationsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCalculationsTime" runat="server" Text='<%# Bind("PCM_PT_Calculations_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="CalculationsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormCalculationsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemCalculationsComment" runat="server" Text='<%# Bind("PCM_PT_Calculations_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceDoctor">Advice to doctor</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceDoctor" runat="server" Text='<%# (bool)(Eval("PCM_PT_AdviceDoctor"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemAdviceDoctor" runat="server" Value='<%# Bind("PCM_PT_AdviceDoctor") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemAdvicetodoctorIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="AdviceDoctorTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceDoctorTime" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="AdviceDoctorComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceDoctorComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceDoctorComment" runat="server" Text='<%# Bind("PCM_PT_AdviceDoctor_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormAdviceNurse">Advice to nurse</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceNurse" runat="server" Text='<%# (bool)(Eval("PCM_PT_AdviceNurse"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemAdviceNurse" runat="server" Value='<%# Bind("PCM_PT_AdviceNurse") %>' />&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_ItemAdvicetonurseIcon" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr id="AdviceNurseTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceNurseTime" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="AdviceNurseComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormAdviceNurseComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemAdviceNurseComment" runat="server" Text='<%# Bind("PCM_PT_AdviceNurse_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormMedicalHistory">Obtaining medical history</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicalHistory" runat="server" Text='<%# (bool)(Eval("PCM_PT_MedicalHistory"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemMedicalHistory" runat="server" Value='<%# Bind("PCM_PT_MedicalHistory") %>' />
                        </td>
                      </tr>
                      <tr id="MedicalHistoryTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicalHistoryTime" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="MedicalHistoryComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormMedicalHistoryComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemMedicalHistoryComment" runat="server" Text='<%# Bind("PCM_PT_MedicalHistory_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormStatistics">Compiling of statistics / reports</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemStatistics" runat="server" Text='<%# (bool)(Eval("PCM_PT_Statistics"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemStatistics" runat="server" Value='<%# Bind("PCM_PT_Statistics") %>' />
                        </td>
                      </tr>
                      <tr id="StatisticsTime">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsTime">Time (Min)</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemStatisticsTime" runat="server" Text='<%# Bind("PCM_PT_Statistics_Time") %>'></asp:Label></td>
                      </tr>
                      <tr id="StatisticsComment">
                        <td style="width: 170px; padding-left: 10px;" id="FormStatisticsComment">Comments</td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_ItemStatisticsComment" runat="server" Text='<%# Bind("PCM_PT_Statistics_Comment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("PCM_PT_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("PCM_PT_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("PCM_PT_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("PCM_PT_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("PCM_PT_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint_PharmacistTime" runat="server" CausesValidation="False" CommandName="Print" Text="Print Intervention" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail_PharmacistTime" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel_PharmacistTime" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemCancel_PharmacistTime_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form" runat="server" OnInserted="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form_Inserted" OnUpdated="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivPharmacistTimeList" runat="server">
            &nbsp;
          </div>
          <table id="TablePharmacistTimeList" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_PharmacistTimeListHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords_PharmacistTime" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_PharmacyClinicalMetrics_PharmacistTime_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_PharmacyClinicalMetrics_PharmacistTime_List_PreRender" OnDataBound="GridView_PharmacyClinicalMetrics_PharmacistTime_List_DataBound" OnRowCreated="GridView_PharmacyClinicalMetrics_PharmacistTime_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize_PharmacistTime" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_PharmacistTime_SelectedIndexChanged">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page_PharmacistTime" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_PharmacistTime_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of <%=GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_CaptureNew_PharmacistTime" runat="server" Text="Capture New Pharmacist Time" CssClass="Controls_Button" OnClick="Button_CaptureNew_PharmacistTime_Click" />&nbsp;
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
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew_PharmacistTime" runat="server" Text="Capture New Pharmacist Time" CssClass="Controls_Button" OnClick="Button_CaptureNew_PharmacistTime_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink_PharmacistTime(Eval("PCM_Intervention_Id"), Eval("PCM_PT_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="PCM_Intervention_Date" HeaderText="Date" ReadOnly="True" SortExpression="PCM_Intervention_Date" />
                          <asp:BoundField DataField="PCM_PT_InterventionBy" HeaderText="By" ReadOnly="True" SortExpression="PCM_PT_InterventionBy" />
                          <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                          <asp:BoundField DataField="PCM_PT_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="PCM_PT_ReportNumber" />
                          <asp:BoundField DataField="Summary" HeaderText="Summary" ReadOnly="True" HtmlEncode="false" SortExpression="Summary" />
                          <asp:BoundField DataField="PCM_PT_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="PCM_PT_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List" runat="server" OnSelected="SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
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
