<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_AntimicrobialStewardshipIntervention.aspx.cs" Inherits="InfoQuestForm.Form_AntimicrobialStewardshipIntervention" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Antimicrobial Stewardship</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_AntimicrobialStewardshipIntervention.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body id="Body_AntimicrobialStewardshipIntervention" runat="server" >
  <form id="form_AntimicrobialStewardshipIntervention" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_AntimicrobialStewardshipIntervention" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_AntimicrobialStewardshipIntervention" AssociatedUpdatePanelID="UpdatePanel_AntimicrobialStewardshipIntervention">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_AntimicrobialStewardshipIntervention" runat="server">
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
          <table class="Table">
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
                    <td id="SearchFacility" style="width: 150px">Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ASI_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ASI_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number
                    </td>
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
          <div>
            &nbsp;
          </div>
          <table id="TableVisitInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_VisitInfoHeading" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_VIFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Age:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIAge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Visit Number:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Admission:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Surname, Name:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Discharge:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
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
                    <td style="text-align: center;">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableCurrentIntervention" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentInterventionHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_AntimicrobialStewardshipIntervention_Form" runat="server" DataKeyNames="ASI_Intervention_Id" CssClass="FormView" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_Form" OnItemInserting="FormView_AntimicrobialStewardshipIntervention_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_AntimicrobialStewardshipIntervention_Form_ItemCommand" OnDataBound="FormView_AntimicrobialStewardshipIntervention_Form_DataBound" OnItemUpdating="FormView_AntimicrobialStewardshipIntervention_Form_ItemUpdating">
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
                        <td style="width: 730px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("ASI_Intervention_ReportNumber") %>' EnableViewState="false"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("ASI_Intervention_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox" EnableViewState="false"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" EnableViewState="false" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate" EnableViewState="false">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark" EnableViewState="false">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormByList">Assessment By
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertByList" runat="server" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("ASI_Intervention_By_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select By</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Assessment Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" AppendDataBoundItems="true" DataTextField="Ward" DataValueField="Ward" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                            <asp:ListItem Value="Pharmacy">Pharmacy</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoctor">Attending / Prescribing Doctor
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertDoctor" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="Nursing">
                        <td colspan="2" class="FormView_TableBodyHeader">Nursing
                        </td>
                      </tr>
                      <tr id="NursingSelectAllYes">
                        <td>Select all aspects Yes</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertNursingSelectAllYes" runat="server" />
                        </td>
                      </tr>
                      <tr id="Nursing1">
                        <td style="width: 170px;" id="FormNursing1">1) Surgical prophylaxis was stopped when the patient left theatre<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">The antibiotic selected for surgical prophylaxis must be in accordance with local evidence-based guidelines for the specific surgical procedure and pathogen resistance patterns.  A prophylactic dose should be administered 30-60 minutes before surgical incision. Refer to the literature on preventing surgical site infections & the infection prevention bundles for more information regarding this aspect of administration time. In most cases, surgical prophylaxis should be stopped once the patient is out of theatre. Further antibiotic administration usually then constitutes treatment.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_1")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no surgery performed or no prophylaxis given)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing1B">
                        <td style="width: 170px;" id="FormNursing1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_1_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing2">
                        <td style="width: 170px;" id="FormNursing2">2) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Most infections can be treated with short courses of antimicrobials (3, 5 or 7 days therapy) and treatment should not be unnecessarily prolonged.  Duration of therapy is usually determined by clinical factors such as the site of infection, severity of illness and response to treatment. As a general guide, antibiotics can be discontinued within 48-72 hours of the temperature and other clinical markers of infection returning to normal. Longer duration of therapy is associated with an increased risk of adverse events and pathogen resistance.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_2")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing2B">
                        <td style="width: 170px;" id="FormNursing2B">2B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_2_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing3">
                        <td style="width: 170px;" id="FormNursing3">3) Microbiology was requested<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Microbiology should be routinely requested on the first day of antimicrobial therapy (ideally before empiric treatment is initiated) for all ICU, High Care and medical patients with a length of stay expectancy greater than 2 days. Appropriate specimens should be taken for culture and sensitivity testing according to the suspected site/s of infection. For example: blood, sputum, urine, cerebrospinal fluid, pus swab, bone marrow, joint or tracheal aspirate.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_3")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing3B">
                        <td style="width: 170px;" id="FormNursing3B">3B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_3_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing4">
                        <td style="width: 170px;" id="FormNursing4">4) Doctor reviewed the microbiology results to consider adjusting treatment<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Results should be received from the microbiology laboratory within 48 hours. Nursing staff should ensure that the Doctor has seen the microbiology results at the next ward round in order to consider adjusting antimicrobial treatment to match the pathogen/s and the resistance/sensitivity results. The doctor should be informed about the lab results sooner if an urgent concern needs to be addressed. Antimicrobial regimens should therefore be reassessed after 48 – 72 hours of initiating empiric therapy. The aim is to use a narrow spectrum agent according to microbiology results and other available clinical evidence.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_4")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (Microbiology was not requested)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5">
                        <td style="width: 170px;" id="FormNursing5">5) IV hang time within 1 hour of prescription<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Prescribed intravenous Antibiotics administered within the first hour.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_5")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no IV antibiotics prescribed)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5B">
                        <td style="width: 170px;" id="FormNursing5B">5B) How long did it take to administer the first antimicrobial after it was prescribed</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing5B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_5_B")%>'>
                            <asp:ListItem Value="1-2 hours">1) 1-2 hours</asp:ListItem>
                            <asp:ListItem Value="2-3 hours">2) 2-3 hours</asp:ListItem>
                            <asp:ListItem Value="3-4 hours">3) 3-4 hours</asp:ListItem>
                            <asp:ListItem Value="More than 4 hours">4) More than 4 hours</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5BReason">
                        <td style="width: 170px;" id="FormNursing5BReason">5B) Reason for delay</td>
                        <td style="width: 730px; padding: 0px;">
                          <asp:CheckBoxList ID="CheckBoxList_InsertNursing5BReason" runat="server" Width="100%" AppendDataBoundItems="true" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatLayout="Table">
                          </asp:CheckBoxList>
                        </td>
                      </tr>
                      <tr id="Nursing6">
                        <td style="width: 170px;" id="FormNursing6">6) When appropriate for patient care, there was a step down from IV to oral administration<br /><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">If the patient is able to swallow with ease (for example, is eating kitchen meals or is on other oral therapy) and is showing clinical signs of improvement, consider changing the route of antimicrobial administration from an IV infusion to oral therapy. An oral antimicrobial with sufficient bioavailability can be provided instead of the IV antimicrobial so that the IV lines (and associated risk of infection) can be removed.</span></span></a></td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_6")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (patient is not receiving antimicrobials intravenously)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing6B">
                        <td style="width: 170px;" id="FormNursing6B">6B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertNursing6B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_6_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing IV administration">3) Provided a clinically appropriate reason for continuing IV administration</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="NursingScore">
                        <td style="width: 170px;">Nursing Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertNursingScore" Width="100px" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy">
                        <td colspan="2" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="PharmacySelectAllYes">
                        <td>Select all aspects Yes</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertPharmacySelectAllYes" runat="server" />
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 170px;" id="FormPharmacy1">1) Surgical prophylaxis was stopped when the patient left theatre</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_1")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no surgery performed or no prophylaxis given)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy1B">
                        <td style="width: 170px;" id="FormPharmacy1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_1_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 170px;" id="FormPharmacy2">2) When appropriate for patient care, there was a step down from IV to oral administration</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_2")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy2B">
                        <td style="width: 170px;" id="FormPharmacy2B">2B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_2_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing IV administration">3) Provided a clinically appropriate reason for continuing IV administration</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 170px;" id="FormPharmacy3">3) Appropriate daily dose (dosage & frequency)</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_3")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3B">
                        <td style="width: 170px;" id="FormPharmacy3B">3B) After suggesting dose alteration, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_3_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for current dose">3) Provided a clinically appropriate reason for current dose</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 170px;" id="FormPharmacy4">4) No duplication of antimicrobial spectrum</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_4")%>'>
                            <asp:ListItem Value="Yes">A) Yes (no duplication)</asp:ListItem>
                            <asp:ListItem Value="No">B) No (there is duplication)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4B">
                        <td style="width: 170px;" id="FormPharmacy4B">4B) After discussing the duplication of antimicrobial spectrum, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy4B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_4_B")%>'>
                            <asp:ListItem Value="Agreed to alter prescription">1) Agreed to alter prescription</asp:ListItem>
                            <asp:ListItem Value="Declined a change to the prescription">2) Declined a change to the prescription</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for the duplication">3) Provided a clinically appropriate reason for the duplication</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 170px;" id="FormPharmacy5">5) Less than four antimicrobials prescribed simultaneously</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_5")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5B">
                        <td style="width: 170px;" id="FormPharmacy5B">5B) After discussing the high number of different antimicrobials being administered each day, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy5B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_5_B")%>'>
                            <asp:ListItem Value="Agreed to alter prescription">1) Agreed to alter prescription</asp:ListItem>
                            <asp:ListItem Value="Declined a change to the prescription">2) Declined a change to the prescription</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason">3) Provided a clinically appropriate reason</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 170px;" id="FormPharmacy6">6) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_6")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (patient is not receiving antimicrobials intravenously)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6B">
                        <td style="width: 170px;" id="FormPharmacy6B">6B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy6B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_6_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 170px;" id="FormPharmacy7">7) Microbiology was requested</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_7")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7B">
                        <td style="width: 170px;" id="FormPharmacy7B">7B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy7B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_7_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 170px;" id="FormPharmacy8">8) Doctor reviewed the microbiology results to consider adjusting treatment</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPharmacy8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_8")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (Microbiology was not requested)</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="PharmacyScore">
                        <td style="width: 170px;">Pharmacy Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertPharmacyScore" Width="100px" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Antibiotics Prescribed
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" id="FormAntibioticsPrescribed" style="padding: 3px;">
                          <table class="Table_Body">
                            <tr>
                              <td>Total Records:
                                <asp:Label ID="Label_InsertTotalRecords" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertTotalRecords" runat="server" />
                              </td>
                            </tr>
                            <tr>
                              <td style="padding: 0px;">
                                <asp:GridView ID="GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_PreRender" OnDataBound="GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_DataBound" OnRowCreated="GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_RowCreated">
                                  <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                                  <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                                  <PagerTemplate>
                                    <table class="GridView_PagerStyle">
                                      <tr>
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </PagerTemplate>
                                  <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <FooterStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                                  <EmptyDataRowStyle CssClass="GridView_EmptyDataStyle_TemplateField" />
                                  <EmptyDataTemplate>
                                    <table class="GridView_EmptyDataStyle">
                                      <tr>
                                        <td>No Antibiotics Prescribed
                                        </td>
                                      </tr>
                                      <tr class="GridView_EmptyDataStyle_FooterStyle">
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </EmptyDataTemplate>
                                  <Columns>
                                    <asp:TemplateField HeaderText="">
                                      <ItemTemplate>
                                        <table class="Table_Body">
                                          <tr>
                                            <td class="Table_TemplateField" style="width: 50px;">
                                              <asp:CheckBox ID="CheckBox_InsertAntibiotic" runat="server" />
                                            </td>
                                            <td class="Table_TemplateField" style="width: 400px;">
                                              <asp:Label ID="Label_InsertAntibioticDescription" runat="server" Text='<%# Bind("Description") %>' Width="400px"></asp:Label>
                                            </td>
                                            <td class="Table_TemplateField" style="width: 450px;">
                                              <asp:TextBox ID="TextBox_InsertAntibiotic_Information" runat="server" TextMode="MultiLine" Rows="3" Width="400px" CssClass="Controls_TextBox"></asp:TextBox>
                                            </td>
                                          </tr>
                                        </table>
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                  </Columns>
                                </asp:GridView>
                              </td>
                            </tr>
                          </table>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("ASI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("ASI_Intervention_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("ASI_Intervention_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" EnableViewState="false" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Assessment" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
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
                        <td style="width: 730px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("ASI_Intervention_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("ASI_Intervention_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:HiddenField ID="HiddenField_EditDate" runat="server" Value='<%# Eval("ASI_Intervention_Date","{0:yyyy/MM/dd}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormByList">Assessment By
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditByList" runat="server" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_EditByList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("ASI_Intervention_By_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select By</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Assessment Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" AppendDataBoundItems="true" DataTextField="Ward" DataValueField="Ward" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDoctor">Attending / Prescribing Doctor
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditDoctor" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="Nursing">
                        <td colspan="2" class="FormView_TableBodyHeader">Nursing
                        </td>
                      </tr>
                      <tr id="NursingSelectAllYes">
                        <td>Select all aspects Yes</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditNursingSelectAllYes" runat="server" />
                        </td>
                      </tr>
                      <tr id="Nursing1">
                        <td style="width: 170px;" id="FormNursing1">1) Surgical prophylaxis was stopped when the patient left theatre&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">The antibiotic selected for surgical prophylaxis must be in accordance with local evidence-based guidelines for the specific surgical procedure and pathogen resistance patterns.  A prophylactic dose should be administered 30-60 minutes before surgical incision. Refer to the literature on preventing surgical site infections & the infection prevention bundles for more information regarding this aspect of administration time. In most cases, surgical prophylaxis should be stopped once the patient is out of theatre. Further antibiotic administration usually then constitutes treatment.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_1")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no surgery performed or no prophylaxis given)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing1B">
                        <td style="width: 170px;" id="FormNursing1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_1_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing2">
                        <td style="width: 170px;" id="FormNursing2">2) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Most infections can be treated with short courses of antimicrobials (3, 5 or 7 days therapy) and treatment should not be unnecessarily prolonged.  Duration of therapy is usually determined by clinical factors such as the site of infection, severity of illness and response to treatment. As a general guide, antibiotics can be discontinued within 48-72 hours of the temperature and other clinical markers of infection returning to normal. Longer duration of therapy is associated with an increased risk of adverse events and pathogen resistance.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_2")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing2B">
                        <td style="width: 170px;" id="FormNursing2B">2B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_2_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing3">
                        <td style="width: 170px;" id="FormNursing3">3) Microbiology was requested&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Microbiology should be routinely requested on the first day of antimicrobial therapy (ideally before empiric treatment is initiated) for all ICU, High Care and medical patients with a length of stay expectancy greater than 2 days. Appropriate specimens should be taken for culture and sensitivity testing according to the suspected site/s of infection. For example: blood, sputum, urine, cerebrospinal fluid, pus swab, bone marrow, joint or tracheal aspirate.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_3")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing3B">
                        <td style="width: 170px;" id="FormNursing3B">3B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_3_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing4">
                        <td style="width: 170px;" id="FormNursing4">4) Doctor reviewed the microbiology results to consider adjusting treatment&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Results should be received from the microbiology laboratory within 48 hours. Nursing staff should ensure that the Doctor has seen the microbiology results at the next ward round in order to consider adjusting antimicrobial treatment to match the pathogen/s and the resistance/sensitivity results. The doctor should be informed about the lab results sooner if an urgent concern needs to be addressed. Antimicrobial regimens should therefore be reassessed after 48 – 72 hours of initiating empiric therapy. The aim is to use a narrow spectrum agent according to microbiology results and other available clinical evidence.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_4")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (Microbiology was not requested)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5">
                        <td style="width: 170px;" id="FormNursing5">5) IV hang time within 1 hour of prescription&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Prescribed intravenous Antibiotics administered within the first hour.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_5")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no IV antibiotics prescribed)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5B">
                        <td style="width: 170px;" id="FormNursing5B">5B) How long did it take to administer the first antimicrobial after it was prescribed</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing5B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_5_B")%>'>
                            <asp:ListItem Value="1-2 hours">1) 1-2 hours</asp:ListItem>
                            <asp:ListItem Value="2-3 hours">2) 2-3 hours</asp:ListItem>
                            <asp:ListItem Value="3-4 hours">3) 3-4 hours</asp:ListItem>
                            <asp:ListItem Value="More than 4 hours">4) More than 4 hours</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing5BReason">
                        <td style="width: 170px;" id="FormNursing5BReason">5B) Reason for delay</td>
                        <td style="width: 730px; padding: 0px;">
                          <asp:CheckBoxList ID="CheckBoxList_EditNursing5BReason" runat="server" Width="100%" AppendDataBoundItems="true" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatLayout="Table" OnDataBound="CheckBoxList_EditNursing5BReason_DataBound">
                          </asp:CheckBoxList>
                        </td>
                      </tr>
                      <tr id="Nursing6">
                        <td style="width: 170px;" id="FormNursing6">6) When appropriate for patient care, there was a step down from IV to oral administration&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">If the patient is able to swallow with ease (for example, is eating kitchen meals or is on other oral therapy) and is showing clinical signs of improvement, consider changing the route of antimicrobial administration from an IV infusion to oral therapy. An oral antimicrobial with sufficient bioavailability can be provided instead of the IV antimicrobial so that the IV lines (and associated risk of infection) can be removed.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_6")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (patient is not receiving antimicrobials intravenously)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Nursing6B">
                        <td style="width: 170px;" id="FormNursing6B">6B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditNursing6B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Nursing_6_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing IV administration">3) Provided a clinically appropriate reason for continuing IV administration</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="NursingScore">
                        <td style="width: 170px;">Nursing Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditNursingScore" Width="100px" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Pharmacy">
                        <td colspan="4" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="PharmacySelectAllYes">
                        <td>Select all aspects Yes</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditPharmacySelectAllYes" runat="server" />
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 170px;" id="FormPharmacy1">1) Surgical prophylaxis was stopped when the patient left theatre</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_1")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (no surgery performed or no prophylaxis given)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy1B">
                        <td style="width: 170px;" id="FormPharmacy1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_1_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 170px;" id="FormPharmacy2">2) When appropriate for patient care, there was a step down from IV to oral administration</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_2")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy2B">
                        <td style="width: 170px;" id="FormPharmacy2B">2B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_2_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing IV administration">3) Provided a clinically appropriate reason for continuing IV administration</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 170px;" id="FormPharmacy3">3) Appropriate daily dose (dosage & frequency)</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_3")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy3B">
                        <td style="width: 170px;" id="FormPharmacy3B">3B) After suggesting dose alteration, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_3_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for current dose">3) Provided a clinically appropriate reason for current dose</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 170px;" id="FormPharmacy4">4) No duplication of antimicrobial spectrum</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_4")%>'>
                            <asp:ListItem Value="Yes">A) Yes (no duplication)</asp:ListItem>
                            <asp:ListItem Value="No">B) No (there is duplication)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy4B">
                        <td style="width: 170px;" id="FormPharmacy4B">4B) After discussing the duplication of antimicrobial spectrum, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy4B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_4_B")%>'>
                            <asp:ListItem Value="Agreed to alter prescription">1) Agreed to alter prescription</asp:ListItem>
                            <asp:ListItem Value="Declined a change to the prescription">2) Declined a change to the prescription</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for the duplication">3) Provided a clinically appropriate reason for the duplication</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 170px;" id="FormPharmacy5">5) Less than four antimicrobials prescribed simultaneously</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_5")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy5B">
                        <td style="width: 170px;" id="FormPharmacy5B">5B) After discussing the high number of different antimicrobials being administered each day, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy5B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_5_B")%>'>
                            <asp:ListItem Value="Agreed to alter prescription">1) Agreed to alter prescription</asp:ListItem>
                            <asp:ListItem Value="Declined a change to the prescription">2) Declined a change to the prescription</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason">3) Provided a clinically appropriate reason</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 170px;" id="FormPharmacy6">6) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_6")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (patient is not receiving antimicrobials intravenously)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy6B">
                        <td style="width: 170px;" id="FormPharmacy6B">6B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy6B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_6_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 170px;" id="FormPharmacy7">7) Microbiology was requested</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_7")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (only surgical prophylaxis was prescribed)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy7B">
                        <td style="width: 170px;" id="FormPharmacy7B">7B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy7B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_7_B")%>'>
                            <asp:ListItem Value="Agreed">1) Agreed</asp:ListItem>
                            <asp:ListItem Value="Declined">2) Declined</asp:ListItem>
                            <asp:ListItem Value="Provided a clinically appropriate reason for continuing">3) Provided a clinically appropriate reason for continuing</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 170px;" id="FormPharmacy8">8) Doctor reviewed the microbiology results to consider adjusting treatment</td>
                        <td style="width: 730px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPharmacy8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" SelectedValue='<%# Bind("ASI_Intervention_Pharmacy_8")%>'>
                            <asp:ListItem Value="Yes">A) Yes</asp:ListItem>
                            <asp:ListItem Value="No">B) No</asp:ListItem>
                            <asp:ListItem Value="Not applicable">C) Not applicable (Microbiology was not requested)</asp:ListItem>
                            <asp:ListItem Value="">Empty</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr id="PharmacyScore">
                        <td style="width: 170px;">Pharmacy Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditPharmacyScore" Width="100px" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Antibiotics Prescribed
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" id="FormAntibioticsPrescribed" style="padding: 3px;">
                          <table class="Table_Body">
                            <tr>
                              <td>Total Records:
                                <asp:Label ID="Label_EditTotalRecords" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenField_EditTotalRecords" runat="server" />
                              </td>
                            </tr>
                            <tr>
                              <td style="padding: 0px;">
                                <asp:GridView ID="GridView_EditAntimicrobialStewardshipIntervention_Antibiotic" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_PreRender" OnDataBound="GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_DataBound" OnRowCreated="GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_RowCreated" OnRowDataBound="GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_RowDataBound">
                                  <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                                  <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                                  <PagerTemplate>
                                    <table class="GridView_PagerStyle">
                                      <tr>
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </PagerTemplate>
                                  <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <FooterStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                                  <EmptyDataRowStyle CssClass="GridView_EmptyDataStyle_TemplateField" />
                                  <EmptyDataTemplate>
                                    <table class="GridView_EmptyDataStyle">
                                      <tr>
                                        <td>No Antibiotics Prescribed
                                        </td>
                                      </tr>
                                      <tr class="GridView_EmptyDataStyle_FooterStyle">
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </EmptyDataTemplate>
                                  <Columns>
                                    <asp:TemplateField HeaderText="">
                                      <ItemTemplate>
                                        <table class="Table_Body">
                                          <tr>
                                            <td class="Table_TemplateField" style="width: 50px;">
                                              <asp:CheckBox ID="CheckBox_EditAntibiotic" runat="server" />
                                            </td>
                                            <td class="Table_TemplateField" style="width: 400px;">
                                              <asp:Label ID="Label_EditAntibioticDescription" runat="server" Text='<%# Bind("Description") %>' Width="400px"></asp:Label><asp:HiddenField ID="HiddenField_EditAntibioticDescription" runat="server" Value='<%# Bind("Description") %>' />
                                            </td>
                                            <td class="Table_TemplateField" style="width: 450px;">
                                              <asp:TextBox ID="TextBox_EditAntibiotic_Information" runat="server" TextMode="MultiLine" Rows="3" Width="400px" CssClass="Controls_TextBox"></asp:TextBox>
                                            </td>
                                          </tr>
                                        </table>
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                  </Columns>
                                </asp:GridView>
                              </td>
                            </tr>
                          </table>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("ASI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("ASI_Intervention_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("ASI_Intervention_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Assessment" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Assessment" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("ASI_Intervention_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("ASI_Intervention_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Assessment By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemByList" runat="server" Text=""></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_ItemByList" runat="server" Value='<%# Eval("ASI_Intervention_By_List") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td>Assessment Unit
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemUnit" runat="server" Text='<%# Bind("ASI_Intervention_Unit") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Attending / Prescribing Doctor
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemDoctor" runat="server" Text='<%# Bind("ASI_Intervention_Doctor") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="Nursing">
                        <td colspan="2" class="FormView_TableBodyHeader">Nursing
                        </td>
                      </tr>
                      <tr id="Nursing1">
                        <td style="width: 170px;" id="FormNursing1">1) Surgical prophylaxis was stopped when the patient left theatre&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">The antibiotic selected for surgical prophylaxis must be in accordance with local evidence-based guidelines for the specific surgical procedure and pathogen resistance patterns.  A prophylactic dose should be administered 30-60 minutes before surgical incision. Refer to the literature on preventing surgical site infections & the infection prevention bundles for more information regarding this aspect of administration time. In most cases, surgical prophylaxis should be stopped once the patient is out of theatre. Further antibiotic administration usually then constitutes treatment.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing1" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_1")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemNursing1" runat="server" Value='<%# Eval("ASI_Intervention_Nursing_1") %>' />
                        </td>
                      </tr>
                      <tr id="Nursing1B">
                        <td style="width: 170px;" id="FormNursing1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing1B" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_1_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Nursing2">
                        <td style="width: 170px;" id="FormNursing2">2) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Most infections can be treated with short courses of antimicrobials (3, 5 or 7 days therapy) and treatment should not be unnecessarily prolonged.  Duration of therapy is usually determined by clinical factors such as the site of infection, severity of illness and response to treatment. As a general guide, antibiotics can be discontinued within 48-72 hours of the temperature and other clinical markers of infection returning to normal. Longer duration of therapy is associated with an increased risk of adverse events and pathogen resistance.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing2" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_2")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemNursing2" runat="server" Value='<%# Eval("ASI_Intervention_Nursing_2") %>' />
                        </td>
                      </tr>
                      <tr id="Nursing2B">
                        <td style="width: 170px;" id="FormNursing2B">2B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing2B" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_2_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Nursing3">
                        <td style="width: 170px;" id="FormNursing3">3) Microbiology was requested&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Microbiology should be routinely requested on the first day of antimicrobial therapy (ideally before empiric treatment is initiated) for all ICU, High Care and medical patients with a length of stay expectancy greater than 2 days. Appropriate specimens should be taken for culture and sensitivity testing according to the suspected site/s of infection. For example: blood, sputum, urine, cerebrospinal fluid, pus swab, bone marrow, joint or tracheal aspirate.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing3" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_3")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemNursing3" runat="server" Value='<%# Eval("ASI_Intervention_Nursing_3") %>' />
                        </td>
                      </tr>
                      <tr id="Nursing3B">
                        <td style="width: 170px;" id="FormNursing3B">3B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing3B" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_3_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Nursing4">
                        <td style="width: 170px;" id="FormNursing4">4) Doctor reviewed the microbiology results to consider adjusting treatment&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Results should be received from the microbiology laboratory within 48 hours. Nursing staff should ensure that the Doctor has seen the microbiology results at the next ward round in order to consider adjusting antimicrobial treatment to match the pathogen/s and the resistance/sensitivity results. The doctor should be informed about the lab results sooner if an urgent concern needs to be addressed. Antimicrobial regimens should therefore be reassessed after 48 – 72 hours of initiating empiric therapy. The aim is to use a narrow spectrum agent according to microbiology results and other available clinical evidence.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing4" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_4")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Nursing5">
                        <td style="width: 170px;" id="FormNursing5">5) IV hang time within 1 hour of prescription&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">Prescribed intravenous Antibiotics administered within the first hour.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing5" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_5")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemNursing5" runat="server" Value='<%# Eval("ASI_Intervention_Nursing_5") %>' />
                        </td>
                      </tr>
                      <tr id="Nursing5B">
                        <td style="width: 170px;" id="FormNursing5B">5B) How long did it take to administer the first antimicrobial after it was prescribed</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing5B" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_5_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Nursing5BReason">
                        <td style="width: 170px;" id="FormNursing5BReason">5B) Reason for delay</td>
                        <td style="width: 730px; padding: 0px;">
                          <asp:GridView ID="GridView_ItemAntimicrobialStewardshipIntervention_Nursing5BReason" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemAntimicrobialStewardshipIntervention_Nursing5BReason_RowCreated" OnPreRender="GridView_ItemAntimicrobialStewardshipIntervention_Nursing5BReason_PreRender">
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
                              No Reason for delay selected
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ListItem_Name" HeaderText="Reason" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="ListItem_Name" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr id="Nursing6">
                        <td style="width: 170px;" id="FormNursing6">6) When appropriate for patient care, there was a step down from IV to oral administration&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle">If the patient is able to swallow with ease (for example, is eating kitchen meals or is on other oral therapy) and is showing clinical signs of improvement, consider changing the route of antimicrobial administration from an IV infusion to oral therapy. An oral antimicrobial with sufficient bioavailability can be provided instead of the IV antimicrobial so that the IV lines (and associated risk of infection) can be removed.</span></span></a>&nbsp;&nbsp;</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing6" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_6")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemNursing6" runat="server" Value='<%# Eval("ASI_Intervention_Nursing_6") %>' />
                        </td>
                      </tr>
                      <tr id="Nursing6B">
                        <td style="width: 170px;" id="FormNursing6B">6B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursing6B" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_6_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="NursingScore">
                        <td style="width: 170px;">Nursing Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursingScore" runat="server" Text='<%# Bind("ASI_Intervention_Nursing_Score") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy">
                        <td colspan="2" class="FormView_TableBodyHeader">Pharmacy
                        </td>
                      </tr>
                      <tr id="Pharmacy1">
                        <td style="width: 170px;" id="FormPharmacy1">1) Surgical prophylaxis was stopped when the patient left theatre</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy1" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_1")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy1" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_1") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy1B">
                        <td style="width: 170px;" id="FormPharmacy1B">1B) After suggesting that prophylaxis be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy1B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_1_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy2">
                        <td style="width: 170px;" id="FormPharmacy2">2) When appropriate for patient care, there was a step down from IV to oral administration</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy2" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_2")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy2" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_2") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy2B">
                        <td style="width: 170px;" id="FormPharmacy2B">2B) After asking whether IV line could be removed, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy2B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_2_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy3">
                        <td style="width: 170px;" id="FormPharmacy3">3) Appropriate daily dose (dosage & frequency)</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy3" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_3")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy3" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_3") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy3B">
                        <td style="width: 170px;" id="FormPharmacy3B">3B) After suggesting dose alteration, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy3B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_3_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy4">
                        <td style="width: 170px;" id="FormPharmacy4">4) No duplication of antimicrobial spectrum</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy4" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_4")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy4" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_4") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy4B">
                        <td style="width: 170px;" id="FormPharmacy4B">4B) After discussing the duplication of antimicrobial spectrum, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy4B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_4_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy5">
                        <td style="width: 170px;" id="FormPharmacy5">5) Less than four antimicrobials prescribed simultaneously</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy5" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_5")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy5" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_5") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy5B">
                        <td style="width: 170px;" id="FormPharmacy5B">5B) After discussing the high number of different antimicrobials being administered each day, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy5B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_5_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy6">
                        <td style="width: 170px;" id="FormPharmacy6">6) Upon reaching day 7 of antimicrobial therapy, treatment was either stopped or altered</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy6" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_6")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy6" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_6") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy6B">
                        <td style="width: 170px;" id="FormPharmacy6B">6B) After asking whether treatment could be stopped, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy6B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_6_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy7">
                        <td style="width: 170px;" id="FormPharmacy7">7) Microbiology was requested</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy7" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_7")%>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemPharmacy7" runat="server" Value='<%# Eval("ASI_Intervention_Pharmacy_7") %>' />
                        </td>
                      </tr>
                      <tr id="Pharmacy7B">
                        <td style="width: 170px;" id="FormPharmacy7B">7B) After suggesting that a specimen be taken for culture, Doctor</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy7B" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_7_B")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="Pharmacy8">
                        <td style="width: 170px;" id="FormPharmacy8">8) Doctor reviewed the microbiology results to consider adjusting treatment</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacy8" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_8")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="PharmacyScore">
                        <td style="width: 170px;">Pharmacy Compliance Score</td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPharmacyScore" runat="server" Text='<%# Bind("ASI_Intervention_Pharmacy_Score") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Antibiotics Prescribed
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px;">
                          <asp:GridView ID="GridView_ItemAntimicrobialStewardshipIntervention_Antibiotic" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemAntimicrobialStewardshipIntervention_Antibiotic_RowCreated" OnPreRender="GridView_ItemAntimicrobialStewardshipIntervention_Antibiotic_PreRender">
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
                              No Antibiotics Prescribed selected
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ASI_Antibiotic_Description" HeaderText="Antibiotic" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="ASI_Antibiotic_Description" />
                              <asp:BoundField DataField="ASI_Antibiotic_Information" HeaderText="Information" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="ASI_Antibiotic_Information" />
                            </Columns>
                          </asp:GridView>
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("ASI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("ASI_Intervention_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("ASI_Intervention_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("ASI_Intervention_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Assessment" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_EditByList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_Form" runat="server" OnInserted="SqlDataSource_AntimicrobialStewardshipIntervention_Form_Inserted" OnUpdated="SqlDataSource_AntimicrobialStewardshipIntervention_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableIntervention" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InterventionHeading" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_AntimicrobialStewardshipIntervention_Intervention" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_AntimicrobialStewardshipIntervention_Intervention" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_AntimicrobialStewardshipIntervention_Intervention_PreRender" OnDataBound="GridView_AntimicrobialStewardshipIntervention_Intervention_DataBound" OnRowCreated="GridView_AntimicrobialStewardshipIntervention_Intervention_RowCreated">
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
                              <td>of <%=GridView_AntimicrobialStewardshipIntervention_Intervention.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Assessment" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Assessment" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("ASI_Intervention_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="ASI_Intervention_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="ASI_Intervention_ReportNumber" />
                          <asp:BoundField DataField="ASI_Intervention_Date" HeaderText="Date" ReadOnly="True" SortExpression="ASI_Intervention_Date" />
                          <asp:BoundField DataField="ASI_Intervention_By_Name" HeaderText="Assessment By" ReadOnly="True" SortExpression="ASI_Intervention_By_Name" />
                          <asp:BoundField DataField="ASI_Intervention_Unit" HeaderText="Unit" ReadOnly="True" SortExpression="ASI_Intervention_Unit" />
                          <asp:BoundField DataField="ASI_Intervention_Score" HeaderText="Score" ReadOnly="True" SortExpression="ASI_Intervention_Score" />
                          <asp:BoundField DataField="ASI_Intervention_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="ASI_Intervention_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_AntimicrobialStewardshipIntervention_Intervention" runat="server" OnSelected="SqlDataSource_AntimicrobialStewardshipIntervention_Intervention_Selected"></asp:SqlDataSource>
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
