<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_CollegeLearningAudit_Summary_Print.aspx.cs" Inherits="InfoQuestForm.Form_CollegeLearningAudit_Summary_Print" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - College Learning Audit Summary Print</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CollegeLearningAudit_Summary_Print" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CollegeLearningAudit_Summary_Print" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CollegeLearningAudit_Summary_Print" AssociatedUpdatePanelID="UpdatePanel_CollegeLearningAudit_Summary_Print">
        <ProgressTemplate>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CollegeLearningAudit_Summary_Print" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_1_col_black.jpg" AlternateText="" BorderWidth="0px" Height="50px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header" style="color: #000000;">
                <asp:Label ID="Label_Title" runat="server" Text=""></asp:Label>
              </td>
              <td style="width: 25px"></td>
              <td>
                <asp:Button ID="Button_Print" runat="server" Text="Print Summary" CssClass="Controls_Button" OnClick="Button_Print_Click" />&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableReviewInfo" class="Table" style="width: 700px; border: 0px;" runat="server">
            <tr>
              <td>
                <table class="Table_Body" style="border: 0px;">
                  <tr>
                    <td style="font-weight: bold; border-color: Black; border-width: 1px; border-style: solid;">Facility:&nbsp;
                    </td>
                    <td style="border-color: Black; border-width: 1px; border-style: solid;">
                      <asp:Label ID="Label_Facility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="font-weight: bold; border-color: Black; border-width: 1px; border-style: solid;">Audit Date:&nbsp;
                    </td>
                    <td style="border-color: Black; border-width: 1px; border-style: solid;">
                      <asp:Label ID="Label_Date" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableList" class="Table" style="width: 700px;" runat="server">
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_CollegeLearningAudit_Summary_List" runat="server" RowStyle-Font-Names="Verdana" ShowHeader="false" CellPadding="3" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource_CollegeLearningAudit_Summary_List" BorderWidth="0px" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_CollegeLearningAudit_Summary_List_PreRender">
                        <EmptyDataTemplate>
                          <table>
                            <tr>
                              <td>No Summary
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="CLA_Summary_Contribution" HeaderText="Contribution" ReadOnly="True" SortExpression="CLA_Summary_Contribution" />
                          <asp:BoundField DataField="CLA_Summary_Element" HeaderText="Element" ReadOnly="True" SortExpression="CLA_Summary_Element" />
                          <asp:BoundField DataField="CLA_Summary_SubElementScore" HeaderText="SubElementScore" ReadOnly="True" SortExpression="CLA_Summary_SubElementScore" />
                          <asp:BoundField DataField="CLA_Summary_ElementScore" HeaderText="ElementScore" ReadOnly="True" SortExpression="CLA_Summary_ElementScore" />
                          <asp:BoundField DataField="CLA_Summary_TotalScore" HeaderText="TotalScore" ReadOnly="True" SortExpression="CLA_Summary_TotalScore" />
                          <asp:BoundField DataField="CLA_Summary_Identifier" HeaderText="" ReadOnly="True" SortExpression="CLA_Summary_Identifier" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CollegeLearningAudit_Summary_List" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </form>
</body>
</html>
