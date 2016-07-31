<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_PageView_List" CodeBehind="Administration_PageView_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Page View List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PageView_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PageView_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PageView_List" AssociatedUpdatePanelID="UpdatePanel_PageView_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PageView_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search Page Views
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
                    <td>Page
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_Page" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>URL
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_URL" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>UserName
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_UserName" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td>From&nbsp;
                    <asp:TextBox ID="TextBox_DateFrom" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_DateFrom" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_DateFrom" runat="server" CssClass="Calendar" TargetControlID="TextBox_DateFrom" Format="yyyy/MM/dd" PopupButtonID="ImageButton_DateFrom">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_DateFrom" runat="server" TargetControlID="TextBox_DateFrom" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;&nbsp;To&nbsp;
                    <asp:TextBox ID="TextBox_DateTo" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_DateTo" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_DateTo" runat="server" CssClass="Calendar" TargetControlID="TextBox_DateTo" Format="yyyy/MM/dd" PopupButtonID="ImageButton_DateTo">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_DateTo" runat="server" TargetControlID="TextBox_DateTo" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
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
          <table id="TableSearch" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>List of Page Views
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
                      <asp:GridView ID="GridView_PageView_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PageView_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_PageView_List_PreRender" OnDataBound="GridView_PageView_List_DataBound" OnRowCreated="GridView_PageView_List_RowCreated">
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
                              <%=GridView_PageView_List.PageCount%>
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
                          <asp:BoundField DataField="PageView_Id" HeaderText="Id" ReadOnly="True" SortExpression="PageView_Id" />
                          <asp:BoundField DataField="PageView_Page" HeaderText="Page" ReadOnly="True" SortExpression="PageView_Page" />
                          <asp:BoundField DataField="PageView_URL" HeaderText="URL" ReadOnly="True" SortExpression="PageView_URL" />
                          <asp:BoundField DataField="PageView_Description" HeaderText="Description" ReadOnly="True" SortExpression="PageView_Description" />
                          <asp:BoundField DataField="PageView_UserName" HeaderText="UserName" ReadOnly="True" SortExpression="PageView_UserName" />
                          <asp:BoundField DataField="PageView_Date" HeaderText="Date" ReadOnly="True" SortExpression="PageView_Date" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PageView_List" runat="server" SelectCommand="spAdministration_Get_PageView_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_PageView_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="Page" QueryStringField="s_PageView_Page" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="URL" QueryStringField="s_PageView_URL" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="UserName" QueryStringField="s_PageView_UserName" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="DateFrom" QueryStringField="s_PageView_DateFrom" Type="DateTime" DefaultValue="" />
                          <asp:QueryStringParameter Name="DateTo" QueryStringField="s_PageView_DateTo" Type="DateTime" DefaultValue="" />
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
