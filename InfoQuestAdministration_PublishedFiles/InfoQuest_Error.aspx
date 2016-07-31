<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.InfoQuest_Error" CodeBehind="InfoQuest_Error.aspx.cs" %>

<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest Administration - Error</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Error" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <table style="width: 100%;">
        <tr>
          <td style="text-align: center;">
            <img alt="" src="App_Images/ErrorCircle-256x256.png" />
          </td>
        </tr>
        <tr>
          <td style="height: 20px"></td>
        </tr>
        <tr>
          <td style="text-align: center;">
            <div style="font-size: 20px; color: #003768; font-family: Verdana">
              There is a problem with the page.
            </div>
          </td>
        </tr>
        <tr>
          <td style="height: 10px">&nbsp;
          </td>
        </tr>
        <tr>
          <td style="text-align: center;">
            <div style="font-size: 20px; color: #003768; font-family: Verdana">
              <a href="" onclick="history.back();return false;" style="font-size: 20px; color: #b0262e; font-family: Verdana">Click Here</a> to go back to the page and try again or Please try again later
            </div>
          </td>
        </tr>
        <tr>
          <td style="height: 10px">&nbsp;
          </td>
        </tr>
        <tr>
          <td style="text-align: center;">
            <div style="font-size: 20px; color: #003768; font-family: Verdana">
              If the problem persists, Please log a call with Contact Centre.
            </div>
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
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
