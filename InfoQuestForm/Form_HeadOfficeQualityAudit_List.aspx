<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_HeadOfficeQualityAudit_List.aspx.cs" Inherits="InfoQuestForm.Form_HeadOfficeQualityAudit_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Head Office Quality Audit Finding List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_HeadOfficeQualityAudit_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_HeadOfficeQualityAudit_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_HeadOfficeQualityAudit_List" AssociatedUpdatePanelID="UpdatePanel_HeadOfficeQualityAudit_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_HeadOfficeQualityAudit_List" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
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
          <table class="Table" style="width: 1000px;">
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
                    <td style="width: 175px;">Facility
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Function
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Function" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_Function" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Function</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_Function" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Financial Year
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_FinancialYear" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_FinancialYear" AppendDataBoundItems="true" DataTextField="HQA_Finding_FinancialYear" DataValueField="HQA_Finding_FinancialYear" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Financial Year</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_FinancialYear" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Classification
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Classification" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_Classification" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Classification</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_Classification" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Tracking
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Tracking" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_Tracking" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Tracking</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_Tracking" runat="server"></asp:SqlDataSource>
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
          <table class="Table" style="width: 1000px;">
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
                      <asp:GridView ID="GridView_HeadOfficeQualityAudit_List" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_HeadOfficeQualityAudit_List_PreRender" OnDataBound="GridView_HeadOfficeQualityAudit_List_DataBound" OnRowCreated="GridView_HeadOfficeQualityAudit_List_RowCreated">
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
                              <%=GridView_HeadOfficeQualityAudit_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Finding" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Finding" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("HQA_Finding_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="HQA_Finding_Function_Name" HeaderText="Function" ReadOnly="True" SortExpression="HQA_Finding_Function_Name" />
                          <asp:BoundField DataField="HQA_Finding_FindingNo" HeaderText="Finding No" ReadOnly="True" SortExpression="HQA_Finding_FindingNo" />
                          <asp:BoundField DataField="HQA_Finding_FinancialYear" HeaderText="Financial Year" ReadOnly="True" SortExpression="HQA_Finding_FinancialYear" />
                          <asp:BoundField DataField="HQA_Finding_Criteria_Name" HeaderText="Criteria" ReadOnly="True" SortExpression="HQA_Finding_Criteria_Name" />
                          <asp:BoundField DataField="HQA_Finding_Classification_Name" HeaderText="Classification" ReadOnly="True" SortExpression="HQA_Finding_Classification_Name" />
                          <asp:BoundField DataField="HQA_Finding_Tracking_Name" HeaderText="Tracking" ReadOnly="True" SortExpression="HQA_Finding_Tracking_Name" />
                          <asp:BoundField DataField="HQA_Finding_Tracking_List" HeaderText="" ReadOnly="True" SortExpression="HQA_Finding_Tracking_List" />
                          <asp:BoundField DataField="HQA_Finding_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="HQA_Finding_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_List" runat="server" OnSelected="SqlDataSource_HeadOfficeQualityAudit_List_Selected"></asp:SqlDataSource>
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
