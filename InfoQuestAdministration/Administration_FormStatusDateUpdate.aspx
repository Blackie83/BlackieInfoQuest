<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_FormStatusDateUpdate" CodeBehind="Administration_FormStatusDateUpdate.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Form Status Date Update</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_FormStatusDateUpdate" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_FormStatusDateUpdate" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_FormStatusDateUpdate" AssociatedUpdatePanelID="UpdatePanel_FormStatusDateUpdate">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_FormStatusDateUpdate" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Form Status Date Update
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body" style="width: 600px;">
                  <tr>
                    <td style="width: 100px;">Report Number
                    </td>
                    <td style="width: 500px;">
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                      &nbsp;
                    <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">Report Number
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_ReportNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">Form Status
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_Status" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">OLD Status Date
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_OLD_StatusDate" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">OLD Month
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_OLD_Month" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">NEW Status Date
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_NEW_StatusDate" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">NEW Month
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_NEW_Month" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 100px;">Query
                    </td>
                    <td style="width: 500px;">
                      <asp:Label ID="Label_Query" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="text-align: center;">
                      <asp:Button ID="Button_Update" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_Update_Click" />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="text-align: center;">
                      <asp:Label ID="Label_UpdateSuccess" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
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
