<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_SecurityAccess_List" CodeBehind="Administration_SecurityAccess_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Access List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityAccess_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityAccess_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityAccess_List" AssociatedUpdatePanelID="UpdatePanel_SecurityAccess_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityAccess_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search Security Access
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>UserName
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_SecurityUserUserName" runat="server" Width="200px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Display Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_SecurityUserDisplayName" runat="server" Width="200px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FacilityId" runat="server" AutoPostBack="true" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_Id" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" OnSelectedIndexChanged="DropDownList_FacilityId_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                        <asp:ListItem Value="0">All Facilities (Form Owners and Form Administrators)</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_Id" runat="server" SelectCommand="SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName FROM vAdministration_Facility_Form_All ORDER BY Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Form
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FormId" runat="server" AutoPostBack="true" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Form_Id" DataTextField="Form_Name" DataValueField="Form_Id" OnSelectedIndexChanged="DropDownList_FormId_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Form</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Form_Id" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name FROM vAdministration_Form_All WHERE @Facility_Id = 0 OR Form_Id IN ( SELECT Form_Id FROM vAdministration_Facility_Form_All WHERE Facility_Id = @Facility_Id OR @Facility_Id = 0 ) ORDER BY Form_Name">
                        <SelectParameters>
                          <asp:Parameter Name="Facility_Id" Type="String" DefaultValue="0" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Security Role
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_SecurityRoleId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_SecurityRole_Id" DataTextField="SecurityRole_Name" DataValueField="SecurityRole_Id">
                        <asp:ListItem Value="">Select Role</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_SecurityRole_Id" runat="server" SelectCommand="SELECT SecurityRole_Id , SecurityRole_Name FROM vAdministration_SecurityRole_All WHERE Form_Id = @Form_Id ORDER BY SecurityRole_Name">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" DefaultValue="" />
                        </SelectParameters>
                      </asp:SqlDataSource>
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
                    <td>List of Security Access
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
                      <asp:GridView ID="GridView_SecurityAccess_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SecurityAccess_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_SecurityAccess_List_PreRender" OnDataBound="GridView_SecurityAccess_List_DataBound" OnRowCreated="GridView_SecurityAccess_List_RowCreated">
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
                              <%=GridView_SecurityAccess_List.PageCount%>
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
                                <asp:Button ID="Button_Update" runat="server" Text="Update Security Access" CssClass="Controls_Button" OnClick="Button_Update_Click" />&nbsp;
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
                                <asp:Button ID="Button_Update" runat="server" Text="Update Security Access" CssClass="Controls_Button" OnClick="Button_Update_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("SecurityUser_Id"), Eval("SecurityUser_UserName")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="SecurityUser_UserName" HeaderText="UserName" ReadOnly="True" SortExpression="SecurityUser_UserName" />
                          <asp:BoundField DataField="SecurityUser_DisplayName" HeaderText="Display Name" ReadOnly="True" SortExpression="SecurityUser_DisplayName" />
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="SecurityRole_Name" HeaderText="Security Role" ReadOnly="True" SortExpression="SecurityRole_Name" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SecurityAccess_List" runat="server" SelectCommand="spAdministration_Get_SecurityAccess_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_SecurityAccess_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="SecurityUserUserName" QueryStringField="s_SecurityUserUserName" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="SecurityUserDisplayName" QueryStringField="s_SecurityUserDisplayName" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FacilityId" QueryStringField="s_Facility_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="SecurityRoleId" QueryStringField="s_SecurityRole_Id" Type="String" DefaultValue="" />
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
