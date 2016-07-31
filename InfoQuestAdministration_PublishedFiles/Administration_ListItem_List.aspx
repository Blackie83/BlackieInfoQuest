<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_ListItem_List" CodeBehind="Administration_ListItem_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - List Item List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_ListItem_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ListItem_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ListItem_List" AssociatedUpdatePanelID="UpdatePanel_ListItem_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ListItem_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search List Items
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
                    <td>List Category
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_ListCategoryId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ListCategory_Id" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_ListCategoryId_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Category</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ListCategory_Id" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name FROM vAdministration_ListCategory_All WHERE (Form_Id = @Form_Id OR @Form_Id = 0) ORDER BY ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')'">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Parent
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Parent" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ListItem_Parent" DataTextField="ListItem_ParentName" DataValueField="ListItem_Parent">
                        <asp:ListItem Value="">Select Parent</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ListItem_Parent" runat="server" SelectCommand="SELECT DISTINCT Administration_ListItem.ListItem_Parent , CASE WHEN LEN(ListItem_1.ListItem_Name) > 100 THEN LEFT(ListItem_1.ListItem_Name,100) + '.....' + ' (' + CASE WHEN ListItem_1.ListItem_IsActive = 1 THEN 'Yes' WHEN ListItem_1.ListItem_IsActive = 0 THEN 'No' END + ')' ELSE ListItem_1.ListItem_Name + ' (' + CASE WHEN ListItem_1.ListItem_IsActive = 1 THEN 'Yes' WHEN ListItem_1.ListItem_IsActive = 0 THEN 'No' END + ')' END AS ListItem_ParentName FROM vAdministration_ListCategory_All RIGHT OUTER JOIN Administration_ListItem ON vAdministration_ListCategory_All.ListCategory_Id = Administration_ListItem.ListCategory_Id LEFT OUTER JOIN Administration_ListItem AS ListItem_1 ON Administration_ListItem.ListItem_Parent = ListItem_1.ListItem_Id WHERE Administration_ListItem.ListItem_Parent != -1 AND (vAdministration_ListCategory_All.Form_Id = @Form_Id OR @Form_Id = 0) AND (vAdministration_ListCategory_All.ListCategory_Id = @ListCategory_Id OR @ListCategory_Id = 0) ORDER BY CASE WHEN LEN(ListItem_1.ListItem_Name) > 100 THEN LEFT(ListItem_1.ListItem_Name,100) + '.....' + ' (' + CASE WHEN ListItem_1.ListItem_IsActive = 1 THEN 'Yes' WHEN ListItem_1.ListItem_IsActive = 0 THEN 'No' END + ')' ELSE ListItem_1.ListItem_Name + ' (' + CASE WHEN ListItem_1.ListItem_IsActive = 1 THEN 'Yes' WHEN ListItem_1.ListItem_IsActive = 0 THEN 'No' END + ')' END">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" />
                          <asp:Parameter Name="ListCategory_Id" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_Name" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
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
                    <td>List of List Items
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
                      <asp:GridView ID="GridView_ListItem_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_ListItem_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_ListItem_List_PreRender" OnDataBound="GridView_ListItem_List_DataBound" OnRowCreated="GridView_ListItem_List_RowCreated">
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
                              <%=GridView_ListItem_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New List Item" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New List Item" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("ListItem_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="ListItem_Id" HeaderText="Id" ReadOnly="True" SortExpression="ListItem_Id" />
                          <asp:BoundField DataField="ListItem_Name" HeaderText="Name" ReadOnly="True" SortExpression="ListItem_Name" />
                          <asp:BoundField DataField="ListItem_ParentName" HeaderText="Parent" ReadOnly="True" SortExpression="ListItem_ParentName" />
                          <asp:BoundField DataField="ListCategory_Name" HeaderText="List Category" ReadOnly="True" SortExpression="ListCategory_Name" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="ListItem_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="ListItem_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_ListItem_List" runat="server" SelectCommand="spAdministration_Get_ListItem_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_ListItem_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="Name" QueryStringField="s_ListItem_Name" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="Parent" QueryStringField="s_ListItem_Parent" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="ListCategoryId" QueryStringField="s_ListCategory_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="IsActive" QueryStringField="s_ListItem_IsActive" Type="String" DefaultValue="" />
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
