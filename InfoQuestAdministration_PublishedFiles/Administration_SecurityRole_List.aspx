<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_SecurityRole_List" CodeBehind="Administration_SecurityRole_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Security Role List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SecurityRole_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SecurityRole_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SecurityRole_List" AssociatedUpdatePanelID="UpdatePanel_SecurityRole_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SecurityRole_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search Security Roles
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Form Name
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FormId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Form_Id" DataTextField="Form_Name" DataValueField="Form_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_FormId_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Form</asp:ListItem>
                        <asp:ListItem Value="-1">No Parent</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Form_Id" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Name
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Id" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_SecurityRole_Id" DataTextField="SecurityRole_Name" DataValueField="SecurityRole_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_SecurityRole_Id" runat="server" SelectCommand="SELECT DISTINCT SecurityRole_Id , SecurityRole_Name + ' (' + CAST(SecurityRole_Rank AS NVARCHAR(MAX)) + ') (' + CASE WHEN SecurityRole_IsActive = 1 THEN 'Yes' WHEN SecurityRole_IsActive = 0 THEN 'No' END + ')' AS SecurityRole_Name FROM Administration_SecurityRole WHERE (Form_Id = @Form_Id OR @Form_Id = 0) ORDER BY SecurityRole_Name">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
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
                    <td>List of Security Roles
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
                      <asp:GridView ID="GridView_SecurityRole_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SecurityRole_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_SecurityRole_List_PreRender" OnDataBound="GridView_SecurityRole_List_DataBound" OnRowCreated="GridView_SecurityRole_List_RowCreated">
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
                              <%=GridView_SecurityRole_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Security Role" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Security Role" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("SecurityRole_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="SecurityRole_Id" HeaderText="Id" ReadOnly="True" SortExpression="SecurityRole_Id" />
                          <asp:BoundField DataField="SecurityRole_Name" HeaderText="Name" ReadOnly="True" SortExpression="SecurityRole_Name" />
                          <asp:BoundField DataField="SecurityRole_Rank" HeaderText="Rank" ReadOnly="True" SortExpression="SecurityRole_Rank" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="SecurityRole_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="SecurityRole_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SecurityRole_List" runat="server" SelectCommand="spAdministration_Get_SecurityRole_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_SecurityRole_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="Id" QueryStringField="s_SecurityRole_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="IsActive" QueryStringField="s_SecurityRole_IsActive" Type="String" DefaultValue="" />
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
