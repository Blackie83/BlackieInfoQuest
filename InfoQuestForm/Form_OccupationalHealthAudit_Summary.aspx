<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_OccupationalHealthAudit_Summary.aspx.cs" Inherits="InfoQuestForm.Form_OccupationalHealthAudit_Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - QMS Review Summary</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_OccupationalHealthAudit_Summary.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_OccupationalHealthAudit_Summary" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_OccupationalHealthAudit_Summary" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_OccupationalHealthAudit_Summary" AssociatedUpdatePanelID="UpdatePanel_OccupationalHealthAudit_Summary">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_OccupationalHealthAudit_Summary" runat="server">
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
          <table id="TableReviewInfo" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_ReviewHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 115px">Facility
                    </td>
                    <td>
                      <asp:Label ID="Label_Facility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Unit
                    </td>
                    <td>
                      <asp:Label ID="Label_Unit" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px;">Audit Date
                    </td>
                    <td>
                      <asp:Label ID="Label_Date" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px;">Completed
                    </td>
                    <td>
                      <asp:Label ID="Label_Completed" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableButtons" runat="server">
            <tr>
              <td style="text-align: center;">
                <asp:Button ID="Button_Print" runat="server" Text="Print Summary" CssClass="Controls_Button" OnClick="Button_Print_Click" />&nbsp;
                <asp:Button ID="Button_Email" runat="server" Text="Email Link" CssClass="Controls_Button" OnClick="Button_Email_Click" />&nbsp;
                <asp:Button ID="Button_Back" runat="server" Text="Back to Audit List" CssClass="Controls_Button" OnClick="Button_Back_Click" />&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableList" class="Table" style="width: 700px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_OccupationalHealthAudit_Summary_List" runat="server" Width="100%" ShowHeader="false" AutoGenerateColumns="False" DataSourceID="SqlDataSource_OccupationalHealthAudit_Summary_List" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnRowCreated="GridView_OccupationalHealthAudit_Summary_List_RowCreated" OnPreRender="GridView_OccupationalHealthAudit_Summary_List_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No Summary
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="OHA_Summary_Contribution" HeaderText="Contribution" ReadOnly="True" SortExpression="OHA_Summary_Contribution" />
                          <asp:BoundField DataField="OHA_Summary_Element" HeaderText="Element" ReadOnly="True" SortExpression="OHA_Summary_Element" />
                          <asp:BoundField DataField="OHA_Summary_ElementScore" HeaderText="ElementScore" ReadOnly="True" SortExpression="OHA_Summary_ElementScore" />
                          <asp:BoundField DataField="OHA_Summary_TotalScore" HeaderText="TotalScore" ReadOnly="True" SortExpression="OHA_Summary_TotalScore" />
                          <asp:BoundField DataField="OHA_Summary_Identifier" HeaderText="" ReadOnly="True" SortExpression="OHA_Summary_Identifier" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_OccupationalHealthAudit_Summary_List" runat="server"></asp:SqlDataSource>
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
