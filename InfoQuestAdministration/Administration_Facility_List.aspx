﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Facility_List" CodeBehind="Administration_Facility_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Facility List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Facility_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Facility_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Facility_List" AssociatedUpdatePanelID="UpdatePanel_Facility_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Facility_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search Facilities</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Facility Name
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Id" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_Id" DataTextField="Facility_FacilityName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_Id" runat="server" SelectCommand="SELECT DISTINCT Facility_Id , Facility_FacilityName FROM Administration_Facility ORDER BY Facility_FacilityName"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility Code
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FacilityCode" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_FacilityCode" DataTextField="Facility_FacilityCode" DataValueField="Facility_FacilityCode">
                        <asp:ListItem Value="">Select Code</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_FacilityCode" runat="server" SelectCommand="SELECT DISTINCT Facility_FacilityCode FROM Administration_Facility ORDER BY Facility_FacilityCode"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility Type
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FacilityType" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_FacilityType" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_FacilityType" runat="server" SelectCommand="SELECT DISTINCT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_IsActive" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
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
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>List of Facilities</td>
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
                      <asp:GridView ID="GridView_Facility_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Facility_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Facility_List_PreRender" OnDataBound="GridView_Facility_List_DataBound" OnRowCreated="GridView_Facility_List_RowCreated">
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
                              <%=GridView_Facility_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Facility" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Facility" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Facility_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_Id" HeaderText="Id" ReadOnly="True" SortExpression="Facility_Id" />
                          <asp:BoundField DataField="Facility_FacilityName" HeaderText="Facility Name" ReadOnly="True" SortExpression="Facility_FacilityName" />
                          <asp:BoundField DataField="Facility_FacilityCode" HeaderText="Facility Code" ReadOnly="True" SortExpression="Facility_FacilityCode" />
                          <asp:BoundField DataField="Facility_Type_Lookup_Name" HeaderText="Facility Type" ReadOnly="True" SortExpression="Facility_Type_Lookup_Name" />
                          <asp:BoundField DataField="Facility_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="Facility_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_List" runat="server" SelectCommand="spAdministration_Get_Facility_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_Facility_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="Id" QueryStringField="s_Facility_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FacilityCode" QueryStringField="s_Facility_FacilityCode" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FacilityType" QueryStringField="s_Facility_FacilityType" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="IsActive" QueryStringField="s_Facility_IsActive" Type="String" DefaultValue="" />
                        </SelectParameters>
                      </asp:SqlDataSource>
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
