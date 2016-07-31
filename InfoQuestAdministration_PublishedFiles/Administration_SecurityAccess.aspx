<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_SecurityAccess" CodeBehind="Administration_SecurityAccess.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Access</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_SecurityAccess.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityAccess" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityAccess" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityAccess" AssociatedUpdatePanelID="UpdatePanel_SecurityAccess">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityAccess" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table style="width: 1000px">
            <tr>
              <td style="width: 310px; vertical-align: top;">
                <table class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Search Security Users
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table class="Table_Body">
                        <tr>
                          <td colspan="2">
                            <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td>Username
                          </td>
                          <td>
                            <asp:TextBox ID="TextBox_UserName" runat="server" CssClass="Controls_TextBox" Width="200px"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>Name or Surname
                          </td>
                          <td>
                            <asp:TextBox ID="TextBox_DisplayName" runat="server" CssClass="Controls_TextBox" Width="200px"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>Employee Number
                          </td>
                          <td>
                            <asp:TextBox ID="TextBox_EmployeeNumber" runat="server" CssClass="Controls_TextBox" Width="200px"></asp:TextBox>
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
              </td>
              <td style="width: 20px;" rowspan="3">&nbsp;
              </td>
              <td style="width: 670px; vertical-align: top;">
                <table id="TableSecurityUsers" runat="server" class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>List of Security Users
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
                          <asp:Label ID="Label_TotalRecords_SecurityUser" runat="server" Text=""></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td style="padding: 0px;">
                            <asp:GridView ID="GridView_SecurityUser_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SecurityUser_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="5" OnPreRender="GridView_SecurityUser_List_PreRender" OnDataBound="GridView_SecurityUser_List_DataBound" OnRowCreated="GridView_SecurityUser_List_RowCreated">
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
                                        <asp:ListItem Value="5">5</asp:ListItem>
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
                                    <%=GridView_SecurityUser_List.PageCount%>
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
                                    <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("SecurityUser_Id")) %>' runat="server"></asp:HyperLink>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SecurityUser_Id" HeaderText="Id" ReadOnly="True" SortExpression="SecurityUser_Id" />
                                <asp:BoundField DataField="SecurityUser_UserName" HeaderText="UserName" ReadOnly="True" SortExpression="SecurityUser_UserName" />
                                <asp:BoundField DataField="SecurityUser_DisplayName" HeaderText="Display Name" ReadOnly="True" SortExpression="SecurityUser_DisplayName" />
                                <asp:BoundField DataField="SecurityUser_EmployeeNumber" HeaderText="Employee Number" ReadOnly="True" SortExpression="SecurityUser_EmployeeNumber" />
                                <asp:BoundField DataField="SecurityUser_ManagerUserName" HeaderText="Manager UserName" ReadOnly="True" SortExpression="SecurityUser_ManagerUserName" />
                                <asp:BoundField DataField="SecurityUser_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="SecurityUser_IsActive" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_SecurityUser_List" runat="server" SelectCommand="spAdministration_Get_SecurityUser_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_SecurityUser_List_Selected">
                              <SelectParameters>
                                <asp:QueryStringParameter Name="UserName" QueryStringField="s_SecurityAccess_UserName" Type="String" DefaultValue="" />
                                <asp:QueryStringParameter Name="DisplayName" QueryStringField="s_SecurityAccess_DisplayName" Type="String" DefaultValue="" />
                                <asp:QueryStringParameter Name="EmployeeNumber" QueryStringField="s_SecurityAccess_EmployeeNumber" Type="String" DefaultValue="" />
                                <asp:QueryStringParameter Name="ManagerUserName" QueryStringField="Empty" Type="String" DefaultValue="" />
                                <asp:QueryStringParameter Name="IsActive" QueryStringField="Empty" Type="String" DefaultValue="" />
                              </SelectParameters>
                            </asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td style="height: 20px;">&nbsp;
              </td>
              <td style="height: 20px;">&nbsp;
              </td>
            </tr>
            <tr>
              <td style="width: 310px; vertical-align: top;">
                <table id="TableSecurityAccess1" runat="server" class="Table">
                  <tr>
                    <td style="vertical-align: top;">
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
                          <asp:Label ID="Label_TotalRecords_SecurityAccess" runat="server" Text=""></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td style="padding: 0px;">
                            <asp:GridView ID="GridView_SecurityAccess_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SecurityAccess_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_SecurityAccess_List_PreRender" OnRowCreated="GridView_SecurityAccess_List_RowCreated">
                              <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                              <PagerTemplate>
                                <table class="GridView_PagerStyle">
                                  <tr>
                                    <td colspan="9">
                                      <asp:Button ID="Button_ClearUser" runat="server" Text="Clear User" CssClass="Controls_Button" OnClick="Button_ClearUser_Click" />&nbsp;
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
                                      <asp:Button ID="Button_ClearUser" runat="server" Text="Clear User" CssClass="Controls_Button" OnClick="Button_ClearUser_Click" />&nbsp;
                                    </td>
                                  </tr>
                                </table>
                              </EmptyDataTemplate>
                              <Columns>
                                <asp:TemplateField HeaderText="">
                                  <ItemTemplate>
                                    <asp:HyperLink ID="Link" Text='<%# GetLinkSecurityAccess(Eval("SecurityUser_Id") , Eval("Facility_Id")) %>' runat="server"></asp:HyperLink>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SecurityUser_UserName" HeaderText="UserName" ReadOnly="True" SortExpression="SecurityUser_UserName" />
                                <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_SecurityAccess_List" runat="server" SelectCommand="SELECT DISTINCT SecurityUser_Id , SecurityUser_UserName , Facility_Id , CASE WHEN Facility_FacilityDisplayName IS NULL THEN 'Administrator Access' ELSE Facility_FacilityDisplayName END AS Facility_FacilityDisplayName FROM vAdministration_SecurityAccess_All WHERE SecurityUser_Id = @SecurityUser_Id ORDER BY Facility_FacilityDisplayName" OnSelected="SqlDataSource_SecurityAccess_List_Selected">
                              <SelectParameters>
                                <asp:QueryStringParameter Name="SecurityUser_Id" QueryStringField="SecurityUser_Id" Type="String" DefaultValue="" />
                              </SelectParameters>
                            </asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="width: 670px; vertical-align: top;">
                <table id="TableSecurityAccess2" runat="server" class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Add / Update Security Access
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table class="Table_Body">
                        <tr>
                          <td colspan="2">
                            <asp:Label ID="Label_InvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 75px;">Security User
                          </td>
                          <td style="width: 580px;">
                            <asp:Label ID="Label_SecurityUser" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 75px;" id="FormFacility" runat="server">Facility
                          </td>
                          <td style="width: 580px;">
                            <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" Width="100%" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                              <asp:ListItem Value="">Select Facility</asp:ListItem>
                              <asp:ListItem Value="0">All Facilities (Form Owners and Form Administrators)</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource_Facility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName + ' : ' + CASE CAST(Facility_IsActive AS NVARCHAR(MAX)) WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName" SelectCommandType="Text"></asp:SqlDataSource>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">Only one Security Role can be selected per Form
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 75px;" id="FormSecurityRole" runat="server">Security Role
                          </td>
                          <td style="width: 580px; padding: 0px; border-left-width: 0px; border-top-width: 1px;">
                            <div style="max-height: 400px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                              <asp:CheckBoxList ID="CheckBoxList_SecurityRole" runat="server" AppendDataBoundItems="true" Width="100%" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_SecurityRole" DataTextField="SecurityRole_Name" DataValueField="SecurityRole_Id" CellPadding="0" CellSpacing="0" RepeatLayout="Table" OnDataBound="CheckBoxList_SecurityRole_DataBound">
                              </asp:CheckBoxList>
                              <asp:SqlDataSource ID="SqlDataSource_SecurityRole" runat="server" SelectCommand="spAdministration_Get_SecurityAccess_SecurityRole" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                  <asp:Parameter Name="FacilityId" Type="String" DefaultValue="" />
                                </SelectParameters>
                              </asp:SqlDataSource>
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
                            <asp:Button ID="Button_SecurityAccessClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_SecurityAccessClear_Click" />&nbsp;
                          <asp:Button ID="Button_SecurityAccessAdd" runat="server" Text="Add" CssClass="Controls_Button" OnClick="Button_SecurityAccessAdd_Click" />
                            <asp:Button ID="Button_SecurityAccessDelete" runat="server" Text="Delete" CssClass="Controls_Button" OnClick="Button_SecurityAccessDelete_Click" />&nbsp;
                          <asp:Button ID="Button_SecurityAccessUpdate" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_SecurityAccessUpdate_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
