<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_List.aspx.cs" Inherits="InfoQuestForm.Form_IPS_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_List" AssociatedUpdatePanelID="UpdatePanel_IPS_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_List" runat="server">
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
                    <td colspan="4">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Facility
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_IPS_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Report Number
                    </td>
                    <td colspan="3">
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Patient Visit Number
                    </td>
                    <td style="width: 150px">
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                    <td style="width: 150px">Patient Surname, Name
                    </td>
                    <td style="width: 150px">
                      <asp:TextBox ID="TextBox_PatientName" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Category
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Category" runat="server" DataSourceID="SqlDataSource_IPS_Category" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Category</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Category" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Type
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Type" runat="server" DataSourceID="SqlDataSource_IPS_Type" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Type" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Completed
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_InfectionCompleted" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">HAI Investigation
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_HAI" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Complete">Complete</asp:ListItem>
                        <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                        <asp:ListItem Value="Not Required">Not Required</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 150px">Is Active
                    </td>
                    <td colspan="3">
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_OnClick" />&nbsp;
                    <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 900px;">
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
                      <asp:GridView ID="GridView_IPS_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_IPS_List_PreRender" OnDataBound="GridView_IPS_List_DataBound" OnRowCreated="GridView_IPS_List_RowCreated">
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
                              <%=GridView_IPS_List.PageCount%>
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_OnClick" />
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("IPS_VisitInformation_Id"), Eval("IPS_Infection_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="IPS_Infection_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="IPS_Infection_ReportNumber" />
                          <asp:BoundField DataField="IPS_VisitInformation_VisitNumber" HeaderText="Visit Number" ReadOnly="True" SortExpression="IPS_VisitInformation_VisitNumber" />
                          <asp:BoundField DataField="PatientInformation_Name" HeaderText="Surname, Name" ReadOnly="True" SortExpression="PatientInformation_Name" />
                          <asp:BoundField DataField="IPS_Infection_Category_Name" HeaderText="Category" ReadOnly="True" SortExpression="IPS_Infection_Category_Name" />
                          <asp:BoundField DataField="IPS_Infection_Type_Name" HeaderText="Type" ReadOnly="True" SortExpression="IPS_Infection_Type_Name" />
                          <asp:BoundField DataField="IPS_Infection_Site_Name" HeaderText="Site" ReadOnly="True" SortExpression="IPS_Infection_Site_Name" />
                          <asp:BoundField DataField="IPS_Infection_Completed" HeaderText="Infection" ReadOnly="True" SortExpression="IPS_Infection_Completed" />
                          <asp:BoundField DataField="HAI" HeaderText="HAI Investigation" ReadOnly="True" SortExpression="HAI" />
                          <asp:BoundField DataField="IPS_Infection_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_Infection_IsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_List" runat="server" OnSelected="SqlDataSource_IPS_List_Selected"></asp:SqlDataSource>
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
