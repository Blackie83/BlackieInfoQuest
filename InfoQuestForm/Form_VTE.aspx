<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_VTE.aspx.cs" Inherits="InfoQuestForm.Form_VTE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - VTE</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_VTE.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_VTE" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_VTE" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_VTE" AssociatedUpdatePanelID="UpdatePanel_VTE">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_VTE" runat="server">
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_VTE_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_VTE_Facility" runat="server"></asp:SqlDataSource>
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
          <table id="TableVisitInfo" class="Table" style="width: 1000px;" runat="server">
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
                      <asp:Label ID="Label_VIAge" runat="server" Text="" EnableViewState="false"></asp:Label>
                      <asp:HiddenField ID="HiddenField_VIAge" runat="server" Value="75" />&nbsp;
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
          <table id="TableCurrentAssesment" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentAssesmentHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_VTE_Form" runat="server" DataKeyNames="VTE_Assesments_Id" CssClass="FormView" DataSourceID="SqlDataSource_VTE_Form" OnItemInserting="FormView_VTE_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_VTE_Form_ItemCommand" OnDataBound="FormView_VTE_Form_DataBound" OnItemUpdating="FormView_VTE_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="5">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="4" style="width: 730px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("VTE_Assesments_ReportNumber") %>' EnableViewState="false"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Date of assesment<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("VTE_Assesments_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox" EnableViewState="false"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" EnableViewState="false" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate" EnableViewState="false">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark" EnableViewState="false">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBy">Assesment by
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertBy" Width="400px" runat="server" Text='<%# Bind("VTE_Assesments_By") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_VTE_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormWeight">Weight (kg)<br />e.g. (75.32 kg)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertWeight" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_Weight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertWeight" runat="server" TargetControlID="TextBox_InsertWeight" FilterType="Custom,Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;kg
                        </td>
                      </tr>
                      <tr>
                        <td id="FormHeight">Height (m)<br />e.g. (1.82 m)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertHeight" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_Height") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertHeight" runat="server" TargetControlID="TextBox_InsertHeight" FilterType="Custom,Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;m
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBMI">BMI (Weight / (Height * Height))<br />e.g. (22.74 BMI)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertBMI" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_BMI") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Baseline Risk Factors
                        </td>
                        <td style="width: 20px;" rowspan="22"></td>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Predisposing Risk Factors
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Medical</u></strong></td>
                        <td colspan="2"><strong><u>Morbidities</u></strong></td>
                      </tr>
                      <tr>
                        <td style="width: 240px;" id="FormBRFMedicalStroke">Stroke</td>
                        <td style="width: 240px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalStroke" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Stroke")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td style="width: 240px;" id="FormPRFMorbiditiesHistory">History of DVT or embolism</td>
                        <td style="width: 240px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFMorbiditiesHistory" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_History")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartAttack">Heart Attack</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalHeartAttack" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_HeartAttack")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesCancer">Active cancer or cancer therapy</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFMorbiditiesCancer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_Cancer")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartFailure">Heart failure (Congestive)</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalHeartFailure" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_HeartFailure")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesObesity">Obesity (BMI > 30)</td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertPRFMorbiditiesObesity" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertPRFMorbiditiesObesity" runat="server" Value='<%# Bind("VTE_Assesments_PRF_Morbidities_Obesity") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalInfection">Infection on IV antibiotics</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalInfection" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Infection")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesVaricoseVeins">Varicose veins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFMorbiditiesVaricoseVeins" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_VaricoseVeins")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalThrombolytic">Thrombolytic medication stopped earlier than 24 hours before surgery.</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalThrombolytic" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Thrombolytic")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesInflammatoryBowel">Inflammatory bowel disease</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFMorbiditiesInflammatoryBowel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_InflammatoryBowel")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalCVA">Central Venous Access</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFMedicalCVA" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_CVA")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2"><strong><u>Age</u></strong></td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Surgical</u></strong></td>
                        <td id="FormPRFAgeYears">Age</td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertPRFAgeYears" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_InsertPRFAgeYears" runat="server" Value='<%# Bind("VTE_Assesments_PRF_Age_Years") %>' /></td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryOfPelvis">Surgery of pelvis/hips/legs</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalSurgeryOfPelvis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryOfPelvis")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalFractureOfPelvis">Fracture of pelvis/hips/legs</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalFractureOfPelvis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_FractureOfPelvis")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"><strong><u>Gender Related</u></strong></td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalMultipleTrauma">Multiple trauma</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalMultipleTrauma" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_MultipleTrauma")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderOralContraceptive">Oral Contraceptive</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFGenderOralContraceptive" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_OralContraceptive")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSpinalCordInjury">Spinal cord injury</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalSpinalCordInjury" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SpinalCordInjury")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderHormoneReplacementTherapy">Hormone Replacement Therapy (HRT)</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFGenderHormoneReplacementTherapy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_HormoneReplacementTherapy")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalPlaster">Plaster cast upper or lower limb</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalPlaster" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_Plaster")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderPregnancy">Pregnancy or <6 weeks post-partum</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPRFGenderPregnancy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_Pregnancy")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryAbove45Min">Any surgery > 45 mins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalSurgeryAbove45Min" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryAbove45Min")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryBelow45Min">Any surgery < 45 mins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFSurgicalSurgeryBelow45Min" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryBelow45Min")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Both</u></strong></td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFBothPatientInBed">Patient in bed > 72 hours</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertBRFBothPatientInBed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Both_PatientInBed")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td><strong><u>Sum of Baseline Risk Factors</u></strong></td>
                        <td><asp:TextBox ID="TextBox_InsertBRFScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_BRF_Score") %>'></asp:TextBox></td>
                        <td><strong><u>Sum of Predisposing Risk Factors</u></strong></td>
                        <td><asp:TextBox ID="TextBox_InsertPRFScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_PRF_Score") %>'></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td><strong><u>Total Risk Factor Score (RFS)</u></strong></td>
                        <td><asp:TextBox ID="TextBox_InsertRFSScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_RFS_Score") %>'></asp:TextBox></td>
                        <td></td>
                        <td><strong><u>Risk Factor Level</u></strong></td>
                        <td><asp:TextBox ID="TextBox_InsertRFSLevel" runat="server" CssClass="Controls_TextBox_Calculation" Width="200px" Text='<%# Bind("VTE_Assesments_RFS_Level") %>'></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 1</td>
                        <td>Low</td>
                        <td rowspan="2"></td>
                        <td>Risk Factor Score (RFS) 2</td>
                        <td>Moderate</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 3 - 4</td>
                        <td>High</td>
                        <td>Risk Factor Score (RFS) 5 / 5+</td>
                        <td>Extremely High</td>
                      </tr>
                      <tr id="RiskFactorLevel1">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel2">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Relative Contra-Indications to Anti Coagulation: Consider Risk of bleeding vs. benefit of Rx</td>
                      </tr>
                      <tr id="RiskFactorLevel3">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel4">
                        <td>Uncontrolled systolic hypertension<br />(> 140mmHg)</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeSystolicHypertension" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_SystolicHypertension") %>' /></td>
                        <td rowspan="3"></td>
                        <td>Use of anti-coagulants as per formulary</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeAntiCoagulants" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_AntiCoagulants") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel5">
                        <td>Acute haemorrhagic stroke</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeHaemorrhagicStroke" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_HaemorrhagicStroke") %>' /></td>
                        <td>Low platelets</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeLowPlatelets" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_LowPlatelets") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel6">
                        <td>Active bleeding</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeActiveBleeding" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_ActiveBleeding") %>' /></td>
                        <td>Acquired or inherited bleeding disorder</td>
                        <td><asp:CheckBox ID="CheckBox_InsertRelativeBleedingDisorder" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_BleedingDisorder") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel7">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel8">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Doctor</td>
                      </tr>
                      <tr id="RiskFactorLevel9">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel10">
                        <td id="FormDoctorDoctorNotified">Doctor Notified</td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_InsertDoctorDoctorNotified" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel11">
                        <td id="FormDoctorReasonNotNotified">Reason why Doctor was not notified</td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertDoctorReasonNotNotified" Width="700px" TextMode="MultiLine" Rows="4" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotNotified") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel12">
                        <td id="FormDoctorTreatmentInitiated">Treatment initiated by Doctor</td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_InsertDoctorTreatmentInitiated" runat="server" SelectedValue='<%# Bind("VTE_Assesments_Doctor_TreatmentInitiated") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel13">
                        <td id="FormDoctorReasonNotInitiated">Reason why Doctor did not initiate treatment</td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertDoctorReasonNotInitiated" Width="700px" TextMode="MultiLine" Rows="4" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotInitiated") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("VTE_Assesments_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("VTE_Assesments_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("VTE_Assesments_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Assesment" CssClass="Controls_Button" EnableViewState="false" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="5">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="4" style="width: 730px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("VTE_Assesments_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Assesment Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("VTE_Assesments_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:HiddenField ID="HiddenField_EditDate" runat="server" Value='<%# Eval("VTE_Assesments_Date","{0:yyyy/MM/dd}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBy">Assesment by
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditBy" Width="400px" runat="server" Text='<%# Bind("VTE_Assesments_By") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_VTE_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormWeight">Weight (kg)<br />e.g. (75.32 kg)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditWeight" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_Weight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditWeight" runat="server" TargetControlID="TextBox_EditWeight" FilterType="Custom,Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;kg
                        </td>
                      </tr>
                      <tr>
                        <td id="FormHeight">Height (m)<br />e.g. (1.82 m)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditHeight" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_Height") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditHeight" runat="server" TargetControlID="TextBox_EditHeight" FilterType="Custom,Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;m
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBMI">BMI (Weight / (Height * Height))<br />e.g. (22.74 BMI)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditBMI" Width="100px" runat="server" Text='<%# Bind("VTE_Assesments_BMI") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation" EnableViewState="false"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Baseline Risk Factors
                        </td>
                        <td style="width: 20px;" rowspan="22"></td>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Predisposing Risk Factors
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Medical</u></strong></td>
                        <td colspan="2"><strong><u>Morbidities</u></strong></td>
                      </tr>
                      <tr>
                        <td style="width: 240px;" id="FormBRFMedicalStroke">Stroke</td>
                        <td style="width: 240px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalStroke" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Stroke")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td style="width: 240px;" id="FormPRFMorbiditiesHistory">History of DVT or embolism</td>
                        <td style="width: 240px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFMorbiditiesHistory" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_History")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartAttack">Heart Attack</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalHeartAttack" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_HeartAttack")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesCancer">Active cancer or cancer therapy</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFMorbiditiesCancer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_Cancer")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartFailure">Heart failure (Congestive)</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalHeartFailure" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_HeartFailure")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesObesity">Obesity (BMI > 30)</td>
                        <td>
                          <asp:TextBox ID="TextBox_EditPRFMorbiditiesObesity" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditPRFMorbiditiesObesity" runat="server" Value='<%# Bind("VTE_Assesments_PRF_Morbidities_Obesity") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalInfection">Infection on IV antibiotics</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalInfection" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Infection")%>'>
                            <asp:ListItem Value="Yes (3)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesVaricoseVeins">Varicose veins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFMorbiditiesVaricoseVeins" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_VaricoseVeins")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalThrombolytic">Thrombolytic medication stopped earlier than 24 hours before surgery.</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalThrombolytic" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_Thrombolytic")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFMorbiditiesInflammatoryBowel">Inflammatory bowel disease</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFMorbiditiesInflammatoryBowel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Morbidities_InflammatoryBowel")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalCVA">Central Venous Access</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFMedicalCVA" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Medical_CVA")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2"><strong><u>Age</u></strong></td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Surgical</u></strong></td>
                        <td id="FormPRFAgeYears">Age</td>
                        <td>
                          <asp:TextBox ID="TextBox_EditPRFAgeYears" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_EditPRFAgeYears" runat="server" Value='<%# Bind("VTE_Assesments_PRF_Age_Years") %>' /></td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryOfPelvis">Surgery of pelvis/hips/legs</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalSurgeryOfPelvis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryOfPelvis")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalFractureOfPelvis">Fracture of pelvis/hips/legs</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalFractureOfPelvis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_FractureOfPelvis")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"><strong><u>Gender Related</u></strong></td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalMultipleTrauma">Multiple trauma</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalMultipleTrauma" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_MultipleTrauma")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderOralContraceptive">Oral Contraceptive</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFGenderOralContraceptive" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_OralContraceptive")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSpinalCordInjury">Spinal cord injury</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalSpinalCordInjury" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SpinalCordInjury")%>'>
                            <asp:ListItem Value="Yes (5)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderHormoneReplacementTherapy">Hormone Replacement Therapy (HRT)</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFGenderHormoneReplacementTherapy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_HormoneReplacementTherapy")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalPlaster">Plaster cast upper or lower limb</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalPlaster" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_Plaster")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormPRFGenderPregnancy">Pregnancy or <6 weeks post-partum</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPRFGenderPregnancy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_PRF_Gender_Pregnancy")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryAbove45Min">Any surgery > 45 mins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalSurgeryAbove45Min" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryAbove45Min")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryBelow45Min">Any surgery < 45 mins</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFSurgicalSurgeryBelow45Min" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryBelow45Min")%>'>
                            <asp:ListItem Value="Yes (1)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Both</u></strong></td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFBothPatientInBed">Patient in bed > 72 hours</td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditBRFBothPatientInBed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("VTE_Assesments_BRF_Both_PatientInBed")%>'>
                            <asp:ListItem Value="Yes (2)">Yes&nbsp&nbsp</asp:ListItem>
                            <asp:ListItem Value="No (0)">No</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td><strong><u>Sum of Baseline Risk Factors</u></strong></td>
                        <td><asp:TextBox ID="TextBox_EditBRFScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_BRF_Score") %>'></asp:TextBox></td>
                        <td><strong><u>Sum of Predisposing Risk Factors</u></strong></td>
                        <td><asp:TextBox ID="TextBox_EditPRFScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_PRF_Score") %>'></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td><strong><u>Total Risk Factor Score (RFS)</u></strong></td>
                        <td><asp:TextBox ID="TextBox_EditRFSScore" runat="server" CssClass="Controls_TextBox_Calculation" Width="100px" Text='<%# Bind("VTE_Assesments_RFS_Score") %>'></asp:TextBox></td>
                        <td></td>
                        <td><strong><u>Risk Factor Level</u></strong></td>
                        <td><asp:TextBox ID="TextBox_EditRFSLevel" runat="server" CssClass="Controls_TextBox_Calculation" Width="200px" Text='<%# Bind("VTE_Assesments_RFS_Level") %>'></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 1</td>
                        <td>Low</td>
                        <td rowspan="2"></td>
                        <td>Risk Factor Score (RFS) 2</td>
                        <td>Moderate</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 3 - 4</td>
                        <td>High</td>
                        <td>Risk Factor Score (RFS) 5 / 5+</td>
                        <td>Extremely High</td>
                      </tr>
                      <tr id="RiskFactorLevel1">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel2">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Relative Contra-Indications to Anti Coagulation: Consider Risk of bleeding vs. benefit of Rx</td>
                      </tr>
                      <tr id="RiskFactorLevel3">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel4">
                        <td>Uncontrolled systolic hypertension<br />(> 140mmHg)</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeSystolicHypertension" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_SystolicHypertension") %>' /></td>
                        <td></td>
                        <td>Use of anti-coagulants as per formulary</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeAntiCoagulants" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_AntiCoagulants") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel5">
                        <td>Acute haemorrhagic stroke</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeHaemorrhagicStroke" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_HaemorrhagicStroke") %>' /></td>
                        <td></td>
                        <td>Low platelets</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeLowPlatelets" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_LowPlatelets") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel6">
                        <td>Active bleeding</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeActiveBleeding" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_ActiveBleeding") %>' /></td>
                        <td></td>
                        <td>Acquired or inherited bleeding disorder</td>
                        <td><asp:CheckBox ID="CheckBox_EditRelativeBleedingDisorder" runat="server" Checked='<%# Bind("VTE_Assesments_Relative_BleedingDisorder") %>' /></td>
                      </tr>
                      <tr id="RiskFactorLevel7">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel8">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Doctor</td>
                      </tr>
                      <tr id="RiskFactorLevel9">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel10">
                        <td id="FormDoctorDoctorNotified">Doctor Notified</td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_EditDoctorDoctorNotified" runat="server" AppendDataBoundItems="true" DataTextField="Practitioner" DataValueField="Practitioner" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Doctor</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel11">
                        <td id="FormDoctorReasonNotNotified">Reason why Doctor was not notified</td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditDoctorReasonNotNotified" Width="700px" TextMode="MultiLine" Rows="4" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotNotified") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel12">
                        <td id="FormDoctorTreatmentInitiated">Treatment initiated by Doctor</td>
                        <td colspan="4">
                          <asp:DropDownList ID="DropDownList_EditDoctorTreatmentInitiated" runat="server" SelectedValue='<%# Bind("VTE_Assesments_Doctor_TreatmentInitiated") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel13">
                        <td id="FormDoctorReasonNotInitiated">Reason why Doctor did not initiate treatment</td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_EditDoctorReasonNotInitiated" Width="700px" TextMode="MultiLine" Rows="4" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotInitiated") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("VTE_Assesments_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("VTE_Assesments_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("VTE_Assesments_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Assesment" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Assesment" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="5"></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="4" style="width: 730px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("VTE_Assesments_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Evaluation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("VTE_Assesments_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Assesment by
                        </td>
                        <td colspan="4">
                            <asp:Label ID="Label_ItemESMCal" runat="server" Text='<%# Bind("VTE_Assesments_By") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Unit
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Weight (kg)<br />e.g. (75.32 kg)
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemWeight" runat="server" Text='<%# Bind("VTE_Assesments_Weight","{0:#,##0.00}") %>'></asp:Label>&nbsp;kg
                        </td>
                      </tr>
                      <tr>
                        <td>Height (m)<br />e.g. (1.82 m)
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemHeight" runat="server" Text='<%# Bind("VTE_Assesments_Height","{0:#,##0.00}") %>'></asp:Label>&nbsp;m
                        </td>
                      </tr>
                      <tr>
                        <td>BMI (Weight / (Height * Height))<br />e.g. (22.74 BMI)
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemBMI" runat="server" Text='<%# Bind("VTE_Assesments_BMI","{0:#,##0.00}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Baseline Risk Factors
                        </td>
                        <td style="width: 20px;" rowspan="22"></td>
                        <td colspan="2" class="FormView_TableBodyHeader" style="width: 480px;">Predisposing Risk Factors
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Medical</u></strong></td>
                        <td colspan="2"><strong><u>Morbidities</u></strong></td>
                      </tr>
                      <tr>
                        <td style="width: 240px;" id="FormBRFMedicalStroke">Stroke</td>
                        <td style="width: 240px;">
                          <asp:Label ID="Label_ItemBRFMedicalStroke" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_Stroke")%>'></asp:Label>
                        </td>
                        <td style="width: 240px;" id="FormPRFMorbiditiesHistory">History of DVT or embolism</td>
                        <td style="width: 240px;">
                          <asp:Label ID="Label_ItemPRFMorbiditiesHistory" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Morbidities_History")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartAttack">Heart Attack</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFMedicalHeartAttack" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_HeartAttack")%>'></asp:Label>
                        </td>
                        <td id="FormPRFMorbiditiesCancer">Active cancer or cancer therapy</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFMorbiditiesCancer" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Morbidities_Cancer")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalHeartFailure">Heart failure (Congestive)</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFMedicalHeartFailure" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_HeartFailure")%>'></asp:Label>
                        </td>
                        <td id="FormPRFMorbiditiesObesity">Obesity (BMI > 30)</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFMorbiditiesObesity" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Morbidities_Obesity") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalInfection">Infection on IV antibiotics</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFMedicalInfection" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_Infection")%>'></asp:Label>
                        </td>
                        <td id="FormPRFMorbiditiesVaricoseVeins">Varicose veins</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFMorbiditiesVaricoseVeins" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Morbidities_VaricoseVeins")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalThrombolytic">Thrombolytic medication stopped earlier than 24 hours before surgery.</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFMedicalThrombolytic" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_Thrombolytic")%>'></asp:Label>
                        </td>
                        <td id="FormPRFMorbiditiesInflammatoryBowel">Inflammatory bowel disease</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFMorbiditiesInflammatoryBowel" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Morbidities_InflammatoryBowel")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFMedicalCVA">Central Venous Access</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFMedicalCVA" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Medical_CVA")%>'></asp:Label>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2"><strong><u>Age</u></strong></td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Surgical</u></strong></td>
                        <td id="FormPRFAgeYears">Age</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFAgeYears" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Age_Years") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryOfPelvis">Surgery of pelvis/hips/legs</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalSurgeryOfPelvis" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryOfPelvis")%>'></asp:Label>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalFractureOfPelvis">Fracture of pelvis/hips/legs</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalFractureOfPelvis" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_FractureOfPelvis")%>'></asp:Label>
                        </td>
                        <td colspan="2"><strong><u>Gender Related</u></strong></td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalMultipleTrauma">Multiple trauma</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalMultipleTrauma" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_MultipleTrauma")%>'></asp:Label>
                        </td>
                        <td id="FormPRFGenderOralContraceptive">Oral Contraceptive</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFGenderOralContraceptive" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Gender_OralContraceptive")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSpinalCordInjury">Spinal cord injury</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalSpinalCordInjury" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_SpinalCordInjury")%>'></asp:Label>
                        </td>
                        <td id="FormPRFGenderHormoneReplacementTherapy">Hormone Replacement Therapy (HRT)</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFGenderHormoneReplacementTherapy" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Gender_HormoneReplacementTherapy")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalPlaster">Plaster cast upper or lower limb</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalPlaster" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_Plaster")%>'></asp:Label>
                        </td>
                        <td id="FormPRFGenderPregnancy">Pregnancy or <6 weeks post-partum</td>
                        <td>
                          <asp:Label ID="Label_ItemPRFGenderPregnancy" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Gender_Pregnancy")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryAbove45Min">Any surgery > 45 mins</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalSurgeryAbove45Min" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryAbove45Min")%>'></asp:Label>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFSurgicalSurgeryBelow45Min">Any surgery < 45 mins</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFSurgicalSurgeryBelow45Min" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Surgical_SurgeryBelow45Min")%>'></asp:Label>
                        </td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong><u>Both</u></strong></td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td id="FormBRFBothPatientInBed">Patient in bed > 72 hours</td>
                        <td>
                          <asp:Label ID="Label_ItemBRFBothPatientInBed" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Both_PatientInBed")%>'></asp:Label>
                        </td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp</td>
                        <td colspan="2">&nbsp</td>
                      </tr>
                      <tr>
                        <td><strong><u>Sum of Baseline Risk Factors</u></strong></td>
                        <td><strong><asp:Label ID="Label_ItemBRFScore" runat="server" Text='<%# Bind("VTE_Assesments_BRF_Score") %>'></asp:Label></strong></td>
                        <td><strong><u>Sum of Predisposing Risk Factors</u></strong></td>
                        <td><strong><asp:Label ID="Label_ItemPRFScore" runat="server" Text='<%# Bind("VTE_Assesments_PRF_Score") %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td><strong><u>Total Risk Factor Score (RFS)</u></strong></td>
                        <td>
                          <strong><asp:Label ID="Label_ItemRFSScore" runat="server" Text='<%# Bind("VTE_Assesments_RFS_Score") %>'></asp:Label></strong>
                          <asp:HiddenField ID="HiddenField_ItemRFSScore" runat="server" Value='<%# Bind("VTE_Assesments_RFS_Score") %>' />
                        </td>
                        <td></td>
                        <td><strong><u>Risk Factor Level</u></strong></td>
                        <td>
                          <strong><asp:Label ID="Label_ItemRFSLevel" runat="server" Text='<%# Bind("VTE_Assesments_RFS_Level") %>'></asp:Label></strong>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 1</td>
                        <td>Low</td>
                        <td rowspan="2"></td>
                        <td>Risk Factor Score (RFS) 2</td>
                        <td>Moderate</td>
                      </tr>
                      <tr>
                        <td>Risk Factor Score (RFS) 3 - 4</td>
                        <td>High</td>
                        <td>Risk Factor Score (RFS) 5 / 5+</td>
                        <td>Extremely High</td>
                      </tr>
                      <tr id="RiskFactorLevel1">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel2">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Relative Contra-Indications to Anti Coagulation: Consider Risk of bleeding vs. benefit of Rx</td>
                      </tr>
                      <tr id="RiskFactorLevel3">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel4">
                        <td>Uncontrolled systolic hypertension<br />(> 140mmHg)</td>
                        <td><asp:Label ID="Label_ItemRelativeSystolicHypertension" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_SystolicHypertension"))?"Yes":"No" %>'></asp:Label></td>
                        <td></td>
                        <td>Use of anti-coagulants as per formulary</td>
                        <td><asp:Label ID="Label_ItemRelativeAntiCoagulants" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_AntiCoagulants"))?"Yes":"No" %>'></asp:Label></td>
                      </tr>
                      <tr id="RiskFactorLevel5">
                        <td>Acute haemorrhagic stroke</td>
                        <td><asp:Label ID="Label_ItemRelativeHaemorrhagicStroke" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_HaemorrhagicStroke"))?"Yes":"No" %>'></asp:Label></td>
                        <td></td>
                        <td>Low platelets</td>
                        <td><asp:Label ID="Label_ItemRelativeLowPlatelets" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_LowPlatelets"))?"Yes":"No" %>'></asp:Label></td>
                      </tr>
                      <tr id="RiskFactorLevel6">
                        <td>Active bleeding</td>
                        <td><asp:Label ID="Label_ItemRelativeActiveBleeding" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_ActiveBleeding"))?"Yes":"No" %>'></asp:Label></td>
                        <td></td>
                        <td>Acquired or inherited bleeding disorder</td>
                        <td><asp:Label ID="Label_ItemRelativeBleedingDisorder" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_Relative_BleedingDisorder"))?"Yes":"No" %>'></asp:Label></td>
                      </tr>
                      <tr id="RiskFactorLevel7">
                        <td colspan="5">&nbsp;</td>
                      </tr>
                      <tr id="RiskFactorLevel8">
                        <td colspan="5" class="FormView_TableBodyHeader" style="text-align: center;">Doctor</td>
                      </tr>
                      <tr id="RiskFactorLevel9">
                        <td colspan="5" style="text-align: center;"><strong>Complete only if Risk Factor Level is "High" or "Extremely High"</strong></td>
                      </tr>
                      <tr id="RiskFactorLevel10">
                        <td>Doctor Notified</td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemDoctorDoctorNotified" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_DoctorNotified") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDoctorDoctorNotified" runat="server" Value='<%# Bind("VTE_Assesments_Doctor_DoctorNotified") %>' />
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel11">
                        <td>Reason why Doctor was not notified</td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemDoctorReasonNotNotified" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotNotified") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel12">
                        <td>Treatment initiated by Doctor</td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemDoctorTreatmentInitiated" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_TreatmentInitiated") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDoctorTreatmentInitiated" runat="server" Value='<%# Bind("VTE_Assesments_Doctor_TreatmentInitiated") %>' />
                        </td>
                      </tr>
                      <tr id="RiskFactorLevel13">
                        <td>Reason why Doctor did not initiate treatment</td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemDoctorReasonNotInitiated" runat="server" Text='<%# Bind("VTE_Assesments_Doctor_ReasonNotInitiated") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("VTE_Assesments_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("VTE_Assesments_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("VTE_Assesments_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("VTE_Assesments_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Assesment" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_VTE_InsertUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_VTE_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_VTE_Form" runat="server" OnInserted="SqlDataSource_VTE_Form_Inserted" OnUpdated="SqlDataSource_VTE_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableAssesment" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_AssesmentHeading" runat="server" Text="" EnableViewState="false"></asp:Label>
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
                      <asp:GridView ID="GridView_VTE_Assesments" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_VTE_Assesments" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_VTE_Assesments_PreRender" OnDataBound="GridView_VTE_Assesments_DataBound" OnRowCreated="GridView_VTE_Assesments_RowCreated">
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
                              <td>of <%=GridView_VTE_Assesments.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Assesment" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Assesment" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("VTE_Assesments_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="VTE_Assesments_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="VTE_Assesments_ReportNumber" />
                          <asp:BoundField DataField="VTE_Assesments_Date" HeaderText="Evaluation Date" ReadOnly="True" SortExpression="VTE_Assesments_Date" />
                          <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                          <asp:BoundField DataField="VTE_Assesments_BRF_Score" HeaderText="BRF Score" ReadOnly="True" SortExpression="VTE_Assesments_BRF_Score" />
                          <asp:BoundField DataField="VTE_Assesments_PRF_Score" HeaderText="PRF Score" ReadOnly="True" SortExpression="VTE_Assesments_PRF_Score" />
                          <asp:BoundField DataField="VTE_Assesments_RFS_Score" HeaderText="RFS Score" ReadOnly="True" SortExpression="VTE_Assesments_RFS_Score" />
                          <asp:BoundField DataField="VTE_Assesments_RFS_Level" HeaderText="RFS Level" ReadOnly="True" SortExpression="VTE_Assesments_RFS_Level" />
                          <asp:BoundField DataField="VTE_Assesments_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="VTE_Assesments_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_VTE_Assesments" runat="server" OnSelected="SqlDataSource_VTE_Assesments_Selected"></asp:SqlDataSource>
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
