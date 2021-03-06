﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Facility_Unit" CodeBehind="Administration_Facility_Unit.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Facility Unit</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Facility_Unit.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Facility_Unit" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Facility_Unit" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Facility_Unit" AssociatedUpdatePanelID="UpdatePanel_Facility_Unit">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Facility_Unit" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table>
            <tr>
              <td style="vertical-align: top;">
                <table class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Search Facilities
                          </td>
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
                            <asp:DropDownList ID="DropDownList_FacilityId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_Id" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                              <asp:ListItem Value="">Select Facility</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource_Facility_Id" runat="server" SelectCommand="SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
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
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>List of Facilities
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
                            <asp:GridView ID="GridView_Facility_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Facility_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Facility_List_PreRender" OnDataBound="GridView_Facility_List_DataBound" OnRowCreated="GridView_Facility_List_RowCreated">
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
                                    <td style="text-align: center;">&nbsp;
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
                                <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Facility_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="Facility_IsActive" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_Facility_List" runat="server" SelectCommand="SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName , CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END AS Facility_IsActive FROM vAdministration_Facility_All WHERE (Facility_Id = @FacilityId OR @FacilityId = 0) ORDER BY Facility_FacilityDisplayName" OnSelected="SqlDataSource_Facility_List_Selected">
                              <SelectParameters>
                                <asp:QueryStringParameter Name="FacilityId" QueryStringField="s_Facility_Id" Type="String" DefaultValue="" />
                              </SelectParameters>
                            </asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="width: 15px;">&nbsp;
              </td>
              <td style="width: 5px; background-color: #b0262e;">&nbsp;
              </td>
              <td style="width: 5px; background-color: #003768;">&nbsp;
              </td>
              <td style="width: 15px;">&nbsp;
              </td>
              <td style="vertical-align: top;">
                <table id="TableFacilityUnit" runat="server" class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Facility Units
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table class="Table_Body">
                        <tr>
                          <td style="width: 75px;">Facility Name
                          </td>
                          <td>
                            <asp:Label ID="Label_FacilityName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 75px;">Unit Name
                          </td>
                          <td style="padding: 0px; border-left-width: 0px; border-top-width: 1px;">
                            <div style="max-height: 580px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                              <asp:CheckBoxList ID="CheckBoxList_Unit" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_Unit" DataTextField="Unit_Name" DataValueField="Unit_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="2" RepeatLayout="Table" OnDataBound="CheckBoxList_Unit_DataBound">
                              </asp:CheckBoxList>
                              <asp:SqlDataSource ID="SqlDataSource_Unit" runat="server" SelectCommand="SELECT DISTINCT Unit_Id , Unit_Name + ' (' + CASE WHEN Unit_IsActive = 1 THEN 'Yes' WHEN Unit_IsActive = 0 THEN 'No' END + ')' AS Unit_Name FROM vAdministration_Unit_All ORDER BY Unit_Name + ' (' + CASE WHEN Unit_IsActive = 1 THEN 'Yes' WHEN Unit_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                            </div>
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
                            <asp:Button ID="Button_FacilityUnitClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_FacilityUnitClear_Click" />&nbsp;
                          <asp:Button ID="Button_FacilityUnitUpdate" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_FacilityUnitUpdate_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
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
