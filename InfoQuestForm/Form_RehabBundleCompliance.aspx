<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_RehabBundleCompliance" CodeBehind="Form_RehabBundleCompliance.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Rehab Bundle Compliance</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_RehabBundleCompliance.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_RehabBundleCompliance" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_RehabBundleCompliance" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_RehabBundleCompliance" AssociatedUpdatePanelID="UpdatePanel_RehabBundleCompliance">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_RehabBundleCompliance" runat="server">
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_RehabBundleCompliance_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_Facility" runat="server"></asp:SqlDataSource>
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
                <asp:FormView ID="FormView_RehabBundleCompliance_Form" runat="server" DataKeyNames="RBC_Bundles_Id" CssClass="FormView" DataSourceID="SqlDataSource_RehabBundleCompliance_Form" OnItemInserting="FormView_RehabBundleCompliance_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_RehabBundleCompliance_Form_ItemCommand" OnDataBound="FormView_RehabBundleCompliance_Form_DataBound" OnItemUpdating="FormView_RehabBundleCompliance_Form_ItemUpdating">
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
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("RBC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("RBC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
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
                        <td id="FormAssessedList">Bundle Assessed
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertAssessedList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_InsertAssessedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_Assessed_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Catheterization</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_InsertAssessedList" runat="server" Value="" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormIUC">Indwelling Urethral Catheter (IUC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormIUC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC1" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_1") %>' /></td>
                        <td>1.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC2" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_2") %>' /></td>
                        <td>1.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC3" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_3") %>' /></td>
                        <td>1.3 Drainage bag is below level of bladder at all times</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC4" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_4") %>' /></td>
                        <td>1.4 Catheter care done once per shift and after every bowel action</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC5">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC5" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_5") %>' /></td>
                        <td>1.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertIUC5NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_5_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC5CatheterList">
                        <td id="FormIUC5Catheter">Catheter
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertIUC5CatheterList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_IUC_5_Catheter_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCCAL">
                        <td>IUC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertIUCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_IUC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormSPC">Supra - Pubic Catheter (SPC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormSPC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC1" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_1") %>' /></td>
                        <td>2.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC2" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_2") %>' /></td>
                        <td>2.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC3" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_3") %>' /></td>
                        <td>2.3 Drainage bag is below level of bladder at all times</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC4" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_4") %>' /></td>
                        <td>2.4 Catheter care done once per shift</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC5">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC5" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_5") %>' /></td>
                        <td>2.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertSPC5NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_5_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC5CatheterList">
                        <td id="FormSPC5Catheter">Catheter
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSPC5CatheterList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_SPC_5_Catheter_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCCAL">
                        <td>SPC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertSPCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_SPC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormICCSelectAll">
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormICC">Intermittent Clean Catheterization (ICC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormICC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC1" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_1") %>' /></td>
                        <td>
                          <asp:Label ID="Label_InsertICC1_TextRehabilitationStaff" runat="server" Text="3.1 Education and training of rehab staff has been done and deemed competent"></asp:Label>
                          <asp:Label ID="Label_InsertICC1_TextPatientPrivateCaregiver" runat="server" Text="3.1 Education and training of caregiver and / or patient has been done and deemed competent"></asp:Label>
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC2" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_2") %>' /></td>
                        <td>3.2 Free access to equipment and materials in the ward</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC3" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_3") %>' /></td>
                        <td>
                          <asp:Label ID="Label_InsertICC3_TextRehabilitationStaff" runat="server" Text="3.3 Task is performed in a 'sterile' manner"></asp:Label>
                          <asp:Label ID="Label_InsertICC3_TextPatientPrivateCaregiver" runat="server" Text="3.3 Task is performed in a 'clean' manner"></asp:Label>
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC4" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_4") %>' /></td>
                        <td>3.4 Catheter stored in appropriate solution between catheterizations</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertICC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormICCCAL">
                        <td>ICC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_InsertICCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_ICC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("RBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("RBC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("RBC_Bundles_IsActive") %>'></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("RBC_Bundles_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormUnit">Unit
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDate">Observation Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("RBC_Bundles_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:HiddenField ID="HiddenField_EditDate" runat="server" Value='<%# Eval("RBC_Bundles_Date","{0:yyyy/MM/dd}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormAssessedList">Bundle Assessed
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditAssessedList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_EditAssessedList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_Assessed_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Catheterization</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditAssessedList" runat="server" Value='<%# Eval("RBC_Bundles_Assessed_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormIUC">Indwelling Urethral Catheter (IUC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormIUC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC1" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_1") %>' /></td>
                        <td>1.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC2" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_2") %>' /></td>
                        <td>1.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC3" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_3") %>' /></td>
                        <td>1.3 Drainage bag is below level of bladder at all times</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC4" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_4") %>' /></td>
                        <td>1.4 Catheter care done once per shift and after every bowel action</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC5">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC5" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_5") %>' /></td>
                        <td>1.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIUC5NA" runat="server" Checked='<%# Bind("RBC_Bundles_IUC_5_NA") %>' /></td>
                      </tr>
                      <tr id="FormIUC5CatheterList">
                        <td id="FormIUC5Catheter">Catheter
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditIUC5CatheterList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_IUC_5_Catheter_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCCAL">
                        <td>IUC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditIUCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_IUC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormSPC">Supra - Pubic Catheter (SPC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormSPC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC1" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_1") %>' /></td>
                        <td>2.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC2" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_2") %>' /></td>
                        <td>2.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC3" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_3") %>' /></td>
                        <td>2.3 Drainage bag is below level of bladder at all times</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC4" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_4") %>' /></td>
                        <td>2.4 Catheter care done once per shift</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC5">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC5" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_5") %>' /></td>
                        <td>2.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditSPC5NA" runat="server" Checked='<%# Bind("RBC_Bundles_SPC_5_NA") %>' /></td>
                      </tr>
                      <tr id="FormSPC5CatheterList">
                        <td id="FormSPC5Catheter">Catheter
                        </td>
                        <td colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSPC5CatheterList" runat="server" DataSourceID="SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("RBC_Bundles_SPC_5_Catheter_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCCAL">
                        <td>SPC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditSPCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_SPC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormICCSelectAll">
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormICC">Intermittent Clean Catheterization (ICC)</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICCSelectAll" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_SelectAll") %>' /></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormICC1">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC1" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_1") %>' /></td>
                        <td>
                          <asp:Label ID="Label_EditICC1_TextRehabilitationStaff" runat="server" Text="3.1 Education and training of rehab staff has been done and deemed competent"></asp:Label>
                          <asp:Label ID="Label_EditICC1_TextPatientPrivateCaregiver" runat="server" Text="3.1 Education and training of caregiver and / or patient has been done and deemed competent"></asp:Label>
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC1NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_1_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC2">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC2" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_2") %>' /></td>
                        <td>3.2 Free access to equipment and materials in the ward</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC2NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_2_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC3">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC3" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_3") %>' /></td>
                        <td>
                          <asp:Label ID="Label_EditICC3_TextRehabilitationStaff" runat="server" Text="3.3 Task is performed in a 'sterile' manner"></asp:Label>
                          <asp:Label ID="Label_EditICC3_TextPatientPrivateCaregiver" runat="server" Text="3.3 Task is performed in a 'clean' manner"></asp:Label>
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC3NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_3_NA") %>' /></td>
                      </tr>
                      <tr id="FormICC4">
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC4" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_4") %>' /></td>
                        <td>3.4 Catheter stored in appropriate solution between cauterizations</td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditICC4NA" runat="server" Checked='<%# Bind("RBC_Bundles_ICC_4_NA") %>' /></td>
                      </tr>
                      <tr id="FormICCCAL">
                        <td>ICC Calculation
                        </td>
                        <td colspan="3">
                          <asp:TextBox ID="Textbox_EditICCCal" Width="100px" runat="server" Text='<%# Bind("RBC_Bundles_ICC_Cal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("RBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("RBC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("RBC_Bundles_IsActive") %>' />&nbsp;
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
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("RBC_Bundles_ReportNumber") %>'></asp:Label>
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
                          <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("RBC_Bundles_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Bundle Assessed
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemAssessedList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemAssessedList" runat="server" Value='<%# Bind("RBC_Bundles_Assessed_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormIUC">Indwelling Urethral Catheter (IUC)</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemIUCSelectAll" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormIUC1">
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC1" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC1NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormIUC2">
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC2" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC2NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormIUC3">
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC3" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.3 Drainage bag is below level of bladder at all times</td>
                        <td><strong>
                          <asp:Label ID="CheckBox_ItemIUC3NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormIUC4">
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC4" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>1.4 Catheter care done once per shift and after every bowel action</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC4NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormIUC5">
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC5" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_5"))?"Yes":"No" %>'></asp:Label></strong><asp:HiddenField ID="HiddenField_ItemIUC5" runat="server" Value='<%# Bind("RBC_Bundles_IUC_5") %>' />
                        </td>
                        <td>1.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemIUC5NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IUC_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormIUC5CatheterList">
                        <td>Catheter
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemIUC5CatheterList" runat="server" Text=''></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormIUCCAL">
                        <td>IUC Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemIUCCal" runat="server" Text='<%# Bind("RBC_Bundles_IUC_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCSelectAll">
                        <td rowspan="6" style="vertical-align: middle; text-align: center;" id="FormSPC">Supra - Pubic Catheter (SPC)</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPCSelectAll" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormSPC1">
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC1" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.1 Replace all catheters on admission – a sterile catheter pack was used</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC1NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormSPC2">
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC2" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.2 The catheter is properly secured to avoid pulling on the urinary tract</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC2NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormSPC3">
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC3" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.3 Drainage bag is below level of bladder at all times</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC3NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormSPC4">
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC4" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>2.4 Catheter care done once per shift</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC4NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormSPC5">
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC5" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_5"))?"Yes":"No" %>'></asp:Label></strong><asp:HiddenField ID="HiddenField_ItemSPC5" runat="server" Value='<%# Bind("RBC_Bundles_SPC_5") %>' />
                        </td>
                        <td>2.5 A weekly review is done of the need to keep the catheter in situ</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemSPC5NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_SPC_5_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormSPC5CatheterList">
                        <td>Catheter
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Label_ItemSPC5CatheterList" runat="server" Text=''></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormSPCCAL">
                        <td>SPC Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Textbox_ItemSPCCal" runat="server" Text='<%# Bind("RBC_Bundles_SPC_Cal") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr id="FormICCSelectAll">
                        <td rowspan="5" style="vertical-align: middle; text-align: center;" id="FormICC">Intermittent Clean Catheterization (ICC)</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemICCSelectAll" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_SelectAll"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>Compliant for all Elements</td>
                        <td>N/A</td>
                      </tr>
                      <tr id="FormICC1">
                        <td><strong>
                          <asp:Label ID="Label_ItemICC1" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_1"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>
                          <asp:Label ID="Label_ItemICC1_TextRehabilitationStaff" runat="server" Text="3.1 Education and training of rehab staff has been done and deemed competent"></asp:Label>
                          <asp:Label ID="Label_ItemICC1_TextPatientPrivateCaregiver" runat="server" Text="3.1 Education and training of caregiver and / or patient has been done and deemed competent"></asp:Label>
                        </td>
                        <td><strong>
                          <asp:Label ID="Label_ItemICC1NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_1_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormICC2">
                        <td><strong>
                          <asp:Label ID="Label_ItemICC2" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_2"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.2 Free access to equipment and materials in the ward</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemICC2NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_2_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormICC3">
                        <td><strong>
                          <asp:Label ID="Label_ItemICC3" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_3"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>
                          <asp:Label ID="Label_ItemICC3_TextRehabilitationStaff" runat="server" Text="3.3 Task is performed in a 'sterile' manner"></asp:Label>
                          <asp:Label ID="Label_ItemICC3_TextPatientPrivateCaregiver" runat="server" Text="3.3 Task is performed in a 'clean' manner"></asp:Label>
                        </td>
                        <td><strong>
                          <asp:Label ID="Label_ItemICC3NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_3_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormICC4">
                        <td><strong>
                          <asp:Label ID="Label_ItemICC4" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_4"))?"Yes":"No" %>'></asp:Label></strong></td>
                        <td>3.4 Catheter stored in appropriate solution between cauterizations</td>
                        <td><strong>
                          <asp:Label ID="Label_ItemICC4NA" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_ICC_4_NA"))?"Yes":"No" %>'></asp:Label></strong></td>
                      </tr>
                      <tr id="FormICCCAL">
                        <td>ICC Calculation
                        </td>
                        <td colspan="3">
                          <strong>
                            <asp:Label ID="Textbox_ItemICCCal" runat="server" Text='<%# Bind("RBC_Bundles_ICC_Cal") %>'></asp:Label></strong>&nbsp;
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("RBC_Bundles_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("RBC_Bundles_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("RBC_Bundles_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("RBC_Bundles_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
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
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_InsertUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_InsertAssessedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_EditAssessedList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_Form" runat="server" OnInserted="SqlDataSource_RehabBundleCompliance_Form_Inserted" OnUpdated="SqlDataSource_RehabBundleCompliance_Form_Updated"></asp:SqlDataSource>
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
                      <asp:GridView ID="GridView_RehabBundleCompliance_Bundles" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_RehabBundleCompliance_Bundles" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_RehabBundleCompliance_Bundles_PreRender" OnDataBound="GridView_RehabBundleCompliance_Bundles_DataBound" OnRowCreated="GridView_RehabBundleCompliance_Bundles_RowCreated">
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
                          <%=GridView_RehabBundleCompliance_Bundles.PageCount%>
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
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("RBC_Bundles_Id"),Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="RBC_Bundles_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="RBC_Bundles_ReportNumber" />
                          <asp:BoundField DataField="RBC_Bundles_Date" HeaderText="OBS Date" ReadOnly="True" SortExpression="RBC_Bundles_Date" />
                          <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                          <asp:BoundField DataField="RBC_Bundles_IUC_Cal" HeaderText="IUC Cal" ReadOnly="True" SortExpression="RBC_Bundles_IUC_Cal" />
                          <asp:BoundField DataField="RBC_Bundles_SPC_Cal" HeaderText="SPC Cal" ReadOnly="True" SortExpression="RBC_Bundles_SPC_Cal" />
                          <asp:BoundField DataField="RBC_Bundles_ICC_Cal" HeaderText="ICC Cal" ReadOnly="True" SortExpression="RBC_Bundles_ICC_Cal" />
                          <asp:BoundField DataField="RBC_Bundles_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="RBC_Bundles_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_RehabBundleCompliance_Bundles" runat="server" OnSelected="SqlDataSource_RehabBundleCompliance_Bundles_Selected"></asp:SqlDataSource>
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
