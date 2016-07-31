<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_InfectionPrevention.aspx.cs" Inherits="InfoQuestForm.Form_InfectionPrevention" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_InfectionPrevention.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_InfectionPrevention" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_InfectionPrevention" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_InfectionPrevention" AssociatedUpdatePanelID="UpdatePanel_InfectionPrevention">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_InfectionPrevention" runat="server">
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
          <table id="TableForm" class="Table" style="width: 600px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FormHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_InfectionPrevention_Form" runat="server" Width="1000px" DataKeyNames="pkiInfectionFormID" CssClass="FormView" DataSourceID="SqlDataSource_InfectionPrevention_Form" OnItemInserting="FormView_InfectionPrevention_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_InfectionPrevention_Form_ItemCommand" OnDataBound="FormView_InfectionPrevention_Form_DataBound" OnItemUpdating="FormView_InfectionPrevention_Form_ItemUpdating">
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
                        <td style="width: 625px;" colspan="3">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertValidIMedsData" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 625px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_InsertFacility" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertPatientVisitNumber" runat="server" Width="200px" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                          <asp:Button ID="Button_InsertSearch" runat="server" OnClick="Button_InsertSearch_OnClick" Text="Find Patient Information" CssClass="Controls_Button" />&nbsp;
                          <br />
                          <asp:Label ID="Label_InsertPatientError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertPatientName" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAge">Age
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertAge" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_InsertDateOfAdmission" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormVisitDiagnosis">Visit Diagnosis
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:CheckBoxList ID="CheckBoxList_InsertVisitDiagnosis" Width="100%" runat="server" CssClass="Controls_CheckBoxListWithScrollbars" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Table">
                          </asp:CheckBoxList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Infection Detail
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitId">Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertUnitId" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_InsertUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateInfectionReported">Date Infection Reported<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDateInfectionReported" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDateInfectionReported" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDateInfectionReported" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDateInfectionReported" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDateInfectionReported">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDateInfectionReported" runat="server" TargetControlID="TextBox_InsertDateInfectionReported" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionType">Infection Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionType" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_InsertInfectionType" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertInfectionType_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Infection Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideSSISubType">
                        <td style="width: 175px;" id="FormSSISubType">SSI Sub Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSSISubType" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_InsertSSISubType" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                            <asp:ListItem Value="">Select SSI Sub Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSeverity">Infection Severity
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_InsertSeverity" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="200px">
                            <asp:ListItem Text="Major" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Minor" Value="2"></asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Infection
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" TextMode="MultiLine" Rows="8" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertDescription" runat="server" TargetControlID="TextBox_InsertDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Surgery / Procedure Details (Current Visit and Visits Selected)
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_InsertInfectionPrevention_Surgery" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_InsertInfectionPrevention_Surgery_DataBound" OnRowCreated="GridView_InsertInfectionPrevention_Surgery_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Surgery
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertSurgery" runat="server" />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <table>
                                    <tr>
                                      <td><strong>Facility</strong></td>
                                      <td><strong>Admission Date</strong></td>
                                      <td><strong>Theatre Invoice</strong></td>
                                      <td><strong>Surgeon</strong></td>
                                      <td><strong>Procedure</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Visit Number</strong></td>
                                      <td><strong>Discharge Date</strong></td>
                                      <td><strong>Theatre</strong></td>
                                      <td><strong>Anaesthetist</strong></td>
                                      <td><strong>Scrub Nurse</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Final Diagnosis</strong></td>
                                      <td><strong>Procedure Date</strong></td>
                                      <td><strong>Theatre Time (min)</strong></td>
                                      <td><strong>Assistant</strong></td>
                                      <td><strong>Wound Category</strong></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_InsertSurgery_FacilityName" runat="server" Width="180px" Text='<%# Bind("FacilityName") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_DateOfAdmission" runat="server" Width="180px" Text='<%# Bind("DateOfAdmission") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_TheatreInvoice" runat="server" Width="180px" Text='<%# Bind("InvoiceNum") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Surgeon" runat="server" Width="180px" Text='<%# Bind("Surgeon") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Procedure" runat="server" Width="180px" Text='<%# GetInsertSurgery_Procedure(Eval("ProcedureDescription"), Eval("ProcedureCode")) %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_InsertSurgery_VisitNumber" runat="server" Width="180px" Text='<%# Bind("VisitNumber") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_DischargeDate" runat="server" Width="180px" Text='<%# Bind("DischargeDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Theatre" runat="server" Width="180px" Text='<%# GetInsertSurgery_Theatre(Eval("TheatreDescription"), Eval("TheatreCode")) %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Anesthetist" runat="server" Width="180px" Text='<%# Bind("Anesthetist") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_ScrubNurse" runat="server" Width="180px" Text='<%# Bind("ScrubNurse") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_InsertSurgery_FinalDiagnosis" runat="server" Width="180px" Text='<%# GetInsertSurgery_FinalDiagnosis(Eval("FinalDiagnosisDescription"), Eval("FinalDiagnosis")) %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Date" runat="server" Width="180px" Text='<%# Bind("Date") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_Duration" runat="server" Width="180px" Text='<%# Bind("Duration") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_AssistantSurgeon" runat="server" Width="180px" Text='<%# Bind("AssistantSurgeon") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_InsertSurgery_WoundCategory" runat="server" Width="180px" Text='<%# Bind("WoundCategory") %>'></asp:Label></td>
                                    </tr>                                    
                                  </table>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Predisposing Conditions and Contributing Factors
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <table style="width: 100%;">
                            <tr>
                              <td></td>
                              <td>Condition</td>
                              <td>Description</td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF1" runat="server" Text="" /></td>
                              <td>Carcinoma</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF1" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF1" runat="server" TargetControlID="TextBox_InsertPCCF1" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF2" runat="server" Text="" /></td>
                              <td>Chronic respiratory disease</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF2" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF2" runat="server" TargetControlID="TextBox_InsertPCCF2" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF3" runat="server" Text="" /></td>
                              <td>Diabetes</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF3" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF3" runat="server" TargetControlID="TextBox_InsertPCCF3" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF4" runat="server" Text="" /></td>
                              <td>Haematology</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF4" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF4" runat="server" TargetControlID="TextBox_InsertPCCF4" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF5" runat="server" Text="" /></td>
                              <td>Hypertention</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF5" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF5" runat="server" TargetControlID="TextBox_InsertPCCF5" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF6" runat="server" Text="" /></td>
                              <td>Immune compromised</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF6" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF6" runat="server" TargetControlID="TextBox_InsertPCCF6" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF7" runat="server" Text="" /></td>
                              <td>Immune suppressed</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF7" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF7" runat="server" TargetControlID="TextBox_InsertPCCF7" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF8" runat="server" Text="" /></td>
                              <td>Obesity</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF8" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF8" runat="server" TargetControlID="TextBox_InsertPCCF8" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF9" runat="server" Text="" /></td>
                              <td>Other</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF9" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF9" runat="server" TargetControlID="TextBox_InsertPCCF9" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF10" runat="server" Text="" /></td>
                              <td>Prematurity</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF10" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF10" runat="server" TargetControlID="TextBox_InsertPCCF10" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF11" runat="server" Text="" /></td>
                              <td>Prolonged hospital stay</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF11" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF11" runat="server" TargetControlID="TextBox_InsertPCCF11" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF12" runat="server" Text="" /></td>
                              <td>Prolonged ventilation</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF12" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF12" runat="server" TargetControlID="TextBox_InsertPCCF12" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF13" runat="server" Text="" /></td>
                              <td>Prosthetic implants</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF13" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF13" runat="server" TargetControlID="TextBox_InsertPCCF13" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF14" runat="server" Text="" /></td>
                              <td>Renal failure</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF14" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF14" runat="server" TargetControlID="TextBox_InsertPCCF14" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertPCCF15" runat="server" Text="" /></td>
                              <td>Steroid treatment</td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertPCCF15" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertPCCF15" runat="server" TargetControlID="TextBox_InsertPCCF15" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bed History
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_InsertInfectionPrevention_BedHistory" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_InsertInfectionPrevention_BedHistory_DataBound" OnRowCreated="GridView_InsertInfectionPrevention_BedHistory_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Bed History
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertBedHistory" runat="server" />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="To Unit / Department">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertBedHistory_To" runat="server" Width="600px" Text='<%# GetInsertBedHistory_To(Eval("DEP_DESCRIPTION"), Eval("ROOMNUM"), Eval("BEDNUM")) %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertBedHistory_Date" runat="server" Width="300px" Text='<%# Bind("VBD_DATE") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable Diseases
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <table style="width: 100%;">
                            <tr>
                              <td></td>
                              <td>Agency / Department</td>
                              <td>Notifiable Disease</td>
                              <td>Date</td>
                              <td>Reference Number</td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertRD" runat="server" Text="" />
                              </td>
                              <td>Department of Health</td>
                              <td>
                                <asp:DropDownList ID="DropDownList_InsertRDNotifiableDisease" runat="server" Width="600px" DataSourceID="SqlDataSource_InfectionPrevention_InsertRDNotifiableDisease" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                                  <asp:ListItem Value="">Select Notifiable Disease</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertRDDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="ImageButton_InsertRDDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                                <Ajax:CalendarExtender ID="CalendarExtender_InsertRDDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertRDDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertRDDate">
                                </Ajax:CalendarExtender>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertRDDate" runat="server" TargetControlID="TextBox_InsertRDDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertRDReferenceNumber" runat="server" Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertRDReferenceNumber" runat="server" TargetControlID="TextBox_InsertRDReferenceNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Laboratory Reports
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_InsertInfectionPrevention_LabReport" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_InsertInfectionPrevention_LabReport_DataBound" OnRowCreated="GridView_InsertInfectionPrevention_LabReport_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Laboratory Reports
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertLabReport" runat="server" />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertLabReport_Date" runat="server" Width="225px" Text='<%# Bind("SPEC_DATE") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Specimen">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertLabReport_Specimen" runat="server" Width="225px" Text='<%# Bind("SPEC_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Organism">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertLabReport_Organism" runat="server" Width="225px" Text='<%# Bind("ORG_DESC") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Lab Number">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertLabReport_LabNumber" runat="server" Width="225px" Text='<%# Bind("SR_LAB_NUM") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bundle Compliance
                        </td>
                      </tr>
                      <tr id="ShowHideDays">
                        <td style="width: 175px;" id="FormDays">No of Days
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertDays" runat="server" Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertDays" runat="server" TargetControlID="TextBox_InsertDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeSSI" rowspan="4">SSI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeSSI1" runat="server" Text="1.1 If hair is removed, it is only done with clippers or dipilatory cream" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeSSI2" runat="server" Text="1.2 There is proof of antibiotic/s given on the peri-operative document" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeSSI3" runat="server" Text="1.3 Blood glucose maintained between 4 - 10 throughout the ICU/HC stay" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeSSI4" runat="server" Text="1.4 Normothermia - temperature is maintained above 36 ̊  - 37.5 ̊  on the first assessment post operatively" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeCLABSI" rowspan="7">CLABSI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI1" runat="server" Text="2.1 Handwashing procedure was followed" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI2" runat="server" Text="2.2 Maximal barrier precautions were used by the doctor as per checklist" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI3" runat="server" Text="2.3 Chlorhexidine skin prep is done and allowed to dry before insertion" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI4" runat="server" Text="2.4 Central line sited in the subclavian vein" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI5">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI5" runat="server" Text="2.5 A daily review is done of the need to keep the line (CVP)" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI6">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI6" runat="server" Text="2.6 The line is properly secured e.g. with a special dressing /device or stitched" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI7">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCLABSI7" runat="server" Text="2.7 The dressing is visibly clean and intact" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeVAP" rowspan="5">VAP
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeVAP1" runat="server" Text="3.1 The head of the bed is elevated to 30 - 40°" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeVAP2" runat="server" Text="3.2 Sedation vacation - patient has been assessed daily for readiness to extubate" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeVAP3" runat="server" Text="3.3 Peptic ulcer prophylaxis is given" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeVAP4" runat="server" Text="3.4 DVT prophylaxis is given" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP5">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeVAP5" runat="server" Text="3.5 Mouth care is done at least 6 hourly using chlorhexidine mouth wash" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeCAUTI" rowspan="4">CAUTI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCAUTI1" runat="server" Text="4.1 A sterile catheter pack was used to insert the catheter" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCAUTI2" runat="server" Text="4.2 The catheter is properly secured to avoid pulling" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCAUTI3" runat="server" Text="4.3 Catheter (perineal) care is done at least twice daily and after every bowel movement using hibiscub and water / chlorhexidine and cetrimide.  A disposeable cloth/cotton wool or gauze may be used.  Note: (bar soap or face cloths are not to used)" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertTypeCAUTI4" runat="server" Text="4.4 A daily review is done of the need to keep the catheter insitu" />
                        </td>
                      </tr>
                      <tr id="ShowHideCompliance">
                        <td style="width: 175px;" id="FormCompliance">100% Compliance to all elements
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_InsertCompliance" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="200px">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRisk">Related High Risk Procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertRiskTPN" runat="server" Text="TPN" />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:CheckBox ID="CheckBox_InsertRiskEnteralFeeding" runat="server" Text="Enteral Feeding" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Section
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDate">Date of Investigation<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertInvestigationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertInvestigationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertInvestigationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertInvestigationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertInvestigationDate" runat="server" TargetControlID="TextBox_InsertInvestigationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationName">Investigator Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationName" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertInvestigationName" runat="server" TargetControlID="TextBox_InsertInvestigationName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDesignation">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationDesignation" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertInvestigationDesignation" runat="server" TargetControlID="TextBox_InsertInvestigationDesignation" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationIPCSName">IPC / S Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationIPCSName" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertInvestigationIPCSName" runat="server" TargetControlID="TextBox_InsertInvestigationIPCSName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationTeamMembers">Team Members
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationTeamMembers" runat="server" TextMode="MultiLine" Rows="8" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertInvestigationTeamMembers" runat="server" TargetControlID="TextBox_InsertInvestigationTeamMembers" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Precautionary / Isolation Measures
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                          <asp:CheckBox ID="CheckBox_InsertPIM1" runat="server" Text="Airborne precautions" /><br />
                          <asp:CheckBox ID="CheckBox_InsertPIM2" runat="server" Text="Contact precautions" /><br />
                          <asp:CheckBox ID="CheckBox_InsertPIM3" runat="server" Text="Droplet precautions" /><br />
                          <asp:CheckBox ID="CheckBox_InsertPIM4" runat="server" Text="Formidable epidemic disease" /><br />
                          <asp:CheckBox ID="CheckBox_InsertPIM5" runat="server" Text="Protective isolation precautions" /><br />
                          <asp:CheckBox ID="CheckBox_InsertPIM6" runat="server" Text="Standard precautions" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Completion
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationCompleted">Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertInvestigationCompleted" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="CheckBox_InsertInvestigationCompleted_CheckedChanged" />
                        </td>
                      </tr>
                      <tr id="ShowHideInvestigationCompletedDate">
                        <td style="width: 175px;" id="FormInvestigationCompletedDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInvestigationCompletedDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertInvestigationCompletedDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertInvestigationCompletedDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertInvestigationCompletedDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertInvestigationCompletedDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertInvestigationCompletedDate" runat="server" TargetControlID="TextBox_InsertInvestigationCompletedDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_OnClick" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Form" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertCaptured" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_InsertCaptured_OnClick" />&nbsp;
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
                        <td style="width: 625px;" colspan="3">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditValidIMedsData" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 625px;" colspan="3">
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditPatientVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditPatientName" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAge">Age
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditAge" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditDateOfAdmission" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormVisitDiagnosis">Visit Diagnosis
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:CheckBoxList ID="CheckBoxList_EditVisitDiagnosis" Width="100%" runat="server" CssClass="Controls_CheckBoxListWithScrollbars" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Table">
                          </asp:CheckBoxList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Infection Detail
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitId">Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditUnitId" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_EditUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateInfectionReported">Date Infection Reported<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDateInfectionReported" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDateInfectionReported" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditDateInfectionReported" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDateInfectionReported" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDateInfectionReported">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDateInfectionReported" runat="server" TargetControlID="TextBox_EditDateInfectionReported" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionType">Infection Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionType" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_EditInfectionType" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditInfectionType_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Infection Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideSSISubType">
                        <td style="width: 175px;" id="FormSSISubType">SSI Sub Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSSISubType" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_EditSSISubType" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                            <asp:ListItem Value="0">Select SSI Sub Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSeverity">Infection Severity
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_EditSeverity" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="200px">
                            <asp:ListItem Text="Major" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Minor" Value="2"></asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Infection
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="8" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditDescription" runat="server" TargetControlID="TextBox_EditDescription" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Surgery / Procedure Details (Current Visit and Visits Selected)
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_EditInfectionPrevention_Surgery" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_EditInfectionPrevention_Surgery_DataBound" OnRowCreated="GridView_EditInfectionPrevention_Surgery_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Surgery
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_EditSurgery" runat="server" Checked='<%# Bind("bSelected") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <table>
                                    <tr>
                                      <td><strong>Facility</strong></td>
                                      <td><strong>Admission Date</strong></td>
                                      <td><strong>Theatre Invoice</strong></td>
                                      <td><strong>Surgeon</strong></td>
                                      <td><strong>Procedure</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Visit Number</strong></td>
                                      <td><strong>Discharge Date</strong></td>
                                      <td><strong>Theatre</strong></td>
                                      <td><strong>Anaesthetist</strong></td>
                                      <td><strong>Scrub Nurse</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Final Diagnosis</strong></td>
                                      <td><strong>Procedure Date</strong></td>
                                      <td><strong>Theatre Time (min)</strong></td>
                                      <td><strong>Assistant</strong></td>
                                      <td><strong>Wound Category</strong></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_EditSurgery_FacilityName" runat="server" Width="180px" Text='<%# Bind("sFacility") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_DateOfAdmission" runat="server" Width="180px" Text='<%# Bind("sAdmissionDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_TheatreInvoice" runat="server" Width="180px" Text='<%# Bind("sTheatreInvoice") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Surgeon" runat="server" Width="180px" Text='<%# Bind("sSurgeon") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Procedure" runat="server" Width="180px" Text='<%# Bind("sProcedure") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_EditSurgery_VisitNumber" runat="server" Width="180px" Text='<%# Bind("sVisitNumber") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_DischargeDate" runat="server" Width="180px" Text='<%# Bind("sDischargeDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Theatre" runat="server" Width="180px" Text='<%# Bind("sTheatre") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Anesthetist" runat="server" Width="180px" Text='<%# Bind("sAnaesthesist") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_ScrubNurse" runat="server" Width="180px" Text='<%# Bind("sScrubNurse") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_EditSurgery_FinalDiagnosis" runat="server" Width="180px" Text='<%# Bind("sFinalDiagnosis") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Date" runat="server" Width="180px" Text='<%# Bind("sSurgeryDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_Duration" runat="server" Width="180px" Text='<%# Bind("sSurgeryDuration") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_AssistantSurgeon" runat="server" Width="180px" Text='<%# Bind("sAssistant") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_EditSurgery_WoundCategory" runat="server" Width="180px" Text='<%# Bind("sWound") %>'></asp:Label></td>
                                    </tr>                                    
                                  </table>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Predisposing Conditions and Contributing Factors
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <table style="width: 100%;">
                            <tr>
                              <td></td>
                              <td>Condition</td>
                              <td>Description</td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF1" runat="server" Text="" /></td>
                              <td>Carcinoma</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF1" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF1" runat="server" TargetControlID="TextBox_EditPCCF1" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF2" runat="server" Text="" /></td>
                              <td>Chronic respiratory disease</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF2" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF2" runat="server" TargetControlID="TextBox_EditPCCF2" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF3" runat="server" Text="" /></td>
                              <td>Diabetes</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF3" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF3" runat="server" TargetControlID="TextBox_EditPCCF3" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF4" runat="server" Text="" /></td>
                              <td>Haematology</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF4" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF4" runat="server" TargetControlID="TextBox_EditPCCF4" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF5" runat="server" Text="" /></td>
                              <td>Hypertention</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF5" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF5" runat="server" TargetControlID="TextBox_EditPCCF5" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF6" runat="server" Text="" /></td>
                              <td>Immune compromised</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF6" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF6" runat="server" TargetControlID="TextBox_EditPCCF6" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF7" runat="server" Text="" /></td>
                              <td>Immune suppressed</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF7" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF7" runat="server" TargetControlID="TextBox_EditPCCF7" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF8" runat="server" Text="" /></td>
                              <td>Obesity</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF8" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF8" runat="server" TargetControlID="TextBox_EditPCCF8" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF9" runat="server" Text="" /></td>
                              <td>Other</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF9" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF9" runat="server" TargetControlID="TextBox_EditPCCF9" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF10" runat="server" Text="" /></td>
                              <td>Prematurity</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF10" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF10" runat="server" TargetControlID="TextBox_EditPCCF10" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF11" runat="server" Text="" /></td>
                              <td>Prolonged hospital stay</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF11" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF11" runat="server" TargetControlID="TextBox_EditPCCF11" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF12" runat="server" Text="" /></td>
                              <td>Prolonged ventilation</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF12" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF12" runat="server" TargetControlID="TextBox_EditPCCF12" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF13" runat="server" Text="" /></td>
                              <td>Prosthetic implants</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF13" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF13" runat="server" TargetControlID="TextBox_EditPCCF13" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF14" runat="server" Text="" /></td>
                              <td>Renal failure</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF14" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF14" runat="server" TargetControlID="TextBox_EditPCCF14" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditPCCF15" runat="server" Text="" /></td>
                              <td>Steroid treatment</td>
                              <td>
                                <asp:TextBox ID="TextBox_EditPCCF15" runat="server" Width="700px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPCCF15" runat="server" TargetControlID="TextBox_EditPCCF15" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bed History
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_EditInfectionPrevention_BedHistory" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_EditInfectionPrevention_BedHistory_DataBound" OnRowCreated="GridView_EditInfectionPrevention_BedHistory_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Bed History
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_EditBedHistory" runat="server" Checked='<%# Bind("bSelected") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="To Unit / Department">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditBedHistory_To" runat="server" Width="600px" Text='<%# Bind("sToUnit") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditBedHistory_Date" runat="server" Width="300px" Text='<%# Bind("sDateTransferred") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable Diseases
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <table style="width: 100%;">
                            <tr>
                              <td></td>
                              <td>Agency / Department</td>
                              <td>Notifiable Disease</td>
                              <td>Date</td>
                              <td>Reference Number</td>
                            </tr>
                            <tr>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditRD" runat="server" Text="" />
                              </td>
                              <td>Department of Health</td>
                              <td>
                                <asp:DropDownList ID="DropDownList_EditRDNotifiableDisease" runat="server" Width="600px" DataSourceID="SqlDataSource_InfectionPrevention_EditRDNotifiableDisease" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList_FixedWidth">
                                  <asp:ListItem Value="">Select Notifiable Disease</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_EditRDDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="ImageButton_EditRDDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                                <Ajax:CalendarExtender ID="CalendarExtender_EditRDDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditRDDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditRDDate">
                                </Ajax:CalendarExtender>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditRDDate" runat="server" TargetControlID="TextBox_EditRDDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_EditRDReferenceNumber" runat="server" Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditRDReferenceNumber" runat="server" TargetControlID="TextBox_EditRDReferenceNumber" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                                </Ajax:FilteredTextBoxExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Laboratory Reports
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_EditInfectionPrevention_LabReport" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_EditInfectionPrevention_LabReport_DataBound" OnRowCreated="GridView_EditInfectionPrevention_LabReport_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Laboratory Reports
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_EditLabReport" runat="server" Checked='<%# Bind("bSelected") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditLabReport_Date" runat="server" Width="225px" Text='<%# Bind("sLabDate") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Specimen">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditLabReport_Specimen" runat="server" Width="225px" Text='<%# Bind("sSpecimen") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Organism">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditLabReport_Organism" runat="server" Width="225px" Text='<%# Bind("sOrganism") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Lab Number">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditLabReport_LabNumber" runat="server" Width="225px" Text='<%# Bind("sLabNumber") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bundle Compliance
                        </td>
                      </tr>
                      <tr id="ShowHideDays">
                        <td style="width: 175px;" id="FormDays">No of Days
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditDays" runat="server" Width="100px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditDays" runat="server" TargetControlID="TextBox_EditDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeSSI" rowspan="4">SSI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeSSI1" runat="server" Text="1.1 If hair is removed, it is only done with clippers or dipilatory cream" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeSSI2" runat="server" Text="1.2 There is proof of antibiotic/s given on the peri-operative document" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeSSI3" runat="server" Text="1.3 Blood glucose maintained between 4 - 10 throughout the ICU/HC stay" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeSSI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeSSI4" runat="server" Text="1.4 Normothermia - temperature is maintained above 36 ̊  - 37.5 ̊  on the first assessment post operatively" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeCLABSI" rowspan="7">CLABSI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI1" runat="server" Text="2.1 Handwashing procedure was followed" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI2" runat="server" Text="2.2 Maximal barrier precautions were used by the doctor as per checklist" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI3" runat="server" Text="2.3 Chlorhexidine skin prep is done and allowed to dry before Edition" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI4" runat="server" Text="2.4 Central line sited in the subclavian vein" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI5">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI5" runat="server" Text="2.5 A daily review is done of the need to keep the line (CVP)" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI6">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI6" runat="server" Text="2.6 The line is properly secured e.g. with a special dressing /device or stitched" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCLABSI7">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCLABSI7" runat="server" Text="2.7 The dressing is visibly clean and intact" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeVAP" rowspan="5">VAP
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeVAP1" runat="server" Text="3.1 The head of the bed is elevated to 30 - 40°" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeVAP2" runat="server" Text="3.2 Sedation vacation - patient has been assessed daily for readiness to extubate" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeVAP3" runat="server" Text="3.3 Peptic ulcer prophylaxis is given" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeVAP4" runat="server" Text="3.4 DVT prophylaxis is given" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeVAP5">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeVAP5" runat="server" Text="3.5 Mouth care is done at least 6 hourly using chlorhexidine mouth wash" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI1">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormTypeCAUTI" rowspan="4">CAUTI
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCAUTI1" runat="server" Text="4.1 A sterile catheter pack was used to Edit the catheter" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI2">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCAUTI2" runat="server" Text="4.2 The catheter is properly secured to avoid pulling" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI3">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCAUTI3" runat="server" Text="4.3 Catheter (perineal) care is done at least twice daily and after every bowel movement using hibiscub and water / chlorhexidine and cetrimide.  A disposeable cloth/cotton wool or gauze may be used.  Note: (bar soap or face cloths are not to used)" />
                        </td>
                      </tr>
                      <tr id="ShowHideTypeCAUTI4">
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditTypeCAUTI4" runat="server" Text="4.4 A daily review is done of the need to keep the catheter insitu" />
                        </td>
                      </tr>
                      <tr id="ShowHideCompliance">
                        <td style="width: 175px;" id="FormCompliance">100% Compliance to all elements
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:RadioButtonList ID="RadioButtonList_EditCompliance" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="200px">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRisk">Related High Risk Procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditRiskTPN" runat="server" Text="TPN" />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:CheckBox ID="CheckBox_EditRiskEnteralFeeding" runat="server" Text="Enteral Feeding" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Section
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDate">Date of Investigation<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditInvestigationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditInvestigationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditInvestigationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditInvestigationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditInvestigationDate" runat="server" TargetControlID="TextBox_EditInvestigationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationName">Investigator Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationName" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInvestigationName" runat="server" TargetControlID="TextBox_EditInvestigationName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDesignation">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationDesignation" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInvestigationDesignation" runat="server" TargetControlID="TextBox_EditInvestigationDesignation" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationIPCSName">IPC / S Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationIPCSName" runat="server" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInvestigationIPCSName" runat="server" TargetControlID="TextBox_EditInvestigationIPCSName" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationTeamMembers">Team Members
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationTeamMembers" runat="server" TextMode="MultiLine" Rows="8" Width="800px" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInvestigationTeamMembers" runat="server" TargetControlID="TextBox_EditInvestigationTeamMembers" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Precautionary / Isolation Measures
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                          <asp:CheckBox ID="CheckBox_EditPIM1" runat="server" Text="Airborne precautions" /><br />
                          <asp:CheckBox ID="CheckBox_EditPIM2" runat="server" Text="Contact precautions" /><br />
                          <asp:CheckBox ID="CheckBox_EditPIM3" runat="server" Text="Droplet precautions" /><br />
                          <asp:CheckBox ID="CheckBox_EditPIM4" runat="server" Text="Formidable epidemic disease" /><br />
                          <asp:CheckBox ID="CheckBox_EditPIM5" runat="server" Text="Protective isolation precautions" /><br />
                          <asp:CheckBox ID="CheckBox_EditPIM6" runat="server" Text="Standard precautions" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Completion
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationCompleted">Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditInvestigationCompleted" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="CheckBox_EditInvestigationCompleted_CheckedChanged" />
                        </td>
                      </tr>
                      <tr id="ShowHideInvestigationCompletedDate">
                        <td style="width: 175px;" id="FormInvestigationCompletedDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationCompletedDate" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditInvestigationCompletedDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditInvestigationCompletedDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditInvestigationCompletedDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditInvestigationCompletedDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditInvestigationCompletedDate" runat="server" TargetControlID="TextBox_EditInvestigationCompletedDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
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
                        <td>Form Status</td>
                        <td colspan="3">
                          <asp:Label ID="Label_EditFormStatus" runat="server" Text="Approved"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                          <asp:Button ID="Button_EditApprove" runat="server" Text="Approve Form" CssClass="Controls_Button" OnClick="Button_EditApprove_OnClick" OnDataBinding="Button_EditApprove_DataBinding" />
                          <asp:Button ID="Button_EditReject" runat="server" Text="Reject Form" CssClass="Controls_Button" OnClick="Button_EditReject_OnClick" OnDataBinding="Button_EditReject_DataBinding" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditCaptured" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_EditCaptured_Click" />&nbsp;
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
                        <td style="width: 625px;" colspan="3">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 625px;" colspan="3">
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientVisitNumber">Visit Number
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPatientName">Name & Surname
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemPatientName" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAge">Age
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemAge" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateOfAdmission">Date of Admission
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDateOfAdmission" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormVisitDiagnosis">Visit Diagnosis
                        </td>
                        <td style="width: 825px; padding: 0px;" colspan="3">
                          <asp:GridView ID="GridView_ItemVisitDiagnosis" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowHeader="false" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemVisitDiagnosis_DataBound" OnRowCreated="GridView_ItemVisitDiagnosis_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Visit Diagnosis
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# (bool)(Eval("bSelected"))?"Yes":"No" %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemVisitDiagnosis" runat="server" Width="600px" Text='<%# GetItemVisitDiagnosis(Eval("sCode"), Eval("sDescription")) %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Infection Detail
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormUnitId">Unit
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemUnitId" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDateInfectionReported">Date Infection Reported<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDateInfectionReported" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionType">Infection Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionType" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemInfectionType" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideSSISubType">
                        <td style="width: 175px;" id="FormSSISubType">SSI Sub Type
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemSSISubType" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSeverity">Infection Severity
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemSeverity" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Infection
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemDescription" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Surgery / Procedure Details (Current Visit and Visits Selected)
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemSurgery" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemSurgery_DataBound" OnRowCreated="GridView_ItemSurgery_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Surgery / Procedure Details
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <table>
                                    <tr>
                                      <td><strong>Facility</strong></td>
                                      <td><strong>Admission Date</strong></td>
                                      <td><strong>Theatre Invoice</strong></td>
                                      <td><strong>Surgeon</strong></td>
                                      <td><strong>Procedure</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Visit Number</strong></td>
                                      <td><strong>Discharge Date</strong></td>
                                      <td><strong>Theatre</strong></td>
                                      <td><strong>Anaesthetist</strong></td>
                                      <td><strong>Scrub Nurse</strong></td>
                                    </tr>
                                    <tr>
                                      <td><strong>Final Diagnosis</strong></td>
                                      <td><strong>Procedure Date</strong></td>
                                      <td><strong>Theatre Time (min)</strong></td>
                                      <td><strong>Assistant</strong></td>
                                      <td><strong>Wound Category</strong></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_ItemSurgery_FacilityName" runat="server" Width="180px" Text='<%# Eval("sFacility") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_DateOfAdmission" runat="server" Width="180px" Text='<%# Eval("sAdmissionDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_TheatreInvoice" runat="server" Width="180px" Text='<%# Eval("sTheatreInvoice") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Surgeon" runat="server" Width="180px" Text='<%# Eval("sSurgeon") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Procedure" runat="server" Width="180px" Text='<%# Eval("sProcedure") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_ItemSurgery_VisitNumber" runat="server" Width="180px" Text='<%# Eval("sVisitNumber") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_DischargeDate" runat="server" Width="180px" Text='<%# Eval("sDischargeDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Theatre" runat="server" Width="180px" Text='<%# Eval("sTheatre") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Anesthetist" runat="server" Width="180px" Text='<%# Eval("sAnaesthesist") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_ScrubNurse" runat="server" Width="180px" Text='<%# Eval("sScrubNurse") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="Label_ItemSurgery_FinalDiagnosis" runat="server" Width="180px" Text='<%# Eval("sFinalDiagnosis") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Date" runat="server" Width="180px" Text='<%# Eval("sSurgeryDate") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_Duration" runat="server" Width="180px" Text='<%# Eval("sSurgeryDuration") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_AssistantSurgeon" runat="server" Width="180px" Text='<%# Eval("sAssistant") %>'></asp:Label></td>
                                      <td><asp:Label ID="Label_ItemSurgery_WoundCategory" runat="server" Width="180px" Text='<%# Eval("sWound") %>'></asp:Label></td>
                                    </tr>                                    
                                  </table>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Predisposing Conditions and Contributing Factors
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemPCCF" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemPCCF_DataBound" OnRowCreated="GridView_ItemPCCF_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Predisposing Conditions and Contributing Factors
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Condition">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemCondition" runat="server" Width="300px" Text='<%# Eval("ListItem_Name") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsDescription" runat="server" Width="600px" Text='<%# Eval("sDescription") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bed History
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemBedHistory" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemBedHistory_DataBound" OnRowCreated="GridView_ItemBedHistory_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Bed History
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="To Unit / Department">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsToUnit" runat="server" Width="700px" Text='<%# Eval("sToUnit") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsDateTransferred" runat="server" Width="200px" Text='<%# Eval("sDateTransferred") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Reportable Diseases
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemReportableDiseases" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemReportableDiseases_DataBound" OnRowCreated="GridView_ItemReportableDiseases_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Reportable Diseases
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Agency / Department">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsReportedToDepartment" runat="server" Width="225px" Text='<%# Eval("sReportedToDepartment") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Notifiable Disease">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsNotifiableDisease" runat="server" Width="225px" Text='<%# Eval("ListItem_Name") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsDateReported" runat="server" Width="225px" Text='<%# Eval("sDateReported") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Reference Number">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsReferenceNumber" runat="server" Width="225px" Text='<%# Eval("sReferenceNumber") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Laboratory Reports
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemLabReport" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemLabReport_DataBound" OnRowCreated="GridView_ItemLabReport_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Laboratory Reports
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsLabDate" runat="server" Width="225px" Text='<%# Eval("sLabDate") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Specimen">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsSpecimen" runat="server" Width="225px" Text='<%# Eval("sSpecimen") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Organism">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsOrganism" runat="server" Width="225px" Text='<%# Eval("sOrganism") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Lab Number">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemsLabNumber" runat="server" Width="225px" Text='<%# Eval("sLabNumber") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bundle Compliance
                        </td>
                      </tr>
                      <tr id="ShowHideDays">
                        <td style="width: 175px;" id="FormDays">No of Days
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_Days" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideBundleType">
                        <td style="width: 175px; vertical-align: middle; text-align: center;" id="FormBundleType">
                          <asp:Label ID="Label_ItemBundleType" runat="server"></asp:Label>&nbsp;
                        </td>
                        <td style="width: 825px; padding:0px;" colspan="3">
                          <asp:GridView ID="GridView_ItemBundleType" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowHeader="false" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemBundleType_DataBound" OnRowCreated="GridView_ItemBundleType_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Bundles
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# Eval("bSelected") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemVisitDiagnosis" runat="server" Width="600px" Text='<%# Eval("ListItem_Name2") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr id="ShowHideCompliance">
                        <td style="width: 175px;" id="FormCompliance">100% Compliance to all elements
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemCompliance" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRisk">Related High Risk Procedure
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemRiskTPN" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Section
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDate">Date of Investigation<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationDate" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationName">Investigator Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationName" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationDesignation">Investigator Designation
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationDesignation" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationIPCSName">IPC / S Name
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationIPCSName" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationTeamMembers">Team Members
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationTeamMembers" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Precautionary / Isolation Measures
                        </td>
                      </tr>
                      <tr>
                        <td style="padding: 0px;" colspan="4">
                          <asp:GridView ID="GridView_ItemPIM" runat="server" AllowPaging="False" Width="100%" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowHeader="false" ShowFooter="False" ShowHeaderWhenEmpty="True" PageSize="1000" OnDataBound="GridView_ItemPIM_DataBound" OnRowCreated="GridView_ItemPIM_RowCreated">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              <table class="GridView_EmptyDataStyle">
                                <tr>
                                  <td>No Precautionary / Isolation Measures
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td style="text-align: center;">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItembSelected" runat="server" Text='<%# (bool)(Eval("bSelected"))?"Yes":"No" %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemVisitDiagnosis" runat="server" Width="600px" Text='<%# Eval("ListItem_Name") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Completed
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInvestigationCompleted">Completed
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationCompleted" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemInvestigationCompleted" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideInvestigationCompletedDate">
                        <td style="width: 175px;" id="FormInvestigationCompletedDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationCompletedDate" runat="server"></asp:Label>&nbsp;
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
                        <td>Form Status</td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemFormStatus" runat="server" Text="Approved"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
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
                          <asp:Button ID="Button_ItemCaptured" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_ItemCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InsertFacility" runat="server" SelectCommand="spAdministration_Execute_Facility_Form" SelectCommandType="StoredProcedure">
                  <SelectParameters>
                    <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                    <asp:Parameter Name="Form_Id" Type="String" DefaultValue="37" />
                    <asp:Parameter Name="Facility_Type" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableSELECT" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableFROM" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableWHERE" Type="String" DefaultValue="0" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InsertUnitId" runat="server" SelectCommand="spAdministration_Execute_Facility_Unit" SelectCommandType="StoredProcedure">
                  <SelectParameters>
                    <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                    <asp:Parameter Name="Form_Id" Type="String" DefaultValue="37" />
                    <asp:QueryStringParameter Name="Facility_Id" Type="String" QueryStringField="s_FacilityId" />
                    <asp:Parameter Name="TableSELECT" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableFROM" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableWHERE" Type="String" DefaultValue="0" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InsertInfectionType" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 13
		AND ListItem_Parent = -1
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InsertSSISubType" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 14
		AND ListItem_Parent = @ListItem_Parent
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name">
                  <SelectParameters>
                    <asp:Parameter Name="ListItem_Parent" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InsertRDNotifiableDisease" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 33
		AND ListItem_Parent = -1
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_EditUnitId" runat="server" SelectCommand="spAdministration_Execute_Facility_Unit" SelectCommandType="StoredProcedure">
                  <SelectParameters>
                    <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                    <asp:Parameter Name="Form_Id" Type="String" DefaultValue="37" />
                    <asp:QueryStringParameter Name="Facility_Id" Type="String" QueryStringField="s_FacilityId" />
                    <asp:Parameter Name="TableSELECT" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableFROM" Type="String" DefaultValue="0" />
                    <asp:Parameter Name="TableWHERE" Type="String" DefaultValue="0" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_EditInfectionType" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 13
		AND ListItem_Parent = -1
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_EditSSISubType" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 14
		AND ListItem_Parent = @ListItem_Parent
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name">
                  <SelectParameters>
                    <asp:Parameter Name="ListItem_Parent" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_Form" runat="server" SelectCommand="SELECT * FROM [tblInfectionPrevention] WHERE ([pkiInfectionFormID] = @pkiInfectionFormID)" InsertCommand="SELECT * FROM [tblInfectionPrevention] WHERE ([pkiInfectionFormID] = @pkiInfectionFormID)" UpdateCommand="SELECT * FROM [tblInfectionPrevention] WHERE ([pkiInfectionFormID] = @pkiInfectionFormID)" ValidateRequestMode="Disabled" >
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="pkiInfectionFormID" QueryStringField="InfectionFormID" Type="Int32" />
                  </SelectParameters>  
                  <InsertParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="pkiInfectionFormID" QueryStringField="InfectionFormID" Type="Int32" />
                  </InsertParameters>
                  <UpdateParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="pkiInfectionFormID" QueryStringField="InfectionFormID" Type="Int32" />
                  </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_EditRDNotifiableDisease" runat="server" SelectCommand="SELECT * FROM (
	SELECT ListItem_Id,ListItem_Name
  FROM vAdministration_ListItem_Active
	WHERE
		Form_Id = 4
		AND ListCategory_Id = 33
		AND ListItem_Parent = -1
) AS TempTableAll ORDER BY TempTableAll.ListItem_Name"></asp:SqlDataSource>
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
