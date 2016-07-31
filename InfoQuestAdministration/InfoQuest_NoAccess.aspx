<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoQuest_NoAccess.aspx.cs" Inherits="InfoQuestAdministration.InfoQuest_NoAccess" %>

<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest Administration - No Access</title>
</head>
<body>
  <form id="form_NoAccess" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <div>
        &nbsp;
      </div>
      <asp:Label ID="label_PageText" runat="server"></asp:Label>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
