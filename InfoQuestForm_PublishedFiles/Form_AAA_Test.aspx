<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_AAA_Test.aspx.cs" Inherits="InfoQuestForm.Form_AAA_Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="Header" TagPrefix="Controls_Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="Navigation" TagPrefix="Controls_Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="Footer" TagPrefix="Controls_Footer" %>
<%@ Register Src="App_Controls/Controls_TimePicker.ascx" TagName="TimePicker" TagPrefix="Controls_TimePicker" %>
<%@ Register Src="App_Controls/Controls_DatePicker.ascx" TagName="DatePicker" TagPrefix="Controls_DatePicker" %>
<%@ Register Src="App_Controls/Controls_DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="Controls_DateTimePicker" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - AAA Test</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/ckeditor/ckeditor.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_JavaScripts/jQuery/jquery-3.0.0.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>"></script>
  </asp:PlaceHolder>
  <style>
    .Table_PCServer {
      border: solid;
      border-width: 3px;
      border-color: #4CAF50;
      border-collapse: collapse;
      width: 500px;
    }

    .Table_PCServer tr:hover {
      background-color: #4CAF50 !important;
      color: #ffffff !important;
    }

    .Table_PCServer td {
      vertical-align: top;
      padding: 5px;
    }

    .Table_PCServer tr:nth-child(odd) {
      background-color: #f2f2f2;
    }

    .Table_PCServer_Header {
      background-color: #4CAF50;
      color: #ffffff;
      text-align: center;
    }

    .Table_CKEditor {
      border: solid;
      border-width: 3px;
      border-color: #4CAF50;
      border-collapse: collapse;
      width: 1000px;
    }

    .Table_CKEditor td {
      vertical-align: top;
      padding: 5px;
    }

    .Table_CKEditor_Header {
      background-color: #4CAF50;
      color: #ffffff;
      text-align: center;
    }

    .Table_DateTimePicker {
      border: solid;
      border-width: 3px;
      border-color: #4CAF50;
      border-collapse: collapse;
    }

    .Table_DateTimePicker td {
      vertical-align: middle;
      padding: 5px;
    }

    .Table_DateTimePicker_Header {
      background-color: #4CAF50;
      color: #ffffff;
      text-align: center;
    }
  </style>
</head>
<body id="Body_AAA_Test" runat="server">
  <form id="form_AAA_Test" runat="server">
    <Controls_Header:Header ID="HeaderText_Page" runat="server" />
    <Controls_Navigation:Navigation ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_AAA_Test" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_AAA_Test" AssociatedUpdatePanelID="UpdatePanel_AAA_Test">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_AAA_Test" runat="server">
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
          <table class="Table_PCServer">
            <tr>
              <td colspan="2" class="Table_PCServer_Header">
                <stong>PC and Server Info</stong>
              </td>
            </tr>
            <tr>
              <td style="width: 75px;">
                <strong>PC Name</strong>:
              </td>
              <td>
                <asp:Label ID="Label_PCName" runat="server"></asp:Label>
              </td>
            </tr>
            <tr>
              <td style="width: 75px;">
                <strong>PC IP</strong>:
              </td>
              <td>
                <asp:Label ID="Label_PCIP" runat="server"></asp:Label>
              </td>
            </tr>
            <tr>
              <td style="width: 75px;">
                <strong>Server IP</strong>:
              </td>
              <td>
                <asp:Label ID="Label_ServerIP" runat="server"></asp:Label>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table_CKEditor">
            <tr>
              <td class="Table_CKEditor_Header"><strong>CKEditor</strong></td>
            </tr>
            <tr>
              <td>
                <asp:TextBox ID="TextBox_CKEditor" Rows="15" Width="100%" TextMode="MultiLine" runat="server" />
                <script>CKEDITOR.replace('TextBox_CKEditor');</script>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table_DateTimePicker">
            <tr>
              <td colspan="5" class="Table_DateTimePicker_Header"><strong>Time Picker</strong></td>
            </tr>
            <tr>
              <td>
                <asp:Button ID="Button_ClearTime" runat="server" OnClick="Button_ClearTime_Click" Text="Clear Time" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Button ID="Button_SetTime" runat="server" OnClick="Button_SetTime_Click" Text="Set Time" CssClass="Controls_Button" />
              </td>
              <td id="Time">
                <Controls_TimePicker:TimePicker runat="server" ID="ControlsTimePicker_SelectTime" />
              </td>
              <td>
                <asp:Button ID="Button_GetTime" runat="server" OnClick="Button_GetTime_Click" Text="Get Time" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Label ID="Label_Time" runat="server" Text="Hour:Minute"></asp:Label>&nbsp;
              </td>
            </tr>
            <tr>
              <td colspan="5" class="Table_DateTimePicker_Header"><strong>Date Picker</strong></td>
            </tr>
            <tr>
              <td>
                <asp:Button ID="Button_ClearDate" runat="server" OnClick="Button_ClearDate_Click" Text="Clear Date" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Button ID="Button_SetDate" runat="server" OnClick="Button_SetDate_Click" Text="Set Date" CssClass="Controls_Button" />
              </td>
              <td id="Date">
                <Controls_DatePicker:DatePicker runat="server" ID="ControlsDatePicker_SelectDate" />
              </td>
              <td>
                <asp:Button ID="Button_GetDate" runat="server" OnClick="Button_GetDate_Click" Text="Get Date" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Label ID="Label_Date" runat="server" Text="Year/Month/Day"></asp:Label>&nbsp;
              </td>
            </tr>
            <tr>
              <td colspan="5" class="Table_DateTimePicker_Header"><strong>Date Time Picker</strong></td>
            </tr>
            <tr>
              <td>
                <asp:Button ID="Button_ClearDateTime" runat="server" OnClick="Button_ClearDateTime_Click" Text="Clear Date Time" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Button ID="Button_SetDateTime" runat="server" OnClick="Button_SetDateTime_Click" Text="Set Date Time" CssClass="Controls_Button" />
              </td>
              <td id="DateTime">
                <Controls_DateTimePicker:DateTimePicker runat="server" ID="ControlsDateTimePicker_SelectDateTime" />
              </td>
              <td>
                <asp:Button ID="Button_GetDateTime" runat="server" OnClick="Button_GetDateTime_Click" Text="Get Date Time" CssClass="Controls_Button" />
              </td>
              <td>
                <asp:Label ID="Label_DateTime" runat="server" Text="Year/Month/Day Hour:Minute"></asp:Label>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table_ShowHide">
            <tr>
              <td>
                <p id="ShowHideMe">If you click on the "Hide" button, I will disappear.</p>
                <asp:Button id="Button_Hide" OnClientClick="Hide('ShowHideMe');" runat="server" OnClick="Button_Hide_Click" Text="Hide" CssClass="Controls_Button" />
                <asp:Button id="Button_Show" OnClientClick="Show('ShowHideMe');" runat="server" OnClick="Button_Show_Click" Text="Show" CssClass="Controls_Button" />
              </td>
            </tr>
          </table>
          <div style="height: 1000px;">
            &nbsp;
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Controls_Footer:Footer ID="FooterText_Page" runat="server" />
  </form>
  <script src="App_JavaScripts/Form_AAA_Test.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
</body>
</html>
