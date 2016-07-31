<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_TransparencyRegister_List.aspx.cs" Inherits="InfoQuestForm.Form_TransparencyRegister_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Transparency Register List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_TransparencyRegister_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_TransparencyRegister_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_TransparencyRegister_List" AssociatedUpdatePanelID="UpdatePanel_TransparencyRegister_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_TransparencyRegister_List" runat="server">
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
                    <td colspan="2">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>Firstname or Lastname
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_Name" runat="server" Width="400px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Employee Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_EmployeeNumber" runat="server" Width="400px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Form Status
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Status" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Status</asp:ListItem>
                        <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr id="ShowAll" runat="server">
                    <td>Show All</td>
                    <td><asp:CheckBox ID="CheckBox_ShowAll" runat="server" /></td>
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
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_TransparencyRegister_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_TransparencyRegister_List" Width="900px" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_TransparencyRegister_List_PreRender" OnDataBound="GridView_TransparencyRegister_List_DataBound" OnRowCreated="GridView_TransparencyRegister_List_RowCreated">
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
                          <%=GridView_TransparencyRegister_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10" style="text-align:center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Transparency Register" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                                <asp:Button ID="Button_Export" runat="server" Text="Export to Excel" CssClass="Controls_Button" OnClick="Button_Export_Click" />
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Transparency Register" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("TransparencyRegister_Id"), Eval("TransparencyRegister_Status"), Eval("EmployeeManager")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate>
                              <asp:Label ID="Label_Employee" Text='<%# Eval("TransparencyRegister_FirstName") + " " + Eval("TransparencyRegister_LastName") + " (" + Eval("TransparencyRegister_EmployeeNumber") + ")" %>' runat="server"></asp:Label>
                            </ItemTemplate>
                          </asp:TemplateField>
								          <%--<asp:BoundField DataField="TransparencyRegister_JobTitle" HeaderText="JobTitle" ReadOnly="True" SortExpression="TransparencyRegister_JobTitle" />--%>
								          <asp:BoundField DataField="TransparencyRegister_Department" HeaderText="Employee Department" ReadOnly="True" SortExpression="TransparencyRegister_Department" />
                          <asp:TemplateField HeaderText="Manager">
                            <ItemTemplate>
                              <asp:Label ID="Label_Manager" Text='<%# Eval("TransparencyRegister_ManagerFirstName") + " " + Eval("TransparencyRegister_ManagerLastName") + " (" + Eval("TransparencyRegister_ManagerEmployeeNumber") + ")" %>' runat="server"></asp:Label>
                            </ItemTemplate>
                          </asp:TemplateField>
								          <asp:BoundField DataField="TransparencyRegister_DeclarationDate" HeaderText="Declaration Date" ReadOnly="True" SortExpression="TransparencyRegister_DeclarationDate" />
								          <asp:BoundField DataField="TransparencyRegister_Status" HeaderText="Status" ReadOnly="True" SortExpression="TransparencyRegister_Status" />
								          <asp:BoundField DataField="TransparencyRegister_StatusDate" HeaderText="Status Date" ReadOnly="True" SortExpression="TransparencyRegister_StatusDate" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_List" runat="server" OnSelected="SqlDataSource_TransparencyRegister_List_Selected"></asp:SqlDataSource>
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
