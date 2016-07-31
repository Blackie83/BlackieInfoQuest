<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_MedicationBundleCompliance.aspx.cs" Inherits="InfoQuestForm.Form_MedicationBundleCompliance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Medication Bundle Compliance</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MedicationBundleCompliance.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_MedicationBundleCompliance" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MedicationBundleCompliance" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MedicationBundleCompliance" AssociatedUpdatePanelID="UpdatePanel_MedicationBundleCompliance">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MedicationBundleCompliance" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">
                <asp:Label ID="Label_Title" runat="server" Text="" EnableViewState="false"></asp:Label>
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SearchHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
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
                      <asp:Label ID="Label_InvalidSearchMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchFacility" style="width: 150px">Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_MBC_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_MBC_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox" EnableViewState="false"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PatientVisitNumber" runat="server" TargetControlID="TextBox_PatientVisitNumber" FilterType="Numbers" EnableViewState="false">
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" CausesValidation="False" EnableViewState="false" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" CausesValidation="False" EnableViewState="false" />&nbsp;
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
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" EnableViewState="false" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableVisitInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_VisitInfoHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
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
                      <asp:Label ID="Label_VIFacility" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Age:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIAge" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Visit Number:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIVisitNumber" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Admission:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateAdmission" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Surname, Name:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIName" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Discharge:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateDischarge" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="width: 100px; text-align: left;">&nbsp;</td>
                    <td style="text-align: center;">
                      &nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableCurrentBundle" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentBundleHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_MedicationBundleCompliance_Form" runat="server" DataKeyNames="MBC_Bundles_Id" CssClass="FormView" DataSourceID="SqlDataSource_MedicationBundleCompliance_Form" OnItemInserting="FormView_MedicationBundleCompliance_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_MedicationBundleCompliance_Form_ItemCommand" OnDataBound="FormView_MedicationBundleCompliance_Form_DataBound" OnItemUpdating="FormView_MedicationBundleCompliance_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="3" style="width: 730px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("MBC_Bundles_ReportNumber") %>' EnableViewState="false"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_MedicationBundleCompliance_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Evaluation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("MBC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox" EnableViewState="false"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" EnableViewState="false" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate" EnableViewState="false">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark" EnableViewState="false">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormAssessed">Bundles Assessed
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertAssessedLMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_LMA") %>' Text="Legal Medication Administration" EnableViewState="false" /><br />
                          <asp:CheckBox ID="CheckBox_InsertAssessedCMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_CMA") %>' Text="Complete Medication Administration" EnableViewState="false" /><br />
                          <asp:CheckBox ID="CheckBox_InsertAssessedRMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_RMA") %>' Text="Recording of Medication Administered" EnableViewState="false" /><br />
                          <asp:CheckBox ID="CheckBox_InsertAssessedESM" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_ESM") %>' Text="Effect / Side Effects of Medicaton monitored and recorded" EnableViewState="false" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormLMA">Legal Medication Administration</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_SelectAll") %>' EnableViewState="false" /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA1" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_1") %>' EnableViewState="false" /></td>
                        <td>1.1 Prescription complies with legal requirements.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_1_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA2" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_2") %>' EnableViewState="false" /></td>
                        <td>1.2 Telephonic prescription: RN and witness signed, time of order.  Dr signed within 24 hours.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_2_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA3" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_3") %>' EnableViewState="false" /></td>
                        <td>1.3 Medication from home:  All medications recorded. RN and witness signed.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_3_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA4" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_4") %>' EnableViewState="false" /></td>
                        <td>1.4 Doctor reviewed home medication and prescribed / signed for medication to be continued.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLMA4NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_4_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>Legal Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertLMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_LMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormCMA">Complete Medication Administration</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_SelectAll") %>' EnableViewState="false" /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA1" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_1") %>' EnableViewState="false" /></td>
                        <td>2.1 Medication calculation correct and double check was done.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_1_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA2" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_2") %>' EnableViewState="false" /></td>
                        <td>2.2 Correct medication according to prescription.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_2_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA3" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_3") %>' EnableViewState="false" /></td>
                        <td>2.3 Administered to the correct patient.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_3_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA4" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_4") %>' EnableViewState="false" /></td>
                        <td>2.4 Pre-med as well as other medication administered according to the right times and frequency.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA4NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_4_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA5" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_5") %>' EnableViewState="false" /></td>
                        <td>2.5 Medication was given via the correct route.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCMA5NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_5_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>Complete Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertCMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_CMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="4" style="vertical-align: middle; text-align: center;" id="FormRMA">Recording of medication administered</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_SelectAll") %>' EnableViewState="false" /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA1" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_1") %>' EnableViewState="false" /></td>
                        <td>3.1 Recorded on the medication chart, nursing notes signed and dated.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_1_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA2" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_2") %>' EnableViewState="false" /></td>
                        <td>3.2 Sample signature up to date and in place.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_2_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA3" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_3") %>' EnableViewState="false" /></td>
                        <td>3.3 Schedule 5 and 6 drugs only : prescription written in words and numbers.  Correct and complete checks and entry made in drug book.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertRMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_3_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>Recording of medication administered Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertRMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_RMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormESM">Effect / Side Effects of Medicaton monitored and recorded</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESMSelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_SelectAll") %>' EnableViewState="false" /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM1" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_1") %>' EnableViewState="false" /></td>
                        <td>4.1 Patient informed of possible side effects of medication given and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM1NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_1_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM2" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_2") %>' EnableViewState="false" /></td>
                        <td>4.2 Analgesia : Pre-administered and 30 minutes post administration pain score recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM2NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_2_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM3" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_3") %>' EnableViewState="false" /></td>
                        <td>4.3 Nebulization : Assessment of patient before and after administration and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM3NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_3_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM4" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_4") %>' EnableViewState="false" /></td>
                        <td>4.4 Record patient response to medication given.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM4NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_4_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM5" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_5") %>' EnableViewState="false" /></td>
                        <td>4.5 Side effects after administration of medication actioned and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertESM5NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_5_NA") %>' EnableViewState="false" /></td>
                      </tr>
                      <tr>
                        <td>Effect / Side Effects of Medicaton monitored and recorded Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertESMCal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_ESM_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("MBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("MBC_Bundles_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("MBC_Bundles_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Bundle" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
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
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="3" style="width: 730px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("MBC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_MedicationBundleCompliance_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Evaluation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("MBC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:HiddenField ID="HiddenField_EditDate" runat="server" Value='<%# Eval("MBC_Bundles_Date","{0:yyyy/MM/dd}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormAssessed">Bundles Assessed
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditAssessedLMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_LMA") %>' Text="Legal Medication Administration" /><br />
                          <asp:CheckBox ID="CheckBox_EditAssessedCMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_CMA") %>' Text="Complete Medication Administration" /><br />
                          <asp:CheckBox ID="CheckBox_EditAssessedRMA" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_RMA") %>' Text="Recording of Medication Administered" /><br />
                          <asp:CheckBox ID="CheckBox_EditAssessedESM" runat="server" Checked='<%# Bind("MBC_Bundles_Assessed_ESM") %>' Text="Effect / Side Effects of Medicaton monitored and recorded" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormLMA">Legal Medication Administration</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA1" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_1") %>' /></td>
                        <td>1.1 Prescription complies with legal requirements.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA2" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_2") %>' /></td>
                        <td>1.2 Telephonic prescription: RN and witness signed, time of order.  Dr signed within 24 hours.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA3" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_3") %>' /></td>
                        <td>1.3 Medication from home:  All medications recorded. RN and witness signed.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA4" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_4") %>' /></td>
                        <td>1.4 Doctor reviewed home medication and prescribed / signed for medication to be continued.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLMA4NA" runat="server" Checked='<%# Bind("MBC_Bundles_LMA_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>Legal Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditLMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_LMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormCMA">Complete Medication Administration</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA1" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_1") %>' /></td>
                        <td>2.1 Medication calculation correct and double check was done.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA2" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_2") %>' /></td>
                        <td>2.2 Correct medication according to prescription.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA3" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_3") %>' /></td>
                        <td>2.3 Administered to the correct patient.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA4" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_4") %>' /></td>
                        <td>2.4 Pre-med as well as other medication administered according to the right times and frequency.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA4NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA5" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_5") %>' /></td>
                        <td>2.5 Medication was given via the correct route.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCMA5NA" runat="server" Checked='<%# Bind("MBC_Bundles_CMA_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>Complete Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditCMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_CMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="4" style="vertical-align: middle; text-align: center;" id="FormRMA">Recording of medication administered</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMASelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA1" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_1") %>' /></td>
                        <td>3.1 Recorded on the medication chart, nursing notes signed and dated.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA1NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA2" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_2") %>' /></td>
                        <td>3.2 Sample signature up to date and in place.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA2NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA3" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_3") %>' /></td>
                        <td>3.3 Schedule 5 and 6 drugs only : prescription written in words and numbers.  Correct and complete checks and entry made in drug book.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditRMA3NA" runat="server" Checked='<%# Bind("MBC_Bundles_RMA_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>Recording of medication administered Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditRMACal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_RMA_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormESM">Effect / Side Effects of Medicaton monitored and recorded</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESMSelectAll" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM1" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_1") %>' /></td>
                        <td>4.1 Patient informed of possible side effects of medication given and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM1NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM2" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_2") %>' /></td>
                        <td>4.2 Analgesia : Pre-administered and 30 minutes post administration pain score recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM2NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM3" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_3") %>' /></td>
                        <td>4.3 Nebulization : Assessment of patient before and after administration and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM3NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM4" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_4") %>' /></td>
                        <td>4.4 Record patient response to medication given.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM4NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM5" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_5") %>' /></td>
                        <td>4.5 Side effects after administration of medication actioned and recorded.</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditESM5NA" runat="server" Checked='<%# Bind("MBC_Bundles_ESM_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>Effect / Side Effects of Medicaton monitored and recorded Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditESMCal" Width="100px" runat="server" Text='<%# Bind("MBC_Bundles_ESM_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("MBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("MBC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("MBC_Bundles_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Bundle" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Bundle" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="3" style="width: 730px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("MBC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Unit
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Evaluation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("MBC_Bundles_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormAssessed">Bundles Assessed
                        </td>
                        <td colspan="3">
                          Legal Medication Administration: <strong><asp:Label ID="Label_ItemAssessedLMA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_Assessed_LMA"))?"Yes":"No" %>' /></strong><br />
                          Complete Medication Administration: <strong><asp:Label ID="Label_ItemAssessedCMA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_Assessed_CMA"))?"Yes":"No" %>' /></strong><br />
                          Recording of Medication Administered: <strong><asp:Label ID="Label_ItemAssessedRMA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_Assessed_RMA"))?"Yes":"No" %>' /></strong><br />
                          Effect / Side Effects of Medicaton monitored and recorded: <strong><asp:Label ID="Label_ItemAssessedESM" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_Assessed_ESM"))?"Yes":"No" %>' /></strong>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormLMA">Legal Medication Administration</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMASelectAll" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA1" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.1 Prescription complies with legal requirements.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA1NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA2" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.2 Telephonic prescription: RN and witness signed, time of order.  Dr signed within 24 hours.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA2NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA3" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.3 Medication from home:  All medications recorded. RN and witness signed.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA3NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA4" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.4 Doctor reviewed home medication and prescribed / signed for medication to be continued.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemLMA4NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_LMA_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>Legal Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemLMACal" runat="server" Text='<%# Bind("MBC_Bundles_LMA_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormCMA">Complete Medication Administration</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMASelectAll" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA1" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.1 Medication calculation correct and double check was done.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA1NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA2" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.2 Correct medication according to prescription.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA2NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA3" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.3 Administered to the correct patient.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA3NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA4" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.4 Pre-med as well as other medication administered according to the right times and frequency.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA4NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA5" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_5"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.5 Medication was given via the correct route.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCMA5NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_CMA_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>Complete Medication Administration Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemCMACal" runat="server" Text='<%# Bind("MBC_Bundles_CMA_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="4" style="vertical-align: middle; text-align: center;" id="FormRMA">Recording of medication administered</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMASelectAll" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA1" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.1 Recorded on the medication chart, nursing notes signed and dated.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA1NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA2" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.2 Sample signature up to date and in place.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA2NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA3" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.3 Schedule 5 and 6 drugs only : prescription written in words and numbers.  Correct and complete checks and entry made in drug book.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemRMA3NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_RMA_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>Recording of medication administered Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemRMACal" runat="server" Text='<%# Bind("MBC_Bundles_RMA_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormESM">Effect / Side Effects of Medicaton monitored and recorded</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESMSelectAll" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM1" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.1 Patient informed of possible side effects of medication given and recorded.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM1NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM2" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.2 Analgesia : Pre-administered and 30 minutes post administration pain score recorded.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM2NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM3" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.3 Nebulization : Assessment of patient before and after administration and recorded.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM3NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM4" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.4 Record patient response to medication given.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM4NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM5" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_5"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.5 Side effects after administration of medication actioned and recorded.</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemESM5NA" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_ESM_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>Effect / Side Effects of Medicaton monitored and recorded Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemESMCal" runat="server" Text='<%# Bind("MBC_Bundles_ESM_Cal") %>'></asp:Label></strong>&nbsp;
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("MBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("MBC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("MBC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("MBC_Bundles_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Bundle" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_MedicationBundleCompliance_InsertUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MedicationBundleCompliance_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MedicationBundleCompliance_Form" runat="server" OnInserted="SqlDataSource_MedicationBundleCompliance_Form_Inserted" OnUpdated="SqlDataSource_MedicationBundleCompliance_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableBundle" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BundleHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords" runat="server" Text="" EnableViewState="false"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_MedicationBundleCompliance_Bundles" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_MedicationBundleCompliance_Bundles" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_MedicationBundleCompliance_Bundles_PreRender" OnDataBound="GridView_MedicationBundleCompliance_Bundles_DataBound" OnRowCreated="GridView_MedicationBundleCompliance_Bundles_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of <%=GridView_MedicationBundleCompliance_Bundles.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Bundle" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Bundle" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("MBC_Bundles_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="MBC_Bundles_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="MBC_Bundles_ReportNumber" />
                          <asp:BoundField DataField="MBC_Bundles_Date" HeaderText="Evaluation Date" ReadOnly="True" SortExpression="MBC_Bundles_Date" />
                          <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                          <asp:BoundField DataField="MBC_Bundles_LMA_Cal" HeaderText="LMA Cal" ReadOnly="True" SortExpression="MBC_Bundles_LMA_Cal" />
                          <asp:BoundField DataField="MBC_Bundles_CMA_Cal" HeaderText="CMA Cal" ReadOnly="True" SortExpression="MBC_Bundles_CMA_Cal" />
                          <asp:BoundField DataField="MBC_Bundles_RMA_Cal" HeaderText="RMA Cal" ReadOnly="True" SortExpression="MBC_Bundles_RMA_Cal" />
                          <asp:BoundField DataField="MBC_Bundles_ESM_Cal" HeaderText="ESM Cal" ReadOnly="True" SortExpression="MBC_Bundles_ESM_Cal" />
                          <asp:BoundField DataField="MBC_Bundles_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="MBC_Bundles_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_MedicationBundleCompliance_Bundles" runat="server" OnSelected="SqlDataSource_MedicationBundleCompliance_Bundles_Selected"></asp:SqlDataSource>
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