﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoQuest_Updated.aspx.cs" Inherits="InfoQuestForm.InfoQuest_Updated" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Updated</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Updated" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Updated" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Updated" AssociatedUpdatePanelID="UpdatePanel_Updated">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Updated" runat="server">
        <ContentTemplate>
          <table style="width: 100%;" border="0">
            <tr>
              <td style="height: 20px">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="text-align: center;">
                <div style="font-family: Verdana; color: #003768; font-size: 30px">
                  Info<strong style="color: #b0262e; font-weight: normal">Q</strong>uest
                </div>
              </td>
            </tr>
            <tr>
              <td style="height: 20px">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="text-align: center;">
                <div style="font-family: Verdana; color: #003768; font-size: 18px; text-decoration: underline">
                  Form Updated
                </div>
              </td>
            </tr>
            <tr>
              <td style="text-align: center;">
                <div style="font-family: Verdana; color: #b0262e; font-size: 18px">
                  <asp:Label ID="Label_FormName" runat="server" Text=""></asp:Label>
                </div>
              </td>
            </tr>
            <tr>
              <td style="height: 10px">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="font-family: Verdana; color: #003768; font-size: 18px; text-align: center;">
                <asp:HyperLink ID="Hyperlink_View" runat="server">View updated form</asp:HyperLink>
                <asp:Label ID="Label_Split" runat="server" Text="&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;" style="font-family: Verdana; color: #b0262e; font-size: 18px; font-weight:bold"></asp:Label>
                <asp:HyperLink ID="Hyperlink_Captured" runat="server">Capture a new form</asp:HyperLink>
              </td>
            </tr>
            <tr>
              <td style="height: 20px">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="text-align: center;">
                <img alt="" src="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" />
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
