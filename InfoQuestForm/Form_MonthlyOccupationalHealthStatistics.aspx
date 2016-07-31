<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_MonthlyOccupationalHealthStatistics.aspx.cs" Inherits="InfoQuestForm.Form_MonthlyOccupationalHealthStatistics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Monthly Occupational Health Statistics</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MonthlyOccupationalHealthStatistics.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_MOHS" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MOHS" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MOHS" AssociatedUpdatePanelID="UpdatePanel_MOHS">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MOHS" runat="server">
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
          <table id="TableMOHSInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_MOHSInfoHeading" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_MOHSFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>Unit:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MOHSUnit" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>Month:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MOHSMonth" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>FY Period:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MOHSFYPeriod" runat="server" Text=""></asp:Label>&nbsp;
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
                      <asp:Label ID="Label_MOHSHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_MOHS_Form" runat="server" DataKeyNames="MOHS_Id" CssClass="FormView" DataSourceID="SqlDataSource_MOHS_Form" DefaultMode="Edit" OnItemCommand="FormView_MOHS_Form_ItemCommand" OnDataBound="FormView_MOHS_Form_DataBound" OnItemUpdating="FormView_MOHS_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Total Clinic Visits
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOH_ClinicVisits" runat="server" Width="150px" Text='<%# Bind("MOHS_OH_ClinicVisits","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOH_ClinicVisits" runat="server" TargetControlID="TextBox_EditOH_ClinicVisits" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOH_ClinicVisits" runat="server" Text='<%# Bind("MOHS_OH_ClinicVisits","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHClinicVisitsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Labour Hours
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOH_LabourHours" runat="server" Width="150px" Text='<%# Bind("MOHS_OH_LabourHours","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOH_LabourHours" runat="server" TargetControlID="TextBox_EditOH_LabourHours" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOH_LabourHours" runat="server" Text='<%# Bind("MOHS_OH_LabourHours","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHLabourHoursInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Patient Satisfaction Score
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOH_PatientSatisfactionScore" runat="server" Width="150px" Text='<%# Bind("MOHS_OH_PatientSatisfactionScore","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOH_PatientSatisfactionScore" runat="server" TargetControlID="TextBox_EditOH_PatientSatisfactionScore" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOH_PatientSatisfactionScore" runat="server" Text='<%# Bind("MOHS_OH_PatientSatisfactionScore","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHPatientSatisfactionScoreInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Patient Satisfaction Response Rate
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditOH_PatientSatisfactionResponseRate" runat="server" Width="150px" Text='<%# Bind("MOHS_OH_PatientSatisfactionResponseRate","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditOH_PatientSatisfactionResponseRate" runat="server" TargetControlID="TextBox_EditOH_PatientSatisfactionResponseRate" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditOH_PatientSatisfactionResponseRate" runat="server" Text='<%# Bind("MOHS_OH_PatientSatisfactionResponseRate","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHPatientSatisfactionResponseRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
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
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedPositive","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedPositive_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedPositive_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedPositive_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedPositive_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedPositiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Suggestions</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedSuggestions_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedSuggestions_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedSuggestions_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedSuggestions_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedSuggestionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Negative (P3)</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative_Published" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedNegative","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedNegative_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedNegative_Calculated" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedNegative_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedNegative_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedNegativeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Comment Cards-Number Received Total</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_CCNReceivedTotal" ReadOnly="true" runat="server" Width="160px" Text='<%# Bind("MOHS_CC_CCNReceivedTotal","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight_AlternatingRowStyle"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_CCNReceivedTotal" runat="server" TargetControlID="TextBox_EditCC_CCNReceivedTotal" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_CCNReceivedTotal" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedTotal","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedTotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Complaints Received at Hospital level P1 and P2</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived_Published" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Published","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived_Published" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived_Published" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived_Published" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_ComplaintsReceived","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_ComplaintsReceived_Calculated" ReadOnly="true" runat="server" Width="140px" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Calculated","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_ComplaintsReceived_Calculated" runat="server" TargetControlID="TextBox_EditCC_ComplaintsReceived_Calculated" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_ComplaintsReceived_Calculated" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt">
                                <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCComplaintsReceivedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Total Complaints</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:TextBox ID="TextBox_EditCC_TotalComplaints" ReadOnly="true" runat="server" Width="160px" Text='<%# Bind("MOHS_CC_TotalComplaints","{0:###0}") %>' CssClass="Controls_TextBox_CalculationAlignRight_AlternatingRowStyle"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCC_TotalComplaints" runat="server" TargetControlID="TextBox_EditCC_TotalComplaints" FilterType="Numbers">
                                </Ajax:FilteredTextBoxExtender>
                                <asp:Label ID="Label_EditCC_TotalComplaints" runat="server" Text='<%# Bind("MOHS_CC_TotalComplaints","{0:#,##0}") %>'></asp:Label>
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
                          <asp:TextBox ID="TextBox_EditCare_TotalStaffTrained" runat="server" Width="150px" Text='<%# Bind("MOHS_Care_TotalStaffTrained","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCare_TotalStaffTrained" runat="server" TargetControlID="TextBox_EditCare_TotalStaffTrained" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditCare_TotalStaffTrained" runat="server" Text='<%# Bind("MOHS_Care_TotalStaffTrained","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffTrainedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total number of employed staff for the month
                        </td>
                        <td style="width: 585px; text-align: right;">
                          <asp:TextBox ID="TextBox_EditCare_TotalStaffEmployed" runat="server" Width="150px" Text='<%# Bind("MOHS_Care_TotalStaffEmployed","{0:###0}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditCare_TotalStaffEmployed" runat="server" TargetControlID="TextBox_EditCare_TotalStaffEmployed" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditCare_TotalStaffEmployed" runat="server" Text='<%# Bind("MOHS_Care_TotalStaffEmployed","{0:#,##0}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffEmployedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
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
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("MOHS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("MOHS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModified" runat="server" Text='<%# (bool)(Eval("MOHS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedBy" runat="server" Text='<%# Bind("MOHS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedDate" runat="server" Text='<%# Bind("MOHS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
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
                        <td style="width: 300px;">Total Clinic Visits</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemOH_ClinicVisits" runat="server" Text='<%# Bind("MOHS_OH_ClinicVisits","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHClinicVisitsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total Labour Hours</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemOH_LabourHours" runat="server" Text='<%# Bind("MOHS_OH_LabourHours","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHLabourHoursInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_TableBody">
                        <td style="width: 300px;">Patient Satisfaction Score</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemOH_PatientSatisfactionScore" runat="server" Text='<%# Bind("MOHS_OH_PatientSatisfactionScore","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHPatientSatisfactionScoreInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Patient Satisfaction Response Rate</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemOH_PatientSatisfactionResponseRate" runat="server" Text='<%# Bind("MOHS_OH_PatientSatisfactionResponseRate","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_OHPatientSatisfactionResponseRateInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
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
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedPositive_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedPositive_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedPositiveInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Suggestions</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedSuggestions_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedSuggestions_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedSuggestionsInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Comment Cards-Number Received Negative (P3)</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative_Published" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedNegative_Calculated" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedNegative_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedNegativeInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Comment Cards-Number Received Total</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:Label ID="Label_ItemCC_CCNReceivedTotal" runat="server" Text='<%# Bind("MOHS_CC_CCNReceivedTotal","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCCCNReceivedTotalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr>
                              <td style="width: 290px;">Complaints Received at Hospital level P1 and P2</td>
                              <td style="width: 186px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived_Published" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Published","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 185px; text-align: right;">
                                <asp:Label ID="Label_ItemCC_ComplaintsReceived_Calculated" runat="server" Text='<%# Bind("MOHS_CC_ComplaintsReceived_Calculated","{0:#,##0}") %>'></asp:Label>
                              </td>
                              <td style="width: 14px;"><a href="#" class="tt"><img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CCComplaintsReceivedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                            </tr>
                            <tr class="FormView_AlternatingRowStyle">
                              <td style="width: 290px;">Total Complaints</td>
                              <td colspan="3" style="text-align: right;">
                                <asp:Label ID="Label_ItemCC_TotalComplaints" runat="server" Text='<%# Bind("MOHS_CC_TotalComplaints","{0:#,##0}") %>'></asp:Label>
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
                        <td style="width: 300px;">Total number of staff trained for the month</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemCare_TotalStaffTrained" runat="server" Text='<%# Bind("MOHS_Care_TotalStaffTrained","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffTrainedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Total number of employed staff for the month</td>
                        <td style="width: 585px; text-align: right;"><asp:Label ID="Label_ItemCare_TotalStaffEmployed" runat="server" Text='<%# Bind("MOHS_Care_TotalStaffEmployed","{0:#,##0}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_CareTotalStaffEmployedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
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
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("MOHS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("MOHS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModified" runat="server" Text='<%# (bool)(Eval("MOHS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedBy" runat="server" Text='<%# Bind("MOHS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedDate" runat="server" Text='<%# Bind("MOHS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
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
                <asp:SqlDataSource ID="SqlDataSource_MOHS_Form" runat="server" OnUpdated="SqlDataSource_MOHS_Form_Updated"></asp:SqlDataSource>
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
