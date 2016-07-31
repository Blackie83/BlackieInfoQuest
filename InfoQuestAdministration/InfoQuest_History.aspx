<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.InfoQuest_History" CodeBehind="InfoQuest_History.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest Administration - History</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_History" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <table border="0">
        <tr>
          <td>
            <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="ImageButton_NoHand" />
          </td>
          <td style="width: 25px"></td>
          <td style="color: #003768; font-size: 18px;">
            <strong>Info</strong><strong style="color: #b0262e">Q</strong><strong>uest History</strong>
          </td>
          <td style="width: 25px"></td>
          <td>&nbsp;
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table border="0">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">InfoQuest History
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
            <table class="Record">
              <tr>
                <td>
                  <asp:GridView ID="GridView_Administration_HistorySplit" runat="server" AutoGenerateColumns="True" DataSourceID="SqlDataSource_Administration_HistorySplit" CssClass="Table" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_Administration_HistorySplit_RowCreated">
                    <HeaderStyle CssClass="Caption" HorizontalAlign="Left" />
                    <AlternatingRowStyle CssClass="AltRow" />
                    <RowStyle CssClass="Row" />
                    <FooterStyle CssClass="Footer" />
                    <PagerStyle CssClass="Pager" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                      <table class="GridNoRecords">
                        <tr class="NoRecords">
                          <td>No History
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td>&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EmptyDataTemplate>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource_Administration_HistorySplit" runat="server" SelectCommand="spAdministration_Execute_HistorySplit" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                      <asp:Parameter Name="StringToSplit" Type="String" />
                    </SelectParameters>
                  </asp:SqlDataSource>
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
