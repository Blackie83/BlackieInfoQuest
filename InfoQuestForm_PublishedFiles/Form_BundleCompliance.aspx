<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_BundleCompliance" CodeBehind="Form_BundleCompliance.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Bundle Compliance</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_BundleCompliance.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_BundleCompliance" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_BundleCompliance" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_BundleCompliance" AssociatedUpdatePanelID="UpdatePanel_BundleCompliance">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_BundleCompliance" runat="server">
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_BundleCompliance_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_BundleCompliance_Facility" runat="server"></asp:SqlDataSource>
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
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go to List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" />&nbsp;
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
                      <asp:Label ID="Label_InterventionsHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_BundleCompliance_Form" runat="server" DataKeyNames="BC_Bundles_Id" CssClass="FormView" DataSourceID="SqlDataSource_BundleCompliance_Form" OnItemInserting="FormView_BundleCompliance_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_BundleCompliance_Form_ItemCommand" OnDataBound="FormView_BundleCompliance_Form_DataBound" OnItemUpdating="FormView_BundleCompliance_Form_ItemUpdating">
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
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("BC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_BundleCompliance_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("BC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
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
                        <td colspan="3">SSI:&nbsp;<asp:CheckBox ID="CheckBox_InsertAssessedSSI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_SSI") %>' />&nbsp;&nbsp;CLABSI:&nbsp;<asp:CheckBox ID="CheckBox_InsertAssessedCLABSI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_CLABSI") %>' />&nbsp;&nbsp;VAP:&nbsp;<asp:CheckBox ID="CheckBox_InsertAssessedVAP" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_VAP") %>' />&nbsp;&nbsp;CAUTI:&nbsp;<asp:CheckBox ID="CheckBox_InsertAssessedCAUTI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_CAUTI") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSSITheatreProcedure">SSI Theatre Information
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSSITheatreProcedure" runat="server" SelectedValue='<%# Bind("BC_Bundles_SSI_TheatreProcedure") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_InsertSSITheatreProcedure_DataBinding">
                            <asp:ListItem Value="">Select Theatre Event</asp:ListItem>
                          </asp:DropDownList>
                          <br />
                          <asp:Label ID="Label_InsertSSITheatreProcedureError" runat="server" Text="A Bundle has already been captured for this Theatre event" CssClass="Controls_Error"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormSSI">SSI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_SSI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI1" runat="server" Checked='<%# Bind("BC_Bundles_SSI_1") %>' /></td>
                        <td>1.1 If hair is removed, it is only done with clippers or depilatory cream</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI1NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI2" runat="server" Checked='<%# Bind("BC_Bundles_SSI_2") %>' /></td>
                        <td>1.2 There is proof of antibiotic/s given on the peri-operative document</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI2NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI3" runat="server" Checked='<%# Bind("BC_Bundles_SSI_3") %>' /></td>
                        <td>1.3 Blood glucose maintained between 4 - 10 mmol/l throughout the ICU / HC stay</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI3NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI4" runat="server" Checked='<%# Bind("BC_Bundles_SSI_4") %>' /></td>
                        <td>1.4 Normothermia - temperature is maintained above 36 ̊ C - 37.5 ̊ C on the first assessment post operatively</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSSI4NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>SSI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertSSICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_SSI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="8" style="vertical-align: middle; text-align: center;" id="FormCLABSI">CLABSI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI1" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_1") %>' /></td>
                        <td>2.1 Hand washing procedure was followed</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI1NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI2" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_2") %>' /></td>
                        <td>2.2 Maximal barrier precautions were used by the doctor as per checklist</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI2NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI3" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_3") %>' /></td>
                        <td>2.3 2% Chlorhexidine in alcohol skin prep is done and allowed to dry before insertion</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI3NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI4" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_4") %>' /></td>
                        <td>2.4 Central line is sited in the subclavian or jugular vein</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI4NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI5" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_5") %>' /></td>
                        <td>2.5 A daily review is done of the need to keep the line (CVP)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI5NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI6" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_6") %>' /></td>
                        <td>2.6 The line is properly secured e.g. with a special dressing / device or stitched</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI6NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_6_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI7" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_7") %>' /></td>
                        <td>2.7 The dressing is visibly clean and intact</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCLABSI7NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_7_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>CLABSI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertCLABSICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_CLABSI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormVAP">VAP</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAPSelectAll" runat="server" Checked='<%# Bind("BC_Bundles_VAP_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP1" runat="server" Checked='<%# Bind("BC_Bundles_VAP_1") %>' /></td>
                        <td>3.1 The head of the bed is elevated to 30 - 40°</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP1NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP2" runat="server" Checked='<%# Bind("BC_Bundles_VAP_2") %>' /></td>
                        <td>3.2 Sedation vacation - patient has been assessed daily for readiness to extubate</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP2NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP3" runat="server" Checked='<%# Bind("BC_Bundles_VAP_3") %>' /></td>
                        <td>3.3 Peptic ulcer prophylaxis is given</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP3NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP4" runat="server" Checked='<%# Bind("BC_Bundles_VAP_4") %>' /></td>
                        <td>3.4 DVT prophylaxis is given / foot pumps are used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP4NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP5" runat="server" Checked='<%# Bind("BC_Bundles_VAP_5") %>' /></td>
                        <td>3.5 Mouth care is done at least 6 hourly using chlorhexidine mouth wash</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertVAP5NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>VAP Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertVAPCal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_VAP_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormCAUTI">CAUTI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI1" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_1") %>' /></td>
                        <td>4.1 A sterile catheter pack was used to insert the catheter</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI1NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI2" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_2") %>' /></td>
                        <td>4.2 The catheter is properly secured to avoid pulling</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI2NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI3" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_3") %>' /></td>
                        <td>4.3 Catheter (perineal) care is done at least twice daily and after every bowel movement using hibiscub and water / chlorhexidine and cetrimide. A disposable cloth / cotton wool or gauze may be used. Note: (bar soap or face cloths are *not* to used)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI3NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI4" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_4") %>' /></td>
                        <td>4.4 A daily review is done of the need to keep the catheter insitu</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertCAUTI4NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>CAUTI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertCAUTICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_CAUTI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("BC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("BC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("BC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("BC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("BC_Bundles_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Bundle" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("BC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_BundleCompliance_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("BC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:HiddenField ID="HiddenField_EditDate" runat="server" Value='<%# Eval("BC_Bundles_Date","{0:yyyy/MM/dd}") %>' />
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
                        <td colspan="3">SSI:&nbsp;<asp:CheckBox ID="CheckBox_EditAssessedSSI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_SSI") %>' />&nbsp;&nbsp;CLABSI:&nbsp;<asp:CheckBox ID="CheckBox_EditAssessedCLABSI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_CLABSI") %>' />&nbsp;&nbsp;VAP:&nbsp;<asp:CheckBox ID="CheckBox_EditAssessedVAP" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_VAP") %>' />&nbsp;&nbsp;CAUTI:&nbsp;<asp:CheckBox ID="CheckBox_EditAssessedCAUTI" runat="server" Checked='<%# Bind("BC_Bundles_Assessed_CAUTI") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormSSITheatreProcedure">SSI Theatre Information
                        </td>
                        <td colspan="3">
                          <asp:HiddenField ID="HiddenField_EditSSITheatreProcedure" runat="server" Value='<%# Eval("BC_Bundles_SSI_TheatreProcedure") %>' />
                          <asp:DropDownList ID="DropDownList_EditSSITheatreProcedure" runat="server" SelectedValue='<%# Bind("BC_Bundles_SSI_TheatreProcedure") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditSSITheatreProcedure_DataBinding">
                            <asp:ListItem Value="">Select Theatre Event</asp:ListItem>
                          </asp:DropDownList>
                          <br />
                          <asp:Label ID="Label_EditSSITheatreProcedureError" runat="server" Text="A Bundle has already been captured for this Theatre event" CssClass="Controls_Error"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormSSI">SSI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_SSI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI1" runat="server" Checked='<%# Bind("BC_Bundles_SSI_1") %>' /></td>
                        <td>1.1 If hair is removed, it is only done with clippers or depilatory cream</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI1NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI2" runat="server" Checked='<%# Bind("BC_Bundles_SSI_2") %>' /></td>
                        <td>1.2 There is proof of antibiotic/s given on the peri-operative document</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI2NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI3" runat="server" Checked='<%# Bind("BC_Bundles_SSI_3") %>' /></td>
                        <td>1.3 Blood glucose maintained between 4 - 10 mmol/l throughout the ICU / HC stay</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI3NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI4" runat="server" Checked='<%# Bind("BC_Bundles_SSI_4") %>' /></td>
                        <td>1.4 Normothermia - temperature is maintained above 36 ̊ C - 37.5 ̊ C on the first assessment post operatively</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSSI4NA" runat="server" Checked='<%# Bind("BC_Bundles_SSI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>SSI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditSSICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_SSI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="8" style="vertical-align: middle; text-align: center;" id="FormCLABSI">CLABSI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI1" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_1") %>' /></td>
                        <td>2.1 Hand washing procedure was followed</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI1NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI2" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_2") %>' /></td>
                        <td>2.2 Maximal barrier precautions were used by the doctor as per checklist</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI2NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI3" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_3") %>' /></td>
                        <td>2.3 2% Chlorhexidine in alcohol skin prep is done and allowed to dry before insertion</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI3NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI4" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_4") %>' /></td>
                        <td>2.4 Central line is sited in the subclavian or jugular vein</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI4NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI5" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_5") %>' /></td>
                        <td>2.5 A daily review is done of the need to keep the line (CVP)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI5NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI6" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_6") %>' /></td>
                        <td>2.6 The line is properly secured e.g. with a special dressing / device or stitched</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI6NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_6_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI7" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_7") %>' /></td>
                        <td>2.7 The dressing is visibly clean and intact</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCLABSI7NA" runat="server" Checked='<%# Bind("BC_Bundles_CLABSI_7_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>CLABSI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditCLABSICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_CLABSI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormVAP">VAP</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAPSelectAll" runat="server" Checked='<%# Bind("BC_Bundles_VAP_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP1" runat="server" Checked='<%# Bind("BC_Bundles_VAP_1") %>' /></td>
                        <td>3.1 The head of the bed is elevated to 30 - 40°</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP1NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP2" runat="server" Checked='<%# Bind("BC_Bundles_VAP_2") %>' /></td>
                        <td>3.2 Sedation vacation - patient has been assessed daily for readiness to extubate</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP2NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP3" runat="server" Checked='<%# Bind("BC_Bundles_VAP_3") %>' /></td>
                        <td>3.3 Peptic ulcer prophylaxis is given</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP3NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP4" runat="server" Checked='<%# Bind("BC_Bundles_VAP_4") %>' /></td>
                        <td>3.4 DVT prophylaxis is given / foot pumps are used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP4NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP5" runat="server" Checked='<%# Bind("BC_Bundles_VAP_5") %>' /></td>
                        <td>3.5 Mouth care is done at least 6 hourly using chlorhexidine mouth wash</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditVAP5NA" runat="server" Checked='<%# Bind("BC_Bundles_VAP_5_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>VAP Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditVAPCal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_VAP_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormCAUTI">CAUTI</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTISelectAll" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI1" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_1") %>' /></td>
                        <td>4.1 A sterile catheter pack was used to Edit the catheter</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI1NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_1_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI2" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_2") %>' /></td>
                        <td>4.2 The catheter is properly secured to avoid pulling</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI2NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_2_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI3" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_3") %>' /></td>
                        <td>4.3 Catheter (perineal) care is done at least twice daily and after every bowel movement using hibiscub and water / chlorhexidine and cetrimide. A disposable cloth / cotton wool or gauze may be used. Note: (bar soap or face cloths are *not* to used)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI3NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_3_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI4" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_4") %>' /></td>
                        <td>4.4 A daily review is done of the need to keep the catheter insitu</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCAUTI4NA" runat="server" Checked='<%# Bind("BC_Bundles_CAUTI_4_NA") %>' /></td>
                      </tr>
                      <tr>
                        <td>CAUTI Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditCAUTICal" Width="100px" runat="server" Text='<%# Bind("BC_Bundles_CAUTI_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("BC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("BC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("BC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("BC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("BC_Bundles_IsActive") %>' />&nbsp;
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
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("BC_Bundles_ReportNumber") %>'></asp:Label>
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
                        <td>Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("BC_Bundles_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormAssessed">Bundles Assessed
                        </td>
                        <td colspan="3">SSI:&nbsp;<strong><asp:Label ID="Label_ItemAssessedSSI" runat="server" Text='<%# (bool)(Eval("BC_Bundles_Assessed_SSI"))?"Yes":"No" %>' /></strong>&nbsp;&nbsp;CLABSI:&nbsp;<strong><asp:Label ID="Label_ItemAssessedCLABSI" runat="server" Text='<%# (bool)(Eval("BC_Bundles_Assessed_CLABSI"))?"Yes":"No" %>' /></strong>&nbsp;&nbsp;VAP:&nbsp;<strong><asp:Label ID="Label_ItemAssessedVAP" runat="server" Text='<%# (bool)(Eval("BC_Bundles_Assessed_VAP"))?"Yes":"No" %>' /></strong>&nbsp;&nbsp;CAUTI:&nbsp;<strong><asp:Label ID="Label_ItemAssessedCAUTI" runat="server" Text='<%# (bool)(Eval("BC_Bundles_Assessed_CAUTI"))?"Yes":"No" %>' /></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>SSI Theatre Information
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="DropDownList_ItemSSITheatreProcedure" runat="server" Text='<%# Bind("BC_Bundles_SSI_TheatreProcedure") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormSSI">SSI</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSISelectAll" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI1" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.1 If hair is removed, it is only done with clippers or depilatory cream</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI1NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI2" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.2 There is proof of antibiotic/s given on the peri-operative document</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI2NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI3" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.3 Blood glucose maintained between 4 - 10 mmol/l throughout the ICU / HC stay</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI3NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI4" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.4 Normothermia - temperature is maintained above 36 ̊ C - 37.5 ̊ C on the first assessment post operatively</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSSI4NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_SSI_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>SSI Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemSSICal" runat="server" Text='<%# Bind("BC_Bundles_SSI_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="8" style="vertical-align: middle; text-align: center;" id="FormCLABSI">CLABSI</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSISelectAll" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI1" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.1 Hand washing procedure was followed</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI1NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI2" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.2 Maximal barrier precautions were used by the doctor as per checklist</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI2NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI3" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.3 2% Chlorhexidine in alcohol skin prep is done and allowed to dry before insertion</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI3NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI4" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.4 Central line is sited in the subclavian or jugular vein</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI4NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI5" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_5"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.5 A daily review is done of the need to keep the line (CVP)</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI5NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI6" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_6"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.6 The line is properly secured e.g. with a special dressing / device or stitched</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI6NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_6_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI7" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_7"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.7 The dressing is visibly clean and intact</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCLABSI7NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CLABSI_7_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>CLABSI Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemCLABSICal" runat="server" Text='<%# Bind("BC_Bundles_CLABSI_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormVAP">VAP</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAPSelectAll" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP1" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.1 The head of the bed is elevated to 30 - 40°</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP1NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP2" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.2 Sedation vacation - patient has been assessed daily for readiness to extubate</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP2NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP3" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.3 Peptic ulcer prophylaxis is given</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP3NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP4" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.4 DVT prophylaxis is given / foot pumps are used</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP4NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP5" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_5"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.5 Mouth care is done at least 6 hourly using chlorhexidine mouth wash</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemVAP5NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_VAP_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>VAP Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemVAPCal" runat="server" Text='<%# Bind("BC_Bundles_VAP_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormCAUTI">CAUTI</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTISelectAll" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI1" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.1 A sterile catheter pack was used to Item the catheter</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI1NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI2" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.2 The catheter is properly secured to avoid pulling</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI2NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI3" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.3 Catheter (perineal) care is done at least twice daily and after every bowel movement using hibiscub and water / chlorhexidine and cetrimide. A disposable cloth / cotton wool or gauze may be used. Note: (bar soap or face cloths are *not* to used)</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI3NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI4" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>4.4 A daily review is done of the need to keep the catheter insitu</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemCAUTI4NA" runat="server" Text='<%# (bool)(Eval("BC_Bundles_CAUTI_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr>
                        <td>CAUTI Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemCAUTICal" runat="server" Text='<%# Bind("BC_Bundles_CAUTI_Cal") %>'></asp:Label></strong>&nbsp;
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("BC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("BC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("BC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("BC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("BC_Bundles_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
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
                <asp:SqlDataSource ID="SqlDataSource_BundleCompliance_InsertUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_BundleCompliance_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_BundleCompliance_Form" runat="server" OnInserted="SqlDataSource_BundleCompliance_Form_Inserted" OnUpdated="SqlDataSource_BundleCompliance_Form_Updated"></asp:SqlDataSource>
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
                      <asp:GridView ID="GridView_BundleCompliance_Bundles" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_BundleCompliance_Bundles" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_BundleCompliance_Bundles_PreRender" OnDataBound="GridView_BundleCompliance_Bundles_DataBound" OnRowCreated="GridView_BundleCompliance_Bundles_RowCreated">
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
                          <%=GridView_BundleCompliance_Bundles.PageCount%>
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
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("BC_Bundles_Id"),Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="BC_Bundles_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="BC_Bundles_ReportNumber" />
                          <asp:BoundField DataField="BC_Bundles_Date" HeaderText="OBS Date" ReadOnly="True" SortExpression="BC_Bundles_Date" />
                          <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                          <asp:BoundField DataField="BC_Bundles_SSI_TheatreProcedure" HeaderText="SSI Theatre Procedure" ReadOnly="True" SortExpression="BC_Bundles_SSI_TheatreProcedure" />
                          <asp:BoundField DataField="BC_Bundles_SSI_Cal" HeaderText="SSI Cal" ReadOnly="True" SortExpression="BC_Bundles_SSI_Cal" />
                          <asp:BoundField DataField="BC_Bundles_CLABSI_Cal" HeaderText="CLABSI Cal" ReadOnly="True" SortExpression="BC_Bundles_CLABSI_Cal" />
                          <asp:BoundField DataField="BC_Bundles_VAP_Cal" HeaderText="VAP Cal" ReadOnly="True" SortExpression="BC_Bundles_VAP_Cal" />
                          <asp:BoundField DataField="BC_Bundles_CAUTI_Cal" HeaderText="CAUTI Cal" ReadOnly="True" SortExpression="BC_Bundles_CAUTI_Cal" />
                          <asp:BoundField DataField="BC_Bundles_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="BC_Bundles_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_BundleCompliance_Bundles" runat="server" OnSelected="SqlDataSource_BundleCompliance_Bundles_Selected"></asp:SqlDataSource>
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
