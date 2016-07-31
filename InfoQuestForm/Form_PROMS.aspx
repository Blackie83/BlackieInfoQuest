<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_PROMS" Codebehind="Form_PROMS.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - PROMS</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PROMS.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="GotoBookmark();Validation_Search();Validation_Form0();Validation_Form1();Calculation_Form0();Calculation_Form1();ShowHide_Form0();ShowHide_Form1();">
  <form id="form_PROMS" runat="server" defaultbutton="Button_Search">
  <Header:HeaderText ID="HeaderText_Page" runat="server" />
  <div>
    <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PROMS" runat="server" CombineScripts="false">
    </Ajax:ToolkitScriptManager>
    <table cellspacing="0" cellpadding="0" border="0">
      <tr>
        <td>
          <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
        </td>
        <td style="width: 25px">
        </td>
        <td style="color: #003768; font-size: 18px; font-weight: bold; padding-top: 20px; padding-bottom: 5px">
          <asp:Label ID="Label_Title" runat="server" Text=""></asp:Label>
        </td>
        <td style="width: 25px">
        </td>
        <td>
          &nbsp;
        </td>
      </tr>
    </table>
    <div>
      &nbsp;
    </div>
    <table cellspacing="0" cellpadding="0" border="0">
      <tr>
        <td style="vertical-align:top;">
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
                <asp:ValidationSummary ID="ValidationSummary_Find" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="PROMS_Find" CssClass="Controls_Validation" />
              </td>
            </tr>
            <tr class="Controls">
              <td class="th" colspan="2">
                <asp:Label ID="Label_InvalidSearch" runat="server" CssClass="Controls_Validation"></asp:Label>
              </td>
            </tr>
            <tr>
              <td runat="server" style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; vertical-align:top;">
                <table width="100%" style="height: 25px" id="SearchFacility">
                  <tr>
                    <td>
                      Facility
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; padding: 3px;">
                <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PROMS_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                  <asp:ListItem Value="">Select Facility</asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource_PROMS_Facility" runat="server"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Facility" runat="server" ErrorMessage="" ControlToValidate="DropDownList_Facility" Display="Dynamic" ValidationGroup="PROMS_Find"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr>
              <td runat="server" style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; vertical-align:top;">
                <table width="100%" style="height: 27px" id="SearchPatientVisitNumber">
                  <tr>
                    <td>
                      Patient Visit Number
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #F7F7F7; border-top: 1px solid #dfdfdf; border-right: 1px solid #dfdfdf; padding: 3px;">
                <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_PatientVisitNumber" runat="server" ErrorMessage="" ControlToValidate="TextBox_PatientVisitNumber" Display="Dynamic" ValidationGroup="PROMS_Find"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr class="Bottom">
              <td colspan="2" style="text-align: right;">
                <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" CausesValidation="False" />&nbsp;
                <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" ValidationGroup="PROMS_Find" OnClick="Button_Search_Click" />&nbsp;
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
    <a id="LinkPatientInfo"></a>
    <div>
      &nbsp;
    </div>
    <table id="TablePatientInfo" cellspacing="0" cellpadding="0" width="700" border="0" runat="server">
      <tr>
        <td style="vertical-align:top;">
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
              <td style="width: 115px">
                Facility:
              </td>
              <td>
                <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Visit Number:
              </td>
              <td>
                <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Surname, Name:
              </td>
              <td>
                <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Age:
              </td>
              <td>
                <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Date of Admission:
              </td>
              <td>
                <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Date of Discharge:
              </td>
              <td>
                <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
            <tr class="Row">
              <td style="width: 115px">
                Contact Number:
              </td>
              <td>
                <asp:Label ID="Label_PIContactNumber" runat="server" Text=""></asp:Label>&nbsp;
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <div>
      &nbsp;
    </div>
    <table id="TableForm" cellspacing="0" cellpadding="0" width="700" border="0" runat="server">
      <tr>
        <td>
          <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tr>
              <td style="vertical-align:top;">
                <table id="TableForm0" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                  <tr>
                    <td style="text-align: center;">
                      <table class="Header" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                          <td class="HeaderLeft">
                            <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                          </td>
                          <td class="Headerth" style="text-align: center; font-weight: bold;">
                            <asp:Label ID="Label_QuestionnaireHeading" runat="server" Text=""></asp:Label>
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
                      <asp:FormView ID="FormView_PROMS_Questionnaire_Form" runat="server" Width="100%" DataKeyNames="PROMS_Questionnaire_Id" CssClass="Record" DataSourceID="SqlDataSource_PROMS_Questionnaire_Form" OnItemInserting="FormView_PROMS_Questionnaire_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_PROMS_Questionnaire_Form_ItemCommand" OnDataBound="FormView_PROMS_Questionnaire_Form_DataBound" OnItemUpdating="FormView_PROMS_Questionnaire_Form_ItemUpdating">
                        <InsertItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                                <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                                <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px;">
                                Report Number
                              </td>
                              <td style="width: 535px;">
                                <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("PROMS_Questionnaire_ReportNumber") %>'></asp:Label>
                                <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                                <asp:HiddenField ID="HiddenField_InsertQuestionnaireTotalQuestions" runat="server" OnDataBinding="HiddenField_InsertQuestionnaireTotalQuestions_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td id="Form0Questionnaire" style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_InsertQuestionnaireList" runat="server" AutoPostBack="true" DataSourceID="SqlDataSource_PROMS_InsertQuestionnaireList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_Questionnaire_Questionnaire_List") %>' CssClass="Controls_DropDownList" OnSelectedIndexChanged="DropDownList_InsertQuestionnaireList_SelectedIndexChanged">
                                  <asp:ListItem Value="">Select Questionnaire</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideAdmissionDate">
                              <td style="width: 165px;">
                                Admission Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertAdmissionDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_AdmissionDate") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ1">
                              <td id="Form0Q1" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q1")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ2">
                              <td id="Form0Q2" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q2")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ3">
                              <td id="Form0Q3" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q3")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ4">
                              <td id="Form0Q4" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q4")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ5">
                              <td id="Form0Q5" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q5")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ6">
                              <td id="Form0Q6" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q6")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ7">
                              <td id="Form0Q7" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q7")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ8">
                              <td id="Form0Q8" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q8")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ9">
                              <td id="Form0Q9" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ9" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q9")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ10">
                              <td id="Form0Q10" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ10" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q10")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ11">
                              <td id="Form0Q11" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ11" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q11")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ12">
                              <td id="Form0Q12" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ12" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q12")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_InsertScore" Width="25px" runat="server" Text='<%# Bind("PROMS_Questionnaire_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                                <strong>out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactPatient">
                              <td id="Form0ContactPatient" style="width: 165px;">
                                Patient to be contacted in the future
                              </td>
                              <td>
                                <asp:RadioButtonList ID="RadioButtonList_InsertContactPatient" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_ContactPatient")%>'>
                                  <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                                  <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactNumber">
                              <td id="Form0ContactNumber" style="width: 165px;">
                                Contact Number
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertContactNumber" runat="server" Width="100px" Text='<%# Bind("PROMS_Questionnaire_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideLanguage">
                              <td id="Form0Language" style="width: 165px;">
                                Response Language
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_InsertLanguageList" runat="server" DataSourceID="SqlDataSource_PROMS_InsertLanguageList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_Questionnaire_Language_List") %>' CssClass="Controls_DropDownList">
                                  <asp:ListItem Value="">Select Language</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailSend">
                              <td style="width: 165px;">
                                Reminder email send to contact Patient
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertEmailSend" runat="server" Text='<%# Bind("PROMS_Questionnaire_EmailSend") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailDate">
                              <td style="width: 165px;">
                                Reminder email send date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertEmailDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_EmailDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Is Active
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("PROMS_Questionnaire_IsActive") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Questionnaire" CssClass="Controls_Button" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                                <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                                <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px;">
                                Report Number
                              </td>
                              <td style="width: 535px;">
                                <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("PROMS_Questionnaire_ReportNumber") %>'></asp:Label>
                                <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                                <asp:HiddenField ID="HiddenField_EditQuestionnaireTotalQuestions" runat="server" OnDataBinding="HiddenField_EditQuestionnaireTotalQuestions_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td id="Form0Questionnaire" style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_EditQuestionnaireList" runat="server" AutoPostBack="true" DataSourceID="SqlDataSource_PROMS_EditQuestionnaireList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_Questionnaire_Questionnaire_List") %>' CssClass="Controls_DropDownList" OnSelectedIndexChanged="DropDownList_EditQuestionnaireList_SelectedIndexChanged">
                                  <asp:ListItem Value="">Select Questionnaire</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideAdmissionDate">
                              <td style="width: 165px;">
                                Admission Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditAdmissionDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_AdmissionDate") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ1">
                              <td id="Form0Q1" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q1")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ2">
                              <td id="Form0Q2" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q2")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ3">
                              <td id="Form0Q3" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q3")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ4">
                              <td id="Form0Q4" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q4")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ5">
                              <td id="Form0Q5" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q5")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ6">
                              <td id="Form0Q6" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q6")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ7">
                              <td id="Form0Q7" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q7")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ8">
                              <td id="Form0Q8" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q8")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ9">
                              <td id="Form0Q9" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ9" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q9")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ10">
                              <td id="Form0Q10" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ10" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q10")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ11">
                              <td id="Form0Q11" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ11" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q11")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ12">
                              <td id="Form0Q12" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ12" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_Q12")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_EditScore" Width="25px" runat="server" Text='<%# Bind("PROMS_Questionnaire_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                                <strong>out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactPatient">
                              <td id="Form0ContactPatient" style="width: 165px;">
                                Patient to be contacted in the future
                              </td>
                              <td>
                                <asp:RadioButtonList ID="RadioButtonList_EditContactPatient" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_Questionnaire_ContactPatient")%>'>
                                  <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                                  <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactNumber">
                              <td id="Form0ContactNumber" style="width: 165px;">
                                Contact Number
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_EditContactNumber" runat="server" Width="100px" Text='<%# Bind("PROMS_Questionnaire_ContactNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideLanguage">
                              <td id="Form0Language" style="width: 165px;">
                                Response Language
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_EditLanguageList" runat="server" DataSourceID="SqlDataSource_PROMS_EditLanguageList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_Questionnaire_Language_List") %>' CssClass="Controls_DropDownList">
                                  <asp:ListItem Value="">Select Language</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailSend">
                              <td style="width: 165px;">
                                Reminder email send to contact Patient
                              </td>
                              <td>
                                <asp:Label ID="Label_EditEmailSend" runat="server" Text='<%# (bool)(Eval("PROMS_Questionnaire_EmailSend"))?"Yes":"No" %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailDate">
                              <td style="width: 165px;">
                                Reminder email send date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditEmailDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_EmailDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Is Active
                              </td>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("PROMS_Questionnaire_IsActive") %>' />&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Questionnaire" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                                <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                                <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Questionnaire" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EditItemTemplate>
                        <ItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px; height: 20px;">
                                Report Number
                              </td>
                              <td style="width: 535px;">
                                <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("PROMS_Questionnaire_ReportNumber") %>'></asp:Label>
                                <asp:HiddenField ID="HiddenField_Item" runat="server" />
                                <asp:HiddenField ID="HiddenField_ItemQuestionnaireTotalQuestions" runat="server" OnDataBinding="HiddenField_ItemQuestionnaireTotalQuestions_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td id="Form0Questionnaire" style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemQuestionnaireList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideAdmissionDate">
                              <td style="width: 165px;">
                                Admission Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemAdmissionDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_AdmissionDate") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ1">
                              <td id="Form0Q1" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ1" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ2">
                              <td id="Form0Q2" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ2" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ3">
                              <td id="Form0Q3" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ3" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ4">
                              <td id="Form0Q4" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ4" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ5">
                              <td id="Form0Q5" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ5" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ6">
                              <td id="Form0Q6" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ6" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ7">
                              <td id="Form0Q7" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ7" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ8">
                              <td id="Form0Q8" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ8" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ9">
                              <td id="Form0Q9" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ9" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ10">
                              <td id="Form0Q10" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ10" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ11">
                              <td id="Form0Q11" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ11" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideQ12">
                              <td id="Form0Q12" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ12" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td style="height: 19px;">
                                <strong>
                                  <asp:Label ID="Textbox_ItemScore" runat="server" Text='<%# Bind("PROMS_Questionnaire_Score") %>'></asp:Label>
                                  &nbsp;out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactPatient">
                              <td id="Form0ContactPatient" style="width: 165px;">
                                Patient to be contacted in the future
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemContactPatient" runat="server" Text='<%# (bool)(Eval("PROMS_Questionnaire_ContactPatient"))?"Yes":"No" %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideContactNumber">
                              <td id="Form0ContactNumber" style="width: 165px;">
                                Contact Number
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemContactNumber" runat="server" Text='<%# Bind("PROMS_Questionnaire_ContactNumber") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideLanguage">
                              <td id="Form0Language" style="width: 165px;">
                                Response Language
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemLanguageList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailSend">
                              <td style="width: 165px;">
                                Reminder email send to contact Patient
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertEmailSend" runat="server" Text='<%# (bool)(Eval("PROMS_Questionnaire_EmailSend"))?"Yes":"No" %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form0ShowHideEmailDate">
                              <td style="width: 165px;">
                                Reminder email send date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertEmailDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_EmailDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("PROMS_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Is Active
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("PROMS_Questionnaire_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Questionnaire" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </ItemTemplate>
                      </asp:FormView>                      
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_InsertQuestionnaireList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_InsertLanguageList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_EditQuestionnaireList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_EditLanguageList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_Questionnaire_Form" runat="server" OnInserted="SqlDataSource_PROMS_Questionnaire_Form_Inserted" OnUpdated="SqlDataSource_PROMS_Questionnaire_Form_Updated"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="vertical-align:top;">
                <table id="TableForm1" cellspacing="0" cellpadding="0" width="350px" border="0" runat="server">
                  <tr>
                    <td rowspan="2">
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align: center;">
                      <table class="Header" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                          <td class="HeaderLeft">
                            <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                          </td>
                          <td class="Headerth" style="text-align: center; font-weight: bold;">
                            <asp:Label ID="Label_FollowUpHeading" runat="server" Text=""></asp:Label>
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
                      <asp:FormView ID="FormView_PROMS_FollowUp_Form" runat="server" Width="100%" DataKeyNames="PROMS_FollowUp_Id" CssClass="Record" DataSourceID="SqlDataSource_PROMS_FollowUp_Form" OnItemInserting="FormView_PROMS_FollowUp_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_PROMS_FollowUp_Form_ItemCommand" OnDataBound="FormView_PROMS_FollowUp_Form_DataBound" OnItemUpdating="FormView_PROMS_FollowUp_Form_ItemUpdating">
                        <InsertItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                                <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                                <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Cancel Follow Up Questionnaire
                              </td>
                              <td>
                                <asp:CheckBox ID="CheckBox_InsertCancelled" runat="server" Checked='<%# Bind("PROMS_FollowUp_Cancelled") %>' />
                                <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                                <asp:HiddenField ID="HiddenField_InsertFollowUpTotalQuestions" runat="server" OnDataBinding="HiddenField_InsertFollowUpTotalQuestions_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCancelledList">
                              <td id="Form1CancelledList" style="width: 165px;">
                                Cancelled Questionnaire reason
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_InsertCancelledList" runat="server" DataSourceID="SqlDataSource_PROMS_InsertCanceledList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_FollowUp_Cancelled_List") %>' CssClass="Controls_DropDownList">
                                  <asp:ListItem Value="">Select Reason</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertQuestionnaireList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCompletionDate">
                              <td style="width: 165px;">
                                Completion Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCompletionDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CompletionDate") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ1">
                              <td id="Form1Q1" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q1")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ2">
                              <td id="Form1Q2" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q2")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ3">
                              <td id="Form1Q3" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q3")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ4">
                              <td id="Form1Q4" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q4")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ5">
                              <td id="Form1Q5" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q5")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ6">
                              <td id="Form1Q6" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q6")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ7">
                              <td id="Form1Q7" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q7")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ8">
                              <td id="Form1Q8" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q8")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ9">
                              <td id="Form1Q9" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ9" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q9")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ10">
                              <td id="Form1Q10" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ10" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q10")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ11">
                              <td id="Form1Q11" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ11" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q11")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ12">
                              <td id="Form1Q12" style="width: 165px;">
                                <asp:Label ID="Label_InsertHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_InsertQ12" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q12")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_InsertScore" Width="25px" runat="server" Text='<%# Bind("PROMS_FollowUp_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                                <strong>out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Notes
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_InsertNotes" TextMode="MultiLine" Rows="4" Width="200px" runat="server" Text='<%# Bind("PROMS_FollowUp_Notes") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;                                
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Follow Up" CssClass="Controls_Button" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                                <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                                <asp:Label ID="Label_EditConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Cancel Follow Up Questionnaire
                              </td>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditCancelled" runat="server" Checked='<%# Bind("PROMS_FollowUp_Cancelled") %>' />
                                <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                                <asp:HiddenField ID="HiddenField_EditFollowUpTotalQuestions" runat="server" OnDataBinding="HiddenField_EditFollowUpTotalQuestions_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCancelledList">
                              <td id="Form1CancelledList" style="width: 165px;">
                                Cancelled Questionnaire reason
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_EditCancelledList" runat="server" DataSourceID="SqlDataSource_PROMS_EditCanceledList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("PROMS_FollowUp_Cancelled_List") %>' CssClass="Controls_DropDownList">
                                  <asp:ListItem Value="">Select Reason</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:Label ID="Label_EditQuestionnaireList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCompletionDate">
                              <td style="width: 165px;">
                                Completion Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCompletionDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CompletionDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ1">
                              <td id="Form1Q1" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q1")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ2">
                              <td id="Form1Q2" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q2")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ3">
                              <td id="Form1Q3" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q3")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ4">
                              <td id="Form1Q4" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ4" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q4")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ5">
                              <td id="Form1Q5" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q5")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ6">
                              <td id="Form1Q6" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ6" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q6")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ7">
                              <td id="Form1Q7" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ7" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q7")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ8">
                              <td id="Form1Q8" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ8" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q8")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ9">
                              <td id="Form1Q9" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ9" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q9")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ10">
                              <td id="Form1Q10" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ10" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q10")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ11">
                              <td id="Form1Q11" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ11" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q11")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ12">
                              <td id="Form1Q12" style="width: 165px;">
                                <asp:Label ID="Label_EditHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="padding: 0px">
                                <asp:RadioButtonList ID="RadioButtonList_EditQ12" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("PROMS_FollowUp_Q12")%>'>
                                </asp:RadioButtonList>
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_EditScore" Width="25px" runat="server" Text='<%# Bind("PROMS_FollowUp_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                                <strong>out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Notes
                              </td>
                              <td>
                                <asp:TextBox ID="Textbox_EditNotes" TextMode="MultiLine" Rows="4" Width="200px" runat="server" Text='<%# Bind("PROMS_FollowUp_Notes") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;                                
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                                <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Follow Up" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EditItemTemplate>
                        <ItemTemplate>
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="Row">
                              <td colspan="2">
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="height: 20px;">
                                Cancel Follow Up Questionnaire
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCancelled" runat="server" Text='<%# (bool)(Eval("PROMS_FollowUp_Cancelled"))?"Yes":"No" %>'></asp:Label>
                                <asp:HiddenField ID="HiddenField_ItemCancelled" runat="server" Value='<%# Bind("PROMS_FollowUp_Cancelled") %>' />
                                <asp:HiddenField ID="HiddenField_Item" runat="server" />
                                <asp:HiddenField ID="HiddenField_ItemFollowUpTotalQuestions" runat="server" OnDataBinding="HiddenField_ItemFollowUpTotalQuestions_DataBinding" />
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCancelledList">
                              <td id="Form1CancelledList" style="width: 165px;">
                                Cancelled Questionnaire reason
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCancelledList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td style="width: 165px;">
                                Questionnaire
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemQuestionnaireList" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideCompletionDate">
                              <td style="width: 165px;">
                                Completion Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCompletionDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CompletionDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ1">
                              <td id="Form1Q1" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ1" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ1" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ2">
                              <td id="Form1Q2" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ2" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ2" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ3">
                              <td id="Form1Q3" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ3" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ3" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ4">
                              <td id="Form1Q4" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ4" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ4" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ5">
                              <td id="Form1Q5" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ5" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ5" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ6">
                              <td id="Form1Q6" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ6" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ6" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ7">
                              <td id="Form1Q7" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ7" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ7" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ8">
                              <td id="Form1Q8" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ8" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ8" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ9">
                              <td id="Form1Q9" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ9" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ9" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ10">
                              <td id="Form1Q10" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ10" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ10" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ11">
                              <td id="Form1Q11" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ11" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ11" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideQ12">
                              <td id="Form1Q12" style="width: 165px;">
                                <asp:Label ID="Label_ItemHeadingQ12" runat="server"></asp:Label>
                              </td>
                              <td style="height: 94px;">
                                <asp:Label ID="Label_ItemQ12" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row" id="Form1ShowHideScore">
                              <td style="text-align: right;">
                                <strong>Score</strong>
                              </td>
                              <td style="height: 19px;">
                                <strong>
                                  <asp:Label ID="Textbox_ItemScore" runat="server" Text='<%# Bind("PROMS_FollowUp_Score") %>'></asp:Label>
                                  &nbsp;out of 48</strong>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Notes
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemNotes" runat="server" Text='<%# Bind("PROMS_FollowUp_Notes") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td colspan="2">
                                &nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Row">
                              <td>
                                Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("PROMS_FollowUp_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="Bottom">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </ItemTemplate>
                      </asp:FormView>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_EditCanceledList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_InsertCanceledList" runat="server"></asp:SqlDataSource>
                      <asp:SqlDataSource ID="SqlDataSource_PROMS_FollowUp_Form" runat="server" OnInserted="SqlDataSource_PROMS_FollowUp_Form_Inserted" OnUpdated="SqlDataSource_PROMS_FollowUp_Form_Updated"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <div>
      &nbsp;
    </div>
    <table id="TableList" cellspacing="0" cellpadding="0" border="0" runat="server">
      <tr>
        <td style="vertical-align:top;">
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
          <table class="Record" cellspacing="0" cellpadding="0">
            <tr class="Row">
              <td>
                Total Records:
                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
              </td>
            </tr>
            <tr>
              <td>
                <asp:GridView ID="GridView_PROMS_Questionnaire" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PROMS_Questionnaire" Width="700px" CssClass="Grid" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_PROMS_Questionnaire_PreRender" OnDataBound="GridView_PROMS_Questionnaire_DataBound" OnRowCreated="GridView_PROMS_Questionnaire_RowCreated">
                  <HeaderStyle CssClass="Caption" HorizontalAlign="Left" />
                  <AlternatingRowStyle CssClass="AltRow" />
                  <PagerTemplate>
                    <table cellpadding="0" cellspacing="0">
                      <tr>
                        <td>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                          Records Per Page:
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                        <td>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                          <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                          <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                        </td>
                        <td>
                          Page
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                          </asp:DropDownList>
                        </td>
                        <td>
                          of
                          <%=GridView_PROMS_Questionnaire.PageCount%>
                        </td>
                        <td>
                          <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                          <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
                        </td>
                        <td>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                          </tr>
                          <tr>
                            <td colspan="10">
                          <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New PROMS" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </PagerTemplate>
                  <RowStyle CssClass="Row" />
                  <FooterStyle CssClass="Footer" />
                  <PagerStyle CssClass="Pager" HorizontalAlign="Center" />
                  <EmptyDataTemplate>
                    <table class="GridNoRecords" cellspacing="0" cellpadding="0">
                      <tr class="NoRecords">
                        <td>
                          No records
                        </td>
                      </tr>
                      <tr class="Footer">
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr class="Footer">
                        <td style="text-align: center;">
                          <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New PROMS" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EmptyDataTemplate>
                  <Columns>
                    <asp:BoundField DataField="Link" HeaderText="" ReadOnly="True" SortExpression="Link" HtmlEncode="False" />
                    <asp:BoundField DataField="PROMS_Questionnaire_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="PROMS_Questionnaire_ReportNumber" />
                    <asp:BoundField DataField="PROMS_Questionnaire_Questionnaire_Name" HeaderText="Questionnaire" ReadOnly="True" SortExpression="PROMS_Questionnaire_Questionnaire_Name" />
                    <asp:BoundField DataField="PROMS_Questionnaire_Score" HeaderText="Score" ReadOnly="True" SortExpression="PROMS_Questionnaire_Score" />
                    <asp:BoundField DataField="PROMS_Questionnaire_ContactPatient" HeaderText="Contact Patient" ReadOnly="True" SortExpression="PROMS_Questionnaire_ContactPatient" />
                    <asp:BoundField DataField="PROMS_Questionnaire_EmailSend" HeaderText="Email Send" ReadOnly="True" SortExpression="PROMS_Questionnaire_EmailSend" />
                    <asp:BoundField DataField="FollowUpDate" HeaderText="Follow Up" ReadOnly="True" SortExpression="FollowUpDate" />
                    <asp:BoundField DataField="PROMS_Questionnaire_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="PROMS_Questionnaire_IsActive" />
                  </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_PROMS_Questionnaire" runat="server" OnSelected="SqlDataSource_PROMS_Questionnaire_Selected"></asp:SqlDataSource>
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
