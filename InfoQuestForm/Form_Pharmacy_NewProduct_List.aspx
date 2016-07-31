<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Pharmacy_NewProduct_List" CodeBehind="Form_Pharmacy_NewProduct_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy - New Product Code Request List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Pharmacy_NewProduct_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Pharmacy_NewProduct_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Pharmacy_NewProduct_List" AssociatedUpdatePanelID="UpdatePanel_Pharmacy_NewProduct_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Pharmacy_NewProduct_List" runat="server">
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
                    <td>Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Pharmacy_NewProduct_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Report Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Manufacturer / Supplier
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Manufacturer" runat="server" CssClass="Controls_DropDownList" DataSourceID="SqlDataSource_Pharmacy_NewProduct_Manufacturer" AppendDataBoundItems="true" DataTextField="Pharmacy_Supplier_Lookup_Description" DataValueField="Pharmacy_Supplier_Lookup_Id">
                        <asp:ListItem Value="">Select Supplier</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_Manufacturer" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_CreatedBy" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Pharmacy_NewProduct_CreatedBy" DataTextField="NewProduct_CreatedBy" DataValueField="NewProduct_CreatedBy">
                        <asp:ListItem Value="">Select User</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_CreatedBy" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_ModifiedBy" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Pharmacy_NewProduct_ModifiedBy" DataTextField="NewProduct_ModifiedBy" DataValueField="NewProduct_ModifiedBy">
                        <asp:ListItem Value="">Select User</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_ModifiedBy" runat="server"></asp:SqlDataSource>
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
                  <tr>
                    <td>Progress Status
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FeedbackProgressStatus" runat="server" CssClass="Controls_DropDownList" DataSourceID="SqlDataSource_Pharmacy_NewProduct_Feedback_ProgressStatus" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id">
                        <asp:ListItem Value="">Select Status</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_Feedback_ProgressStatus" runat="server"></asp:SqlDataSource>
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
                      <asp:GridView ID="GridView_Pharmacy_NewProduct_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Pharmacy_NewProduct_List" CssClass="GridView" Width="1000px" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Pharmacy_NewProduct_List_PreRender" OnDataBound="GridView_Pharmacy_NewProduct_List_DataBound" OnRowCreated="GridView_Pharmacy_NewProduct_List_RowCreated">
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
                              <%=GridView_Pharmacy_NewProduct_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Product Code Request" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                                <%--<asp:Button ID="Button_GoToPF" runat="server" Text="Go to Products and Formularies" CssClass="Controls_Button" OnClick="Button_GoToPF_OnClick" />--%>
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
                              <td></td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Product Code Request" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                            <%--<asp:Button ID="Button_GoToPF" runat="server" Text="Go to Products and Formularies" CssClass="Controls_Button" OnClick="Button_GoToPF_OnClick" />--%>
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("NewProduct_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="NewProduct_Date" HeaderText="Date" ReadOnly="True" SortExpression="NewProduct_Date" />
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Pharmacy Name" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="NewProduct_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="NewProduct_ReportNumber" />
                          <asp:BoundField DataField="NewProduct_Manufacturer_Name" HeaderText="Manufacturer / Supplier" ReadOnly="True" SortExpression="NewProduct_Manufacturer_Name" />
                          <asp:BoundField DataField="NewProduct_SupplierCatalogNumber" HeaderText="Supplier Catalog Number" ReadOnly="True" SortExpression="NewProduct_SupplierCatalogNumber" />
                          <asp:BoundField DataField="NewProduct_NappiCode" HeaderText="NAPPI code" ReadOnly="True" SortExpression="NewProduct_NappiCode" />
                          <asp:BoundField DataField="NewProduct_CreatedBy" HeaderText="Created By" ReadOnly="True" SortExpression="NewProduct_CreatedBy" />
                          <asp:BoundField DataField="NewProduct_ModifiedBy" HeaderText="Modified By" ReadOnly="True" SortExpression="NewProduct_ModifiedBy" />
                          <asp:BoundField DataField="NewProduct_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="NewProduct_IsActive" />
                          <asp:BoundField DataField="NewProduct_Feedback_ProgressStatus_Name" HeaderText="Progress Status" ReadOnly="True" SortExpression="NewProduct_Feedback_ProgressStatus_Name" />
                          <asp:BoundField DataField="NewProduct_Feedback_ProgressStatus_List" HeaderText="" ReadOnly="True" SortExpression="NewProduct_Feedback_ProgressStatus_List" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_List" runat="server" OnSelected="SqlDataSource_Pharmacy_NewProduct_List_Selected"></asp:SqlDataSource>
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
