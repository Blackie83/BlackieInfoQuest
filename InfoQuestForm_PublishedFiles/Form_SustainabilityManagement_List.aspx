﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_SustainabilityManagement_List.aspx.cs" Inherits="InfoQuestForm.Form_SustainabilityManagement_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Sustainability Management List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SustainabilityManagement_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SustainabilityManagement_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SustainabilityManagement_List" AssociatedUpdatePanelID="UpdatePanel_SustainabilityManagement_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SustainabilityManagement_List" runat="server">
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
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_SustainabilityManagement_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Month
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Period" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_SustainabilityManagement_Period" DataTextField="SustainabilityManagement_Period" DataValueField="SustainabilityManagement_Period">
                        <asp:ListItem Value="">Select Month</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_Period" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>FY Period
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FYPeriod" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_SustainabilityManagement_FYPeriod" DataTextField="SustainabilityManagement_FYPeriod" DataValueField="SustainabilityManagement_FYPeriod">
                        <asp:ListItem Value="">Select Period</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_FYPeriod" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr class="Bottom">
                    <td colspan="2" style="text-align: right;">
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
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
                    <td>
                      Total Records:
                      <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_SustainabilityManagement_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SustainabilityManagement_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_SustainabilityManagement_List_PreRender" OnDataBound="GridView_SustainabilityManagement_List_DataBound" OnRowCreated="GridView_SustainabilityManagement_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
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
                          <%=GridView_SustainabilityManagement_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
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
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("SustainabilityManagement_Id"),Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="SustainabilityManagement_Period" HeaderText="Month" ReadOnly="True" SortExpression="SustainabilityManagement_Period" />
                          <asp:BoundField DataField="SustainabilityManagement_FYPeriod" HeaderText="FY Period" ReadOnly="True" SortExpression="SustainabilityManagement_FYPeriod" />
                          <asp:BoundField DataField="Closed" HeaderText="Closed" ReadOnly="True" SortExpression="Closed" />
                          <asp:BoundField DataField="SustainabilityManagement_ModifiedBy" HeaderText="Modified By" ReadOnly="True" SortExpression="SustainabilityManagement_ModifiedBy" />
                          <asp:BoundField DataField="SustainabilityManagement_ModifiedDate" HeaderText="Modified Date" ReadOnly="True" SortExpression="SustainabilityManagement_ModifiedDate" />
                          <asp:BoundField DataField="SustainabilityManagement_BeingModified" HeaderText="Being Modified" ReadOnly="True" SortExpression="SustainabilityManagement_BeingModified" />
                          <asp:BoundField DataField="SustainabilityManagement_BeingModifiedBy" HeaderText="Being Modified By" ReadOnly="True" SortExpression="SustainabilityManagement_BeingModifiedBy" />
                          <asp:BoundField DataField="SustainabilityManagement_BeingModifiedDate" HeaderText="Being Modified Date" ReadOnly="True" SortExpression="SustainabilityManagement_BeingModifiedDate" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_List" runat="server" OnSelected="SqlDataSource_SustainabilityManagement_List_Selected"></asp:SqlDataSource>
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
