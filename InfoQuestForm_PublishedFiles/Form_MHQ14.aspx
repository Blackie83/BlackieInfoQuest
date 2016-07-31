<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_MHQ14" CodeBehind="Form_MHQ14.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />--%>
  <title>InfoQuest - Mental Health Questionnaire 14</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MHQ14.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_MHQ14" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MHQ14" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MHQ14" AssociatedUpdatePanelID="UpdatePanel_MHQ14">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MHQ14" runat="server">
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
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_MHQ14_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_MHQ14_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PatientVisitNumber" runat="server" TargetControlID="TextBox_PatientVisitNumber" FilterType="Numbers">
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
                      <asp:Button ID="Button_GoToRequired" runat="server" Text="Go To Required" CssClass="Controls_Button" OnClick="Button_GoToRequired_Click" CausesValidation="False" />&nbsp;
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go To Captured Forms" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TablePatientInfo" class="Table" style="width: 700px;" runat="server">
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
          <table id="TableForm" class="Table" style="width: 700px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_QuestionnaireHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_MHQ14_Form" runat="server" Width="700px" DataKeyNames="MHQ14_Questionnaire_Id" CssClass="Record" DataSourceID="SqlDataSource_MHQ14_Form" OnItemInserting="FormView_MHQ14_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_MHQ14_Form_ItemCommand" OnDataBound="FormView_MHQ14_Form_DataBound" OnItemUpdating="FormView_MHQ14_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="5">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Report Number
                        </td>
                        <td colspan="4" style="width: 530px;">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5" class="FormView_TableBodyHeader">
                          Admission Questionnaire
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDate">Assessment Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="TextBox_InsertADMSDate" runat="server" Width="75px" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertADMSDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_InsertADMSDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertADMSDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertADMSDate">
                    </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertADMSDate" runat="server" TargetControlID="TextBox_InsertADMSDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">Diagnosis
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ1">1. Do you suffer from a substance abuse disorder (e.g. alcohol or other)
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSDiagnosisQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q1")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ2">2. Do you suffer from a mental health disorder
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSDiagnosisQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q2")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ3">3. Do you suffer from more than one mental health disorder
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSDiagnosisQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q3")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5" id="FormADMSDiagnosisQ4List">4. Select the diagnosis which is most relevant to this admission
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSDiagnosisQ4List" runat="server" DataSourceID="SqlDataSource_MHQ14_InsertQuestionnaireADMSDiagnosisQ4List" DataTextField="ListItem_Name" DataValueField="ListItem_Id" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List")%>'></asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">How much time during the past week
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1A" style="width: 170px;">1A. Have you been a very nervous person
                        </td>
                        <td colspan="4" style="width: 530px;">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ1A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1A")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1B">1B. Have you felt so down in the dumps that nothing could cheer you up
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1B")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1C">1C. Have you felt calm and peaceful
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ1C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1C")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1D">1D. Have you felt down
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ1D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1D")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1E">1E. Have you been a happy person
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ1E" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1E")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 1 Score</strong>
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="Textbox_InsertADMSSection1Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section1Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">How much of the time during the past week
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2A">2A. Did you feel full of life
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ2A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2A")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2B">2B. Did you have a lot of energy
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2B")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2C">2C. Did you feel worn out
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ2C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2C")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2D">2D. Did you feel tired
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ2D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2D")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 2 Score</strong>
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="Textbox_InsertADMSSection2Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section2Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ3A">3A. How much of the time have your emotional problems interfered  with your social activities (like visiting with friends, relatives etc.)
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ3A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q3A")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ3B">3B. To what extent have your emotional problems interfered with your normal social activities with family, friends, neighbors, or groups
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q3B")%>'>
                            <asp:ListItem Value="100">Not at all&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="75">Slightly&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="50">Moderately&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="25">Quite a bit&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Extremely&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 3 Score</strong>
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="Textbox_InsertADMSSection3Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section3Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">During the past week, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4A">4A. Cut down the amount of time you spent on work or other activities
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ4A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4A")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4B">4B. Accomplished less than you would like
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ4B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4B")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4C">4C. Didn't do work or other activities as carefully as usual
                        </td>
                        <td colspan="4">
                          <asp:RadioButtonList ID="RadioButtonList_InsertADMSQ4C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4C")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 4 Score</strong>
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="Textbox_InsertADMSSection4Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section4Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Admission Score</strong>
                        </td>
                        <td colspan="4">
                          <asp:TextBox ID="Textbox_InsertADMSScore" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("MHQ14_Questionnaire_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Questionnaire" CssClass="Controls_Button" />&nbsp;
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
                        <td style="width: 170px;">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                        <td rowspan="39" style="width: 20px;">&nbsp;
                        </td>
                        <td style="width: 170px;">Complete Discharge<br />
                          Questionnaire
                        </td>
                        <td style="width: 170px;">
                          <asp:DropDownList ID="DropDownList_EditDISCHCompleteDischarge" runat="server" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_CompleteDischarge") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;" class="FormView_TableBodyHeader">
                          Admission Questionnaire
                        </td>
                        <td colspan="2" style="width: 340px;" class="FormView_TableBodyHeader">
                          Discharge Questionnaire
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDate">Assessment Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditADMSDate" runat="server" Width="75px" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditADMSDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_EditADMSDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditADMSDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditADMSDate">
                    </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditADMSDate" runat="server" TargetControlID="TextBox_EditADMSDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                        <td id="FormDISCHDate">
                          <asp:Label ID="Label_EditDISCHDateText" runat="server" Text="Assessment Date<br />(yyyy/mm/dd)"></asp:Label>
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDISCHDate" runat="server" Width="75px" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDISCHDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_EditDISCHDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDISCHDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDISCHDate">
                    </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDISCHDate" runat="server" TargetControlID="TextBox_EditDISCHDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="MHQ14QuesionnaireDISCHNoDischargeReasonList1">
                        <td colspan="2" style="width: 340px;">&nbsp;
                        </td>
                        <td colspan="2" id="FormDISCHNoDischargeReasonList" style="width: 340px;">
                          <asp:Label ID="Label_EditDISCHNoDischargeReasonListText" runat="server" Text="Discharge Questionnaire could not be completed because"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="MHQ14QuesionnaireDISCHNoDischargeReasonList2">
                        <td colspan="2" style="width: 340px;">&nbsp;
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:DropDownList ID="DropDownList_EditDISCHNoDischargeReasonList" runat="server" DataSourceID="SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_NoDischargeReason_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reason</asp:ListItem>
                          </asp:DropDownList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">Diagnosis
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_EditDISCHText" runat="server" Text="Diagnosis"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ1" style="width: 170px;">1. Do you suffer from a substance abuse disorder (e.g. alcohol or other)
                        </td>
                        <td style="width: 170px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSDiagnosisQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q1")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                        <td id="FormDISCHDiagnosisQ1" style="width: 170px;">
                          <asp:Label ID="Label_EditDISCHDiagnosisQ1Text" runat="server" Text="1. Do you suffer from a substance abuse disorder (e.g. alcohol or other)"></asp:Label>&nbsp;
                        </td>
                        <td style="width: 170px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHDiagnosisQ1" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Diagnosis_Q1")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ2">2. Do you suffer from a mental health disorder
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSDiagnosisQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q2")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                        <td id="FormDISCHDiagnosisQ2">
                          <asp:Label ID="Label_EditDISCHDiagnosisQ2Text" runat="server" Text="2. Do you suffer from a mental health disorder"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHDiagnosisQ2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Diagnosis_Q2")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSDiagnosisQ3">3. Do you suffer from more than one mental health disorder
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSDiagnosisQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q3")%>'>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                        <td id="FormDISCHDiagnosisQ3">
                          <asp:Label ID="Label_EditDISCHDiagnosisQ3Text" runat="server" Text="3. Do you suffer from more than one mental health disorder"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHDiagnosisQ3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Diagnosis_Q3")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="True">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="False">No&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;" id="FormADMSDiagnosisQ4List">4. Select the diagnosis which is most relevant to this admission
                        </td>
                        <td colspan="2" style="width: 340px;" id="FormDISCHDiagnosisQ4List">
                          <asp:Label ID="Label_EditDISCHDiagnosisQ4ListText" runat="server" Text="4. Select the diagnosis which is most relevant to this discharge"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSDiagnosisQ4List" runat="server" DataSourceID="SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List" DataTextField="ListItem_Name" DataValueField="ListItem_Id" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List")%>'></asp:RadioButtonList>&nbsp;
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHDiagnosisQ4List" runat="server" DataSourceID="SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                          </asp:RadioButtonList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">How much time during the past week
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditDISCHQ1Text" runat="server" Text="How much time during the past week"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1A">1A. Have you been a very nervous person
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ1A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1A")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ1A">
                          <asp:Label ID="Label_EditDISCHQ1AText" runat="server" Text="1A. Have you been a very nervous person"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ1A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q1A")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1B">1B. Have you felt so down in the dumps that nothing could cheer you up
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1B")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ1B">
                          <asp:Label ID="Label_EditDISCHQ1BText" runat="server" Text="1B. Have you felt so down in the dumps that nothing could cheer you up"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ1B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q1B")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1C">1C. Have you felt calm and peaceful
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ1C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1C")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ1C">
                          <asp:Label ID="Label_EditDISCHQ1CText" runat="server" Text="1C. Have you felt calm and peaceful"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ1C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q1C")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1D">1D. Have you felt down
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ1D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1D")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ1D">
                          <asp:Label ID="Label_EditDISCHQ1DText" runat="server" Text="1D. Have you felt down"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ1D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q1D")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1E">1E. Have you been a happy person
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ1E" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q1E")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ1E">
                          <asp:Label ID="Label_EditDISCHQ1EText" runat="server" Text="1E. Have you been a happy person"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ1E" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q1E")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 1 Score</strong>
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditADMSSection1Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section1Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHSection1ScoreText" runat="server" Text="Section 1 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHSection1Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section1Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">How much of the time during the past week
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditDISCHQ2Text" runat="server" Text="How much of the time during the past week"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2A">2A. Did you feel full of life
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ2A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2A")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ2A">
                          <asp:Label ID="Label_EditDISCHQ2AText" runat="server" Text="2A. Did you feel full of life"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ2A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q2A")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2B">2B. Did you have a lot of energy
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2B")%>'>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ2B">
                          <asp:Label ID="Label_EditDISCHQ2BText" runat="server" Text="2B. Did you have a lot of energy"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ2B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q2B")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2C">2C. Did you feel worn out
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ2C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2C")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ2C">
                          <asp:Label ID="Label_EditDISCHQ2CText" runat="server" Text="2C. Did you feel worn out"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ2C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q2C")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ2D">2D. Did you feel tired
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ2D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q2D")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ2D">
                          <asp:Label ID="Label_EditDISCHQ2DText" runat="server" Text="2D. Did you feel tired"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ2D" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q2D")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 2 Score</strong>
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditADMSSection2Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section2Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHSection2ScoreText" runat="server" Text="Section 2 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHSection2Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section2Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ3A">3A. How much of the time have your emotional problems interfered  with your social activities (like visiting with friends, relatives etc.)
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ3A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q3A")%>'>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ3A">
                          <asp:Label ID="Label_EditDISCHQ3AText" runat="server" Text="3A. How much of the time have your emotional problems interfered  with your social activities (like visiting with friends, relatives etc.)"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ3A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q3A")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">All of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="20">Most of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="40">A good bit of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="60">Some of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="80">A little of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">None of the time&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ3B">3B. To what extent have your emotional problems interfered with your normal social activities with family, friends, neighbors, or groups
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q3B")%>'>
                            <asp:ListItem Value="100">Not at all&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="75">Slightly&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="50">Moderately&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="25">Quite a bit&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Extremely&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ3B">
                          <asp:Label ID="Label_EditDISCHQ3BText" runat="server" Text="3B. To what extent have your emotional problems interfered with your normal social activities with family, friends, neighbours, or groups"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ3B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q3B")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">Not at all&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="75">Slightly&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="50">Moderately&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="25">Quite a bit&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Extremely&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 3 Score</strong>
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditADMSSection3Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section3Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHSection3ScoreText" runat="server" Text="Section 3 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHSection3Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section3Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">During the past week, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_EditDISCHQ4Text" runat="server" Text="During the past week, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4A">4A. Cut down the amount of time you spent on work or other activities
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ4A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4A")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ4A">
                          <asp:Label ID="Label_EditDISCHQ4AText" runat="server" Text="4A. Cut down the amount of time you spent on work or other activities"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ4A" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q4A")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4B">4B. Accomplished less than you would like
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ4B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4B")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ4B">
                          <asp:Label ID="Label_EditDISCHQ4BText" runat="server" Text="4B. Accomplished less than you would like"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ4B" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q4B")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ4C">4C. Didn't do work or other activities as carefully as usual
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditADMSQ4C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_ADMS_Q4C")%>'>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td id="FormDISCHQ4C">
                          <asp:Label ID="Label_EditDISCHQ4CText" runat="server" Text="4C. Didn't do work or other activities as carefully as usual"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:RadioButtonList ID="RadioButtonList_EditDISCHQ4C" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SelectedValue='<%# Bind("MHQ14_Questionnaire_DISCH_Q4C")%>'>
                            <asp:ListItem Value="">DISCHEmptyValue&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">Yes&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="100">No&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 4 Score</strong>
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditADMSSection4Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section4Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHSection4ScoreText" runat="server" Text="Section 4 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHSection4Score" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section4Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Admission Score</strong>
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditADMSScore" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHScoreText" runat="server" Text="Discharge Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHScore" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Score") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_EditDISCHDifferenceText" runat="server" Text="Difference"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <asp:TextBox ID="Textbox_EditDISCHDifference" Width="50px" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Difference") %>' ReadOnly="true" CssClass="Controls_TextBox_Calculation"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("MHQ14_Questionnaire_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Questionnaire" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                    <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                    <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Questionnaire" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                        <td style="width: 170px;">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                        <td rowspan="39" style="width: 20px;">&nbsp;
                        </td>
                        <td style="width: 170px;">Complete Discharge<br />
                          Questionnaire
                        </td>
                        <td style="width: 170px;">
                          <asp:Label ID="Label_ItemDISCHCompleteDischarge" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_CompleteDischarge") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemDISCHCompleteDischarge" runat="server" Value='<%# Eval("MHQ14_Questionnaire_DISCH_CompleteDischarge") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">
                          Admission Questionnaire
                        </td>
                        <td colspan="2" class="FormView_TableBodyHeader">
                          Discharge Questionnaire
                        </td>
                      </tr>
                      <tr>
                        <td>Assessment Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDateText" runat="server" Text="Assessment Date<br />(yyyy/mm/dd)"></asp:Label>
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="MHQ14QuesionnaireDISCHNoDischargeReasonList1">
                        <td colspan="2" style="width: 340px;">&nbsp;
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHNoDischargeReasonListText" runat="server" Text="Discharge Questionnaire could not be completed because"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="MHQ14QuesionnaireDISCHNoDischargeReasonList2">
                        <td colspan="2" style="width: 340px;">&nbsp;
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHNoDischargeReasonList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">Diagnosis
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHText" runat="server" Text="Diagnosis"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">1. Do you suffer from a substance abuse disorder (e.g. alcohol or other)
                        </td>
                        <td style="width: 170px;">
                          <asp:Label ID="Label_ItemADMSDiagnosisQ1" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td style="width: 170px;">
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ1Text" runat="server" Text="1. Do you suffer from a substance abuse disorder (e.g. alcohol or other)"></asp:Label>&nbsp;
                        </td>
                        <td style="width: 170px;">
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ1" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>2. Do you suffer from a mental health disorder
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSDiagnosisQ2" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ2Text" runat="server" Text="2. Do you suffer from a mental health disorder"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ2" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>3. Do you suffer from more than one mental health disorder
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSDiagnosisQ3" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ3Text" runat="server" Text="3. Do you suffer from more than one mental health disorder"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ3" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">4. Select the diagnosis which is most relevant to this admission
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ4ListText" runat="server" Text="4. Select the diagnosis which is most relevant to this discharge"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemADMSDiagnosisQ4List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHDiagnosisQ4List" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">How much time during the past week
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemDISCHQ1Text" runat="server" Text="How much time during the past week"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>1A. Have you been a very nervous person
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ1A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1AText" runat="server" Text="1A. Have you been a very nervous person"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormADMSQ1B">1B. Have you felt so down in the dumps that nothing could cheer you up
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ1B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1BText" runat="server" Text="1B. Have you felt so down in the dumps that nothing could cheer you up"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>1C. Have you felt calm and peaceful
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ1C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1CText" runat="server" Text="1C. Have you felt calm and peaceful"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>1D. Have you felt down
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ1D" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1DText" runat="server" Text="1D. Have you felt down"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1D" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>1E. Have you been a happy person
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ1E" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1EText" runat="server" Text="1E. Have you been a happy person"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ1E" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 1 Score</strong>
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemADMSSection1Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section1Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection1ScoreText" runat="server" Text="Section 1 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection1Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section1Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">How much of the time during the past week
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemDISCHQ2Text" runat="server" Text="How much of the time during the past week"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>2A. Did you feel full of life
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ2A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2AText" runat="server" Text="2A. Did you feel full of life"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>2B. Did you have a lot of energy
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ2B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2BText" runat="server" Text="2B. Did you have a lot of energy"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>2C. Did you feel worn out
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ2C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2CText" runat="server" Text="2C. Did you feel worn out"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>2D. Did you feel tired
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ2D" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2DText" runat="server" Text="2D. Did you feel tired"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ2D" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 2 Score</strong>
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemADMSSection2Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section2Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection2ScoreText" runat="server" Text="Section 2 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection2Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section2Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>3A. How much of the time have your emotional problems interfered  with your social activities (like visiting with friends, relatives etc.)
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ3A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ3AText" runat="server" Text="3A. How much of the time have your emotional problems interfered  with your social activities (like visiting with friends, relatives etc.)"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ3A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>3B. To what extent have your emotional problems interfered with your normal social activities with family, friends, neighbors, or groups
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ3B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ3BText" runat="server" Text="3B. To what extent have your emotional problems interfered with your normal social activities with family, friends, neighbours, or groups"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ3B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 3 Score</strong>
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemADMSSection3Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section3Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection3ScoreText" runat="server" Text="Section 3 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection3Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section3Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 340px;">During the past week, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)
                        </td>
                        <td colspan="2" style="width: 340px;">
                          <asp:Label ID="Label_ItemDISCHQ4Text" runat="server" Text="During the past week, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>4A. Cut down the amount of time you spent on work or other activities
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ4A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4AText" runat="server" Text="4A. Cut down the amount of time you spent on work or other activities"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4A" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>4B. Accomplished less than you would like
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ4B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4BText" runat="server" Text="4B. Accomplished less than you would like"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4B" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>4C. Didn't do work or other activities as carefully as usual
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemADMSQ4C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4CText" runat="server" Text="4C. Didn't do work or other activities as carefully as usual"></asp:Label>&nbsp;
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDISCHQ4C" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Section 4 Score</strong>
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemADMSSection4Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Section4Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection4ScoreText" runat="server" Text="Section 4 Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHSection4Score" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Section4Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Admission Score</strong>
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemADMSScore" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ADMS_Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHScoreText" runat="server" Text="Discharge Score"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHScore" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Score") %>'></asp:Label></strong>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHDifferenceText" runat="server" Text="Difference"></asp:Label></strong>&nbsp;
                        </td>
                        <td>
                          <strong>
                            <asp:Label ID="Label_ItemDISCHDifference" runat="server" Text='<%# Bind("MHQ14_Questionnaire_DISCH_Difference") %>'></asp:Label></strong>&nbsp;
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("MHQ14_Questionnaire_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="4">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("MHQ14_Questionnaire_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="5">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Questionnaire" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_MHQ14_InsertQuestionnaireADMSDiagnosisQ4List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_MHQ14_Form" runat="server" OnInserted="SqlDataSource_MHQ14_Form_Inserted" OnUpdated="SqlDataSource_MHQ14_Form_Updated"></asp:SqlDataSource>
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
