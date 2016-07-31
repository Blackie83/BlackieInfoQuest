<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_FIMFAM" CodeBehind="Form_FIMFAM.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - FIM / FAM</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_FIMFAM.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_FIMFAM" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_FIMFAM" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_FIMFAM" AssociatedUpdatePanelID="UpdatePanel_FIMFAM">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_FIMFAM" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Rehabilitation/65_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_FIMFAM_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_FIMFAM_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
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
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TablePatientInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_PatientInfoHeading" runat="server" Text=""></asp:Label>
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
                    <td>
                      <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Visit Number:
                    </td>
                    <td>
                      <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Surname, Name:
                    </td>
                    <td>
                      <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Age:
                    </td>
                    <td>
                      <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Date of Admission:
                    </td>
                    <td>
                      <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Date of Discharge:
                    </td>
                    <td>
                      <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Ailments:
                    </td>
                    <td>
                      <asp:BulletedList ID="BulletedList_PAAilments" runat="server">
                      </asp:BulletedList>
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
          <table id="TableForm" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_ElementsHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_FIMFAM_Form" runat="server" DataKeyNames="FIMFAM_Elements_Id" CssClass="FormView" DataSourceID="SqlDataSource_FIMFAM_Form" OnItemInserting="FormView_FIMFAM_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_FIMFAM_Form_ItemCommand" OnDataBound="FormView_FIMFAM_Form_DataBound" OnItemUpdating="FormView_FIMFAM_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="3" style="width: 730px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("FIMFAM_Elements_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td id="FormObservationDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertObservationDate" runat="server" Width="75px" Text='<%# Bind("FIMFAM_Elements_ObservationDate","{0:yyyy/MM/dd}") %>' AutoPostBack="True" OnTextChanged="TextBox_InsertObservationDate_TextChanged" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertObservationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertObservationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertObservationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertObservationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertObservationDate" runat="server" TargetControlID="TextBox_InsertObservationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormOnsetDate">Onset Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertOnsetDate" runat="server"></asp:Label>
                          <asp:TextBox ID="TextBox_InsertOnsetDate" runat="server" Width="75px" Text='<%# Bind("FIMFAM_Elements_OnsetDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertOnsetDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertOnsetDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertOnsetDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertOnsetDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertOnsetDate" runat="server" TargetControlID="TextBox_InsertOnsetDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Selfcare</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareEating">Eating
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareEating" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Eating")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareGrooming">Grooming
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareGrooming" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Grooming")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareBathing">Bathing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareBathing" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Bathing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingUpper">Dressing Upper
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareDressingUpper" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_DressingUpper")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingLower">Dressing Lower
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareDressingLower" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_DressingLower")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareToileting">Toileting
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareToileting" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Toileting")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareSwallowing">Swallowing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSelfcareSwallowing" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Swallowing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Sphincter</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder1">Bladder I: Assistance
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSphincterBladder1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bladder1")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder2">Bladder II: Frequency
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSphincterBladder2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bladder2")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel1">Bowel I: Assistance
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSphincterBowel1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bowel1")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel2">Bowel II: Frequency
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertSphincterBowel2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bowel2")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Transfer</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferBCW">Bed, Chair, Wheelchair
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertTransferBCW" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_BCW")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferToilet">Toilet
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertTransferToilet" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_Toilet")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferTS">Tub, Shower
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertTransferTS" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_TS")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferCarTransfer">Car Transfer
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertTransferCarTransfer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_CarTransfer")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Locomotion</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionWW">Walk / Wheelchair
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertLocomotionWW" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_WW")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionStairs">Stairs
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertLocomotionStairs" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_Stairs")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionCommunityAccess">Community Access
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertLocomotionCommunityAccess" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_CommunityAccess")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>Motor FIM + FAM Subscore</td>
                        <td>
                          <asp:TextBox ID="Textbox_InsertMotorSubScore" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_MotorSubScore") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 112</strong>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Communication</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationAV">Comprehension Audio / Visual
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCommunicationAV" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_AV")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationVN">Expression Verbal / Non-Verbal
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCommunicationVN" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_VN")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationReading">Reading
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCommunicationReading" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Reading")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationWriting">Writing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCommunicationWriting" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Writing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationSpeech">Speech Intelligibility
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCommunicationSpeech" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Speech")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Psych / Soc Adjust</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustSocialInteraction">Social Interaction
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPSAdjustSocialInteraction" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_SocialInteraction")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmotionalStatus">Emotional Status
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPSAdjustEmotionalStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_EmotionalStatus")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustAdjustment">Adjustment to Limitations
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPSAdjustAdjustment" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_Adjustment")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmployability">Employability
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertPSAdjustEmployability" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_Employability")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Cognitive Function</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionProblemSolving">Problem Solving
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCognitiveFunctionProblemSolving" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_ProblemSolving")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionMemory">Memory
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCognitiveFunctionMemory" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Memory")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionOrientation">Orientation
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCognitiveFunctionOrientation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Orientation")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionAttention">Attention
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCognitiveFunctionAttention" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Attention")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionSafetyJudgement">Safety Judgement
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_InsertCognitiveFunctionSafetyJudgement" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_SafetyJudgement")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td>Cognitive FIM + FAM Subscore</td>
                        <td>
                          <asp:TextBox ID="Textbox_InsertCognitiveSubScore" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveSubScore") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 98</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Total FIM + FAM Score</td>
                        <td>
                          <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_Total") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 210</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("FIMFAM_Elements_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add FIM / FAM" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("FIMFAM_Elements_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormObservationDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditObservationDate" runat="server" Width="75px" Text='<%# Bind("FIMFAM_Elements_ObservationDate","{0:yyyy/MM/dd}") %>' AutoPostBack="True" OnTextChanged="TextBox_EditObservationDate_TextChanged" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditObservationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditObservationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditObservationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditObservationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditObservationDate" runat="server" TargetControlID="TextBox_EditObservationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormOnsetDate">Onset Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_EditOnsetDate" runat="server"></asp:Label>
                          <asp:TextBox ID="TextBox_EditOnsetDate" runat="server" Width="75px" Text='<%# Bind("FIMFAM_Elements_OnsetDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditOnsetDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditOnsetDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditOnsetDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditOnsetDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditOnsetDate" runat="server" TargetControlID="TextBox_EditOnsetDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Selfcare</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareEating">Eating
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareEating" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Eating")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareGrooming">Grooming
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareGrooming" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Grooming")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareBathing">Bathing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareBathing" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Bathing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingUpper">Dressing Upper
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareDressingUpper" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_DressingUpper")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingLower">Dressing Lower
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareDressingLower" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_DressingLower")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareToileting">Toileting
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareToileting" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Toileting")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareSwallowing">Swallowing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSelfcareSwallowing" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Selfcare_Swallowing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Sphincter</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder1">Bladder I: Assistance
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSphincterBladder1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bladder1")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder2">Bladder II: Frequency
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSphincterBladder2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bladder2")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel1">Bowel I: Assistance
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSphincterBowel1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bowel1")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel2">Bowel II: Frequency
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditSphincterBowel2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Sphincter_Bowel2")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Transfer</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferBCW">Bed, Chair, Wheelchair
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditTransferBCW" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_BCW")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferToilet">Toilet
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditTransferToilet" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_Toilet")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferTS">Tub, Shower
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditTransferTS" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_TS")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferCarTransfer">Car Transfer
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditTransferCarTransfer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Transfer_CarTransfer")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Locomotion</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionWW">Walk / Wheelchair
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditLocomotionWW" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_WW")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionStairs">Stairs
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditLocomotionStairs" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_Stairs")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionCommunityAccess">Community Access
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditLocomotionCommunityAccess" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Locomotion_CommunityAccess")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>Motor FIM + FAM Subscore</td>
                        <td>
                          <asp:TextBox ID="Textbox_EditMotorSubScore" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_MotorSubScore") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 112</strong>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Communication</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationAV">Comprehension Audio / Visual
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCommunicationAV" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_AV")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationVN">Expression Verbal / Non-Verbal
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCommunicationVN" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_VN")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationReading">Reading
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCommunicationReading" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Reading")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationWriting">Writing
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCommunicationWriting" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Writing")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationSpeech">Speech Intelligibility
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCommunicationSpeech" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_Communication_Speech")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Psych / Soc Adjust</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustSocialInteraction">Social Interaction
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPSAdjustSocialInteraction" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_SocialInteraction")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmotionalStatus">Emotional Status
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPSAdjustEmotionalStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_EmotionalStatus")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustAdjustment">Adjustment to Limitations
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPSAdjustAdjustment" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_Adjustment")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmployability">Employability
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditPSAdjustEmployability" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_PSAdjust_Employability")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Cognitive Function</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionProblemSolving">Problem Solving
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCognitiveFunctionProblemSolving" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_ProblemSolving")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionMemory">Memory
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCognitiveFunctionMemory" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Memory")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionOrientation">Orientation
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCognitiveFunctionOrientation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Orientation")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionAttention">Attention
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCognitiveFunctionAttention" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_Attention")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionSafetyJudgement">Safety Judgement
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditCognitiveFunctionSafetyJudgement" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" SelectedValue='<%# Bind("FIMFAM_Elements_CognitiveFunction_SafetyJudgement")%>'>
                            <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                          </asp:RadioButtonList></td>
                      </tr>
                      <tr>
                        <td>Cognitive FIM + FAM Subscore</td>
                        <td>
                          <asp:TextBox ID="Textbox_EditCognitiveSubScore" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveSubScore") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 98</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Total FIM + FAM Score</td>
                        <td>
                          <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("FIMFAM_Elements_Total") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 210</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("FIMFAM_Elements_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print FIM / FAM" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update FIM / FAM" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                        <td colspan="3" style="width: 730px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("FIMFAM_Elements_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormObservationDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemObservationDate" runat="server" Text='<%# Bind("FIMFAM_Elements_ObservationDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormOnsetDate">Onset Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemOnsetDate" runat="server" Text='<%# Bind("FIMFAM_Elements_OnsetDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Selfcare</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareEating">Eating
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareEating" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_Eating") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareGrooming">Grooming
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareGrooming" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_Grooming") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareBathing">Bathing
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareBathing" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_Bathing") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingUpper">Dressing Upper
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareDressingUpper" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_DressingUpper") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareDressingLower">Dressing Lower
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareDressingLower" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_DressingLower") %>'></asp:Label>&nbsp;</td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareToileting">Toileting
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareToileting" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_Toileting") %>'></asp:Label>&nbsp;</td>
                      </tr>
                      <tr>
                        <td id="FormSelfcareSwallowing">Swallowing
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSelfcareSwallowing" runat="server" Text='<%# Bind("FIMFAM_Elements_Selfcare_Swallowing") %>'></asp:Label>&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Sphincter</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder1">Bladder I: Assistance
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSphincterBladder1" runat="server" Text='<%# Bind("FIMFAM_Elements_Sphincter_Bladder1") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBladder2">Bladder II: Frequency
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSphincterBladder2" runat="server" Text='<%# Bind("FIMFAM_Elements_Sphincter_Bladder2") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel1">Bowel I: Assistance
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSphincterBowel1" runat="server" Text='<%# Bind("FIMFAM_Elements_Sphincter_Bowel1") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSphincterBowel2">Bowel II: Frequency
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSphincterBowel2" runat="server" Text='<%# Bind("FIMFAM_Elements_Sphincter_Bowel2") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Transfer</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferBCW">Bed, Chair, Wheelchair
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTransferBCW" runat="server" Text='<%# Bind("FIMFAM_Elements_Transfer_BCW") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferToilet">Toilet
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTransferToilet" runat="server" Text='<%# Bind("FIMFAM_Elements_Transfer_Toilet") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferTS">Tub, Shower
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTransferTS" runat="server" Text='<%# Bind("FIMFAM_Elements_Transfer_TS") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormTransferCarTransfer">Car Transfer
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTransferCarTransfer" runat="server" Text='<%# Bind("FIMFAM_Elements_Transfer_CarTransfer") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Locomotion</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionWW">Walk / Wheelchair
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemLocomotionWW" runat="server" Text='<%# Bind("FIMFAM_Elements_Locomotion_WW") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionStairs">Stairs
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemLocomotionStairs" runat="server" Text='<%# Bind("FIMFAM_Elements_Locomotion_Stairs") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormLocomotionCommunityAccess">Community Access
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemLocomotionCommunityAccess" runat="server" Text='<%# Bind("FIMFAM_Elements_Locomotion_CommunityAccess")%>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Motor FIM + FAM Subscore</td>
                        <td>
                          <asp:Label ID="Label_ItemMotorSubScore" runat="server" Text='<%# Bind("FIMFAM_Elements_MotorSubScore") %>'></asp:Label><strong> out of 112</strong>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Communication</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationAV">Comprehension Audio / Visual
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCommunicationAV" runat="server" Text='<%# Bind("FIMFAM_Elements_Communication_AV") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationVN">Expression Verbal / Non-Verbal
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCommunicationVN" runat="server" Text='<%# Bind("FIMFAM_Elements_Communication_VN") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationReading">Reading
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCommunicationReading" runat="server" Text='<%# Bind("FIMFAM_Elements_Communication_Reading") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationWriting">Writing
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCommunicationWriting" runat="server" Text='<%# Bind("FIMFAM_Elements_Communication_Writing") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCommunicationSpeech">Speech Intelligibility
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCommunicationSpeech" runat="server" Text='<%# Bind("FIMFAM_Elements_Communication_Speech") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Psych / Soc Adjust</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustSocialInteraction">Social Interaction
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemPSAdjustSocialInteraction" runat="server" Text='<%# Bind("FIMFAM_Elements_PSAdjust_SocialInteraction") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmotionalStatus">Emotional Status
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemPSAdjustEmotionalStatus" runat="server" Text='<%# Bind("FIMFAM_Elements_PSAdjust_EmotionalStatus") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustAdjustment">Adjustment to Limitations
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemPSAdjustAdjustment" runat="server" Text='<%# Bind("FIMFAM_Elements_PSAdjust_Adjustment") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormPSAdjustEmployability">Employability
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemPSAdjustEmployability" runat="server" Text='<%# Bind("FIMFAM_Elements_PSAdjust_Employability") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"><strong>Cognitive Function</strong>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionProblemSolving">Problem Solving
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveFunctionProblemSolving" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveFunction_ProblemSolving") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionMemory">Memory
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveFunctionMemory" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveFunction_Memory") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionOrientation">Orientation
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveFunctionOrientation" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveFunction_Orientation") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionAttention">Attention
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveFunctionAttention" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveFunction_Attention") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td id="FormCognitiveFunctionSafetyJudgement">Safety Judgement
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveFunctionSafetyJudgement" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveFunction_SafetyJudgement") %>'></asp:Label></td>
                      </tr>
                      <tr>
                        <td>Cognitive FIM + FAM Subscore</td>
                        <td>
                          <asp:Label ID="Label_ItemCognitiveSubScore" runat="server" Text='<%# Bind("FIMFAM_Elements_CognitiveSubScore") %>'></asp:Label><strong> out of 98</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Total FIM + FAM Score</td>
                        <td>
                          <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("FIMFAM_Elements_Total") %>'></asp:Label><strong> out of 210</strong></td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("FIMFAM_Elements_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("FIMFAM_Elements_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print FIM / FAM" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_FIMFAM_Form" runat="server" OnInserted="SqlDataSource_FIMFAM_Form_Inserted" OnUpdated="SqlDataSource_FIMFAM_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableList" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
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
                    <td>
                      <asp:GridView ID="GridView_FIMFAM_Elements" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_FIMFAM_Elements" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_FIMFAM_Elements_PreRender" OnDataBound="GridView_FIMFAM_Elements_DataBound" OnRowCreated="GridView_FIMFAM_Elements_RowCreated">
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
                              <td>of
                          <%=GridView_FIMFAM_Elements.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New FIM / FAM" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New FIM / FAM" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("FIMFAM_Elements_Id"),Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="FIMFAM_Elements_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="FIMFAM_Elements_ReportNumber" />
                          <asp:BoundField DataField="FIMFAM_Elements_ObservationDate" HeaderText="Observation Date" ReadOnly="True" SortExpression="FIMFAM_Elements_ObservationDate" />
                          <asp:BoundField DataField="FIMFAM_Elements_Total" HeaderText="Total" ReadOnly="True" SortExpression="FIMFAM_Elements_Total" />
                          <asp:BoundField DataField="FIMFAM_Elements_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="FIMFAM_Elements_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_FIMFAM_Elements" runat="server" OnSelected="SqlDataSource_FIMFAM_Elements_Selected"></asp:SqlDataSource>
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
