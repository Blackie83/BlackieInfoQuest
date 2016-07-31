<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_PharmacySurveys_Create.aspx.cs" Inherits="InfoQuestForm.Form_PharmacySurveys_Create" %>

<%@ Register Assembly="InfoQuestWCF" Namespace="InfoQuestWCF" TagPrefix="CheckBoxListAttribute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy Surveys Create</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PharmacySurveys_Create.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PharmacySurveys_Create" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PharmacySurveys_Create" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PharmacySurveys_Create" AssociatedUpdatePanelID="UpdatePanel_PharmacySurveys_Create">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PharmacySurveys_Create" runat="server">
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
          <table id="TableForm" class="Table" style="width: 900px;" runat="server">
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
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_InvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 170px;" id="FormFacility">Facility
                    </td>
                    <td style="width: 730px;">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PharmacySurveys_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 170px;" id="FormLoadedSurveysName">Survey
                    </td>
                    <td style="width: 730px;">
                      <asp:DropDownList ID="DropDownList_LoadedSurveysName" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PharmacySurveys_LoadedSurveysName" DataTextField="LoadedSurveys_Name" DataValueField="LoadedSurveys_Id" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_LoadedSurveysName_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Survey</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedSurveysName" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Survey Information
                    </td>
                  </tr>
                  <tr>
                    <td>Previous Survey Created
                    </td>
                    <td>
                      <asp:Label ID="Label_PreviousSurvey" runat="server"></asp:Label>
                      <asp:HiddenField ID="HiddenField_PreviousSurvey" runat="server" />
                      <asp:HiddenField ID="HiddenField_SurveyActive" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Person Search
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <table style="width: 100%">
                        <tr>
                          <td style="width: 49%; padding: 0px;">
                            <table style="width: 100%">
                              <tr>
                                <td class="FormView_TableBodyHeader">Employees to Complete Survey - Active</td>
                              </tr>
                              <tr>
                                <td style="padding: 0px; border-left-width: 0px; border-top-width: 1px; height:321px;">
                                  <div style="max-height: 321px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                                    <CheckBoxListAttribute:Override_CheckBoxList ID="CheckBoxList_CreatedSurveysName" runat="server" Width="420px" CellPadding="0" CellSpacing="0" RepeatLayout="Table" CssClass="Controls_CheckBoxListWithScrollbars">
                                    </CheckBoxListAttribute:Override_CheckBoxList>
                                  </div>
                                </td>
                              </tr>
                              <tr class="FormView_TableFooter">
                                <td>
                                  <asp:Button ID="Button_RemoveSelected" runat="server" Text="Remove Selected" CssClass="Controls_Button" OnClick="Button_RemoveSelected_Click" />&nbsp;
                                  <asp:Button ID="Button_RemoveAll" runat="server" Text="Remove All" CssClass="Controls_Button" OnClick="Button_RemoveAll_Click" />&nbsp;
                                </td>
                              </tr>
                              <tr>
                                <td class="FormView_TableBodyHeader">Employees to Complete Survey - Cancelled</td>
                              </tr>
                              <tr>
                                <td style="padding: 0px; border-left-width: 0px; border-top-width: 1px; height:100px;">
                                  <div style="max-height: 100px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                                    <CheckBoxListAttribute:Override_CheckBoxList ID="CheckBoxList_CreatedSurveysNameCanceled" runat="server" Width="420px" CellPadding="0" CellSpacing="0" RepeatLayout="Table" CssClass="Controls_CheckBoxListWithScrollbars">
                                    </CheckBoxListAttribute:Override_CheckBoxList>
                                  </div>
                                </td>
                              </tr>
                              <tr class="FormView_TableFooter">
                                <td>
                                  <asp:Button ID="Button_ReAddSelected" runat="server" Text="Re-Add Selected" CssClass="Controls_Button" OnClick="Button_ReAddSelected_Click" />&nbsp;
                                  <asp:Button ID="Button_ReAddAll" runat="server" Text="Re-Add All" CssClass="Controls_Button" OnClick="Button_ReAddAll_Click" />&nbsp;</td>
                              </tr>
                            </table>
                          </td>
                          <td style="width: 2%"></td>
                          <td style="width: 49%; padding: 0px;">
                            <table style="width: 100%">
                              <tr>
                                <td colspan="2" class="FormView_TableBodyHeader">Search for Employees</td>
                              </tr>
                              <tr>
                                <td colspan="2">
                                  <asp:Label ID="Label_SearchMessage" runat="server" CssClass="Controls_Validation"></asp:Label>&nbsp;
                                </td>
                              </tr>
                              <tr>
                                <td>Username</td>
                                <td>
                                  <asp:TextBox ID="TextBox_SearchUserName" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                                </td>
                              </tr>
                              <tr>
                                <td>Name or<br />Surname</td>
                                <td>
                                  <asp:TextBox ID="TextBox_SearchName" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                                </td>
                              </tr>
                              <tr>
                                <td>Employee<br />Number</td>
                                <td>
                                  <asp:TextBox ID="TextBox_SearchEmployeeNumber" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                                </td>
                              </tr>
                              <tr class="FormView_TableFooter">
                                <td colspan="2">
                                  <asp:Button ID="Button_SearchClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_SearchClear_Click" />&nbsp;
                                  <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                                </td>
                              </tr>
                              <tr>
                                <td colspan="2" class="FormView_TableBodyHeader">List of Employees</td>
                              </tr>
                              <tr>
                                <td colspan="2" style="height: 309px; padding: 0px; border-left-width: 0px; border-top-width: 1px;">
                                  <div style="max-height: 309px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                                    <CheckBoxListAttribute:Override_CheckBoxList ID="CheckBoxList_SearchName" runat="server" Width="420px" CellPadding="0" CellSpacing="0" RepeatLayout="Table" CssClass="Controls_CheckBoxListWithScrollbars">
                                    </CheckBoxListAttribute:Override_CheckBoxList>
                                  </div>
                                </td>
                              </tr>
                              <tr class="FormView_TableFooter">
                                <td colspan="2">
                                  <asp:Button ID="Button_AddSelected" runat="server" Text="Add Selected" CssClass="Controls_Button" OnClick="Button_AddSelected_Click" />&nbsp;
                                  <asp:Button ID="Button_AddAll" runat="server" Text="Add All" CssClass="Controls_Button" OnClick="Button_AddAll_Click" />&nbsp;
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                      <asp:Button ID="Button_Create" runat="server" Text="Create / Modify Survey" CssClass="Controls_Button" OnClick="Button_Create_Click" />&nbsp;
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
