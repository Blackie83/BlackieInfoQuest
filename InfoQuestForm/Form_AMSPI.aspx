<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_AMSPI" CodeBehind="Form_AMSPI.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Antimicrobial Stewardship - Pharmacist Intervention</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/Form_AMSPI.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();">
  <form id="form_AMSPI" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_AMSPI" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
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
                  <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_AMSPI_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                    <asp:ListItem Value="">Select Facility</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_AMSPI_Facility" runat="server"></asp:SqlDataSource>
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
      <table id="TablePatientInfo" class="Table" style="width: 600px;" runat="server">
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
      <table id="TableForm" class="Table" style="width: 600px;" runat="server">
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
            <asp:FormView ID="FormView_AMSPI_Form" runat="server" Width="600px" DataKeyNames="AMSPI_Intervention_Id" CssClass="Record" DataSourceID="SqlDataSource_AMSPI_Form" OnItemInserting="FormView_AMSPI_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_AMSPI_Form_ItemCommand" OnDataBound="FormView_AMSPI_Form_DataBound" OnItemUpdating="FormView_AMSPI_Form_ItemUpdating">
              <InsertItemTemplate>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2">
                      <asp:ValidationSummary ID="ValidationSummary_Form" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="AMSPI_Form" CssClass="Controls_Validation" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("AMSPI_Intervention_ReportNumber") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormDate">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("AMSPI_Intervention_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                    </Ajax:CalendarExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDate" runat="server" ErrorMessage="" ControlToValidate="TextBox_InsertDate" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormUnit">Unit
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_InsertUnit" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Unit_Id") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Unit</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertUnit" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertUnit" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormCommunicationList">Communication method
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_InsertCommunicationList" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertCommunicationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Communication_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Method</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertCommunicationList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertCommunicationList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormTime">Pharmacist time (minutes)
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_InsertTime" runat="server" Width="75px" Text='<%# Bind("AMSPI_Intervention_Time") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertTime" runat="server" TargetControlID="TextBox_InsertTime" FilterType="Numbers">
                      </Ajax:FilteredTextBoxExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertTime" runat="server" ErrorMessage="" ControlToValidate="TextBox_InsertTime" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionList">Intervention suggested
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_InsertInterventionList" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertInterventionList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Intervention_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertInterventionList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertInterventionList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionInList">Intervention location
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_InsertInterventionInList" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertInterventionInList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_InterventionIn_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertInterventionInList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertInterventionInList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType" colspan="2">Type of intervention suggested
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType1" style="width: 175px;">Duration of therapy
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType1" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type1") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType1List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type1_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType1List" OnServerValidate="CustomValidator_InsertType1List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType1List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType2" style="width: 175px;">Dose
                    </td>
                    <td style="width: 425px;">
                      <asp:CheckBox ID="CheckBox_InsertType2" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type2") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType2List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type2_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType2List" OnServerValidate="CustomValidator_InsertType2List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType2List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType3" style="width: 175px;">Frequency of administration
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType3" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type3") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType3List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType3List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type3_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType3List" OnServerValidate="CustomValidator_InsertType3List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType3List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType4" style="width: 175px;">Route of administration
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType4" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type4") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType4List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType4List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type4_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType4List" OnServerValidate="CustomValidator_InsertType4List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType4List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType5" style="width: 175px;">Microbiology requested
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType5" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type5") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType6" style="width: 175px;">Empiric treatment not tailored to Microbiology results
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType6" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type6") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType7" style="width: 175px;">Treatment spectrum duplication (G+ve / G-ve / Antifungal)
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType7" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type7") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType7List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType7List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type7_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType7List" OnServerValidate="CustomValidator_InsertType7List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType7List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType8" style="width: 175px;">Four or more antimicrobials prescribed simultaneously
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType8" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type8") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType9" style="width: 175px;">No clinical signs of infection present
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType9" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type9") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType9List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType9List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type9_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType9List" OnServerValidate="CustomValidator_InsertType9List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType9List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType10" style="width: 175px;">Treatment not in accordance with local guidelines
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType10" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type10") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType10List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType10List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type10_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType10List" OnServerValidate="CustomValidator_InsertType10List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType10List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType11" style="width: 175px;">Medication Safety
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_InsertType11" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type11") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_InsertType11List" runat="server" DataSourceID="SqlDataSource_AMSPI_InsertType11List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type11_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_InsertType11List" OnServerValidate="CustomValidator_InsertType11List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertType11List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="text-align: right;">
                      <strong>Total</strong>
                    </td>
                    <td>
                      <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("AMSPI_Intervention_TypeTotal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 11</strong>
                      <asp:RangeValidator ID="RangeValidator_InsertTotal" runat="server" ValidationGroup="AMSPI_Form" Type="Integer" ControlToValidate="Textbox_InsertTotal" MinimumValue="1" MaximumValue="11"></asp:RangeValidator>&nbsp;
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
                      <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("AMSPI_Intervention_IsActive") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Intervention" CssClass="Controls_Button" ValidationGroup="AMSPI_Form" />&nbsp;
                    </td>
                  </tr>
                </table>
              </InsertItemTemplate>
              <EditItemTemplate>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2">
                      <asp:ValidationSummary ID="ValidationSummary_Form" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="AMSPI_Form" CssClass="Controls_Validation" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td>
                      <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("AMSPI_Intervention_ReportNumber") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormDate">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("AMSPI_Intervention_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                    </Ajax:CalendarExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDate" runat="server" ErrorMessage="" ControlToValidate="TextBox_EditDate" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormUnit">Unit
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_AMSPI_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Unit_Id") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Unit</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditUnit" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditUnit" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormCommunicationList">Communication method
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_EditCommunicationList" runat="server" DataSourceID="SqlDataSource_AMSPI_EditCommunicationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Communication_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Method</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditCommunicationList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditCommunicationList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormTime">Pharmacist time (minutes)
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_EditTime" runat="server" Width="75px" Text='<%# Bind("AMSPI_Intervention_Time") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditTime" runat="server" TargetControlID="TextBox_EditTime" FilterType="Numbers">
                      </Ajax:FilteredTextBoxExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditTime" runat="server" ErrorMessage="" ControlToValidate="TextBox_EditTime" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionList">Intervention suggested
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_EditInterventionList" runat="server" DataSourceID="SqlDataSource_AMSPI_EditInterventionList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Intervention_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditInterventionList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditInterventionList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionInList">Intervention location
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_EditInterventionInList" runat="server" DataSourceID="SqlDataSource_AMSPI_EditInterventionInList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_InterventionIn_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditInterventionInList" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditInterventionInList" ValidationGroup="AMSPI_Form"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType" colspan="2">Type of intervention suggested
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType1" style="width: 175px;">Duration of therapy
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType1" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type1") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType1List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType1List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type1_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType1List" OnServerValidate="CustomValidator_EditType1List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType1List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType2" style="width: 175px;">Dose
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType2" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type2") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType2List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType2List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type2_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType2List" OnServerValidate="CustomValidator_EditType2List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType2List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType3" style="width: 175px;">Frequency of administration
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType3" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type3") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType3List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType3List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type3_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType3List" OnServerValidate="CustomValidator_EditType3List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType3List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType4" style="width: 175px;">Route of administration
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType4" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type4") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType4List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType4List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type4_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType4List" OnServerValidate="CustomValidator_EditType4List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType4List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType5" style="width: 175px;">Microbiology requested
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType5" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type5") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType6" style="width: 175px;">Empiric treatment not tailored to Microbiology results
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType6" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type6") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType7" style="width: 175px;">Treatment spectrum duplication (G+ve/ G-ve/ Antifungal)
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType7" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type7") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType7List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType7List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type7_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType7List" OnServerValidate="CustomValidator_EditType7List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType7List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType8" style="width: 175px;">Four or more antimicrobials prescribed simultaneously
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType8" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type8") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType9" style="width: 175px;">No clinical signs of infection present
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType9" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type9") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType9List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType9List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type9_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType9List" OnServerValidate="CustomValidator_EditType9List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType9List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType10" style="width: 175px;">Treatment not in accordance with local guidelines
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType10" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type10") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType10List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType10List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type10_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType10List" OnServerValidate="CustomValidator_EditType10List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType10List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType11" style="width: 175px;">Medication Safety
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditType11" runat="server" Checked='<%# Bind("AMSPI_Intervention_Type11") %>' />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList_EditType11List" runat="server" DataSourceID="SqlDataSource_AMSPI_EditType11List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("AMSPI_Intervention_Type11_List") %>' CssClass="Controls_DropDownList">
                      <asp:ListItem Value="">Select Value</asp:ListItem>
                    </asp:DropDownList>
                      <asp:CustomValidator ID="CustomValidator_EditType11List" OnServerValidate="CustomValidator_EditType11List_ServerValidate" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditType11List" ValidationGroup="AMSPI_Form" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="text-align: right;">
                      <strong>Total</strong>
                    </td>
                    <td>
                      <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("AMSPI_Intervention_TypeTotal") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox><strong>out of 11</strong>
                      <asp:RangeValidator ID="RangeValidator_EditTotal" runat="server" ValidationGroup="AMSPI_Form" Type="Integer" ControlToValidate="Textbox_EditTotal" MinimumValue="1" MaximumValue="11"></asp:RangeValidator>&nbsp;
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
                      <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("AMSPI_Intervention_IsActive") %>' />&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print Intervention" CssClass="Controls_Button" ValidationGroup="AMSPI_Form" OnClick="Button_EditPrint_Click" />&nbsp;
                    <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" ValidationGroup="AMSPI_Form" OnClick="Button_EditEmail_Click" />&nbsp;
                    <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Intervention" CssClass="Controls_Button" ValidationGroup="AMSPI_Form" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                    <td colspan="2"></td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("AMSPI_Intervention_ReportNumber") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormDate">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("AMSPI_Intervention_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormUnit">Unit
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemUnit" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormCommunicationList">Communication method
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemCommunicationList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormTime">Pharmacist time (minutes)
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemTime" runat="server" Text='<%# Bind("AMSPI_Intervention_Time") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionList">Intervention suggested
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemInterventionList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormInterventionInList">Intervention location
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemInterventionInList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType" colspan="2">Type of intervention suggested
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType1" style="width: 175px;">Duration of therapy
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType1" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type1"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType1List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType2" style="width: 175px;">Dose
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType2" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type2"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType2List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType3" style="width: 175px;">Frequency of administration
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType3" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type3"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType3List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType4" style="width: 175px;">Route of administration
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType4" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type4"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType4List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType5" style="width: 175px;">Microbiology requested
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType5" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type5"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType6" style="width: 175px;">Empiric treatment not tailored to Microbiology results
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType6" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type6"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType7" style="width: 175px;">Treatment spectrum duplication (G+ve/ G-ve/ Antifungal)
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType7" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type7"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType7List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType8" style="width: 175px;">Four or more antimicrobials prescribed simultaneously
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType8" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type8"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType9" style="width: 175px;">No clinical signs of infection present
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType9" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type9"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType9List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType10" style="width: 175px;">Treatment not in accordance with local guidelines
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType10" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type10"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType10List" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td id="FormType11" style="width: 175px;">Medication Safety
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemType11" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_Type11"))?"Yes  :  ":"No" %>'></asp:Label>
                      <asp:Label ID="Label_ItemType11List" runat="server" Text=""></asp:Label>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="text-align: right;">
                      <strong>Total</strong>
                    </td>
                    <td>
                      <strong>
                        <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("AMSPI_Intervention_TypeTotal") %>'></asp:Label>
                        out of 11</strong>&nbsp;
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
                      <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("AMSPI_Intervention_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("AMSPI_Intervention_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Intervention" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                    </td>
                  </tr>
                </table>
              </ItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertUnit" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditUnit" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditCommunicationList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertCommunicationList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditInterventionList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertInterventionList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditInterventionInList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertInterventionInList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType1List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType1List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType2List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType2List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType3List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType3List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType4List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType4List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType7List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType7List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType9List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType9List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType10List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType10List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_EditType11List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_InsertType11List" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_AMSPI_Form" runat="server" OnInserted="SqlDataSource_AMSPI_Form_Inserted" OnUpdated="SqlDataSource_AMSPI_Form_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableList" class="Table" style="width: 600px;" runat="server">
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
                  <asp:GridView ID="GridView_AMSPI_Intervention" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_AMSPI_Intervention" Width="600px" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_AMSPI_Intervention_PreRender" OnDataBound="GridView_AMSPI_Intervention_DataBound" OnRowCreated="GridView_AMSPI_Intervention_RowCreated">
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
                          <%=GridView_AMSPI_Intervention.PageCount%>
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
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Pharmacist Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Pharmacist Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EmptyDataTemplate>
                    <Columns>
                      <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                          <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("AMSPI_Intervention_Id"),Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="AMSPI_Intervention_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="AMSPI_Intervention_ReportNumber" />
                      <asp:BoundField DataField="Unit_Name" HeaderText="Unit" ReadOnly="True" SortExpression="Unit_Name" />
                      <asp:BoundField DataField="AMSPI_Intervention_Date" HeaderText="Date" ReadOnly="True" SortExpression="AMSPI_Intervention_Date" />
                      <asp:BoundField DataField="AMSPI_Intervention_TypeTotal" HeaderText="Total" ReadOnly="True" SortExpression="AMSPI_Intervention_TypeTotal" />
                      <asp:BoundField DataField="AMSPI_Intervention_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="AMSPI_Intervention_IsActive" />
                    </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource_AMSPI_Intervention" runat="server" OnSelected="SqlDataSource_AMSPI_Intervention_Selected"></asp:SqlDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
