<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_ListCategory_List" CodeBehind="Administration_ListCategory_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - List Category List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_ListCategory_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ListCategory_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ListCategory_List" AssociatedUpdatePanelID="UpdatePanel_ListCategory_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ListCategory_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search List Categories
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
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Form_Id" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Parent
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Parent" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ListCategory_Parent" DataTextField="ListCategory_ParentName" DataValueField="ListCategory_Parent" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Parent_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Parent</asp:ListItem>
                        <asp:ListItem Value="-1">No Parent</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ListCategory_Parent" runat="server" SelectCommand="SELECT DISTINCT Administration_ListCategory.ListCategory_Parent, ListCategory_1.ListCategory_Name + ' (' + CASE WHEN ListCategory_1.ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_1.ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_ParentName FROM Administration_ListCategory LEFT OUTER JOIN Administration_ListCategory AS ListCategory_1 ON Administration_ListCategory.ListCategory_Parent = ListCategory_1.ListCategory_Id WHERE Administration_ListCategory.ListCategory_Parent != -1 AND (ListCategory_1.Form_Id = @Form_Id OR @Form_Id = 0) ORDER BY ListCategory_1.ListCategory_Name + ' (' + CASE WHEN ListCategory_1.ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_1.ListCategory_IsActive = 0 THEN 'No' END + ')'">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Name
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Id" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ListCategory_Id" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id">
                        <asp:ListItem Value="">Select Name</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ListCategory_Id" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name , ListCategory_Parent FROM Administration_ListCategory WHERE (Form_Id = @Form_Id OR @Form_Id = 0) AND (ListCategory_Parent = @ListCategory_Parent OR @ListCategory_Parent = 0) ORDER BY ListCategory_Name">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" />
                          <asp:Parameter Name="ListCategory_Parent" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Linked Category
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_LinkedCategory" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                      </asp:DropDownList>
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
                    <td>List of List Categories
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
                      <asp:GridView ID="GridView_ListCategory_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_ListCategory_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_ListCategory_List_PreRender" OnDataBound="GridView_ListCategory_List_DataBound" OnRowCreated="GridView_ListCategory_List_RowCreated">
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
                              <%=GridView_ListCategory_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New List Category" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New List Category" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("ListCategory_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="ListCategory_Id" HeaderText="Id" ReadOnly="True" SortExpression="ListCategory_Id" />
                          <asp:BoundField DataField="ListCategory_Name" HeaderText="Name" ReadOnly="True" SortExpression="ListCategory_Name" />
                          <asp:BoundField DataField="ListCategory_ParentName" HeaderText="Parent" ReadOnly="True" SortExpression="ListCategory_ParentName" />
                          <asp:BoundField DataField="ListCategory_LinkedCategory" HeaderText="Linked" ReadOnly="True" SortExpression="ListCategory_LinkedCategory" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="ListCategory_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="ListCategory_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_ListCategory_List" runat="server" SelectCommand="spAdministration_Get_ListCategory_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_ListCategory_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="Id" QueryStringField="s_ListCategory_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="Parent" QueryStringField="s_ListCategory_Parent" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="LinkedCategory" QueryStringField="s_ListCategory_LinkedCategory" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="IsActive" QueryStringField="s_ListCategory_IsActive" Type="String" DefaultValue="" />
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
