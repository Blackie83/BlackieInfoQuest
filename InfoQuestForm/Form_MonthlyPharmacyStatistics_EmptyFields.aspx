﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_MonthlyPharmacyStatistics_EmptyFields.aspx.cs" Inherits="InfoQuestForm.Form_MonthlyPharmacyStatistics_EmptyFields" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Monthly Pharmacy Statistics Empty Fields</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_MonthlyPharmacyStatistics_EmptyFields" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MonthlyPharmacyStatistics_EmptyFields" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MonthlyPharmacyStatistics_EmptyFields" AssociatedUpdatePanelID="UpdatePanel_MonthlyPharmacyStatistics_EmptyFields">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MonthlyPharmacyStatistics_EmptyFields" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">
                <asp:Label ID="Label_Title" runat="server"></asp:Label>
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SearchHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Facility
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Month From
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_PeriodFrom" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PeriodFrom" DataTextField="MPS_Period" DataValueField="MPS_Period">
                        <asp:ListItem Value="">Select Month</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PeriodFrom" runat="server"></asp:SqlDataSource>
                    </td>
                    <td>Month To
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_PeriodTo" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_PeriodTo" DataTextField="MPS_Period" DataValueField="MPS_Period">
                        <asp:ListItem Value="">Select Month</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_PeriodTo" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>FY Period From
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FYPeriodFrom" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_FYPeriodFrom" DataTextField="MPS_FYPeriod" DataValueField="MPS_FYPeriod">
                        <asp:ListItem Value="">Select Period</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_FYPeriodFrom" runat="server"></asp:SqlDataSource>
                    </td>
                    <td>FY Period To
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FYPeriodTo" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_FYPeriodTo" DataTextField="MPS_FYPeriod" DataValueField="MPS_FYPeriod">
                        <asp:ListItem Value="">Select Period</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_FYPeriodTo" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Empty Field</td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_EmptyField" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Empty Field</asp:ListItem>
                        <asp:ListItem Value="Negative Stock">Negative Stock</asp:ListItem>
                        <asp:ListItem Value="Cost Reduction Opportunities Realized">Cost Reduction Opportunities Realized</asp:ListItem>
                      </asp:DropDownList>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>          
          <div id="DivEmptyFields1" runat="server">
            &nbsp;
          </div>
          <div id="DivEmptyFields2" runat="server">
            &nbsp;
          </div>
          <table id="TableEmptyFields" class="Table" runat="server">
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
                    <td>Total Records:
                      <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_MonthlyPharmacyStatistics_EmptyFields" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_MonthlyPharmacyStatistics_EmptyFields" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_MonthlyPharmacyStatistics_EmptyFields_PreRender" OnDataBound="GridView_MonthlyPharmacyStatistics_EmptyFields_DataBound" OnRowCreated="GridView_MonthlyPharmacyStatistics_EmptyFields_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" HorizontalAlign="Left" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                          <%=GridView_MonthlyPharmacyStatistics_EmptyFields.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" HorizontalAlign="Left" />
                        <FooterStyle CssClass="GridView_FooterStyle" HorizontalAlign="Center" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
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
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="MPS_Period" HeaderText="Month" ReadOnly="True" SortExpression="MPS_Period" />
                          <asp:BoundField DataField="MPS_FYPeriod" HeaderText="FY Period" ReadOnly="True" SortExpression="MPS_FYPeriod" />
                          <asp:BoundField DataField="MPS_Pharmacy_NegativeStock" HeaderText="Negative Stock" ReadOnly="True" SortExpression="MPS_Pharmacy_NegativeStock" />
                          <asp:BoundField DataField="MPS_Pharmacy_CostReductionOpportunitiesRealized" HeaderText="Cost Reduction" ReadOnly="True" SortExpression="MPS_Pharmacy_CostReductionOpportunitiesRealized" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_MonthlyPharmacyStatistics_EmptyFields" runat="server" OnSelected="SqlDataSource_MonthlyPharmacyStatistics_EmptyFields_Selected"></asp:SqlDataSource>
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
