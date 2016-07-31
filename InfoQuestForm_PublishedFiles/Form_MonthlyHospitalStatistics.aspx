<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_MonthlyHospitalStatistics" CodeBehind="Form_MonthlyHospitalStatistics.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Monthly Hospital Statistics</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MonthlyHospitalStatistics.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Calculation_HABSI_Percentage();Calculation_CC_CCNReceivedPositive_Calculated();Calculation_CC_CCNReceivedSuggestions_Calculated();Calculation_CCNReceivedNegative_Calculated();Calculation_CC_ComplaintsReceived_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();">
  <form id="form_MHS" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MHS" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MHS" AssociatedUpdatePanelID="UpdatePanel_MHS">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MHS" runat="server">
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
          <table id="TableMHSInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_MHSInfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>
                      <strong>Facility:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MHSFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>Month:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MHSMonth" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>FY Period:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MHSFYPeriod" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableLinks" style="width: 900px;" runat="server">
            <tr>
              <td style="text-align: center;">
                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
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
                      <asp:Label ID="Label_MHSHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_MHS_Form" runat="server" DataKeyNames="MHS_Id" CssClass="FormView" DataSourceID="SqlDataSource_MHS_Form" DefaultMode="Edit" OnItemCommand="FormView_MHS_Form_ItemCommand" OnDataBound="FormView_MHS_Form_DataBound" OnItemUpdating="FormView_MHS_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Visits
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_TotalAdmissions" runat="server" Text='<%# Bind("MHS_OS_TotalAdmissions","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalAdmissionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">In Patient
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_InPatients" runat="server" Text='<%# Bind("MHS_OS_InPatients","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSInPatientsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Out Patient (Excluding Emergencies)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_OutPatients" runat="server" Text='<%# Bind("MHS_OS_OutPatients","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSOutPatientsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Accident and Emergencies
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_AE" runat="server" Text='<%# Bind("MHS_OS_AE","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSAEInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Births Normal
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_BirthsNormal" runat="server" Text='<%# Bind("MHS_OS_BirthsNormal","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBirthsNormalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Births Caesarean
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_BirthsCaesarean" runat="server" Text='<%# Bind("MHS_OS_BirthsCaesarean","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBirthsCaesareanInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Theatre Time (Min)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_TheatreTime" runat="server" Text='<%# Bind("MHS_OS_TheatreTime","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTheatreTimeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_TotalTheatreCases" runat="server" Text='<%# Bind("MHS_OS_TotalTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Major Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_MajorTheatreCases" runat="server" Text='<%# Bind("MHS_OS_MajorTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSMajorTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Minor Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_MinorTheatreCases" runat="server" Text='<%# Bind("MHS_OS_MinorTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSMinorTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Theatre Cases for SSI (Excluding Endoscopes)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_TheatreCasesSSI" runat="server" Text='<%# Bind("MHS_OS_TheatreCasesSSI","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTheatreCasesSSIInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Labour Hours
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_LabourHours" runat="server" Width="150px" Text='<%# Bind("MHS_OS_LabourHours","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_LabourHours" runat="server" TargetControlID="TextBox_EditOS_LabourHours" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_LabourHours" runat="server" Text='<%# Bind("MHS_OS_LabourHours","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSLabourHoursInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Active Beds
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_BedsActive" runat="server" Text='<%# Bind("MHS_OS_BedsActive","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBedsActiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Registered Beds
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_BedsRegistered" runat="server" Text='<%# Bind("MHS_OS_BedsRegistered","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBedsRegisteredInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Hospital PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_TotalHospital_PPD" runat="server" Text='<%# Bind("MHS_OS_TotalHospital_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalHospitalPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">ICU, HC PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_ICUHC_PPD" runat="server" Text='<%# Bind("MHS_OS_ICUHC_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSICUHCPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">NNICU PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_NNICU_PPD" runat="server" Text='<%# Bind("MHS_OS_NNICU_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSNNICUPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Central Line Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_CentralLineDays" runat="server" Width="150px" Text='<%# Bind("MHS_OS_CentralLineDays","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_CentralLineDays" runat="server" TargetControlID="TextBox_EditOS_CentralLineDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_CentralLineDays" runat="server" Text='<%# Bind("MHS_OS_CentralLineDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCentralLineDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Uretheral Catheter Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_CatheterDays" runat="server" Width="150px" Text='<%# Bind("MHS_OS_CatheterDays","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_CatheterDays" runat="server" TargetControlID="TextBox_EditOS_CatheterDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_CatheterDays" runat="server" Text='<%# Bind("MHS_OS_CatheterDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCatheterDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Esidimeni Number of HAI's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_EsidimeniHAITotal" runat="server" Width="150px" Text='<%# Bind("MHS_OS_EsidimeniHAITotal","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_EsidimeniHAITotal" runat="server" TargetControlID="TextBox_EditOS_EsidimeniHAITotal" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_EsidimeniHAITotal" runat="server" Text='<%# Bind("MHS_OS_EsidimeniHAITotal","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSEsidimeniHAITotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">VAP Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_VAPDays" runat="server" Width="150px" Text='<%# Bind("MHS_OS_VAPDays","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_VAPDays" runat="server" TargetControlID="TextBox_EditOS_VAPDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_VAPDays" runat="server" Text='<%# Bind("MHS_OS_VAPDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSVAPDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">HAI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_HAIRate" runat="server" Text='<%# Bind("MHS_OS_HAIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSHAIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">VAP Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_VAPRate" runat="server" Text='<%# Bind("MHS_OS_VAPRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSVAPRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">SSI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_SSIRate" runat="server" Text='<%# Bind("MHS_OS_SSIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSSSIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">CAUTI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_CAUTIRate" runat="server" Text='<%# Bind("MHS_OS_CAUTIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCAUTIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">CLABSI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_CLABSIRate" runat="server" Text='<%# Bind("MHS_OS_CLABSIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCLABSIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Spotlight on cleaning
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOS_SpotlightOnCleaning" runat="server" Width="150px" Text='<%# Bind("MHS_OS_SpotlightOnCleaning","{0:#,##0.00}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOS_SpotlightOnCleaning" runat="server" TargetControlID="TextBox_EditOS_SpotlightOnCleaning" FilterType="Custom, Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOS_SpotlightOnCleaning" runat="server" Text='<%# Bind("MHS_OS_SpotlightOnCleaning","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSSpotlightOnCleaningInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">DSO Internal
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_DSOInternal" runat="server" Text='<%# Bind("MHS_OS_DSOInternal","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOInternalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">DSO External
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_DSOExternal" runat="server" Text='<%# Bind("MHS_OS_DSOExternal","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOExternalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">DSO External Excluding COID
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOS_DSOExternalExcludingCOID" runat="server" Text='<%# Bind("MHS_OS_DSOExternalExcludingCOID","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOExternalExcludingCOIDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Hospital Associated Bloodstream Infections (HA BSI)
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total number of HA BSI cultured with any Staphylococcus species
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditHABSI_MSRA" runat="server" Width="150px" Text='<%# Bind("MHS_HABSI_MSRA","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditHABSI_MSRA" runat="server" TargetControlID="TextBox_EditHABSI_MSRA" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditHABSI_MSRA" runat="server" Text='<%# Bind("MHS_HABSI_MSRA","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIMSRAInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Number of HA BSI Cultured with MRSA
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditHABSI_MRSA" runat="server" Width="150px" Text='<%# Bind("MHS_HABSI_MRSA","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditHABSI_MRSA" runat="server" TargetControlID="TextBox_EditHABSI_MRSA" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditHABSI_MRSA" runat="server" Text='<%# Bind("MHS_HABSI_MRSA","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIMRSAInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">MRSA as a % of Total HA BSI
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditHABSI_Percentage" runat="server" ReadOnly="true" Width="150px" Text='<%# Bind("MHS_HABSI_Percentage","{0:###0.00}") %>' CssClass="Controls_TextBox_CalculationAlignRight_AlternatingRowStyle"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditHABSI_Percentage" runat="server" TargetControlID="TextBox_EditHABSI_Percentage" FilterType="Custom, Numbers" ValidChars=".">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditHABSI_Percentage" runat="server" Text='<%# Bind("MHS_HABSI_Percentage","{0:#,##0.00}") %>'></asp:Label>&nbsp;%
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIPercentageInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Colonisations
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Colonisations
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOther_Colonisations" runat="server" Text='<%# Bind("MHS_Other_Colonisations","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherColonisationsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">TB Statistics
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Patients Cases Total
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOther_TBPatents_Cases_Clinical" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_Clinical","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesClinicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Patients Cases (MDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOther_TBPatents_Cases_MDR" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_MDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesMDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Patients Cases (XDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_EditOther_TBPatents_Cases_XDR" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_XDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesXDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Staff Cases Total
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOther_TBStaff_Cases_Clinical" runat="server" Width="150px" Text='<%# Bind("MHS_Other_TBStaff_Cases_Clinical","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOther_TBStaff_Cases_Clinical" runat="server" TargetControlID="TextBox_EditOther_TBStaff_Cases_Clinical" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOther_TBStaff_Cases_Clinical" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_Clinical","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesClinicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Staff Cases (MDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOther_TBStaff_Cases_MDR" runat="server" Width="150px" Text='<%# Bind("MHS_Other_TBStaff_Cases_MDR","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOther_TBStaff_Cases_MDR" runat="server" TargetControlID="TextBox_EditOther_TBStaff_Cases_MDR" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOther_TBStaff_Cases_MDR" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_MDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesMDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Staff Cases (XDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOther_TBStaff_Cases_XDR" runat="server" Width="150px" Text='<%# Bind("MHS_Other_TBStaff_Cases_XDR","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOther_TBStaff_Cases_XDR" runat="server" TargetControlID="TextBox_EditOther_TBStaff_Cases_XDR" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOther_TBStaff_Cases_XDR" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_XDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesXDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Customer Care
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" style="padding: 0px;">
                          <table>
                            <tr>
                              <td style="width: 290px;"></td>
                              <td style="width: 186px; text-align: right;">Published</td>
                              <td style="width: 185px; text-align: right;">Captured</td>
                              <td style="width: 185px; text-align: right;">Calculated</td>
                              <td style="width: 14px;"></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Positive</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedPositive","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedPositiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Suggestions</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedSuggestionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Negative (P3)</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedNegative","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedNegativeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Comment Cards-Number Received Total</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedTotal" ReadOnly="true" runat="server" Width="160px" Text='<%# Bind("MHS_CC_CCNReceivedTotal","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight_AlternatingRowStyle"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedTotal" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedTotal" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedTotal" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedTotal","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedTotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Complaints Received at Hospital level P1 and P2</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_ComplaintsReceived_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived_Published" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived_Published" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_ComplaintsReceived","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MHS_CC_ComplaintsReceived_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived_Calculated" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived_Calculated" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCComplaintsReceivedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Total Complaints</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_TotalComplaints" ReadOnly="true" runat="server" Width="160px" Text='<%# Bind("MHS_CC_TotalComplaints","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight_AlternatingRowStyle"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_TotalComplaints" runat="server" TargetControlID="TextBox_EditCC_TotalComplaints" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_TotalComplaints" runat="server" Text='<%# Bind("MHS_CC_TotalComplaints","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCTotalComplaintsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Care
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total number of staff trained for the month
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditCare_TotalStaffTrained" runat="server" Width="150px" Text='<%# Bind("MHS_Care_TotalStaffTrained","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCare_TotalStaffTrained" runat="server" TargetControlID="TextBox_EditCare_TotalStaffTrained" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditCare_TotalStaffTrained" runat="server" Text='<%# Bind("MHS_Care_TotalStaffTrained","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffTrainedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Form Information
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("MHS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("MHS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModified" runat="server" Text='<%# (bool)(Eval("MHS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedBy" runat="server" Text='<%# Bind("MHS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedDate" runat="server" Text='<%# Bind("MHS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print Statistics" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                    <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                    <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Statistics" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back to List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td style="width: 300px;">Total Visits
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_TotalAdmissions" runat="server" Text='<%# Bind("MHS_OS_TotalAdmissions","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalAdmissionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">In Patient
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_InPatients" runat="server" Text='<%# Bind("MHS_OS_InPatients","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSInPatientsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Out Patient (Excluding Emergencies)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_OutPatients" runat="server" Text='<%# Bind("MHS_OS_OutPatients","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSOutPatientsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Accident and Emergencies
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_AE" runat="server" Text='<%# Bind("MHS_OS_AE","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSAEInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Births Normal
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_BirthsNormal" runat="server" Text='<%# Bind("MHS_OS_BirthsNormal","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBirthsNormalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Births Caesarean
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_BirthsCaesarean" runat="server" Text='<%# Bind("MHS_OS_BirthsCaesarean","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBirthsCaesareanInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Theatre Time (Min)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_TheatreTime" runat="server" Text='<%# Bind("MHS_OS_TheatreTime","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTheatreTimeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_TotalTheatreCases" runat="server" Text='<%# Bind("MHS_OS_TotalTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Major Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_MajorTheatreCases" runat="server" Text='<%# Bind("MHS_OS_MajorTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSMajorTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Minor Theatre Cases
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_MinorTheatreCases" runat="server" Text='<%# Bind("MHS_OS_MinorTheatreCases","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSMinorTheatreCasesInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Theatre Cases for SSI (Excluding Endoscopes)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_TheatreCasesSSI" runat="server" Text='<%# Bind("MHS_OS_TheatreCasesSSI","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTheatreCasesSSIInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Labour Hours
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_LabourHours" runat="server" Text='<%# Bind("MHS_OS_LabourHours","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSLabourHoursInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Active Beds
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_BedsActive" runat="server" Text='<%# Bind("MHS_OS_BedsActive","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBedsActiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Registered Beds
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_BedsRegistered" runat="server" Text='<%# Bind("MHS_OS_BedsRegistered","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSBedsRegisteredInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Hospital PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_TotalHospital_PPD" runat="server" Text='<%# Bind("MHS_OS_TotalHospital_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSTotalHospitalPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">ICU, HC PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_ICUHC_PPD" runat="server" Text='<%# Bind("MHS_OS_ICUHC_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSICUHCPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">NNICU PPD's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_NNICU_PPD" runat="server" Text='<%# Bind("MHS_OS_NNICU_PPD","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSNNICUPPDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Central Line Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_CentralLineDays" runat="server" Text='<%# Bind("MHS_OS_CentralLineDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCentralLineDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Uretheral Catheter Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_CatheterDays" runat="server" Text='<%# Bind("MHS_OS_CatheterDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCatheterDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Esidimeni Number of HAI's
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_EsidimeniHAITotal" runat="server" Text='<%# Bind("MHS_OS_EsidimeniHAITotal","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSEsidimeniHAITotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">VAP Days
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_VAPDays" runat="server" Text='<%# Bind("MHS_OS_VAPDays","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSVAPDaysInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">HAI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_HAIRate" runat="server" Text='<%# Bind("MHS_OS_HAIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSHAIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">VAP Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_VAPRate" runat="server" Text='<%# Bind("MHS_OS_VAPRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSVAPRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">SSI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_SSIRate" runat="server" Text='<%# Bind("MHS_OS_SSIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSSSIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">CAUTI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_CAUTIRate" runat="server" Text='<%# Bind("MHS_OS_CAUTIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCAUTIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">CLABSI Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_CLABSIRate" runat="server" Text='<%# Bind("MHS_OS_CLABSIRate","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSCLABSIRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Spotlight on cleaning
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_SpotlightOnCleaning" runat="server" Text='<%# Bind("MHS_OS_SpotlightOnCleaning","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSSpotlightOnCleaningInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">DSO Internal
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_DSOInternal" runat="server" Text='<%# Bind("MHS_OS_DSOInternal","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOInternalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">DSO External
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_DSOExternal" runat="server" Text='<%# Bind("MHS_OS_DSOExternal","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOExternalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">DSO External Excluding COID
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOS_DSOExternalExcludingCOID" runat="server" Text='<%# Bind("MHS_OS_DSOExternalExcludingCOID","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OSDSOExternalExcludingCOIDInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Hospital Associated Bloodstream Infections (HA BSI)
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total number of HA BSI cultured with any Staphylococcus species
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemHABSI_MSRA" runat="server" Text='<%# Bind("MHS_HABSI_MSRA","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIMSRAInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Number of HA BSI Cultured with MRSA
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemHABSI_MRSA" runat="server" Text='<%# Bind("MHS_HABSI_MRSA","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIMRSAInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">MRSA as a % of Total HA BSI
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemHABSI_Percentage" runat="server" Text='<%# Bind("MHS_HABSI_Percentage","{0:#,##0.00}") %>'></asp:Label>&nbsp;%
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_HABSIPercentageInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Colonisations
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Colonisations
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_Colonisations" runat="server" Text='<%# Bind("MHS_Other_Colonisations","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherColonisationsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">TB Statistics
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Patients Cases Total
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBPatents_Cases_Clinical" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_Clinical","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesClinicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Patients Cases (MDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBPatents_Cases_MDR" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_MDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesMDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Patients Cases (XDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBPatents_Cases_XDR" runat="server" Text='<%# Bind("MHS_Other_TBPatents_Cases_XDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBPatentsCasesXDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Staff Cases Total
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBStaff_Cases_Clinical" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_Clinical","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesClinicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">TB Staff Cases (MDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBStaff_Cases_MDR" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_MDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesMDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">TB Staff Cases (XDR)
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemOther_TBStaff_Cases_XDR" runat="server" Text='<%# Bind("MHS_Other_TBStaff_Cases_XDR","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OtherTBStaffCasesXDRInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Customer Care
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" style="padding: 0px;">
                          <table>
                            <tr>
                              <td style="width: 290px;"></td>
                              <td style="width: 186px; text-align: right;">Published</td>
                              <td style="width: 185px; text-align: right;">Captured</td>
                              <td style="width: 185px; text-align: right;">Calculated</td>
                              <td style="width: 14px;"></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Positive</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedPositive_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedPositiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Suggestions</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedSuggestions_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedSuggestionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Negative (P3)</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative_Published" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative_Calculated" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedNegative_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedNegativeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Comment Cards-Number Received Total</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedTotal" runat="server" Text='<%# Bind("MHS_CC_CCNReceivedTotal","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedTotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Complaints Received at Hospital level P1 and P2</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived_Published" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived_Calculated" runat="server" Text='<%# Bind("MHS_CC_ComplaintsReceived_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCComplaintsReceivedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Total Complaints</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:Label ID="Label_ItemCC_TotalComplaints" runat="server" Text='<%# Bind("MHS_CC_TotalComplaints","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCTotalComplaintsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Care
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total number of staff trained for the month
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:Label ID="Label_ItemCare_TotalStaffTrained" runat="server" Text='<%# Bind("MHS_Care_TotalStaffTrained","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffTrainedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        &nbsp;&nbsp;&nbsp;</td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Form Information
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("MHS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("MHS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModified" runat="server" Text='<%# (bool)(Eval("MHS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedBy" runat="server" Text='<%# Bind("MHS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedDate" runat="server" Text='<%# Bind("MHS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Statistics" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back to List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_MHS_Form" runat="server" OnUpdated="SqlDataSource_MHS_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div>
            &nbsp;
          </div>
          <table id="TableOrganisms" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridOrganisms" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_TotalRecordsOrganisms" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_MHS_Organisms" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_MHS_Organisms" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_MHS_Organisms_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="text-align: left;">&nbsp;</td>
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
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="MHS_Organisms_Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="MHS_Organisms_Description" />
                          <asp:BoundField DataField="MHS_Organisms_Value" HeaderText="Value" HeaderStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="MHS_Organisms_Value" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_MHS_Organisms" runat="server" OnSelected="SqlDataSource_MHS_Organisms_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <%--<div>
            &nbsp;
          </div>
          <div>
            &nbsp;
          </div>
          <table id="TableWaste" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridWaste" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_TotalRecordsWaste" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_MHS_Waste" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_MHS_Waste" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_MHS_Waste_PreRender" OnRowCreated="GridView_MHS_Waste_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle" style="width: 100%">
                            <tr>
                              <td>
                                <asp:Button ID="Button_UpdateWaste" runat="server" Text="Update Waste" CssClass="Controls_Button" OnClick="Button_UpdateWaste" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>
                                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="MHS_Waste_Identifier_Name" HeaderText="Identifier" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="MHS_Waste_Identifier_Name" />
                          <asp:BoundField DataField="MHS_Waste_Identifier_Parent" HeaderText="Waste" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="MHS_Waste_Identifier_Parent" />
                          <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="MHS_Waste_Value">
                            <ItemTemplate>
                              <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                              <asp:TextBox ID="TextBox_WasteValue" runat="server" Width="150px" Text='<%# Bind("MHS_Waste_Value","{0:#,##0.00}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                              <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_WasteValue" runat="server" TargetControlID="TextBox_WasteValue" FilterType="Custom,Numbers" ValidChars=".">
                              </Ajax:FilteredTextBoxExtender>
                              <asp:Label ID="Label_WasteValue" runat="server" Text='<%# Bind("MHS_Waste_Value","{0:#,##0.00}") %>'></asp:Label>
                              <asp:HiddenField ID="HiddenField_EditWasteId" runat="server" Value='<%# Bind("MHS_Waste_Id") %>' />&nbsp;kg&nbsp;
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="MHS_Waste_PPD" HeaderText="PPD" HeaderStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="MHS_Waste_PPD" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_MHS_Waste" runat="server" OnSelected="SqlDataSource_MHS_Waste_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TableWasteList" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridWasteList" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_TotalRecordsWasteList" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_MHS_Waste_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_MHS_Waste_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_MHS_Waste_List_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>
                                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="MHS_Waste_Identifier_Name" HeaderText="Identifier" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="MHS_Waste_Identifier_Name" />
                          <asp:BoundField DataField="MHS_Waste_Identifier_Parent" HeaderText="Waste" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="MHS_Waste_Identifier_Parent" />
                          <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="MHS_Waste_Value">
                            <ItemTemplate>
                              <asp:Label ID="Label_WasteValue" runat="server" Text='<%# Bind("MHS_Waste_Value","{0:#,##0.00}") %>'></asp:Label>&nbsp;kg&nbsp;
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="MHS_Waste_PPD" HeaderText="PPD" HeaderStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="MHS_Waste_PPD" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_MHS_Waste_List" runat="server" OnSelected="SqlDataSource_MHS_Waste_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>--%>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
