<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Isidima" CodeBehind="Form_Isidima.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Isidima</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Isidima.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Validation_Search();Validation_Form();Calculation_Form();">
  <form id="form_Isidima" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Isidima" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td>
            <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Esidimeni/85_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </td>
          <td style="width: 25px"></td>
          <td style="color: #003768; font-size: 18px; font-weight: bold; padding-top: 18px; padding-bottom: 7px">
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
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_SearchHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Record" cellspacing="0" cellpadding="0">
              <tr>
                <td colspan="2">
                  <asp:ValidationSummary ID="ValidationSummary_Find" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Find" CssClass="Controls_Validation" />
                </td>
              </tr>
              <tr class="Controls">
                <td class="th" colspan="2">
                  <asp:Label ID="Label_InvalidSearch" runat="server" CssClass="Controls_Validation"></asp:Label>
                </td>
              </tr>
              <tr>
                <td runat="server" style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; vertical-align: top;">
                  <table width="100%" style="height: 25px" id="SearchFacility">
                    <tr>
                      <td>Facility
                      </td>
                    </tr>
                  </table>
                </td>
                <td style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; padding: 3px;">
                  <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Isidima_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                    <asp:ListItem Value="">Select Facility</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_Facility" runat="server"></asp:SqlDataSource>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator_Facility" runat="server" ErrorMessage="" ControlToValidate="DropDownList_Facility" Display="Dynamic" ValidationGroup="Isidima_Find"></asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr>
                <td runat="server" style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; vertical-align: top;">
                  <table width="100%" style="height: 27px" id="SearchPatientVisitNumber">
                    <tr>
                      <td>Patient Visit Number
                      </td>
                    </tr>
                  </table>
                </td>
                <td style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; padding: 3px;">
                  <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator_PatientVisitNumber" runat="server" ErrorMessage="" ControlToValidate="TextBox_PatientVisitNumber" Display="Dynamic" ValidationGroup="Isidima_Find"></asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr class="Bottom">
                <td colspan="2" style="text-align: right;">
                  <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" CausesValidation="False" />&nbsp;
                <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" ValidationGroup="Isidima_Find" OnClick="Button_Search_Click" />&nbsp;
                </td>
              </tr>
              <tr class="Bottom">
                <td colspan="2" style="text-align: right;">
                  <asp:Button ID="Button_GoToNext" runat="server" Text="Go to Next" CssClass="Controls_Button" OnClick="Button_GoToNext_Click" CausesValidation="False" />&nbsp;
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
      <table id="TablePatientInfo" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_PatientInfoHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table class="Grid" cellspacing="0" cellpadding="0">
              <tr class="Row">
                <td style="width: 115px">Facility:
                </td>
                <td>
                  <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Visit Number:
                </td>
                <td>
                  <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Surname, Name:
                </td>
                <td>
                  <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Age:
                </td>
                <td>
                  <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Date of Admission:
                </td>
                <td>
                  <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Date of Discharge:
                </td>
                <td>
                  <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Ward:
                </td>
                <td>
                  <asp:Label ID="Label_PIWard" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableForm0View" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form0ViewHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table class="Grid" cellspacing="0" cellpadding="0">
              <tr class="Row">
                <td style="width: 115px">Report Number:
                </td>
                <td>
                  <asp:Label ID="Label_FIReportNumber" runat="server" Text=""></asp:Label>&nbsp;
                </td>
                <td style="width: 115px">PatientCategory:
                </td>
                <td>
                  <asp:Label ID="Label_FIPatientCategory" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 115px">Date:
                </td>
                <td>
                  <asp:Label ID="Label_FIDate" runat="server" Text=""></asp:Label>&nbsp;
                </td>
                <td style="width: 115px">Is Active:
                </td>
                <td>
                  <asp:Label ID="Label_FIIsActive" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <div>
              &nbsp;
            </div>
          </td>
        </tr>
      </table>
      <table id="TableForm0" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form0Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form0" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Category_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form0" OnItemInserting="DetailsView_Isidima_Form0_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form0_ItemCommand" OnDataBound="DetailsView_Isidima_Form0_DataBound" OnItemUpdating="DetailsView_Isidima_Form0_ItemUpdating">
              <FieldHeaderStyle Width="175px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form0" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form0" CssClass="Controls_Validation" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Report Number" SortExpression="Isidima_Category_ReportNumber">
                  <ItemStyle BorderWidth="1" />
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("Isidima_Category_ReportNumber") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("Isidima_Category_ReportNumber") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" SortExpression="Isidima_Category_PatientCategory_List">
                  <HeaderTemplate>
                    <table width="102%" id="Form0PatientCategory">
                      <tr>
                        <td>Patient Category
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList_EditPatientCategory" runat="server" DataSourceID="SqlDataSource_Isidima_EditPatientCategory" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Isidima_Category_PatientCategory_List") %>'>
                      <asp:ListItem Value="">Select Category</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPatientCategory" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditPatientCategory" ValidationGroup="Isidima_Form0"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList_InsertPatientCategory" runat="server" DataSourceID="SqlDataSource_Isidima_InsertPatientCategory" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("Isidima_Category_PatientCategory_List") %>'>
                      <asp:ListItem Value="">Select Category</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPatientCategory" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertPatientCategory" ValidationGroup="Isidima_Form0"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ControlStyle CssClass="Controls_DropDownList" />
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" SortExpression="Isidima_Category_Date">
                  <HeaderTemplate>
                    <table width="102%" id="Form0Date">
                      <tr>
                        <td>Date (yyyy/mm/dd)
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <EditItemTemplate>
                    <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("Isidima_Category_Date","{0:yyyy/MM/dd}") %>'></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                  <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                  </Ajax:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDate" runat="server" ErrorMessage="" ControlToValidate="TextBox_EditDate" ValidationGroup="Isidima_Form0"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("Isidima_Category_Date","{0:yyyy/MM/dd}") %>'></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                  <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                  </Ajax:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDate" runat="server" ErrorMessage="" ControlToValidate="TextBox_InsertDate" ValidationGroup="Isidima_Form0"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ControlStyle CssClass="Controls_TextBox" />
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Category_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Category_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Category_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Category_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Category_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Category_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Category_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Category_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Category_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Category_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Category_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Category_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Active" SortExpression="Isidima_Category_IsActive">
                  <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Isidima_Category_IsActive") %>' />
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Isidima_Category_IsActive") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Isidima" CssClass="Controls_Button" ValidationGroup="Isidima_Form0" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Isidima" CssClass="Controls_Button" ValidationGroup="Isidima_Form0" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_EditPatientCategory" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_InsertPatientCategory" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form0" runat="server" OnInserted="SqlDataSource_Isidima_Form0_Inserted" OnUpdated="SqlDataSource_Isidima_Form0_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm1" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form1Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form1" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_MHA_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form1" OnItemInserting="DetailsView_Isidima_Form1_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form1_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form1_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form1" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form1" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_MHA_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A01" SortExpression="Isidima_Section_MHA_A01">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A01">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A01">
                          <asp:Label ID="Label_MHA_A01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A01" runat="server" Text='<%# Bind("Isidima_Section_MHA_A01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A01" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A01" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A02" SortExpression="Isidima_Section_MHA_A02">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A02">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A02">
                          <asp:Label ID="Label_MHA_A02" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A02" runat="server" Text='<%# Bind("Isidima_Section_MHA_A02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A02" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A02" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A03" SortExpression="Isidima_Section_MHA_A03">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A03">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A03">
                          <asp:Label ID="Label_MHA_A03" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A03" runat="server" Text='<%# Bind("Isidima_Section_MHA_A03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A03" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A03" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A04" SortExpression="Isidima_Section_MHA_A04">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A04">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A04">
                          <asp:Label ID="Label_MHA_A04" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A04" runat="server" Text='<%# Bind("Isidima_Section_MHA_A04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A04" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A04" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A05" SortExpression="Isidima_Section_MHA_A05">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A05">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A05">
                          <asp:Label ID="Label_MHA_A05" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A05" runat="server" Text='<%# Bind("Isidima_Section_MHA_A05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A05" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A05" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A06" SortExpression="Isidima_Section_MHA_A06">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A06">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A06">
                          <asp:Label ID="Label_MHA_A06" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A06" runat="server" Text='<%# Bind("Isidima_Section_MHA_A06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A06" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A06" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A07" SortExpression="Isidima_Section_MHA_A07">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A07">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A07">
                          <asp:Label ID="Label_MHA_A07" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A07" runat="server" Text='<%# Bind("Isidima_Section_MHA_A07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A07" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A07" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A08" SortExpression="Isidima_Section_MHA_A08">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A08">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A08">
                          <asp:Label ID="Label_MHA_A08" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A08" runat="server" Text='<%# Bind("Isidima_Section_MHA_A08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A08" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A08" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A09" SortExpression="Isidima_Section_MHA_A09">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A09">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A09">
                          <asp:Label ID="Label_MHA_A09" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A09" runat="server" Text='<%# Bind("Isidima_Section_MHA_A09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A09" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A09" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A10" SortExpression="Isidima_Section_MHA_A10">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A10">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A10">
                          <asp:Label ID="Label_MHA_A10" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A10" runat="server" Text='<%# Bind("Isidima_Section_MHA_A10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A10" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A10" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A11" SortExpression="Isidima_Section_MHA_A11">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A11">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A11">
                          <asp:Label ID="Label_MHA_A11" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A11" runat="server" Text='<%# Bind("Isidima_Section_MHA_A11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A11" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A11" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A12" SortExpression="Isidima_Section_MHA_A12">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A12">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A12">
                          <asp:Label ID="Label_MHA_A12" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A12" runat="server" Text='<%# Bind("Isidima_Section_MHA_A12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A12" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A12" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A13" SortExpression="Isidima_Section_MHA_A13">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A13">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A13">
                          <asp:Label ID="Label_MHA_A13" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A13" runat="server" Text='<%# Bind("Isidima_Section_MHA_A13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A13" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A13" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A14" SortExpression="Isidima_Section_MHA_A14">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A14">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A14">
                          <asp:Label ID="Label_MHA_A14" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A14" runat="server" Text='<%# Bind("Isidima_Section_MHA_A14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A14" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A14" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A15" SortExpression="Isidima_Section_MHA_A15">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A15">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A15">
                          <asp:Label ID="Label_MHA_A15" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A15" runat="server" Text='<%# Bind("Isidima_Section_MHA_A15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A15" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A15" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A16" SortExpression="Isidima_Section_MHA_A16">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A16">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A16">
                          <asp:Label ID="Label_MHA_A16" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A16" runat="server" Text='<%# Bind("Isidima_Section_MHA_A16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A16" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A16" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A17" SortExpression="Isidima_Section_MHA_A17">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A17">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A17">
                          <asp:Label ID="Label_MHA_A17" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A17" runat="server" Text='<%# Bind("Isidima_Section_MHA_A17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A17" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A17" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A18" SortExpression="Isidima_Section_MHA_A18">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A18">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A18">
                          <asp:Label ID="Label_MHA_A18" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A18" runat="server" Text='<%# Bind("Isidima_Section_MHA_A18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A18" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A18" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MHA_A19" SortExpression="Isidima_Section_MHA_A19">
                  <HeaderTemplate>
                    <table width="102%" id="Form1MHA_A19">
                      <tr>
                        <td style="vertical-align: middle;" id="MHA_A19">
                          <asp:Label ID="Label_MHA_A19" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_MHA_A19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_MHA_A19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemMHA_A19" runat="server" Text='<%# Bind("Isidima_Section_MHA_A19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditMHA_A19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditMHA_A19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditMHA_A19" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertMHA_A19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_MHA_A19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertMHA_A19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertMHA_A19" ValidationGroup="Isidima_Form1"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total MHA Score" SortExpression="Isidima_Section_MHA_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_MHA_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_MHA_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_MHA_Total") %>'></asp:Label><strong> out of 30</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_MHA_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_MHA_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_MHA_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_MHA_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_MHA_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Administration (MHA)" CssClass="Controls_Button" ValidationGroup="Isidima_Form1" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Administration (MHA)" CssClass="Controls_Button" ValidationGroup="Isidima_Form1" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form1" runat="server" OnInserted="SqlDataSource_Isidima_Form1_Inserted" OnUpdated="SqlDataSource_Isidima_Form1_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm2" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form2Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form2" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_VPA_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form2" OnItemInserting="DetailsView_Isidima_Form2_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form2_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form2_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form2" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form2" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_VPA_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A01" SortExpression="Isidima_Section_VPA_A01">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A01">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A01">
                          <asp:Label ID="Label_VPA_A01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A01" runat="server" Text='<%# Bind("Isidima_Section_VPA_A01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A01" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A01" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A02" SortExpression="Isidima_Section_VPA_A02">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A02">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A02">
                          <asp:Label ID="Label_VPA_A02" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A02" runat="server" Text='<%# Bind("Isidima_Section_VPA_A02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A02" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A02" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A03" SortExpression="Isidima_Section_VPA_A03">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A03">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A03">
                          <asp:Label ID="Label_VPA_A03" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A03" runat="server" Text='<%# Bind("Isidima_Section_VPA_A03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A03" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A03" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A04" SortExpression="Isidima_Section_VPA_A04">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A04">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A04">
                          <asp:Label ID="Label_VPA_A04" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A04" runat="server" Text='<%# Bind("Isidima_Section_VPA_A04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A04" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A04" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A05" SortExpression="Isidima_Section_VPA_A05">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A05">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A05">
                          <asp:Label ID="Label_VPA_A05" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A05" runat="server" Text='<%# Bind("Isidima_Section_VPA_A05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A05" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A05" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A06" SortExpression="Isidima_Section_VPA_A06">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A06">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A06">
                          <asp:Label ID="Label_VPA_A06" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A06" runat="server" Text='<%# Bind("Isidima_Section_VPA_A06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A06" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A06" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A07" SortExpression="Isidima_Section_VPA_A07">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A07">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A07">
                          <asp:Label ID="Label_VPA_A07" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A07" runat="server" Text='<%# Bind("Isidima_Section_VPA_A07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A07" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A07" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A08" SortExpression="Isidima_Section_VPA_A08">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A08">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A08">
                          <asp:Label ID="Label_VPA_A08" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A08" runat="server" Text='<%# Bind("Isidima_Section_VPA_A08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A08" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A08" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A09" SortExpression="Isidima_Section_VPA_A09">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A09">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A09">
                          <asp:Label ID="Label_VPA_A09" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A09" runat="server" Text='<%# Bind("Isidima_Section_VPA_A09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A09" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A09" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A10" SortExpression="Isidima_Section_VPA_A10">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A10">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A10">
                          <asp:Label ID="Label_VPA_A10" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A10" runat="server" Text='<%# Bind("Isidima_Section_VPA_A10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A10" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A10" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A11" SortExpression="Isidima_Section_VPA_A11">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A11">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A11">
                          <asp:Label ID="Label_VPA_A11" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A11" runat="server" Text='<%# Bind("Isidima_Section_VPA_A11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A11" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A11" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A12" SortExpression="Isidima_Section_VPA_A12">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A12">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A12">
                          <asp:Label ID="Label_VPA_A12" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A12" runat="server" Text='<%# Bind("Isidima_Section_VPA_A12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A12" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A12" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VPA_A13" SortExpression="Isidima_Section_VPA_A13">
                  <HeaderTemplate>
                    <table width="102%" id="Form2VPA_A13">
                      <tr>
                        <td style="vertical-align: middle;" id="VPA_A13">
                          <asp:Label ID="Label_VPA_A13" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_VPA_A13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_VPA_A13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemVPA_A13" runat="server" Text='<%# Bind("Isidima_Section_VPA_A13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditVPA_A13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditVPA_A13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditVPA_A13" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertVPA_A13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_VPA_A13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertVPA_A13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertVPA_A13" ValidationGroup="Isidima_Form2"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total VPA Score" SortExpression="Isidima_Section_VPA_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_VPA_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 18</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_VPA_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 18</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_VPA_Total") %>'></asp:Label><strong> out of 18</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_VPA_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_VPA_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_VPA_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_VPA_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_VPA_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Administration (VPA)" CssClass="Controls_Button" ValidationGroup="Isidima_Form2" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Administration (VPA)" CssClass="Controls_Button" ValidationGroup="Isidima_Form2" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form2" runat="server" OnInserted="SqlDataSource_Isidima_Form2_Inserted" OnUpdated="SqlDataSource_Isidima_Form2_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm3" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form3Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form3" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_J_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form3" OnItemInserting="DetailsView_Isidima_Form3_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form3_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form3_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form3" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form3" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_J_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J01" SortExpression="Isidima_Section_J_J01">
                  <HeaderTemplate>
                    <table width="102%" id="Form3J_J01">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J01">
                          <asp:Label ID="Label_J_J01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J01" runat="server" Text='<%# Bind("Isidima_Section_J_J01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J01" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J01" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J02" SortExpression="Isidima_Section_J_J02">
                  <HeaderTemplate>
                    <table width="102%" id="Form3J_J02">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J02">
                          <asp:Label ID="Label_J_J02" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J02" runat="server" Text='<%# Bind("Isidima_Section_J_J02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J02" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J02" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J03" SortExpression="Isidima_Section_J_J03">
                  <HeaderTemplate>
                    <table width="103%" id="Form3J_J03">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J03">
                          <asp:Label ID="Label_J_J03" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J03" runat="server" Text='<%# Bind("Isidima_Section_J_J03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J03" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J03" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J04" SortExpression="Isidima_Section_J_J04">
                  <HeaderTemplate>
                    <table width="104%" id="Form3J_J04">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J04">
                          <asp:Label ID="Label_J_J04" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J04" runat="server" Text='<%# Bind("Isidima_Section_J_J04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J04" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J04" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J05" SortExpression="Isidima_Section_J_J05">
                  <HeaderTemplate>
                    <table width="105%" id="Form3J_J05">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J05">
                          <asp:Label ID="Label_J_J05" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J05" runat="server" Text='<%# Bind("Isidima_Section_J_J05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J05" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J05" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J06" SortExpression="Isidima_Section_J_J06">
                  <HeaderTemplate>
                    <table width="106%" id="Form3J_J06">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J06">
                          <asp:Label ID="Label_J_J06" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J06" runat="server" Text='<%# Bind("Isidima_Section_J_J06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J06" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J06" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J07" SortExpression="Isidima_Section_J_J07">
                  <HeaderTemplate>
                    <table width="107%" id="Form3J_J07">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J07">
                          <asp:Label ID="Label_J_J07" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J07" runat="server" Text='<%# Bind("Isidima_Section_J_J07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J07" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J07" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J08" SortExpression="Isidima_Section_J_J08">
                  <HeaderTemplate>
                    <table width="108%" id="Form3J_J08">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J08">
                          <asp:Label ID="Label_J_J08" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J08" runat="server" Text='<%# Bind("Isidima_Section_J_J08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J08" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J08" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J09" SortExpression="Isidima_Section_J_J09">
                  <HeaderTemplate>
                    <table width="109%" id="Form3J_J09">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J09">
                          <asp:Label ID="Label_J_J09" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J09" runat="server" Text='<%# Bind("Isidima_Section_J_J09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J09" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J09" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J10" SortExpression="Isidima_Section_J_J10">
                  <HeaderTemplate>
                    <table width="110%" id="Form3J_J10">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J10">
                          <asp:Label ID="Label_J_J10" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J10" runat="server" Text='<%# Bind("Isidima_Section_J_J10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J10" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J10" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J11" SortExpression="Isidima_Section_J_J11">
                  <HeaderTemplate>
                    <table width="111%" id="Form3J_J11">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J11">
                          <asp:Label ID="Label_J_J11" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J11" runat="server" Text='<%# Bind("Isidima_Section_J_J11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J11" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J11" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J12" SortExpression="Isidima_Section_J_J12">
                  <HeaderTemplate>
                    <table width="112%" id="Form3J_J12">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J12">
                          <asp:Label ID="Label_J_J12" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J12" runat="server" Text='<%# Bind("Isidima_Section_J_J12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J12" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J12" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J13" SortExpression="Isidima_Section_J_J13">
                  <HeaderTemplate>
                    <table width="113%" id="Form3J_J13">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J13">
                          <asp:Label ID="Label_J_J13" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J13" runat="server" Text='<%# Bind("Isidima_Section_J_J13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J13" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J13" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J14" SortExpression="Isidima_Section_J_J14">
                  <HeaderTemplate>
                    <table width="114%" id="Form3J_J14">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J14">
                          <asp:Label ID="Label_J_J14" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J14" runat="server" Text='<%# Bind("Isidima_Section_J_J14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J14" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J14" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J15" SortExpression="Isidima_Section_J_J15">
                  <HeaderTemplate>
                    <table width="115%" id="Form3J_J15">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J15">
                          <asp:Label ID="Label_J_J15" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J15" runat="server" Text='<%# Bind("Isidima_Section_J_J15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J15" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J15" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J16" SortExpression="Isidima_Section_J_J16">
                  <HeaderTemplate>
                    <table width="116%" id="Form3J_J16">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J16">
                          <asp:Label ID="Label_J_J16" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J16" runat="server" Text='<%# Bind("Isidima_Section_J_J16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J16" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J16" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J17" SortExpression="Isidima_Section_J_J17">
                  <HeaderTemplate>
                    <table width="117%" id="Form3J_J17">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J17">
                          <asp:Label ID="Label_J_J17" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J17" runat="server" Text='<%# Bind("Isidima_Section_J_J17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J17" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J17" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J18" SortExpression="Isidima_Section_J_J18">
                  <HeaderTemplate>
                    <table width="118%" id="Form3J_J18">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J18">
                          <asp:Label ID="Label_J_J18" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J18" runat="server" Text='<%# Bind("Isidima_Section_J_J18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J18" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J18" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J19" SortExpression="Isidima_Section_J_J19">
                  <HeaderTemplate>
                    <table width="119%" id="Form3J_J19">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J19">
                          <asp:Label ID="Label_J_J19" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J19" runat="server" Text='<%# Bind("Isidima_Section_J_J19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J19" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J19" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J20" SortExpression="Isidima_Section_J_J20">
                  <HeaderTemplate>
                    <table width="120%" id="Form3J_J20">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J20">
                          <asp:Label ID="Label_J_J20" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J20" runat="server" Text='<%# Bind("Isidima_Section_J_J20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J20" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J20" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J21" SortExpression="Isidima_Section_J_J21">
                  <HeaderTemplate>
                    <table width="121%" id="Form3J_J21">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J21">
                          <asp:Label ID="Label_J_J21" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J21" runat="server" Text='<%# Bind("Isidima_Section_J_J21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J21" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J21" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J22" SortExpression="Isidima_Section_J_J22">
                  <HeaderTemplate>
                    <table width="122%" id="Form3J_J22">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J22">
                          <asp:Label ID="Label_J_J22" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J22" runat="server" Text='<%# Bind("Isidima_Section_J_J22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J22" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J22" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J23" SortExpression="Isidima_Section_J_J23">
                  <HeaderTemplate>
                    <table width="123%" id="Form3J_J23">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J23">
                          <asp:Label ID="Label_J_J23" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J23" runat="server" Text='<%# Bind("Isidima_Section_J_J23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J23" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J23" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J24" SortExpression="Isidima_Section_J_J24">
                  <HeaderTemplate>
                    <table width="124%" id="Form3J_J24">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J24">
                          <asp:Label ID="Label_J_J24" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J24Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J24No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J24" runat="server" Text='<%# Bind("Isidima_Section_J_J24") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J24" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J24" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J25" SortExpression="Isidima_Section_J_J25">
                  <HeaderTemplate>
                    <table width="125%" id="Form3J_J25">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J25">
                          <asp:Label ID="Label_J_J25" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J25Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J25No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J25" runat="server" Text='<%# Bind("Isidima_Section_J_J25") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J25" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J25" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="J_J26" SortExpression="Isidima_Section_J_J26">
                  <HeaderTemplate>
                    <table width="126%" id="Form3J_J26">
                      <tr>
                        <td style="vertical-align: middle;" id="J_J26">
                          <asp:Label ID="Label_J_J26" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_J_J26Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_J_J26No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemJ_J26" runat="server" Text='<%# Bind("Isidima_Section_J_J26") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditJ_J26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditJ_J26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditJ_J26" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertJ_J26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_J_J26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertJ_J26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertJ_J26" ValidationGroup="Isidima_Form3"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total J Score" SortExpression="Isidima_Section_J_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_J_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 34</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_J_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 34</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_J_Total") %>'></asp:Label><strong> out of 34</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_J_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_J_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_J_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_J_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_J_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_J_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Child (J)" CssClass="Controls_Button" ValidationGroup="Isidima_Form3" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Child (J)" CssClass="Controls_Button" ValidationGroup="Isidima_Form3" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form3" runat="server" OnInserted="SqlDataSource_Isidima_Form3_Inserted" OnUpdated="SqlDataSource_Isidima_Form3_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm4" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form4Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form4" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_DMH_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form4" OnItemInserting="DetailsView_Isidima_Form4_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form4_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form4_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form4" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form4" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_DMH_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S01" SortExpression="Isidima_Section_DMH_S01">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S01">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S01">
                          <asp:Label ID="Label_DMH_S01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S01" runat="server" Text='<%# Bind("Isidima_Section_DMH_S01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S01" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S01" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S02" SortExpression="Isidima_Section_DMH_S02">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S02">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S02">
                          <asp:Label ID="Label_DMH_S02" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S02" runat="server" Text='<%# Bind("Isidima_Section_DMH_S02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S02" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S02" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S03" SortExpression="Isidima_Section_DMH_S03">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S03">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S03">
                          <asp:Label ID="Label_DMH_S03" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S03" runat="server" Text='<%# Bind("Isidima_Section_DMH_S03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S03" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S03" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S04" SortExpression="Isidima_Section_DMH_S04">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S04">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S04">
                          <asp:Label ID="Label_DMH_S04" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S04" runat="server" Text='<%# Bind("Isidima_Section_DMH_S04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S04" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S04" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S05" SortExpression="Isidima_Section_DMH_S05">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S05">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S05">
                          <asp:Label ID="Label_DMH_S05" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S05" runat="server" Text='<%# Bind("Isidima_Section_DMH_S05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S05" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S05" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S06" SortExpression="Isidima_Section_DMH_S06">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S06">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S06">
                          <asp:Label ID="Label_DMH_S06" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S06" runat="server" Text='<%# Bind("Isidima_Section_DMH_S06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S06" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S06" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S07" SortExpression="Isidima_Section_DMH_S07">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S07">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S07">
                          <asp:Label ID="Label_DMH_S07" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S07" runat="server" Text='<%# Bind("Isidima_Section_DMH_S07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S07" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S07" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S08" SortExpression="Isidima_Section_DMH_S08">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S08">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S08">
                          <asp:Label ID="Label_DMH_S08" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S08" runat="server" Text='<%# Bind("Isidima_Section_DMH_S08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S08" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S08" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S09" SortExpression="Isidima_Section_DMH_S09">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S09">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S09">
                          <asp:Label ID="Label_DMH_S09" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S09" runat="server" Text='<%# Bind("Isidima_Section_DMH_S09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S09" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S09" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S10" SortExpression="Isidima_Section_DMH_S10">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S10">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S10">
                          <asp:Label ID="Label_DMH_S10" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S10" runat="server" Text='<%# Bind("Isidima_Section_DMH_S10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S10" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S10" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S11" SortExpression="Isidima_Section_DMH_S11">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S11">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S11">
                          <asp:Label ID="Label_DMH_S11" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S11" runat="server" Text='<%# Bind("Isidima_Section_DMH_S11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S11" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S11" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S12" SortExpression="Isidima_Section_DMH_S12">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S12">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S12">
                          <asp:Label ID="Label_DMH_S12" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S12" runat="server" Text='<%# Bind("Isidima_Section_DMH_S12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S12" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S12" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S13" SortExpression="Isidima_Section_DMH_S13">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S13">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S13">
                          <asp:Label ID="Label_DMH_S13" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S13" runat="server" Text='<%# Bind("Isidima_Section_DMH_S13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S13" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S13" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S14" SortExpression="Isidima_Section_DMH_S14">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S14">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S14">
                          <asp:Label ID="Label_DMH_S14" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S14" runat="server" Text='<%# Bind("Isidima_Section_DMH_S14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S14" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S14" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S15" SortExpression="Isidima_Section_DMH_S15">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S15">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S15">
                          <asp:Label ID="Label_DMH_S15" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S15" runat="server" Text='<%# Bind("Isidima_Section_DMH_S15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S15" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S15" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S16" SortExpression="Isidima_Section_DMH_S16">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S16">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S16">
                          <asp:Label ID="Label_DMH_S16" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S16" runat="server" Text='<%# Bind("Isidima_Section_DMH_S16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S16" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S16" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S17" SortExpression="Isidima_Section_DMH_S17">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S17">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S17">
                          <asp:Label ID="Label_DMH_S17" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S17" runat="server" Text='<%# Bind("Isidima_Section_DMH_S17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S17" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S17" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S18" SortExpression="Isidima_Section_DMH_S18">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S18">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S18">
                          <asp:Label ID="Label_DMH_S18" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S18" runat="server" Text='<%# Bind("Isidima_Section_DMH_S18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S18" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S18" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S19" SortExpression="Isidima_Section_DMH_S19">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S19">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S19">
                          <asp:Label ID="Label_DMH_S19" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S19" runat="server" Text='<%# Bind("Isidima_Section_DMH_S19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S19" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S19" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S20" SortExpression="Isidima_Section_DMH_S20">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S20">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S20">
                          <asp:Label ID="Label_DMH_S20" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S20" runat="server" Text='<%# Bind("Isidima_Section_DMH_S20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S20" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S20" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S21" SortExpression="Isidima_Section_DMH_S21">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S21">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S21">
                          <asp:Label ID="Label_DMH_S21" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S21" runat="server" Text='<%# Bind("Isidima_Section_DMH_S21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S21" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S21" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMH_S22" SortExpression="Isidima_Section_DMH_S22">
                  <HeaderTemplate>
                    <table width="102%" id="Form4DMH_S22">
                      <tr>
                        <td style="vertical-align: middle;" id="DMH_S22">
                          <asp:Label ID="Label_DMH_S22" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_DMH_S22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_DMH_S22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemDMH_S22" runat="server" Text='<%# Bind("Isidima_Section_DMH_S22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditDMH_S22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditDMH_S22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditDMH_S22" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertDMH_S22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_DMH_S22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertDMH_S22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertDMH_S22" ValidationGroup="Isidima_Form4"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total DMH Score" SortExpression="Isidima_Section_DMH_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_DMH_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_DMH_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_DMH_Total") %>'></asp:Label><strong> out of 30</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_DMH_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_DMH_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_DMH_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_DMH_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_DMH_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Discharge (DMH)" CssClass="Controls_Button" ValidationGroup="Isidima_Form4" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Discharge (DMH)" CssClass="Controls_Button" ValidationGroup="Isidima_Form4" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form4" runat="server" OnInserted="SqlDataSource_Isidima_Form4_Inserted" OnUpdated="SqlDataSource_Isidima_Form4_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm5" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form5Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form5" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_F_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form5" OnItemInserting="DetailsView_Isidima_Form5_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form5_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form5_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form5" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form5" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_F_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F01" SortExpression="Isidima_Section_F_F01">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F01">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F01">
                          <asp:Label ID="Label_F_F01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F01" runat="server" Text='<%# Bind("Isidima_Section_F_F01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F01" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F01" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F02" SortExpression="Isidima_Section_F_F02">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F02">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F02">
                          <asp:Label ID="Label_F_F02" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F02" runat="server" Text='<%# Bind("Isidima_Section_F_F02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F02" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F02" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F03" SortExpression="Isidima_Section_F_F03">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F03">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F03">
                          <asp:Label ID="Label_F_F03" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F03" runat="server" Text='<%# Bind("Isidima_Section_F_F03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F03" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F03" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F04" SortExpression="Isidima_Section_F_F04">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F04">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F04">
                          <asp:Label ID="Label_F_F04" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F04" runat="server" Text='<%# Bind("Isidima_Section_F_F04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F04" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F04" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F05" SortExpression="Isidima_Section_F_F05">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F05">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F05">
                          <asp:Label ID="Label_F_F05" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F05" runat="server" Text='<%# Bind("Isidima_Section_F_F05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F05" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F05" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F06" SortExpression="Isidima_Section_F_F06">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F06">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F06">
                          <asp:Label ID="Label_F_F06" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F06" runat="server" Text='<%# Bind("Isidima_Section_F_F06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F06" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F06" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F07" SortExpression="Isidima_Section_F_F07">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F07">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F07">
                          <asp:Label ID="Label_F_F07" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F07" runat="server" Text='<%# Bind("Isidima_Section_F_F07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F07" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F07" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F08" SortExpression="Isidima_Section_F_F08">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F08">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F08">
                          <asp:Label ID="Label_F_F08" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F08" runat="server" Text='<%# Bind("Isidima_Section_F_F08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F08" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F08" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F09" SortExpression="Isidima_Section_F_F09">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F09">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F09">
                          <asp:Label ID="Label_F_F09" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F09" runat="server" Text='<%# Bind("Isidima_Section_F_F09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F09" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F09" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F10" SortExpression="Isidima_Section_F_F10">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F10">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F10">
                          <asp:Label ID="Label_F_F10" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F10" runat="server" Text='<%# Bind("Isidima_Section_F_F10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F10" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F10" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F11" SortExpression="Isidima_Section_F_F11">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F11">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F11">
                          <asp:Label ID="Label_F_F11" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_F_F11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F11" runat="server" Text='<%# Bind("Isidima_Section_F_F11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F11" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F11" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F12" SortExpression="Isidima_Section_F_F12">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F12">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F12">
                          <asp:Label ID="Label_F_F12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F12" runat="server" Text='<%# Bind("Isidima_Section_F_F12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F12" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F12" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F13" SortExpression="Isidima_Section_F_F13">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F13">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F13">
                          <asp:Label ID="Label_F_F13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F13" runat="server" Text='<%# Bind("Isidima_Section_F_F13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F13" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F13" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F14" SortExpression="Isidima_Section_F_F14">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F14">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F14">
                          <asp:Label ID="Label_F_F14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F14" runat="server" Text='<%# Bind("Isidima_Section_F_F14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F14" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F14" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F15" SortExpression="Isidima_Section_F_F15">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F15">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F15">
                          <asp:Label ID="Label_F_F15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F15" runat="server" Text='<%# Bind("Isidima_Section_F_F15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F15" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F15" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F16" SortExpression="Isidima_Section_F_F16">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F16">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F16">
                          <asp:Label ID="Label_F_F16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F16" runat="server" Text='<%# Bind("Isidima_Section_F_F16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F16" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F16" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F17" SortExpression="Isidima_Section_F_F17">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F17">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F17">
                          <asp:Label ID="Label_F_F17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F17" runat="server" Text='<%# Bind("Isidima_Section_F_F17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F17" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F17" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F18" SortExpression="Isidima_Section_F_F18">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F18">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F18">
                          <asp:Label ID="Label_F_F18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F18" runat="server" Text='<%# Bind("Isidima_Section_F_F18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F18" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F18" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F19" SortExpression="Isidima_Section_F_F19">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F19">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F19">
                          <asp:Label ID="Label_F_F19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F19" runat="server" Text='<%# Bind("Isidima_Section_F_F19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F19" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F19" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F20" SortExpression="Isidima_Section_F_F20">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F20">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F20">
                          <asp:Label ID="Label_F_F20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F20" runat="server" Text='<%# Bind("Isidima_Section_F_F20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F20" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F20" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F21" SortExpression="Isidima_Section_F_F21">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F21">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F21">
                          <asp:Label ID="Label_F_F21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F21" runat="server" Text='<%# Bind("Isidima_Section_F_F21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F21" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F21" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="F_F22" SortExpression="Isidima_Section_F_F22">
                  <HeaderTemplate>
                    <table width="102%" id="Form5F_F22">
                      <tr>
                        <td style="vertical-align: middle;" id="F_F22">
                          <asp:Label ID="Label_F_F22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_F_F22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_F_F22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemF_F22" runat="server" Text='<%# Bind("Isidima_Section_F_F22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditF_F22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditF_F22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditF_F22" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertF_F22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_F_F22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertF_F22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertF_F22" ValidationGroup="Isidima_Form5"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total F Score" SortExpression="Isidima_Section_F_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_F_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 27</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_F_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 27</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_F_Total") %>'></asp:Label><strong> out of 27</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_F_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_F_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_F_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_F_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_F_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_F_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Function (F)" CssClass="Controls_Button" ValidationGroup="Isidima_Form5" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Function (F)" CssClass="Controls_Button" ValidationGroup="Isidima_Form5" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form5" runat="server" OnInserted="SqlDataSource_Isidima_Form5_Inserted" OnUpdated="SqlDataSource_Isidima_Form5_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm6" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form6Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form6" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_I_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form6" OnItemInserting="DetailsView_Isidima_Form6_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form6_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form6_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form6" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form6" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_I_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I01" SortExpression="Isidima_Section_I_I01">
                  <HeaderTemplate>
                    <table width="102%" id="Form6I_I01">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I01">
                          <asp:Label ID="Label_I_I01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_I_I01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I01" runat="server" Text='<%# Bind("Isidima_Section_I_I01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I01" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I01" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I02" SortExpression="Isidima_Section_I_I02">
                  <HeaderTemplate>
                    <table width="102%" id="Form6I_I02">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I02">
                          <asp:Label ID="Label_I_I02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I02" runat="server" Text='<%# Bind("Isidima_Section_I_I02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I02" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I02" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I03" SortExpression="Isidima_Section_I_I03">
                  <HeaderTemplate>
                    <table width="103%" id="Form6I_I03">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I03">
                          <asp:Label ID="Label_I_I03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I03" runat="server" Text='<%# Bind("Isidima_Section_I_I03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I03" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I03" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I04" SortExpression="Isidima_Section_I_I04">
                  <HeaderTemplate>
                    <table width="104%" id="Form6I_I04">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I04">
                          <asp:Label ID="Label_I_I04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I04" runat="server" Text='<%# Bind("Isidima_Section_I_I04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I04" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I04" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I05" SortExpression="Isidima_Section_I_I05">
                  <HeaderTemplate>
                    <table width="105%" id="Form6I_I05">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I05">
                          <asp:Label ID="Label_I_I05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I05" runat="server" Text='<%# Bind("Isidima_Section_I_I05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I05" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I05" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I06" SortExpression="Isidima_Section_I_I06">
                  <HeaderTemplate>
                    <table width="106%" id="Form6I_I06">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I06">
                          <asp:Label ID="Label_I_I06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I06" runat="server" Text='<%# Bind("Isidima_Section_I_I06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I06" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I06" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I07" SortExpression="Isidima_Section_I_I07">
                  <HeaderTemplate>
                    <table width="107%" id="Form6I_I07">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I07">
                          <asp:Label ID="Label_I_I07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I07" runat="server" Text='<%# Bind("Isidima_Section_I_I07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I07" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I07" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I08" SortExpression="Isidima_Section_I_I08">
                  <HeaderTemplate>
                    <table width="108%" id="Form6I_I08">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I08">
                          <asp:Label ID="Label_I_I08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I08" runat="server" Text='<%# Bind("Isidima_Section_I_I08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I08" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I08" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I09" SortExpression="Isidima_Section_I_I09">
                  <HeaderTemplate>
                    <table width="109%" id="Form6I_I09">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I09">
                          <asp:Label ID="Label_I_I09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I09" runat="server" Text='<%# Bind("Isidima_Section_I_I09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I09" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I09" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I10" SortExpression="Isidima_Section_I_I10">
                  <HeaderTemplate>
                    <table width="110%" id="Form6I_I10">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I10">
                          <asp:Label ID="Label_I_I10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I10" runat="server" Text='<%# Bind("Isidima_Section_I_I10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I10" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I10" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I11" SortExpression="Isidima_Section_I_I11">
                  <HeaderTemplate>
                    <table width="111%" id="Form6I_I11">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I11">
                          <asp:Label ID="Label_I_I11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I11" runat="server" Text='<%# Bind("Isidima_Section_I_I11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I11" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I11" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I12" SortExpression="Isidima_Section_I_I12">
                  <HeaderTemplate>
                    <table width="112%" id="Form6I_I12">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I12">
                          <asp:Label ID="Label_I_I12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I12" runat="server" Text='<%# Bind("Isidima_Section_I_I12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I12" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I12" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I13" SortExpression="Isidima_Section_I_I13">
                  <HeaderTemplate>
                    <table width="113%" id="Form6I_I13">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I13">
                          <asp:Label ID="Label_I_I13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I13" runat="server" Text='<%# Bind("Isidima_Section_I_I13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I13" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I13" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I14" SortExpression="Isidima_Section_I_I14">
                  <HeaderTemplate>
                    <table width="114%" id="Form6I_I14">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I14">
                          <asp:Label ID="Label_I_I14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I14" runat="server" Text='<%# Bind("Isidima_Section_I_I14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I14" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I14" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I15" SortExpression="Isidima_Section_I_I15">
                  <HeaderTemplate>
                    <table width="115%" id="Form6I_I15">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I15">
                          <asp:Label ID="Label_I_I15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I15" runat="server" Text='<%# Bind("Isidima_Section_I_I15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I15" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I15" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I16" SortExpression="Isidima_Section_I_I16">
                  <HeaderTemplate>
                    <table width="116%" id="Form6I_I16">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I16">
                          <asp:Label ID="Label_I_I16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I16" runat="server" Text='<%# Bind("Isidima_Section_I_I16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I16" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I16" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I17" SortExpression="Isidima_Section_I_I17">
                  <HeaderTemplate>
                    <table width="117%" id="Form6I_I17">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I17">
                          <asp:Label ID="Label_I_I17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I17" runat="server" Text='<%# Bind("Isidima_Section_I_I17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I17" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I17" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I18" SortExpression="Isidima_Section_I_I18">
                  <HeaderTemplate>
                    <table width="118%" id="Form6I_I18">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I18">
                          <asp:Label ID="Label_I_I18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I18" runat="server" Text='<%# Bind("Isidima_Section_I_I18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I18" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I18" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I19" SortExpression="Isidima_Section_I_I19">
                  <HeaderTemplate>
                    <table width="119%" id="Form6I_I19">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I19">
                          <asp:Label ID="Label_I_I19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I19" runat="server" Text='<%# Bind("Isidima_Section_I_I19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I19" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I19" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I20" SortExpression="Isidima_Section_I_I20">
                  <HeaderTemplate>
                    <table width="120%" id="Form6I_I20">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I20">
                          <asp:Label ID="Label_I_I20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I20" runat="server" Text='<%# Bind("Isidima_Section_I_I20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I20" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I20" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I21" SortExpression="Isidima_Section_I_I21">
                  <HeaderTemplate>
                    <table width="121%" id="Form6I_I21">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I21">
                          <asp:Label ID="Label_I_I21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I21" runat="server" Text='<%# Bind("Isidima_Section_I_I21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I21" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I21" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I22" SortExpression="Isidima_Section_I_I22">
                  <HeaderTemplate>
                    <table width="122%" id="Form6I_I22">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I22">
                          <asp:Label ID="Label_I_I22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I22" runat="server" Text='<%# Bind("Isidima_Section_I_I22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I22" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I22" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I23" SortExpression="Isidima_Section_I_I23">
                  <HeaderTemplate>
                    <table width="123%" id="Form6I_I23">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I23">
                          <asp:Label ID="Label_I_I23" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I23" runat="server" Text='<%# Bind("Isidima_Section_I_I23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I23" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I23" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I24" SortExpression="Isidima_Section_I_I24">
                  <HeaderTemplate>
                    <table width="124%" id="Form6I_I24">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I24">
                          <asp:Label ID="Label_I_I24" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I24Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I24No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I24" runat="server" Text='<%# Bind("Isidima_Section_I_I24") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I24" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I24" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I25" SortExpression="Isidima_Section_I_I25">
                  <HeaderTemplate>
                    <table width="125%" id="Form6I_I25">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I25">
                          <asp:Label ID="Label_I_I25" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I25Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I25No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I25" runat="server" Text='<%# Bind("Isidima_Section_I_I25") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I25" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I25" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I26" SortExpression="Isidima_Section_I_I26">
                  <HeaderTemplate>
                    <table width="126%" id="Form6I_I26">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I26">
                          <asp:Label ID="Label_I_I26" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I26Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I26No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I26" runat="server" Text='<%# Bind("Isidima_Section_I_I26") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I26" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I26" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I27" SortExpression="Isidima_Section_I_I27">
                  <HeaderTemplate>
                    <table width="127%" id="Form6I_I27">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I27">
                          <asp:Label ID="Label_I_I27" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I27Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I27No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I27" runat="server" Text='<%# Bind("Isidima_Section_I_I27") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I27" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I27")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I27" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I27" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I27" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I27")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I27" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I27" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I_I28" SortExpression="Isidima_Section_I_I28">
                  <HeaderTemplate>
                    <table width="128%" id="Form6I_I28">
                      <tr>
                        <td style="vertical-align: middle;" id="I_I28">
                          <asp:Label ID="Label_I_I28" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_I_I28Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_I_I28No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemI_I28" runat="server" Text='<%# Bind("Isidima_Section_I_I28") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditI_I28" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I28")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditI_I28" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditI_I28" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertI_I28" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_I_I28")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertI_I28" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertI_I28" ValidationGroup="Isidima_Form6"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total I Score" SortExpression="Isidima_Section_I_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_I_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 39</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_I_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 39</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_I_Total") %>'></asp:Label><strong> out of 39</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_I_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_I_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_I_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_I_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_I_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_I_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Independence (I)" CssClass="Controls_Button" ValidationGroup="Isidima_Form6" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Independence (I)" CssClass="Controls_Button" ValidationGroup="Isidima_Form6" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form6" runat="server" OnInserted="SqlDataSource_Isidima_Form6_Inserted" OnUpdated="SqlDataSource_Isidima_Form6_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm7" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form7Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form7" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_PSY_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form7" OnItemInserting="DetailsView_Isidima_Form7_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form7_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form7_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form7" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form7" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_PSY_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C01" SortExpression="Isidima_Section_PSY_C01">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C01">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C01">
                          <asp:Label ID="Label_PSY_C01" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_PSY_C01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C01" runat="server" Text='<%# Bind("Isidima_Section_PSY_C01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C01" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C01" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C02" SortExpression="Isidima_Section_PSY_C02">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C02">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C02">
                          <asp:Label ID="Label_PSY_C02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C02" runat="server" Text='<%# Bind("Isidima_Section_PSY_C02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C02" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C02" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C03" SortExpression="Isidima_Section_PSY_C03">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C03">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C03">
                          <asp:Label ID="Label_PSY_C03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C03" runat="server" Text='<%# Bind("Isidima_Section_PSY_C03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C03" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C03" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C04" SortExpression="Isidima_Section_PSY_C04">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C04">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C04">
                          <asp:Label ID="Label_PSY_C04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C04" runat="server" Text='<%# Bind("Isidima_Section_PSY_C04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C04" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C04" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C05" SortExpression="Isidima_Section_PSY_C05">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C05">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C05">
                          <asp:Label ID="Label_PSY_C05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C05" runat="server" Text='<%# Bind("Isidima_Section_PSY_C05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C05" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C05" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C06" SortExpression="Isidima_Section_PSY_C06">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C06">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C06">
                          <asp:Label ID="Label_PSY_C06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C06" runat="server" Text='<%# Bind("Isidima_Section_PSY_C06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C06" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C06" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C07" SortExpression="Isidima_Section_PSY_C07">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C07">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C07">
                          <asp:Label ID="Label_PSY_C07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C07" runat="server" Text='<%# Bind("Isidima_Section_PSY_C07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C07" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C07" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C08" SortExpression="Isidima_Section_PSY_C08">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C08">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C08">
                          <asp:Label ID="Label_PSY_C08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C08" runat="server" Text='<%# Bind("Isidima_Section_PSY_C08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C08" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C08" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C09" SortExpression="Isidima_Section_PSY_C09">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C09">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C09">
                          <asp:Label ID="Label_PSY_C09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C09" runat="server" Text='<%# Bind("Isidima_Section_PSY_C09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C09" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C09" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C10" SortExpression="Isidima_Section_PSY_C10">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C10">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C10">
                          <asp:Label ID="Label_PSY_C10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C10" runat="server" Text='<%# Bind("Isidima_Section_PSY_C10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C10" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C10" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C11" SortExpression="Isidima_Section_PSY_C11">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C11">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C11">
                          <asp:Label ID="Label_PSY_C11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C11" runat="server" Text='<%# Bind("Isidima_Section_PSY_C11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C11" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C11" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C12" SortExpression="Isidima_Section_PSY_C12">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C12">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C12">
                          <asp:Label ID="Label_PSY_C12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C12" runat="server" Text='<%# Bind("Isidima_Section_PSY_C12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C12" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C12" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C13" SortExpression="Isidima_Section_PSY_C13">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C13">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C13">
                          <asp:Label ID="Label_PSY_C13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C13" runat="server" Text='<%# Bind("Isidima_Section_PSY_C13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C13" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C13" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C14" SortExpression="Isidima_Section_PSY_C14">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C14">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C14">
                          <asp:Label ID="Label_PSY_C14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C14" runat="server" Text='<%# Bind("Isidima_Section_PSY_C14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C14" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C14" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C15" SortExpression="Isidima_Section_PSY_C15">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C15">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C15">
                          <asp:Label ID="Label_PSY_C15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C15" runat="server" Text='<%# Bind("Isidima_Section_PSY_C15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C15" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C15" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C16" SortExpression="Isidima_Section_PSY_C16">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C16">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C16">
                          <asp:Label ID="Label_PSY_C16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C16" runat="server" Text='<%# Bind("Isidima_Section_PSY_C16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C16" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C16" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C17" SortExpression="Isidima_Section_PSY_C17">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C17">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C17">
                          <asp:Label ID="Label_PSY_C17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C17" runat="server" Text='<%# Bind("Isidima_Section_PSY_C17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C17" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C17" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C18" SortExpression="Isidima_Section_PSY_C18">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C18">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C18">
                          <asp:Label ID="Label_PSY_C18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C18" runat="server" Text='<%# Bind("Isidima_Section_PSY_C18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C18" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C18" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C19" SortExpression="Isidima_Section_PSY_C19">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C19">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C19">
                          <asp:Label ID="Label_PSY_C19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C19" runat="server" Text='<%# Bind("Isidima_Section_PSY_C19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C19" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C19" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C20" SortExpression="Isidima_Section_PSY_C20">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C20">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C20">
                          <asp:Label ID="Label_PSY_C20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C20" runat="server" Text='<%# Bind("Isidima_Section_PSY_C20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C20" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C20" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C21" SortExpression="Isidima_Section_PSY_C21">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C21">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C21">
                          <asp:Label ID="Label_PSY_C21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C21" runat="server" Text='<%# Bind("Isidima_Section_PSY_C21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C21" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C21" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C22" SortExpression="Isidima_Section_PSY_C22">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C22">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C22">
                          <asp:Label ID="Label_PSY_C22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C22" runat="server" Text='<%# Bind("Isidima_Section_PSY_C22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C22" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C22" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C23" SortExpression="Isidima_Section_PSY_C23">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C23">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C23">
                          <asp:Label ID="Label_PSY_C23" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C23" runat="server" Text='<%# Bind("Isidima_Section_PSY_C23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C23" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C23" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C24" SortExpression="Isidima_Section_PSY_C24">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C24">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C24">
                          <asp:Label ID="Label_PSY_C24" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C24Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C24No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C24" runat="server" Text='<%# Bind("Isidima_Section_PSY_C24") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C24" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C24" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C25" SortExpression="Isidima_Section_PSY_C25">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C25">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C25">
                          <asp:Label ID="Label_PSY_C25" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C25Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C25No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C25" runat="server" Text='<%# Bind("Isidima_Section_PSY_C25") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C25" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C25" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PSY_C26" SortExpression="Isidima_Section_PSY_C26">
                  <HeaderTemplate>
                    <table width="102%" id="Form7PSY_C26">
                      <tr>
                        <td style="vertical-align: middle;" id="PSY_C26">
                          <asp:Label ID="Label_PSY_C26" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_PSY_C26Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_PSY_C26No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemPSY_C26" runat="server" Text='<%# Bind("Isidima_Section_PSY_C26") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditPSY_C26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditPSY_C26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditPSY_C26" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertPSY_C26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_PSY_C26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertPSY_C26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertPSY_C26" ValidationGroup="Isidima_Form7"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total PSY Score" SortExpression="Isidima_Section_PSY_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_PSY_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 34</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_PSY_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 34</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_PSY_Total") %>'></asp:Label><strong> out of 34</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_PSY_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_PSY_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_PSY_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_PSY_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_PSY_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Mental Health (PSY)" CssClass="Controls_Button" ValidationGroup="Isidima_Form7" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Mental Health (PSY)" CssClass="Controls_Button" ValidationGroup="Isidima_Form7" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form7" runat="server" OnInserted="SqlDataSource_Isidima_Form7_Inserted" OnUpdated="SqlDataSource_Isidima_Form7_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm8" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form8Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form8" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_T_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form8" OnItemInserting="DetailsView_Isidima_Form8_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form8_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form8_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form8" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form8" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_T_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T01" SortExpression="Isidima_Section_T_T01">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T01">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T01">
                          <asp:Label ID="Label_T_T01" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T01" runat="server" Text='<%# Bind("Isidima_Section_T_T01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T01" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T01" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T02" SortExpression="Isidima_Section_T_T02">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T02">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T02">
                          <asp:Label ID="Label_T_T02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T02" runat="server" Text='<%# Bind("Isidima_Section_T_T02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T02" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T02" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T03" SortExpression="Isidima_Section_T_T03">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T03">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T03">
                          <asp:Label ID="Label_T_T03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T03" runat="server" Text='<%# Bind("Isidima_Section_T_T03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T03" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T03" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T04" SortExpression="Isidima_Section_T_T04">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T04">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T04">
                          <asp:Label ID="Label_T_T04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T04" runat="server" Text='<%# Bind("Isidima_Section_T_T04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T04" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T04" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T05" SortExpression="Isidima_Section_T_T05">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T05">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T05">
                          <asp:Label ID="Label_T_T05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T05" runat="server" Text='<%# Bind("Isidima_Section_T_T05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T05" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T05" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T06" SortExpression="Isidima_Section_T_T06">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T06">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T06">
                          <asp:Label ID="Label_T_T06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T06" runat="server" Text='<%# Bind("Isidima_Section_T_T06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T06" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T06" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T07" SortExpression="Isidima_Section_T_T07">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T07">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T07">
                          <asp:Label ID="Label_T_T07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T07" runat="server" Text='<%# Bind("Isidima_Section_T_T07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T07" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T07" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T08" SortExpression="Isidima_Section_T_T08">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T08">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T08">
                          <asp:Label ID="Label_T_T08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T08" runat="server" Text='<%# Bind("Isidima_Section_T_T08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T08" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T08" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T09" SortExpression="Isidima_Section_T_T09">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T09">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T09">
                          <asp:Label ID="Label_T_T09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T09" runat="server" Text='<%# Bind("Isidima_Section_T_T09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T09" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T09" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T10" SortExpression="Isidima_Section_T_T10">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T10">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T10">
                          <asp:Label ID="Label_T_T10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T10" runat="server" Text='<%# Bind("Isidima_Section_T_T10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T10" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T10" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T11" SortExpression="Isidima_Section_T_T11">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T11">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T11">
                          <asp:Label ID="Label_T_T11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T11" runat="server" Text='<%# Bind("Isidima_Section_T_T11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T11" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T11" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T12" SortExpression="Isidima_Section_T_T12">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T12">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T12">
                          <asp:Label ID="Label_T_T12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T12" runat="server" Text='<%# Bind("Isidima_Section_T_T12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T12" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T12" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T13" SortExpression="Isidima_Section_T_T13">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T13">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T13">
                          <asp:Label ID="Label_T_T13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T13" runat="server" Text='<%# Bind("Isidima_Section_T_T13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T13" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T13" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T14" SortExpression="Isidima_Section_T_T14">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T14">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T14">
                          <asp:Label ID="Label_T_T14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T14" runat="server" Text='<%# Bind("Isidima_Section_T_T14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T14" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T14" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T15" SortExpression="Isidima_Section_T_T15">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T15">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T15">
                          <asp:Label ID="Label_T_T15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T15" runat="server" Text='<%# Bind("Isidima_Section_T_T15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T15" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T15" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T16" SortExpression="Isidima_Section_T_T16">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T16">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T16">
                          <asp:Label ID="Label_T_T16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T16" runat="server" Text='<%# Bind("Isidima_Section_T_T16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T16" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T16" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T17" SortExpression="Isidima_Section_T_T17">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T17">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T17">
                          <asp:Label ID="Label_T_T17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T17" runat="server" Text='<%# Bind("Isidima_Section_T_T17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T17" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T17" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T18" SortExpression="Isidima_Section_T_T18">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T18">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T18">
                          <asp:Label ID="Label_T_T18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T18" runat="server" Text='<%# Bind("Isidima_Section_T_T18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T18" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T18" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T19" SortExpression="Isidima_Section_T_T19">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T19">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T19">
                          <asp:Label ID="Label_T_T19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T19" runat="server" Text='<%# Bind("Isidima_Section_T_T19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T19" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T19" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T20" SortExpression="Isidima_Section_T_T20">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T20">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T20">
                          <asp:Label ID="Label_T_T20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T20" runat="server" Text='<%# Bind("Isidima_Section_T_T20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T20" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T20" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T21" SortExpression="Isidima_Section_T_T21">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T21">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T21">
                          <asp:Label ID="Label_T_T21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T21" runat="server" Text='<%# Bind("Isidima_Section_T_T21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T21" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T21" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T22" SortExpression="Isidima_Section_T_T22">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T22">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T22">
                          <asp:Label ID="Label_T_T22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T22" runat="server" Text='<%# Bind("Isidima_Section_T_T22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T22" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T22" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="T_T23" SortExpression="Isidima_Section_T_T23">
                  <HeaderTemplate>
                    <table width="102%" id="Form8T_T23">
                      <tr>
                        <td style="vertical-align: middle;" id="T_T23">
                          <asp:Label ID="Label_T_T23" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_T_T23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_T_T23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemT_T23" runat="server" Text='<%# Bind("Isidima_Section_T_T23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditT_T23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditT_T23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditT_T23" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertT_T23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_T_T23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertT_T23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertT_T23" ValidationGroup="Isidima_Form8"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total T Score" SortExpression="Isidima_Section_T_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_T_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 29</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_T_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 29</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_T_Total") %>'></asp:Label><strong> out of 29</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_T_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_T_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_T_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_T_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_T_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_T_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Personality Traits (T)" CssClass="Controls_Button" ValidationGroup="Isidima_Form8" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Personality Traits (T)" CssClass="Controls_Button" ValidationGroup="Isidima_Form8" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form8" runat="server" OnInserted="SqlDataSource_Isidima_Form8_Inserted" OnUpdated="SqlDataSource_Isidima_Form8_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm9" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form9Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form9" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_B_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form9" OnItemInserting="DetailsView_Isidima_Form9_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form9_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form9_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form9" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form9" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_B_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B01" SortExpression="Isidima_Section_B_B01">
                  <HeaderTemplate>
                    <table width="102%" id="Form9B_B01">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B01">
                          <asp:Label ID="Label_B_B01" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B01" runat="server" Text='<%# Bind("Isidima_Section_B_B01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B01" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B01" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B02" SortExpression="Isidima_Section_B_B02">
                  <HeaderTemplate>
                    <table width="102%" id="Form9B_B02">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B02">
                          <asp:Label ID="Label_B_B02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B02" runat="server" Text='<%# Bind("Isidima_Section_B_B02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B02" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B02" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B03" SortExpression="Isidima_Section_B_B03">
                  <HeaderTemplate>
                    <table width="103%" id="Form9B_B03">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B03">
                          <asp:Label ID="Label_B_B03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B03" runat="server" Text='<%# Bind("Isidima_Section_B_B03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B03" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B03" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B04" SortExpression="Isidima_Section_B_B04">
                  <HeaderTemplate>
                    <table width="104%" id="Form9B_B04">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B04">
                          <asp:Label ID="Label_B_B04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B04" runat="server" Text='<%# Bind("Isidima_Section_B_B04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B04" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B04" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B05" SortExpression="Isidima_Section_B_B05">
                  <HeaderTemplate>
                    <table width="105%" id="Form9B_B05">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B05">
                          <asp:Label ID="Label_B_B05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B05" runat="server" Text='<%# Bind("Isidima_Section_B_B05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B05" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B05" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B06" SortExpression="Isidima_Section_B_B06">
                  <HeaderTemplate>
                    <table width="106%" id="Form9B_B06">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B06">
                          <asp:Label ID="Label_B_B06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B06" runat="server" Text='<%# Bind("Isidima_Section_B_B06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B06" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B06" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B07" SortExpression="Isidima_Section_B_B07">
                  <HeaderTemplate>
                    <table width="107%" id="Form9B_B07">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B07">
                          <asp:Label ID="Label_B_B07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B07" runat="server" Text='<%# Bind("Isidima_Section_B_B07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B07" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B07" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B08" SortExpression="Isidima_Section_B_B08">
                  <HeaderTemplate>
                    <table width="108%" id="Form9B_B08">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B08">
                          <asp:Label ID="Label_B_B08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B08" runat="server" Text='<%# Bind("Isidima_Section_B_B08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B08" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B08" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B09" SortExpression="Isidima_Section_B_B09">
                  <HeaderTemplate>
                    <table width="109%" id="Form9B_B09">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B09">
                          <asp:Label ID="Label_B_B09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B09" runat="server" Text='<%# Bind("Isidima_Section_B_B09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B09" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B09" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B10" SortExpression="Isidima_Section_B_B10">
                  <HeaderTemplate>
                    <table width="110%" id="Form9B_B10">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B10">
                          <asp:Label ID="Label_B_B10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B10" runat="server" Text='<%# Bind("Isidima_Section_B_B10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B10" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B10" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B11" SortExpression="Isidima_Section_B_B11">
                  <HeaderTemplate>
                    <table width="111%" id="Form9B_B11">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B11">
                          <asp:Label ID="Label_B_B11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B11" runat="server" Text='<%# Bind("Isidima_Section_B_B11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B11" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B11" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B12" SortExpression="Isidima_Section_B_B12">
                  <HeaderTemplate>
                    <table width="112%" id="Form9B_B12">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B12">
                          <asp:Label ID="Label_B_B12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B12" runat="server" Text='<%# Bind("Isidima_Section_B_B12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B12" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B12" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B13" SortExpression="Isidima_Section_B_B13">
                  <HeaderTemplate>
                    <table width="113%" id="Form9B_B13">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B13">
                          <asp:Label ID="Label_B_B13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B13" runat="server" Text='<%# Bind("Isidima_Section_B_B13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B13" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B13" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B14" SortExpression="Isidima_Section_B_B14">
                  <HeaderTemplate>
                    <table width="114%" id="Form9B_B14">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B14">
                          <asp:Label ID="Label_B_B14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B14" runat="server" Text='<%# Bind("Isidima_Section_B_B14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B14" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B14" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B15" SortExpression="Isidima_Section_B_B15">
                  <HeaderTemplate>
                    <table width="115%" id="Form9B_B15">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B15">
                          <asp:Label ID="Label_B_B15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B15" runat="server" Text='<%# Bind("Isidima_Section_B_B15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B15" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B15" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B16" SortExpression="Isidima_Section_B_B16">
                  <HeaderTemplate>
                    <table width="116%" id="Form9B_B16">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B16">
                          <asp:Label ID="Label_B_B16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B16" runat="server" Text='<%# Bind("Isidima_Section_B_B16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B16" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B16" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B17" SortExpression="Isidima_Section_B_B17">
                  <HeaderTemplate>
                    <table width="117%" id="Form9B_B17">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B17">
                          <asp:Label ID="Label_B_B17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B17" runat="server" Text='<%# Bind("Isidima_Section_B_B17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B17" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B17" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B18" SortExpression="Isidima_Section_B_B18">
                  <HeaderTemplate>
                    <table width="118%" id="Form9B_B18">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B18">
                          <asp:Label ID="Label_B_B18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B18" runat="server" Text='<%# Bind("Isidima_Section_B_B18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B18" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B18" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B19" SortExpression="Isidima_Section_B_B19">
                  <HeaderTemplate>
                    <table width="119%" id="Form9B_B19">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B19">
                          <asp:Label ID="Label_B_B19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B19" runat="server" Text='<%# Bind("Isidima_Section_B_B19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B19" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B19" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B20" SortExpression="Isidima_Section_B_B20">
                  <HeaderTemplate>
                    <table width="120%" id="Form9B_B20">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B20">
                          <asp:Label ID="Label_B_B20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B20" runat="server" Text='<%# Bind("Isidima_Section_B_B20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B20" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B20" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B21" SortExpression="Isidima_Section_B_B21">
                  <HeaderTemplate>
                    <table width="121%" id="Form9B_B21">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B21">
                          <asp:Label ID="Label_B_B21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B21" runat="server" Text='<%# Bind("Isidima_Section_B_B21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B21" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B21" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B22" SortExpression="Isidima_Section_B_B22">
                  <HeaderTemplate>
                    <table width="122%" id="Form9B_B22">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B22">
                          <asp:Label ID="Label_B_B22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B22" runat="server" Text='<%# Bind("Isidima_Section_B_B22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B22" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B22" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B23" SortExpression="Isidima_Section_B_B23">
                  <HeaderTemplate>
                    <table width="123%" id="Form9B_B23">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B23">
                          <asp:Label ID="Label_B_B23" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B23" runat="server" Text='<%# Bind("Isidima_Section_B_B23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B23" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B23" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B24" SortExpression="Isidima_Section_B_B24">
                  <HeaderTemplate>
                    <table width="124%" id="Form9B_B24">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B24">
                          <asp:Label ID="Label_B_B24" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B24Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B24No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B24" runat="server" Text='<%# Bind("Isidima_Section_B_B24") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B24" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B24" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B25" SortExpression="Isidima_Section_B_B25">
                  <HeaderTemplate>
                    <table width="125%" id="Form9B_B25">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B25">
                          <asp:Label ID="Label_B_B25" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B25Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B25No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B25" runat="server" Text='<%# Bind("Isidima_Section_B_B25") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B25" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B25" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B26" SortExpression="Isidima_Section_B_B26">
                  <HeaderTemplate>
                    <table width="126%" id="Form9B_B26">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B26">
                          <asp:Label ID="Label_B_B26" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B26Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B26No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B26" runat="server" Text='<%# Bind("Isidima_Section_B_B26") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B26" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B26" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B26")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B26" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B26" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B27" SortExpression="Isidima_Section_B_B27">
                  <HeaderTemplate>
                    <table width="127%" id="Form9B_B27">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B27">
                          <asp:Label ID="Label_B_B27" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B27Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B27No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B27" runat="server" Text='<%# Bind("Isidima_Section_B_B27") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B27" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B27")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B27" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B27" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B27" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B27")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B27" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B27" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="B_B28" SortExpression="Isidima_Section_B_B28">
                  <HeaderTemplate>
                    <table width="128%" id="Form9B_B28">
                      <tr>
                        <td style="vertical-align: middle;" id="B_B28">
                          <asp:Label ID="Label_B_B28" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_B_B28Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_B_B28No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemB_B28" runat="server" Text='<%# Bind("Isidima_Section_B_B28") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditB_B28" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B28")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditB_B28" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditB_B28" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertB_B28" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_B_B28")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertB_B28" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertB_B28" ValidationGroup="Isidima_Form9"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total B Score" SortExpression="Isidima_Section_B_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_B_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 51</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_B_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 51</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_B_Total") %>'></asp:Label><strong> out of 51</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_B_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_B_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_B_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_B_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_B_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_B_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Physical (B)" CssClass="Controls_Button" ValidationGroup="Isidima_Form9" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Physical (B)" CssClass="Controls_Button" ValidationGroup="Isidima_Form9" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form9" runat="server" OnInserted="SqlDataSource_Isidima_Form9_Inserted" OnUpdated="SqlDataSource_Isidima_Form9_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm10" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form10Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form10" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_R_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form10" OnItemInserting="DetailsView_Isidima_Form10_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form10_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form10_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form10" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form10" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_R_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R01" SortExpression="Isidima_Section_R_R01">
                  <HeaderTemplate>
                    <table width="102%" id="Form10R_R01">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R01">
                          <asp:Label ID="Label_R_R01" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R01" runat="server" Text='<%# Bind("Isidima_Section_R_R01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R01" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R01" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R02" SortExpression="Isidima_Section_R_R02">
                  <HeaderTemplate>
                    <table width="102%" id="Form10R_R02">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R02">
                          <asp:Label ID="Label_R_R02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R02" runat="server" Text='<%# Bind("Isidima_Section_R_R02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R02" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R02" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R03" SortExpression="Isidima_Section_R_R03">
                  <HeaderTemplate>
                    <table width="103%" id="Form10R_R03">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R03">
                          <asp:Label ID="Label_R_R03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R03" runat="server" Text='<%# Bind("Isidima_Section_R_R03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R03" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R03" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R04" SortExpression="Isidima_Section_R_R04">
                  <HeaderTemplate>
                    <table width="104%" id="Form10R_R04">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R04">
                          <asp:Label ID="Label_R_R04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R04" runat="server" Text='<%# Bind("Isidima_Section_R_R04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R04" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R04" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R05" SortExpression="Isidima_Section_R_R05">
                  <HeaderTemplate>
                    <table width="105%" id="Form10R_R05">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R05">
                          <asp:Label ID="Label_R_R05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R05" runat="server" Text='<%# Bind("Isidima_Section_R_R05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R05" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R05" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R06" SortExpression="Isidima_Section_R_R06">
                  <HeaderTemplate>
                    <table width="106%" id="Form10R_R06">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R06">
                          <asp:Label ID="Label_R_R06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R06" runat="server" Text='<%# Bind("Isidima_Section_R_R06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R06" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R06" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R07" SortExpression="Isidima_Section_R_R07">
                  <HeaderTemplate>
                    <table width="107%" id="Form10R_R07">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R07">
                          <asp:Label ID="Label_R_R07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R07" runat="server" Text='<%# Bind("Isidima_Section_R_R07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R07" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R07" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R08" SortExpression="Isidima_Section_R_R08">
                  <HeaderTemplate>
                    <table width="108%" id="Form10R_R08">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R08">
                          <asp:Label ID="Label_R_R08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R08" runat="server" Text='<%# Bind("Isidima_Section_R_R08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R08" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R08" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R09" SortExpression="Isidima_Section_R_R09">
                  <HeaderTemplate>
                    <table width="109%" id="Form10R_R09">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R09">
                          <asp:Label ID="Label_R_R09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R09" runat="server" Text='<%# Bind("Isidima_Section_R_R09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R09" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R09" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R10" SortExpression="Isidima_Section_R_R10">
                  <HeaderTemplate>
                    <table width="110%" id="Form10R_R10">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R10">
                          <asp:Label ID="Label_R_R10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R10" runat="server" Text='<%# Bind("Isidima_Section_R_R10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R10" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R10" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R11" SortExpression="Isidima_Section_R_R11">
                  <HeaderTemplate>
                    <table width="111%" id="Form10R_R11">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R11">
                          <asp:Label ID="Label_R_R11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R11" runat="server" Text='<%# Bind("Isidima_Section_R_R11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R11" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R11" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R12" SortExpression="Isidima_Section_R_R12">
                  <HeaderTemplate>
                    <table width="112%" id="Form10R_R12">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R12">
                          <asp:Label ID="Label_R_R12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R12" runat="server" Text='<%# Bind("Isidima_Section_R_R12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R12" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R12" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R13" SortExpression="Isidima_Section_R_R13">
                  <HeaderTemplate>
                    <table width="113%" id="Form10R_R13">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R13">
                          <asp:Label ID="Label_R_R13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R13" runat="server" Text='<%# Bind("Isidima_Section_R_R13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R13" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R13" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R14" SortExpression="Isidima_Section_R_R14">
                  <HeaderTemplate>
                    <table width="114%" id="Form10R_R14">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R14">
                          <asp:Label ID="Label_R_R14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R14" runat="server" Text='<%# Bind("Isidima_Section_R_R14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R14" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R14" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R15" SortExpression="Isidima_Section_R_R15">
                  <HeaderTemplate>
                    <table width="115%" id="Form10R_R15">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R15">
                          <asp:Label ID="Label_R_R15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R15" runat="server" Text='<%# Bind("Isidima_Section_R_R15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R15" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R15" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R16" SortExpression="Isidima_Section_R_R16">
                  <HeaderTemplate>
                    <table width="116%" id="Form10R_R16">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R16">
                          <asp:Label ID="Label_R_R16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R16" runat="server" Text='<%# Bind("Isidima_Section_R_R16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R16" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R16" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R17" SortExpression="Isidima_Section_R_R17">
                  <HeaderTemplate>
                    <table width="117%" id="Form10R_R17">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R17">
                          <asp:Label ID="Label_R_R17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R17" runat="server" Text='<%# Bind("Isidima_Section_R_R17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R17" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R17" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R_R18" SortExpression="Isidima_Section_R_R18">
                  <HeaderTemplate>
                    <table width="118%" id="Form10R_R18">
                      <tr>
                        <td style="vertical-align: middle;" id="R_R18">
                          <asp:Label ID="Label_R_R18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_R_R18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_R_R18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemR_R18" runat="server" Text='<%# Bind("Isidima_Section_R_R18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditR_R18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditR_R18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditR_R18" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertR_R18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_R_R18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertR_R18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertR_R18" ValidationGroup="Isidima_Form10"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total R Score" SortExpression="Isidima_Section_R_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_R_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 24</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_R_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 24</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_R_Total") %>'></asp:Label><strong> out of 24</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_R_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_R_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_R_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_R_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_R_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_R_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Recreational (R)" CssClass="Controls_Button" ValidationGroup="Isidima_Form10" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Recreational (R)" CssClass="Controls_Button" ValidationGroup="Isidima_Form10" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form10" runat="server" OnInserted="SqlDataSource_Isidima_Form10_Inserted" OnUpdated="SqlDataSource_Isidima_Form10_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm11" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form11Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form11" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_S_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form11" OnItemInserting="DetailsView_Isidima_Form11_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form11_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form11_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form11" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form11" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_S_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S01" SortExpression="Isidima_Section_S_S01">
                  <HeaderTemplate>
                    <table width="106%" id="Form11S_S01">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S01">
                          <asp:Label ID="Label_S_S01" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S01" runat="server" Text='<%# Bind("Isidima_Section_S_S01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S01" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S01" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S02" SortExpression="Isidima_Section_S_S02">
                  <HeaderTemplate>
                    <table width="102%" id="Form11S_S02">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S02">
                          <asp:Label ID="Label_S_S02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S02" runat="server" Text='<%# Bind("Isidima_Section_S_S02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S02" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S02" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S03" SortExpression="Isidima_Section_S_S03">
                  <HeaderTemplate>
                    <table width="103%" id="Form11S_S03">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S03">
                          <asp:Label ID="Label_S_S03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S03" runat="server" Text='<%# Bind("Isidima_Section_S_S03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S03" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S03" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S04" SortExpression="Isidima_Section_S_S04">
                  <HeaderTemplate>
                    <table width="104%" id="Form11S_S04">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S04">
                          <asp:Label ID="Label_S_S04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S04" runat="server" Text='<%# Bind("Isidima_Section_S_S04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S04" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S04" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S05" SortExpression="Isidima_Section_S_S05">
                  <HeaderTemplate>
                    <table width="105%" id="Form11S_S05">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S05">
                          <asp:Label ID="Label_S_S05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S05" runat="server" Text='<%# Bind("Isidima_Section_S_S05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S05" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S05" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S06" SortExpression="Isidima_Section_S_S06">
                  <HeaderTemplate>
                    <table width="106%" id="Form11S_S06">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S06">
                          <asp:Label ID="Label_S_S06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S06" runat="server" Text='<%# Bind("Isidima_Section_S_S06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S06" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S06" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S07" SortExpression="Isidima_Section_S_S07">
                  <HeaderTemplate>
                    <table width="107%" id="Form11S_S07">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S07">
                          <asp:Label ID="Label_S_S07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S07" runat="server" Text='<%# Bind("Isidima_Section_S_S07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S07" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S07" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S08" SortExpression="Isidima_Section_S_S08">
                  <HeaderTemplate>
                    <table width="108%" id="Form11S_S08">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S08">
                          <asp:Label ID="Label_S_S08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S08" runat="server" Text='<%# Bind("Isidima_Section_S_S08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S08" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S08" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S09" SortExpression="Isidima_Section_S_S09">
                  <HeaderTemplate>
                    <table width="109%" id="Form11S_S09">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S09">
                          <asp:Label ID="Label_S_S09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S09" runat="server" Text='<%# Bind("Isidima_Section_S_S09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S09" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S09" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S10" SortExpression="Isidima_Section_S_S10">
                  <HeaderTemplate>
                    <table width="110%" id="Form11S_S10">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S10">
                          <asp:Label ID="Label_S_S10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S10" runat="server" Text='<%# Bind("Isidima_Section_S_S10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S10" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S10" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S11" SortExpression="Isidima_Section_S_S11">
                  <HeaderTemplate>
                    <table width="111%" id="Form11S_S11">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S11">
                          <asp:Label ID="Label_S_S11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S11" runat="server" Text='<%# Bind("Isidima_Section_S_S11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S11" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S11" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S12" SortExpression="Isidima_Section_S_S12">
                  <HeaderTemplate>
                    <table width="112%" id="Form11S_S12">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S12">
                          <asp:Label ID="Label_S_S12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S12" runat="server" Text='<%# Bind("Isidima_Section_S_S12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S12" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S12" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S13" SortExpression="Isidima_Section_S_S13">
                  <HeaderTemplate>
                    <table width="113%" id="Form11S_S13">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S13">
                          <asp:Label ID="Label_S_S13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S13" runat="server" Text='<%# Bind("Isidima_Section_S_S13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S13" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S13" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S14" SortExpression="Isidima_Section_S_S14">
                  <HeaderTemplate>
                    <table width="114%" id="Form11S_S14">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S14">
                          <asp:Label ID="Label_S_S14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S14" runat="server" Text='<%# Bind("Isidima_Section_S_S14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S14" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S14" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S15" SortExpression="Isidima_Section_S_S15">
                  <HeaderTemplate>
                    <table width="115%" id="Form11S_S15">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S15">
                          <asp:Label ID="Label_S_S15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S15" runat="server" Text='<%# Bind("Isidima_Section_S_S15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S15" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S15" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S16" SortExpression="Isidima_Section_S_S16">
                  <HeaderTemplate>
                    <table width="116%" id="Form11S_S16">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S16">
                          <asp:Label ID="Label_S_S16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S16" runat="server" Text='<%# Bind("Isidima_Section_S_S16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S16" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S16" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S17" SortExpression="Isidima_Section_S_S17">
                  <HeaderTemplate>
                    <table width="117%" id="Form11S_S17">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S17">
                          <asp:Label ID="Label_S_S17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S17" runat="server" Text='<%# Bind("Isidima_Section_S_S17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S17" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S17" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S18" SortExpression="Isidima_Section_S_S18">
                  <HeaderTemplate>
                    <table width="118%" id="Form11S_S18">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S18">
                          <asp:Label ID="Label_S_S18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S18" runat="server" Text='<%# Bind("Isidima_Section_S_S18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S18" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S18" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S19" SortExpression="Isidima_Section_S_S19">
                  <HeaderTemplate>
                    <table width="119%" id="Form11S_S19">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S19">
                          <asp:Label ID="Label_S_S19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S19" runat="server" Text='<%# Bind("Isidima_Section_S_S19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S19" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S19" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S20" SortExpression="Isidima_Section_S_S20">
                  <HeaderTemplate>
                    <table width="120%" id="Form11S_S20">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S20">
                          <asp:Label ID="Label_S_S20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S20" runat="server" Text='<%# Bind("Isidima_Section_S_S20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S20" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S20" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S21" SortExpression="Isidima_Section_S_S21">
                  <HeaderTemplate>
                    <table width="121%" id="Form11S_S21">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S21">
                          <asp:Label ID="Label_S_S21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S21" runat="server" Text='<%# Bind("Isidima_Section_S_S21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S21" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S21" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S22" SortExpression="Isidima_Section_S_S22">
                  <HeaderTemplate>
                    <table width="122%" id="Form11S_S22">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S22">
                          <asp:Label ID="Label_S_S22" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S22Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S22No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S22" runat="server" Text='<%# Bind("Isidima_Section_S_S22") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S22" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S22")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S22" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S22" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S23" SortExpression="Isidima_Section_S_S23">
                  <HeaderTemplate>
                    <table width="123%" id="Form11S_S23">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S23">
                          <asp:Label ID="Label_S_S23" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S23Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S23No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S23" runat="server" Text='<%# Bind("Isidima_Section_S_S23") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S23" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S23" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S23")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S23" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S23" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S24" SortExpression="Isidima_Section_S_S24">
                  <HeaderTemplate>
                    <table width="124%" id="Form11S_S24">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S24">
                          <asp:Label ID="Label_S_S24" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S24Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S24No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S24" runat="server" Text='<%# Bind("Isidima_Section_S_S24") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S24" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S24" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S24")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S24" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S24" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S_S25" SortExpression="Isidima_Section_S_S25">
                  <HeaderTemplate>
                    <table width="125%" id="Form11S_S25">
                      <tr>
                        <td style="vertical-align: middle;" id="S_S25">
                          <asp:Label ID="Label_S_S25" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_S_S25Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_S_S25No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemS_S25" runat="server" Text='<%# Bind("Isidima_Section_S_S25") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditS_S25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditS_S25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditS_S25" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertS_S25" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_S_S25")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertS_S25" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertS_S25" ValidationGroup="Isidima_Form11"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total S Score" SortExpression="Isidima_Section_S_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_S_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 32</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_S_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 32</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_S_Total") %>'></asp:Label><strong> out of 32</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_S_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_S_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_S_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_S_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_S_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_S_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Social (S)" CssClass="Controls_Button" ValidationGroup="Isidima_Form11" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Social (S)" CssClass="Controls_Button" ValidationGroup="Isidima_Form11" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form11" runat="server" OnInserted="SqlDataSource_Isidima_Form11_Inserted" OnUpdated="SqlDataSource_Isidima_Form11_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <table id="TableForm12" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Form12Heading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:DetailsView runat="server" ID="DetailsView_Isidima_Form12" Width="700px" AutoGenerateRows="False" DataKeyNames="Isidima_Section_V_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_Form12" OnItemInserting="DetailsView_Isidima_Form12_ItemInserting" DefaultMode="Insert" OnItemCommand="DetailsView_Isidima_Form12_ItemCommand" OnItemUpdating="DetailsView_Isidima_Form12_ItemUpdating">
              <FieldHeaderStyle Width="500px" />
              <Fields>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary_Form12" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_Form12" CssClass="Controls_Validation" />
                    <asp:HiddenField ID="HiddenField_V_TotalQuestions" runat="server" />
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                    <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BackColor="#f7f7f7" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V01" SortExpression="Isidima_Section_V_V01">
                  <HeaderTemplate>
                    <table width="102%" id="Form12V_V01">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V01">
                          <asp:Label ID="Label_V_V01" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V01Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V01No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V01" runat="server" Text='<%# Bind("Isidima_Section_V_V01") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V01" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V01" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V01")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V01" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V01" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V02" SortExpression="Isidima_Section_V_V02">
                  <HeaderTemplate>
                    <table width="102%" id="Form12V_V02">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V02">
                          <asp:Label ID="Label_V_V02" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V02Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V02No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V02" runat="server" Text='<%# Bind("Isidima_Section_V_V02") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V02" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V02" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V02")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V02" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V02" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V03" SortExpression="Isidima_Section_V_V03">
                  <HeaderTemplate>
                    <table width="103%" id="Form12V_V03">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V03">
                          <asp:Label ID="Label_V_V03" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V03Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V03No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V03" runat="server" Text='<%# Bind("Isidima_Section_V_V03") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V03" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V03" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V03")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V03" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V03" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V04" SortExpression="Isidima_Section_V_V04">
                  <HeaderTemplate>
                    <table width="104%" id="Form12V_V04">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V04">
                          <asp:Label ID="Label_V_V04" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V04Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V04No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V04" runat="server" Text='<%# Bind("Isidima_Section_V_V04") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V04" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V04" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V04")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V04" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V04" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V05" SortExpression="Isidima_Section_V_V05">
                  <HeaderTemplate>
                    <table width="105%" id="Form12V_V05">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V05">
                          <asp:Label ID="Label_V_V05" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V05Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V05No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V05" runat="server" Text='<%# Bind("Isidima_Section_V_V05") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V05" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V05" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V05")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V05" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V05" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V06" SortExpression="Isidima_Section_V_V06">
                  <HeaderTemplate>
                    <table width="106%" id="Form12V_V06">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V06">
                          <asp:Label ID="Label_V_V06" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V06Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V06No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V06" runat="server" Text='<%# Bind("Isidima_Section_V_V06") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V06" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V06" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V06")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V06" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V06" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V07" SortExpression="Isidima_Section_V_V07">
                  <HeaderTemplate>
                    <table width="107%" id="Form12V_V07">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V07">
                          <asp:Label ID="Label_V_V07" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V07Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V07No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V07" runat="server" Text='<%# Bind("Isidima_Section_V_V07") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V07" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V07" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V07")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V07" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V07" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V08" SortExpression="Isidima_Section_V_V08">
                  <HeaderTemplate>
                    <table width="108%" id="Form12V_V08">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V08">
                          <asp:Label ID="Label_V_V08" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V08Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V08No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V08" runat="server" Text='<%# Bind("Isidima_Section_V_V08") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V08" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V08" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V08")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V08" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V08" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V09" SortExpression="Isidima_Section_V_V09">
                  <HeaderTemplate>
                    <table width="109%" id="Form12V_V09">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V09">
                          <asp:Label ID="Label_V_V09" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V09Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V09No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V09" runat="server" Text='<%# Bind("Isidima_Section_V_V09") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V09" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V09" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V09")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V09" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V09" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V10" SortExpression="Isidima_Section_V_V10">
                  <HeaderTemplate>
                    <table width="110%" id="Form12V_V10">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V10">
                          <asp:Label ID="Label_V_V10" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V10Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V10No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V10" runat="server" Text='<%# Bind("Isidima_Section_V_V10") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V10" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V10")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V10" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V10" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V11" SortExpression="Isidima_Section_V_V11">
                  <HeaderTemplate>
                    <table width="111%" id="Form12V_V11">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V11">
                          <asp:Label ID="Label_V_V11" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V11Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V11No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V11" runat="server" Text='<%# Bind("Isidima_Section_V_V11") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V11" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V11")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V11" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V11" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V12" SortExpression="Isidima_Section_V_V12">
                  <HeaderTemplate>
                    <table width="112%" id="Form12V_V12">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V12">
                          <asp:Label ID="Label_V_V12" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V12Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V12No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V12" runat="server" Text='<%# Bind("Isidima_Section_V_V12") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V12" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V12")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V12" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V12" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V13" SortExpression="Isidima_Section_V_V13">
                  <HeaderTemplate>
                    <table width="113%" id="Form12V_V13">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V13">
                          <asp:Label ID="Label_V_V13" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V13Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V13No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V13" runat="server" Text='<%# Bind("Isidima_Section_V_V13") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V13" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V13")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V13" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V13" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V14" SortExpression="Isidima_Section_V_V14">
                  <HeaderTemplate>
                    <table width="114%" id="Form12V_V14">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V14">
                          <asp:Label ID="Label_V_V14" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V14Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V14No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V14" runat="server" Text='<%# Bind("Isidima_Section_V_V14") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V14" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V14")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V14" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V14" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V15" SortExpression="Isidima_Section_V_V15">
                  <HeaderTemplate>
                    <table width="115%" id="Form12V_V15">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V15">
                          <asp:Label ID="Label_V_V15" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V15Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V15No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V15" runat="server" Text='<%# Bind("Isidima_Section_V_V15") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V15" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V15")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V15" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V15" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V16" SortExpression="Isidima_Section_V_V16">
                  <HeaderTemplate>
                    <table width="116%" id="Form12V_V16">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V16">
                          <asp:Label ID="Label_V_V16" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V16Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V16No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V16" runat="server" Text='<%# Bind("Isidima_Section_V_V16") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V16" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V16")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V16" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V16" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V17" SortExpression="Isidima_Section_V_V17">
                  <HeaderTemplate>
                    <table width="117%" id="Form12V_V17">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V17">
                          <asp:Label ID="Label_V_V17" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V17Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V17No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V17" runat="server" Text='<%# Bind("Isidima_Section_V_V17") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V17" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V17")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V17" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V17" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V18" SortExpression="Isidima_Section_V_V18">
                  <HeaderTemplate>
                    <table width="118%" id="Form12V_V18">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V18">
                          <asp:Label ID="Label_V_V18" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V18Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V18No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V18" runat="server" Text='<%# Bind("Isidima_Section_V_V18") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V18" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V18")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V18" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V18" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V19" SortExpression="Isidima_Section_V_V19">
                  <HeaderTemplate>
                    <table width="119%" id="Form12V_V19">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V19">
                          <asp:Label ID="Label_V_V19" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V19Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V19No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V19" runat="server" Text='<%# Bind("Isidima_Section_V_V19") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V19" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V19")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V19" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V19" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V20" SortExpression="Isidima_Section_V_V20">
                  <HeaderTemplate>
                    <table width="120%" id="Form12V_V20">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V20">
                          <asp:Label ID="Label_V_V20" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V20Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V20No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V20" runat="server" Text='<%# Bind("Isidima_Section_V_V20") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V20" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V20")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V20" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V20" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="V_V21" SortExpression="Isidima_Section_V_V21">
                  <HeaderTemplate>
                    <table width="121%" id="Form12V_V21">
                      <tr>
                        <td style="vertical-align: middle;" id="V_V21">
                          <asp:Label ID="Label_V_V21" runat="server"></asp:Label><asp:HiddenField ID="HiddenField_V_V21Yes" runat="server" />
                          <asp:HiddenField ID="HiddenField_V_V21No" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemV_V21" runat="server" Text='<%# Bind("Isidima_Section_V_V21") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_EditV_V21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditV_V21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_EditV_V21" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList_InsertV_V21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" SelectedValue='<%# Bind("Isidima_Section_V_V21")%>'>
                      <asp:ListItem Value="Yes">Yes&nbsp;&nbsp;</asp:ListItem>
                      <asp:ListItem Value="No">No&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertV_V21" runat="server" ErrorMessage="" ControlToValidate="RadioButtonList_InsertV_V21" ValidationGroup="Isidima_Form12"></asp:RequiredFieldValidator>
                  </InsertItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" BackColor="#f7f7f7" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total V Score" SortExpression="Isidima_Section_V_Total">
                  <EditItemTemplate>
                    <asp:TextBox ID="Textbox_EditTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_V_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:TextBox ID="Textbox_InsertTotal" Width="25px" runat="server" Text='<%# Bind("Isidima_Section_V_Total") %>' ReadOnly="true"></asp:TextBox><strong>out of 30</strong>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemTotal" runat="server" Text='<%# Bind("Isidima_Section_V_Total") %>'></asp:Label><strong> out of 30</strong>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" VerticalAlign="Middle" HorizontalAlign="Right" Font-Bold="true" />
                  <ControlStyle CssClass="Controls_TextBox_Calculation" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <ItemTemplate>
                    &nbsp;
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" SortExpression="Isidima_Section_V_CreatedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Isidima_Section_V_CreatedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("Isidima_Section_V_CreatedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified Date" SortExpression="Isidima_Section_V_ModifiedDate">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified By" SortExpression="Isidima_Section_V_ModifiedBy">
                  <EditItemTemplate>
                    <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedBy") %>'></asp:Label>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedBy") %>'></asp:Label>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("Isidima_Section_V_ModifiedBy") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_NoBorder" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                  <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Vocational (V)" CssClass="Controls_Button" ValidationGroup="Isidima_Form12" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <InsertItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Vocational (V)" CssClass="Controls_Button" ValidationGroup="Isidima_Form12" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr class="Bottom">
                        <td style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                  <ItemStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                  <HeaderStyle BorderColor="#dfdfdf" BorderWidth="1" CssClass="Controls_Button" />
                </asp:TemplateField>
              </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource_Isidima_Form12" runat="server" OnInserted="SqlDataSource_Isidima_Form12_Inserted" OnUpdated="SqlDataSource_Isidima_Form12_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableListLinks" cellspacing="0" cellpadding="0" border="0" runat="server" width="700px">
        <tr>
          <td style="text-align: center;">
            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Isidima" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
          <asp:Button ID="Button_Print" runat="server" Text="Print Isidima" CssClass="Controls_Button" />&nbsp;
          <asp:Button ID="Button_Email" runat="server" Text="Email Link" CssClass="Controls_Button" />&nbsp;
          <asp:Button ID="Button_Admin" runat="server" Text="Visit Administration" CssClass="Controls_Button" OnClick="Button_Admin_Click" />&nbsp;
          </td>
        </tr>
        <tr>
          <td>
            <div>
              &nbsp;
            </div>
          </td>
        </tr>
      </table>
      <table id="TableList" cellspacing="0" cellpadding="0" border="0" runat="server" width="700px">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Record" cellspacing="0" cellpadding="0" width="100%">
              <tr class="Row">
                <td>Total Records:
                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td>
                  <asp:ListView ID="ListView_Isidima_Category" runat="server" DataSourceID="SqlDataSource_Isidima_Category" OnPreRender="ListView_Isidima_Category_PreRender" OnDataBound="ListView_Isidima_Category_DataBound">
                    <EmptyDataTemplate>
                      <table class="GridNoRecords" cellspacing="0" cellpadding="0" width="100%">
                        <tr class="NoRecords">
                          <td>No records
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td>&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                      <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server" id="Table_Isidima_Category">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                      </table>
                      <table cellpadding="0" width="100%" cellspacing="0" style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border: 0px solid #dfdfdf; border-left: 0px none; background-image: url('App_Images/CaptionBg.gif'); color: #000000; text-align: center; vertical-align: middle; white-space: nowrap;">
                        <tr>
                          <td style="text-align: center;">
                            <asp:DataPager runat="server" ID="DataPager_Isidima_Category" PageSize="5">
                              <Fields>
                                <asp:TemplatePagerField OnPagerCommand="PagerCommand">
                                  <PagerTemplate>
                                    <table cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border: 0px solid #dfdfdf; border-left: 0px none; background-image: url('App_Images/CaptionBg.gif'); color: #000000; text-align: center; vertical-align: middle; white-space: nowrap; color: #000000;">
                                      <tr>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>Records Per Page:
                                        </td>
                                        <td>
                                          <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                          </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                          <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                        <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                                        </td>
                                        <td>Page
                                        </td>
                                        <td>
                                          <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                          </asp:DropDownList>
                                        </td>
                                        <td>of
                                        <asp:Label ID="Label_TotalPages" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                          <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                        <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                      </tr>
                                    </table>
                                  </PagerTemplate>
                                </asp:TemplatePagerField>
                              </Fields>
                            </asp:DataPager>
                          </td>
                        </tr>
                      </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                      <tr runat="server">
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                          <asp:HyperLink ID="Link" Text='<%# GetLinkForm0(Eval("Isidima_Category_Id") , Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>&nbsp;
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold;">Report Number
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                          <asp:Label ID="Label_Isidima_Category_ReportNumber" runat="server" Text='<%#Eval("Isidima_Category_ReportNumber") %>' />
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold;">Patient Category
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                          <asp:Label ID="Label_Isidima_Category_PatientCategory_Name" runat="server" Text='<%#Eval("Isidima_Category_PatientCategory_Name") %>' />
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold;">Date
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                          <asp:Label ID="Label_Isidima_Category_Date" runat="server" Text='<%#Eval("Isidima_Category_Date") %>' />
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold;">Is Active
                        </td>
                        <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: left; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                          <asp:Label ID="Label_Isidima_Category_IsActive" runat="server" Text='<%#Eval("Isidima_Category_IsActive") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td style="height: 5px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; background-color: #f7f7f7; width: 50px">&nbsp;
                        </td>
                        <td style="height: 5px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; background-color: #f7f7f7; width: 650px;" colspan="8">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="8">
                          <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 125px;">Administration (MHA)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 125px;">Administration (VPA)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 70px;">Child (J)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 100px;">Discharge (DMH)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 70px;">Function (F)
                              </td>
                              <td colspan="3" style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 100px;">Independence (I)
                              </td>
                            </tr>
                            <tr>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_MHA" runat="server" Text='<%#Eval("MHA") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_VPA" runat="server" Text='<%#Eval("VPA") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_J" runat="server" Text='<%#Eval("J") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_DMH" runat="server" Text='<%#Eval("DMH") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_F" runat="server" Text='<%#Eval("F") %>' />
                              </td>
                              <td colspan="3" style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_I" runat="server" Text='<%#Eval("I") %>' />
                              </td>
                            </tr>
                            <tr>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 125px;">Mental Health (PSY)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 125px;">Personality Traits (T)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 70px;">Physical (B)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 100px;">Recreational (R)
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 70px;">Social (S)
                              </td>
                              <td colspan="3" style="font-size: 11px; font-family: Arial, Verdana; padding: 3px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #ebf2f8; color: #000000; font-weight: bold; width: 100px;">Vocational (V)
                              </td>
                            </tr>
                            <tr>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_PSY" runat="server" Text='<%#Eval("PSY") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_T" runat="server" Text='<%#Eval("T") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_B" runat="server" Text='<%#Eval("B") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_R" runat="server" Text='<%#Eval("R") %>' />
                              </td>
                              <td style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_S" runat="server" Text='<%#Eval("S") %>' />
                              </td>
                              <td colspan="3" style="font-size: 11px; font-family: Arial, Verdana; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; text-align: center; vertical-align: top; background-color: #f7f7f7; color: #000000;">
                                <asp:Label ID="Label_V" runat="server" Text='<%#Eval("V") %>' />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="9" style="height: 10px; border-top: 1px solid #dfdfdf; border-left: 1px solid #dfdfdf; background-color: #ebf2f8; text-align: center;">
                          <hr style="width: 90%; color: #003768; height: 2px;" />
                        </td>
                      </tr>
                    </ItemTemplate>
                  </asp:ListView>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_Category" runat="server" OnSelected="SqlDataSource_Isidima_Category_Selected"></asp:SqlDataSource>
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
