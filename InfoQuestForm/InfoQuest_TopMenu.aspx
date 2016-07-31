<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.InfoQuest_TopMenu" CodeBehind="InfoQuest_TopMenu.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Top Menu</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body style="margin-left: 0; margin-top: 0; margin-bottom: 10px; overflow-x: scroll; overflow-y: hidden;">
  <form id="form_TopMenu" runat="server">
    <div>
      <table cellspacing="0" cellpadding="0" style="width: 100%">
        <tr>
          <td colspan="5" nowrap="nowrap">
            <table class="Header" cellspacing="0" cellpadding="0" style="border: 0">
              <tr>
                <td class="HeaderLeft" nowrap="nowrap">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <th class="Headerth" nowrap="nowrap">
                  <table cellspacing="0" cellpadding="0" style="width: 100%; border: 0; height: 20px">
                    <tr>
                      <td nowrap="nowrap" style="text-align: left;">
                        <asp:Label ID="MenuContent" runat="server" Text=""></asp:Label>
                      </td>
                    </tr>
                  </table>
                </th>
                <td class="HeaderRight" nowrap="nowrap">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
