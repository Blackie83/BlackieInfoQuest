<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.InfoQuest_Email" CodeBehind="InfoQuest_Email.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Email</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_Email.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body style="margin: 2px;">
  <form id="form_Email" runat="server">
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_EmailAddress" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Email" AssociatedUpdatePanelID="UpdatePanel_Email">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Email" runat="server">
        <ContentTemplate>
          <table style="width: 724px;">
            <tr>
              <td style="padding: 0px;">
                <Header:HeaderText ID="HeaderText_Page" runat="server" />
              </td>
            </tr>
          </table>
          <table style="width: 730px;">
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header" style="width: 680px;">Info<span style="color: #b0262e">Q</span>uest Email
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table style="width: 730px;">
            <tr>
              <td style="color: #003768;">Email a link of the <span style="color: #b0262e; text-decoration: underline; font-weight: bold;">
                <asp:Label ID="Label_FormName" runat="server" Text=""></asp:Label></span> to the selected people
              </td>
            </tr>
            <tr>
              <td style="height: 10px">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="color: #003768;">Please select the people to receive a email and click Send Email.
              </td>
            </tr>
            <tr>
              <td style="color: #003768;">A description can be added to the email.
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableEmail" style="width: 720px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Email
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
                      <asp:Label ID="Label_EmailMessage" runat="server" CssClass="Controls_EmailMessage"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Select Email address
                    </td>
                    <td style="width: 570px; padding: 0px;">
                      <div id="InsertComplaintCategoryItemList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                        <asp:CheckBoxList ID="CheckBoxList_EmailAddress" runat="server" Width="100%" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_InfoQuest_Email" DataTextField="DisplayName_Email" DataValueField="SecurityUser_Email" RepeatDirection="Vertical">
                        </asp:CheckBoxList>
                      </div>
                      <asp:SqlDataSource ID="SqlDataSource_InfoQuest_Email" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr id="EnterEmailAddress" runat="server">
                    <td style="width: 150px;">Enter Email address
                    </td>
                    <td style="width: 570px;">
                      <asp:TextBox ID="TextBox_EmailAddress" runat="server" Width="520px" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EmailAddress_TextChanged"></asp:TextBox>
                      <Ajax:AutoCompleteExtender ID="AutoCompleteExtender_EmailAddress" runat="server" TargetControlID="TextBox_EmailAddress" ServiceMethod="GetEmailAddressList" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="10" EnableCaching="true" UseContextKey="True" DelimiterCharacters=";, :" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListCssClass="Controls_AutoCompleteExtender_CompletionListCssClass" CompletionListHighlightedItemCssClass="Controls_AutoCompleteExtender_CompletionListHighlightedItemCssClass" CompletionListItemCssClass="Controls_AutoCompleteExtender_CompletionListItemCssClass">
                      </Ajax:AutoCompleteExtender>
                      <br />
                      <asp:Label ID="Label_EmailAddressError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px;">Description
                    </td>
                    <td style="width: 570px;">
                      <asp:TextBox ID="TextBox_Description" runat="server" TextMode="MultiLine" Rows="4" Width="520px" Text="" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
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
                      <asp:Button ID="Button_SendEmail" runat="server" Text="Send Email" CssClass="Controls_Button" OnClick="Button_SendEmail_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table style="width: 724px;">
            <tr>
              <td style="padding: 0px;">
                <Footer:FooterText ID="FooterText_Page" runat="server" />
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </form>
</body>
</html>
