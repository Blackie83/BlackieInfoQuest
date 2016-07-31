<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration_SecurityAccess_SwitchAccess.aspx.cs" Inherits="InfoQuestAdministration.Administration_SecurityAccess_SwitchAccess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Access Switch Access</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityAccess_SwitchAccess" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityAccess_SwitchAccess" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityAccess_SwitchAccess" AssociatedUpdatePanelID="UpdatePanel_SecurityAccess_SwitchAccess">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityAccess_SwitchAccess" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Current Security Access Switched Access</td>
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
                      <asp:GridView ID="GridView_SwitchAccess_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SwitchAccess_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_SwitchAccess_List_PreRender" OnDataBound="GridView_SwitchAccess_List_DataBound" OnRowCreated="GridView_SwitchAccess_List_RowCreated">
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
                              <%=GridView_SwitchAccess_List.PageCount%>
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
                              <td>No Switched Access
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="SecurityUser_Id" HeaderText="User Id" ReadOnly="True" SortExpression="SecurityUser_Id" />
                          <asp:BoundField DataField="SecurityUser_UserName" HeaderText="User Name" ReadOnly="True" SortExpression="SecurityUser_UserName" />
                          <asp:BoundField DataField="SecurityUser_DisplayName" HeaderText="User Display Name" ReadOnly="True" SortExpression="SecurityUser_DisplayName" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="SecurityRole_Name" HeaderText="Security Role" ReadOnly="True" SortExpression="SecurityRole_Name" />
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SwitchAccess_List" runat="server" SelectCommand="spAdministration_Get_SecurityAccess_SwitchAccess_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_SwitchAccess_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Switch Access</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr id="SwitchAccess1" runat="server">
                    <td colspan="4">
                      <asp:Label ID="Label_ErrorMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr id="SwitchAccess2" runat="server">
                    <td style="width: 100px;">User Name A
                    </td>
                    <td style="width: 400px;">
                      <asp:DropDownList ID="DropDownList_UserName_A" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_UserName_A" DataTextField="SecurityUser_UserName" DataValueField="SecurityUser_UserName">
                        <asp:ListItem Value="">Select User Name A</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_UserName_A" runat="server" SelectCommand="SELECT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE SecurityRole_Id = 1"></asp:SqlDataSource>
                    </td>
                    <td style="width: 100px;">User Name B
                    </td>
                    <td style="width: 400px;">
                      <asp:DropDownList ID="DropDownList_UserName_B" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_UserName_B" DataTextField="SecurityUser_UserName" DataValueField="SecurityUser_UserName">
                        <asp:ListItem Value="">Select User Name B</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_UserName_B" runat="server" SelectCommand="SELECT 'LHC\infoquest_sa' AS SecurityUser_UserName"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr id="SwitchAccess3" runat="server">
                    <td style="text-align: center;" colspan="4">
                      <asp:Button ID="Button_SwitchAccess" runat="server" Text="Switch Access" CssClass="Controls_Button" OnClick="Button_SwitchAccess_Click" />&nbsp;
                    </td>
                  </tr>
                  <tr id="SwitchAccessBack1" runat="server">
                    <td style="text-align: center;" colspan="4">
                      <asp:Button ID="Button_SwitchAccessBack" runat="server" Text="Switch Access Back" CssClass="Controls_Button" OnClick="Button_SwitchAccessBack_Click" />&nbsp;
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
